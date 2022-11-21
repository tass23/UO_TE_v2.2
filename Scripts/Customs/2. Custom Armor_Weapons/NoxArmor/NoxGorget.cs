using System;
using Server;

namespace Server.Items
{
	public class NoxGorget : LeatherGorget
	{
	 	public override int ArtifactRarity{ get{ return 100; } }
	 	public override int InitMinHits{ get{ return 255; } }
	 	public override int InitMaxHits{ get{ return 255; } }

	 	[Constructable]
	 	public NoxGorget()
	 	{
	 	 	Name = "Gorget of the Swamp Queen";
	 	 	Hue = 1272;
	 	 	ArmorAttributes.MageArmor = 1;
	 	 	ArmorAttributes.SelfRepair = 1;
            PoisonBonus = 10;
	 	 	Attributes.RegenHits = 2;
	 	 	Attributes.SpellDamage = 5;
	 	}

	 	public NoxGorget(Serial serial) : base( serial )
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