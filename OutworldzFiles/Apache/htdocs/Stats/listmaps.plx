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

my $rs = $schema->resultset('Sim')->search({dateupdated => {'>' => \["dateadd(dd,-15,getdate())"]}},{order_by => { -asc => 'regionname' }});

my %unique;
foreach my $row ($rs->all) {
	
	my $width = '32';
	$width= '400';
	my $X = $row->X;
	my $Y = $row->Y;
	my $map = "map-1-$X-$Y-objects.jpg";
	my $size = $Input->param('size');
	$size =~ /.*?(\d+).*?, (\d+)/;		# <768.000000, 768.000000, 0.000000>
	$size = $1 . 'X' . $2;
		
	push @sims, {name => $row->regionname,				 
				 regionsize => $size,
				 dateupdated => $row->dateupdated,
				 map=> $map,
				 width=>$width,
				 link => "/stats/?q=" . $row->regionname,
				 location => $row->location,
				 };

}

my $vars = {sims => \@sims};
my $out;
$tt->process('listmaps.tt', $vars, \$out)  || die $tt->error(), "\n";

print $out;
