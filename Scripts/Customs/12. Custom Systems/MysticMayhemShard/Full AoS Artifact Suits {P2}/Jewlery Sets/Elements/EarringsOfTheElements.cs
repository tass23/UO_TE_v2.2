using System;
using Server;

namespace Server.Items
{
	public class EarringsOfTheElements : GoldEarrings
	{
		public override int LabelNumber{ get{ return 1061104; } } // Earrings of the Elements
		public override int ArtifactRarity{ get{ return 11; } }

		[Constructable]
		public EarringsOfTheElements()
		{
			Name = "Earrings of the Elements";
			Hue = 0x4E9;
			Attributes.Luck = 95;
			Resistances.Fire = 14;
			Resistances.Cold = 14;
			Resistances.Poison = 14;
			Resistances.Energy = 14;
		}

		public EarringsOfTheElements( Serial serial ) : base( serial )
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