/* Created by Hammerhand*/

using System;
using Server.Network;
using Server.Items;

namespace Server.Items
{
	public class LuminousFlames : BaseSword
	{
        public override int Hue { get { return 1359; } }
        public override WeaponAbility PrimaryAbility { get { return WeaponAbility.DefenseMastery; } }
        public override WeaponAbility SecondaryAbility { get { return WeaponAbility.FrenziedWhirlwind; } }

        public override int AosStrengthReq { get { return 30; } }
        public override int AosMinDamage { get { return 15; } }
        public override int AosMaxDamage { get { return 17; } }
        public override int AosSpeed { get { return 35; } }
		public override float MlSpeed{ get{ return 3.00f; } }

        public override int OldStrengthReq { get { return 30; } }
        public override int OldMinDamage { get { return 15; } }
        public override int OldMaxDamage { get { return 17; } }
        public override int OldSpeed { get { return 35; } }

        public override int DefHitSound { get { return 0x23B; } }
        public override int DefMissSound { get { return 0x239; } }

        public override int InitMinHits { get { return 200; } }
        public override int InitMaxHits { get { return 230; } }

        public override SkillName DefSkill { get { return SkillName.Fencing; } }
        public override WeaponType DefType { get { return WeaponType.Piercing; } }
        public override WeaponAnimation DefAnimation { get { return WeaponAnimation.Pierce1H; } }

		[Constructable]
        public LuminousFlames()
            : base(0x2D32)          
		{
            Name = "Luminous Flames";
            ItemID = 0x2D32;
            Hue = 1359;

			Attributes.NightSight = 1;
			Attributes.WeaponSpeed = 25;
			Attributes.WeaponDamage = Utility.RandomMinMax(10, 20);
            WeaponAttributes.HitLeechStam = Utility.RandomMinMax(2, 10);

            Weight = 7.0;
            Layer = Layer.TwoHanded;
            LootType = LootType.Regular;

		}

        public override void GetDamageTypes(Mobile wielder, out int phys, out int fire, out int cold, out int pois, out int nrgy, out int chaos, out int direct)
        {
            fire = nrgy = cold = chaos = direct = 0;
            phys = 55;
            pois = 45;
        }

        public LuminousFlames(Serial serial)
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