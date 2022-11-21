//bingo game
//by henry_r
//02/01/08
using System;
using Server.Items;
using Server.Network;
using Server.Misc;

namespace Server.Items
{
	public class BingoGame : Container
	{
        public static void PlaceItemIn( Container parent, int x, int y, Item item )
		{
			parent.AddItem( item );
			item.Location = new Point3D( x, y, 0 );
		}

		[Constructable]
		public  BingoGame() : base ( 0xE76 )
		{
			Name = "Bingo Game";  
			Hue = 1072;
			CharacterCreation.PlaceItemIn( this, 29, 34, new BingoCardBag( ) );
			CharacterCreation.PlaceItemIn( this, 93, 34, new BingoCardMasterListBag( ) );
			CharacterCreation.PlaceItemIn( this, 29, 96, new BingoNumbersBag( ) );
			CharacterCreation.PlaceItemIn( this, 93, 96, new BingoCalledNumbersBag( ) );
			CharacterCreation.PlaceItemIn( this, 78, 34, new BingoPlayerCardRegister( ) );
			CharacterCreation.PlaceItemIn( this, 81, 100, new BingoCalledNumbers( ) );                                                    			
		}

		public BingoGame( Serial serial ) : base( serial )
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