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

/* Scripts/Mobiles/Monsters/Humanoid/Magic/WraithRiderMage.cs
 * ChangeLog
 *  12/03/06 Taran Kain
 *      Set Female to false - no tranny wraith riders!
 *  7/02/06, Kit
 *		InitBody/InitOutfit additions
 *	4/13/05, Kit
 *		Switch to new region specific loot model
 *	12/11/04, Pix
 *		Changed ControlSlots for IOBF.
 *	11/19/04, Adam
 *		switch back to AIType.AI_Mage from AIType.AI_Council (Council is just too damn uber)
 *	11/19/04, Adam
 *		1. Add magic item drop (Category 4 MID)
 *		2. Add Magic equipment drop
 *	11/17/04 - Pixie
 *		Initial Version.
 */

using System;
using Server.Misc;
using Server.Network;
using System.Collections;
using Server.Items;
using Server.Targeting;
//using Server.Engines.IOBSystem;

namespace Server.Mobiles
{
	public class WraithRiderMage : BaseCreature
	{
		public override bool ShowFameTitle{ get{ return false; } }

		[Constructable]
		public WraithRiderMage():base( AIType.AI_Mage, /*FightMode.All |*/ FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			
			Title = "The Wraith Rider";
			Hue = -1;
			//IOBAlignment = IOBAlignment.Undead;
			//ControlSlots = 10;
			Female = false;
			Body = 0x190;
			Name = NameList.RandomName( "wraithrider" );

			SetStr( 356, 396 );
			SetDex( 105, 135 );
			SetInt( 530, 653 );

			SetSkill(SkillName.EvalInt, 120.1, 130.0);
			SetSkill(SkillName.Magery, 120.1, 130.0);
			SetSkill(SkillName.MagicResist, 175.2, 200.0);
			SetSkill(SkillName.Meditation, 100.1, 101.0);
			SetSkill(SkillName.Tactics, 90.1, 100.0);
			SetSkill(SkillName.Wrestling, 100.0, 110.0);
			SetSkill(SkillName.Poisoning, 100.1, 101.0);

			VirtualArmor = 60;
			SetFameLevel( 4 );
			SetKarmaLevel( 4 );
			
			BoneArms arms = new BoneArms();
			AddItem( arms );

			BoneGloves gloves = new BoneGloves();
			AddItem( gloves );

			BoneChest tunic = new BoneChest();
			AddItem( tunic );
			BoneLegs legs = new BoneLegs();
			AddItem( legs );

			BoneHelm helm = new BoneHelm();
			AddItem( helm );

			AddItem( new Shoes() );

			//InitBody();
			//InitOutfit();

			new HellSteed().Rider = this;
		}

		public override bool AlwaysMurderer{ get{ return true; } }
		public override bool Unprovokable{ get{ return true; } }

		[CommandProperty( AccessLevel.GameMaster )]
		public override int HitsMax { get { return 753; } }

		/*public override void InitBody()
		{
            Female = false;
			Body = 0x190;
			Name = NameList.RandomName( "wraithrider" );
		}
		public override void InitOutfit()
		{
			WipeLayers();
			BoneArms arms = new BoneArms();
			AddItem( arms );

			BoneGloves gloves = new BoneGloves();
			AddItem( gloves );

			BoneChest tunic = new BoneChest();
			AddItem( tunic );
			BoneLegs legs = new BoneLegs();
			AddItem( legs );

			BoneHelm helm = new BoneHelm();
			AddItem( helm );

			AddItem( new Shoes() );
			
		}*/
		
		public WraithRiderMage( Serial serial ) : base( serial )
		{
		}

		public override bool OnBeforeDeath()
		{
			BoneMagi rm = new BoneMagi();

			rm.Team = this.Team;
			rm.MoveToWorld( this.Location, this.Map );

			Effects.SendLocationEffect( Location,Map, 0x3709, 13, 0x3B2, 0 );

			Container bag = new Bag();

			switch ( Utility.Random( 9 ))
			{
				case 0: bag.DropItem( new Amber() ); break;
				case 1: bag.DropItem( new Amethyst() ); break;
				case 2: bag.DropItem( new Citrine() ); break;
				case 3: bag.DropItem( new Diamond() ); break;
				case 4: bag.DropItem( new Emerald() ); break;
				case 5: bag.DropItem( new Ruby() ); break;
				case 6: bag.DropItem( new Sapphire() ); break;
				case 7: bag.DropItem( new StarSapphire() ); break;
				case 8: bag.DropItem( new Tourmaline() ); break;
			}

			switch ( Utility.Random( 8 ))
			{
				case 0: bag.DropItem( new SpidersSilk( 3 ) ); break;
				case 1: bag.DropItem( new BlackPearl( 3 ) ); break;
				case 2: bag.DropItem( new Bloodmoss( 3 ) ); break;
				case 3: bag.DropItem( new Garlic( 3 ) ); break;
				case 4: bag.DropItem( new MandrakeRoot( 3 ) ); break;
				case 5: bag.DropItem( new Nightshade( 3 ) ); break;
				case 6: bag.DropItem( new SulfurousAsh( 3 ) ); break;
				case 7: bag.DropItem( new Ginseng( 3 ) ); break;
			}

			/*if (0.12 > Utility.RandomDouble())
			{
				Item iob = Loot.RandomIOB();
				bag.DropItem( iob );
			}*/

			bag.DropItem( new Gold( 1000, 1500 ));
			
			/*if (IOBRegions.GetIOBStronghold(this) == IOBAlignment)
			{
				// 30% boost to gold
				PackGold( base.GetGold()/3 );
			}*/

			// Category 4 MID
			/*rm.PackMagicItem(2, 3, 0.10);
			rm.PackMagicItem(2, 3, 0.05);
			rm.PackMagicItem(2, 3, 0.02);

			rm.PackMagicEquipment(2, 3, 0.60, 0.60);
			rm.PackMagicEquipment(2, 3, 0.25, 0.25);*/

			rm.AddItem( bag );

			this.Delete();

			return false;
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