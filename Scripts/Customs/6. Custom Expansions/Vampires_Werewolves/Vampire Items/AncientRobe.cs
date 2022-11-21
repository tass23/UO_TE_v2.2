using System;
using Server.Network;
using Server.Items;

namespace Server.Items
{
	public class VampireAncientRobe : BaseOuterTorso
	{
		public override int ArtifactRarity{ get{ return 100; } }
		
		public override int InitMinHits{ get{ return 100; } }
		public override int InitMaxHits{ get{ return 100; } }
		
		[Constructable]
		public VampireAncientRobe() : base( 0x2683 )
		{
			Weight = 2.0;
			Hue = 37;
			Name = "an Ancient Vampire Robe";
			Attributes.BonusHits = 25;
			Attributes.BonusMana = 25;
			Attributes.BonusStam = 25;
			Attributes.BonusStr = 5;
			Attributes.BonusDex = 5;
			Attributes.BonusInt = 5;
			
			ItemID = 0x2683;
			if (Utility.RandomDouble() < 0.05) { Movable = true; } else { Movable = false; }
		}

		public VampireAncientRobe( Serial serial ) : base( serial )
		{
		}

		public override bool Dye( Mobile from, DyeTub sender )
		{
			if ( Deleted )
				return false;
			else if ( RootParent is Mobile && from != RootParent )
				return false;

			Hue = 37;
			from.SendMessage( "The dye will not color this fabric." );
			return true;
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.WriteEncodedInt( 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadEncodedInt();
		}
	}
}