////////////////////////////////////////////////////////////////////
///          FragDoctor Scripting Group Presents:                ///
///      Skill Ball with Skill Selection Gump, by Rasmenar       ///
///      Use: Set Seven skills at 100. Easily change the         ///
///      number of skills allowed, and the amount they are       ///
///      set at. Sections you may want to change will be         ///
///                          noted.                              ///
////////////////////////////////////////////////////////////////////

using System;
using Server;
using Server.Network;
using Server.Mobiles;
using Server.Items;
using Server.Gumps;
using Server.Misc;

namespace Server
{
	public class SkillPickGump : Gump
	{
///////////////////////////////////////////////////////////////////////
////////////////////If you want your players to be able to pick more
////////////////////than 7 skills, increase this number.

		private int switches = 3;

///////////////////This number is the value selected skills are
///////////////////set to.

		private double val = 100;

///////////////////////////////////////////////////////////////////////
		public SkillPickGump()
			: base( 0, 0 )
		{
			this.Closable=false;
			this.Disposable=true;
			this.Dragable=true;
			this.Resizable=false;

			this.AddPage(0);
			this.AddBackground(39, 33, 219, 370, 5054);
			this.AddLabel(67, 41, 32, @"Please select your skills");
			this.AddLabel(56, 317, 32, @"Warning: All skills will be");
			this.AddLabel(56, 333, 32, @"set to 0, except those you");
			this.AddLabel(56, 349, 32, @"have chosen to be 100.");
			this.AddButton(54, 368, 247, 248, (int)Buttons.FinishButton, GumpButtonType.Reply, 0);
			this.AddBackground(52, 60, 191, 253, 9350);
			this.AddPage(1);

			this.AddButton(187, 374, 2469, 2470, (int)Buttons.GoPage3, GumpButtonType.Page, 2);
			this.AddCheck(55, 65, 210, 211, false, 40);
			this.AddCheck(55, 90, 210, 211, false, 2);
			this.AddCheck(55, 115, 210, 211, false, 39);
			this.AddCheck(55, 140, 210, 211, false, 36);
			this.AddCheck(55, 165, 210, 211, false, 5);
			this.AddCheck(55, 190, 210, 211, false, 37);
			this.AddCheck(55, 215, 210, 211, false, 38);
			this.AddCheck(55, 240, 210, 211, false, 7);
			this.AddCheck(55, 265, 210, 211, false, 41);
			this.AddCheck(55, 290, 210, 211, false, 8);
			this.AddLabel(80, 65, 0, @"Tactics");
			this.AddLabel(80, 90, 0, @"Anatomy");
			this.AddLabel(80, 115, 0, @"Swordsmanship");
			this.AddLabel(80, 140, 0, @"Fencing");
			this.AddLabel(80, 165, 0, @"Archery");
			this.AddLabel(80, 190, 0, @"Macefighting");
			this.AddLabel(80, 215, 0, @"Parry");
			this.AddLabel(80, 240, 0, @"Arms Lore");
			this.AddLabel(80, 265, 0, @"Wrestling");
			this.AddLabel(80, 290, 0, @"Bushido");

			this.AddPage(2);
			this.AddButton(187, 374, 2469, 2470, (int)Buttons.GoPage4, GumpButtonType.Page, 3);
			this.AddCheck(55, 65, 210, 211, false, 9);
			this.AddCheck(55, 90, 210, 211, false, 13);
			this.AddCheck(55, 115, 210, 211, false, 34);
			this.AddCheck(55, 140, 210, 211, false, 33);
			this.AddCheck(55, 165, 210, 211, false, 15);
			this.AddCheck(55, 190, 210, 211, false, 14);
			this.AddCheck(55, 215, 210, 211, false, 12);
			this.AddCheck(55, 240, 210, 211, false, 42);
			this.AddCheck(55, 265, 210, 211, false, 23);
			this.AddCheck(55, 290, 210, 211, false, 20);

			this.AddLabel(80, 65, 0, @"Blacksmithing");
			this.AddLabel(80, 90, 0, @"Carpentry");
			this.AddLabel(80, 115, 0, @"Tinkering");
			this.AddLabel(80, 140, 0, @"Tailoring");
			this.AddLabel(80, 165, 0, @"Fishing");
			this.AddLabel(80, 190, 0, @"Cooking");
			this.AddLabel(80, 215, 0, @"Fletching");
			this.AddLabel(80, 240, 0, @"Cartography");
			this.AddLabel(80, 265, 0, @"Mining");
			this.AddLabel(80, 290, 0, @"Lumberjacking");
			this.AddButton(122, 369, 2322, 2323, (int)Buttons.BackPage2, GumpButtonType.Page, 1);

			this.AddPage(3);
			this.AddButton(187, 374, 2469, 2470, (int)Buttons.GoPage5, GumpButtonType.Page, 4);
			this.AddCheck(55, 65, 210, 211, false, 1);
			this.AddCheck(55, 90, 210, 211, false, 44);
			this.AddCheck(55, 115, 210, 211, false, 30);
			this.AddCheck(55, 140, 210, 211, false, 21);
			this.AddCheck(55, 165, 210, 211, false, 25);
			this.AddCheck(55, 190, 210, 211, false, 48);
			this.AddCheck(55, 215, 210, 211, false, 50);
			this.AddCheck(55, 240, 210, 211, false, 10);
			this.AddCheck(55, 265, 210, 211, false, 16);
			this.AddCheck(55, 290, 210, 211, false, 22);
			this.AddLabel(80, 65, 0, @"Alchemy");
			this.AddLabel(80, 90, 0, @"Inscription");
			this.AddLabel(80, 115, 0, @"Spellweaving");
			this.AddLabel(80, 140, 0, @"Magery");
			this.AddLabel(80, 165, 0, @"Necromancy");
			this.AddLabel(80, 190, 0, @"Spirit Speak");
			this.AddLabel(80, 215, 0, @"Evaluating Intelligence");
			this.AddLabel(80, 240, 0, @"Chivalry");
			this.AddLabel(80, 265, 0, @"Focus");
			this.AddLabel(80, 290, 0, @"Meditation");
			this.AddButton(122, 369, 2322, 2323, (int)Buttons.BackPage3, GumpButtonType.Page, 2);

			this.AddPage(4);
			this.AddButton(187, 374, 2469, 2470, (int)Buttons.GoPage6, GumpButtonType.Page, 5);
			this.AddCheck(55, 65, 210, 211, false, 55);
			this.AddCheck(55, 90, 210, 211, false, 32);
			this.AddCheck(55, 115, 210, 211, false, 29);
			this.AddCheck(55, 140, 210, 211, false, 31);
			this.AddCheck(55, 165, 210, 211, false, 19);
			this.AddCheck(55, 190, 210, 211, false, 26);
			this.AddCheck(55, 215, 210, 211, false, 43);
			this.AddCheck(55, 240, 210, 211, false, 27);
			this.AddCheck(55, 265, 210, 211, false, 49);
			this.AddCheck(55, 290, 210, 211, false, 46);

			this.AddLabel(80, 65, 0, @"Hiding");
			this.AddLabel(80, 90, 0, @"Stealth");
			this.AddLabel(80, 115, 0, @"Snooping");
			this.AddLabel(80, 140, 0, @"Stealing");
			this.AddLabel(80, 165, 0, @"Lockpicking");
			this.AddLabel(80, 190, 0, @"Ninjitsu");
			this.AddLabel(80, 215, 0, @"Detecting Hidden");
			this.AddLabel(80, 240, 0, @"Remove Trap");
			this.AddLabel(80, 265, 0, @"Tracking");
			this.AddLabel(80, 290, 0, @"Poisoning");
			this.AddButton(122, 369, 2322, 2323, (int)Buttons.BackPage4, GumpButtonType.Page, 3);

			this.AddPage(5);
			this.AddButton(187, 374, 2469, 2470, (int)Buttons.GoPage7, GumpButtonType.Page, 6);
			this.AddCheck(55, 65, 210, 211, false, 4);
			this.AddCheck(55, 90, 210, 211, false, 3);
			this.AddCheck(55, 115, 210, 211, false, 11);
			this.AddCheck(55, 140, 210, 211, false, 24);
			this.AddCheck(55, 165, 210, 211, false, 54);
			this.AddCheck(55, 190, 210, 211, false, 47);
			this.AddCheck(55, 215, 210, 211, false, 45);
			this.AddCheck(55, 240, 210, 211, false, 52);
			this.AddCheck(55, 265, 210, 211, false, 53);
			this.AddCheck(55, 290, 210, 211, false, 51);

			this.AddLabel(80, 65, 0, @"Animal Taming");
			this.AddLabel(80, 90, 0, @"Animal Lore");
			this.AddLabel(80, 115, 0, @"Camping");
			this.AddLabel(80, 140, 0, @"Musicianship");
			this.AddLabel(80, 165, 0, @"Discordance");
			this.AddLabel(80, 190, 0, @"Provocation");
			this.AddLabel(80, 215, 0, @"Peacemaking");
			this.AddLabel(80, 240, 0, @"Item Identification");
			this.AddLabel(80, 265, 0, @"Taste Identification");
			this.AddLabel(80, 290, 0, @"Foresic Evaluation");
			this.AddButton(122, 369, 2322, 2323, (int)Buttons.BackPage5, GumpButtonType.Page, 4);

			this.AddPage(6);
			this.AddCheck(55, 65, 210, 211, false, 7);
			this.AddCheck(55, 90, 210, 211, false, 17);
			this.AddCheck(55, 115, 210, 211, false, 18);
			this.AddCheck(55, 140, 210, 211, false, 28);
			this.AddCheck(55, 165, 210, 211, false, 35);

			this.AddLabel(80, 65, 0, @"Begging");
			this.AddLabel(80, 90, 0, @"Healing");
			this.AddLabel(80, 115, 0, @"Herding");
			this.AddLabel(80, 140, 0, @"Resisting Spells");
			this.AddLabel(80, 165, 0, @"Veterinary");
			this.AddButton(122, 369, 2322, 2323, (int)Buttons.CopyofBackPage6, GumpButtonType.Page, 5);

		}
		
		public enum Buttons
		{
			Close,
			FinishButton,
			GoPage3,
			GoPage4,
			BackPage2,
			GoPage5,
			BackPage3,
			GoPage6,
			BackPage4,
			GoPage7,
			BackPage5,
			CopyofBackPage6,
		}
		public override void OnResponse( NetState state, RelayInfo info )
		{
			Mobile m = state.Mobile;

			switch( info.ButtonID )
			{
				case 0: {break;}
				case 1:
				{
					if ( info.Switches.Length < switches )
					{
						m.SendGump( new SkillPickGump() );
						m.SendMessage( 0, "You must pick {0} more skills.", switches - info.Switches.Length );
						break;
					}
					else if ( info.Switches.Length > switches )
					{
						m.SendGump( new SkillPickGump() );
						m.SendMessage( 0, "Please get rid of {0} skills, you have exceeded 3.", info.Switches.Length - switches);
						break;
					}
					else
					{
						Server.Skills skills = m.Skills;

						for ( int i = 0; i < skills.Length; ++i )
							skills[i].Base = 0;
						if ( info.IsSwitched( 1 ))
							m.Skills[SkillName.Alchemy].Base = val;
						if ( info.IsSwitched( 2 ))
							m.Skills[SkillName.Anatomy].Base = val;
						if ( info.IsSwitched( 3 ))
							m.Skills[SkillName.AnimalLore].Base = val;
						if ( info.IsSwitched( 4 ))
							m.Skills[SkillName.AnimalTaming].Base = val;
						if ( info.IsSwitched( 5 ))
							m.Skills[SkillName.Archery].Base = val;
						if ( info.IsSwitched( 6 ))
							m.Skills[SkillName.ArmsLore].Base = val;
						if ( info.IsSwitched( 7 ))
							m.Skills[SkillName.Begging].Base = val;
						if ( info.IsSwitched( 8 ))
							m.Skills[SkillName.Bushido].Base = val;
						if ( info.IsSwitched( 9 ))
							m.Skills[SkillName.Blacksmith].Base = val;
						if ( info.IsSwitched( 10 ))
							m.Skills[SkillName.Chivalry].Base = val;
						if ( info.IsSwitched( 11 ))
							m.Skills[SkillName.Camping].Base = val;
						if ( info.IsSwitched( 12 ))
							m.Skills[SkillName.Fletching].Base = val;
						if ( info.IsSwitched( 13 ))
							m.Skills[SkillName.Carpentry].Base = val;
						if ( info.IsSwitched( 14 ))
							m.Skills[SkillName.Cooking].Base = val;
						if ( info.IsSwitched( 15 ))
							m.Skills[SkillName.Fishing].Base = val;
						if ( info.IsSwitched( 16 ))
							m.Skills[SkillName.Focus].Base = val;
						if ( info.IsSwitched( 17 ))
							m.Skills[SkillName.Healing].Base = val;
						if ( info.IsSwitched( 18 ))
							m.Skills[SkillName.Herding].Base = val;
						if ( info.IsSwitched( 19 ))
							m.Skills[SkillName.Lockpicking].Base = val;
						if ( info.IsSwitched( 20 ))
							m.Skills[SkillName.Lumberjacking].Base = val;
						if ( info.IsSwitched( 21 ))
							m.Skills[SkillName.Magery].Base = val;
						if ( info.IsSwitched( 22 ))
							m.Skills[SkillName.Meditation].Base = val;
						if ( info.IsSwitched( 23 ))
							m.Skills[SkillName.Mining].Base = val;
						if ( info.IsSwitched( 24 ))
							m.Skills[SkillName.Musicianship].Base = val;
						if ( info.IsSwitched( 25 ))
							m.Skills[SkillName.Necromancy].Base = val;
						if ( info.IsSwitched( 26 ))
							m.Skills[SkillName.Ninjitsu].Base = val;
						if ( info.IsSwitched( 27 ))
							m.Skills[SkillName.RemoveTrap].Base = val;
						if ( info.IsSwitched( 28 ))
							m.Skills[SkillName.MagicResist].Base = val;
						if ( info.IsSwitched( 29 ))
							m.Skills[SkillName.Snooping].Base = val;
						if ( info.IsSwitched( 30 ))
							m.Skills[SkillName.Spellweaving].Base = val;
						if ( info.IsSwitched( 31 ))
							m.Skills[SkillName.Stealing].Base = val;
						if ( info.IsSwitched( 32 ))
							m.Skills[SkillName.Stealth].Base = val;
						if ( info.IsSwitched( 33 ))
							m.Skills[SkillName.Tailoring].Base = val;
						if ( info.IsSwitched( 34 ))
							m.Skills[SkillName.Tinkering].Base = val;
						if ( info.IsSwitched( 35 ))
							m.Skills[SkillName.Veterinary].Base = val;
						if ( info.IsSwitched( 36 ))
							m.Skills[SkillName.Fencing].Base = val;
						if ( info.IsSwitched( 37 ))
							m.Skills[SkillName.Macing].Base = val;
						if ( info.IsSwitched( 38 ))
							m.Skills[SkillName.Parry].Base = val;
						if ( info.IsSwitched( 39 ))
							m.Skills[SkillName.Swords].Base = val;
						if ( info.IsSwitched( 40 ))
							m.Skills[SkillName.Tactics].Base = val;
						if ( info.IsSwitched( 41 ))
							m.Skills[SkillName.Wrestling].Base = val;
						if ( info.IsSwitched( 42 ))
							m.Skills[SkillName.Cartography].Base = val;
						if ( info.IsSwitched( 43 ))
							m.Skills[SkillName.DetectHidden].Base = val;
						if ( info.IsSwitched( 44 ))
							m.Skills[SkillName.Inscribe].Base = val;
						if ( info.IsSwitched( 45 ))
							m.Skills[SkillName.Peacemaking].Base = val;
						if ( info.IsSwitched( 46 ))
							m.Skills[SkillName.Poisoning].Base = val;
						if ( info.IsSwitched( 47 ))
							m.Skills[SkillName.Provocation].Base = val;
						if ( info.IsSwitched( 48 ))
							m.Skills[SkillName.SpiritSpeak].Base = val;
						if ( info.IsSwitched( 49 ))
							m.Skills[SkillName.Tracking].Base = val;
						if ( info.IsSwitched( 50 ))
							m.Skills[SkillName.EvalInt].Base = val;
						if ( info.IsSwitched( 51 ))
							m.Skills[SkillName.Forensics].Base = val;
						if ( info.IsSwitched( 52 ))
							m.Skills[SkillName.ItemID].Base = val;
						if ( info.IsSwitched( 53 ))
							m.Skills[SkillName.TasteID].Base = val;
						if ( info.IsSwitched( 54 ))
							m.Skills[SkillName.Discordance].Base = val;
						if ( info.IsSwitched( 55 ))
							m.Skills[SkillName.Hiding].Base = val;
					}
					break;
				}
			}
		}
	}

	public class SkillBall : Item
	{ 
		[Constructable] 
		public SkillBall() :  base( 0x1870 ) 
		{ 
			Weight = 1.0; 
			Hue = 1194; 
			Name = "skill ball"; 
			Movable =  false;
		}

		public override void OnDoubleClick( Mobile m ) 
		{				
			m.CloseGump( typeof( SkillPickGump ) );
			m.SendGump ( new SkillPickGump() );
			this.Delete();
		} 

		public SkillBall( Serial serial ) : base( serial ) 
		{ 
		} 
       
		public override void Serialize( GenericWriter writer ) 
		{ 
			base.Serialize( writer ); 
			writer.Write( (int) 1 ); // version 
		}

		public override void Deserialize( GenericReader reader ) 
		{ 
			base.Deserialize( reader ); 
			int version = reader.ReadInt(); 
		} 
	}
}