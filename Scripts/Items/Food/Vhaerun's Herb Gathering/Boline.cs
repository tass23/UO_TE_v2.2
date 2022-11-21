using System;
using Server.Network;
using Server.Targeting;
using Server.Items;

namespace Server.Items
{
	public class Boline : BaseBoline
	{
		public override WeaponAbility PrimaryAbility{ get{ return WeaponAbility.InfectiousStrike; } }
		public override WeaponAbility SecondaryAbility{ get{ return WeaponAbility.ShadowStrike; } }

		public override int AosStrengthReq{ get{ return 10; } }
		public override int AosMinDamage{ get{ return 6; } }
		public override int AosMaxDamage{ get{ return 8; } }
		public override int AosSpeed{ get{ return 56; } }
		public override float MlSpeed{ get{ return 1.50f; } }

		public override int InitMinHits{ get{ return 31; } }
		public override int InitMaxHits{ get{ return 40; } }

		public override SkillName DefSkill{ get{ return SkillName.Fencing; } }
		public override WeaponType DefType{ get{ return WeaponType.Piercing; } }
		public override WeaponAnimation DefAnimation{ get{ return WeaponAnimation.Pierce1H; } }

		[Constructable]
		public Boline() : this( 0x1401, 50 )
		{
			Name = "boline";
		}

		[Constructable]
		public Boline(int usesremaining) : this( 0x1401, usesremaining )
		{
			Name = "boline";
		}

		[Constructable]
		public Boline(int itemid, int usesremaining) : base( itemid, usesremaining )
		{
			Name = "boline";
		}

		public Boline( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}
}