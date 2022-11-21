using System;
using System.Globalization;
using Server;
using Server.Items;
using Server.Factions;
using Server.Mobiles;
using Server.Network;
using Server.Engines.Quests;

namespace Server.Items
{
	public class SilverCheck : Item
	{
		private int m_SilverAmount;

		[CommandProperty( AccessLevel.GameMaster )]
		public int SilverAmount
		{
			get{ return m_SilverAmount; }
			set{ m_SilverAmount = value; InvalidateProperties(); }
		}

		public SilverCheck( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version

			writer.Write( (int) m_SilverAmount );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			LootType = LootType.Blessed;

			int version = reader.ReadInt();

			switch ( version )
			{
				case 0:
				{
					m_SilverAmount = reader.ReadInt();
					break;
				}
			}
		}

		[Constructable]
		public SilverCheck( int SilverAmount ) : base( 5360 )
		{
			Weight = 1.0;
			Hue = 961;
			LootType = LootType.Blessed;
			Name = "a Silver Check";
			m_SilverAmount = SilverAmount;
		}

		public override bool DisplayLootType{ get{ return Core.AOS; } }

		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );

			string SilverAmount;

			if ( Core.ML )
				SilverAmount = m_SilverAmount.ToString( "N0", CultureInfo.GetCultureInfo( "en-US" ) );
			else
				SilverAmount = m_SilverAmount.ToString();

			list.Add( 1060738, SilverAmount ); // value: ~1_val~
		}

		public override void OnDoubleClick( Mobile from )
		{
			BankBox box = from.FindBankNoCreate();

			if ( box != null && IsChildOf( box ) )
			{
				Delete();

				int deposited = 0;

				int toAdd = m_SilverAmount;

				Silver silver;

				while ( toAdd > 60000 )
				{
					silver = new Silver( 60000 );

					if ( box.TryDropItem( from, silver, false ) )
					{
						toAdd -= 60000;
						deposited += 60000;
					}
					else
					{
						silver.Delete();

						from.AddToBackpack( new SilverCheck( toAdd ) );
						toAdd = 0;

						break;
					}
				}

				if ( toAdd > 0 )
				{
					silver = new Silver( toAdd );

					if ( box.TryDropItem( from, silver, false ) )
					{
						deposited += toAdd;
					}
					else
					{
						silver.Delete();

						from.AddToBackpack( new SilverCheck( toAdd ) );
					}
				}

				// Gold was deposited in your account:
				from.SendMessage( "Silver was deposited into your account: ", true, " " + deposited.ToString() );

				PlayerMobile pm = from as PlayerMobile;

			}
			else
			{
				from.SendLocalizedMessage( 1047026 ); // That must be in your bank box to use it.
			}
		}
	}
}