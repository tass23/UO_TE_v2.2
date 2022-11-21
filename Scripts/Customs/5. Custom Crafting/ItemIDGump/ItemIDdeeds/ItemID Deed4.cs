using System;
using Server.Network;
using Server.Prompts;
using Server.Items;
using Server.Targeting;
using Server;

namespace Server.Items
{
	public class ItemIDTarget4 : Target
	{
		private ItemIDDeed4 m_Deed;

		public ItemIDTarget4( ItemIDDeed4 deed ) : base( 1, false, TargetFlags.None )
		{
			m_Deed = deed;
		}

		protected override void OnTarget( Mobile from, object target )
		{
			if ( target is Item )
			{
				Item item = (Item)target;

				if ( (item).ItemID == 0x144E )
				{
					from.SendMessage( "");
				}
				else
				{
					if( item.RootParent != from ) // Make sure its in their pack or they are wearing it
					{
						from.SendMessage( "" );
					}
					else
					{
						(item).ItemID = 1;
						from.SendMessage( "" );

						m_Deed.Delete(); // Delete the deed
					}
				}
			}
			if ( target is Item )
			{
				Item item = (Item)target;

				if ( (item).ItemID == 0x144E )
				{
					from.SendMessage( "");
				}
				else
				{
					if( item.RootParent != from )
					{
						from.SendMessage( "" );
					}
					else
					{
						(item).ItemID = 0x144E;
						from.SendMessage( "" );

						m_Deed.Delete();
					}
				}
			}
			else
			{
				from.SendMessage( "Successfully changed" );
			}
		}
	}

	public class ItemIDDeed4 : Item
	{
		[Constructable]
		public ItemIDDeed4() : base( 0x14F0 )
		{
			Weight = 1.0;
			Name = "an ItemID Deed";
			LootType = LootType.Blessed;
			Hue = 1161;
		}

		public ItemIDDeed4( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			LootType = LootType.Blessed;

			int version = reader.ReadInt();
		}

		public override bool DisplayLootType{ get{ return false; } }

		public override void OnDoubleClick( Mobile from )
		{
			if ( !IsChildOf( from.Backpack ) ) // Make sure its in their pack
			{
				 from.SendLocalizedMessage( 1042001 );
			}
			else
			{
				from.SendMessage("Choose the item you want to change the Item ID of!!!"  );
				from.Target = new ItemIDTarget4( this );
			 }
		}	
	}
}