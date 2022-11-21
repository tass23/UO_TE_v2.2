/* Created by Hammerhand*/

using System;
using Server;

namespace Server.Items
{
	public class ShieldOfCrystalineFire : BaseShield
	{
        public override int Hue { get { return 1357; } }
		public override int BaseFireResistance{ get{ return 9; } }

		public override int InitMinHits{ get{ return 250; } }
		public override int InitMaxHits{ get{ return 255; } }

		public override int AosStrReq{ get{ return 90; } }

		public override int ArmorBase{ get{ return 23; } }

		[Constructable]
        public ShieldOfCrystalineFire()
            : base(0x1BC4)
		{
            Name = "Shield Of CrystalineFire";
            Hue = 1357;
			Weight = 8.0;

            Attributes.CastRecovery = Utility.RandomMinMax(6, 15);
            Attributes.CastSpeed = Utility.RandomMinMax(4, 7);
            Attributes.SpellDamage = Utility.RandomMinMax(20, 35);
            Attributes.Luck = Utility.RandomMinMax(100, 250);
            Attributes.BonusStr = Utility.RandomMinMax(3, 9);
            Attributes.WeaponSpeed = Utility.RandomMinMax(15, 30);
            Attributes.SpellChanneling = 1;
            Attributes.ReflectPhysical = Utility.RandomMinMax(8, 15);

            PhysicalBonus = Utility.RandomMinMax(3, 9);
            FireBonus = Utility.RandomMinMax(10, 15);

            LootType = LootType.Regular;
		}

        public ShieldOfCrystalineFire(Serial serial)
            : base(serial)
		{
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int)0 );//version
		}
	}
}
