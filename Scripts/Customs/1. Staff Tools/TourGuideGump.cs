// // // // // // // // // //
// Author: BBraamse        //
// Script: TourGuideGump   //
// Version: v1.0           //
// // // // // // // // // //
using System;
using Server;
using Server.Gumps;
using Server.Mobiles;
using Server.Network;
using Server.Commands;
using Server.Engines.XmlSpawner2;

namespace Server.Gumps
{
    public class TourGuideGump : Gump
    {
        public static void Initialize()
        {
            CommandSystem.Register("TourGuide", AccessLevel.Player, new CommandEventHandler(TourGuide_OnCommand));
        }

        [Usage("TourGuide")]
        [Description("Makes a call to your tour guide gump.")]
        public static void TourGuide_OnCommand(CommandEventArgs e)
        {
            Mobile from = e.Mobile;

            if (from.HasGump(typeof(TourGuideGump)))
                from.CloseGump(typeof(TourGuideGump));
            from.SendGump(new TourGuideGump(from, 0));
        }

        // Private variables
        PlayerMobile caller;
        int iNumber;
        string sMessage;

        // Locations
        Point3D loc0 = new Point3D(3502, 2574, 14);		//Opening New Haven
        Point3D loc1 = new Point3D(3495, 2566, 15);		//New Haven
        Point3D loc2 = new Point3D(1021, 470, -90);		//Staff House
        Point3D loc3 = new Point3D(3571, 2597, 0);		//New Player Champ Spawn
        Point3D loc4 = new Point3D(5758, 66, 0);		//Spellcrafters
		Point3D loc5 = new Point3D(1008, 659, -88);		//Luna Casino
		Point3D loc6 = new Point3D(993, 527, -50);		//Luna Bank
		Point3D loc7 = new Point3D(1094, 509, -90);		//Luna Stargate
		Point3D loc8 = new Point3D(1224, 1723, 0);		//Farming
		Point3D loc9 = new Point3D(1323, 1624, 55);		//Monty Python-Holy Grail
		Point3D loc10 = new Point3D(2334, 1954, -49);	//Jedi Academy
		Point3D loc11 = new Point3D(572, 2014, -84);	//Sith Academy
		Point3D loc12 = new Point3D(5736, 715, 2);		//Gomzul
		Point3D loc13 = new Point3D(3468, 2600, 10);	//Evil Dead
		Point3D loc14 = new Point3D(3702, 2209, 20);	//Skullball
		Point3D loc15 = new Point3D(3686, 2167, 20);	//Rungrim's
		Point3D loc16 = new Point3D(3599, 2160, 48);	//-A-Tron
		Point3D loc17 = new Point3D(997, 728, -90);		//Bomberman
		Point3D loc18 = new Point3D(998, 697, -90);		//Battle Chess
		Point3D loc19 = new Point3D(5556, 1307, 0);		//New Player Dungeon
		Point3D loc20 = new Point3D(1432, 1695, 0);		//Vote Stones
		Point3D loc21 = new Point3D(695, 277, -94);		//Rainbow Mount Zoo

        public TourGuideGump( Mobile from, int iLoc ): base(200, 200)
        {
            caller = (PlayerMobile)from;
            iNumber = iLoc;

            VerifyLocation(caller, iNumber);

            this.Closable=false;
			this.Disposable=false;
			this.Dragable=true;

            // Page 0
            AddPage( 0 );

			AddBackground( 0, 0, 400, 300, 5054 ); // background
			AddAlphaRegion( 15, 15, 370, 270 ); // transparency
			AddImage(235, -13, 665);
			AddImage(235, -14, 61055, 1177);
			AddImage(235, -10, 653, 1080);
			AddImage(235, -13, 50341, 1067);
			AddButton( 290, 260, 12006, 12008, 0, GumpButtonType.Reply, 0 ); // close
			AddBackground(289, 259, 75, 22, 3000);
			AddLabel(309, 261, 1074, "Close");

            // Make a check to see if this is the final stop or not.
            // If true, do not show the continue button.
            // Change the number after the "<" sign to the maximum
            // numbers of locations you have.
            if (iNumber < 21)
            {
                AddButton(290, 235, 12009, 12011, 1, GumpButtonType.Reply, 0); // continue
				AddBackground(289, 234, 75, 22, 3000);
				AddLabel(299, 236, 1074, "Continue");
            }

            AddHtml( 20, 25, 240, 250, sMessage, (bool)false, (bool)true ); // text
        }

        public override void OnResponse( NetState sender, RelayInfo info )
        {
            Mobile from = sender.Mobile;

            switch( info.ButtonID )
            {
                case 0:
                    {
                        // Added support for ArteGordon's XMLSpawner2 system
                        XmlData a = (XmlData)XmlAttach.FindAttachment( from, typeof( XmlData ), "FinishedTour" );

                        if ( a != null && a.Data != "true" )
                        {
                            a.Data = "true";
                        }

                        from.CloseGump( typeof( TourGuideGump ) );
                        from.Location = loc0;
						from.Map = Map.Trammel;
                        from.SendMessage( "You have chosen to end the tour and go to New Haven." );
                        from.Blessed = false;
                        from.Frozen = false;
						from.LightLevel = 10;

                        break;
                    }
                case 1:
                    {
                        from.SendGump(new TourGuideGump( caller, ++iNumber ) );
						from.Blessed = true;
                        from.Frozen = true;
						from.LightLevel = 25;
                        break;
                    }
            }
        }

        public void VerifyLocation( PlayerMobile from, int msgNum )
        {
            switch (msgNum)
            {
                case 0:
                    {
                        from.Location = loc0;
						from.Map = Map.Trammel;
                        sMessage = @"<BASEFONT COLOR=#FFFFFF><CENTER><U>Welcome, " + caller.Name + "!</U></CENTER><BASEFONT COLOR=#FFFF00>"
                            + "<P>Welcome to The Expanse. We, the community, would like to give you a tour of our world. During the tour you will learn some of the new discoveries in this world, as well as places to go for information and entertainment. First timers to this shard are encouraged to take the tour. Make sure you visit the website (www.UOExpanse.com) to read about some of the content during the tour and make sure you register on the forums, content updates and patch files will only be accessible there.<BR/>"
                            + "<P>If you would like to take our tour, please click the \"Continue\" button.<BR/>"
                            + "<P>If you choose not to take the tour, you may click the \"Close\" button at any time during the tour.<P/>";

                        break;
                    }
                case 1:
                    {
                        from.Location = loc1;
						from.Map = Map.Trammel;
                        sMessage = @"<BASEFONT COLOR=#FFFFFF><CENTER><U>New Haven</U></CENTER><BASEFONT COLOR=#FFFF00>"
                            + "<P>Welcome to New Haven. You will find this town packed with quest givers to help level your skills before you venture too far out into the world. Make sure to take a trip to the Donation Chest and get some starting gear (just look for the bright GREEN chest). You may wish to train a few basic skills now, or go right into the New Player Dungeon. In either case, don't forget to use your Skill Ball(s) and Stat Ball!<P/>";

                        break;
                    }
                case 2:
                    {
                        from.Location = loc2;
						from.Map = Map.Malas;
                        sMessage = @"<BASEFONT COLOR=#FFFFFF><CENTER><U>GM Staff House</U></CENTER><BASEFONT COLOR=#FFFF00>"
                            + "<P>Welcome to GM Staff House. Placed near Luna Bank and Luna Moongate, the GM Staff House will be home to specific vendor types that can't be found anywhere else in the world. These vendors stock items such as Alchemist Cauldron, Fairy Jars and other unique items. If you should have a problem, or complaint, feel free to use the Help button on the in-game menu and one of our staff will pop in to assist you immediately, 24 hours a day, 7 days a week. You may also use the Bug Report Stone which is located between the GM Staff House and the Vendor House, just look for the blue stone. NOTE: You will notice that the GM Staff House is a much larger house than normal (36 x 18), we have the ability to merge houses, but house placement is a key factor. Ask a GM to assist you. The fenced off area across from the Staff House is reserved for Staff usage only.<P/>";

                        break;
                    }
                case 3:
                    {
                        from.Location = loc3;
						from.Map = Map.Trammel;
                        sMessage = @"<BASEFONT COLOR=#FFFFFF><CENTER><U>New Player Champ Spawn</U></CENTER><BASEFONT COLOR=#FFFF00>"
                            + "<P>Welcome to one of the New Player Champ Spawns. Located just outside the town of New Haven, you will find Newbie Champ Spawn. Contained within this dungeon is one of two lower level Champ Spawns for beginning players. You'll have to find the second one on your own. Good luck!<P/>";

                        break;
                    }
				case 4:
                    {
                        from.Location = loc4;
						from.Map = Map.Felucca;
                        sMessage = @"<BASEFONT COLOR=#FFFFFF><CENTER><U>Spellcrafters</U></CENTER><BASEFONT COLOR=#FFFF00>"
                            + "<P>Welcome to Spellcrafters Fort. These mages appeared wielding enhancing magic of terrible force. It is rumored that these mages carry their spellbooks on them from time to time, as well as magic jewels and Spellcrafting jewels, which any new Spellcrafter needs. Do you have what it takes to control those forces and master the art of Spellcrafting to enhance weapons and armor? In order to enter the fort, you will need to complete a quest.<P/>";

                        break;
                    }
				case 5:
                    {
                        from.Location = loc5;
						from.Map = Map.Malas;
                        sMessage = @"<BASEFONT COLOR=#FFFFFF><CENTER><U>Luna Casino</U></CENTER><BASEFONT COLOR=#FFFF00>"
                            + "<P>Welcome to the Luna Palms Casino. You won't find a more wretched hive of scum and villainy, you must be cautious. Actually, it's not such a bad place. Located on the outskirts of the fabulous Luna City, is the shining jewel of the gambling industry, Luna Palms Casino. Feel free to use your free casino token to try out some of the games. Keep an eye out for Kenny Rogers though, he's not such a nice guy. A very well-known gambler around Luna, a long, hard streak of bad luck has turned this once jovial gambler into a cut-throat. He has been seen picking pockets of visitors to the casino, you might get lucky and find a stolen casino token.<P/>";

                        break;
                    }
				case 6:
                    {
                        from.Location = loc6;
						from.Map = Map.Malas;
                        sMessage = @"<BASEFONT COLOR=#FFFFFF><CENTER><U>Luna Bank</U></CENTER><BASEFONT COLOR=#FFFF00>"
                            + "<P>Welcome to the Luna Bank. This white barrel in front of you is the Power Scroll Exchange Center. You simply drop your PS into the container and get credits which you can use to buy different PS's. This system was designed to prevent the waste of PS's instead of trying to sell them on a vendor, which you can still do if you like.<P/>";

                        break;
                    }
				case 7:
                    {
                        from.Location = loc7;
						from.Map = Map.Malas;
                        sMessage = @"<BASEFONT COLOR=#FFFFFF><CENTER><U>Luna Stargate</U></CENTER><BASEFONT COLOR=#FFFF00>"
                            + "<P>This is the Luna Stargate. These devices were unearthed by the Luna Mining Guild last year. Not much is known about them, but they are being discovered all over the world. In order to use a stargate, you must first discover it by clicking on the green gem on the side. You may then click it again to access the device. In order to use it for travel, you must 'dial' your destination using the correct code in the interface. For now, the Luna Mining Guild is still documenting all the locations and the dialing codes. Once complete, they will be added to the library and uploaded to the website. Feel free to use the system until then, just keep track of your dialing codes as you discover new stargates.<P/>";

                        break;
                    }
				case 8:
                    {
                        from.Location = loc8;
						from.Map = Map.Trammel;
                        sMessage = @"<BASEFONT COLOR=#FFFFFF><CENTER><U>Farming</U></CENTER><BASEFONT COLOR=#FFFF00>"
                            + "<P>We've added some additional features for farming. Crops were always able to be harvested, but now you can grow your own crops in your own garden in your own house! In order to obtain the seeds though, you'll have to kill the Animated Seeds you see hopping around here. They are kind of tough, so be careful, one of them can easily kill a player. There are over 90 new farming seeds that have been added, in addition to tons of new cooking recipes. You can also brew your own drinks, alcoholic and non-alcoholic, as well as wine. You can purchase a vineyard deed from the brewer and the garden deed from the gardener. If you need beeswax and honey, you can purchase a beehive from the beekeepers, or, if you want others to be able to get beeswax and honey from your hive, you can purchase a beehive house.<P/>";

                        break;
                    }
				case 9:
                    {
                        from.Location = loc9;
						from.Map = Map.Trammel;
                        sMessage = @"<BASEFONT COLOR=#FFFFFF><CENTER><U>Engaging Quest Lines</U></CENTER><BASEFONT COLOR=#FFFF00>"
                            + "<P>This is one of several quest lines that anyone may choose to do on The Expanse. This particular quest line starts with King Arthur. It is the Quest for the Holy Grail. If you are familiar with Monty Python, this quest will will suit you nicely. Almost step by step through the movie from start to finish. You will receive several rewards along the way including; clapping coconuts, a Black Knight statuette, even a special treat from Dennis the Peasant.<P/>";

                        break;
                    }
				case 10:
                    {
                        from.Location = loc10;
						from.Map = Map.Malas;
                        sMessage = @"<BASEFONT COLOR=#FFFFFF><CENTER><U>Star Wars-Jedi Academy</U></CENTER><BASEFONT COLOR=#FFFF00>"
                            + "<P>Welcome to the Jedi Academy. Inside these hallowed walls, a young Padawan may find their path in the Force. The first floor contains a Lightsaber Forge and sample Focusing Crystals with which to build a custom Lightsaber. The second floor houses the sleeping quarters, complete with a bar for refreshments. The third floor has various quest givers as well as the entrance to the Star Wars Peerless dungeon and the entrance to the Council Chambers. The top floor is the Council Chambers themselves. Various Council Members may be found here. This is also where new Padawans come to begin their training as a Jedi.<P/> <P>If you only see black space, you do not have the custom client installed. Please refer to the Connecting Info page on the website.<P/>";

                        break;
                    }
				case 11:
                    {
                        from.Location = loc11;
						from.Map = Map.Malas;
                        sMessage = @"<BASEFONT COLOR=#FFFFFF><CENTER><U>Star Wars-Sith Academy</U></CENTER><BASEFONT COLOR=#FFFF00>"
                            + "<P>Welcome to the Sith Academy. Within these walls, fledgling Force-users discover their true potential by embracing the Dark Side. The main floor of the academy contains the Lightsaber Forge and sample Focusing Crystals that may be used to create a custom Lightsaber. Various quest givers can be found throughout the academy. Young Sith Apprentices must stand before the Emperor before beginning their Sith Training. Various Sith Lords await to guide you along your quest for greatness and power.<P/><P>If you only see black space, you do not have the custom client installed. Please refer to the Connecting Info page on the website.<P/>";

                        break;
                    }
				case 12:
                    {
                        from.Location = loc12;
						from.Map = Map.Trammel;
                        sMessage = @"<BASEFONT COLOR=#FFFFFF><CENTER><U>Indiana Jones-Gomzul</U></CENTER><BASEFONT COLOR=#FFFF00>"
                            + "<P>Welcome to the Ancient Underground Gargoyle city of Gomzul. This city was discovered by Jonesy Ford during the First Era of The Expanse. Jonesy brings several facets of Indiana Jones with him, including several quests taken from the movies and lots of unforgettable characters and locations. Some of the exotic places you will visit while helping Jonesy unearth long forgotten history: An ancient temple containing a Golden Idol, The Temple of Doom and even the final resting place of the Holy Grail.<P/><P>If you only see black space, you do not have the custom client installed. Please refer to the Connecting Info page on the website.<P/>";

                        break;
                    }
				case 13:
                    {
                        from.Location = loc13;
						from.Map = Map.Trammel;
                        sMessage = @"<BASEFONT COLOR=#FFFFFF><CENTER><U>Evil Dead-Gimme Some Sugar Baby!</U></CENTER><BASEFONT COLOR=#FFFF00>"
                            + "<P>During Halloween of the Third Era, Evil Dead was brought to The Expanse. Help Ash defeat the hideous Army of Darkness! Take up the mantle against the forces of evil by finding and returning the Necronomicon to Wiseman John. Explore terrifying locations, until ultimately you are brought face-to-face with yourself in a battle to the death!<P/>";

                        break;
                    }
				case 14:
                    {
                        from.Location = loc14;
						from.Map = Map.Felucca;
                        sMessage = @"<BASEFONT COLOR=#FFFFFF><CENTER><U>Skullball</U></CENTER><BASEFONT COLOR=#FFFF00>"
                            + "<P>Skullball has been around since the First Era and is similar to soccer, but enhanced as only The Expanse can offer. Complete with new additions and options in the game itself, as well as new trophies and awards, Skullball at The Expanse is something everyone should try! Join a friend and compete in 2 vs. 2 matches and tournaments to find out who is the most skilled at Skullball!<P/>";

                        break;
                    }
				case 15:
                    {
                        from.Location = loc15;
						from.Map = Map.Felucca;
                        sMessage = @"<BASEFONT COLOR=#FFFFFF><CENTER><U>Rungrim's Odorous Obstacle Course</U></CENTER><BASEFONT COLOR=#FFFF00>"
                            + "<P>Think you're fleet of foot with nerves of steel? Why don't you put that to the test? Rungrim will be waiting for you at the finish, if you can navigate along balance beams that disappear, quickly teleport from platform to platform and other such challenges. It would be wise to make sure you have everything insured.<P/>";

                        break;
                    }
                case 16:
                    {
                        from.Location = loc16;
						from.Map = Map.Felucca;
                        sMessage = @"<BASEFONT COLOR=#FFFFFF><CENTER><U>-A-Tron</U></CENTER><BASEFONT COLOR=#FFFF00>"
                            + "<P>No, it's not a typo. This...is -A-Tron! Various fun and games are to be had in any -A-Tron event! From Chicken-A-Tron to Pork-A-Tron and everything in between! A PvM event - Player vs. Maggot that is. Fight against wave upon of maggots before facing a fearsome boss at the end - all whilst polymorphed as a ______. Could be a Chicken, or a Pig, or Cow, which is where the name comes from and that's where the fun comes in! <P/>";

                        break;
                    }
                case 17:
                    {
                        from.Location = loc17;
						from.Map = Map.Malas;
                        sMessage = @"<BASEFONT COLOR=#FFFFFF><CENTER><U>Bomberman</U></CENTER><BASEFONT COLOR=#FFFF00>"
                            + "<P>We'd like to thank The Luna Builders Association and the Royal Britannia Vendor and Gaming Company for building the Bomberman gaming arena. Up to 8 players can compete against each other to try and blow up walls to get the other players. Last person standing is the winner. Be careful, you don't want to blow yourself up!<P/>";

                        break;
                    }
                case 18:
                    {
                        from.Location = loc18;
						from.Map = Map.Malas;
                        sMessage = @"<BASEFONT COLOR=#FFFFFF><CENTER><U>Battle Chess Board</U></CENTER><BASEFONT COLOR=#FFFF00>"
                            + "<P>In an effort to help players relax and take some time to enjoy the finer aspects of The Expanse, The Luna Builders Association, combined with the Royal Britannia Vendor and Gaming Company have brought forth two features; Battle Chess and Bomberman. This is the Battle Chess Board. There are a couple of different styles of pieces to choose from and the winner gets a nice scroll with a time and date stamp and who their opponent was. To the victor go the spoils!<P/>";

                        break;
                    }
                case 19:
                    {
                        from.Location = loc19;
						from.Map = Map.Trammel;
                        sMessage = @"<BASEFONT COLOR=#FFFFFF><CENTER><U>New Player Dungeon</U></CENTER><BASEFONT COLOR=#FFFF00>"
                            + "<P>This is the New Player Dungeon. New Players must download the patch from the forums to see the dungeon entrance. Inside the dungeon you will find brand new low level monsters. There are named monsters in here that have a chance to drop a Cursed LRC suit, as well as magical weapons, armor, and jewelry.<BR/>"
							+ "<P>While you're inside the New Player Dungeon, skill gains are increased up to a skill of 80. Once you hit 80 in a skill, that skill will not raise any higher while in the New Player Dungeon. There is a safe location to do non-combat skill training as well; lockpicking, cartography, animal taming and herding. Step over by the light blue stone to the West and say 'I wish to train non-combat skills' and you will be teleported to another place in the dungeon. There are quest givers inside this dungeon as well. Speak with them and complete their quests. If you complete all 5 quests, the 6th quest, given by Alvina, requires you to collect the 5 items from the previous quests, in return you'll get a key that opens the door to Chuckles the Clown. <P/>";

                        break;
                    }
                case 20:
                    {
                        from.Location = loc20;
						from.Map = Map.Trammel;
                        sMessage = @"<BASEFONT COLOR=#FFFFFF><CENTER><U>Vote Stones and Online Store Stones</U></CENTER><BASEFONT COLOR=#FFFF00>"
                            + "<P>Throughout various places in the world, you will see these Vote Stones and the Online Store Stones. If you double click on them, they will open your default browser to the page indicated; either the vote page that has links to various UO Free Shard Lists, or to The Expanse Online Store.<BR/>"
							+ "<P>You are allowed to vote once every 24 hours. So when you double click on the stone, you won't be able to use the vote stones again for 24 hours. The Expanse Online Store is always available for use with the link stones, so feel free to visit it whenever you wish.<P/>";

                        break;
                    }
                case 21:
                    {
                        from.Location = loc21;
						from.Map = Map.Malas;
                        sMessage = @"<BASEFONT COLOR=#FFFFFF><CENTER><U>Rainbow Mount Zoo</U></CENTER><BASEFONT COLOR=#FFFF00>"
                            + "<P>Here is the Rainbow Mount Zoo. There are 16 Rainbow Mounts altogether and players must gather the 5 Rainbow Mount Tokens and convert them into the Rainbow Token to enter the stable. The tokens can be found on various monsters throughout the land. Speak with Terri Irwin in Luna to begin the quest to obtain the 5 Rainbow Mount Tokens, then she will send you to speak with her husband Steve in the Rainbow Mount Zoo.<BR/>"
							+ "<P>Rainbow Mounts are unique pets. They can level like any other pet, their stats are slightly higher than their regular counterparts, but they can also Res players, even if the pet is dead. These are extremely beneficial pets to have around.<P/>"
							+ " <P>We, the staff and players on The Expanse, would like to thank you for joining us. If you should have any questions, feel free to contact Raist directly (via PM through the forums). This concludes the tour and general orientation. Please do not forget to register on the forums so you can stay up-to-date on the latest content and events. (www.UOExpanse.com/forum).<P/>"
							+" Don't forget to use your Skill Ball(s) before you train any skills!<P/>";

                        break;
                    }
            }
        }
    }
}