#!/bin/bash
echo "Stopping running instance and delete old version of application"
apt-get install sshpass
sshpass -p '$KOLOBOKSPASSWORD' ssh koloboks@185.159.130.85 pkill -f 'dotnet' | pkill -f 'dotnet' | rm -rf /usr/details/details-output
sshpass -p '$KOLOBOKSPASSWORD' scp -r ./details-output/ koloboks@185.159.130.85:/usr/details/
sshpass -p '$KOLOBOKSPASSWORD' ssh koloboks@185.159.130.85 dotnet /usr/details/details-output/WebApplication.dll &