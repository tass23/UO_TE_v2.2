/***************************************************************************
 *                               CREDITS
 *                         -------------------
 *                         : (C) 2004-2009 Luke Tomasello (AKA Adam Ant)
 *                         :   and the Angel Island Software Team
 *                         :   luke@tomasello.com
 *                         :   Official Documentation:
 *                         :   www.game-master.net, wiki.game-master.net
 *                         :   Official Source Code (SVN Repository):
 *                         :   http://game-master.net:8050/svn/angelisland
 *                         : 
 *                         : (C) May 1, 2002 The RunUO Software Team
 *                         :   info@runuo.com
 *
 *   Give credit where credit is due!
 *   Even though this is 'free software', you are encouraged to give
 *    credit to the individuals that spent many hundreds of hours
 *    developing this software.
 *   Many of the ideas you will find in this Angel Island version of 
 *   Ultima Online are unique and one-of-a-kind in the gaming industry! 
 *
 ***************************************************************************/

/***************************************************************************
 *
 *   This program is free software; you can redistribute it and/or modify
 *   it under the terms of the GNU General Public License as published by
 *   the Free Software Foundation; either version 2 of the License, or
 *   (at your option) any later version.
 *
 ***************************************************************************/

/* Scripts/Mobiles/Monsters/Humanoid/Magic/PirateWench.cs	
 * ChangeLog:
 *  7/02/06, Kit
 *		InitBody/InitOutfit additions
 *	7/26/05, erlein
 *		Automated removal of AoS resistance related function calls. 8 lines removed.
 *	4/13/05, Kit
 *		Switch to new region specific loot model
 *	4/9/05, Adam
 *		Upgrade treasure map level to 3 from 2
 *	1/2/05, Adam
 *		Cleanup pirate name management, make use of Titles
 *			Show title when clicked = true
 *  1/02/05, Jade
 *      Increased speed to bring Pirates up to par with other human IOB kin.
 *	12/30/04 Created by Adam
 */

using System;
using System.Collections;
using Server.Items;
using Server.Targeting;
using Server.Spells;
//using Server.Engines.IOBSystem;

namespace Server.Mobiles
{
	[CorpseName( "corpse of a salty seadog" )]
	public class PirateWench : BaseCreature
	{
		private TimeSpan m_SpeechDelay = TimeSpan.FromSeconds( 10.0 ); // time between pirate speech
		public DateTime m_NextSpeechTime;

		[Constructable]
		public PirateWench() : base( AIType.AI_Mage, /*FightMode.All |*/ FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			//IOBAlignment = IOBAlignment.Pirate;
			//ControlSlots = 3;
            Hue = Utility.RandomSkinHue();
            
            Female = true;
			if ( Female )
			{
				Body = 0x191;
				// "Lizzie" "the Black"
				Name = NameList.RandomName("pirate_female");
				Title = NameList.RandomName("pirate_title");
			}
			else
			{
				Body = 0x190;
				if (Utility.RandomBool())
				{
					// "John" "the Black"
					Name = NameList.RandomName("pirate_male");
					Title = NameList.RandomName("pirate_title");
				}
				else
				{
					// "John" "Black""Beard"
					Name = NameList.RandomName("pirate_male") + " " + NameList.RandomName("pirate_color") + NameList.RandomName("pirate_part");
				}
			}
			
			AddItem( new SkullCap( Utility.RandomRedHue() ) );
			
			if ( Utility.RandomBool() )
			{
				Item shirt = new Shirt( Utility.RandomRedHue() );
				AddItem( shirt );	
			}
			
			Item sash = new BodySash(0x85);
			Item hair = new Item( Utility.RandomList( 0x203B, 0x203C, 0x203D, 0x2044, 0x2045, 0x2047, 0x2049, 0x204A ) );
			Item pants = new LongPants( Utility.RandomRedHue() );
			Item boots = new Boots( Utility.RandomRedHue() );
			hair.Hue = Utility.RandomHairHue();
			hair.Layer = Layer.Hair;
			hair.Movable = false;
			
			AddItem( hair );
			AddItem( sash );
			AddItem( pants );
			AddItem( boots );

			SetStr( 171, 200 );
			SetDex( 126, 145 );
			SetInt( 276, 305 );

			SetHits( 103, 120 );

			SetDamage( 24, 26 );

			SetSkill( SkillName.EvalInt, 100.0 );
			SetSkill( SkillName.Magery, 70.1, 80.0 );
			SetSkill( SkillName.Meditation, 85.1, 95.0 );
			SetSkill( SkillName.MagicResist, 80.1, 100.0 );
			SetSkill( SkillName.Tactics, 70.1, 90.0 );

			Fame = 8000;
			Karma = -8000;

			//InitBody();
			//InitOutfit();

			VirtualArmor = 50;

			SpeechHue = Utility.RandomDyedHue();
			
			Title = "the pirate wench";

			m_NextSpeechTime = DateTime.Now + m_SpeechDelay;		
		}

		public override int TreasureMapLevel{ get{ return 3; } }	
		public override bool AlwaysMurderer{ get{ return true; } }
		public override bool CanRummageCorpses{ get{ return true; } }
		public override bool ShowFameTitle{ get{ return false; } }
		public override bool ClickTitle { get { return true; } }

		/*public override void InitBody()
		{
			Female = true;
			if ( Female )
			{
				Body = 0x191;
				// "Lizzie" "the Black"
				Name = NameList.RandomName("pirate_female");
				Title = NameList.RandomName("pirate_title");
			}
			else
			{
				Body = 0x190;
				if (Utility.RandomBool())
				{
					// "John" "the Black"
					Name = NameList.RandomName("pirate_male");
					Title = NameList.RandomName("pirate_title");
				}
				else
				{
					// "John" "Black""Beard"
					Name = NameList.RandomName("pirate_male") + " " + NameList.RandomName("pirate_color") + NameList.RandomName("pirate_part");
				}
			}
		}

		public override void InitOutfit()
		{
			WipeLayers();
			
			AddItem( new SkullCap( Utility.RandomRedHue() ) );
			
			if ( Utility.RandomBool() )
			{
				Item shirt = new Shirt( Utility.RandomRedHue() );
				AddItem( shirt );	
			}
			
			Item sash = new BodySash(0x85);
			Item hair = new Item( Utility.RandomList( 0x203B, 0x203C, 0x203D, 0x2044, 0x2045, 0x2047, 0x2049, 0x204A ) );
			Item pants = new LongPants( Utility.RandomRedHue() );
			Item boots = new Boots( Utility.RandomRedHue() );
			hair.Hue = Utility.RandomHairHue();
			hair.Layer = Layer.Hair;
			hair.Movable = false;
			
			AddItem( hair );
			AddItem( sash );
			AddItem( pants );
			AddItem( boots );
		
		}*/
		public override void OnThink()
		{
			if ( DateTime.Now >= m_NextSpeechTime )
			{
				Mobile combatant = this.Combatant;

				if ( combatant != null && combatant.Map == this.Map && combatant.InRange( this, 8 ) )
				{
					int phrase = Utility.Random( 4 );

					switch ( phrase )
					{
						case 0: this.Say( true, "Lights and liver!" ); break;
						case 1: this.Say( true, "Arr! Get ye a-swabbin' or yer life ends now!" ); break;
						case 2: this.Say( true, "I'll rip off yer fins and burn ya to slow fire!" ); break;
						case 3: this.Say( true, "Keel haul ye we will!" ); break;
					}
					
					m_NextSpeechTime = DateTime.Now + m_SpeechDelay;				
				}

				base.OnThink();
			}			
			
		}

		public override void GenerateLoot()
		{
			PackGem();
			PackReg( 8, 12 );
			PackScroll( 2, 7 );
			PackScroll( 2, 7 );
			//PackMagicEquipment( 1, 2, 0.25, 0.25 );
			//PackMagicEquipment( 1, 2, 0.05, 0.05 );
			PackGold( 170, 220 );
			AddLoot( LootPack.FilthyRich );

			int phrase = Utility.Random( 2 );

			switch ( phrase )
			{
				case 0: this.Say( true, "Heh! On to Davy Jones' lockarrr.." ); break;
				case 1: this.Say( true, "Sink me!" ); break;
			}

            		// Froste: 12% random IOB drop
           		/*if (0.12 > Utility.RandomDouble())
            		{
                		Item iob = Loot.RandomIOB();
                		PackItem(iob);
            		}*/

            		// pack bulk reg
			PackItem( new MandrakeRoot( Utility.RandomMinMax( 5, 10 ) ) );

			/*if (IOBRegions.GetIOBStronghold(this) == IOBAlignment)
			{
				// 30% boost to gold
				PackGold( base.GetGold()/3 );
			}*/
		}
		
		public override void Damage( int amount, Mobile from )
		{
			Mobile combatant = this.Combatant;

			if ( combatant != null && combatant.Map == this.Map && combatant.InRange( this, 8 ) )
			{
				if ( Utility.RandomBool() )
				{

					int phrase = Utility.Random( 4 );

					switch ( phrase )
					{
						case 0: this.Say( true, "Har! The mackerel wiggles!" ); break;
						case 1: this.Say( true, "Ye stink like a rotten clam! Bring it on yet!?" ); break;
						case 2: this.Say( true, "Arr, treacherous monkey!" ); break;
						case 3: this.Say( true, "Ye'll not get my swag!" ); break;
					}
					
					m_NextSpeechTime = DateTime.Now + m_SpeechDelay;				
				}
			}
				
			base.Damage( amount, from );
		}

		/*public override void GetContextMenuEntries(Mobile from, ArrayList list)
		{
			base.GetContextMenuEntries( from, list );

			for ( int i = 0; i < list.Count; ++i )
			{
				if ( list[i] is ContextMenus.PaperdollEntry )
					list.RemoveAt( i-- );
			}
		}*/

		public PirateWench( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
}
