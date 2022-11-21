using System;
using Server.Network;
using Server.Items;

namespace Server.Items
{
	public class Boomerang : BaseThrown
	{
		public override int MinThrowRange{ get{ return 2; } }	// MaxRange 6

		public override WeaponAbility PrimaryAbility{ get{ return WeaponAbility.MysticArc; } }
		public override WeaponAbility SecondaryAbility{ get{ return WeaponAbility.ConcussionBlow; } }

		/*
		Boomerang 0x8FF: MysticArc, ConcussionBlow
		Cyclone 2305/0x901: MovingShot, InfusedThrow
		Soul Glaive 2314/0x090A: ArmorIgnore, MortalStrike
		*/

		public override int AosStrengthReq{ get{ return 25; } }
		public override int AosMinDamage{ get{ return 8; } }
		public override int AosMaxDamage{ get{ return 12; } }
		public override int AosSpeed{ get{ return 25; } }
		public override float MlSpeed{ get{ return 2.00f; } }

		public override int OldStrengthReq{ get{ return 20; } }
		public override int OldMinDamage{ get{ return 9; } }
		public override int OldMaxDamage{ get{ return 41; } }
		public override int OldSpeed{ get{ return 20; } }

		public override int InitMinHits{ get{ return 31; } }
		public override int InitMaxHits{ get{ return 60; } }

		public override bool CanBeWornByGargoyles{ get{ return true; } }

		[Constructable]
		public Boomerang() : base( 0x8FF )
		{
			Weight = 6.0;
			Layer = Layer.OneHanded;
		}

		public Boomerang( Serial serial ) : base( serial )
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