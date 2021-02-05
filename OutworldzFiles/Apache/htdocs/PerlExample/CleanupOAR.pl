#  Strawberry Perl program to keep a monthly backup  in $src and move all others to $newpath

use 5.010;
use strict;
use warnings;
use IO::All;
use File::Copy qw(move);
use File::Basename;
use CGI;
$| = 1;  #non buffered out

print header;

# fill these in with $src and $Destination paths.
my $src = 'B:/OAR';
my $dest = 'Z:/Backups/OAR';

my @f    = io->dir($src)->all;

my %result;

foreach my $folder (sort @f) {
    
    my @files    = io->dir($folder)->all;
    
      
    foreach my $file (sort @files) {            
        
        $file->name =~ /(.*?)_(\d{4})y_(\d{2})M_(\d{2})d_|(.*?)_(\d{4})-(\d{2})-(\d{2})_/;
        my $name = $1 || $5;
        if ($name) {
            $name = basename($name);
            next unless $name;
        }
        my $yyyy = $2 || $6;
        my $mm = $3 || $7;
        my $dd = $4 || $8;
        
        if ($name && $yyyy && $mm && $dd ) {        
            if ($result{"$name $yyyy-$mm"})
            { 
                say("Move $name $yyyy-$mm              " . $file->name);
                my $newname = $file->name;
                $newname =~ s/\\/\//g;
                $newname =~ s/$src/$dest/;
                my $srcname = dirname($newname);
                mkdir $srcname;
                move $file->name, $newname || die $!;
            } else {
                say("Keep $name $yyyy-$mm " . $file->name);
                
                $result{"$name $yyyy-$mm"} = $file->name;    
            }                
        }
    }

}