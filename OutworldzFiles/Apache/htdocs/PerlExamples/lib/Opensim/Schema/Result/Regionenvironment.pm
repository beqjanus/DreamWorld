use utf8;
package Opensim::Schema::Result::Regionenvironment;

# Created by DBIx::Class::Schema::Loader
# DO NOT MODIFY THE FIRST PART OF THIS FILE

=head1 NAME

Opensim::Schema::Result::Regionenvironment

=cut

use strict;
use warnings;

use base 'DBIx::Class::Core';

=head1 TABLE: C<regionenvironment>

=cut

__PACKAGE__->table("regionenvironment");

=head1 ACCESSORS

=head2 region_id

  data_type: 'varchar'
  is_nullable: 0
  size: 36

=head2 llsd_settings

  data_type: 'text'
  is_nullable: 0

=cut

__PACKAGE__->add_columns(
  "region_id",
  { data_type => "varchar", is_nullable => 0, size => 36 },
  "llsd_settings",
  { data_type => "text", is_nullable => 0 },
);

=head1 PRIMARY KEY

=over 4

=item * L</region_id>

=back

=cut

__PACKAGE__->set_primary_key("region_id");


# Created by DBIx::Class::Schema::Loader v0.07042 @ 2014-10-14 12:09:34
# DO NOT MODIFY THIS OR ANYTHING ABOVE! md5sum:XnVqzitSgSC6H2y0k5J8ng


# You can replace this text with custom code or comments, and it will be preserved on regeneration
1;
