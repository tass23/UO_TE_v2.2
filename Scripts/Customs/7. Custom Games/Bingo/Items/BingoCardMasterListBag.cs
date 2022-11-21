using System;
using Server.Items;
using Server.Network;
using Server.Misc;

namespace Server.Items
{
	
	public class BingoCardMasterListBag : Container
	{
                                   public static void PlaceItemIn( Container parent, int x, int y, Item item )
		{
			parent.AddItem( item );
			item.Location = new Point3D( x, y, 0 );
		}

		[Constructable]
		public BingoCardMasterListBag () : base ( 0xE76 )
                                                                                      
                                  
		{
                                                      Name = "Bingo Card Master List Bag "; 
                                                      
		   	 CharacterCreation.PlaceItemIn( this, 29, 34, new BingoCard1( ) );
                                                      CharacterCreation.PlaceItemIn( this, 43, 34, new BingoCard2( ) );
                                                      CharacterCreation.PlaceItemIn( this, 53, 34, new BingoCard3( ) );
                                                      CharacterCreation.PlaceItemIn( this, 66, 34, new BingoCard4( ) );
                                                      CharacterCreation.PlaceItemIn( this, 78, 34, new BingoCard5( ) );
                                                      CharacterCreation.PlaceItemIn( this, 93, 34, new BingoCard6( ) );
                                                      CharacterCreation.PlaceItemIn( this, 29, 68, new BingoCard7( ) );
                                                      CharacterCreation.PlaceItemIn( this, 43, 68, new BingoCard8( ) );
                                                      CharacterCreation.PlaceItemIn( this, 53, 68, new BingoCard9( ) );
                                                      CharacterCreation.PlaceItemIn( this, 66, 68, new BingoCard10( ) );
                                                      CharacterCreation.PlaceItemIn( this, 78, 68, new BingoCard10( ) );
                                                      CharacterCreation.PlaceItemIn( this, 93, 68, new BingoCard12( ) );
                                                      CharacterCreation.PlaceItemIn( this, 29, 100, new BingoCard13( ) );
                                                      CharacterCreation.PlaceItemIn( this, 43, 100, new BingoCard14( ) );
                                                      CharacterCreation.PlaceItemIn( this, 53, 100, new BingoCard15( ) );
                                                      CharacterCreation.PlaceItemIn( this, 66, 100, new BingoCard16( ) );
                                                      CharacterCreation.PlaceItemIn( this, 78, 100, new BingoCard17( ) );
                                                      CharacterCreation.PlaceItemIn( this, 93, 100, new BingoCard18( ) );
                                                           
                                        
		   	                                                    			
		}
                                                                                                                        
		public BingoCardMasterListBag( Serial serial ) : base( serial )
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