///////////////////////////////////////////////
// HueStone Script
// slapped together on 2007-MAY-26 at 04:44a
//
// released to the public, with no claims of support, proper function or warranties of any kind
//
// Changelog:
//
// -- 0.1: released to www.runuo.com forums
//
// code appropriated from TMSTKSBK's UniDyeTub.cs
//
// Running this file against UniDyeTub.cs might be a good exercise for n00b scripters to
// see how I made this.
//
// I can't really take much credit for this, as the code in here is basically lifted from TMSTKSBK's UniDyeTub.cs
// Thanks and all credit go to him for writing this.
//
//
///////////////////////////////////////////////

using Server;
using System;
using Server.Items;
using Server.Multis;
using Server.Targeting;
using Server.Mobiles;

namespace Server.Items
{
	public class HueStoneTarget : Target
	{
		private Item m_Stone;
		private int theHue;

		public HueStoneTarget( HueStone huestone ) : base( 12, false, TargetFlags.None )
		{
			m_Stone = huestone;
			theHue = (int) huestone.Hue;
		}

		protected override void OnTarget( Mobile from, object targeted )
		{
			if ( targeted is Item )
			{
				Item item = (Item) targeted;
				// remove checks from here to remove the ability to dye certain items
				if( ( FurnitureAttribute.Check( item ) || (item is PotionKeg) ) || 
				      item is BaseArmor || item is BaseWeapon || item is IDyable || 
				      item is MonsterStatuette || item is EtherealMount || 
				      item is Spellbook || item is Runebook || item is RecallRune )
				{
					if( !item.IsChildOf(from.Backpack) )
						from.SendMessage("The item must be in your pack.");	
					else
					{
						item.Hue = theHue;
						from.PlaySound( 0x23F );
					}
				}
				else
					from.SendMessage("That item cannot be dyed.");
			}
			else
				from.SendMessage("You cannot dye that.");
		}
	}

	public class HueStone : Item
	{
		[Constructable] 
		// the itemID below for the stone is 3806.  change this to use a different item
		public HueStone() : base ( 3806 )
		{
			Name = "Hue Stone";
			Movable = false;
			Weight = 0.0;
		}

		[Constructable] 
		public HueStone( int newHue ) : base ( 3806 )
		{
			Name = "Hue Stone: " + Convert.ToString(newHue);
			Movable = false;
			Weight = 0.0;
			Hue=newHue;
		}

		public HueStone( Serial serial ) : base( serial ){}
		
		public override void OnDoubleClick( Mobile from )
		{
			DoOut( from );
		}

		public void DoOut ( Mobile from )
		{
			if ( from.InRange( this.GetWorldLocation(), 1 ) )
			{
				from.SendMessage( "Select the item to dye" );
				from.Target = new HueStoneTarget( this );
			}
			else
				from.SendLocalizedMessage( 500446 ); // That is too far away.
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
