use utf8;
package Robust::Schema::Result::Gloebitsubscription;

# Created by DBIx::Class::Schema::Loader
# DO NOT MODIFY THE FIRST PART OF THIS FILE

=head1 NAME

Robust::Schema::Result::Gloebitsubscription

=cut

use strict;
use warnings;

use base 'DBIx::Class::Core';

=head1 TABLE: C<gloebitsubscriptions>

=cut

__PACKAGE__->table("gloebitsubscriptions");

=head1 ACCESSORS

=head2 subscriptionid

  data_type: 'char'
  is_nullable: 1
  size: 36

=head2 objectid

  data_type: 'char'
  is_nullable: 0
  size: 36

=head2 appkey

  data_type: 'varchar'
  is_nullable: 0
  size: 64

=head2 glbapiurl

  data_type: 'varchar'
  is_nullable: 0
  size: 100

=head2 enabled

  data_type: 'tinyint'
  is_nullable: 0

=head2 objectname

  data_type: 'varchar'
  is_nullable: 1
  size: 64

=head2 description

  data_type: 'varchar'
  is_nullable: 0
  size: 255

=head2 ctime

  data_type: 'timestamp'
  datetime_undef_if_invalid: 1
  default_value: current_timestamp
  is_nullable: 0

=cut

__PACKAGE__->add_columns(
  "subscriptionid",
  { data_type => "char", is_nullable => 1, size => 36 },
  "objectid",
  { data_type => "char", is_nullable => 0, size => 36 },
  "appkey",
  { data_type => "varchar", is_nullable => 0, size => 64 },
  "glbapiurl",
  { data_type => "varchar", is_nullable => 0, size => 100 },
  "enabled",
  { data_type => "tinyint", is_nullable => 0 },
  "objectname",
  { data_type => "varchar", is_nullable => 1, size => 64 },
  "description",
  { data_type => "varchar", is_nullable => 0, size => 255 },
  "ctime",
  {
    data_type => "timestamp",
    datetime_undef_if_invalid => 1,
    default_value => \"current_timestamp",
    is_nullable => 0,
  },
);

=head1 PRIMARY KEY

=over 4

=item * L</objectid>

=item * L</appkey>

=item * L</glbapiurl>

=back

=cut

__PACKAGE__->set_primary_key("objectid", "appkey", "glbapiurl");

=head1 UNIQUE CONSTRAINTS

=head2 C<k_sub_api>

=over 4

=item * L</subscriptionid>

=item * L</glbapiurl>

=back

=cut

__PACKAGE__->add_unique_constraint("k_sub_api", ["subscriptionid", "glbapiurl"]);


# Created by DBIx::Class::Schema::Loader v0.07049 @ 2019-04-03 15:12:23
# DO NOT MODIFY THIS OR ANYTHING ABOVE! md5sum:W8nQPwemDkP17xEAKi3BHw


# You can replace this text with custom code or comments, and it will be preserved on regeneration
1;
