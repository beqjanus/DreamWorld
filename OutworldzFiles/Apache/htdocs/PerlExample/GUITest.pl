
# Print 'show stats via perl on each region

use CGI;
# uncomment these next lines if you wish to run this via Apache. Commented out, it secure this example.
# if you uncomment out line example should run on Apache over the web.

#print $Input->header(   # and print it as UTF-8
#      -type    => 'text/html',
#      -charset =>  'utf-8',
#   );


use Config::IniFiles;
use File::BOM;    # fixes a bug in Perl with UTF-8

# get the path to the Settings.ini
use Cwd;
my $path = getcwd();

$path =~ /(.*?\/Outworldzfiles)/i;
my $file = $1 . '/Settings.ini';

# Read the Right Thing from a unicode file with BOM:
open( CONFIG, '<:via(File::BOM)', $file );
my $Config = Config::IniFiles->new( -file => *CONFIG );

my $Path =$Config->val( 'Data', 'Myfolder' );
# edit this to point to Opensim folder,. slashed to the right
my $Opensim = "$Path/Outworldzfiles/Opensim";

use Win32::GuiTest qw(FindWindowLike GetWindowText SetForegroundWindow SendKeys);
$Win32::GuiTest::debug = 0; # Set to "1" to enable verbose mode

my @sims =glob ("'$Opensim/bin/regions/*'");

foreach my $sim (sort @sims) {
    $sim =~ s/$Opensim\/bin\/Regions\///i;

    my @windows = FindWindowLike(0, $sim, "");
    for (@windows) {
        print "$_>\t'", GetWindowText($_), "'\n";
        SetForegroundWindow($_);

        SendKeys("\n{ENTER}show stats{ENTER}\n");
        <STDIN>;
    }
}
