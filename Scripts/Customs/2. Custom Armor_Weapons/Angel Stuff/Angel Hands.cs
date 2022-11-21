using System;
using Server.Network;
using Server.Items;
using Server.Targeting;

namespace Server.Items
{
	public class AngelHands : PlateGloves
	{
		public override int ArtifactRarity{ get{ return 100; } }
		public override int InitMinHits{ get{ return 255; } }
		public override int InitMaxHits{ get{ return 255; } }
		public override int BaseColdResistance{ get{ return 12; } } 
		public override int BaseEnergyResistance{ get{ return 15; } } 
		public override int BasePhysicalResistance{ get{ return 20; } } 
		public override int BasePoisonResistance{ get{ return 19; } } 
		public override int BaseFireResistance{ get{ return 26; } } 
      
		[Constructable]
		public AngelHands()
		{
			Weight = 10;
			Name = "Angel's Hands";
			Hue = 1153;
			ArmorAttributes.MageArmor = -1;
			ArmorAttributes.SelfRepair = 1;
			Attributes.BonusHits = 10;
			Attributes.DefendChance = 5;
			Attributes.RegenHits = 2;
			LootType = LootType.Blessed;
		}

		public AngelHands( Serial serial ) : base( serial )
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