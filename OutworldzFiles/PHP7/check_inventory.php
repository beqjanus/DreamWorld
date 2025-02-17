<?php

// Database setup, edit as needed
$database_info = array();
include "databaseinfo.php";

$database_info['hostname'] = $DB_HOST;
$database_info['database'] = $ROBUST_NAME;
$database_info['username'] = $DB_USER;
$database_info['password'] = $DB_PASSWORD;
////////////////////////////////////////////////

$errors = 0;
$wikilink = "http://opensimulator.org/wiki/Check_inventory_script";

// Handle passed arguments
if (isset($argv[1]) && isset($argv[2]))
{
	$firstname = $argv[1];
	$lastname = $argv[2];
	
	echo "Checking inventory of " . $firstname . " " . $lastname . "\n";
	echo "##############################################\n";
	
	$user = Get_useruuid($firstname, $lastname);
	Check_rootfolders($user);
	
	if (isset($argv[3]) && $argv[3] == "true")
		Check_duplicatefolders($user);
	
	report();	
}
else if (isset($argv[1]) && $argv[1] == "everyone")
{
	// Running through all users with warnings
	$everyone = Get_everyone();
	foreach ($everyone as $user)
	{
		$user = $user['PrincipalID'];
		echo "Checking user " . $user . "\n";
		echo "##############################################\n";
		Check_rootfolders($user);
		if (isset($argv[2]) && $argv[2] == "true")
			Check_duplicatefolders($user);
		echo "##############################################\n";
	}
	report();
}
else
{
	echo "Specify a user first and last name or everyone to run through all users!\n";
	exit(1);
}

// Functions
////////////////////

// Get all users
function Get_everyone()
{
	global $database_info;
	$connection = new mysqli($database_info['hostname'], $database_info['username'], $database_info['password'], $database_info['database']);
	$query = $connection->query("SELECT PrincipalID FROM `UserAccounts`;");
	if ($query->num_rows > 0)
	{
		echo "Found " . $query->num_rows . " users\n";
		return $query;
	}
}

// Fetch from useraccounts
function Get_useruuid($firstname, $lastname)
{
	global $database_info;
	$connection = new mysqli($database_info['hostname'], $database_info['username'], $database_info['password'], $database_info['database']);
	$query = $connection->query("SELECT PrincipalID FROM `UserAccounts` WHERE FirstName = '".$firstname."' AND LastName = '".$lastname."';");
	if ($query->num_rows == 1)
	{
		$row = $query->fetch_assoc();
		return $row['PrincipalID'];
	}
	else
		echo "User not found";
}

// Get parent folder
function Get_parentfolder($folderid)
{
	global $database_info;
	$connection = new mysqli($database_info['hostname'], $database_info['username'], $database_info['password'], $database_info['database']);
	$query = $connection->query("SELECT parentFolderID FROM `inventoryfolders` WHERE folderID = '".$folderid."';");
	if ($query->num_rows == 1)
	{
		$row = $query->fetch_assoc();
		return $row['parentFolderID'];
	}
}

// Get folder name
function Get_foldername($folderid)
{
	global $database_info;
	$connection = new mysqli($database_info['hostname'], $database_info['username'], $database_info['password'], $database_info['database']);
	$query = $connection->query("SELECT folderName FROM `inventoryfolders` WHERE folderID = '".$folderid."';");
	if ($query->num_rows == 1)
	{
		$row = $query->fetch_assoc();
		return $row['folderName'];
	}
}

// Check if there are folders of type 8 not named My Inventory
function Check_rootfolders($user_uuid)
{
	global $database_info;
	global $errors;
	$connection = new mysqli($database_info['hostname'], $database_info['username'], $database_info['password'], $database_info['database']);
	$query = $connection->query("SELECT * FROM `inventoryfolders` WHERE (`type` = '8' OR `type` = '9') AND agentID = '".$user_uuid."';");
    if ($query->num_rows > 0)
	{
		foreach ($query as $row)
		{
			$foldername = $row['folderName'];
			$folderuuid = $row['folderID'];
			if ($foldername != "My Inventory")
			{
				echo "Found root type folder outside of inventory structure: " . $foldername . " (" . $folderuuid . ")\n";
				$errors++;
			}
			if ($row['type'] == "9")
			{
				echo "Root type folder of wrong type: " . $row['type'] . "\n";
				$errors++;
			}
		}
	}
	else
		echo "No inventory found\n";
}

// Check if within an inventory all folders are exactly present once
function Check_duplicatefolders($user_uuid)
{
	global $database_info;
	global $errors;
	$folders = array();
	$connection = new mysqli($database_info['hostname'], $database_info['username'], $database_info['password'], $database_info['database']);
	$query = $connection->query("SELECT * FROM `inventoryfolders` WHERE `type` != '-1' AND agentID = '".$user_uuid."';");
	if ($query->num_rows > 0)
	{
		foreach ($query as $row)
		{
			// Stuff array key of type with all the folders found of that type
			$folders[$row['type']] = (isset($folders[$row['type']])) ? $folders[$row['type']] = $folders[$row['type']] . "," . $row['folderID'] : $folders[$row['type']] = $row['folderID'];
		}
		
		// Now iterate over that and output the bad ones
		foreach ($folders as $folder)
		{
			// If contains comma means more than one folder
			if (strpos($folder, ",") !== false)
			{
				$dup_folder = explode(",", $folder);
				foreach ($dup_folder as $folderid)
				{
					$foldername = Get_foldername($folderid);
					echo "Duplicate folders: " . $foldername . " (" . $folderid . ") parent: " . Get_parentfolder($folderid);
					$parentname = Get_foldername((Get_parentfolder($folderid)));
					if ($parentname != "My Inventory")
					{
						echo " not in root inventory folder, found in: " . $parentname . "\n";
					}
					else
						echo "\n";
				}
				$errors++;
			}
		}
	}
	else
		echo "No inventory found\n";
}

function report()
{
	global $errors;
	global $wikilink;
	echo "\nFound " . $errors . " potential inventory errors\n";
	echo "Here is how: ". $wikilink . " to resolve them.\n";
	$a = readline('Press Enter to quit');
}



?>