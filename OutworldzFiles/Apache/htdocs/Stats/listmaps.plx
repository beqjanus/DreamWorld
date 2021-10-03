#!perl
# author - Fred Beckhusen
# Copyright Outworldz, LLC.
# AGPL3.0  https://opensource.org/licenses/AGPL

use strict;
use warnings;

#use CGI::Carp('fatalsToBrowser');
use CGI qw(:standard);
my $Input = CGI->new();
$|=1;

my  $debug ;  # program scope = set to non-zero values for debug info
$debug = 1      if (  ! $ENV{REMOTE_ADDR} );   # so we can single step in Komodo

#DBIx
use lib qw(lib .);
use Util;
my $schema = Util::mysql_connect;
$schema->storage->debug(0);

	
use Template;

my $tt = Template->new({
	ABSOLUTE => 1,
}) || die "$Template::ERROR\n";
print header;

###########################

my @sims;

my $rs = $schema->resultset('Sim')->search({},{order_by => { -asc => 'regionname' }});
my $width= '400';
my %unique;
foreach my $row ($rs->all) {
		
	my $X = $row->locationX;
	my $Y = $row->locationY;
	my $S = $row->regionsize/256;
	


	push @sims, {regionname => $row->regionname,				 
				 regionsize =>  $row->regionsize  . " X " . $row->regionsize  ,				 
				 map=> '/Stats/maps/' . $row->regionname . '.jpg',
				 width=>$width,
				 link=>'/Stats/map.htm?q=' . $row->regionname,				 
				 };

}

if (!@sims) {
	push @sims, {regionname => 'No Regions Found',				 
				 regionsize =>  0,
				 map=> '/Stats/images/blankbox.jpg',
				 width=>$width,
				 };

}
my $vars = {sims => \@sims};
my $out;
$tt->process('listmaps.tt', $vars, \$out)  || die $tt->error(), "\n";

print $out;
