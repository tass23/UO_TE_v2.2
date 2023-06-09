using System;
using Server;

namespace Server.Items
{
	public class ArcaneTunic : LeatherChest
	{
		public override int LabelNumber{ get{ return 1061101; } } // Arcane Tunic
		public override SetItem SetID{ get{ return SetItem.Arcane; } }
		public override int Pieces{ get{ return 6; } }
		public override int ArtifactRarity{ get{ return 11; } }

		public override int InitMinHits{ get{ return 255; } }
		public override int InitMaxHits{ get{ return 255; } }

		[Constructable]
		public ArcaneTunic()
		{
			Name = "Arcane Tunic";
			Hue = 0x556;
			
			SetAttributes.NightSight = 1;
			SetAttributes.SpellChanneling = 1;
			SetAttributes.DefendChance = 14;
			SetAttributes.CastSpeed = 1;
		}

		public ArcaneTunic( Serial serial ) : base( serial )
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

			if ( Attributes.NightSight == 0 )
				Attributes.NightSight = 1;
		}
	}
}