using System;
using Server.Network;
using Server.Items;

namespace Server.Items
{
    public class TheNightReaper : RepeatingCrossbow, ITokunoDyable
	{
		public override int LabelNumber{ get{ return 1072912; } } // The Night Reaper

        public override int InitMinHits { get { return 255; } }
        public override int InitMaxHits { get { return 255; } }

		[Constructable]
		public TheNightReaper()
		{
			ItemID = 0x26CD;
			Hue = 0x41C;

			Slayer = SlayerName.Exorcism;
			Attributes.NightSight = 1;
			Attributes.WeaponSpeed = 25;
			Attributes.WeaponDamage = 55;
		}

		public TheNightReaper( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}
}