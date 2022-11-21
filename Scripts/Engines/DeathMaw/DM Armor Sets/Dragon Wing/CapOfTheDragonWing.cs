using System;
using Server;

namespace Server.Items
{
	public class CapOfTheDragonWing : DragonHelm
	{
		public override SetItem SetID{ get{ return SetItem.DragonWing; } }
		public override int Pieces{ get{ return 5; } }
		
		public override int ArtifactRarity{ get{ return 15; } }

		public override int InitMinHits{ get{ return 255; } }
		public override int InitMaxHits{ get{ return 255; } }

		[Constructable]
		public CapOfTheDragonWing()
		{
			Name = "Lindwyrm Helm";
			Hue = 1089;

			Attributes.SpellChanneling = 1;
			FireBonus = Utility.RandomMinMax (5,8);
			
			SetAttributes.RegenHits = 5;
			SetAttributes.RegenMana = 5;
		}

		public CapOfTheDragonWing( Serial serial ) : base( serial )
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