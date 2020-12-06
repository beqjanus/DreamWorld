use utf8;
package Robust::Schema::Result::Gloebituser;

# Created by DBIx::Class::Schema::Loader
# DO NOT MODIFY THE FIRST PART OF THIS FILE

=head1 NAME

Robust::Schema::Result::Gloebituser

=cut

use strict;
use warnings;

use base 'DBIx::Class::Core';

=head1 TABLE: C<gloebitusers>

=cut

__PACKAGE__->table("gloebitusers");

=head1 ACCESSORS

=head2 appkey

  data_type: 'char'
  is_nullable: 0
  size: 36

=head2 principalid

  data_type: 'char'
  is_nullable: 0
  size: 36

=head2 gloebitid

  data_type: 'char'
  is_nullable: 0
  size: 36

=head2 gloebittoken

  data_type: 'char'
  is_nullable: 0
  size: 36

=head2 lastsessionid

  data_type: 'char'
  is_nullable: 0
  size: 36

=cut

__PACKAGE__->add_columns(
  "appkey",
  { data_type => "char", is_nullable => 0, size => 36 },
  "principalid",
  { data_type => "char", is_nullable => 0, size => 36 },
  "gloebitid",
  { data_type => "char", is_nullable => 0, size => 36 },
  "gloebittoken",
  { data_type => "char", is_nullable => 0, size => 36 },
  "lastsessionid",
  { data_type => "char", is_nullable => 0, size => 36 },
);

=head1 PRIMARY KEY

=over 4

=item * L</appkey>

=item * L</principalid>

=back

=cut

__PACKAGE__->set_primary_key("appkey", "principalid");


# Created by DBIx::Class::Schema::Loader v0.07049 @ 2019-04-03 15:12:23
# DO NOT MODIFY THIS OR ANYTHING ABOVE! md5sum:ABPwd2+vpGBcIo83lSmuyA


# You can replace this text with custom code or comments, and it will be preserved on regeneration
1;
