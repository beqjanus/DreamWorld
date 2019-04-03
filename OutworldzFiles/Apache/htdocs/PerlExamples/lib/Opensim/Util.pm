package Opensim::Util;
use strict;use warnings;

# Edit this to set your Opensim database name, username, and password.
# These can be found in your GridCommon.ini or StandaloneCommon.ini files in the "ConnectionString" area
# ConnectionString = "Data Source=localhost;Database=opensim;User ID=opensimuser;Password=opensimpassword;"

sub mysql_connect {
   my $dbname     = 'opensim';
   my $user       = 'opensimuser';
   my $password   = 'opensimpassword';

   require Opensim::Schema;
   Opensim::Schema->connect("dbi:mysql:dbname=$dbname",$user,$password,{quote_names => 1,});
}
1;
