using System;
using Server;

namespace Server.Items
{
	public class NoxRobe : Robe
	{
	 	public override int ArtifactRarity{ get{ return 100; } }

	 	[Constructable]
	 	public NoxRobe()
	 	{
	 	 	Name = "Robe of the Swamp Queen";
	 	 	Hue = 1272;
			Attributes.BonusInt = 10;
			Attributes.CastRecovery = 1;
			Attributes.CastSpeed = 1;
			Attributes.DefendChance = 15;
	 	}

	 	public NoxRobe(Serial serial) : base( serial )
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