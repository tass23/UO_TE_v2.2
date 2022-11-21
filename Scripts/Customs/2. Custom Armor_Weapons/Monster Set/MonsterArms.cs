//Created with Script Creator By Marak & Rockstar
using System;
using Server.Network;
using Server.Items;
using Server.Targeting;

namespace Server.Items
{
	public class MonsterArms : LeatherArms
	{
		public override int ArtifactRarity{ get{ return 100; } }
		public override int InitMinHits{ get{ return 255; } }
		public override int InitMaxHits{ get{ return 255; } }
		public override int BaseColdResistance{ get{ return 12; } } 
		public override int BaseEnergyResistance{ get{ return 20; } } 
		public override int BasePhysicalResistance{ get{ return 12; } } 
		public override int BasePoisonResistance{ get{ return 15; } } 
		public override int BaseFireResistance{ get{ return 15; } } 
		
		[Constructable]
		public MonsterArms()
		{
			Weight = 15;
			Name = "Monster Arms";
			Hue = 69;			
			Attributes.AttackChance = 10;
			Attributes.DefendChance = 5;
			Attributes.NightSight = 1;
			Attributes.ReflectPhysical = 5;
		}

		public MonsterArms( Serial serial ) : base( serial )
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