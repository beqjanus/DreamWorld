
This will run any anything that Opensim and Perl runs on.

1) You need Perl.  Install Perl from http://strawberryperl.com/

2) Install DBIX::Class:
    From the command prompt type in 'cpan DBIx::Class<ret>'.

3) Install Template::Toolkit
    From the command prompt type in 'cpan Toolkit<ret>'.

4) Setup your Database Connection:

    Edit the file lib/Util.pm and set your Opensim database name, username, and password.

    These can be found in your GridCommon.ini or StandaloneCommon.ini files in the "ConnectionString" area as shown below.
    ConnectionString = "Data Source=localhost;Database=opensim;User ID=opensimuser;Password=opensimpassword;"
