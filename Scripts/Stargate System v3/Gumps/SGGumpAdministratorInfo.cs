using System;
using Server;
using Server.Gumps;

namespace Server.Gumps
{
    public class SGGumpAdministratorInfo : Gump
	{
        public string instructions = "";

        public SGGumpAdministratorInfo() : base( 200, 50 )
        {
            this.Closable = true;
            this.Disposable = true;
            this.Dragable = true;
            this.Resizable = false;
            this.AddPage(0);
            this.AddBackground(0, 0, 350, 400, 9270);
            this.AddBackground(10, 10, 330, 380, 83);
            this.AddLabel(70, 20, 1153, @"Stargate Administrator Information");
            this.AddButton(25, 350, 4017, 4019, (int)Buttons.Button1, GumpButtonType.Reply, 0);
            this.AddLabel(60, 351, 64, @"Exit / Close");

            this.AddHtml(30, 60, 290, 270, "<BODY>" +

                // Administration Text...

                "<BASEFONT COLOR=#00EEFF>" + // Heading Colour
                "*** General Information ***<br><br>" +
                "<BASEFONT COLOR=#FFAA00>" + // Normal Text

                "Below is a brief description of each section on the Stargate Administration Panel with a small description of what each option does.<br><br>" +

                "<BASEFONT COLOR=#FF6677>" + // Colour
                "Stargates on this server<br>" +
                "<BASEFONT COLOR=#FFAA00>" + // Normal Text
                "This shows the current amount of stargates your server has in the WHOLE game world (i.e All Facets).<br><br>" +

                "<BASEFONT COLOR=#FF6677>" + // Colour
                "Stargate locations avaliable<br>" +
                "<BASEFONT COLOR=#FFAA00>" + // Normal Text
                "This shows the amount of remaining gate addresses/platforms you can add to your server, this number is limited to 15625 in total due to " +
                "each stargate's need to have a unique dialing code.<br><br>" +

                "Each code digit/symbol goes from 1-5 and theres 5 digit/symbols in each stargate address, which means you have 5x5x5x5x5=3125 possible addresses per facet with this system. " +
                "So for 5 facets/maps thats 5x3125 = 15625 different locations avaliable using this system.<br><br>" +

                "You can however use the same code for an address but on a different facet/map.<br><br>" +

                "<BASEFONT COLOR=#FF6677>" + // Colour
                "Status<br>" +
                "<BASEFONT COLOR=#FFAA00>" + // Normal Text
                "Pretty simple to follow, either enabled or disabled.<br><br>" +

                "If you've disabled your shards stargates the only people on your shard that can access the crystals at each platform are GM or above access level, this " +
                "is to allow GM's to still use the gate system or delete stargate platforms.<br><br>" +
                "Platforms can only be deleted if the system is DISABLED, and the only person " +
                "allowed to disable the system is the ADMINISTRATOR access level or above.<br><br>" +

                "If a stargate is activated by a GM or Admin while the systems disabled ANYONE can still use those opened gates.<br><br>" +

                "<BASEFONT COLOR=#66FF77>" + // Colour
                "Adding A Stargate<br>" +
                "<BASEFONT COLOR=#FFAA00>" + // Normal Text
                "The details from the current facet / map section in BLUE are the details of the next gate that will be added to your world when you select the OK option.<br><br>" +

                "The platform style & type can be changed using the two arrows provided to the right of this option, the system comes with 3 pre-defined addon's, each in " +
                "East & South facing positions.<br><br>" +
                "The platform direction option will show which way the platform will face, which is changed in the same way " +
                "as the platform styles are.<br><br>" +

                "To enter a platform name, move your cursor over the name entry box and select a new name removing the default unnamed entry, if you don't this will be the name of the " +
                "gate you add 'Default Unnamed', you will then have to remove the gate (IN THE CORRECT WAY, see below for that) and start over.<br><br>" +

                "The address or code for the gate can be selected in much the same way, by default the code in the admin gump is 1,2,3,4,5 which you can change " +
                "each time you add a new gate, the system also checks to ensure you don't enter a duplicated address on the facet your on, you will be told if " +
                "you have and the gate does not get added.<br><br>" +

                "<BASEFONT COLOR=#66FF77>" + // Colour
                "*** IMPORTANT *** Deleting a gate <br>" +
                "<BASEFONT COLOR=#FFAA00>" + // Normal Text
                "Stargates MUST BE DELETED IN THE CORRECT WAY...<br><br>" +
                "If you want to delete a gate it MUST BE DONE FROM THE CRYSTAL CONTROL DEVICE AT THE STARGATE ITSELF, if you just delete it from your " +
                "world using the remove function from the admin menu it will wipe it BUT the SYSTEM THINKS IT'S STILL THERE !!!, remember " +
                "the system MUST be disabled to do this.<br><br>" +

                "<BASEFONT COLOR=#00EEFF>" + // Heading Colour
                "*** Control Options ***<br><br>" +
                "<BASEFONT COLOR=#FFAA00>" + // Normal Text
                "Here you have five additional buttons, there functions are explained below...<br><br>" +

                "<BASEFONT COLOR=#FF6677>" + // Colour
                "Enable / Disable<br>" +
                "<BASEFONT COLOR=#FFAA00>" + // Normal Text
                "Nuff said.<br><br>" +

                "<BASEFONT COLOR=#FF6677>" + // Colour
                "Generate HTML<br>" +
                "<BASEFONT COLOR=#FFAA00>" + // Normal Text
                "Selecting this option will produce an additional file in the 'Stargate Data' folder, found inside the servers 'Data' folder<br><br>" +

                "This file is a HTML file containing EVERY stargate address for EVERY facet, it will show each stargates address details.<br><br>" +

                "This was done to help shard admins view all of the stargate data for there shards, including if a stargate address was used or not, " +
                "which on a shard with alot of stargates can be very helpful when guessing new stargate codes for new gates.<br><br>" +
                "Mainly an Admin help tool, or if you wanted you could place it on your shards web site too so players could view all there shards addresses.<br><br>" +

                "<BASEFONT COLOR=#FF6677>" + // Colour
                "*** IMPORTANT *** Force XML File Save<br>" +
                "<BASEFONT COLOR=#FFAA00>" + // Normal Text
                "This button will force a save of CURRENT stargate information into the XML file (CAUSING AN OVERWRITE OF THIS FILE), so if " +
                "you've just deleted gates (in the correct way) they will not be saved to the file.<br><br>" +

                "There is a posibility of loosing all of your stargate location data if clicked after a [sgdelete command is used, this is " +
                "why the buttons labled RED as a precaution to you.<br><br>"+

                "It's there to be clicked if your just adding gates and don't intend to let the shard do a world save before a shutdown command is issued.<br><br>" +

                "Basically, do all of your stargate edits, additions or deletions then use it when your finished with the admin side of the system.<br><br>" +

                "<BASEFONT COLOR=#FF6677>" + // Colour
                "*** IMPORTANT *** Force XML File Re-Load<br>" +
                "<BASEFONT COLOR=#FFAA00>" + // Normal Text
                "Pressing this button will force a Re-Load of the CURRENT XML file contained in the 'Stargate Data' folder.<br><br>" +

                "If your shard has not done a world save, or you have not updated the XML file (by using the 'force save' button) it will " +
                "just re-load the exsisting XML file.<br><br>" +

                "<BASEFONT COLOR=#FF6677>" + // Colour
                "Exit Administration Panel<br>" +
                "<BASEFONT COLOR=#FFAA00>" + // Normal Text
                "Obvious, exit's the admin panel.<br><br>" +

                "<BASEFONT COLOR=#00EEFF>" + // Heading Colour
                "*** How It Works ***<br><br>" +
                "<BASEFONT COLOR=#FFAA00>" + // Normal Text
                "When the shard is 1st run the system checks and creates the working direcroties it needs, up to this point theres no XML file saved.<br><br>" +
                "The system then creates a XML file called 'SGData.xml' in it's own folder at each world save.<br><br>" +

                "When a shard is 1st run and a 'SGData.xml' file is found it loads this file and builds each stargate location into your shard using the details " +
                "of this file, the system was built like this to allow an element of interchangability between shards, in english... you could " +
                "in theory use someone elses 'SGData.xml' files, allowing you to try out their stargate locations in your own shard.<br><br>" +

                "With careful edits of the 'SGData.xml' file data (which i haven't tried myself), in theory could allow people to " +
                "send new gate locations to their shard admin's to add and try out, again this is something which i think could be done " +
                "but i've not tried myself, if you intend to try this out... ALWAYS SAVE A BACKUP OF THE SGDATA.XML FILE 1ST !!!<br><br>" +

                "<BASEFONT COLOR=#FF6677>" + // Colour
                "Crystal Control Devices (CCD's)<br>" +
                "<BASEFONT COLOR=#FFAA00>" + // Normal Text
                "The CCD's are the way players use this system & also the way GM's or Administrators delete stargates.<br><br>" +

                "Each CCD has it's own timer, the timers are set to automatically shutdown and release a dialer if a player " +
                "has not activated or used it within 2 minutes.<br><br>" +
                "The CCD's will also shutdown if a player has become disconnected " +
                "from the shard, releasing that dialer and preventing players from locking out the dialer they were using to other players.<br><br>" +

                "Hopefully this is enough information to help you understand how to use this system in the right way, if you have questions " +
                "about this system please ask in the RunUO script release section where you found this script release.<br><br>" +

                "<BASEFONT COLOR=#00EEFF>" + // Heading Colour
                "*** Credits ***<br><br>" +
                "<BASEFONT COLOR=#00DD44>" + // Normal Text
                "Stargate System v3.0 By FingersMcSteal<br>" +
                "Written for RunUO 2.0 SVN Revision 199<br>" +
                "Using 6.0.3.1 client files for gump and items<br><br>" +

                "<BASEFONT COLOR=#FFAA00>" + // Normal Text
                "For support please refer to the appropriate section of the RunUO forums in there script release section for help." +

               // End of Instruction Text...
               
               "</BODY>", (bool)false, (bool)true);

        }

        public enum Buttons
        {
            Button1,
        }

    }
}