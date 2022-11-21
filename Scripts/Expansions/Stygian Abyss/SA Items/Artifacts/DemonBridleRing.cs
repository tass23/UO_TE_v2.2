using System;
using Server;

namespace Server.Items
{
	public class DemonBridleRing : GoldRing
	{
		public override int InitMinHits{ get{ return 255; } }
		public override int InitMaxHits{ get{ return 255; } }

		[Constructable]
		public DemonBridleRing()
		{
		
			Name = ("Demon Bridle Ring");
		
			Hue = 39;	
		
			Attributes.CastRecovery = 2;
			Attributes.CastSpeed = 1;	
			Attributes.RegenHits = 1;
			Attributes.RegenMana = 1;
			Attributes.DefendChance = 10;
			Attributes.LowerManaCost = 4;
			Resistances.Fire = 5;
		}

		public DemonBridleRing( Serial serial ) : base( serial )
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

			if ( Hue == 0x4F4 )
				Hue = 0x4F7;
		}
	}
}