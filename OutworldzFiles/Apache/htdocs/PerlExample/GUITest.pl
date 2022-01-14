
# Print 'show stats via perl on each region
# edit this to point to Opensim folder,. slashed to the right
my $Opensim = "E:/Outworldz Dreamgrid/Outworldzfiles/Opensim";


# uncomment these next lines if you wish to run this via Apache. COmmented out, it secure this example, which shows your users.
# if you uncomment out line example should run on Apache over the web.
# see README  for how to install Strawberry Perl and the necessary Perl modules

#print $Input->header(   # and print it as UTF-8
#      -type    => 'text/html',
#      -charset =>  'utf-8',
#   );




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
