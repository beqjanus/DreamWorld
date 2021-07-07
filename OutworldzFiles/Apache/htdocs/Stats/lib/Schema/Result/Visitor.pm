package Schema::Result::Visitor;

use strict;
use warnings;

use parent 'DBIx::Class::Core';

__PACKAGE__->table("Visitor");

__PACKAGE__->add_columns(
  "location",
  { data_type => "bigint", is_nullable => 0 },
  "dateandtime",
  { data_type => "datetime", is_nullable => 1 },
);


sub TO_JSON {
	my $self = shift;
	{
		location 		=> $self->location,
		dateandtime 	=> $self->dateandtime,
	}
}


1;
