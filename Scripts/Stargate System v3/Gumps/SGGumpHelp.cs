using System;
using Server;
using Server.Gumps;

namespace Server.Gumps
{
	public class SGGumpHelp : Gump
	{
        public string instructions = "";

        public SGGumpHelp() : base( 100, 100 )
        {
            this.Closable = true;
            this.Disposable = true;
            this.Dragable = true;
            this.Resizable = false;
            this.AddPage(0);
            this.AddBackground(0, 0, 350, 400, 9270);
            this.AddBackground(10, 10, 330, 380, 83);
            this.AddLabel(40, 20, 1153, @"Stargate Information, Help & Instructions");
            this.AddButton(25, 350, 4017, 4019, (int)Buttons.Button1, GumpButtonType.Reply, 0);
            this.AddLabel(60, 351, 64, @"Exit / Close");

            this.AddHtml(30, 60, 290, 270, "<BODY>" +

                // Instruction Text...

                "<BASEFONT COLOR=#00EEFF>" + // Heading Colour
                "*** Welcome ***<br><br>" +
                "<BASEFONT COLOR=#FFAA00>" + // Normal Text

                "Here you will hopfully find the answers to any questions you might have regarding how this system works, please continue...<br><br>" +

                "<BASEFONT COLOR=#00EEFF>" + // Heading Colour
                "*** General Information ***<br><br>" +
                "<BASEFONT COLOR=#FFAA00>" + // Normal Text

                "At first the whole thing may seem a little confusing but don't worry, it's not all that hard to master the art of using this system at all. " +
                "The systems like any other, once you've started using them they soon become second nature, so hows it all work...<br><br>" +
                "This is a travel system, like most others where there is a location your going from and a destination you wish to get to. " +
                "The difference with this system is the selections you have once you open the control window (The stargate gump), all your required " +
                "to do is dial a code for your destination, depending which code you've dialed decide's where you'll end up.<br><br>" +

                "<BASEFONT COLOR=#00EEFF>" + // Heading Colour
                "*** Platform's & Crystals ***<br><br>" +
                "<BASEFONT COLOR=#FFAA00>" + // Normal Text

                "When you get to a platform you should see the platform, with a Crystal Control Device (CCD) next to it, also along the front " +
                "(South Facing Side) you'll also see five symbols, next to the CCD theres also an additional symbol. The symbol next to " +
                "the CCD is the symbol which represents the facet code, the symbols at the front of the platform's are the actual address's " +
                "of the gate itself, the address is from left to right (West to East). The same information is displayed in the top left of the gump when you open it.<br><br>" +

                "When you open the gump the crystal will start to cycle through various colours and play a low pulsing sound to " +
                "let you know it's being accessed, if it's already being used by another player you will be unable to open the gump " +
                "until the other player has finished using it. This is also the same if theres already been a gate opened from or to the " +
                "location your at, if you wish to use the stargate you will have to wait until it has cleared from the last use.<br><br>" +

                "If the CCD is red then the shards stargate system has been disabled by the shard admin, if this is the case " +
                "you will be unable to open the CCD's gump.<br><br>" +

                "<BASEFONT COLOR=#00EEFF>" + // Heading Colour
                "*** Control Gump Layout ***<br><br>" +
                "<BASEFONT COLOR=#FFAA00>" + // Normal Text
                "When you open the CCD's control gump you will have to dial the address you want within a specified time span, "+
                "the amount of time by default is set to two minutes, after this time the gump will " +
                "automatically close and release the dialer allowing other players to have access to it, click the CCD again to start over.<br><br>" +

                "This amount of time may be different between shards depending how the shard administrator has setup the system.<br><br>" +

                "With the gump open you should see the following...<br><br>" +
                "<BASEFONT COLOR=#FF6677>" + // Colour
                "Your Location<br>" +
                "<BASEFONT COLOR=#FFAA00>" + // Normal Text
                "Where you are (your current location) is displayed on the top left of the gump, here you will see the name of this location " +
                "followed by 6 address symbols below it, from left to right this address always starts with the facet symbol followed " +
                "by the five symbols which represent this locations address. Your location is displayed by green symbols.<br><br>" +

                "<BASEFONT COLOR=#FF6677>" + // Colour
                "Discovered By<br>" +
                "<BASEFONT COLOR=#FFAA00>" + // Normal Text
                "This is the name of the person who found and used this location first. All CCD's which are used for the first time remember the names " +
                "of the players who found and used them.<br><br>" +

                "<BASEFONT COLOR=#FF6677>" + // Colour
                "Facet Information<br>" +
                "<BASEFONT COLOR=#FFAA00>" + // Normal Text
                "Under the destination selection theres also a gate count for each facet, the count is a total number of stargates " +
                "on each facet, below that number is the number of hidden locations which are still to be discovered on the selected facet. " +
                "If there are any undiscovered locations in the list they are hidden to players until someone finds and uses the locations " +
                "CCD. Finding and using stargate CCD's reveals them to other players that are using the stargate system anywhere else on the shard.<br><br>" +

                "<BASEFONT COLOR=#FF6677>" + // Colour
                "Reset Button<br>" +
                "<BASEFONT COLOR=#FFAA00>" + // Normal Text
                "You can press the reset button at anytime to reset the address selection back to it's default state, this clears any previously " +
                "dialed codes and resets everything.<br><br>" +

                "<BASEFONT COLOR=#FF6677>" + // Colour
                "Cancel Button<br>" +
                "<BASEFONT COLOR=#FFAA00>" + // Normal Text
                "Not too hard to figure out, closes the gump and turns the CCD off.<br><br>" +

                "<BASEFONT COLOR=#FF6677>" + // Colour
                "Activate Button<br>" +
                "<BASEFONT COLOR=#FFAA00>" + // Normal Text
                "Pressing this will start the stargate up, provided you've entered a valid location and theres no open gate at the location you want " +
                "to go to then things will start to happen.<br><br>If you've entered a location that does not exsist the gate will fail to make a connection " +
                "and just shutdown. If theres already an open gate at the selected destination you tried you will be told.<br><br>" +

                "<BASEFONT COLOR=#FF6677>" + // Colour
                "Dialing & Speed Dialing<br>" +
                "<BASEFONT COLOR=#FFAA00>" + // Normal Text
                "In order to go somewhere you need to dial the location you want, there are also a few shortcuts you can take too which " +
                "i've called speed dialing, but i'll come back to those.<br><br>" +

                "In your gumps 'Destination Seletion' you'll have four red symbols and one white symbol, the red symbols are the symbols (or codes) " +
                "that have not been selected yet, the white symbol is the symbol which is ready to be selected.<br><br>You'll also notice the facet symbol " +
                "to the left of those, the facet symbol can be changed at any time during the dialing sequence with the 'Destination Facet' buttons.<br><br>" +
                "Each facet symbol is automatically entered into the destination selection and the facet name is also displayed near the buttons " +
                "to help you know which facet your dialing to. Each time the facet is changed the list of addresses (if any exsist) for that facet are show, " +
                "if there are no stargates on the facet you've picked then the list will remain empty.<br><br>" +

                "To choose a symbol to enter into the destination address (the white icon) pick one from the right handside of the gump, when you " +
                "do this the selection is entered and the next symbol to pick becomes white, moving along for each symbol in the destination selected " +
                "area of the gump until there all green, from there you may use the activate option. Provided the address is there, your on your way.<br><br>" +

                "To 'speed dial' somewhere you could just press the activate button, provided the location address is a valid one, i'll explain an example...<br><br>" +

                "Two stargates exsist, one on FEL and one on TRAMMEL, both gates can have the SAME address because there on DIFFERENT facets, lets say these two " +
                "gates share the address 1,1,1,1,1 (which just happens to be the default setting), what you could do is just flip the facet selection " +
                "from one facet to the other and press activate (no need to dial 1,1,1,1,1 since it's already displayed).<br><br>" +

                "The same technique can be applied when dialing any address, you only need the symbols for a valid address in the destination selection " +
                "before pressing the activate button, so if there's addresses like (1st address 1,1,1,1,1) & (2nd address 2,1,1,1,1) the first symbol needs to be changed from 1 to a 2, " +
                "then press activate and your there. If it sounds confusing, don't worry it's not, you'll pick it up.<br><br>" +

                "<BASEFONT COLOR=#FF6677>" + // Colour
                "Symbol Selector<br>" +
                "<BASEFONT COLOR=#FFAA00>" + // Normal Text
                "This is the part for picking the code, the best way to think of this part is like a clock face, start at the top & work around " +
                "the numbers, 1st symbol represents code 1, 2nd symbol is code 2 etc etc through to 5.<br><br>" +

                "All of the stargates need a five digit code to work and there all set out in the same order, from left to right " +
                "on the platforms.<br><br>" +

                "<BASEFONT COLOR=#00EEFF>" + // Heading Colour
                "*** Finally ***<br><br>" +
                "<BASEFONT COLOR=#FFAA00>" + // Normal Text
                "You've finally reached the end of this instruction gump. Hopefully i've covered most of the basics you'll need to know, " +
                "if not i'm sure your shard administrator or GM's can help.<br><br>" +

                "<BASEFONT COLOR=#00EEFF>" + // Heading Colour
                "*** Credits ***<br><br>" +
                "<BASEFONT COLOR=#00DD44>" + // Normal Text
                "Stargate System v3.0 By FingersMcSteal<br>" +
                "Written for RunUO 2.0 SVN Revision 199<br>" +
                "Using 6.0.3.1 client files for gump and items<br>" +

               // End of Instruction Text...
               
               "</BODY>", (bool)false, (bool)true);

        }

        public enum Buttons
        {
            Button1,
        }

    }
}