using System;
using Server;
using System.IO;
using System.Collections;
using Server.Misc;
using Server.Network;
using Server.Mobiles;
using Server.Targeting;
using Server.Targets;
using Server.Gumps;
using Server.Items;
using Server.Prompts;
using Server.Regions;
using Server.ContextMenus;
using System.Reflection;
using Server.Commands;
using System.Text;


namespace Server
{
	public class Beasts
	{
		public static void Initialize()
		{
			CommandSystem.Register( "Beasts", AccessLevel.Player, new CommandEventHandler( Beasts_OnCommand ) );
		}
		public static void Beasts_OnCommand( CommandEventArgs e ) 
		{
            if ( ((PlayerMobile)e.Mobile).Skills[SkillName.Forensics].Base > 80.0 && e.Mobile.Alive)
            e.Mobile.Target = new BeastTarget();
            return;
		}
		/*
		public static void Initialize()
		{
			EventSink.Speech += new SpeechEventHandler( Speech_Event );
		}
		*/

		/*
		public static void Speech_Event( SpeechEventArgs e )
		{
                  	string keyword = e.Speech;

                  	switch( keyword )
                	{
                		
               		case "beasts":
               			{
				//If you want to have this controled by a certain skill, edit the line below.
               			if ( ((PlayerMobile)e.Mobile).Skills[SkillName.Forensics].Base > 80.0 && e.Mobile.Alive)
               			e.Mobile.Target = new BeastTarget();
                  		break;
               			}
			case "Beasts":
               			{
				//If you want to have this controled by a certain skill, edit the line below.
               			if ( ((PlayerMobile)e.Mobile).Skills[SkillName.Forensics].Base > 80.0 && e.Mobile.Alive)
               			e.Mobile.Target = new BeastTarget();
                  		break;
               			}
			}
		}
		*/		
	}
}

namespace Server.Targets
{
 	public class BeastTarget : Target
 	{
 		public BeastTarget() : base( 12, false, TargetFlags.Harmful )
 		{
 		}
 		protected override void OnTarget( Mobile from, object o )
 		{
 			if ( o is Mobile )
 			{
 				Mobile targ = (Mobile)o;

				if ( !targ.InRange( from, 8 ))
 				{
 					from.SendMessage( 0x35, "You must be closer to see!" );
 					return;
 				}
 				if (targ is PlayerMobile)
 				{
					from.SendMessage( 0x35, "You can not target another player!" );
 					return;
 				}
				if ( targ is BaseVendor )
 				{
 					from.SendMessage( 0x35, "You can not target a vendor!" );
 					return;
 				}
 				else if ( targ is BaseCreature )
 				{
 					from.SendGump( new BinfoGump( from, o ) );
 				}
 			}
 			else
 			{
 				from.SendMessage( 0x35, "You can't do that" );
 			}
 		}
 	}
}

namespace Server.Gumps
{
	public class BinfoGump : Gump
	{
		private Mobile m_Mobile;
		private object m_Object;

		public object o
		{ 
			get{ return m_Object; } 
			set{ m_Object = value; } 
		}

		public BinfoGump( Mobile mobile, object o ) : base( 0, 0 )
		{
			if ( o is Mobile )
 			{
				Mobile m_Object = (Mobile)o; 
				Mobile m_this = (Mobile)mobile;

				Closable=true;
				Disposable=true;
				Dragable=true;
				Resizable=false;

				AddPage(0);

				//AddImage(277, 444, 2083);
				//AddImage(277, 375, 2082);
				//AddImage(277, 307, 2081);
				AddImage(236, 157, 1250, 573);
				AddImage(302, 8, 5010);
				//AddImage(382, 0, 4500);
				AddImage(370, 166, 9804, 573);
				AddImage(326, 155, 10460, 598);
				AddImage(450, 155, 10460, 573);
				AddImage(329, 34, 10460, 598);
				AddImage(446, 34, 10460, 573);
				AddImage(370, 482, 2642); //warning light off
				if ((m_Object.RawStr + m_Object.RawInt + m_Object.HitsMax) > (m_this.RawStr + m_this.RawInt + m_this.HitsMax + m_this.SkillsTotal))
					AddImage(370, 482, 2643); //warning light on
				//AddImage(483, 179, 4502);
				//AddImage(285, 179, 4508);
				AddImage(211, 142, 10440, 573);
				AddImage(491, 142, 10441, 573);
				//AddImage(272, 498, 10556);
				//AddImageTiled(300, 498, 221, 62, 10557);
				//AddImage(521, 498, 10558);
				AddItem(380, 86, (ShrinkTable.Lookup( m_Object )), ( m_Object.Hue ));
				AddLabel(298, 215, 49, @"Name: " + m_Object.Name );
				AddLabel(299, 235, 95, @"Strength: " + m_Object.RawStr );
				AddLabel(299, 255, 95, @"Dexterity: " + m_Object.RawDex );
				AddLabel(299, 275, 95, @"Intelligence: " + m_Object.RawInt );
				if(m_Object.Female == false)
					AddLabel(299, 295, 95, @"Sex: M");
				if(m_Object.Female == true)
					AddLabel(299, 295, 95, @"Sex: F");
				AddLabel(299, 315, 95, @"Armor: " + m_Object.VirtualArmor );
				AddLabel(299, 335, 95, @"Hits: " + m_Object.Hits );
				AddLabel(299, 355, 95, @"Mana: " + m_Object.Mana );
				AddLabel(299, 375, 95, @"Stamina: " + m_Object.Stam );
				//AddLabel(297, 412, 95, @"Weight: " + (m_Object.TotalWeight + m_Object.RawStr));
				if( Core.AOS == true )
				{
					AddImage(299, 400, 2360, 50);
					AddImage(299, 415, 2360);
					AddImage(299, 430, 2362);
					AddImage(299, 445, 2361);
					AddImage(299, 460, 2360, 17);
					AddLabel(317, 395, 95, @"" + m_Object.PhysicalResistance );
					AddLabel(317, 410, 95, @"" + m_Object.FireResistance );
					AddLabel(317, 425, 95, @"" + m_Object.ColdResistance );
					AddLabel(317, 440, 95, @"" + m_Object.PoisonResistance );
					AddLabel(317, 455, 95, @"" + m_Object.EnergyResistance );
				}
				if ((m_Object.RawStr + m_Object.RawInt + m_Object.HitsMax) > (m_this.RawStr + m_this.RawInt + m_this.HitsMax + m_this.SkillsTotal))
						AddLabel(261, 558, 95, @"This mobile seems to be stronger then you.");
				if ((m_Object.RawStr + m_Object.RawInt + m_Object.HitsMax) < (m_this.RawStr + m_this.RawInt + m_this.HitsMax + m_this.SkillsTotal))
						AddLabel(261, 558, 95, @"This mobile seems to be weaker then you.");

				AddButton(521, 522, 2640, 2641, 1, GumpButtonType.Reply, 0); //close
				AddButton(243, 522, 2640, 2641, 2, GumpButtonType.Reply, 0); //capture
					AddLabel(517, 499, 95, @"Close");
					AddLabel(230, 499, 95, @"Capture");
				AddButton(423, 406, 5567, 5568, 3, GumpButtonType.Reply, 0); //capture
					AddLabel(418, 384, 95, @"Online Bestiary");
				AddLabel(310, 525, 95, @" ~ The Expanse Bestiary ~ ");
			}
		}

		public override void OnResponse( Server.Network.NetState sender, RelayInfo info )
 		{
			Mobile from = sender.Mobile;

			PlayerMobile From = from as PlayerMobile;

			From.CloseGump( typeof( BinfoGump ) );

			if ( info.ButtonID == 1 )
			{
				From.CloseGump( typeof( BinfoGump ) );
				return;
			}
			if ( info.ButtonID == 2 )
			{
				From.Target = new BCaptureTarget();
				From.SendMessage("Please target the same creature to take its picture!");
				return;
			}
			if ( info.ButtonID == 3 )
			{
				From.LaunchBrowser( "http://www.uoexpanse.com/bestiary.php" );
				return;
			}
 		}
	}
}

