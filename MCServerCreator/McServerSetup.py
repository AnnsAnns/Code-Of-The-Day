import sys
import os
import shutil
import time
import urllib.request

server_properties = """#Minecraft server properties
#Sun Aug 18 14:55:49 CEST 2019
spawn-protection=0
max-tick-time=60000
query.port=25565
generator-settings=
force-gamemode=false
allow-nether=true
enforce-whitelist=false
gamemode=survival
broadcast-console-to-ops=true
enable-query=false
player-idle-timeout=0
difficulty=normal
spawn-monsters=true
broadcast-rcon-to-ops=true
op-permission-level=4
pvp=true
snooper-enabled=true
level-type=default
hardcore=false
enable-command-block=true
max-players=20
network-compression-threshold=256
resource-pack-sha1=
max-world-size=29999984
rcon.port=25575
server-port=25565
server-ip=
spawn-npcs=true
allow-flight=false
level-name=world
view-distance=10
resource-pack=
spawn-animals=true
white-list=false
rcon.password=
generate-structures=true
online-mode=true
max-build-height=256
level-seed=
use-native-transport=true
prevent-proxy-connections=false
enable-rcon=false
motd=Created by Meme's py tool"""

eula = """#By changing the setting below to TRUE you are indicating your agreement to our EULA (https://account.mojang.com/documents/minecraft_eula).
#Sat Dec 01 20:50:15 CET 2018
eula=True"""

start_bat = """@echo off
java -Xmx2G -Xms1G -XX:MaxPermSize=128m -jar "server.jar" nogui"""

ops = "[]"

versiondict = {
	"1.16.4": "https://launcher.mojang.com/v1/objects/35139deedbd5182953cf1caa23835da59ca3d7cd/server.jar",
	"1.16.3": "https://launcher.mojang.com/v1/objects/f02f4473dbf152c23d7d484952121db0b36698cb/server.jar",
	"1.16.2": "https://launcher.mojang.com/v1/objects/c5f6fb23c3876461d46ec380421e42b289789530/server.jar",
	"1.15.2": "https://launcher.mojang.com/v1/objects/bb2b6b1aefcd70dfd1892149ac3a215f6c636b07/server.jar",
	"1.14.4": "https://launcher.mojang.com/v1/objects/3dc3d84a581f14691199cf6831b71ed1296a9fdf/server.jar",
	"1.14.3": "https://launcher.mojang.com/v1/objects/d0d0fe2b1dc6ab4c65554cb734270872b72dadd6/server.jar",
	"1.14.2": "https://launcher.mojang.com/v1/objects/808be3869e2ca6b62378f9f4b33c946621620019/server.jar",
	"1.14.1": "https://launcher.mojang.com/v1/objects/ed76d597a44c5266be2a7fcd77a8270f1f0bc118/server.jar",
	"1.14": "https://launcher.mojang.com/v1/objects/f1a0073671057f01aa843443fef34330281333ce/server.jar",
	"1.13.2": "https://launcher.mojang.com/v1/objects/3737db93722a9e39eeada7c27e7aca28b144ffa7/server.jar",
	"1.13.1": "https://launcher.mojang.com/v1/objects/fe123682e9cb30031eae351764f653500b7396c9/server.jar",
	"1.13": "https://launcher.mojang.com/v1/objects/d0caafb8438ebd206f99930cfaecfa6c9a13dca0/server.jar",
	"1.12.2": "https://launcher.mojang.com/v1/objects/886945bfb2b978778c3a0288fd7fab09d315b25f/server.jar",
	"1.11.2": "https://launcher.mojang.com/v1/objects/f00c294a1576e03fddcac777c3cf4c7d404c4ba4/server.jar",
	"1.10.2": "https://launcher.mojang.com/v1/objects/3d501b23df53c548254f5e3f66492d178a48db63/server.jar",
	"1.9.4": "https://launcher.mojang.com/v1/objects/edbb7b1758af33d365bf835eb9d13de005b1e274/server.jar",
	"1.8.9": "https://launcher.mojang.com/v1/objects/b58b2ceb36e01bcd8dbf49c8fb66c55a9f0676cd/server.jar",
	"1.7.10": "https://launcher.mojang.com/v1/objects/952438ac4e01b4d115c5fc38f891710c4941df29/server.jar"
}

def checkforversion(versionstring):

	if versionstring in versiondict:
		return versiondict[versionstring]

def checkandcreatefile(file, variable):
	if os.path.isfile(f"{file}") == False:
		print(f"{file} not found... Creating file")
		with open(f"{file}", "w") as text_file:
			text_file.write(variable)

def mainfunction(args):
	pathtoserver = "server"
	print("Script running:\n")

	if len(args) == 1:
		return "No arguments were given, exiting"
	if len(args) > 1:
		versionlink = checkforversion(args[1])
		if versionlink == None:
			return f"No link was found for the specified version\n\nAvailable versions are:\n{', '.join(versiondict.keys())}"
	if os.path.isdir(pathtoserver) == True:
		if len(args) > 2:
			if args[2] == "Clean" or args[2] == "clean":
				shutil.rmtree(pathtoserver)
				print("Deleted server folder")
				time.sleep(1)
				os.mkdir(pathtoserver)
				print("Made new server folder")

	else:
		os.mkdir(pathtoserver)
		print("Made new server folder")
	save_path = pathtoserver + "/server.jar"
	print("Downloading server file...")
	with urllib.request.urlopen(versionlink) as response, open(save_path, 'wb') as out_file:
		shutil.copyfileobj(response, out_file)
	print("Download done!")

	checkandcreatefile("server/server.properties", server_properties)
	checkandcreatefile("server/eula.txt", eula)
	checkandcreatefile("server/start.bat", start_bat)
	checkandcreatefile("server/ops.json", ops)

	return "Success!"


if __name__ == '__main__':
	print("\nOutput: {}".format(mainfunction(sys.argv)))