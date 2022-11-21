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

/* Scripts/Mobiles/Monsters/Humanoid/Melee/BrigandArcher.cs
 * ChangeLog
 *  07/02/06, Kit
 *		InitBody/InitOutfit additions, changed fight range to 6! archers arent suppose to stand
 *		toe to toe.
 *  08/29/05 TK
 *		Changed AIType to Archer
 *	7/26/05, erlein
 *		Automated removal of AoS resistance related function calls. 1 lines removed.
 *	4/13/05, Kit
 *		Switch to new region specific loot model
 *	2/4/05, Adam
 *		Hookup PowderOfTranslocation drop rates to the CoreManagementConsole
 *	1/25/05, Adam
 *		Add PowderOfTranslocation as region specific loot 
 *		Brigands are the only ones that carry this loot.
 *	12/30/04, Adam
 *		Created by Adam.
 *		Cleanup name management, make use of Titles
 *			Show title when clicked = false
 */

using System;
using System.Collections;
using Server.Items;
using Server.ContextMenus;
using Server.Misc;
using Server.Network;
//using Server.Engines.IOBSystem;

namespace Server.Mobiles
{
	public class BrigandArcher : BaseCreature
	{
		[Constructable]
		public BrigandArcher() : base( AIType.AI_Archer, FightMode.Weakest, 16, 1, 0.1, 0.1 )
		{
			//CanRun = true;
			//UsesBandages = true;
			SpeechHue = Utility.RandomDyedHue();
			Title = "the brigand";
			Hue = Utility.RandomSkinHue();
			//IOBAlignment = IOBAlignment.Brigand;
			//ControlSlots = 3;
			

			if ( this.Female = Utility.RandomBool() )
			{
				Body = 0x191;
				Name = NameList.RandomName( "female" );
				AddItem( new Skirt( Utility.RandomNeutralHue() ) );
			}
			else
			{
				Body = 0x190;
				Name = NameList.RandomName( "male" );
				AddItem( new ShortPants( Utility.RandomNeutralHue() ) );
			}

			SetStr( 146, 200 );
			SetDex( 130, 170 );
			SetInt( 51, 65 );

			SetDamage( 23, 27 );

			SetSkill( SkillName.Archery, 90.0, 100.5 );
			SetSkill( SkillName.Macing, 60.0, 82.5 );
			SetSkill( SkillName.Poisoning, 60.0, 82.5 );
			SetSkill( SkillName.MagicResist, 57.5, 80.0 );
			SetSkill( SkillName.Anatomy, 80.0, 92.5 );
			SetSkill( SkillName.Tactics, 80.0, 92.5 );

			AddItem( new Boots( Utility.RandomNeutralHue() ) );
			AddItem( new FancyShirt());
			AddItem( new Bandana());
			AddItem( new Bow() );

			Fame = 1500;
			Karma = -1500;
			Utility.AssignRandomHair( this );

			PackItem( new Bandage( Utility.RandomMinMax( 1, 15 ) ) );
			
		}

		public override bool AlwaysMurderer{ get{ return true; } }
		public override bool ShowFameTitle{ get{ return false; } }
		public override bool CanRummageCorpses{ get{ return true; } }
		public override bool ClickTitle { get { return false; } }

		//public override bool CanBandage{ get{ return true; } }
		//public override TimeSpan BandageDelay{ get{ return TimeSpan.FromSeconds( Utility.RandomMinMax( 10, 11 ) ); } }

		public BrigandArcher( Serial serial ) : base( serial )
		{
		}

		/*public override void InitBody()
		{
			if ( this.Female = Utility.RandomBool() )
			{
				Body = 0x191;
				Name = NameList.RandomName( "female" );
				
			}
			else
			{
				Body = 0x190;
				Name = NameList.RandomName( "male" );
			}
		}
		public override void InitOutfit()
		{
			WipeLayers();
			Item hair = new Item( Utility.RandomList( 0x203B, 0x2049, 0x2048, 0x204A ) );
			hair.Hue = Utility.RandomNondyedHue();
			hair.Layer = Layer.Hair;
			hair.Movable = false;
			AddItem( hair );

			AddItem( new Boots( Utility.RandomNeutralHue() ) );
			AddItem( new FancyShirt());
			AddItem( new Bandana() );
			AddItem( new Bow() ); 

			if(Female)
				AddItem( new Skirt( Utility.RandomNeutralHue() ) );
			else
				AddItem( new ShortPants( Utility.RandomNeutralHue() ) );

		}*/

		public override void GenerateLoot()
		{
			AddLoot( LootPack.FilthyRich );
			PackGold( 170, 220 );

			PackItem( new Arrow( Utility.RandomMinMax( 20, 30 ) ) );

            		// Froste: 12% random IOB drop
            		/*if (0.12 > Utility.RandomDouble())
            		{
                		Item iob = Loot.RandomIOB();
                		PackItem(iob);
            		}*/

			/*if (IOBRegions.GetIOBStronghold(this) == IOBAlignment)
			{
				// 30% boost to gold
				PackGold( base.GetGold()/3 );
				
				// chance at powder of translocation
				if (CoreAI.PowderOfTranslocationAvail > Utility.RandomDouble())			
					PackItem(new PowderOfTranslocation(Utility.RandomMinMax( 3, 10 )));
			}*/
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
