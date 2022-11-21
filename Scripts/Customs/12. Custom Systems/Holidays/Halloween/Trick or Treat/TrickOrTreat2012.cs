using System;
using Server;
using Server.Items;
using Server.Guilds;
using Server.Mobiles;
using Server.Gumps;
using Server.Network;

namespace Server.Misc
{
	public class TrickOrTreat2012
	{
		public static void Initialize()
		{
			// Register our speech handler
			EventSink.Speech += new SpeechEventHandler( EventSink_Speech );
		}

		public static void EventSink_Speech( SpeechEventArgs args )
		{
			Mobile from = args.Mobile;
			int[] keywords = args.Keywords;


			if ( from is PlayerMobile )
			{
				PlayerMobile player = from as PlayerMobile;
				
				if ( args.Speech.ToLower().Equals("trick or treat"))
				{
					Item[] items = from.Backpack.FindItemsByType( typeof( TrickOrTreatBag2012 ) );

					if ( items.Length == 0 )
					{
						from.SendMessage ("You need a goodie basket to go trick or treating!");
					}
					else
					{
						bool foundbag = false;
						
						foreach( TrickOrTreatBag2012 tb in items )
						{
							if ( tb.Uses > 0 )
							{ 
								foreach ( Mobile m in from.GetMobilesInRange( 2 ) ) // TODO: Validate range
								{
									if ( !m.Player && m.Body.IsHuman && ( m is BaseVendor ) )
									{
										if (m is BaseCreature && (((BaseCreature)m).IsHumanInTown() ) )
										{
											from.Direction = from.GetDirectionTo( m );
											m.Direction = m.GetDirectionTo( from );
											
											TrickOrTreat2012.GiveTreat( from, m, tb );
											tb.ConsumeUse( from );
											
											return;
										}
									}
								}

								foundbag = true;

								break;
							}
						}
						
						
						if ( !foundbag )
						{
							from.SendMessage("You don't have any uses left on your goodie basket.");
						}
					}
				}
			}
		}

		private static void PlaceItemIn( Container parent, int x, int y, Item item )
		{
			parent.AddItem( item );
			item.Location = new Point3D( x, y, 0 );
		}
		
		public static void GiveTreat ( Mobile from, Mobile vendor, Container gb)
		{
			if ( Utility.RandomDouble() < 0.60 )
			{
				//Give special Items.
			
				switch ( Utility.Random ( 5 ) )
				{
					case 0:
						//Give mask
						vendor.Say ("Well, I am out of goodies, but let me give you this Spooky Decoration.");
						PlaceItemIn( gb, 93, 96, new GrimWarning() );break;
					case 1:
						//Give knife
						vendor.Say ("Well, I am out of goodies, but let me give you these scarey skulls.");
						PlaceItemIn( gb, 93, 75, new SkullsOnPike() );break;
					case 2:
						//Give costume
						vendor.Say ("Well, I am out of goodies, but let me give you this Black Cat.");
						PlaceItemIn( gb, 93, 34, new BlackCatStatue() );break;	
					case 3:
						//Give costume
						vendor.Say ("Well, I am out of goodies, but let me give you this Scarey Tapestry.");
						PlaceItemIn( gb, 93, 96, new RuinedTapestry() );break;
					case 4:
						//Give costume
						vendor.Say ("Well, I am out of goodies, but let me give you this Pumpkin Scarecrow.");
						PlaceItemIn( gb, 93, 75, new PumpkinScarecrow() );break;
					

				}		
				
			}
			else
			{
				switch ( Utility.Random ( 5 ) )
				{
			
			
					case 0:
						//Give cookies
						vendor.Say ("Here's a spooky spider web for you.  Happy Halloween");
						PlaceItemIn( gb, 29, 53, new HalloweenWeb() );
						
						break;
			
					case 1:
						//Give Candy Heart
						vendor.Say ("Here's some candy for you.  Happy Halloween");
						PlaceItemIn( gb, 55, 56, new HalloweenCandy() );
						
						break;
					case 2:
						//Give Gummy Worm
						vendor.Say ("Here's some candy for you.  Happy Halloween");
						PlaceItemIn( gb, 59, 84, new HalloweenCandy2() );
						
						break;
					case 3:
						//Give Chocolate Skeleton
						vendor.Say ("Here's some candy for you.  Happy Halloween");
						PlaceItemIn( gb, 77, 56, new HalloweenCandy() );
						
						break;
					case 4:
						//Give Pumpkin Candycorn
						vendor.Say ("Here's some candy for you.  Happy Halloween");
						PlaceItemIn( gb, 73, 96, new HalloweenCandy2() );
						
						break;
				}
			}
		}
	}
}
