
use strict;
use warnings;
use LWP::UserAgent ();
my $ua = LWP::UserAgent->new(timeout => 10);
my $url = "http://grid.myvirtualbeach.com:8002/CAPS/GetTexture/?texture_id=1c0f4007-d476-46c9-bcd9-f5a342e48855&format=jpeg";
$url = "http://blue.outworldz.net:8002/CAPS/GetTexture/?texture_id=4b74cd26-1dc2-4843-836b-3234da74e46d&format=jpeg";

$ua->default_header( 'Connection' => 'keep-alive' );
my $res = $ua->get( $url );
if ($res->is_success) {
    print $res->content;
}
else {
    die $res->status_line;
}