using System;
using Server;

namespace Server.Items
{
	public class ArmorOfTheVampires : BoneChest
	{
		public override int ArtifactRarity{ get{ return 100; } }
		public override int InitMinHits{ get{ return 255; } }
		public override int InitMaxHits{ get{ return 255; } }

		[Constructable]
		public ArmorOfTheVampires()
		{
			Weight = 5;
			Name = "Armor of the Vampires";
			Hue = 1194;

			Attributes.BonusHits = 10;
			Attributes.DefendChance = 15;
			Attributes.ReflectPhysical = 20;
			Attributes.RegenHits = 2;
			StrBonus = 5;
		}

		public ArmorOfTheVampires( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}
		
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
}