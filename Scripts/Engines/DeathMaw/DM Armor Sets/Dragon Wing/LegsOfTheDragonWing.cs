using System;
using Server;

namespace Server.Items
{
	public class LegsOfTheDragonWing : DragonLegs
	{
		public override SetItem SetID{ get{ return SetItem.DragonWing; } }
		public override int Pieces{ get{ return 5; } }
		
		public override int ArtifactRarity{ get{ return 15; } }

		public override int InitMinHits{ get{ return 255; } }
		public override int InitMaxHits{ get{ return 255; } }

		[Constructable]
		public LegsOfTheDragonWing()
		{
			Name = "Lindwyrm Legs";
			Hue = 1089;

			FireBonus = Utility.RandomMinMax(8,18);
			
			SetAttributes.RegenHits = 5;
			SetAttributes.RegenMana = 5;
		}

		public LegsOfTheDragonWing( Serial serial ) : base( serial )
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