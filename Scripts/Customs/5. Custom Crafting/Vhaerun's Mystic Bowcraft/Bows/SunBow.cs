using System;
using Server.Network;
using Server.Items;

namespace Server.Items
{
	[FlipableAttribute( 0x27F0, 0x27A5 )]
	public class SunBow : BaseRanged
	{
		public override int EffectID{ get{ return 0x36FE; } }

		public override Type AmmoType{ get{ return typeof( SunArrow ); } }
		public override Item Ammo{ get{ return new SunArrow(); } }

		public override int DefHitSound{ get{ return 0x22C; } }
		public override int DefMissSound{ get{ return 0x238; } }

		public override WeaponAbility PrimaryAbility{ get{ return WeaponAbility.DoubleShot; } }
		public override WeaponAbility SecondaryAbility{ get{ return WeaponAbility.MortalStrike; } }

		public override int AosDexterityReq{ get{ return 65; } }
		public override int AosMinDamage{ get{ return 17; } }
		public override int AosMaxDamage{ get{ return 21; } }
		public override int AosSpeed{ get{ return 30; } }
		public override float MlSpeed{ get{ return 4.25f; } }

		public override int OldStrengthReq{ get{ return 30; } }
		public override int OldMinDamage{ get{ return 8; } }
		public override int OldMaxDamage{ get{ return 43; } }
		public override int OldSpeed{ get{ return 18; } }

		public override int DefMaxRange{ get{ return 9; } }

		public override int InitMinHits{ get{ return 60; } }
		public override int InitMaxHits{ get{ return 90; } }

		public override WeaponAnimation DefAnimation{ get{ return WeaponAnimation.ShootBow; } }

		[Constructable]
		public SunBow() : base( 0x27F0 )
		{
			Name = "Bow of the Sun";
			Weight = 3.0;
			Layer = Layer.TwoHanded;
			Hue = 0x465;
			Attributes.Luck = 22;
			Attributes.BonusStr = 4;
			Attributes.WeaponDamage = 10;
		}

		public override void GetDamageTypes( Mobile wielder, out int phys, out int fire, out int cold, out int pois, out int nrgy, out int chaos, out int direct )
		{
			phys = 25;
			fire = 75;
			nrgy = cold = pois = chaos = direct = 0;
		}

		public SunBow( Serial serial ) : base( serial )
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