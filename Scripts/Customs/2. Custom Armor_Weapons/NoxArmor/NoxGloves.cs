using System;
using Server;

namespace Server.Items
{
	public class NoxGloves : LeatherGloves
	{
	 	public override int ArtifactRarity{ get{ return 100; } }
	 	public override int InitMinHits{ get{ return 255; } }
	 	public override int InitMaxHits{ get{ return 255; } }

	 	[Constructable]
	 	public NoxGloves()
	 	{
	 	 	Name = "Gloves of the Swamp Queen";
	 	 	Hue = 1272;
	 	 	ArmorAttributes.MageArmor = 1;
	 	 	ArmorAttributes.SelfRepair = 1;
            PoisonBonus = 10;
			Attributes.Luck = 25;
			Attributes.RegenMana = 3;
	 	}

	 	public NoxGloves(Serial serial) : base( serial )
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