#!perl
# author - Fred Beckhusen
# AGPL 3.0

use strict;
use  warnings;

=pod

APIKey=123 must match the Voice password set in the Settings->Speech Panel

Minimum URL to say "Hello" in the default voice
http://127.0.0.1/TTS/tts.plx?&APIKey=123&TTS=Hello

If your Apache port is not  80, you must add it:
http://127.0.0.1:80/TTS/tts.plx?&APIKey=123&TTS=Hello

Speak Default Voice out of the server speakers. The HTTP responses is a 200 OK and "Spoken"
http://192.168.2.139/TTS/tts.plx?&APIkey=123&Speak=1&TTS=Hello

Use Ziras voice to say 1 2 3 4 5 
http://127.0.0.1/TTS/tts.plx?TTS=test 1 2 3 4 5&Voice=Zira&APIKey=123

Force a Male voice to say A B C D
http://127.0.0.1/TTS/tts.plx?TTS=A B C D&APIKey=123&Sex=M

Force a Male voice to say A B C D
http://127.0.0.1/TTS/tts.plx?TTS=M:A B C D&APIKey=123

Force a Feale voice to say A B C D E
http://127.0.0.1/TTS/tts.plx?TTS=F:A B C D E&APIKey=123

Parameters:
TTS= Something to say
If a line beings with M: it will speak in a Male voice
If a line beings with F: it will speak in a female voice
Sex=M  it will speak in a Male voice
Sex=F  it will speak in a female voice
Voice=Zira|Mark|Some SAPI5 voice name

APIKey=123 must match the Voice password set in the Settings->Speech Panel
Speak=Any value will cause it to speak using the default voice device, typically the servers speakers.
The response should be the world "Spoken"
If Speak is left off, a 301 Redirect to the mp3 will be sent.


=cut

my $debug = 0; # set to any value but 0 to be able to test parts of it.

$|=1;
use CGI qw(:standard);
use Config::IniFiles;
use File::BOM;  # fixes a bug in Perl with UTF-8
# get the path to the Settings.ini
use Cwd;
my $path = getcwd();


$path =~ /(.*?\/Outworldzfiles)/i;
my $file = $1 . '/Settings.ini';

 # Read the Right Thing from a unicode file with BOM:
open(CONFIG , '<:via(File::BOM)', $file);   
my $Config = Config::IniFiles->new(-file => *CONFIG);

if (! $Config)  {
	print header;
	print "Cannot read INI";
	return;
}
my $APIDefault = '123';
my $TTSDefault = 'Hello there!';
my $VoiceDefault = 'eva';
my $SexDefault = '';
my $SpeakDefault = '';

if ($debug) {
	$APIDefault = $Config->val('Data','APIKey');
	$TTSDefault = 'test 1,2,3';
	$VoiceDefault = 'Zira';
	$SexDefault = '';		# M or F for Male or female if no Voice is specified.
	$SpeakDefault = '';
}
	
# Read the key and text to speak
use CGI qw(:standard);
my $Input = CGI->new();

my $APIKey = $Input->param('APIKey') || $APIDefault;
my $TTS  = $Input->param('TTS') || $TTSDefault;
if (!$TTS) {
	print header;
	print "Error, no text to speak!";
	exit;
}
my $Sex  = $Input->param('Sex') || $SexDefault;
my $Voice  = $Input->param('Voice') || $VoiceDefault;
my $Speak = $Input->param('Speak') || $SpeakDefault;

my $key = $Config->val('Data','APIKey')|| '';
if ($key ne $APIKey) {
	print header();
	print "Bad API Key";
	exit;
}

my $Url =$Config->val('Data','PublicIP') || '127.0.0.1';
my $Port = $Config->val('Data','DiagnosticPort') || '8001';
my $Password = $Config->val('Data','MachineID') || '';

my $url = "http://$Url:$Port/TTS?TTS=$TTS&Voice=$Voice&Password=$Password&Sex=$Sex";
if ($Speak) {
	$url .= "&Speak=$Speak";
}

#print $url;

use LWP::UserAgent;
my $ua = LWP::UserAgent->new;
$ua->agent("DreamGrid");	 
 
# Create a request
my $req = HTTP::Request->new(GET => $url);

# Pass request to the user agent and get a response back
my $res = $ua->request($req);
 my $r = $res->content;
# Check the outcome of the response
if ($res->is_success) {
	if ($Speak) {
		print header;
		print "Spoken";
	} elsif (length($r) > 0 ) {				
		# send a redirect	
		print $Input->header(-location => $r);
	} else {
		print header;
		print "Error, no file generated";
	}
	
}
else {
	print header();
	print $res->status_line, "\n";
}