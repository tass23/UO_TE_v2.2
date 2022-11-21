/* Created by Hammerhand*/

using System;
using Server.Network;
using Server.Items;

namespace Server.Items
{
	public class RadiantFire : BaseSword
	{
        public override int Hue { get { return 1360; } }
        public override WeaponAbility PrimaryAbility { get { return WeaponAbility.Feint; } }
        public override WeaponAbility SecondaryAbility { get { return WeaponAbility.DoubleStrike; } }

        public override int AosStrengthReq { get { return 45; } }
        public override int AosMinDamage { get { return 18; } }
        public override int AosMaxDamage { get { return 24; } }
        public override int AosSpeed { get { return 30; } }
		public override float MlSpeed{ get{ return 2.50f; } }

        public override int OldStrengthReq { get { return 20; } }
        public override int OldMinDamage { get { return 12; } }
        public override int OldMaxDamage { get { return 14; } }
        public override int OldSpeed { get { return 43; } }

        public override int DefHitSound { get { return 0x23B; } }
        public override int DefMissSound { get { return 0x239; } }

        public override int InitMinHits { get { return 190; } }
        public override int InitMaxHits { get { return 240; } }
		
		[Constructable]
        public RadiantFire() : base(0x2D27)
		{
            Name = "Radiant Fire";
            ItemID = 0x2D27;
			Hue = 1360;

			Attributes.WeaponSpeed = Utility.RandomMinMax(2, 15);
			Attributes.WeaponDamage = Utility.RandomMinMax(5, 20);
            WeaponAttributes.UseBestSkill = 1;
            WeaponAttributes.HitLowerDefend = Utility.RandomMinMax(2, 12);

            LootType = LootType.Regular;

		}

        public override void GetDamageTypes(Mobile wielder, out int phys, out int fire, out int cold, out int pois, out int nrgy, out int chaos, out int direct)
        {
            phys = cold = pois = chaos = direct = 0;
            fire = nrgy = 50;
        }

        public RadiantFire(Serial serial)
            : base(serial)
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.WriteEncodedInt( 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadEncodedInt();
		}
	}
}