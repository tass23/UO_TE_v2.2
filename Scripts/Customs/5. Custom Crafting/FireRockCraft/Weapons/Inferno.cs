/* Created by Hammerhand*/

using System;
using Server.Network;
using Server.Items;

namespace Server.Items
{
	public class Inferno : BaseSword
	{
		public override WeaponAbility PrimaryAbility{ get{ return WeaponAbility.DoubleStrike; } }
		public override WeaponAbility SecondaryAbility{ get{ return WeaponAbility.MortalStrike; } }

        public override int Hue { get { return 1359; } }
		public override int AosStrengthReq{ get{ return 55; } }
		public override int AosMinDamage{ get{ return 11; } }
		public override int AosMaxDamage{ get{ return 14; } }
		public override int AosSpeed{ get{ return 47; } }
		public override float MlSpeed{ get{ return 2.50f; } }

		public override int OldStrengthReq{ get{ return 55; } }
		public override int OldMinDamage{ get{ return 11; } }
		public override int OldMaxDamage{ get{ return 14; } }
		public override int OldSpeed{ get{ return 47; } }

		public override int DefHitSound{ get{ return 0x23B; } }
		public override int DefMissSound{ get{ return 0x23A; } }

		public override int InitMinHits{ get{ return 81; } }
		public override int InitMaxHits{ get{ return 150; } }

		[Constructable]
		public Inferno() : base( 0x26C1 )
		{
            Name = "Inferno";
            Hue = 1359;
			Weight = 1.0;

            Attributes.WeaponSpeed = 25;
            Attributes.SpellChanneling = 1;
            Attributes.Luck = 100;
            WeaponAttributes.HitFireArea = 20;
            WeaponAttributes.HitLeechHits = 15;

            LootType = LootType.Regular;
		}

        public Inferno(Serial serial)
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