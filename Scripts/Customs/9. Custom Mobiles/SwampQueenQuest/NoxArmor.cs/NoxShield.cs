using System;
using Server;

namespace Server.Items
{
	public class NoxShield : WoodenKiteShield
	{ 
		public override int ArtifactRarity{ get{ return 100; } }
		public override int InitMinHits{ get{ return 255; } }
		public override int InitMaxHits{ get{ return 255; } }

		[Constructable]
		public NoxShield()
		{
			ItemID = 0x1B78;
			Name = "Nox Shield";
			Hue = 1272;
			Attributes.NightSight = 1;
			Attributes.SpellChanneling = 1;
			Attributes.DefendChance = 25;
			Attributes.CastSpeed = 2;
		}

		public NoxShield( Serial serial ) : base( serial )
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