using System;
using Server.Network;
using Server.Items;
using Server.Targeting;

namespace Server.Items
{
	public class AngelsNeck : PlateGorget
	{
		public override int ArtifactRarity{ get{ return 100; } }
		public override int InitMinHits{ get{ return 255; } }
		public override int InitMaxHits{ get{ return 255; } }
		public override int BaseColdResistance{ get{ return 11; } } 
		public override int BaseEnergyResistance{ get{ return 16; } } 
		public override int BasePhysicalResistance{ get{ return 10; } } 
		public override int BasePoisonResistance{ get{ return 21; } } 
		public override int BaseFireResistance{ get{ return 12; } } 
      
		[Constructable]
		public AngelsNeck()
		{
			Weight = 10;
			Name = "Angel's Neck";
			Hue = 1153;
			ArmorAttributes.MageArmor = -1;
			ArmorAttributes.SelfRepair = 2;
			Attributes.AttackChance = 5;
			Attributes.BonusDex = 2;
			Attributes.DefendChance = 5;
			LootType = LootType.Blessed;
		}

		public AngelsNeck( Serial serial ) : base( serial )
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