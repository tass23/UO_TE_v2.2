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

/* Scripts/Mobiles/Monsters/Humanoid/Magic/BoneMagiLord.cs
 * ChangeLog
 *	7/26/05, erlein
 *		Automated removal of AoS resistance related function calls. 8 lines removed.
 *	4/13/05, Kit
 *		Switch to new region specific loot model
 *	12/11/04, Pix
 *		Changed ControlSlots for IOBF.
 *  11/19/04, Adam
 *		1. Create from BoneMagi.cs
 *		2. stats and loot based on lich
 */

using System;
using Server;
using Server.Items;
//using Server.Engines.IOBSystem;

namespace Server.Mobiles
{
	[CorpseName( "a bone magi lord corpse" )]
	public class BoneMagiLord : BaseCreature
	{
		[Constructable]
		public BoneMagiLord() : base( AIType.AI_Mage, /*FightMode.All |*/ FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a bone magi lord";
			Body = 148;
			BaseSoundID = 451;
           // IOBAlignment = IOBAlignment.Undead;
			//ControlSlots = 3;

			SetStr(171, 200);	
			SetDex(126, 145);	
			SetInt(276, 305);

			SetHits(103, 120);

			SetDamage(24, 26);



			SetSkill(SkillName.EvalInt, 100.0);
			SetSkill(SkillName.Magery, 70.1, 80.0);
			SetSkill(SkillName.Meditation, 85.1, 95.0);
			SetSkill(SkillName.MagicResist, 80.1, 100.0);
			SetSkill(SkillName.Tactics, 70.1, 90.0);

			Fame = 8000;
			Karma = -8000;

			VirtualArmor = 50;
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Average );
			AddLoot( LootPack.LowScrolls );
			AddLoot( LootPack.Potions );
			AddLoot( LootPack.FilthyRich );
		}
		
		public override Poison PoisonImmune{ get{ return Poison.Regular; } }

		public BoneMagiLord( Serial serial ) : base( serial )
		{
		}

		public override bool OnBeforeDeath()
		{
			PackScroll( 1, 5 );
			PackReg( 3 );
			PackItem( new Bone( Utility.Random( 10, 12 ) ) );

			PackGold(170, 220);
			//PackMagicEquipment(1, 2, 0.25, 0.25);
			//PackMagicEquipment(1, 2, 0.05, 0.05);

			// Category 2 MID
			//PackMagicItem( 1, 1, 0.05 );

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
