//Created with Script Creator By Marak & Rockstar
using System;
using Server.Network;
using Server.Items;
using Server.Targeting;

namespace Server.Items
{
	public class MonsterNeck : LeatherMempo
	{
		public override int ArtifactRarity{ get{ return 100; } }		
		public override int InitMinHits{ get{ return 255; } }
		public override int InitMaxHits{ get{ return 255; } }
		public override int BaseColdResistance{ get{ return 11; } } 
		public override int BaseEnergyResistance{ get{ return 20; } } 
		public override int BasePhysicalResistance{ get{ return 12; } } 
		public override int BasePoisonResistance{ get{ return 9; } } 
		public override int BaseFireResistance{ get{ return 14; } } 
      
		[Constructable]
		public MonsterNeck()
		{
			Weight = 15;
			Name = "Monster Neck";
			Hue = 69;			
			ArmorAttributes.SelfRepair = 3;
			Attributes.DefendChance = 5;
			Attributes.LowerManaCost = 12;
			Attributes.NightSight = 1;
			Attributes.ReflectPhysical = 6;
		}

		public MonsterNeck( Serial serial ) : base( serial )
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