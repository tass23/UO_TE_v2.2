using System;
using Server.Items;
using Reward = Server.Engines.Quests.BaseReward;

namespace Server.Items
{
	public class Schematic : Item
	{
		private static int[] m_IDs = new int[] 
		{
			0x0FED, 0x0FE7
		};
		
		private static int[] m_Hues = new int[] 
		{
			1086, 1090, 1096, 1165, 1184
		};

		[Constructable]
		public Schematic( ) : base( Utility.RandomList ( m_IDs ) )
		{
			Weight = 1.0;
			Name = "a tattered schematic";
			Hue = Utility.RandomList( m_Hues );
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
					typeof( Schematic )
				},
				new int[]
				{
					5
				} );


			switch ( tele )
			{
				case 0:
				{
					from.SendMessage("You need more schematics to be able to create a complete master.");
					break;
				}
				default:
				{
					from.SendMessage("You have combined your schematics into a complete master schematic.");
					Schematic2 r = new Schematic2();	//This is the new item that is created and drop on the ground next to the player.

					r.MoveToWorld( from.Location, from.Map );
					from.PlaySound( 0x241 );

					break;
				}

			}
		}
		
		public Schematic( Serial serial ) : base( serial )
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
	public class Schematic2 : Item
	{
		private static int[] m_Hues = new int[] 
		{
			1086, 1090, 1096, 1165, 1184
		};

		[Constructable]
		public Schematic2( ) : base( 0x0FEC )
		{
			Weight = 2.0;
			Name = "a complete master schematic";
			Hue = Utility.RandomList( m_Hues );
		}
		
		public Schematic2( Serial serial ) : base( serial )
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