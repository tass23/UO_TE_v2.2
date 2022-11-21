using System;
using Server;

namespace Server.Items
{
	public class NoxLegs : LeatherLegs
	{
	 	public override int ArtifactRarity{ get{ return 100; } }
	 	public override int InitMinHits{ get{ return 255; } }
	 	public override int InitMaxHits{ get{ return 255; } }

	 	[Constructable]
	 	public NoxLegs()
	 	{
	 	 	Name = "Legs of the Swamp Queen";
	 	 	Hue = 1272;
	 	 	ArmorAttributes.MageArmor = 1;
            ArmorAttributes.SelfRepair = 2;
            PoisonBonus = 10;
			Attributes.RegenHits = 2;
			Attributes.RegenMana = 2;
	 	}

	 	public NoxLegs(Serial serial) : base( serial )
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