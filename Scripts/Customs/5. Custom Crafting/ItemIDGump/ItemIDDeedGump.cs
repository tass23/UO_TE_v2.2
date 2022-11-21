using System; 
using Server; 
using Server.Network;
using Server.Prompts;
using Server.Items;
using Server.Guilds;
using Server.Gumps;
using Server.Mobiles; 
using Server.Targeting;

namespace Server.Items
{ 
	public class ItemIDDeeDs : Item
	{
		[Constructable]
		public ItemIDDeeDs() : base( 0x14F0 )
		{
			Weight = 1.0;
                  	Hue = 2842;
			Movable = true;
			Name = "an ItemID Deed";
			LootType = LootType.Blessed;
		}

		public ItemIDDeeDs( Serial serial ) : base( serial )
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

		public override bool DisplayLootType{ get{ return false; } }

		public override void OnDoubleClick( Mobile from ) 
		{
			if ( !IsChildOf( from.Backpack ) ) // Make sure its in their pack
			{
				 from.SendLocalizedMessage( 1042001 ); // That must be in your pack for you to use it.
			}
			else
			{
         from.SendGump( new ItemIDDeeDsGump2( from ) );
		 this.Delete();
			 }
		}	
	}
   public class ItemIDDeeDsGump2 : Gump 
   { 
   		
public ItemIDDeeDsGump2( Mobile owner ) : base( 300,100 )
{
this.Closable=true;
			
			this.Closable=true;
			this.Disposable=true;
			this.Dragable=true;
			this.Resizable=false;
			this.AddPage(0);
			this.AddBackground(-1, 0, 763, 594, 5054);
			this.AddButton(18, 15, 4502, 248, 1, GumpButtonType.Reply, 0);
			this.AddButton(17, 62, 4502, 248, 2, GumpButtonType.Reply, 0);
			this.AddButton(15, 110, 4502, 248, 3, GumpButtonType.Reply, 0);
			this.AddButton(16, 156, 4502, 248, 4, GumpButtonType.Reply, 0);
			this.AddButton(16, 202, 4502, 248, 5, GumpButtonType.Reply, 0);
			this.AddButton(17, 247, 4502, 248, 6, GumpButtonType.Reply, 0);
			this.AddButton(16, 293, 4502, 248, 7, GumpButtonType.Reply, 0);
			this.AddButton(15, 338, 4502, 248, 8, GumpButtonType.Reply, 0);
			this.AddButton(13, 383, 4502, 248, 9, GumpButtonType.Reply, 0);
			this.AddButton(12, 428, 4502, 248, 10, GumpButtonType.Reply, 0);
			this.AddButton(11, 473, 4502, 248, 11, GumpButtonType.Reply, 0);
			this.AddButton(561, 17, 4502, 248, 12, GumpButtonType.Reply, 0);
			this.AddButton(562, 63, 4502, 248, 13, GumpButtonType.Reply, 0);
			this.AddButton(561, 109, 4502, 248, 14, GumpButtonType.Reply, 0);
			this.AddButton(561, 153, 4502, 248, 15, GumpButtonType.Reply, 0);
			this.AddButton(560, 198, 4502, 248, 16, GumpButtonType.Reply, 0);
			this.AddButton(561, 243, 4502, 248, 17, GumpButtonType.Reply, 0);
			this.AddButton(561, 288, 4502, 248, 18, GumpButtonType.Reply, 0);
			this.AddButton(560, 334, 4502, 248, 19, GumpButtonType.Reply, 0);
			this.AddButton(560, 380, 4502, 248, 20, GumpButtonType.Reply, 0);
			this.AddButton(560, 425, 4502, 248, 21, GumpButtonType.Reply, 0);
			this.AddButton(560, 470, 4502, 248, 22, GumpButtonType.Reply, 0);
			this.AddLabel(79, 123, 1259, @"bone helmet");
			this.AddLabel(78, 77, 1259, @"bonelegs");
			this.AddLabel(78, 29, 1259, @"bone armor");
			this.AddLabel(81, 264, 1259, @"wizard's Hat");
			this.AddLabel(79, 212, 1259, @"bone gloves");
			this.AddLabel(80, 170, 1259, @"bone arms");
			this.AddLabel(83, 306, 1259, @"straw hat");
			this.AddLabel(80, 487, 1259, @"jester hat");
			this.AddLabel(80, 439, 1259, @"feathered hat");
			this.AddLabel(82, 396, 1259, @"Deer Mask");
			this.AddLabel(83, 352, 1259, @"wide-brim hat");
			this.AddLabel(625, 256, 1259, @"torch");
			this.AddLabel(626, 301, 1259, @"lanter");
			this.AddLabel(628, 347, 1259, @"skirt");
			this.AddLabel(628, 394, 1259, @"kilt");
			this.AddLabel(630, 442, 1259, @"short pants");
			this.AddLabel(630, 486, 1259, @"tunic");
			this.AddLabel(622, 29, 1259, @"tricorne hat");
			this.AddLabel(622, 74, 1259, @"bonnet");
			this.AddLabel(624, 123, 1259, @"TallStraw hat");
			this.AddLabel(626, 165, 1259, @"half apron");
			this.AddLabel(626, 209, 1259, @"Fancy Shirt");
			this.AddImage(231, 106, 11);
			this.AddImage(230, 101, 12);
			this.AddImage(230, 97, 50415);
			this.AddImage(230, 100, 50418);
			this.AddImage(233, 102, 50422);
			this.AddImage(231, 104, 50553);
			this.AddImage(228, 101, 50930);
			this.AddImage(230, 100, 50650);
			this.AddImage(381, 257, 50562);
			this.AddImage(231, 106, 60518);
			this.AddImage(230, 101, 60554);
			this.AddImage(231, 100, 60555);
			this.AddImage(230, 104, 60556);
			this.AddImage(231, 103, 60557);
			this.AddImage(230, 99, 50500);
			this.AddImage(229, 98, 50501);
			this.AddLabel(316, 349, 1259, @"The Expanse");
			this.AddLabel(307, 20, 1259, @"ItemID Menu");
			this.AddButton(13, 516, 4502, 248, 23, GumpButtonType.Reply, 0);
			this.AddLabel(82, 530, 1259, @"Orc Helm");
			this.AddButton(560, 518, 4502, 248, 24, GumpButtonType.Reply, 0);
			this.AddLabel(629, 532, 1259, @"Close Helm");
			this.AddButton(259, 407, 4502, 248, 25, GumpButtonType.Reply, 0);
			this.AddLabel(325, 420, 1259, @"Cloak");
			this.AddButton(257, 461, 4502, 248, 26, GumpButtonType.Reply, 0);
			this.AddLabel(326, 476, 1259, @"Vulture Helm");


}
public override void OnResponse( NetState state, RelayInfo info )  
      { 
        Mobile from = state.Mobile; 
         switch ( info.ButtonID ) 
         { 
            /*case 10: 
            { 
		   from.AddToBackpack(new BoardDeed());
               from.SendMessage( "" );
			   from.PlaySound( 521 ); 
               break;
            }*/
            case 1:
            { 
		   from.AddToBackpack(new ItemIDDeed1());
               from.SendMessage( "Item Id deed placed your backpack....." );
			   from.PlaySound( 521 ); 

		   break;
            } 
            case 2:
            { 
		   from.AddToBackpack(new ItemIDDeed2());
               from.SendMessage( "Item Id deed placed your backpack....." );
			   from.PlaySound( 521 ); 

		   break;
            }
            case 3:  
            { 
		   from.AddToBackpack(new ItemIDDeed3());
               from.SendMessage( "Item Id deed placed your backpack....." );
			   from.PlaySound( 521 ); 

		   break;
            }
            case 4:  
           { 
		  from.AddToBackpack(new ItemIDDeed4());
              from.SendMessage( "Item Id deed placed your backpack....." );
			  from.PlaySound( 521 ); 

		  break;
            }
            case 5:  
            { 
		   from.AddToBackpack(new ItemIDDeed5());
               from.SendMessage( "Item Id deed placed your backpack....." );
			   from.PlaySound( 521 );

		   break;
             }
            case 6:
            { 
		   from.AddToBackpack(new ItemIDDeed6());
               from.SendMessage( "Item Id deed placed your backpack....." );
			   from.PlaySound( 521 ); 

		   break;
            } 
            case 7:
            { 
		   from.AddToBackpack(new ItemIDDeed7());
               from.SendMessage( "Item Id deed placed your backpack....." );
			   from.PlaySound( 521 ); 

		   break;
            }
            case 8:  
            { 
		   from.AddToBackpack(new ItemIDDeed8());
               from.SendMessage( "Item Id deed placed your backpack....." );
			   from.PlaySound( 521 ); 

		   break;
            }
            case 9:  
           { 
		  from.AddToBackpack(new ItemIDDeed9());
              from.SendMessage( "Item Id deed placed your backpack....." );
			  from.PlaySound( 521 ); 

		  break;
            }
            case 10:  
            { 
		   from.AddToBackpack(new ItemIDDeed10());
               from.SendMessage( "Item Id deed placed your backpack....." );
			   from.PlaySound( 521 ); 

		break;
              }
            case 11:
            { 
		   from.AddToBackpack(new ItemIDDeed11());
               from.SendMessage( "Item Id deed placed your backpack....." );
			   from.PlaySound( 521 ); 

		   break;
            } 
            case 12:
            { 
		   from.AddToBackpack(new ItemIDDeed12());
               from.SendMessage( "Item Id deed placed your backpack....." );
			   from.PlaySound( 521 ); 

		   break;
            }
            case 13:  
            { 
		   from.AddToBackpack(new ItemIDDeed13());
               from.SendMessage( "Item Id deed placed your backpack....." );
			   from.PlaySound( 521 ); 

		   break;
            }
            case 14:  
           { 
		  from.AddToBackpack(new ItemIDDeed14());
              from.SendMessage( "Item Id deed placed your backpack....." );
			  from.PlaySound( 521 ); 

		  break;
            }
            case 15:  
            { 
		   from.AddToBackpack(new ItemIDDeed15());
               from.SendMessage( "Item Id deed placed your backpack....." );
			   from.PlaySound( 521 ); 

		break;
              }
            case 16:
            { 
		   from.AddToBackpack(new ItemIDDeed16());
               from.SendMessage( "Item Id deed placed your backpack....." );
			   from.PlaySound( 521 ); 

		   break;
            } 
            case 17:
            { 
		   from.AddToBackpack(new ItemIDDeed17());
               from.SendMessage( "Item Id deed placed your backpack....." );
			   from.PlaySound( 521 ); 

		   break;
            }
            case 18:  
            { 
		   from.AddToBackpack(new ItemIDDeed18());
               from.SendMessage( "Item Id deed placed your backpack....." );
			   from.PlaySound( 521 ); 

		   break;
            }
            case 19:  
           { 
		  from.AddToBackpack(new ItemIDDeed19());
              from.SendMessage( "Item Id deed placed your backpack....." );
			  from.PlaySound( 521 ); 

		  break;
            }
            case 20:  
            { 
		   from.AddToBackpack(new ItemIDDeed20());
               from.SendMessage( "Item Id deed placed your backpack....." );
			   from.PlaySound( 521 ); 

		break;
              }
            case 21:
            { 
		   from.AddToBackpack(new ItemIDDeed21());
               from.SendMessage( "Item Id deed placed your backpack....." );
			   from.PlaySound( 521 ); 

		   break;
            } 
            case 22:
            { 
		   from.AddToBackpack(new ItemIDDeed22());
               from.SendMessage( "Item Id deed placed your backpack....." );
			   from.PlaySound( 521 ); 

		   break;
            } 
            case 23:
            { 
		   from.AddToBackpack(new ItemIDDeed23());
               from.SendMessage( "Item Id deed placed your backpack....." );
			   from.PlaySound( 521 ); 

		   break;
            } 
            case 24:
            { 
		   from.AddToBackpack(new ItemIDDeed24());
               from.SendMessage( "Item Id deed placed your backpack....." );
			   from.PlaySound( 521 ); 

		   break;
            } 
            case 25:
            { 
		   from.AddToBackpack(new ItemIDDeed25());
               from.SendMessage( "Item Id deed placed your backpack....." );
			   from.PlaySound( 521 ); 

		   break;
            } 
            case 26:
            { 
		   from.AddToBackpack(new ItemIDDeed26());
               from.SendMessage( "Item Id deed placed your backpack....." );
			   from.PlaySound( 521 ); 

		   break;
            } 
         }
      }
   }
}
