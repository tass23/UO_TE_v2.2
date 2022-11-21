using System;
using Server;

namespace Server.Items
{
	public class NoxTunic : LeatherChest
	{
	 	public override int ArtifactRarity{ get{ return 100; } }
	 	public override int InitMinHits{ get{ return 255; } }
	 	public override int InitMaxHits{ get{ return 255; } }

	 	[Constructable]
	 	public NoxTunic()
	 	{
	 	 	Name = "Tunic of the Swamp Queen";
	 	 	Hue = 1272;
	 	 	ArmorAttributes.MageArmor = 1;
	 	 	ArmorAttributes.SelfRepair = 2;
            PoisonBonus = 10;
			Attributes.CastRecovery = 2;
			Attributes.CastSpeed = 1;
			Attributes.RegenMana = 2;
	 	}

	 	public NoxTunic(Serial serial) : base( serial )
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