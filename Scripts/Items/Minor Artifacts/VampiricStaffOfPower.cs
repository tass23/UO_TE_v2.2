using System;
using Server;

namespace Server.Items
{
	public class VampiricStaffOfPower : Item
	{
		public override void GetProperties( ObjectPropertyList list )
		{
		base.GetProperties( list );
		list.Add( "Controls all elemental forces" );
		}
		//public override int LabelNumber{ get{ return 1070692; } }
		
		/*
		public override WeaponAbility PrimaryAbility{ get{ return WeaponAbility.WhirlwindAttack; } }
		public override WeaponAbility SecondaryAbility{ get{ return WeaponAbility.ParalyzingBlow; } }

		public override int AosStrengthReq{ get{ return 35; } }
		public override int AosMinDamage{ get{ return 13; } }
		public override int AosMaxDamage{ get{ return 16; } }
		public override int AosSpeed{ get{ return 39; } }
		public override float MlSpeed{ get{ return 2.75f; } }

		public override int OldStrengthReq{ get{ return 35; } }
		public override int OldMinDamage{ get{ return 8; } }
		public override int OldMaxDamage{ get{ return 33; } }
		public override int OldSpeed{ get{ return 35; } }

		public override int InitMinHits{ get{ return 255; } }
		public override int InitMaxHits{ get{ return 255; } }
		*/

		[Constructable]
		public VampiricStaffOfPower() : base( 0xDF0 )
		{
			Name = "Vampiric Staff Of Power";
			Hue = 0x25;
			/*
			WeaponAttributes.MageWeapon = 20;
			Attributes.SpellChanneling = 1;
			Attributes.SpellDamage = 10;
			Attributes.CastRecovery = 3;
			Attributes.LowerManaCost = 2;
			*/
		}

		public VampiricStaffOfPower( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}
		
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}
}