using System;
using System.Collections;
using System.Collections.Generic;
using Server;
using Server.Mobiles;
using Server.Network;
using Server.Items;
using Server.ContextMenus;
using Server.Targeting;

namespace Server.Mobiles
{
	public class VanityPetDog : VanityPet
	{
		public VanityPetDog()
		{
			if ( Utility.RandomDouble() < 0.25 )
			{
				Body = 27;
			}
			else if ( Utility.RandomDouble() < 0.15 )
			{
				Body = 217;
			}
			else if ( Utility.RandomDouble() < 0.10 )
			{
				Body = 277;
			}
			else
			{
				Body = 27;
			}

			Name = "a pet dog";
            Title = "your furry friend";
			BaseSoundID = 0x087;
			
			SetStr( 1, 5 );
			SetDex( 25, 30 );
			SetInt( 2 );
			
			SetHits( 1, Str );
			SetStam( 25, Dex );
			SetMana( 0 );
			ControlSlots = 0;

			Blessed = true;
			ControlOrder = OrderType.Follow;
		}

		public VanityPetDog( Serial serial ) : base( serial )
		{
		}

		public override void OnSpeech( SpeechEventArgs e )
		{
			base.OnSpeech( e );
			
			if ( Utility.RandomDouble() < 0.05 )
			{
				Say( e.Speech );
				PlaySound( 0x086 );
			}
		}
		
		public override bool OnDragDrop( Mobile from, Item dropped )
		{
			if ( dropped is PremiumDogFood )
			{
				dropped.Delete();
				from.EmoteHue = this.Hue;

				switch ( Utility.Random( 6 ) )
				{
					case 0:
					{
						Say("I was daydreaming about the biscuit-making place.");
						if ( chance > 3 )
						{
							from.Emote("*pants rigorously*");
							from.PlaySound( from.Female ? 783 : 1054 );	//Say( "*woohoo*" );
							from.SendMessage("Doggie Pawsight: You re-live the dog's moments in your head.");
						}
						break;	//"Vanity Pet: Doggie Pawsight"
					}
					case 1:
					{
						Say("Do you smell that?");
						if ( chance > 3 )
						{
							from.Emote("*sniffs*");
							from.PlaySound( from.Female ? 818 : 1092 );	//Say( "*sniffs*" );
							from.PlaySound( from.Female ? 818 : 1092 );
							if( !from.Mounted && UseAnimations )
								from.Animate( 34, 5, 1, true, false, 0 );
							from.SendMessage("Doggie Nostrilogy: Your nose sniffs the air to determine the smell.");
						}
						break;	//"Vanity Pet: Doggie Nostrilogy"
					}
					case 2:
					{
						Say("I licked all of the food in your bags to make sure nothing was poisoned.");
						if ( chance > 3 )
						{
							from.Emote("*spits*");
							from.PlaySound( from.Female ? 820 : 1094 );	//Say( "*spits*" );
							if( !from.Mounted && UseAnimations )
								from.Animate( 6, 5, 1, true, false, 0 );
							from.SendMessage("Doggie Snacks: You spit some food out of your mouth in disgust.");
						}
						break;	//"Vanity Pet: Doggie Snacks"
					}
					case 3:
					{
						Say("Well, it was small, but then there were lots! Hell cats are mean!");
						if ( chance > 3 )
						{
							from.Say( "*aaahh!*" );
							from.PlaySound( from.Female ? 814 : 1088 );	//Say( "*aaahh!*" );
							from.SendMessage("Doggie Visions: You scream in terror.");
						}
						break;	//"Vanity Pet: Doggie Visions"
					}
					case 4:
					{
						Say("...and then I thought the ball was thrown, but you had it the whole time!");
						if ( chance > 3 )
						{
							from.Say("*gasps*");
							from.PlaySound( from.Female ? 793 : 1065 );	//Say( "*gasps*" );
							from.Say( "*giggles*" );
							from.PlaySound( from.Female ? 794 : 1066 );	//Say( "*giggles*" );
							from.SendMessage("Doggie Discordance: You feign shock and surprise, badly.");
						}
						break;	//"Vanity Pet: Doggie Discordance"
					}
					case 5:
					{
						Say("Have you seen any butterflies?");
						if ( chance > 3 )
						{
							from.Say( "*whistles*" );
							from.PlaySound( from.Female ? 821 : 1095 );	//Say( "*whistles*" );
							if ( !from.Mounted && UseAnimations )
								from.Animate( 5, 5, 1, true, false, 0 );
							from.SendMessage("Doggie Distress: You think a whistle might summon a butterfly for some reason.");
						}
						break;	//"Vanity Pet: Doggie Distress"
					}
				}

				PlaySound( Utility.RandomMinMax( 0x85, 0x89 ) );
				return true;
			}
			else
				return false;
		}
		
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override FoodType FavoriteFood{ get{ return FoodType.FruitsAndVegies; } }

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}

	public class VanityPetBird : VanityPet
	{
		public VanityPetBird()
		{
			if ( Utility.RandomDouble() < 0.25 )
			{
				Body = 0x11A;
			}
			else if ( Utility.RandomDouble() < 0.15 )
			{
				Body = 0x5;
			}
			else if ( Utility.RandomDouble() < 0.10 )
			{
				Body = 0x6;
			}
			else
			{
				Body = 0x11A;
			}

			Name = "a pet bird";
            Title = "your feathery friend";
			BaseSoundID = 0x087;
			
			SetStr( 1, 5 );
			SetDex( 25, 30 );
			SetInt( 2 );
			
			SetHits( 1, Str );
			SetStam( 25, Dex );
			SetMana( 0 );
			ControlSlots = 0;

			Blessed = true;
			ControlOrder = OrderType.Follow;
		}

		public VanityPetBird( Serial serial ) : base( serial )
		{
		}

		public override void OnSpeech( SpeechEventArgs e )
		{
			base.OnSpeech( e );
			
			if ( Utility.RandomDouble() < 0.05 )
			{
				Say( e.Speech );
				PlaySound( 0x19 );
			}
		}
		
		public override bool OnDragDrop( Mobile from, Item dropped )
		{
			if ( dropped is PremiumParrotFood )
			{
				dropped.Delete();

				switch ( Utility.Random( 6 ) )
				{
					case 0:
					{
						Say("I was flying over Luna earlier and saw some interesting people!");
						if ( chance > 2 )
						{
							from.Emote("*flaps arms*");
							from.PlaySound( from.Female ? 783 : 1054 );	//Say( "*woohoo*" );
							from.SendMessage("Birdie Eyesight: You see the uninteresting people the bird saw in your head.");
						}
						break;	//"Vanity Pet: Birdie Eyesight"
					}
					case 1:
					{
						Say( "Is that a raspberry seed that I smell?");
						if ( chance > 2 )
						{
							from.Emote("*sniffs*");
							from.PlaySound( from.Female ? 818 : 1092 );	//Say( "*sniffs*" );
							from.PlaySound( from.Female ? 818 : 1092 );
							if( !from.Mounted && UseAnimations )
								from.Animate( 34, 5, 1, true, false, 0 );
							from.SendMessage("Birdie Cravings: You really wish you had some raspberries to eat!");
						}
						break;	//"Vanity Pet: Birdie Cravings"
					}
					case 2:
					{
						Say("I found a half-eaten worm this morning.");
						if ( chance > 2 )
						{
							from.Emote("*spits*");
							from.PlaySound( from.Female ? 820 : 1094 );	//Say( "*spits*" );
							if( !from.Mounted && UseAnimations )
								from.Animate( 6, 5, 1, true, false, 0 );
							from.SendMessage("Birdie Regrets: You can taste worm guts in your mouth.");
						}
						break;	//"Vanity Pet: Birdie Regrets"
					}
					case 3:
					{
						Say("(hours later)...so then, I flew all the way back home to get that.");
						if ( chance > 2 )
						{
							from.Say( "*aaahh!*" );
							from.PlaySound( from.Female ? 814 : 1088 );	//Say( "*aaahh!*" );
							from.SendMessage("Birdie Storytelling: You scream from boredom.");
						}
						break;	//"Vanity Pet: Birdie Storytelling"
					}
					case 4:
					{
						Say("...and they were all 'Get in the cage, bird!' and I was like...");
						if ( chance > 2 )
						{
							from.Say("*gasps*");
							from.PlaySound( from.Female ? 793 : 1065 );	//Say( "*gasps*" );
							from.Say( "*giggles*" );
							from.PlaySound( from.Female ? 794 : 1066 );	//Say( "*giggles*" );
							from.SendMessage("Birdie Shock and Aw: You feign shock and surprise, badly.");
						}
						break;	//"Vanity Pet: Birdie Shock and Aw"
					}
					case 5:
					{
						Say("I was once a Pirate, with my own ship and my own treasure!");
						if ( chance > 2 )
						{
							from.Say( "*bs cough*" );
							from.PlaySound( from.Female ? 786 : 1057 );	//Say( "*bs cough!*" );
							from.SendMessage("Birdie Bragging: You glare at the bird with dismay.");
						}
						break;	//"Vanity Pet: Birdie Bragging"
					}
				}

				PlaySound( Utility.RandomMinMax( 0x19, 0x01D ) );
				return true;
			}
			else
				return false;
		}
		
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override FoodType FavoriteFood{ get{ return FoodType.FruitsAndVegies; } }

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}
}