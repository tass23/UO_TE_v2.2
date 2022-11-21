using System;
using Server.Items;
using Server.Network;
using Server.Engines.Harvest;

namespace Server.Items
{
	[FlipableAttribute( 0xE86, 0xE85 )]
	//public class RewardPickaxe : BaseRewardAxe//, IUsesRemaining
	public class RewardPickaxe : BaseAxe, IUsesRemaining
	{
		[CommandProperty(AccessLevel.GameMaster)]
        public bool IsInfinite
        {
            get { return UsesRemaining > 9999; }
            set
            {
                UsesRemaining = value ? int.MaxValue : 50;
                ShowUsesRemaining = false;
            }
        }

		public override HarvestSystem HarvestSystem{ get{ return Mining.System; } }

		public override WeaponAbility PrimaryAbility{ get{ return WeaponAbility.DoubleStrike; } }
		public override WeaponAbility SecondaryAbility{ get{ return WeaponAbility.Disarm; } }

		public override int AosStrengthReq{ get{ return 50; } }
		public override int AosMinDamage{ get{ return 13; } }
		public override int AosMaxDamage{ get{ return 15; } }
		public override int AosSpeed{ get{ return 35; } }
		public override float MlSpeed{ get{ return 3.00f; } }

		public override int OldStrengthReq{ get{ return 25; } }
		public override int OldMinDamage{ get{ return 1; } }
		public override int OldMaxDamage{ get{ return 15; } }
		public override int OldSpeed{ get{ return 35; } }

		public override int InitMinHits{ get{ return 255; } }
		public override int InitMaxHits{ get{ return 255; } }

		public override WeaponAnimation DefAnimation{ get{ return WeaponAnimation.Slash1H; } }

		public override bool CanBeWornByGargoyles{ get{ return true; } }

		[Constructable]
		public RewardPickaxe() : base( 0xE86 )
		{
			WeaponAttributes.HitPoisonArea = 75;
			WeaponAttributes.HitHarm = 33;
			WeaponAttributes.HitLowerAttack = 40;
			Attributes.WeaponDamage = 40;
			WeaponAttributes.ResistColdBonus = 10;
			Weight = 11.0;
			//UsesRemaining = 50;
			//ShowUsesRemaining = false;
			Name = "Everlasting Pickaxe";
			Hue = 1099;
			IsInfinite = true;
		}

		public RewardPickaxe( Serial serial ) : base( serial )
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
			//ShowUsesRemaining = true;
		}
	}
}