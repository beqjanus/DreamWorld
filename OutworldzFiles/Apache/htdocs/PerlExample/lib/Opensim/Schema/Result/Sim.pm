package Schema::Result::Sim;

use strict;
use warnings;

use parent 'DBIx::Class::Core';

__PACKAGE__->table('Sims');

__PACKAGE__->add_columns(
  
  "regionname",
  { data_type => "nvarchar", is_nullable => 0, size => 255 },
  "regionsize",
  { data_type => "nvarchar", is_nullable => 0 , size => 255 },
   "XYZ",
  { data_type => "nvarchar", is_nullable => 0, size => 128 },
);

__PACKAGE__->set_primary_key('regionname');
__PACKAGE__->has_many('visitors' => ':Schema::Result::SimVisitor',{'foreign.gridname' => 'self.gridname'});

sub TO_JSON {
	my $self = shift;
	{
		id				=> $self->id,
		regionname		=> $self->regionname,
		regionsize		=> $self->regionsize,
		XYZ				=> $self->XYZ,		
	}
}

1;



