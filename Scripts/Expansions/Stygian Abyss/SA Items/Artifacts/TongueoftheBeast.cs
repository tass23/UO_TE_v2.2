using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class TongueoftheBeast : WoodenKiteShield//, ITokunoDyable
	{
		public override int LabelNumber{ get{ return 1112405; } } // Tongue of the Beast 
		public override int ArtifactRarity{ get{ return 11; } }

        public override int BasePhysicalResistance { get { return 10; } }
        public override int BaseEnergyResistance { get { return 10; } }


		public override int InitMinHits{ get{ return 150; } }
		public override int InitMaxHits{ get{ return 150; } }

		[Constructable]
		public TongueoftheBeast()
		{
			ItemID = 0x1B78;
			Hue = 0x556;

			Attributes.SpellChanneling = 1;
            Attributes.RegenStam = 3;
			Attributes.RegenMana = 3;
            
		}

		public TongueoftheBeast( Serial serial ) : base( serial )
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