using System;
using Server;

namespace Server.Items
{
	public class NoxHelm : PlateHelm
	{
	 	public override int ArtifactRarity{ get{ return 100; } }
	 	public override int InitMinHits{ get{ return 255; } }
	 	public override int InitMaxHits{ get{ return 255; } }

	 	[Constructable]
	 	public NoxHelm()
	 	{
	 	 	Name = "Helm of the Forsaken";
	 	 	Hue = 1272;
	 	 	ArmorAttributes.MageArmor = 1;
	 	 	ArmorAttributes.SelfRepair = 2;
            PoisonBonus = 10;
			Attributes.DefendChance = 5;
			Attributes.SpellDamage = 10;
	 	}

	 	public NoxHelm(Serial serial) : base( serial )
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