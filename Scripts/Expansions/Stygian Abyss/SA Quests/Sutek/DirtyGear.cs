using System; 
using Server;
using Server.Misc; 
using Server.Items;
using Server.Items.MusicBox;
using Server.Network;
using Server.Mobiles;

namespace Server.Items
{ 
	public class DirtyGear : Item 
	{ 

		[Constructable]
		public DirtyGear() : this( 1 )
		{
			ItemID = 0x1053;
			Movable = true;
			Hue = 962;
			Name = "Sutek's Dirty Gear";	
		}
		
		public override void OnDoubleClick( Mobile from )
      	{
			if( Utility.RandomDouble() < 0.05 )
			{
				from.AddToBackpack( MusicBoxGears.RandomMusixBoxGears( TrackRarity.Rare ) );
			}
			else
			{
				if( Utility.RandomBool() )
					from.AddToBackpack( MusicBoxGears.RandomMusixBoxGears( TrackRarity.Common ) );
				else
					from.AddToBackpack( MusicBoxGears.RandomMusixBoxGears( TrackRarity.UnCommon ) );
			}
				this.Delete();
        }

		[Constructable]
		public DirtyGear( int amount ) 
        {
		}

		public DirtyGear( Serial serial ) : base( serial ) 
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