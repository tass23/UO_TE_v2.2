using System;
using Server.Items;
using Server.Network;
using Server.Misc;

namespace Server.Items
{
	
	public class BingoNumbersBag : Container
	{
                                   public static void PlaceItemIn( Container parent, int x, int y, Item item )
		{
			parent.AddItem( item );
			item.Location = new Point3D( x, y, 0 );
		}
		[Constructable]
		public  BingoNumbersBag() : base ( 0xE75 )
                                                                                      
                                  
		{
                                                      Name = "Bingo Numbers Bag";                                                        
                                                      
		   	 CharacterCreation.PlaceItemIn( this, 44, 66, new BingoB1( ) );
                                                      CharacterCreation.PlaceItemIn( this, 51, 66, new BingoB2( ) );
                                                      CharacterCreation.PlaceItemIn( this, 58, 66, new BingoB3( ) );
                                                      CharacterCreation.PlaceItemIn( this, 63, 66, new BingoB4( ) );
                                                      CharacterCreation.PlaceItemIn( this, 69, 66, new BingoB5( ) );
                                                      CharacterCreation.PlaceItemIn( this, 75, 66, new BingoB6( ) );
                                                      CharacterCreation.PlaceItemIn( this, 81, 66, new BingoB7( ) );
                                                      CharacterCreation.PlaceItemIn( this, 87, 66, new BingoB8( ) );
                                                      CharacterCreation.PlaceItemIn( this, 93, 66, new BingoB9( ) );
                                                      CharacterCreation.PlaceItemIn( this, 99, 66, new BingoB10( ) );
                                                      CharacterCreation.PlaceItemIn( this, 105, 66, new BingoB11( ) );
                                                      CharacterCreation.PlaceItemIn( this, 111, 66, new BingoB12( ) );
                                                      CharacterCreation.PlaceItemIn( this, 117, 66, new BingoB13( ) );
                                                      CharacterCreation.PlaceItemIn( this, 123, 66, new BingoB14( ) );
                                                      CharacterCreation.PlaceItemIn( this, 129, 66, new BingoB15( ) );

                                                      CharacterCreation.PlaceItemIn( this, 44, 80, new BingoI16( ) );
                                                      CharacterCreation.PlaceItemIn( this, 51, 80, new BingoI17( ) );
                                                      CharacterCreation.PlaceItemIn( this, 58, 80, new BingoI18( ) );
                                                      CharacterCreation.PlaceItemIn( this, 63, 80, new BingoI19( ) );
                                                      CharacterCreation.PlaceItemIn( this, 69, 80, new BingoI20( ) );
                                                      CharacterCreation.PlaceItemIn( this, 75, 80, new BingoI21( ) );
                                                      CharacterCreation.PlaceItemIn( this, 81, 80, new BingoI22( ) );
                                                      CharacterCreation.PlaceItemIn( this, 87, 80, new BingoI23( ) );
                                                      CharacterCreation.PlaceItemIn( this, 93, 80, new BingoI24( ) );
                                                      CharacterCreation.PlaceItemIn( this, 99, 80, new BingoI25( ) );
                                                      CharacterCreation.PlaceItemIn( this, 105, 80, new BingoI26( ) );
                                                      CharacterCreation.PlaceItemIn( this, 111, 80, new BingoI27( ) );
                                                      CharacterCreation.PlaceItemIn( this, 117, 80, new BingoI28( ) );
                                                      CharacterCreation.PlaceItemIn( this, 123, 80, new BingoI28( ) );
                                                      CharacterCreation.PlaceItemIn( this, 129, 80, new BingoI30( ) );

                                                      CharacterCreation.PlaceItemIn( this, 44, 97, new BingoN31( ) );
                                                      CharacterCreation.PlaceItemIn( this, 51, 97, new BingoN32( ) );
                                                      CharacterCreation.PlaceItemIn( this, 58, 97, new BingoN33( ) );
                                                      CharacterCreation.PlaceItemIn( this, 63, 97, new BingoN34( ) );
                                                      CharacterCreation.PlaceItemIn( this, 69, 97, new BingoN35( ) );
                                                      CharacterCreation.PlaceItemIn( this, 75, 97, new BingoN36( ) );
                                                      CharacterCreation.PlaceItemIn( this, 81, 97, new BingoN37( ) );
                                                      CharacterCreation.PlaceItemIn( this, 87, 97, new BingoN38( ) );
                                                      CharacterCreation.PlaceItemIn( this, 93, 97, new BingoN38( ) );
                                                      CharacterCreation.PlaceItemIn( this, 99, 97, new BingoN40( ) );
                                                      CharacterCreation.PlaceItemIn( this, 105, 97, new BingoN41( ) );
                                                      CharacterCreation.PlaceItemIn( this, 111, 97, new BingoN42( ) );
                                                      CharacterCreation.PlaceItemIn( this, 117, 97, new BingoN43( ) );
                                                      CharacterCreation.PlaceItemIn( this, 123, 97, new BingoN44( ) );
                                                      CharacterCreation.PlaceItemIn( this, 129, 97, new BingoN45( ) );

                                                      CharacterCreation.PlaceItemIn( this, 44, 113, new BingoG46( ) );
                                                      CharacterCreation.PlaceItemIn( this, 51, 113, new BingoG47( ) );
                                                      CharacterCreation.PlaceItemIn( this, 58, 113, new BingoG48( ) );
                                                      CharacterCreation.PlaceItemIn( this, 63, 113, new BingoG49( ) );
                                                      CharacterCreation.PlaceItemIn( this, 69, 113, new BingoG50( ) );
                                                      CharacterCreation.PlaceItemIn( this, 75, 113, new BingoG51( ) );
                                                      CharacterCreation.PlaceItemIn( this, 81, 113, new BingoG52( ) );
                                                      CharacterCreation.PlaceItemIn( this, 87, 113, new BingoG53( ) );
                                                      CharacterCreation.PlaceItemIn( this, 93, 113, new BingoG54( ) );
                                                      CharacterCreation.PlaceItemIn( this, 99, 113, new BingoG55( ) );
                                                      CharacterCreation.PlaceItemIn( this, 105, 113, new BingoG56( ) );
                                                      CharacterCreation.PlaceItemIn( this, 111, 113, new BingoG57( ) );
                                                      CharacterCreation.PlaceItemIn( this, 117, 113, new BingoG58( ) );
                                                      CharacterCreation.PlaceItemIn( this, 123, 113, new BingoG58( ) );
                                                      CharacterCreation.PlaceItemIn( this, 129, 113, new BingoG60( ) );

                                                      CharacterCreation.PlaceItemIn( this, 44, 128, new BingoO61( ) );
                                                      CharacterCreation.PlaceItemIn( this, 51, 128, new BingoO62( ) );
                                                      CharacterCreation.PlaceItemIn( this, 58, 128, new BingoO63( ) );
                                                      CharacterCreation.PlaceItemIn( this, 63, 128, new BingoO64( ) );
                                                      CharacterCreation.PlaceItemIn( this, 69, 128, new BingoO65( ) );
                                                      CharacterCreation.PlaceItemIn( this, 75, 128, new BingoO66( ) );
                                                      CharacterCreation.PlaceItemIn( this, 81, 128, new BingoO67( ) );
                                                      CharacterCreation.PlaceItemIn( this, 87, 128, new BingoO68( ) );
                                                      CharacterCreation.PlaceItemIn( this, 93, 128, new BingoO69( ) );
                                                      CharacterCreation.PlaceItemIn( this, 99, 128, new BingoO70( ) );
                                                      CharacterCreation.PlaceItemIn( this, 105, 128, new BingoO71( ) );
                                                      CharacterCreation.PlaceItemIn( this, 111, 128, new BingoO72( ) );
                                                      CharacterCreation.PlaceItemIn( this, 117, 128, new BingoO73( ) );
                                                      CharacterCreation.PlaceItemIn( this, 123, 128, new BingoO74( ) );
                                                      CharacterCreation.PlaceItemIn( this, 129, 128, new BingoO75( ) );
                                                    
			
		}
                                   
                                                                                     
		public BingoNumbersBag( Serial serial ) : base( serial )
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