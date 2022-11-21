# UO_TE_v2.1
UO-The Expanse Server engine v2.1
Maintained by: Raist

# [UO-The Expanse]

UO-The Expanse was an Ultima Online freeshard server emulator written in c# that was available to the public starting in 2011. July 2017 was the final month for UO-The Expanse and the server went offline. This version is an exact replica of the final, playable version of UO-The Expanse. A lot of time and work has gone in to creating content for this 'shard, but most importantly is the amount of support for staff members in places like Green Acres (Felucca/Trammel). Xmlspawners have been set up to offer training to new 'shard owners and the staff. In order to interact with all the content, you must also have the custom client for UO-The Expanse. All commands for staff are proceeded by a [, and then the Command. Visit all the custom content via the command [StaffRunebook, which will open a toolbar with each facet listed, as well as Runebooks for each expansion. Hope you enjoy it! - Raist/Tass23

### Installation

Follow these instructions to install UO-The Expanse:

#### Windows
First, extract the free shard Client files into C:\UO_TE, and then just run `RecompileCore.bat` and follow the prompts. This script will compile both the server binary and Ultima binary for you and launch the server at end. After this you can run the server by executing `server.exe`. NOTE: If you change the file path for the free shard client file, please edit Scripts->Misc->DataPath.cs and enter the custom path, THEN run 'RecompileCore.bat'. Otherwise the free shard has no client files to load from and compiling it will cause it to crash.

#### OSX
No solutions exist.

#### Ubuntu
No solutions exist.

### Development
You can submit a pull request. Each request will be reviewed on a first come, first serve basis.
New updates will be included, which means updating any forks, so backup your changes if you fork the main trunk!

License
----

GPL V2
