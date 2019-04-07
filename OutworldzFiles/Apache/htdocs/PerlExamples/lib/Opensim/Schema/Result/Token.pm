use utf8;
package Opensim::Schema::Result::Token;

# Created by DBIx::Class::Schema::Loader
# DO NOT MODIFY THE FIRST PART OF THIS FILE

=head1 NAME

Opensim::Schema::Result::Token

=cut

use strict;
use warnings;

use base 'DBIx::Class::Core';

=head1 TABLE: C<tokens>

=cut

__PACKAGE__->table("tokens");

=head1 ACCESSORS

=head2 uuid

  data_type: 'char'
  is_nullable: 0
  size: 36

=head2 token

  data_type: 'varchar'
  is_nullable: 0
  size: 255

=head2 validity

  data_type: 'datetime'
  datetime_undef_if_invalid: 1
  is_nullable: 0

=cut

__PACKAGE__->add_columns(
  "uuid",
  { data_type => "char", is_nullable => 0, size => 36 },
  "token",
  { data_type => "varchar", is_nullable => 0, size => 255 },
  "validity",
  {
    data_type => "datetime",
    datetime_undef_if_invalid => 1,
    is_nullable => 0,
  },
);

=head1 UNIQUE CONSTRAINTS

=head2 C<uuid_token>

=over 4

=item * L</uuid>

=item * L</token>

=back

=cut

__PACKAGE__->add_unique_constraint("uuid_token", ["uuid", "token"]);


# Created by DBIx::Class::Schema::Loader v0.07042 @ 2014-10-14 12:09:34
# DO NOT MODIFY THIS OR ANYTHING ABOVE! md5sum:i6ogwqa89oT/l5CgRhWEgQ


# You can replace this text with custom code or comments, and it will be preserved on regeneration
1;
