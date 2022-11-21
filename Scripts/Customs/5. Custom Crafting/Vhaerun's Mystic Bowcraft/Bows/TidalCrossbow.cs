using System;
using Server.Network;
using Server.Items;

namespace Server.Items
{
	[FlipableAttribute( 0x13FD, 0x14FC )]
	public class TidalCrossbow : BaseRanged
	{
		public override int EffectID{ get{ return 0x1FA3; } }

		public override Type AmmoType{ get{ return typeof( WaterBolt ); } }
		public override Item Ammo{ get{ return new WaterBolt(); } }

		public override int DefHitSound{ get{ return 0x26; } }
		public override int DefMissSound{ get{ return 0x5BD; } }

		public override WeaponAbility PrimaryAbility{ get{ return WeaponAbility.DoubleShot; } }
		public override WeaponAbility SecondaryAbility{ get{ return WeaponAbility.MortalStrike; } }

		public override int AosStrengthReq{ get{ return 55; } }
		public override int AosMinDamage{ get{ return 18; } }
		public override int AosMaxDamage{ get{ return 22; } }
		public override int AosSpeed{ get{ return 28; } }

		public override int OldStrengthReq{ get{ return 30; } }
		public override int OldMinDamage{ get{ return 8; } }
		public override int OldMaxDamage{ get{ return 43; } }
		public override int OldSpeed{ get{ return 18; } }

		public override int DefMaxRange{ get{ return 9; } }

		public override int InitMinHits{ get{ return 50; } }
		public override int InitMaxHits{ get{ return 75; } }

		public override WeaponAnimation DefAnimation{ get{ return WeaponAnimation.ShootBow; } }

		[Constructable]
		public TidalCrossbow() : base( 0x13FD )
		{
			Name = "Tidal Crossbow";
			Weight = 3.0;
			Layer = Layer.TwoHanded;
			Hue = 0x18E;
			Attributes.WeaponDamage = 10;
			WeaponAttributes.HitMagicArrow = 15;
		}

		public override void GetDamageTypes( Mobile wielder, out int phys, out int fire, out int cold, out int pois, out int nrgy, out int chaos, out int direct )
		{
			fire = nrgy = pois = chaos = direct = 0;
			phys = cold = 50;
		}

		public TidalCrossbow( Serial serial ) : base( serial )
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