# UO_TE_v2.2
UO-The Expanse Server engine v2.2
Maintained by: Raist

# [UO-The Expanse]

UO-The Expanse was an Ultima Online freeshard server emulator written in C#, utilizing a hybrid engine of RunUO, Orb3.0, and ServUO, that was available to the public beginning in 2011. July 2017 was the final month for UO-The Expanse and the freeshard went offline. This version is almost an exact replica of the final, playable version of UO-The Expanse, with a few new bells and whistles that never were released when the freeshard was live. A lot of time and work has gone into creating content for this 'shard, but most importantly is the amount of support for staff members in places like Green Acres (Felucca/Trammel). Xmlspawners have been set up to offer training to new 'shard owners and the staff. In order to interact with some of the content, you must have the custom client for UO-The Expanse. With the exception of the BUSHIDO and NINJITSU books, all other OSI Spellbooks have an updated user interface with extra buttons and spell icons that can be positioned and locked in place. Most of the OSI gumps have been replaced/altered by custom artwork, including the Spell Icons. A few new items have been added, like Flaming Trees created by Eri (Thank you, Eri!), Lightsaber hilt, Overcharged Moongates, 10 sets of Doors, 12 sets of Walls, Hues, and Fonts. Three animations were installed from the UO Brazilian Reborn Mount Patch, created by soulblighter666@hotmail.com (Thanks SB!): Raptor (204, 0xCC), Beetle (222, 0xDE), Chickenhawk (226, 0xE2). All commands for staff are proceeded by a [, and then the Command, like [ADD Xmlspawner.
Hope you have fun with all this! - Raist/Tass23

### Installation

Follow these instructions to install and run UO-The Expanse v2.2 Freeshard:

#### Windows
The Freeshard is set up to use the Client already installed (which is the same Client being used by the BME Freeshard) in the C:\UO_TE folder, but if any changes are made to the install location, the file path to the Client files has to be changed in Freeshard under Scripts->Misc->DataPath.cs (this must be repeated for the BME Freeshard as well!). To run the Freeshard, simply doubleclick UO-The Expanse v2.2 Freeshard icon on the Desktop (the Freeshard uses a RED icon, whereas the BME Freeshard uses a BLUE icon), and this launches server.exe inside the C:\UO_TE\Freeshard folder. This system also includes the Ultima and Server folders, so if any Core changes need to be made, the entire Freeshard can be compiled again by running `RecompileCore.bat` and follow the on-screen prompts. This will compile both the Server binary and Ultima binary and then launch the Freeshard at end. Both Freeshards are set up with a SAVES folder, and a 'current' save (this is needed for some features to work without crashing the client). An Owner account already exists, with a character named Raist. Feel free to change it, and the current password! The login information for the Admin account is:
** Username: Admin **
** Password: admin **

If you ever attempt to Recompile, and it fails, the server.exe file is DELETED.
Extract the server.zip archive, which contains a backup of server.exe, into the ROOT folder.
This helps to make sure a server.exe always exists, in case the launch one is deleted.

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
