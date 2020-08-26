netsh advfirewall firewall  delete rule name="Opensim TCP Port 5001"
netsh advfirewall firewall  delete rule name="Opensim HTTP TCP Port 5002"
netsh advfirewall firewall  delete rule name="Opensim HTTP UDP Port 5002"
netsh advfirewall firewall  delete rule name="Apache HTTP Web Port 80"
netsh advfirewall firewall  delete rule name="Region TCP Port 5004"
netsh advfirewall firewall  delete rule name="Region UDP Port 5004"
netsh advfirewall firewall  add rule name="Opensim TCP Port 5001" dir=in action=allow protocol=TCP localport=5001
netsh advfirewall firewall  add rule name="Opensim HTTP TCP Port 5002" dir=in action=allow protocol=TCP localport=5002
netsh advfirewall firewall  add rule name="Opensim HTTP UDP Port 5002" dir=in action=allow protocol=UDP localport=5002
netsh advfirewall firewall  add rule name="Apache HTTP Web Port 80" dir=in action=allow protocol=TCP localport=80
netsh advfirewall firewall  add rule name="Region TCP Port 5004" dir=in action=allow protocol=TCP localport=5004
netsh advfirewall firewall  add rule name="Region UDP Port 5004" dir=in action=allow protocol=UDP localport=5004

