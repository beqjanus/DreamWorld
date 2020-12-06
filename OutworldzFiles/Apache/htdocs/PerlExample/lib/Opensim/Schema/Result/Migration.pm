use utf8;
package Opensim::Schema::Result::Migration;

# Created by DBIx::Class::Schema::Loader
# DO NOT MODIFY THE FIRST PART OF THIS FILE

=head1 NAME

Opensim::Schema::Result::Migration

=cut

use strict;
use warnings;

use base 'DBIx::Class::Core';

=head1 TABLE: C<migrations>

=cut

__PACKAGE__->table("migrations");

=head1 ACCESSORS

=head2 name

  data_type: 'varchar'
  is_nullable: 1
  size: 100

=head2 version

  data_type: 'integer'
  is_nullable: 1

=cut

__PACKAGE__->add_columns(
  "name",
  { data_type => "varchar", is_nullable => 1, size => 100 },
  "version",
  { data_type => "integer", is_nullable => 1 },
);


# Created by DBIx::Class::Schema::Loader v0.07042 @ 2014-10-14 12:09:34
# DO NOT MODIFY THIS OR ANYTHING ABOVE! md5sum:Vh4iNYrRfp/dSnOEeDR17A


# You can replace this text with custom code or comments, and it will be preserved on regeneration
1;
