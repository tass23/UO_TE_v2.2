using System;
using Server.Network;
using Server.Items;
using Server.Targeting;

namespace Server.Items
{
	public class AngelsCrown : StandardPlateKabuto
	{
		public override int ArtifactRarity{ get{ return 100; } }
		public override int InitMinHits{ get{ return 255; } }
		public override int InitMaxHits{ get{ return 255; } }
		public override int BaseColdResistance{ get{ return 10; } } 
		public override int BaseEnergyResistance{ get{ return 12; } } 
		public override int BasePhysicalResistance{ get{ return 16; } } 
		public override int BasePoisonResistance{ get{ return 19; } } 
		public override int BaseFireResistance{ get{ return 20; } } 
      
		[Constructable]
		public AngelsCrown()
		{
			Weight = 60;
			Name = "Angel's Crown";
			Hue = 1153;
			ArmorAttributes.MageArmor = -1;
			ArmorAttributes.SelfRepair = 3;
			Attributes.AttackChance = 10;
			Attributes.NightSight = 1;
			LootType = LootType.Blessed;
		}

		public AngelsCrown( Serial serial ) : base( serial )
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