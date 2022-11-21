using System;
using Server.Network;
using Server.Items;

namespace Server.Items
{
	[FlipableAttribute( 0x26CC, 0x26C2 )]
	public class BowStars : BaseRanged
	{
		public override int EffectID{ get{ return 0x379E; } }

		public override Type AmmoType{ get{ return typeof( StarArrow ); } }
		public override Item Ammo{ get{ return new StarArrow(); } }

		public override int DefHitSound{ get{ return 0x1F0; } }
		public override int DefMissSound{ get{ return 0x51B; } }

		public override WeaponAbility PrimaryAbility{ get{ return WeaponAbility.DoubleShot; } }
		public override WeaponAbility SecondaryAbility{ get{ return WeaponAbility.MortalStrike; } }

		public override int AosDexterityReq{ get{ return 75; } }
		public override int AosMinDamage{ get{ return 18; } }
		public override int AosMaxDamage{ get{ return 23; } }
		public override int AosSpeed{ get{ return 32; } }
		public override float MlSpeed{ get{ return 4.25f; } }

		public override int OldStrengthReq{ get{ return 30; } }
		public override int OldMinDamage{ get{ return 8; } }
		public override int OldMaxDamage{ get{ return 43; } }
		public override int OldSpeed{ get{ return 18; } }

		public override int DefMaxRange{ get{ return 9; } }

		public override int InitMinHits{ get{ return 60; } }
		public override int InitMaxHits{ get{ return 95; } }

		public override WeaponAnimation DefAnimation{ get{ return WeaponAnimation.ShootBow; } }

		[Constructable]
		public BowStars() : base( 0x26CC )
		{
			Name = "Bow of the Stars";
			Weight = 3.0;
			Layer = Layer.TwoHanded;
			Hue = 0x498;
			WeaponAttributes.SelfRepair = 2;
			WeaponAttributes.HitHarm = 10;
			Attributes.BonusInt = 2;
			Attributes.WeaponDamage = 15;
		}

		public override void GetDamageTypes( Mobile wielder, out int phys, out int fire, out int cold, out int pois, out int nrgy, out int chaos, out int direct )
		{
			nrgy = 35;
			cold = 65;
			fire = phys = pois = chaos = direct = 0;
		}

		public BowStars( Serial serial ) : base( serial )
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