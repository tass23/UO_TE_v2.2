//   RunUO script: Skill Ball
//   Copyright (c) 2003 by Wulf C. Krueger <wk@@mailstation.de>
//
//   This script is free software; you can redistribute it and/or modify
//   it under the terms of the GNU General Public License as published by
//   the Free Software Foundation; version 2 of the License applies.
//
//   This program is distributed in the hope that it will be useful,
//   but WITHOUT ANY WARRANTY; without even the implied warranty of
//   MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//   GNU General Public License for more details.
//
//   Please do NOT remove or change this header.
//
//


//  Made into a BonusSkillBall25 by X-SirSly-X
//  www.LandOfObsidian.com

//  Revised by: Lucid Nagual (Admin of "The Conjuring")
//  Added memory & save abilities to the skill bonus so that the
//  skill bonus can be split between multiple skills and max'd
//  to the skill cap allowance.

//  Re-Revised by: Murzin
//  Eliminated a bunch of code and fixed skill bonus issue.


using System;
using Server.Network;
using Server.Items;
using Server.Gumps;

namespace Server.Items
{
	public class BonusSkillBall25 : Item
	{
		private int m_SkillBonus = 25;
		private int m_SkillBallCap = 100;
		private string m_BaseName = "a skill ball +";
		public bool GumpOpen = false;
		
		[CommandProperty( AccessLevel.Administrator )]
		public int SkillBonus
		{
			get { return m_SkillBonus; }
			set {
				m_SkillBonus = value;
				this.Name = m_BaseName + m_SkillBonus.ToString();
			}
		}
		
		[CommandProperty( AccessLevel.Administrator )]
		public int SkillBallCap 
		{
			get { return m_SkillBallCap; }
			set { m_SkillBallCap = value; }
		}
		
		[Constructable]
		public BonusSkillBall25( int SkillBonus, int SkillBallCap ) : base( 6249 )
		{
			m_SkillBonus = SkillBonus;
			m_SkillBallCap = SkillBallCap;
			Name = m_BaseName + SkillBonus.ToString();
			Movable = false;
			LootType = LootType.Blessed;
		}
		
		[Constructable]
		public BonusSkillBall25() : base( 6249 )
		{
			Name = m_BaseName + SkillBonus.ToString();
			Movable = false;
		}
		
		public BonusSkillBall25( Serial serial ) : base( serial )
		{
		}
		
		public override void OnDoubleClick( Mobile from )
		{
			if ( ( this.SkillBonus == 0 ) && ( from.AccessLevel < AccessLevel.GameMaster ) ) {
				from.SendMessage("This Skill Ball isn't charged. Please page for a GM." );
				return;
			}
			else if ( ( from.AccessLevel >= AccessLevel.Administrator ) && (this.SkillBonus == 0) ) {
				from.SendGump( new PropertiesGump( from, this ) );
				return;
			}
			else if ( this.SkillBonus < 0 )
			{
				from.SendMessage("You have used up your skillball.");
				this.Delete();
			}
			
			if ( !IsChildOf( from.Backpack ) )
				from.SendLocalizedMessage( 1042001 ); // That must be in your pack for you to use it.
			else if (!GumpOpen) 
			{
				GumpOpen = true;
				from.SendGump( new BonusSkillBall25Gump( from, this ) );
			}
			else if (GumpOpen)
				from.SendMessage("You're already using the ball.");
		}
		
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 1 ); // version

			writer.Write( m_SkillBallCap );
			writer.Write( m_SkillBonus );
		}
		
		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			
			int version = reader.ReadInt();
			
			switch (version) 
			{
					case 1 : {
						m_SkillBallCap = reader.ReadInt();
						goto case 0;
					}

					case 0 : {
						m_SkillBonus = reader.ReadInt();
						break;
					}
			}
		}
	}
}


namespace Server.Gumps
{
	public class BonusSkillBall25Gump : Gump
	{
		private Skill m_Skill;
		private BonusSkillBall25 m_skb;
		
		public BonusSkillBall25Gump ( Mobile from, BonusSkillBall25 skb ) : base ( 20, 30 )
		{
			int FieldsPerPage = 14;
			m_skb = skb;
			
			AddPage ( 0 );
			AddBackground( 0, 0, 260, 351, 5054 );
			
			AddImageTiled( 10, 10, 240, 23, 0x52 );
			AddImageTiled( 11, 11, 238, 21, 0xBBC );
			
			AddLabel( 65, 11, 0, "Skills you can raise" );
			
			AddPage( 1 );
			
			int page = 1;
			int index = 0;
			
			Skills skills = from.Skills;
			
			int number;
			if ( Core.AOS )
				number = 0;
			else
				number = 3;
			
			for ( int i = 0; i < ( skills.Length - number ); ++i ) {
				if ( index >= FieldsPerPage ) {
					AddButton( 231, 13, 0x15E1, 0x15E5, 0, GumpButtonType.Page, page + 1 );
					
					++page;
					index = 0;
					
					AddPage( page );
					
					AddButton( 213, 13, 0x15E3, 0x15E7, 0, GumpButtonType.Page, page - 1 );
				}
				
				Skill skill = skills[i];
				
				//if ( (skill.Base + m_skb.SkillBonus) <= 200 ) 
				if ( m_skb.SkillBonus >= 0 )
				{
					AddImageTiled( 10, 32 + (index * 22), 240, 23, 0x52 );
					AddImageTiled( 11, 33 + (index * 22), 238, 21, 0xBBC );
					
					AddLabelCropped( 13, 33 + (index * 22), 150, 21, 0, skill.Name );
					AddImageTiled( 180, 34 + (index * 22), 50, 19, 0x52 );
					AddImageTiled( 181, 35 + (index * 22), 48, 17, 0xBBC );
					AddLabelCropped( 182, 35 + (index * 22), 234, 21, 0, skill.Base.ToString( "F1" ) );
					
					if ( from.AccessLevel >= AccessLevel.Player )
						AddButton( 231, 35 + (index * 22), 0x15E1, 0x15E5, i + 1, GumpButtonType.Reply, 0 );
					else
						AddImage( 231, 35 + (index * 22), 0x2622 );
					
					++index;
				}
			}
		}
		
		public override void OnResponse( NetState state, RelayInfo info )
		{
			Mobile from = state.Mobile;
			Container pack = from.Backpack;
			
			if ( (from == null) || (m_skb.Deleted) )
				return;
			
			m_skb.GumpOpen = false;
			
			if ( info.ButtonID > 0 ) {
				m_Skill = from.Skills[(info.ButtonID-1)];
				
				if ( m_Skill == null )
					return;

				while ( m_skb != null && m_skb.SkillBonus > 0 && ( m_skb.SkillBallCap - m_Skill.Base ) > 0 )
				{
					m_skb.SkillBonus -=1;
					m_Skill.Base +=1;
					if ( m_skb.SkillBonus < 1 ) { m_skb.Consume(); }
				}
			}
		}
	}
}
