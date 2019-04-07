use utf8;
package Robust::Schema::Result::Gloebittransaction;

# Created by DBIx::Class::Schema::Loader
# DO NOT MODIFY THE FIRST PART OF THIS FILE

=head1 NAME

Robust::Schema::Result::Gloebittransaction

=cut

use strict;
use warnings;

use base 'DBIx::Class::Core';

=head1 TABLE: C<gloebittransactions>

=cut

__PACKAGE__->table("gloebittransactions");

=head1 ACCESSORS

=head2 transactionid

  data_type: 'char'
  is_nullable: 0
  size: 36

=head2 payerid

  data_type: 'char'
  is_nullable: 0
  size: 36

=head2 payername

  data_type: 'varchar'
  is_nullable: 0
  size: 255

=head2 payeeid

  data_type: 'char'
  is_nullable: 0
  size: 36

=head2 payeename

  data_type: 'varchar'
  is_nullable: 0
  size: 255

=head2 amount

  data_type: 'integer'
  is_nullable: 0

=head2 transactiontype

  data_type: 'integer'
  is_nullable: 0

=head2 transactiontypestring

  data_type: 'varchar'
  is_nullable: 0
  size: 64

=head2 issubscriptiondebit

  data_type: 'tinyint'
  is_nullable: 0

=head2 subscriptionid

  data_type: 'char'
  is_nullable: 0
  size: 36

=head2 partid

  data_type: 'char'
  is_nullable: 1
  size: 36

=head2 partname

  data_type: 'varchar'
  is_nullable: 1
  size: 64

=head2 partdescription

  data_type: 'varchar'
  is_nullable: 1
  size: 128

=head2 categoryid

  data_type: 'char'
  is_nullable: 1
  size: 36

=head2 saletype

  data_type: 'integer'
  is_nullable: 1

=head2 submitted

  data_type: 'tinyint'
  is_nullable: 0

=head2 responsereceived

  data_type: 'tinyint'
  is_nullable: 0

=head2 responsesuccess

  data_type: 'tinyint'
  is_nullable: 0

=head2 responsestatus

  data_type: 'varchar'
  is_nullable: 0
  size: 64

=head2 responsereason

  data_type: 'varchar'
  is_nullable: 0
  size: 255

=head2 payerendingbalance

  data_type: 'integer'
  is_nullable: 0

=head2 enacted

  data_type: 'tinyint'
  is_nullable: 0

=head2 consumed

  data_type: 'tinyint'
  is_nullable: 0

=head2 canceled

  data_type: 'tinyint'
  is_nullable: 0

=head2 ctime

  data_type: 'timestamp'
  datetime_undef_if_invalid: 1
  default_value: current_timestamp
  is_nullable: 0

=head2 enactedtime

  data_type: 'timestamp'
  datetime_undef_if_invalid: 1
  is_nullable: 1

=head2 finishedtime

  data_type: 'timestamp'
  datetime_undef_if_invalid: 1
  is_nullable: 1

=cut

__PACKAGE__->add_columns(
  "transactionid",
  { data_type => "char", is_nullable => 0, size => 36 },
  "payerid",
  { data_type => "char", is_nullable => 0, size => 36 },
  "payername",
  { data_type => "varchar", is_nullable => 0, size => 255 },
  "payeeid",
  { data_type => "char", is_nullable => 0, size => 36 },
  "payeename",
  { data_type => "varchar", is_nullable => 0, size => 255 },
  "amount",
  { data_type => "integer", is_nullable => 0 },
  "transactiontype",
  { data_type => "integer", is_nullable => 0 },
  "transactiontypestring",
  { data_type => "varchar", is_nullable => 0, size => 64 },
  "issubscriptiondebit",
  { data_type => "tinyint", is_nullable => 0 },
  "subscriptionid",
  { data_type => "char", is_nullable => 0, size => 36 },
  "partid",
  { data_type => "char", is_nullable => 1, size => 36 },
  "partname",
  { data_type => "varchar", is_nullable => 1, size => 64 },
  "partdescription",
  { data_type => "varchar", is_nullable => 1, size => 128 },
  "categoryid",
  { data_type => "char", is_nullable => 1, size => 36 },
  "saletype",
  { data_type => "integer", is_nullable => 1 },
  "submitted",
  { data_type => "tinyint", is_nullable => 0 },
  "responsereceived",
  { data_type => "tinyint", is_nullable => 0 },
  "responsesuccess",
  { data_type => "tinyint", is_nullable => 0 },
  "responsestatus",
  { data_type => "varchar", is_nullable => 0, size => 64 },
  "responsereason",
  { data_type => "varchar", is_nullable => 0, size => 255 },
  "payerendingbalance",
  { data_type => "integer", is_nullable => 0 },
  "enacted",
  { data_type => "tinyint", is_nullable => 0 },
  "consumed",
  { data_type => "tinyint", is_nullable => 0 },
  "canceled",
  { data_type => "tinyint", is_nullable => 0 },
  "ctime",
  {
    data_type => "timestamp",
    datetime_undef_if_invalid => 1,
    default_value => \"current_timestamp",
    is_nullable => 0,
  },
  "enactedtime",
  {
    data_type => "timestamp",
    datetime_undef_if_invalid => 1,
    is_nullable => 1,
  },
  "finishedtime",
  {
    data_type => "timestamp",
    datetime_undef_if_invalid => 1,
    is_nullable => 1,
  },
);

=head1 PRIMARY KEY

=over 4

=item * L</transactionid>

=back

=cut

__PACKAGE__->set_primary_key("transactionid");


# Created by DBIx::Class::Schema::Loader v0.07049 @ 2019-04-03 15:12:23
# DO NOT MODIFY THIS OR ANYTHING ABOVE! md5sum:CYVvW7HEj1f/YZimL5LAFQ


# You can replace this text with custom code or comments, and it will be preserved on regeneration
1;
