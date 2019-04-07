#!perl.exe

#This line causes an error. It is to secure this example, which shows your users.

# if you comment out the above line this "list users" example should run.
# see README  for how to install Strawberry Perl and the necessary Perl modules

use strict;
use warnings;
use lib qw(lib); # look in lib for modules and databases
binmode (STDOUT,':utf8'); # we can use unicode
use CGI qw(:standard); # so we can read and write to the web
my $Input = CGI->new();

use Robust::Util; # Dbix::Class stuff - set up the DSN in your ODBC driver, and put the details of the DSN in DSN.txt
my $schema = Robust::Util::mysql_connect;
$schema->storage->debug(0);   # set to 1 to see detailed database SQL as it is generated

# See the fabulous Template::Toolkit at http://template-toolkit.org/
use Template;
my $tt = Template->new({
       INTERPOLATE  => 0,
   }) || die "$Template::ERROR\n";

# Select all records from the UserAccounts table into a recordset
my $userRS =  $schema->resultset('Useraccount');
my $count = $userRS->count;   # OO method for how many users on my grid

my $welcomenote =  $schema->resultset('Asset')->search({name=>'WelcomeNote'})->first->data;


my @data ;
foreach my $contact ($userRS->all) #  read each row object into $contact
{
   push @data, {  email => $contact->email,     # read each column and store it in a hash by ID
                  first => $contact->firstname,
                  last  => $contact->lastname,

               };
}
print $Input->header(   # and print it as UTF-8
      -type    => 'text/html',
      -charset =>  'utf-8',
   );
$tt->process('template/users.tt',  {
      users=>\@data,
      usercount=>$count,
      welcome => $welcomenote,
   } ) || die $tt->error(), "\n"; # convert to a web page

