/* Created by Hammerhand*/

using System;
using Server.Network;
using Server.Items;

namespace Server.Items
{
	public class FlowingFire : BaseSword
	{
        public override WeaponAbility PrimaryAbility { get { return WeaponAbility.ArmorIgnore; } }
        public override WeaponAbility SecondaryAbility { get { return WeaponAbility.InfectiousStrike; } }

        public override int Hue { get { return 1359; } }
		public override int AosStrengthReq{ get{ return 30; } }
		public override int AosMinDamage{ get{ return 12; } }
		public override int AosMaxDamage{ get{ return 15; } }
		public override int AosSpeed{ get{ return 45; } }
		public override float MlSpeed{ get{ return 2.00f; } }

		public override int OldStrengthReq{ get{ return 25; } }
		public override int OldMinDamage{ get{ return 5; } }
		public override int OldMaxDamage{ get{ return 33; } }
		public override int OldSpeed{ get{ return 35; } }

        public override int DefHitSound { get { return 0x23C; } }
        public override int DefMissSound { get { return 0x238; } }

		public override int InitMinHits{ get{ return 98; } }
		public override int InitMaxHits{ get{ return 180; } }

        public override SkillName DefSkill { get { return SkillName.Fencing; } }
        public override WeaponType DefType { get { return WeaponType.Piercing; } }
        public override WeaponAnimation DefAnimation { get { return WeaponAnimation.Pierce1H; } }

		[Constructable]
        public FlowingFire() : base(0x1401)
		{
            Name = "Flowing Fire";
            Hue = 1359;
			Weight = 2.0;

            Attributes.BonusDex = Utility.RandomMinMax(3, 5);
            Attributes.SpellChanneling = 1;
            Attributes.Luck = Utility.RandomMinMax(50, 100);
            Attributes.RegenStam = Utility.RandomMinMax(2, 4);
            Attributes.ReflectPhysical = Utility.RandomMinMax(7, 25);

            WeaponAttributes.HitFireArea = Utility.RandomMinMax(5, 25);

            LootType = LootType.Regular;
		}

        public FlowingFire(Serial serial) : base(serial)
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