use utf8;
package Opensim::Schema::Result::Avatar;

# Created by DBIx::Class::Schema::Loader
# DO NOT MODIFY THE FIRST PART OF THIS FILE

=head1 NAME

Opensim::Schema::Result::Avatar

=cut

use strict;
use warnings;

use base 'DBIx::Class::Core';

=head1 TABLE: C<avatars>

=cut

__PACKAGE__->table("avatars");

=head1 ACCESSORS

=head2 principalid

  data_type: 'char'
  is_nullable: 0
  size: 36

=head2 name

  data_type: 'varchar'
  is_nullable: 0
  size: 32

=head2 value

  data_type: 'text'
  is_nullable: 1

=cut

__PACKAGE__->add_columns(
  "principalid",
  { data_type => "char", is_nullable => 0, size => 36 },
  "name",
  { data_type => "varchar", is_nullable => 0, size => 32 },
  "value",
  { data_type => "text", is_nullable => 1 },
);

=head1 PRIMARY KEY

=over 4

=item * L</principalid>

=item * L</name>

=back

=cut

__PACKAGE__->set_primary_key("principalid", "name");


# Created by DBIx::Class::Schema::Loader v0.07042 @ 2014-10-14 12:09:34
# DO NOT MODIFY THIS OR ANYTHING ABOVE! md5sum:iToqsb2n9aH8bEgSme2Bnw


# You can replace this text with custom code or comments, and it will be preserved on regeneration
1;
