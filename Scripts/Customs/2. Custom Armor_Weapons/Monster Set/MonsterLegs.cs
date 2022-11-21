//Created with Script Creator By Marak & Rockstar
using System;
using Server.Network;
using Server.Items;
using Server.Targeting;

namespace Server.Items
{
	public class MonsterLegs : LeatherSuneate
	{
		public override int ArtifactRarity{ get{ return 100; } }		
		public override int InitMinHits{ get{ return 255; } }
		public override int InitMaxHits{ get{ return 255; } }
		public override int BaseColdResistance{ get{ return 14; } } 
		public override int BaseEnergyResistance{ get{ return 20; } } 
		public override int BasePhysicalResistance{ get{ return 12; } } 
		public override int BasePoisonResistance{ get{ return 15; } } 
		public override int BaseFireResistance{ get{ return 13; } } 
      
		[Constructable]
		public MonsterLegs()
		{
			Weight = 15;
			Name = "Monster Legs";
			Hue = 69;			
			ArmorAttributes.MageArmor = 1;
			Attributes.BonusDex = 5;
			Attributes.DefendChance = 10;
			Attributes.ReflectPhysical = 5;
			Attributes.RegenStam = 5;
		}

		public MonsterLegs( Serial serial ) : base( serial )
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