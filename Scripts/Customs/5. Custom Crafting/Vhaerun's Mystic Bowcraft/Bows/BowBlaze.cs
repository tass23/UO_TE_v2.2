using System;
using Server.Items;

namespace Server.Items
{
	[FlipableAttribute( 0x26C2, 0x26CC )]
	public class BowBlaze : BaseRanged
	{
		public override int EffectID{ get{ return 0x36E4; } }

		public override Type AmmoType{ get{ return typeof( FireArrow ); } }
		public override Item Ammo{ get{ return new FireArrow(); } }

		public override int DefHitSound{ get{ return 0x226; } }
		public override int DefMissSound{ get{ return 0x228; } }

		public override WeaponAbility PrimaryAbility{ get{ return WeaponAbility.ConcussionBlow; } }
		public override WeaponAbility SecondaryAbility{ get{ return WeaponAbility.MortalStrike; } }

		public override int AosDexterityReq{ get{ return 45; } }
		public override int AosMinDamage{ get{ return 15; } }
		public override int AosMaxDamage{ get{ return 17; } }
		public override int AosSpeed{ get{ return 25; } }
		public override float MlSpeed{ get{ return 4.25f; } }

		public override int OldStrengthReq{ get{ return 30; } }
		public override int OldMinDamage{ get{ return 8; } }
		public override int OldMaxDamage{ get{ return 43; } }
		public override int OldSpeed{ get{ return 18; } }

		public override int DefMaxRange{ get{ return 9; } }

		public override int InitMinHits{ get{ return 50; } }
		public override int InitMaxHits{ get{ return 80; } }

		public override WeaponAnimation DefAnimation{ get{ return WeaponAnimation.ShootBow; } }

		[Constructable]
		public BowBlaze() : base( 0x13B2 )
		{
			Name = "Bow of Blaze";
			Weight = 3.0;
			Layer = Layer.TwoHanded;
			Hue = 0x48E;
			WeaponAttributes.HitFireball = 10;
			Attributes.WeaponDamage = 15;
		}

		public override void GetDamageTypes( Mobile wielder, out int phys, out int fire, out int cold, out int pois, out int nrgy, out int chaos, out int direct )
		{
			cold = pois = nrgy = chaos = direct = 0;
			phys = fire = 50;
		}

		public BowBlaze( Serial serial ) : base( serial )
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