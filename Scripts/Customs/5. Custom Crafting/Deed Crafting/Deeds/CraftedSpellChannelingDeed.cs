using System;
using Server.Network;
using Server.Prompts;
using Server.Items;
using Server.Targeting;
using Server;

namespace Server.Items
{
	public class CraftedSpellChannelingTarget : Target // Create our targeting class (which we derive from the base target class)
	{
		private CraftedSpellChannelingDeed m_Deed;

		public CraftedSpellChannelingTarget( CraftedSpellChannelingDeed deed ) : base( 1, false, TargetFlags.None )
		{
			m_Deed = deed;
		}

		protected override void OnTarget( Mobile from, object target ) // Override the protected OnTarget() for our feature
		{
			if ( target is BaseWeapon )
			{
				Item item = (Item)target;

				if ( ((BaseWeapon)item).Attributes.SpellChanneling == 1 )
				{
					from.SendMessage( "That already has spell channeling!");
				}
				else
				{
					if( item.RootParent != from ) // Make sure its in their pack or they are wearing it
					{
						from.SendMessage( "You cannot put spell channeling on that there!" ); // You cannot bless that object
					}
					else
					{
						((BaseWeapon)item).Attributes.SpellChanneling = 1;
						from.SendMessage( "You magically add spell channeling to your weapon...." );

						m_Deed.Delete(); // Delete the deed
					}
				}
			}
			else
			{
				from.SendMessage( "You cannot put spell channeling on that" );
			}
		}
	}

	public class CraftedSpellChannelingDeed : Item // Create the item class which is derived from the base item class
	{
		[Constructable]
		public CraftedSpellChannelingDeed() : base( 0x14F0 )
		{
			Weight = 1.0;
			Name = "a Spell Channeling deed";
			LootType = LootType.Blessed;
			Hue = 0x492;
		}

		public CraftedSpellChannelingDeed( Serial serial ) : base( serial )
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
			LootType = LootType.Blessed;

			int version = reader.ReadInt();
		}

		public override bool DisplayLootType{ get{ return false; } }

		public override void OnDoubleClick( Mobile from ) // Override double click of the deed to call our target
		{
			if ( !IsChildOf( from.Backpack ) ) // Make sure its in their pack
			{
				 from.SendLocalizedMessage( 1042001 ); // That must be in your pack for you to use it.
			}
			else
			{
				from.SendMessage("What item would you like to add spell channeling to?"  );
				from.Target = new CraftedSpellChannelingTarget( this ); // Call our target
			 }
		}	
	}
}


