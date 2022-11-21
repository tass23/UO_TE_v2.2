/* Created by Hammerhand*/

using System;
using Server.Network;
using Server.Items;

namespace Server.Items
{
	public class FlameThrower : BaseRanged
	{
        public override int Hue { get { return 1359; } }
		public override int EffectID{ get{ return 0xF42; } }
		public override Type AmmoType{ get{ return typeof( FlamingArrow ); } }
		public override Item Ammo{ get{ return new FlamingArrow(); } }

		public override WeaponAbility PrimaryAbility{ get{ return WeaponAbility.ArmorIgnore; } }
		public override WeaponAbility SecondaryAbility{ get{ return WeaponAbility.MovingShot; } }

		public override int AosStrengthReq{ get{ return 45; } }
		public override int AosMinDamage{ get{ return 19; } }
		public override int AosMaxDamage{ get{ return 27; } }
		public override int AosSpeed{ get{ return 25; } }
		public override float MlSpeed{ get{ return 4.00f; } }

		public override int OldStrengthReq{ get{ return 45; } }
		public override int OldMinDamage{ get{ return 15; } }
		public override int OldMaxDamage{ get{ return 17; } }
		public override int OldSpeed{ get{ return 25; } }

		public override int DefMaxRange{ get{ return 10; } }

		public override int InitMinHits{ get{ return 31; } }
		public override int InitMaxHits{ get{ return 70; } }

		public override WeaponAnimation DefAnimation{ get{ return WeaponAnimation.ShootBow; } }

		[Constructable]
		public FlameThrower() : base( 0x26C2 )
		{
            Name = "Flame Thrower";
			Weight = 5.0;

            WeaponAttributes.HitFireArea = 20;
            Attributes.AttackChance = 25;
		}

        public FlameThrower(Serial serial)
            : base(serial)
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