/////////////////////////////////////////////////
//                                             //
//             BurialSystem                    //
//               by LIACS                      //
//              02/19/2007                     //
/////////////////////////////////////////////////

using System;
using Server.Items;
using Server.Mobiles;
using Server.Network;
using Server;
using Server.Targeting;

namespace Server.Items
{
	public class GraveShovel : Item
	{
        private bool CanBuryPlayers = false; //shard owners customize here

		[Constructable]
        public GraveShovel()
            : base(0xF39)
		{
			Movable = true;
			Name = "Grave Shovel";
		}

        public override void OnDoubleClick(Mobile from)
        {
            if (!IsChildOf(from.Backpack))
            {
                from.SendLocalizedMessage(1042001); // That must be in your pack for you to use it.
                return;
            }
            else
            {
                from.SendMessage("What do you want to bury?");
                from.Target = new BuryTarget(this);
            }
        }

        private class BuryTarget : Target
        {
            GraveShovel m_Bury;

            public BuryTarget(GraveShovel bury)
                : base(2, false, TargetFlags.None) // public
            {
                m_Bury = bury;
            }
            protected override void OnTarget(Mobile from, object target)
            {
                PlayerMobile pm = (PlayerMobile)from;

                if (target is Corpse)
                {
                    Corpse corpse = target as Corpse;
                    Point3D loc = corpse.Location;
                    Map map = corpse.Map;

                    if (corpse.Owner.Player == true && m_Bury.CanBuryPlayers == false)
                        pm.SendMessage(" You cannot bury player corpses.");

                    else
                    #region map
                   
                    {
                        LandTile lt = map.Tiles.GetLandTile(from.X, from.Y);

                        if (IsDirt(lt.ID))
                        {
                            Grave grave = new Grave();
                            from.SendMessage("You bury the creature. Your deity rewards you with some karma.");
                            from.Karma += Utility.Random(1, 10); // customize what you want to give
                            from.Stam -= 2;

                            if (from.Luck >= 10)
                            {
                                switch (Utility.Random(100))
                                {
                                    case 1:
                                        {
                                            from.AddToBackpack(new Gold(5)); // customize what you want to give
                                            from.PlaySound(0x2E6); // drop gold sound
                                            from.SendMessage("You are lucky and find a coin in the ground.");
                                            break;
                                        }
                                    case 2:
                                        {
                                            from.AddToBackpack(new FertileDirt(Utility.Random(1, 2)));// customize what you want to give
                                            from.SendMessage("This is very fertile ground.You decide to take some of it.");
                                            break;
                                        }
                                    case 3:
                                        {
                                            from.AddToBackpack(new Spoon());// customize what you want to give
                                            from.SendMessage("You found an old spoon in the ground.");
                                            break;
                                        }
                                    default: break;
                                }
                            }

                            grave.MoveToWorld(loc, map);
                            corpse.Delete();

                            ItemRemovalTimer2 thisTimer = new ItemRemovalTimer2(grave);
                            thisTimer.Start();
                        }

                        if (IsSand(lt.ID))
                        {
                            //Grave grave = new Grave();
                            from.SendMessage("You bury the creature in the sand. Your deity rewards you with some karma.");
                            from.Karma += Utility.Random(1, 10); // customize what you want to give
                            from.Stam -= 2;

                            if (from.Luck >= 10)
                            {
                                switch (Utility.Random(100))
                                {
                                    case 1:
                                        {
                                            from.AddToBackpack(new Gold(5)); // customize what you want to give
                                            from.PlaySound(0x2E6); // drop gold sound
                                            from.SendMessage("You are lucky and find a coin in the sand.");
                                            break;
                                        }
                                    case 2:
                                        {
                                            from.AddToBackpack(new Diamond());// customize what you want to give
                                            from.SendMessage("You found a diamond in the sand!");
                                            break;
                                        }
                                    default: break;
                                }
                            }
                            corpse.Delete();
                        }
                    #endregion

                        #region static
                        // is it a static swamp?

                        StaticTile[] tiles = map.Tiles.GetStaticTiles(from.X, from.Y);

                        for (int i = 0; i < tiles.Length; ++i)
                        {
                            StaticTile t = tiles[i];
                            ItemData id = TileData.ItemTable[t.ID & 0x3FFF];

                            int tand = t.ID & 0x3FFF;

                            if (t.Z != from.Z)
                            {
                                continue;
                            }
                            else if (IsStaticDirt(tand))
                            {
                                Grave grave = new Grave();
                                from.SendMessage("You bury the creature. Your deity rewards you with some karma.");
                                from.Karma += Utility.Random(1, 10); // customize what you want to give
                                from.Stam -= 2;

                                if (from.Luck >= 10)
                                {
                                    switch (Utility.Random(100))
                                    {
                                        case 1:
                                            {
                                                from.AddToBackpack(new Gold(5)); // customize what you want to give
                                                from.PlaySound(0x2E6); // drop gold sound
                                                from.SendMessage("You are lucky and find a coin in the ground.");
                                                break;
                                            }
                                        case 2:
                                            {
                                                from.AddToBackpack(new FertileDirt(Utility.Random(1, 2)));// customize what you want to give
                                                from.SendMessage("This is very fertile ground.You decide to take some of it.");
                                                break;
                                            }
                                        case 3:
                                            {
                                                from.AddToBackpack(new Spoon());// customize what you want to give
                                                from.SendMessage("You found an old spoon in the ground.");
                                                break;
                                            }
                                        default: break;
                                    }
                                }

                                grave.MoveToWorld(loc, map);
                                corpse.Delete();

                                ItemRemovalTimer2 thisTimer = new ItemRemovalTimer2(grave);
                                thisTimer.Start();
                            }


                            else if (IsStaticSand(tand))
                            {
                                from.SendMessage("You bury the creature in the sand. Your deity rewards you with some karma.");
                                from.Karma += Utility.Random(1, 10); // customize what you want to give
                                from.Stam -= 2;

                                if (from.Luck >= 10)
                                {
                                    switch (Utility.Random(100))
                                    {
                                        case 1:
                                            {
                                                from.AddToBackpack(new Gold(5)); // customize what you want to give
                                                from.PlaySound(0x2E6); // drop gold sound
                                                from.SendMessage("You are lucky and find a coin in the sand.");
                                                break;
                                            }
                                        case 2:
                                            {
                                                from.AddToBackpack(new Diamond());// customize what you want to give
                                                from.SendMessage("You found a diamond in the sand!");
                                                break;
                                            }
                                        default: break;
                                    }
                                }
                            }
                            else
                                from.SendMessage("You can't bury this here.");
                        }
                    }
#endregion

                 }

                else
                    from.SendMessage("You may only use this to bury corpses.");
                return;
            }
        }

        private static bool IsDirt(int itemID)
        {
            if (itemID >= 3 && itemID <= 21) // dirt
                return true;

            if (itemID >= 113 && itemID <= 167) // dirt
                return true;

            if (itemID >= 172 && itemID <= 285) // // dirt 
                return true;

            if (itemID >= 321 && itemID <= 394) // dirt
                return true;

            if (itemID >= 420 && itemID <= 423) // dirt
                return true;

            if (itemID >= 433 && itemID <= 434) // dirt
                return true;

            if (itemID >= 476 && itemID <= 499)//dirt
                return true;

            if (itemID >= 561 && itemID <= 601) // dirt
                return true;

            if (itemID >= 622 && itemID <= 641) // dirt
                return true;

            if (itemID >= 658 && itemID <= 661) // dirt
                return true;

            if (itemID >= 741 && itemID <= 856) // dirt
                return true;

            if (itemID >= 871 && itemID <= 940) // dirt
                return true;
    
            if (itemID >= 971 && itemID <= 974) // dirt
                return true;

            if (itemID >= 1351 && itemID <= 1446) // dirt
                return true;

            if (itemID >= 1471 && itemID <= 1598) // dirt
                return true;

            if (itemID >= 1643 && itemID <= 1646) // dirt
                return true;
            
            return false;
        }

        private static bool IsSand(int itemID)
        {
                    if (itemID >= 22 && itemID <= 75)  // light sand
                return true;
                    if (itemID >= 1611 && itemID <= 1642) // light sand
                return true;
                    if (itemID >= 1447 && itemID <= 1466) // light sand
                return true;
                if (itemID >= 951 && itemID <= 970) // light sand
                return true;
                    if (itemID >= 857 && itemID <= 860) // light sand
                return true;
                    if (itemID >= 720 && itemID <= 727) // light sand
                return true;
                    if (itemID >= 642 && itemID <= 657) // light sand
                return true;
                    if (itemID >= 424 && itemID <= 427) // light snad
                return true;
            if (itemID >= 441 && itemID <= 465) // light sand
                return true;
        
            if (itemID >= 286 && itemID <= 301) // light sand
                return true;
                    if (itemID == 402 ) // light sand
                return true;

            return false;
        }

        private static bool IsStaticSand(int itemID)
        {
            if (itemID >= 13671 && itemID <= 13682)  // light sand
                return true;



            return false;
        }

        private static bool IsStaticDirt(int itemID)
        {
            if (itemID >= 13683 && itemID <= 13745)  // dirt
                return true;



            return false;
        }


        public class ItemRemovalTimer2 : Timer
        {
            private Item i_item;

            public ItemRemovalTimer2(Item item)
                : base(TimeSpan.FromSeconds(5.0))
            {
                Priority = TimerPriority.OneSecond;
                i_item = item;
            }

            protected override void OnTick()
            {
                if ((i_item != null) && (!i_item.Deleted))
                    i_item.Delete();
            }
        } 
		public GraveShovel( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}

    
}