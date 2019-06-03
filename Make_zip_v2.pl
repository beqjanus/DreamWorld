use strict;
use warnings;
use 5.010;

use File::Copy;
use File::Path;

my $type  = '-V2.94' ;  # '-Beta-V1.5';

use Cwd;
my $dir = getcwd;

say ('Server Publish? <enter for no>');
my $publish = <stdin>;
chomp $publish;


my $x = `net stop ApacheHTTPServer`;
$x =~ /was stopped|^$/  || die;

say("Clean up opensim");
my @deletions = (
	"$dir/OutworldzFiles/AutoBackup",
	
	"$dir/OutworldzFiles/Opensim/WifiPages-Custom",
	
	"$dir/OutworldzFiles/Opensim/bin/WifiPages-Custom",
	"$dir/OutworldzFiles/Opensim/bin/datasnapshot",
	"$dir/OutworldzFiles/Opensim/bin/assetcache",
	"$dir/OutworldzFiles/Opensim/bin/j2kDecodeCache",
	"$dir/OutworldzFiles/Opensim/bin/MeshCache",
	"$dir/OutworldzFiles/Opensim/bin/ScriptEngines",
	"$dir/OutworldzFiles/Opensim/bin/maptiles",
	"$dir/OutworldzFiles/Opensim/bin/Regions",
	"$dir/OutworldzFiles/Opensim/bin/bakes",
	"$dir/OutworldzFiles/Opensim/bin/addin-db-002",
	"$dir/OutworldzFiles/Opensim/bin/fsassets",
	
	"$dir/OutworldzFiles/mysql/data/opensim",
	"$dir/OutworldzFiles/mysql/data/robust",
	
	"$dir/OutworldzFiles/Apache/logs/",
);

foreach my $path ( @deletions) {
	say ($path);
	DeleteandKeep($path);
}

unlink ("$dir/BareTail.udm");
unlink "$dir/OutworldzFiles/Apache/htdocs/Search/flog.log" ;
unlink "$dir/OutworldzFiles/Opensim/bin/Error.log" ;
unlink "$dir/OutworldzFiles/Opensim/bin/Opensim.log" ;
unlink "$dir/OutworldzFiles/Opensim/bin/Opensimstats.log" ;
unlink "$dir/OutworldzFiles/PHPLog.log" ;
unlink "$dir/SET_externalIP-log.txt";
unlink "$dir/OutworldzFiles/Photo.png";
unlink "$dir/OutworldzFiles/XYSettings.ini";
unlink "$dir/Outworldzfiles/Icecast/log/error.log" ;
unlink "$dir/Outworldzfiles/Icecast/log/access.log" ;

unlink "$dir/OutworldzFiles/Opensim/bin/OpensimConsoleHistory.txt" ;
unlink "$dir/OutworldzFiles/Opensim/bin/LocalUserStatistics.db" ;

#Setting
unlink "$dir/Outworldzfiles/Settings.ini" ;


#logs
unlink "$dir/OutworldzFiles/Diagnostics.log" ;
unlink "$dir/OutworldzFiles/Outworldz.log" ;
unlink "$dir/OutworldzFiles/Init.txt" ;
unlink "$dir/OutworldzFiles/upnp.log" ;
unlink "$dir/OutworldzFiles/http.log" ;

unlink "../Zips/DreamGrid$type.zip" ;
unlink "../Zips/Outworldz-Update$type.zip" ;

say "DLL List Build";
use File::Find;

open (OUT, ">", 'dlls.txt');

find({ wanted => \&process_file, no_chdir => 1 }, $dir . '/Outworldzfiles/opensim/bin/');

sub process_file {
   # if (-f $_) {
       # print "This is a file: $_\n";
    #} else {
       # print "This is not file: $_\n";
   # }
	if ($_ !~ /dll$/) {
		return;
	}
	my $fullpath = $_;
	$fullpath =~ s/$dir//g;
	$fullpath =~ s/\//\\/g;
	
	print OUT $fullpath . "\n";	
}

close OUT;

if (!copy ("$dir/Installer_Src/Setup DreamWorld/bin/Release/Start.exe", "$dir"))  {die $!;}


say("Signing");
use IO::All;

my @files = io->dir($dir)->all(0);  


foreach my $file (@files) {
    my $name = $file->name;
    next if $name =~ /Installer_Src|\.git|baretail/;
    if ($name =~ /dll$|exe$/ ) {
        
        my $r = qq!../Certs/sigcheck64.exe "$name"!;
        print $r. "\n";
        my $result1 = `$r`;
        if ($result1 =~ /Publisher:.*Outworldz, LLC/) {
            next;
        }
        
        my $f = qq!../Certs/DigiCertUtil.exe sign /noInput /sha1 "52CADF8EA98C9382D0350815A68B2C79340E141F" "$name"!;
        print $f;
        my $result = `$f`;
        print $result. "\n";
        if ($result !~ /success/) {
            say ("***** Failed to sign!");
			die;
        }
    }
}

say("Mysql");
chdir(qq!$dir/OutworldzFiles/mysql/bin/!);
print `mysqladmin.exe --port 3309 -u root shutdown`;
print `mysqladmin.exe --port 3306 -u root shutdown`;

sleep(5);
chdir ($dir);
DeleteandKeep("$dir/OutworldzFiles/mysql/data");

use IO::Uncompress::Unzip qw(unzip $UnzipError  );
use IO::File ;

Perlunzip( "mysql/Blank-Mysql-Data-folder.zip", 'mysql');

print "Processing Main Zip\n";


@files =   `cmd /c dir /b `;

foreach my $file (@files) {
	chomp $file;
	next if -d $file;
	#next if $file eq 'Make_zip_v2.pl';
	next if $file =~ /^\./;
	print  "$file ";
	Process ("../7z.exe -tzip a ..\\Zips\\DreamGrid$type.zip \"$dir\\$file\" ");
}

say("Adding folders");

Process ("../7z.exe -tzip a ..\\Zips\\DreamGrid$type.zip Licenses_to_Content");
Process ("../7z.exe -tzip a ..\\Zips\\DreamGrid$type.zip OutworldzFiles");

		
say("Updater Build");
if (!copy ("../Zips/DreamGrid$type.zip", "../Zips/DreamGrid-Update$type.zip"))  {die $!;}

say("Drop mysql files from update");
# now delete the mysql from the UPDATE

Process ("../7z.exe -tzip d ..\\Zips\\DreamGrid-Update$type.zip Outworldzfiles\\mysql\\data\\ -r ");

my @filestodrop = qw (OpenSim Prebuild share Thirdparty doc addon-modules);
foreach my $file (@filestodrop)
{
	Process ("../7z.exe -tzip d ..\\Zips\\DreamGrid-Update$type.zip Outworldzfiles\\Opensim\\$file ");
}
# del Dot net because we cannot overwrite an open file
Process ("../7z.exe -tzip d ..\\Zips\\DreamGrid-Update$type.zip DotNetZip.dll ");

#####################
print "Server Copy\n";

# Ready to move it all
unlink "y:/Inetpub/Secondlife/Outworldz_Installer/Grid/DreamGrid$type.zip";
if (!copy ("../Zips/DreamGrid$type.zip", "y:/Inetpub/Secondlife/Outworldz_Installer/Grid/DreamGrid$type.zip"))  {die $!;}

#web server
print "Copy Update\n";
unlink "y:/Inetpub/Secondlife/Outworldz_Installer/Grid/DreamGrid-Update$type.zip";
if (!copy ("../Zips/DreamGrid-Update$type.zip", "y:/Inetpub/Secondlife/Outworldz_Installer/Grid/DreamGrid-Update$type.zip"))  {die $!;}


if ($publish)
{
	say ("Publishing now");
	unlink "y:/Inetpub/Secondlife/Outworldz_Installer/Grid/DreamGrid.zip";
	if (!copy ("../Zips/DreamGrid$type.zip", "y:/Inetpub/Secondlife/Outworldz_Installer/Grid/DreamGrid.zip"))  {die $!;}
	unlink "y:/Inetpub/Secondlife/Outworldz_Installer/Grid/DreamGrid-Update.zip";
	if (!copy ("../Zips/DreamGrid-Update$type.zip", "y:/Inetpub/Secondlife/Outworldz_Installer/Grid/DreamGrid-Update.zip"))  {die $!;}

}


print "Revisions\n";
if (!copy ('Revisions.txt', 'y:/Inetpub/Secondlife/Outworldz_Installer/Revisions.txt'))  {die $!;}
if (!copy ('Revisions.txt', 'y:/Inetpub/Secondlife/Outworldz_Installer/Grid/Revisions.txt'))  {die $!;}
if (!copy ('Revisions.txt', 			'y:/Inetpub/Secondlife/Outworldz_Installer/Revisions.txt'))  {die $!;}


say "Done!";

sub Write
{
	my $dest = shift;
	my $content = shift;
	open OUT, ">$dest" || die $!;
	print OUT $content;
	close OUT;
}

sub Process
{
	my $file = shift;
	
	my $x = `$file`;
	if ($x =~ /Everything is Ok/) {
		print "\n";
	} else {
		print "Fail: $x\n";
		exit;
	}
}

sub rm {
	
my $path = shift;
	
	my $errors;
	while ($_ = glob("'$path/*'")) {
		rmtree($_)
		  or ++$errors, warn("Can't remove $_: $!");
	}
	
	#exit(1) if $errors;
}

sub DeleteandKeep {

	my $path = shift;
	
	rm $path;
	mkdir $path ;
	open (FILE, '>', $path . '/.keep') or die;
	print FILE "git will not save empty folders unless there is a file in it. This is a placeholder only\n";
	close FILE;
	
}


# for importers
sub Perlunzip {
	
	
	use Archive::Zip qw(:ERROR_CODES :CONSTANTS);
	use Exporter 'import';
	
	my ($zip_file , $out_file, $filter) = @_;
	$zip_file = $dir . '/Outworldzfiles/'. $zip_file;
	$out_file = $dir . '/Outworldzfiles/'. $out_file;
	
	my $zip = Archive::Zip->new($zip_file);
	unless ($zip->extractTree($filter || '', $out_file) == AZ_OK) {
		warn "unzip not successful: $!\n";
	}
}