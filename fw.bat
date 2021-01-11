netsh advfirewall firewall delete rule name="Diagnostics TCP Port 8001"
netsh advfirewall firewall delete rule name="Opensim HTTP TCP Port 8002"
netsh advfirewall firewall delete rule name="Opensim HTTP UDP Port 8002"
netsh advfirewall firewall delete rule name="Icecast Port1 UDP 8100"
netsh advfirewall firewall delete rule name="Icecast Port1 TCP 8100"
netsh advfirewall firewall delete rule name="Icecast Port2 UDP 8101"
netsh advfirewall firewall delete rule name="Icecast Port2 TCP 8101"
netsh advfirewall firewall delete rule name="Apache HTTP Web Port 80"
netsh advfirewall firewall delete rule name="Region TCP Port 8004"
netsh advfirewall firewall delete rule name="Region UDP Port 8004"
netsh advfirewall firewall add rule name="Diagnostics TCP Port 8001" dir=in action=allow protocol=TCP localport=8001
netsh advfirewall firewall add rule name="Opensim HTTP TCP Port 8002" dir=in action=allow protocol=TCP localport=8002
netsh advfirewall firewall add rule name="Opensim HTTP UDP Port 8002" dir=in action=allow protocol=UDP localport=8002
netsh advfirewall firewall add rule name="Apache HTTP Web Port 80" dir=in action=allow protocol=TCP localport=80
netsh advfirewall firewall add rule name="Icecast Port1 UDP 8100" dir=in action=allow protocol=UDP localport=8100
netsh advfirewall firewall add rule name="Icecast Port1 TCP 8100" dir=in action=allow protocol=TCP localport=8100
netsh advfirewall firewall add rule name="Icecast Port2 UDP 8101" dir=in action=allow protocol=UDP localport=8101
netsh advfirewall firewall add rule name="Icecast Port2 TCP 8101" dir=in action=allow protocol=TCP localport=8101
netsh advfirewall firewall add rule name="Region TCP Port 8004" dir=in action=allow protocol=TCP localport=8004
netsh advfirewall firewall add rule name="Region UDP Port 8004" dir=in action=allow protocol=UDP localport=8004

