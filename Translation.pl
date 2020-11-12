

use strict;

use IO::All;

    open (OUT , ">", "translations.txt");
    
    print OUT "Module Translate

    Public Sub Run(Name As String)

        Select Case Name
";

    my @files    = io->dir('./Installer_Src/Setup DreamWorld/')->all;            # Get a list of files
    
    my $lastname;
    foreach my $file  (sort @files)
    {
        my $filename = $file->filename;
        next if $filename !~ /Designer.vb$/;
        
        my $shortname = $filename;
        my $modulename = $filename;
        
        $modulename =~ s/\.Designer\.vb//;

        $shortname =~ s/\.Designer\.vb//i;

        next if $shortname eq 'Translate';
        
        
        print OUT "Case \"$shortname\"\n";
        print "Case \"$shortname\"\n";

        
        my  $contents = io->file($file)->slurp;    # Read an entire file
        my @lines = split("\n",$contents);
        foreach my $line (sort @lines) {
            
            if ($line =~ /My.Resources/)
            {
                
                $line =~ s/Me[\.,]/$modulename\./g;
                $line =~ s/\.Designer\.vb//;
                
                print OUT "$line\n";
                print "$line\n";
                
                
            }
            
        }
        
    }
    
    print OUT "        End Select

    End Sub

End Module
\n";

    print "        End Select

    End Sub

End Module
\n";
            
    


