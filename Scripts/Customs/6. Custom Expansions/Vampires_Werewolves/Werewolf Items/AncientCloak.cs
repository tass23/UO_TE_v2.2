using System;
using Server.Network;
using Server.Items;

namespace Server.Items
{
	public class WerewolfAncientRobe : BaseOuterTorso
	{
		public override int ArtifactRarity{ get{ return 100; } }
		
		public override int InitMinHits{ get{ return 100; } }
		public override int InitMaxHits{ get{ return 100; } }
		
		[Constructable]
		public WerewolfAncientRobe() : base( 0x2683 )
		{
			Weight = 2.0;
			Hue = 1905;
			Name = "Ancient Werewolf Robe";
			Attributes.BonusHits = 30;
			Attributes.BonusMana = 5;
			Attributes.BonusStam = 30;
			Attributes.BonusStr = 8;
			Attributes.BonusDex = 5;
			Attributes.BonusInt = 1;
			
			ItemID = 0x2683;
			if (Utility.RandomDouble() < 0.05) { Movable = true; } else { Movable = false; }
		}

		public WerewolfAncientRobe( Serial serial ) : base( serial )
		{
		}

		public override bool Dye( Mobile from, DyeTub sender )
		{
			if ( Deleted )
				return false;
			else if ( RootParent is Mobile && from != RootParent )
				return false;

			Hue = 1905;

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