using System;
using Server.Network;
using Server.Items;

namespace Server.Items
{
	[FlipableAttribute( 0x26CC, 0x26C2 )]
	public class BowDark : BaseRanged
	{
		public override int EffectID{ get{ return 0x3789; } }

		public override Type AmmoType{ get{ return typeof( DarkArrow ); } }
		public override Item Ammo{ get{ return new DarkArrow(); } }

		public override int DefHitSound{ get{ return 0x520; } }
		public override int DefMissSound{ get{ return 0x238; } }

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
		public BowDark() : base( 0x26CC )
		{
			Name = "Bow of the Dark";
			Weight = 3.0;
			Layer = Layer.TwoHanded;
			Hue = 0x3D6;
			WeaponAttributes.HitLeechHits = 12;
			Attributes.NightSight = 1;
			Attributes.RegenHits = 3;
			Attributes.WeaponSpeed = 15;
		}

		public override void GetDamageTypes( Mobile wielder, out int phys, out int fire, out int cold, out int pois, out int nrgy, out int chaos, out int direct )
		{
			pois = 30;
			cold = 30;
			phys = 40;
			fire = nrgy = chaos = direct = 0;
		}

		public BowDark( Serial serial ) : base( serial )
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