using System;
using Server.Network;
using Server.Items;

namespace Server.Items
{
	[FlipableAttribute( 0xF50, 0xF4F )]
	public class ScreechingCrossbow : BaseRanged
	{
		public override int EffectID{ get{ return 0x3789; } }

		public override Type AmmoType{ get{ return typeof( AirBolt ); } }
		public override Item Ammo{ get{ return new AirBolt(); } }

		public override int DefHitSound{ get{ return 0x92; } }
		public override int DefMissSound{ get{ return 0x98; } }

		public override WeaponAbility PrimaryAbility{ get{ return WeaponAbility.DoubleShot; } }
		public override WeaponAbility SecondaryAbility{ get{ return WeaponAbility.MortalStrike; } }

		public override int AosStrengthReq{ get{ return 55; } }
		public override int AosMinDamage{ get{ return 18; } }
		public override int AosMaxDamage{ get{ return 22; } }
		public override int AosSpeed{ get{ return 28; } }
		public override float MlSpeed{ get{ return 4.25f; } }

		public override int OldStrengthReq{ get{ return 30; } }
		public override int OldMinDamage{ get{ return 8; } }
		public override int OldMaxDamage{ get{ return 43; } }
		public override int OldSpeed{ get{ return 18; } }

		public override int DefMaxRange{ get{ return 9; } }

		public override int InitMinHits{ get{ return 50; } }
		public override int InitMaxHits{ get{ return 75; } }

		public override WeaponAnimation DefAnimation{ get{ return WeaponAnimation.ShootBow; } }

		[Constructable]
		public ScreechingCrossbow() : base( 0xF50 )
		{
			Name = "Screeching Crossbow";
			Weight = 3.0;
			Layer = Layer.TwoHanded;
			Hue = 0x456;
			Attributes.Luck = 15;
			Attributes.SpellChanneling = 1;
			WeaponAttributes.HitLowerDefend = 10;
		}

		public override void GetDamageTypes( Mobile wielder, out int phys, out int fire, out int cold, out int pois, out int nrgy, out int chaos, out int direct )
		{
			cold = fire = nrgy = pois = chaos = direct = 0;
			phys = 100;
		}

		public ScreechingCrossbow( Serial serial ) : base( serial )
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