use utf8;
package Opensim::Schema::Result::Inventoryfolder;

# Created by DBIx::Class::Schema::Loader
# DO NOT MODIFY THE FIRST PART OF THIS FILE

=head1 NAME

Opensim::Schema::Result::Inventoryfolder

=cut

use strict;
use warnings;

use base 'DBIx::Class::Core';

=head1 TABLE: C<inventoryfolders>

=cut

__PACKAGE__->table("inventoryfolders");

=head1 ACCESSORS

=head2 foldername

  data_type: 'varchar'
  is_nullable: 1
  size: 64

=head2 type

  data_type: 'smallint'
  default_value: 0
  is_nullable: 0

=head2 version

  data_type: 'integer'
  default_value: 0
  is_nullable: 0

=head2 folderid

  data_type: 'char'
  default_value: '00000000-0000-0000-0000-000000000000'
  is_nullable: 0
  size: 36

=head2 agentid

  data_type: 'char'
  is_nullable: 1
  size: 36

=head2 parentfolderid

  data_type: 'char'
  is_nullable: 1
  size: 36

=cut

__PACKAGE__->add_columns(
  "foldername",
  { data_type => "varchar", is_nullable => 1, size => 64 },
  "type",
  { data_type => "smallint", default_value => 0, is_nullable => 0 },
  "version",
  { data_type => "integer", default_value => 0, is_nullable => 0 },
  "folderid",
  {
    data_type => "char",
    default_value => "00000000-0000-0000-0000-000000000000",
    is_nullable => 0,
    size => 36,
  },
  "agentid",
  { data_type => "char", is_nullable => 1, size => 36 },
  "parentfolderid",
  { data_type => "char", is_nullable => 1, size => 36 },
);

=head1 PRIMARY KEY

=over 4

=item * L</folderid>

=back

=cut

__PACKAGE__->set_primary_key("folderid");


# Created by DBIx::Class::Schema::Loader v0.07042 @ 2014-10-14 12:09:34
# DO NOT MODIFY THIS OR ANYTHING ABOVE! md5sum:gEZ/OHUDkW0sKTA9ZexDNA


# You can replace this text with custom code or comments, and it will be preserved on regeneration
1;
