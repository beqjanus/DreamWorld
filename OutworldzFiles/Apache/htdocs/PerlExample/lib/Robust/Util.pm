package Robust::Util;
use strict;use warnings;

# Edit this to set your Robust database name, username, and password.
# These can be found in your GridCommon.ini or StandaloneCommon.ini files in the "ConnectionString" area
# ConnectionString = "Data Source=localhost;Database=robust;User ID=robustuser;Password=robustpassword;"

sub mysql_connect {
   my $dbname     = 'robust';
   my $user       = 'robustuser';
   my $password   = 'robustpassword';

   require Robust::Schema;
   Robust::Schema->connect("dbi:mysql:dbname=$dbname",$user,$password,{quote_names => 1,});
}
1;
