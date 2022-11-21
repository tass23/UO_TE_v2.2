using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class ShadowBox2013 : GiftBoxRectangle
	{
		public override string DefaultName
		{
			get { return "Halloween 2013 Gift Box"; }
		}

		[Constructable]
		public ShadowBox2013() : this( 1 )
		{
			Movable = true;
			Hue = 1175;
		}

		[Constructable]
		public ShadowBox2013( int amount )
		{
			DropItem( new FireDemonStatueDeed() );
			DropItem( new SpikeColumnDeed() );
			DropItem( new SpikePostDeed() );
			
			switch ( Utility.Random( 2 ))
			{
				case 0:DropItem( new ShadowAltarSouthAddonDeed() );break;
				case 1:DropItem( new ShadowAltarEastAddonDeed() );break;
			}
			switch ( Utility.Random( 2 ))
			{
				case 0:DropItem( new ShadowBannerEastAddonDeed() );break;
				case 1:DropItem( new ShadowBannerSouthAddonDeed() );break;
			}
			switch ( Utility.Random( 3 ))
			{
				case 0:DropItem( new ObsidianPillarDeed() );break;
				case 1:DropItem( new ObsidianRockDeed() );break;
				case 2:DropItem( new ShadowPillarDeed() );break;
			}
                      	

		}
		
		public ShadowBox2013( Serial serial ) : base( serial )
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
