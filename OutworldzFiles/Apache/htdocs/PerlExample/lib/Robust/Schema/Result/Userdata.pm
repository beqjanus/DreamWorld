use utf8;
package Robust::Schema::Result::Userdata;

# Created by DBIx::Class::Schema::Loader
# DO NOT MODIFY THE FIRST PART OF THIS FILE

=head1 NAME

Robust::Schema::Result::Userdata

=cut

use strict;
use warnings;

use base 'DBIx::Class::Core';

=head1 TABLE: C<userdata>

=cut

__PACKAGE__->table("userdata");

=head1 ACCESSORS

=head2 userid

  data_type: 'char'
  is_nullable: 0
  size: 36

=head2 tagid

  data_type: 'varchar'
  is_nullable: 0
  size: 64

=head2 datakey

  data_type: 'varchar'
  is_nullable: 1
  size: 255

=head2 dataval

  data_type: 'varchar'
  is_nullable: 1
  size: 255

=cut

__PACKAGE__->add_columns(
  "userid",
  { data_type => "char", is_nullable => 0, size => 36 },
  "tagid",
  { data_type => "varchar", is_nullable => 0, size => 64 },
  "datakey",
  { data_type => "varchar", is_nullable => 1, size => 255 },
  "dataval",
  { data_type => "varchar", is_nullable => 1, size => 255 },
);

=head1 PRIMARY KEY

=over 4

=item * L</userid>

=item * L</tagid>

=back

=cut

__PACKAGE__->set_primary_key("userid", "tagid");


# Created by DBIx::Class::Schema::Loader v0.07049 @ 2022-01-14 22:23:40
# DO NOT MODIFY THIS OR ANYTHING ABOVE! md5sum:hTYESfDzgDk5pQS5mmmgSQ


# You can replace this text with custom code or comments, and it will be preserved on regeneration
1;
