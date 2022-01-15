#!perl.exe
use strict;
use warnings;
use lib qw(lib); # look in lib for modules and databases
binmode (STDOUT,':utf8'); # we can use unicode
use CGI qw(:standard); # so we can read and write to the web
my $Input = CGI->new(); # read data from the web

# uncomment these next lines if you wish to run this via Apache. COmmented out, it secure this example, which shows your users.
# if you uncomment out line example should run on Apache over the web.
# see README  for how to install Strawberry Perl and the necessary Perl modules

#print $Input->header(   # and print it as UTF-8
#      -type    => 'text/html',
#      -charset =>  'utf-8',
#   );

# Dbix::Class stuff - set up the DSN in your ODBC driver, and put the details of the DSN in DSN.txt
use Robust::Util; 
my $schema = Robust::Util::mysql_connect;
$schema->storage->debug(0);   # set to 1 to see detailed database SQL as it is generated

# See the fabulous Template::Toolkit at http://template-toolkit.org/
use Template ;
my $tt = Template->new({
       INTERPOLATE  => 0,
   }) || die "$Template::ERROR\n";

# Select all records from the UserAccounts table into a recordset
my $RS =  $schema->resultset('Region');


my @data ;
foreach my $r ($RS->all) #  read each row object into $contact
{
   push @data, {  regionName => $r->regionname,     # read each column and store it in a hash by ID
                  serverPort => $r->serverhttpport,
               };
}


$tt->process('template/regions.tt',  {
      regions=>\@data,
      regioncount=>$RS->count,
      welcome => 'This is a list of registered Regions from Robust',
   } ) || die $tt->error(), "\n"; # convert to a web page

