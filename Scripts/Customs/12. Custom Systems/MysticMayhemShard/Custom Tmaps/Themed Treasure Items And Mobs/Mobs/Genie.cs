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

/* Scripts/Mobiles/Monsters/Elemental/Magic/Genie.cs
 * ChangeLog
 *	07/23/08, weaver
 *		Automated IPooledEnumerable optimizations. 1 loops updated.
 * 07/23/06, Kit
 *		Add Dispell levels, genies now dispelable by targeted mass disepll.
 * 07/02/06, Kit
 *		InitBody/InitOutfit additions
 * 05/06/06, Kit
 *		Added constructor to allow spawning of genies via [add and [tile
 * 11/04/05, Kit
 *		Added check to prevent teleporting/following Inmates, thus preventing from accidently teleporting to Angel
 *		Island, added title "the genie"
 *	7/26/05, erlein
 *		Automated removal of AoS resistance related function calls. 8 lines removed.
 * 6/04/05, Kit
 *		Made Bard Immune, increased active speed, gave scimitar
 * 4/09/05, Kitaras
 *	added target switching AI
 * 3/30/05, Kitaras
 *	Initial Creation
 */

using System;
using Server;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "a genie corpse" )]
	public class Genie : BaseCreature
	{
		public override double DispelDifficulty{ get{ return 125.0; } }
		public override double DispelFocus{ get{ return 45.0; } }
		private Mobile m_Target;

		[Constructable]
		public Genie() : this(null)
		{
		}

		[Constructable]
		public Genie (Mobile target) : base( AIType.AI_Mage, FightMode.Aggressor, 10, 1, 0.13, 0.25)
		{
			m_Target = target;
			//BardImmune = true;
						
			SpeechHue = Utility.RandomDyedHue();
			Title = "the genie";
			
			if (Female = Utility.RandomBool())
			{
				Body = 184;
				Name = NameList.RandomName("genie_female");
			}
			else
			{
				Body = 183;
				Name = NameList.RandomName("genie_male");
			}
			
			Scimitar sword = new Scimitar();
			sword.Quality = WeaponQuality.Exceptional;
			sword.Movable = false;
			AddItem( sword );

			//Item hair = new KrisnaHair();
			Item hair = new Item( Utility.RandomList( 0x203B, 0x203C, 0x203D, 0x2044, 0x2045, 0x2047, 0x2049, 0x204A ) );
			hair.Hue = 1263;
			hair.Layer = Layer.Hair;
			hair.Movable = false;
			AddItem(hair);

			BoneArms arms = new BoneArms();
			arms.Name = "magical bindings";
			arms.Hue = 1706;
			arms.LootType = LootType.Newbied;
			AddItem( arms );
			
			GoldNecklace necklace = new GoldNecklace();
			necklace.Name = "magical collar";
			necklace.Hue = 1706;
			necklace.LootType = LootType.Newbied;
			AddItem( necklace );
			
			Hue = 501;
			
			SetStr( 428 );
			SetDex( 80 );
			SetInt( 597 );

			SetHits(250, 500);

			SetDamage(14, 18);

			SetSkill( SkillName.EvalInt, 120.1, 130.0 );
			SetSkill(SkillName.Magery, 90.1, 100.1);
			SetSkill(SkillName.Meditation, 100.1, 100.1);
			SetSkill(SkillName.MagicResist, 90.5, 100.0);
			SetSkill(SkillName.Wrestling, 110.1, 120.0);
			SetSkill(SkillName.Swords, 90.1, 100.0);
			SetSkill(SkillName.Tactics, 85.1, 100.0);

			Fame = 15000;
			Karma = -15000;

			//InitBody();
			//InitOutfit();

			VirtualArmor = 56;
		}

		public override bool AlwaysMurderer{ get{ return true; } }
		public override bool ShowFameTitle{ get{ return false; } }
		public override bool AutoDispel{ get{ return true; } }

		/*public override void InitBody()
		{
			if (Female = Utility.RandomBool())
			{
				Body = 184;
				Name = NameList.RandomName("genie_female");
			}
			else
			{
				Body = 183;
				Name = NameList.RandomName("genie_male");
			}
		}
		public override void InitOutfit()
		{
			WipeLayers();

			Scimitar sword = new Scimitar();
			sword.Quality = WeaponQuality.Exceptional;
			sword.Movable = false;
			AddItem( sword );

			Item hair = new KrisnaHair();
			hair.Hue = 1263;
			hair.Layer = Layer.Hair;
			hair.Movable = false;
			AddItem(hair);

			BoneArms arms = new BoneArms();
			arms.Name = "magical bindings";
			arms.Hue = 1706;
			arms.LootType = LootType.Newbied;
			AddItem( arms );
			
			GoldNecklace necklace = new GoldNecklace();
			necklace.Name = "magical collar";
			necklace.Hue = 1706;
			necklace.LootType = LootType.Newbied;
			AddItem( necklace );
			
		}*/
		public override void OnThink()
		{
			//teleport to player if they arent pass a 15 tile range
			//make sure there not a inmate and thus on Angel Island.
			if ( m_Target != null && m_Target.Alive && !Paralyzed && CanSee( m_Target) && ( (m_Target is PlayerMobile) /*(((PlayerMobile)m_Target).Inmate != true)*/))
			{
				if (!InRange( m_Target, 15 ) )
				{
					Map fromMap = Map;
					Point3D from = Location;

					Map toMap = m_Target.Map;
					Point3D to = m_Target.Location;

					if ( toMap != null )
					{
						for ( int i = 0; i < 5; ++i )
						{
							Point3D loc = new Point3D( to.X - 4 + Utility.Random( 9 ), to.Y - 4 + Utility.Random( 9 ), to.Z );

							if ( toMap.CanSpawnMobile( loc ) )
							{
								to = loc;
								break;
							}
							else
							{
								loc.Z = toMap.GetAverageZ( loc.X, loc.Y );

								if ( toMap.CanSpawnMobile( loc ) )
								{
									to = loc;
									break;
								}
							}
						}
					}

					Map = toMap;
					Location = to;
			
					
				Effects.SendLocationEffect( new Point3D( from.X + 1, from.Y, from.Z ), fromMap, 0x3728, 13 );
				Effects.SendLocationEffect( new Point3D( from.X + 1, from.Y, from.Z - 4 ), fromMap, 0x3728, 13 );
				Effects.SendLocationEffect( new Point3D( from.X, from.Y + 1, from.Z + 4 ), fromMap, 0x3728, 13 );
				Effects.SendLocationEffect( new Point3D( from.X, from.Y + 1, from.Z ), fromMap, 0x3728, 13 );
				Effects.SendLocationEffect( new Point3D( from.X, from.Y + 1, from.Z - 4 ), fromMap, 0x3728, 13 );
				Effects.SendLocationEffect( new Point3D( from.X + 1, from.Y + 1, from.Z + 11 ), fromMap, 0x3728, 13 );
				Effects.SendLocationEffect( new Point3D( from.X + 1, from.Y + 1, from.Z + 7 ), fromMap, 0x3728, 13 );
				Effects.SendLocationEffect( new Point3D( from.X + 1, from.Y + 1, from.Z + 3 ), fromMap, 0x3728, 13 );
				Effects.SendLocationEffect( new Point3D( from.X + 1, from.Y + 1, from.Z - 1 ), fromMap, 0x3728, 13 );
				Effects.SendLocationEffect( new Point3D( from.X + 1, from.Y, from.Z + 4 ), fromMap, 0x3728, 13 );
				Effects.SendLocationEffect( new Point3D( to.X + 1, to.Y, to.Z ), toMap, 0x3728, 13 );
				Effects.SendLocationEffect( new Point3D( to.X + 1, to.Y, to.Z - 4 ), toMap, 0x3728, 13 );
				Effects.SendLocationEffect( new Point3D( to.X, to.Y + 1, to.Z + 4 ), toMap, 0x3728, 13 );
				Effects.SendLocationEffect( new Point3D( to.X, to.Y + 1, to.Z ), toMap, 0x3728, 13 );
				Effects.SendLocationEffect( new Point3D( to.X, to.Y + 1, to.Z - 4 ), toMap, 0x3728, 13 );
				Effects.SendLocationEffect( new Point3D( to.X + 1, to.Y + 1, to.Z + 11 ), toMap, 0x3728, 13 );
				Effects.SendLocationEffect( new Point3D( to.X + 1, to.Y + 1, to.Z + 7 ), toMap, 0x3728, 13 );
				Effects.SendLocationEffect( new Point3D( to.X + 1, to.Y + 1, to.Z + 3 ), toMap, 0x3728, 13 );
				Effects.SendLocationEffect( new Point3D( to.X + 1, to.Y + 1, to.Z - 1 ), toMap, 0x3728, 13 );
				
				PlaySound( 0x37D );
			
				this.Say("You didnt think you could escape so easily did you");
			}

			Combatant = m_Target;
			FocusMob = m_Target;

			if ( AIObject != null )
				AIObject.Action = ActionType.Combat;

			base.OnThink();
		}

		if (Combatant == null || !CanSee( Combatant ) )
		{	
	
			IPooledEnumerable eable = GetMobilesInRange( 12 );
			foreach ( Mobile m in eable)
			{
				if(m != null && m is PlayerMobile && CanSee( m )) 
				m_Target = m;
			}
			eable.Free();
	
			base.OnThink();
		}
	}

	
		public Genie( Serial serial ) : base( serial )
		{
		}

		public override void GenerateLoot()
		{
			//PackMagicEquipment(1, 3, 0.80, 0.80);
			//PackMagicEquipment(1, 3, 0.10, 0.10);
			PackGold(600, 700);

			GenieBottle lamp = new GenieBottle(true);
			AddItem( lamp );

			// Category 3 MID
			//PackMagicItem(1, 2, 0.10);
			//PackMagicItem(1, 2, 0.05);
		
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
