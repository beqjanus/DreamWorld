use utf8;
package Opensim::Schema::Result::EstateUser;

# Created by DBIx::Class::Schema::Loader
# DO NOT MODIFY THE FIRST PART OF THIS FILE

=head1 NAME

Opensim::Schema::Result::EstateUser

=cut

use strict;
use warnings;

use base 'DBIx::Class::Core';

=head1 TABLE: C<estate_users>

=cut

__PACKAGE__->table("estate_users");

=head1 ACCESSORS

=head2 estateid

  data_type: 'integer'
  extra: {unsigned => 1}
  is_nullable: 0

=head2 uuid

  data_type: 'char'
  is_nullable: 0
  size: 36

=cut

__PACKAGE__->add_columns(
  "estateid",
  { data_type => "integer", extra => { unsigned => 1 }, is_nullable => 0 },
  "uuid",
  { data_type => "char", is_nullable => 0, size => 36 },
);


# Created by DBIx::Class::Schema::Loader v0.07042 @ 2014-10-14 12:09:34
# DO NOT MODIFY THIS OR ANYTHING ABOVE! md5sum:N4xifA6jYP4oLwRC8neumg


# You can replace this text with custom code or comments, and it will be preserved on regeneration
1;
