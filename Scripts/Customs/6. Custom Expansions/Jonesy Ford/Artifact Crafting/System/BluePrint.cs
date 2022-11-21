using System;
using Server.Items;
using Reward = Server.Engines.Quests.BaseReward;

namespace Server.Items
{
	public class BluePrint : Item
	{
		private static int[] m_IDs = new int[] 
		{
			0x1768, 0x1766
		};
		
		private static int[] m_Hues = new int[] 
		{
			301, 1321, 1337
		};

		[Constructable]
		public BluePrint( ) : base( Utility.RandomList ( m_IDs ) )
		{
			Weight = 2.0;
			Name = "an old, tattered blueprint";
			Hue = Utility.RandomList( m_Hues );
			Stackable = true;
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( !IsChildOf( from.Backpack ) )
			{
				from.SendLocalizedMessage( 1042001 ); // That must be in your pack for you to use it.
				return;
			}
			
			Container pack = from.Backpack;

			if ( pack == null )
				return;
				
			int tele = pack.ConsumeTotal(
				new Type[]
				{
					typeof( BluePrint )
				},
				new int[]
				{
					3
				} );


			switch ( tele )
			{
				case 0:
				{
					from.SendMessage("You need more blueprints to be able to create a master.");
					break;
				}
				default:
				{
					if ( Utility.RandomDouble() < .35 )
					{
						from.SendMessage("You have combined your blueprints into a master blueprint and been rewarded.");
						ArtySatchel r = new ArtySatchel();	//This is the new item that is created and drop on the ground next to the player.
						r.MoveToWorld( from.Location, from.Map );
						from.PlaySound( 0x241 );
					}
					else
					{
						from.SendMessage("You have combined your blueprints into a master blueprint");
						from.PlaySound( 0x241 );
					}
					break;
				}
			}
		}
		
		
		public BluePrint( Serial serial ) : base( serial )
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
	
	public class BluePrint2 : Item
	{
		private static int[] m_IDs = new int[] 
		{
			0x1764, 0x1762
		};
		
		private static int[] m_Hues = new int[] 
		{
			1719, 1192, 1126
		};

		[Constructable]
		public BluePrint2( ) : base( Utility.RandomList ( m_IDs ) )
		{
			Weight = 2.0;
			Name = "an old, worn master blueprint";
			Hue = Utility.RandomList( m_Hues );
			Stackable = true;
		}
		
		public override void OnDoubleClick( Mobile from )
		{
			if ( !IsChildOf( from.Backpack ) )
			{
				from.SendLocalizedMessage( 1042001 ); // That must be in your pack for you to use it.
				return;
			}
			
			Container pack = from.Backpack;

			if ( pack == null )
				return;
				
			int tele = pack.ConsumeTotal(
				new Type[]
				{
					typeof( BluePrint ),
					typeof( BluePrint2 )
				},
				new int[]
				{
					1,
                    1
				} );


			switch ( tele )
			{
				case 0:
				{
					from.SendMessage("You need more blueprints to complete a master blueprint.");
					break;
				}
				default:
				{
					from.SendMessage("You have created a master blueprint and been awarded a bonus.");
					ArtySatchel2 r = new ArtySatchel2();	//This is the new item that is created and drop on the ground next to the player.

					r.MoveToWorld( from.Location, from.Map );
					from.PlaySound( 0x241 );

					break;
				}

			}
		}
		
		public BluePrint2( Serial serial ) : base( serial )
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