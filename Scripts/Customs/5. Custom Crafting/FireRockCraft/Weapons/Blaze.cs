/* Created by Hammerhand*/

using System;
using Server.Network;
using Server.Items;

namespace Server.Items
{
	public class Blaze : BaseSword
	{
		public override WeaponAbility PrimaryAbility{ get{ return WeaponAbility.ArmorIgnore; } }
		public override WeaponAbility SecondaryAbility{ get{ return WeaponAbility.ConcussionBlow; } }

        public override int Hue { get { return 1359; } }
		public override int AosStrengthReq{ get{ return 35; } }
		public override int AosMinDamage{ get{ return 15; } }
		public override int AosMaxDamage{ get{ return 16; } }
		public override int AosSpeed{ get{ return 30; } }
		public override float MlSpeed{ get{ return 3.50f; } }

		public override int OldStrengthReq{ get{ return 25; } }
		public override int OldMinDamage{ get{ return 5; } }
		public override int OldMaxDamage{ get{ return 33; } }
		public override int OldSpeed{ get{ return 35; } }

		public override int DefHitSound{ get{ return 0x237; } }
		public override int DefMissSound{ get{ return 0x23A; } }

		public override int InitMinHits{ get{ return 110; } }
		public override int InitMaxHits{ get{ return 190; } }

		[Constructable]
		public Blaze() : base( 0xF61 )
		{
            Name = "Blaze";
            Hue = 1359;
			Weight = 7.0;

            Attributes.BonusStr = 2;
            Attributes.WeaponSpeed = 5;
            Attributes.SpellChanneling = 1;
            Attributes.Luck = 100;
            Attributes.ReflectPhysical = 13;

            WeaponAttributes.HitFireArea = 35;

            LootType = LootType.Regular;
		}

        public Blaze(Serial serial)
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