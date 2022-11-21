using System;
using Server.Network;
using Server.Items;

namespace Server.Items
{
	[FlipableAttribute( 0x26C3, 0x26CD )]
	public class ShadowCrossbow : BaseRanged
	{
		public override int EffectID{ get{ return 0x1FA3; } }

		public override Type AmmoType{ get{ return typeof( DeathBolt ); } }
		public override Item Ammo{ get{ return new DeathBolt(); } }

		public override int DefHitSound{ get{ return 0x50F; } }
		public override int DefMissSound{ get{ return 0x5BD; } }

		public override WeaponAbility PrimaryAbility{ get{ return WeaponAbility.DoubleShot; } }
		public override WeaponAbility SecondaryAbility{ get{ return WeaponAbility.MortalStrike; } }

		public override int AosStrengthReq{ get{ return 75; } }
		public override int AosMinDamage{ get{ return 11; } }
		public override int AosMaxDamage{ get{ return 14; } }
		public override int AosSpeed{ get{ return 46; } }
		public override float MlSpeed{ get{ return 4.25f; } }

		public override int OldStrengthReq{ get{ return 30; } }
		public override int OldMinDamage{ get{ return 8; } }
		public override int OldMaxDamage{ get{ return 43; } }
		public override int OldSpeed{ get{ return 18; } }

		public override int DefMaxRange{ get{ return 8; } }

		public override int InitMinHits{ get{ return 70; } }
		public override int InitMaxHits{ get{ return 95; } }

		public override WeaponAnimation DefAnimation{ get{ return WeaponAnimation.ShootBow; } }

		[Constructable]
		public ShadowCrossbow() : base( 0x26C3 )
		{
			Name = "Crossbow of Shadow";
			Weight = 3.0;
			Layer = Layer.TwoHanded;
			Hue = 0x3D6;
			Attributes.AttackChance = 10;
			Attributes.ReflectPhysical = 10;
			WeaponAttributes.HitLeechMana = 12;
			WeaponAttributes.HitLeechStam = 12;
		}

		public override void GetDamageTypes( Mobile wielder, out int phys, out int fire, out int cold, out int pois, out int nrgy, out int chaos, out int direct )
		{
			fire = nrgy = pois = phys = cold = 20;
			chaos = direct = 0;
		}

		public ShadowCrossbow( Serial serial ) : base( serial )
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