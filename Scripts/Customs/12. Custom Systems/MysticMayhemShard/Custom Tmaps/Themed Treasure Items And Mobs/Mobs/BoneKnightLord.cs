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

/* Scripts/Mobiles/Monsters/Humanoid/Melee/BoneKnightLord.cs
 * ChangeLog
 *	7/26/05, erlein
 *		Automated removal of AoS resistance related function calls. 7 lines removed.
 *	4/13/05, Kit
 *		Switch to new region specific loot model
 *	12/11/04, Pix
 *		Changed ControlSlots for IOBF.
 *  11/19/04, Adam
 *		1. Create from BoneKnight.cs
 *		2. stats and loot based on a scaled down Executioner
 */

using System;
using System.Collections;
using Server.Items;
using Server.Targeting;
//using Server.Engines.IOBSystem;

namespace Server.Mobiles
{
	[CorpseName( "a bone knight lord corpse" )]
	public class BoneKnightLord : BaseCreature
	{
		[Constructable]
		public BoneKnightLord() : base( AIType.AI_Melee, /*FightMode.All |*/ FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a bone knight lord";
			Body = 57;
			BaseSoundID = 451;
            //IOBAlignment = IOBAlignment.Undead;
			//ControlSlots = 3;

            SetStr( 196 + 20, 250 + 20);
			SetDex( 76, 95 );
			SetInt( 36, 60 );

			SetHits(118 + 20, 150 + 20);

			SetDamage( 8 + 1, 18 + 2);



			SetSkill( SkillName.MagicResist, 65.1, 80.0 );
			SetSkill( SkillName.Tactics, 85.1, 100.0 );
			SetSkill( SkillName.Wrestling, 85.1, 95.0 );

			Fame = 3000;
			Karma = -3000;

			VirtualArmor = 40;
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Average );
			AddLoot( LootPack.FilthyRich );
		}

		public BoneKnightLord( Serial serial ) : base( serial )
		{
		}

		public override bool OnBeforeDeath()
		{
			switch ( Utility.Random( 6 ) )
			{
				case 0: PackItem( new PlateArms() ); break;
				case 1: PackItem( new PlateChest() ); break;
				case 2: PackItem( new PlateGloves() ); break;
				case 3: PackItem( new PlateGorget() ); break;
				case 4: PackItem( new PlateLegs() ); break;
				case 5: PackItem( new PlateHelm() ); break;
			}

			PackItem( new Scimitar() );
			PackItem( new WoodenShield() );
			PackItem( new Bone( Utility.Random( 9, 16 ) ) );

			PackGold(200, 400);
			// Category 2 MID
			//PackMagicItem(1, 1, 0.05);

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
			}*/

            		return base.OnBeforeDeath();
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
