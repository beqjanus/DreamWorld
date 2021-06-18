echo "Scrubbing Directories"
echo "*.csproj" 
find . -name "*.csproj" -type f -print0 | xargs -0 /bin/rm -f
echo "*.csproj.user" 
find . -name "*.csproj.user" -type f -print0 | xargs -0 /bin/rm -f
echo "*.build"
find . -name "*.build" -type f -print0 | xargs -0 /bin/rm -f
echo "*Temporary*" 
find . -name "*Temporary*" -type f -print0 | xargs -0 /bin/rm -f
echo "*.cache"
find . -name "*.cache" -type f -print0 | xargs -0 /bin/rm -f
echo "*.rej"
find . -name "*.rej" -type f -print0 | xargs -0 /bin/rm -f
echo "*.orig"
find . -name "*.orig" -type f -print0 | xargs -0 /bin/rm -f
echo "*.pdb"
find . -name "*.pdb" -type f -print0 | xargs -0 /bin/rm -f
echo "*.mdb"
find . -name "*.mdb" -type f -print0 | xargs -0 /bin/rm -f
echo "*obj"
find . -name "*obj" -type f -print0 | xargs -0 /bin/rm -rf
echo "obj"
find . -name "*.swp" -type f -print0 | xargs -0 /bin/rm -f
