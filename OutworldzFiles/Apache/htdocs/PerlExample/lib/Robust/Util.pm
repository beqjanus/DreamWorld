package Robust::Util;
use strict;
use warnings;

# Read your Opensim database name, username, and password.

sub mysql_connect {
        
   use lib qw (. lib);
   use Config::IniFiles;
   use File::BOM;  # fixes a bug in Perl with UTF-8
   
   # get the path to the Settings.ini
   use Cwd;
   my $path = getcwd();
   
   $path =~ /(.*?\/Outworldzfiles)/i;
   my $file = $1 . '/Settings.ini';
      
   # Read the Right Thing from a unicode file with BOM:
   open(CONFIG , '<:via(File::BOM)', $file);   
   my $Config = Config::IniFiles->new(-file => *CONFIG);
   
   if (! $Config)  {
      print "Cannot read INI";
      return;
   #   
   #   foreach my $line (@Config::IniFiles::errors)
   #   {
   #      print $line;
   #   }
   }

   my $dbname = $Config->val('Data','RobustDataBaseName')|| 'robust';
   my $port = $Config->val('Data','MySqlRobustDBPort')   || 3306;
   my $host = $Config->val('Data','RobustServerIP')      || '127.0.0.1';
   my $user = $Config->val('Data','RobustUsername')      || 'robustuser';
   my $password = $Config->val('Data','password')         || 'robustpassword';   
         
   use Robust::Schema;
   if ($dbname) {
      Schema->connect("dbi:mysql:dbname=$dbname;host=$host;port=$port",$user,$password,{quote_names => 1,});
   }
   
}

1;

