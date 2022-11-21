using System;
using Server.Network;
using Server.Items;

namespace Server.Items
{
	[FlipableAttribute( 0x1403, 0x1402 )]
	public class SilverStake : BaseSpear
	{
		public override WeaponAbility PrimaryAbility{ get{ return WeaponAbility.ShadowStrike; } }
		public override WeaponAbility SecondaryAbility{ get{ return WeaponAbility.MortalStrike; } }

		public override int AosStrengthReq{ get{ return 35; } }
		public override int AosMinDamage{ get{ return 48; } }
		public override int AosMaxDamage{ get{ return 82; } }
		public override int AosSpeed{ get{ return 44; } }
		
		#region Mondain's Legacy
		public override float MlSpeed{ get{ return 1.75f; } }
		#endregion

		public override int OldStrengthReq{ get{ return 35; } }
		public override int OldMinDamage{ get{ return 44; } }
		public override int OldMaxDamage{ get{ return 80; } }
		public override int OldSpeed{ get{ return 44; } }

		public override int InitMinHits{ get{ return 1; } }
		public override int InitMaxHits{ get{ return 1; } }
		
		public override WeaponAnimation DefAnimation{ get{ return WeaponAnimation.Pierce1H; } }

		[Constructable]
		public SilverStake() : base( 0x1403 )
		{
			Weight = 5.0;
			Hue = 2036;
			Name = "a silver stake";
			LootType = LootType.Blessed;
			Slayer = SlayerName.Silver;
			WeaponAttributes.UseBestSkill = 1;
			Attributes.WeaponDamage = 100;
			AosElementDamages.Fire = 100;
			this.HitPoints = this.MaxHitPoints = 1;
		}
		
		public SilverStake( Serial serial ) : base( serial )
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