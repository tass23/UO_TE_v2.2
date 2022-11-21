using System;
using Server.Network;
using Server.Items;

namespace Server.Items
{
	[FlipableAttribute( 0x27A5, 0x27F0 )]
	public class LunaBow : BaseRanged
	{
		public override int EffectID{ get{ return 0x1FA3; } }

		public override Type AmmoType{ get{ return typeof( MoonArrow ); } }
		public override Item Ammo{ get{ return new MoonArrow(); } }

		public override int DefHitSound{ get{ return 0x525; } }
		public override int DefMissSound{ get{ return 0x512; } }

		public override WeaponAbility PrimaryAbility{ get{ return WeaponAbility.DoubleShot; } }
		public override WeaponAbility SecondaryAbility{ get{ return WeaponAbility.MortalStrike; } }

		public override int AosDexterityReq{ get{ return 55; } }
		public override int AosMinDamage{ get{ return 16; } }
		public override int AosMaxDamage{ get{ return 19; } }
		public override int AosSpeed{ get{ return 27; } }
		public override float MlSpeed{ get{ return 4.25f; } }

		public override int OldStrengthReq{ get{ return 30; } }
		public override int OldMinDamage{ get{ return 8; } }
		public override int OldMaxDamage{ get{ return 43; } }
		public override int OldSpeed{ get{ return 18; } }

		public override int DefMaxRange{ get{ return 9; } }

		public override int InitMinHits{ get{ return 50; } }
		public override int InitMaxHits{ get{ return 90; } }

		public override WeaponAnimation DefAnimation{ get{ return WeaponAnimation.ShootBow; } }

		[Constructable]
		public LunaBow() : base( 0x27A5 )
		{
			Name = "Luna Bow";
			Weight = 3.0;
			Layer = Layer.TwoHanded;
			Hue = 0x26B;
			WeaponAttributes.HitLowerAttack = 10;
			Attributes.SpellChanneling = 1;
			Attributes.WeaponDamage = 10;
		}

		public override void GetDamageTypes( Mobile wielder, out int phys, out int fire, out int cold, out int pois, out int nrgy, out int chaos, out int direct )
		{
			pois = fire = chaos = direct = 0;
			nrgy = phys = 33;
			cold = 34;
		}

		public LunaBow( Serial serial ) : base( serial )
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