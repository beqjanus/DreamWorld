use utf8;
package Opensim::Schema::Result::Regionextra;

# Created by DBIx::Class::Schema::Loader
# DO NOT MODIFY THE FIRST PART OF THIS FILE

=head1 NAME

Opensim::Schema::Result::Regionextra

=cut

use strict;
use warnings;

use base 'DBIx::Class::Core';

=head1 TABLE: C<regionextra>

=cut

__PACKAGE__->table("regionextra");

=head1 ACCESSORS

=head2 regionid

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
  "regionid",
  { data_type => "char", is_nullable => 0, size => 36 },
  "name",
  { data_type => "varchar", is_nullable => 0, size => 32 },
  "value",
  { data_type => "text", is_nullable => 1 },
);

=head1 PRIMARY KEY

=over 4

=item * L</regionid>

=item * L</name>

=back

=cut

__PACKAGE__->set_primary_key("regionid", "name");


# Created by DBIx::Class::Schema::Loader v0.07042 @ 2014-10-14 12:09:34
# DO NOT MODIFY THIS OR ANYTHING ABOVE! md5sum:EvgfPDVWmTUKGKQvcU3GnA


# You can replace this text with custom code or comments, and it will be preserved on regeneration
1;
