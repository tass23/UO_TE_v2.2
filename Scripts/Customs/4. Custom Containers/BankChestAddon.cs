/*
BankChestAddon.cs for RunUO 2.0 by Alari (alarihyena@gmail.com)
Released under the GNU GPL License v3.

Constructable Items:

BankChestAddon - A bank chest addon, double click to open bank box.
BankChestAddonDeed - A deed to allow a player to place a bank chest in their home.
*/

//          Main settings

/*
The SET_MAXITEMS setting is a something I use as an easy way to change 
the max items for a player's bank box. I'm testing, trying to find the 
right amount of capacity to let the average beginning player live out of 
their bank box for a while. Code below 
*/

// #define SET_MAXITEMS


using System;
using System.Collections;
using System.Collections.Generic;
using Server;
using Server.ContextMenus;
using Server.Gumps;
using Server.Multis;
using Server.Network;

namespace Server.Items
{
	[FlipableAttribute( 0xE41, 0xE40 )]	 // Metal & Gold chest
	public class BankChestComponent : AddonComponent
	{
		public BankChestComponent() : this( Utility.RandomList( 0xE40, 0xE41 ) )
		{
		}
		
		public BankChestComponent( int itemID ) : base( itemID )
		{
			Hue = Utility.RandomYellowHue();
		}
		
		public override void AddNameProperty(ObjectPropertyList list)
		{
			list.Add( 1062824 );  // Your Bank Box
		}
		
		public override void OnDoubleClick( Mobile from )
		{
			if ( from != null )
			{
				if ( from.InRange( this.GetWorldLocation(), 3 ) )  // 3 tiles away.
				{
					BankBox box = from.BankBox;
				
					if ( box != null )
					{

#if SET_MAXITEMS					
						if ( box.MaxItems < 300 )  // my li'l update. ;)
							box.MaxItems = 300;
#endif

						box.Open();
					}
					else
					{
						from.SendLocalizedMessage( 1043288 );  // An internal error has occurred...
						Console.WriteLine( "BankChestAddon.cs: Player {0}'s bank box was null...", from.Name );
					}
				}
				else
				{
					from.SendLocalizedMessage( 500446 );  // That is too far away.
				}
			}
		}
		
		public BankChestComponent( Serial serial ) : base( serial )
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
	
	public class BankChestAddon : BaseAddon
	{
		public override BaseAddonDeed Deed{ get{ return new BankChestAddonDeed(); } }
		
		[Constructable]
		public BankChestAddon()
		{
			AddComponent( new BankChestComponent(), 0, 0, 0 );
		}
		
		public BankChestAddon( Serial serial ) : base( serial )
		{
		}
		
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			
			writer.Write( (int) 0 );  // version
		}
		
		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			
			int version = reader.ReadInt();
		}
	}
	
	public class BankChestAddonDeed : BaseAddonDeed
	{
		public override BaseAddon Addon{ get{ return new BankChestAddon(); } }
		
		[Constructable]
		public BankChestAddonDeed()
		{
			Name = "a bank chest deed";
		}
		
		public BankChestAddonDeed( Serial serial ) : base( serial )
		{
		}
		
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			
			writer.Write( (int) 0 );  // version
		}
		
		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			
			int version = reader.ReadInt();
		}
	}
}