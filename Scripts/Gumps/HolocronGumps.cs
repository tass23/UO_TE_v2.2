using System;
using Server;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using Server.Gumps;
using Server.Items;
using Server.Network;
using Server.Mobiles;
using Server.Spells;
using Server.Factions;
using Server.Targeting;
using Server.SkillHandlers;
using Server.Gumps;
using Server.Accounting;
using Server.Commands;
using Server.ACC.CM;
using Server.ACC.CSS.Modules;
using Server.ACC.CSS;
using Server.ACC.CSS.Systems.LightForce;
using Server.ACC.CSS.Systems.DarkForce;
using Server.ACC.CSS.Systems.Werewolf;
using Server.ACC.CSS.Systems.Vampire;

namespace Server.Gumps
{
	public abstract class HolocronGump : Gump	//CSpellbookGump
    {
        private CSpellbook m_Book;
        private ArrayList m_Spells;

        private int Pages;
        private int CurrentPage;

        public abstract string TextHue { get; }
        public abstract int BGImage { get; }
        public abstract int SpellBtn { get; }
        public abstract int SpellBtnP { get; }
        public abstract string Label1 { get; }
        public abstract string Label2 { get; }
        public abstract Type GumpType { get; }

        public HolocronGump(CSpellbook book)
            : base(50, 100)
        {
            if (!CSS.Running)
                return;

            m_Book = book;
            m_Spells = book.SchoolSpells;

            Pages = (int)Math.Ceiling((book.SpellCount / 14.0));

			/*if( Pages > 1 && book.Mark > 0 )
			{
				ArrayList temp = new ArrayList();
				for( int i = 0; i < book.Mark*16 && i < m_Spells.Count; i++ )
					temp.Add( m_Spells[i] );
				m_Spells.RemoveRange( 0, (book.Mark*16)-1 );
				m_Spells.AddRange( temp );
			}*/

            AddPage(0);
            AddImage(70, 100, BGImage);

            CurrentPage = 1;

            for (int i = 0; i < Pages; i++, CurrentPage++)
            {
                AddPage(CurrentPage);

                //Hidden Buttons
                for (int j = (CurrentPage - 1) * 14, C = 0; j < CurrentPage * 14 && j < m_Spells.Count; j++, C++)
                {
                    if (HasSpell((Type)m_Spells[j]))
                    {
                        AddButton((C > 6 ? 305 : 144), 155 + (C > 6 ? (C - 7) * 20 : C * 20), 2482, 2482, j + 1000, GumpButtonType.Reply, 0);
                    }
                }
                AddImage(70, 100, BGImage);
                AddHtml(161, 115, 100, 20, String.Format("<big><basefont color=#{0}><Center>{1}</Center></basefont color>", TextHue, Label1), false, false);
                AddHtml(305, 115, 100, 20, String.Format("<big><basefont color=#{0}><Center>{1}</Center></basefont color>", TextHue, Label2), false, false);
                //End Hidden Buttons

                //Prev/Next Buttons
                if (Pages > 1)
                {
                    if (CurrentPage > 1)
                        AddButton(142, 125, 2223, 2223, 0, GumpButtonType.Page, CurrentPage - 1);	//AddButton(124, 109, 2205, 2205, 0, GumpButtonType.Page, CurrentPage - 1);
                    if (CurrentPage < Pages)
                        AddButton(412, 125, 2224, 2224, 0, GumpButtonType.Page, CurrentPage + 1);
                }
                //End Prev/Next Buttons

                //Spell Buttons/Labels
                for (int j = (CurrentPage - 1) * 14, C = 0; j < CurrentPage * 14 && j < m_Spells.Count; j++, C++)
                {
                    if (HasSpell((Type)m_Spells[j]))
                    {
                        CSpellInfo info = SpellInfoRegistry.GetInfo(m_Book.School, (Type)m_Spells[j]);
                        if (info == null)
                            continue;

                        AddHtml((C > 6 ? 305 : 145), 155 + (C > 6 ? (C - 7) * 20 : C * 20), 110, 20, String.Format("<basefont color=#{0}>{1}</basefont>", TextHue, info.Name), false, false);
                        AddButton((C > 6 ? 426 : 260), 153 + (C > 6 ? (C - 7) * 20 : C * 20), SpellBtn, SpellBtnP, j + 2000, GumpButtonType.Reply, 0);
                        //AddButton((C > 6 ? 426 : 260), 153 + (C > 6 ? (C - 7) * 20 : C * 20), 5411, 5411, j + 1000, GumpButtonType.Reply, 0);
                    }
                }
                //End Spell Buttons/Labels
            }
        }

        public bool HasSpell(Type type)
        {
            return (m_Book != null && m_Book.HasSpell(type));
        }

        public override void OnResponse(NetState state, RelayInfo info)
        {
            if (info.ButtonID == 0 || !CSS.Running)
                return;

            else if (info.ButtonID >= 1000 && info.ButtonID < (1000 + m_Spells.Count))
            {
                if (SpellRestrictions.UseRestrictions && !SpellRestrictions.CheckRestrictions(state.Mobile, m_Book.School))
                {
                    state.Mobile.SendMessage("You are not allowed to use this Force Power.");
                    return;
                }

                CSpellInfo si = SpellInfoRegistry.GetInfo(m_Book.School, (Type)m_Spells[info.ButtonID - 1000]);
                if (si == null)
                {
                    state.Mobile.SendMessage("That Force Power is disabled.");
                    return;
                }
                state.Mobile.CloseGump(typeof(Holocron));
                state.Mobile.SendGump(new Holocron(m_Book, si, TextHue, state.Mobile));
                //				m_Book.Mark = (info.ButtonID-1000)/16;
                //				state.Mobile.SendMessage( "{0}", m_Book.Mark );
            }

            else if (info.ButtonID >= 2000 && info.ButtonID < (2000 + m_Spells.Count))
            {
                if (SpellRestrictions.UseRestrictions && !SpellRestrictions.CheckRestrictions(state.Mobile, m_Book.School))
                {
                    state.Mobile.SendMessage("You are not allowed to use this Force Power.");
                    return;
                }

                if (!CSpellbook.MobileHasSpell(state.Mobile, m_Book.School, (Type)m_Spells[info.ButtonID - 2000]))
                {
                    state.Mobile.SendMessage("You do not have that Force Power.");
                    return;
                }

                Spell spell = SpellInfoRegistry.NewSpell((Type)m_Spells[info.ButtonID - 2000], m_Book.School, state.Mobile, null);
                if (spell == null)
                    state.Mobile.SendMessage("That Force Power is disabled.");
                else
                    spell.Cast();
                //				m_Book.Mark = (info.ButtonID-2000)/16;
                //				state.Mobile.SendMessage( "{0}", m_Book.Mark );
            }

            object[] Params = new object[1] { m_Book };
            CSpellbookGump gump = Activator.CreateInstance(GumpType, Params) as CSpellbookGump;
            if (gump != null)
                state.Mobile.SendGump(gump);

            //GumpUpTimer
        }
    }

	public class Holocron : Gump
    {
        private CSpellInfo m_Info;
        private string m_TextHue;
        private CSpellbook m_Book;
        private CastInfo m_CastInfo;
        private CastCommandsModule m_CastCommandModule;
		public int m_HolocronHue;

        public Holocron(CSpellbook book, CSpellInfo info, string textHue, Mobile sender)
            : base(485, 175)
        {
            if (info == null || book == null || !CSS.Running)
                return;

            m_Info = info;
            m_Book = book;
            m_TextHue = textHue;
            m_CastInfo = new CastInfo(info.Type, info.School);
			
			if (info.School == School.DarkForce)
				m_HolocronHue = 2944;
			else
				m_HolocronHue = 2995;

            Closable = true;
            Disposable = true;
            Dragable = true;
            Resizable = false;

            AddPage(0);
			AddImage(155, 37, 9731, m_HolocronHue);
			AddImage(181, 37, 9731, m_HolocronHue);
			AddImage(120, 41, 9731, m_HolocronHue);
			AddImage(73, 117, 2643, m_HolocronHue);
			AddImage(112, 117, 2643, m_HolocronHue);
			AddImage(151, 117, 2643, m_HolocronHue);
			AddImage(190, 117, 2643, m_HolocronHue);
			AddImage(148, 51, 2661, m_HolocronHue);
			AddImage(94, 41, 9731, m_HolocronHue);
			AddImage(68, 41, 9731, m_HolocronHue);
			AddImage(207, 37, 9731, m_HolocronHue);
			AddImage(66, 51, 2661, m_HolocronHue);
			AddImage(121, 51, 2661, m_HolocronHue);
			AddImage(94, 51, 2661, m_HolocronHue);
			AddImage(175, 51, 2661, m_HolocronHue);
			AddImage(203, 51, 2661, m_HolocronHue);
			AddImageTiled(71, 7, 145, 20, 3507);
			AddImageTiled(60, 27, 145, 20, 3507);
			AddImageTiled(76, 66, 150, 72, 3004);
			AddImage(6, 6, 10461, m_HolocronHue); //Info BG LG
			AddImage(20, 41, 2643, m_HolocronHue); //Info BG 1
			AddImage(20, 6, 2643, m_HolocronHue); //Info BG 2

            if (info.Name != null)
				AddHtml(73, 6, 140, 20, String.Format("<basefont color=#{0}><center>{1}</center></font>", textHue, info.Name), false, false);

            AddButton(18, 20, info.Icon, info.Icon, 3, GumpButtonType.Reply, 0);
            AddButton(214, 45, 2331, 2338, 1, GumpButtonType.Reply, 0);

            //Info
            string InfoString = "";
            if (info.Desc != null)
                InfoString += String.Format("<basefont color=black>{0}</font><br><br>", info.Desc);

            if (info.Regs != null)
            {
                string[] Regs = info.Regs.Split(';');
                InfoString += String.Format("<basefont color=black>Reagents :</font><br><basefont color=#{0}>", textHue);
                foreach (string r in Regs)
                    InfoString += String.Format("-{0}<br>", r.TrimStart());
                InfoString += "</font><br>";
            }

            if (info.Info != null)
            {
                string[] Info = info.Info.Split(';');
                InfoString += String.Format("<basefont color=#{0}>", textHue);
                foreach (string s in Info)
                    InfoString += String.Format("{0}<br>", s.TrimStart());
                InfoString += "</font><br>";
            }
            AddHtml(80, 66, 150, 72, InfoString, false, true); //AddHtml(30, 95, 140, 130, InfoString, false, true);
            //End Info

            #region CastInfo
            if (CentralMemory.Running)
            {
                m_CastCommandModule = (CastCommandsModule)CentralMemory.GetModule(sender.Serial, typeof(CastCommandsModule));

                AddLabel(78, 45, 252, "Key");
                if (m_CastCommandModule == null)
                    AddTextEntry(81, 27, 100, 20, 0, 5, ""); //Key Loc,Size,Hue,ID
                else
                    AddTextEntry(81, 27, 100, 20, 0, 5, m_CastCommandModule.GetCommandForInfo(m_CastInfo));//Key Loc,Size,Hue,ID
                AddButton(186, 31, 2103, 2104, 4, GumpButtonType.Reply, 0); //KeyButton
            }
            #endregion //CastInfo
        }

        public override void OnResponse(NetState state, RelayInfo info)
        {
            if (info.ButtonID == 0 || !CSS.Running)
                return;

            else if (info.ButtonID == 1)
            {
                if (SpellRestrictions.UseRestrictions && !SpellRestrictions.CheckRestrictions(state.Mobile, m_Info.School))
                {
                    state.Mobile.SendMessage("You are not allowed to use this Force Power.");
                    return;
                }

                if (!CSpellbook.MobileHasSpell(state.Mobile, m_Info.School, m_Info.Type))
                {
                    state.Mobile.SendMessage("You do not have that Force Power.");
                    return;
                }

                Spell spell = SpellInfoRegistry.NewSpell(m_Info.Type, m_Info.School, state.Mobile, null);
                if (spell == null)
                    state.Mobile.SendMessage("That Force Power is disabled.");
                else
                    spell.Cast();
            }

            else if (info.ButtonID == 2)
            {
                //Scribe
            }

            else if (info.ButtonID == 3)
            {
                if (!CentralMemory.Running)
                    return;

                if (SpellRestrictions.UseRestrictions && !SpellRestrictions.CheckRestrictions(state.Mobile, m_Info.School))
                    return;

                state.Mobile.SendGump(new IconPlacementGump(m_Book, state.Mobile, 100, 100, 10, m_Info.Icon, m_Info.Type, m_Info.Back, m_Book.School));
            }

            else if (info.ButtonID == 4)
            {
                if (!CentralMemory.Running)
                    return;

                string command = info.GetTextEntry(5).Text;

                if (command == null || command.Length == 0)
                {
                    if (m_CastCommandModule == null)
                    {
                        state.Mobile.SendGump(new Holocron(m_Book, m_Info, m_TextHue, state.Mobile));
                        return;
                    }

                    m_CastCommandModule.RemoveCommandByInfo(m_CastInfo);
                    state.Mobile.SendGump(new Holocron(m_Book, m_Info, m_TextHue, state.Mobile));
                }
                else
                {
                    if (m_CastCommandModule == null)
                    {
                        CentralMemory.AddModule(new CastCommandsModule(state.Mobile.Serial, command, m_CastInfo));
                        state.Mobile.SendGump(new Holocron(m_Book, m_Info, m_TextHue, state.Mobile));
                        return;
                    }

                    m_CastCommandModule.Add(command, m_CastInfo);
                    state.Mobile.SendGump(new Holocron(m_Book, m_Info, m_TextHue, state.Mobile));
                }
            }
        }
    }

    public abstract class JHolocronGump : Gump	//CSpellbookGump
    {
        private CSpellbook m_Book;
        private ArrayList m_Spells;
		public BaseTool m_Tool;

        private int Pages;
        private int CurrentPage;
		public string BookHue  { get{ return "999000"; } }
        public abstract string TextHue { get; }
        public abstract int BGImage { get; }
        public abstract int SpellBtn { get; }
        public abstract int SpellBtnP { get; }
        public abstract string Label1 { get; }
        public abstract string Label2 { get; }
        public abstract Type GumpType { get; }

        public JHolocronGump(CSpellbook book)
            : base(50, 100)
        {
            if (!CSS.Running)
                return;

            m_Book = book;
            m_Spells = book.SchoolSpells;
            Pages = (int)Math.Ceiling((book.SpellCount / 14.0));

            AddPage(0);
            AddImage(70, 100, BGImage);
            CurrentPage = 1;

            for (int i = 0; i < Pages; i++, CurrentPage++)
            {
                AddPage(CurrentPage);

                //Hidden Buttons
                for (int j = (CurrentPage - 1) * 14, C = 0; j < CurrentPage * 14 && j < m_Spells.Count; j++, C++)
                {
                    if (HasSpell((Type)m_Spells[j]))
                    {
                        AddButton((C > 6 ? 305 : 144), 155 + (C > 6 ? (C - 7) * 20 : C * 20), 2482, 2482, j + 1000, GumpButtonType.Reply, 0);
                    }
                }
				AddButton(453, 244, 4000, 4000, 6, GumpButtonType.Reply, 0); //Minimize button
				AddButton(87, 198, 2708, 2708, 7, GumpButtonType.Reply, 0);	//Food capsule left button
				AddButton(87, 215, 2708, 2708, 7, GumpButtonType.Reply, 0);	//Food capsule left button
				AddButton(85, 140, 2643, 2643, 8, GumpButtonType.Reply, 0);	//Make rune left button
				AddButton(465, 200, 2708, 2708, 9, GumpButtonType.Reply, 0); //Water capsule right button
				AddButton(465, 217, 2708, 2708, 9, GumpButtonType.Reply, 0); //Water capsule right button
				AddButton(450, 140, 2643, 2643, 10, GumpButtonType.Reply, 0); //Master crafting right button

				AddButton(113, 230, 5102, 5102, 11, GumpButtonType.Reply, 0); //Lockpicking left button
				AddButton(113, 243, 5102, 5102, 12, GumpButtonType.Reply, 0); //Taste Indentification left button
				AddButton(113, 255, 5102, 5102, 13, GumpButtonType.Reply, 0); //Inscription skill left button
				AddButton(113, 268, 5102, 5102, 14, GumpButtonType.Reply, 0); //Arms lore skill left button
				AddButton(113, 282, 5102, 5102, 15, GumpButtonType.Reply, 0); //Tracking skill left button
                AddImage(70, 100, BGImage);
                AddHtml(161, 115, 100, 20, String.Format("<big><basefont color=#{0}><Center>{1}</Center>", BookHue, Label1), false, false);
                AddHtml(305, 115, 100, 20, String.Format("<big><basefont color=#{0}><Center>{1}</Center>", BookHue, Label2), false, false);
                //End Hidden Buttons

                //Prev/Next Buttons
                if (Pages > 1)
                {
                    if (CurrentPage > 1)
                        AddButton(142, 125, 2223, 2223, 0, GumpButtonType.Page, CurrentPage - 1);
                    if (CurrentPage < Pages)
                        AddButton(412, 125, 2224, 2224, 0, GumpButtonType.Page, CurrentPage + 1);
                }
                //End Prev/Next Buttons

                //Spell Buttons/Labels
                for (int j = (CurrentPage - 1) * 14, C = 0; j < CurrentPage * 14 && j < m_Spells.Count; j++, C++)
                {
                    if (HasSpell((Type)m_Spells[j]))
                    {
                        CSpellInfo info = SpellInfoRegistry.GetInfo(m_Book.School, (Type)m_Spells[j]);
                        if (info == null)
                            continue;

                        AddHtml((C > 6 ? 305 : 145), 155 + (C > 6 ? (C - 7) * 20 : C * 20), 110, 20, String.Format("<basefont color=#{0}>{1}</basefont>", TextHue, info.Name), false, false);
                        AddButton((C > 6 ? 426 : 260), 157 + (C > 6 ? (C - 7) * 20 : C * 20), SpellBtn, SpellBtnP, j + 2000, GumpButtonType.Reply, 0);
                        //AddButton((C > 6 ? 426 : 260), 153 + (C > 6 ? (C - 7) * 20 : C * 20), 5411, 5411, j + 1000, GumpButtonType.Reply, 0);
                    }
                }
                //End Spell Buttons/Labels
            }
        }

        public bool HasSpell(Type type)
        {
            return (m_Book != null && m_Book.HasSpell(type));
        }

		private static FoodInfo[] m_Food = new FoodInfo[]
		{
			new FoodInfo( typeof( Grapes ), "a grape bunch" ),
			new FoodInfo( typeof( Ham ), "a ham" ),
			new FoodInfo( typeof( CheeseWedge ), "a wedge of cheese" ),
			new FoodInfo( typeof( Muffins ), "muffins" ),
			new FoodInfo( typeof( FishSteak ), "a fish steak" ),
			new FoodInfo( typeof( Ribs ), "cut of ribs" ),
			new FoodInfo( typeof( CookedBird ), "a cooked bird" ),
			new FoodInfo( typeof( Sausage ), "sausage" ),
			new FoodInfo( typeof( Apple ), "an apple" ),
			new FoodInfo( typeof( Peach ), "a peach" )
		};

        public override void OnResponse(NetState state, RelayInfo info)
        {
			Mobile m = state.Mobile;
			Container pack = m.Backpack;
			BaseTool tool = (BaseTool)pack.FindItemByType(typeof(BaseTool));

            if (info.ButtonID == 0 || !CSS.Running)
                return;

			else if (info.ButtonID == 6)
			{
                state.Mobile.CloseGump(typeof(JediHolocronGump));
                state.Mobile.SendGump(new JediHolocronMiniGump(m_Book));
			}

			else if (info.ButtonID == 7)
			{
				FoodInfo foodInfo = m_Food[Utility.Random( m_Food.Length )];
				Item food = foodInfo.Create();

				if ( food != null )
				{
					m.SendMessage("You dispense some food from the container.");
					m.AddToBackpack(food);
					m.PlaySound( 0x1E2 );
					//state.Mobile.CloseGump(typeof(JediHolocronGump));
					state.Mobile.SendGump(new JediHolocronGump(m_Book));
				}				
			}

			else if (info.ButtonID == 8)
			{
				m.SendMessage("You create a blank rune.");
				m.AddToBackpack( new RecallRune() );
				//state.Mobile.CloseGump(typeof(JediHolocronGump));
                state.Mobile.SendGump(new JediHolocronGump(m_Book));
			}

			else if (info.ButtonID == 9)
			{
				m.SendMessage("You dispense some water from the container.");
				m.AddToBackpack( new Pitcher( BeverageType.Water ) );
				//state.Mobile.CloseGump(typeof(JediHolocronGump));
                state.Mobile.SendGump(new JediHolocronGump(m_Book));
			}

			else if (info.ButtonID == 10)
			{
				//state.Mobile.CloseGump(typeof(JediHolocronGump));	//Add Master Craft Menu to select specific crafting menu.
                m.SendMessage( "What would you like to Craft?" );
				m.SendGump( new MasterCraftGump(m) );
			}

			else if (info.ButtonID == 11)
			{
				if ( m.Skills[SkillName.Lockpicking].Value < 50 )
				{
					m.SendLocalizedMessage( 502366 ); // You do not know enough about locks.  Become better at picking locks.
				}
				else if ( m.Skills[SkillName.DetectHidden].Value < 50 )
				{
					m.SendLocalizedMessage( 502367 ); // You are not perceptive enough.  Become better at detect hidden.
				}
				else
				{
					m.Target = new RTrapTarget();

					m.SendLocalizedMessage( 502368 ); // Which trap will you attempt to disarm?
				}
				
				//state.Mobile.CloseGump(typeof(JediHolocronGump));
                state.Mobile.SendGump(new JediHolocronGump(m_Book));
			}

			else if (info.ButtonID == 12)
			{
				m.UseSkill( SkillName.TasteID );
				//state.Mobile.CloseGump(typeof(JediHolocronGump));
                state.Mobile.SendGump(new JediHolocronGump(m_Book));
			}

			else if (info.ButtonID == 13)
			{
				m.UseSkill( SkillName.Forensics );
				//state.Mobile.CloseGump(typeof(JediHolocronGump));
                state.Mobile.SendGump(new JediHolocronGump(m_Book));
			}

			else if (info.ButtonID == 14)
			{
				m.UseSkill( SkillName.ItemID );
				//state.Mobile.CloseGump(typeof(JediHolocronGump));
                state.Mobile.SendGump(new JediHolocronGump(m_Book));
			}

			else if (info.ButtonID == 15)
			{
				m.UseSkill( SkillName.Tracking );
				//state.Mobile.CloseGump(typeof(JediHolocronGump));
                state.Mobile.SendGump(new JediHolocronGump(m_Book));
			}

            else if (info.ButtonID >= 1000 && info.ButtonID < (1000 + m_Spells.Count))
            {
                if (SpellRestrictions.UseRestrictions && !SpellRestrictions.CheckRestrictions(state.Mobile, m_Book.School))
                {
                    state.Mobile.SendMessage("You are not allowed to use this Force Power.");
                    return;
                }

                CSpellInfo si = SpellInfoRegistry.GetInfo(m_Book.School, (Type)m_Spells[info.ButtonID - 1000]);
                if (si == null)
                {
                    state.Mobile.SendMessage("That Force Power is disabled.");
                    return;
                }
                //state.Mobile.CloseGump(typeof(JHolocronGump));
                state.Mobile.SendGump(new JediHolocronGump(m_Book));
                state.Mobile.SendGump(new Holocron(m_Book, si, TextHue, state.Mobile));
                //				m_Book.Mark = (info.ButtonID-1000)/16;
                //				state.Mobile.SendMessage( "{0}", m_Book.Mark );
            }

            else if (info.ButtonID >= 2000 && info.ButtonID < (2000 + m_Spells.Count))
            {
                if (SpellRestrictions.UseRestrictions && !SpellRestrictions.CheckRestrictions(state.Mobile, m_Book.School))
                {
                    state.Mobile.SendMessage("You are not allowed to use this Force Power.");
                    return;
                }

                if (!CSpellbook.MobileHasSpell(state.Mobile, m_Book.School, (Type)m_Spells[info.ButtonID - 2000]))
                {
                    state.Mobile.SendMessage("You do not have that Force Power.");
                    return;
                }

                Spell spell = SpellInfoRegistry.NewSpell((Type)m_Spells[info.ButtonID - 2000], m_Book.School, state.Mobile, null);
                if (spell == null)
                    state.Mobile.SendMessage("That Force Power is disabled.");
                else
                    spell.Cast();
				
                state.Mobile.SendGump(new JediHolocronGump(m_Book));
                //				m_Book.Mark = (info.ButtonID-2000)/16;
                //				state.Mobile.SendMessage( "{0}", m_Book.Mark );
            }

            object[] Params = new object[1] { m_Book };
            CSpellbookGump gump = Activator.CreateInstance(GumpType, Params) as CSpellbookGump;
            if (gump != null)
                state.Mobile.SendGump(gump);

            //GumpUpTimer
        }
		
		private class RTrapTarget : Target
		{
			public RTrapTarget() :  base ( 2, false, TargetFlags.None )
			{
			}

			protected override void OnTarget( Mobile from, object targeted )
			{
				if ( targeted is Mobile )
				{
					from.SendLocalizedMessage( 502816 ); // You feel that such an action would be inappropriate
				}
				else if ( targeted is TrapableContainer )
				{
					TrapableContainer targ = (TrapableContainer)targeted;

					from.Direction = from.GetDirectionTo( targ );

					if ( targ.TrapType == TrapType.None )
					{
						from.SendLocalizedMessage( 502373 ); // That doesn't appear to be trapped
						return;
					}

					from.PlaySound( 0x241 );
					
					if ( from.CheckTargetSkill( SkillName.RemoveTrap, targ, targ.TrapPower, targ.TrapPower + 30 ) )
					{
						targ.TrapPower = 0;
						targ.TrapLevel = 0;
						targ.TrapType = TrapType.None;
						from.SendLocalizedMessage( 502377 ); // You successfully render the trap harmless
					}
					else
					{
						from.SendLocalizedMessage( 502372 ); // You fail to disarm the trap... but you don't set it off
					}
				}
				else if ( targeted is BaseFactionTrap )
				{
					BaseFactionTrap trap = (BaseFactionTrap) targeted;
					Faction faction = Faction.Find( from );

					FactionTrapRemovalKit kit = ( from.Backpack == null ? null : from.Backpack.FindItemByType( typeof( FactionTrapRemovalKit ) ) as FactionTrapRemovalKit );

					bool isOwner = ( trap.Placer == from || ( trap.Faction != null && trap.Faction.IsCommander( from ) ) );

					if ( faction == null )
					{
						from.SendLocalizedMessage( 1010538 ); // You may not disarm faction traps unless you are in an opposing faction
					}
					else if ( faction == trap.Faction && trap.Faction != null && !isOwner )
					{
						from.SendLocalizedMessage( 1010537 ); // You may not disarm traps set by your own faction!
					}
					else if ( !isOwner && kit == null )
					{
						from.SendLocalizedMessage( 1042530 ); // You must have a trap removal kit at the base level of your pack to disarm a faction trap.
					}
					else
					{
						if ( (Core.ML && isOwner) || (from.CheckTargetSkill( SkillName.RemoveTrap, trap, 80.0, 100.0 ) && from.CheckTargetSkill( SkillName.Tinkering, trap, 80.0, 100.0 )) )
						{
							from.PrivateOverheadMessage( MessageType.Regular, trap.MessageHue, trap.DisarmMessage, from.NetState );

							if ( !isOwner )
							{
								int silver = faction.AwardSilver( from, trap.SilverFromDisarm );

								if ( silver > 0 )
									from.SendLocalizedMessage( 1008113, true, silver.ToString( "N0" ) ); // You have been granted faction silver for removing the enemy trap :
							}

							trap.Delete();
						}
						else
						{
							from.SendLocalizedMessage( 502372 ); // You fail to disarm the trap... but you don't set it off
						}

						if ( !isOwner && kit != null )
							kit.ConsumeCharge( from );
					}
				}
				else
				{
					from.SendLocalizedMessage( 502373 ); // That does'nt appear to be trapped
				}
			}
		}
    }

	public abstract class SHolocronGump : Gump	//CSpellbookGump
    {
        private CSpellbook m_Book;
        private ArrayList m_Spells;
        private int Pages;
        private int CurrentPage;
		public BaseTool m_Tool;
		
		public string BookHue  { get{ return "999000"; } }
        public abstract string TextHue { get; }
        public abstract int BGImage { get; }
        public abstract int SpellBtn { get; }
        public abstract int SpellBtnP { get; }
        public abstract string Label1 { get; }
        public abstract string Label2 { get; }
        public abstract Type GumpType { get; }

        public SHolocronGump(CSpellbook book)
            : base(50, 100)
        {
            if (!CSS.Running)
                return;

            m_Book = book;
            m_Spells = book.SchoolSpells;
            Pages = (int)Math.Ceiling((book.SpellCount / 14.0));

            AddPage(0);
            AddImage(70, 100, BGImage);
            CurrentPage = 1;

            for (int i = 0; i < Pages; i++, CurrentPage++)
            {
                AddPage(CurrentPage);

                //Hidden Buttons
                for (int j = (CurrentPage - 1) * 14, C = 0; j < CurrentPage * 14 && j < m_Spells.Count; j++, C++)
                {
                    if (HasSpell((Type)m_Spells[j]))
                    {
                        AddButton((C > 6 ? 305 : 144), 155 + (C > 6 ? (C - 7) * 20 : C * 20), 2482, 2482, j + 1000, GumpButtonType.Reply, 0);
                    }
                }
				AddButton(89, 244, 4000, 4000, 6, GumpButtonType.Reply, 0);	//Minimize button
				AddButton(87, 198, 2708, 2708, 7, GumpButtonType.Reply, 0);	//Food capsule left button
				AddButton(87, 215, 2708, 2708, 7, GumpButtonType.Reply, 0);	//Food capsule left button
				AddButton(85, 140, 2643, 2643, 8, GumpButtonType.Reply, 0);	//Make rune left button
				AddButton(465, 200, 2708, 2708, 9, GumpButtonType.Reply, 0); //Water capsule right button
				AddButton(465, 217, 2708, 2708, 9, GumpButtonType.Reply, 0); //Water capsule right button
				AddButton(450, 140, 2643, 2643, 10, GumpButtonType.Reply, 0); //Master crafting right button

				AddButton(450, 230, 5102, 5102, 11, GumpButtonType.Reply, 0); //Trap Disarm right button
				AddButton(450, 243, 5102, 5102, 12, GumpButtonType.Reply, 0); //Taste Identification right button
				AddButton(450, 255, 5102, 5102, 13, GumpButtonType.Reply, 0); //Forensics right button
				AddButton(450, 268, 5102, 5102, 14, GumpButtonType.Reply, 0); //Item Identification right button
				AddButton(450, 281, 5102, 5102, 15, GumpButtonType.Reply, 0); //Tracking right button
                AddImage(70, 100, BGImage);
                AddHtml(161, 115, 100, 20, String.Format("<big><basefont color=#{0}><Center>{1}</Center>", TextHue, Label1), false, false);
                AddHtml(305, 115, 100, 20, String.Format("<big><basefont color=#{0}><Center>{1}</Center>", TextHue, Label2), false, false);
                //End Hidden Buttons

                //Prev/Next Buttons
                if (Pages > 1)
                {
                    if (CurrentPage > 1)
                        AddButton(142, 125, 2223, 2223, 0, GumpButtonType.Page, CurrentPage - 1);
                    if (CurrentPage < Pages)
                        AddButton(412, 125, 2224, 2224, 0, GumpButtonType.Page, CurrentPage + 1);
                }
                //End Prev/Next Buttons

                //Spell Buttons/Labels
                for (int j = (CurrentPage - 1) * 14, C = 0; j < CurrentPage * 14 && j < m_Spells.Count; j++, C++)
                {
                    if (HasSpell((Type)m_Spells[j]))
                    {
                        CSpellInfo info = SpellInfoRegistry.GetInfo(m_Book.School, (Type)m_Spells[j]);
                        if (info == null)
                            continue;

                        AddHtml((C > 6 ? 305 : 145), 155 + (C > 6 ? (C - 7) * 20 : C * 20), 110, 20, String.Format("<basefont color=#{0}>{1}</basefont>", BookHue, info.Name), false, false);
                        AddButton((C > 6 ? 426 : 260), 157 + (C > 6 ? (C - 7) * 20 : C * 20), SpellBtn, SpellBtnP, j + 2000, GumpButtonType.Reply, 0);
                        //AddButton((C > 6 ? 426 : 260), 153 + (C > 6 ? (C - 7) * 20 : C * 20), 5411, 5411, j + 1000, GumpButtonType.Reply, 0);
                    }
                }
                //End Spell Buttons/Labels
            }
        }

        public bool HasSpell(Type type)
        {
            return (m_Book != null && m_Book.HasSpell(type));
        }

		private static FoodInfo[] m_Food = new FoodInfo[]
		{
			new FoodInfo( typeof( Grapes ), "a grape bunch" ),
			new FoodInfo( typeof( Ham ), "a ham" ),
			new FoodInfo( typeof( CheeseWedge ), "a wedge of cheese" ),
			new FoodInfo( typeof( Muffins ), "muffins" ),
			new FoodInfo( typeof( FishSteak ), "a fish steak" ),
			new FoodInfo( typeof( Ribs ), "cut of ribs" ),
			new FoodInfo( typeof( CookedBird ), "a cooked bird" ),
			new FoodInfo( typeof( Sausage ), "sausage" ),
			new FoodInfo( typeof( Apple ), "an apple" ),
			new FoodInfo( typeof( Peach ), "a peach" )
		};

        public override void OnResponse(NetState state, RelayInfo info)
        {
			Mobile m = state.Mobile;
			Container pack = m.Backpack;
			BaseTool tool = (BaseTool)pack.FindItemByType(typeof(BaseTool));

            if (info.ButtonID == 0 || !CSS.Running)
                return;

			else if (info.ButtonID == 6)
			{
                state.Mobile.CloseGump(typeof(SithHolocronGump));
                state.Mobile.SendGump(new SithHolocronMiniGump(m_Book));
			}

			else if (info.ButtonID == 7)
			{
				FoodInfo foodInfo = m_Food[Utility.Random( m_Food.Length )];
				Item food = foodInfo.Create();

				if ( food != null )
				{
					m.SendMessage("You dispense some food from the container.");
					m.AddToBackpack(food);
					m.PlaySound( 0x1E2 );
					//state.Mobile.CloseGump(typeof(SithHolocronGump));
					state.Mobile.SendGump(new SithHolocronGump(m_Book));
				}				
			}

			else if (info.ButtonID == 8)
			{
				m.SendMessage("You create a blank rune.");
				m.AddToBackpack( new RecallRune() );
				//state.Mobile.CloseGump(typeof(SithHolocronGump));
                state.Mobile.SendGump(new SithHolocronGump(m_Book));
			}

			else if (info.ButtonID == 9)
			{
				m.SendMessage("You dispense some water from the container.");
				m.AddToBackpack( new Pitcher( BeverageType.Water ) );
				//state.Mobile.CloseGump(typeof(SithHolocronGump));
                state.Mobile.SendGump(new SithHolocronGump(m_Book));
			}

			else if (info.ButtonID == 10)
			{
				//state.Mobile.CloseGump(typeof(SithHolocronGump));	//Add Master Craft Menu to select specific crafting menu.
                m.SendMessage( "What do you want to Craft?" );
				m.SendGump( new MasterCraftGump(m) );
			}

			else if (info.ButtonID == 11)
			{
				if ( m.Skills[SkillName.Lockpicking].Value < 50 )
				{
					m.SendLocalizedMessage( 502366 ); // You do not know enough about locks.  Become better at picking locks.
				}
				else if ( m.Skills[SkillName.DetectHidden].Value < 50 )
				{
					m.SendLocalizedMessage( 502367 ); // You are not perceptive enough.  Become better at detect hidden.
				}
				else
				{
					m.Target = new RTrapTarget();

					m.SendLocalizedMessage( 502368 ); // Wich trap will you attempt to disarm?
				}
				//state.Mobile.CloseGump(typeof(SithHolocronGump));
                state.Mobile.SendGump(new SithHolocronGump(m_Book));
			}

			else if (info.ButtonID == 12)
			{
				m.UseSkill( SkillName.TasteID );
				//state.Mobile.CloseGump(typeof(SithHolocronGump));
                state.Mobile.SendGump(new SithHolocronGump(m_Book));
			}

			else if (info.ButtonID == 13)
			{
				m.UseSkill( SkillName.Forensics );
				//state.Mobile.CloseGump(typeof(SithHolocronGump));
                state.Mobile.SendGump(new SithHolocronGump(m_Book));
			}

			else if (info.ButtonID == 14)
			{
				m.UseSkill( SkillName.ItemID );
				//state.Mobile.CloseGump(typeof(SithHolocronGump));
                state.Mobile.SendGump(new SithHolocronGump(m_Book));
			}

			else if (info.ButtonID == 15)
			{
				//state.Mobile.CloseGump(typeof(SithHolocronGump));
                state.Mobile.SendGump(new SithHolocronGump(m_Book));
                m.UseSkill( SkillName.Tracking );
			}

            else if (info.ButtonID >= 1000 && info.ButtonID < (1000 + m_Spells.Count))
            {
                if (SpellRestrictions.UseRestrictions && !SpellRestrictions.CheckRestrictions(state.Mobile, m_Book.School))
                {
                    state.Mobile.SendMessage("You are not allowed to use this Force Power.");
                    return;
                }

                CSpellInfo si = SpellInfoRegistry.GetInfo(m_Book.School, (Type)m_Spells[info.ButtonID - 1000]);
                if (si == null)
                {
                    state.Mobile.SendMessage("That Force Power is disabled.");
                    return;
                }
                //state.Mobile.CloseGump(typeof(SithHolocronGump));
                state.Mobile.SendGump(new SithHolocronGump(m_Book));
                state.Mobile.SendGump(new Holocron(m_Book, si, TextHue, state.Mobile));
                //				m_Book.Mark = (info.ButtonID-1000)/16;
                //				state.Mobile.SendMessage( "{0}", m_Book.Mark );
            }

            else if (info.ButtonID >= 2000 && info.ButtonID < (2000 + m_Spells.Count))
            {
                if (SpellRestrictions.UseRestrictions && !SpellRestrictions.CheckRestrictions(state.Mobile, m_Book.School))
                {
                    state.Mobile.SendMessage("You are not allowed to use this Force Power.");
                    return;
                }

                if (!CSpellbook.MobileHasSpell(state.Mobile, m_Book.School, (Type)m_Spells[info.ButtonID - 2000]))
                {
                    state.Mobile.SendMessage("You do not have that Force Power.");
                    return;
                }

                Spell spell = SpellInfoRegistry.NewSpell((Type)m_Spells[info.ButtonID - 2000], m_Book.School, state.Mobile, null);
                if (spell == null)
                    state.Mobile.SendMessage("That Force Power is disabled.");
                else
                    spell.Cast();
				
                state.Mobile.SendGump(new SithHolocronGump(m_Book));
                //				m_Book.Mark = (info.ButtonID-2000)/16;
                //				state.Mobile.SendMessage( "{0}", m_Book.Mark );
            }

            object[] Params = new object[1] { m_Book };
            CSpellbookGump gump = Activator.CreateInstance(GumpType, Params) as CSpellbookGump;
            if (gump != null)
                state.Mobile.SendGump(gump);

            //GumpUpTimer
        }
		
		private class RTrapTarget : Target
		{
			public RTrapTarget() :  base ( 2, false, TargetFlags.None )
			{
			}

			protected override void OnTarget( Mobile from, object targeted )
			{
				if ( targeted is Mobile )
				{
					from.SendLocalizedMessage( 502816 ); // You feel that such an action would be inappropriate
				}
				else if ( targeted is TrapableContainer )
				{
					TrapableContainer targ = (TrapableContainer)targeted;

					from.Direction = from.GetDirectionTo( targ );

					if ( targ.TrapType == TrapType.None )
					{
						from.SendLocalizedMessage( 502373 ); // That doesn't appear to be trapped
						return;
					}

					from.PlaySound( 0x241 );
					
					if ( from.CheckTargetSkill( SkillName.RemoveTrap, targ, targ.TrapPower, targ.TrapPower + 30 ) )
					{
						targ.TrapPower = 0;
						targ.TrapLevel = 0;
						targ.TrapType = TrapType.None;
						from.SendLocalizedMessage( 502377 ); // You successfully render the trap harmless
					}
					else
					{
						from.SendLocalizedMessage( 502372 ); // You fail to disarm the trap... but you don't set it off
					}
				}
				else if ( targeted is BaseFactionTrap )
				{
					BaseFactionTrap trap = (BaseFactionTrap) targeted;
					Faction faction = Faction.Find( from );

					FactionTrapRemovalKit kit = ( from.Backpack == null ? null : from.Backpack.FindItemByType( typeof( FactionTrapRemovalKit ) ) as FactionTrapRemovalKit );

					bool isOwner = ( trap.Placer == from || ( trap.Faction != null && trap.Faction.IsCommander( from ) ) );

					if ( faction == null )
					{
						from.SendLocalizedMessage( 1010538 ); // You may not disarm faction traps unless you are in an opposing faction
					}
					else if ( faction == trap.Faction && trap.Faction != null && !isOwner )
					{
						from.SendLocalizedMessage( 1010537 ); // You may not disarm traps set by your own faction!
					}
					else if ( !isOwner && kit == null )
					{
						from.SendLocalizedMessage( 1042530 ); // You must have a trap removal kit at the base level of your pack to disarm a faction trap.
					}
					else
					{
						if ( (Core.ML && isOwner) || (from.CheckTargetSkill( SkillName.RemoveTrap, trap, 80.0, 100.0 ) && from.CheckTargetSkill( SkillName.Tinkering, trap, 80.0, 100.0 )) )
						{
							from.PrivateOverheadMessage( MessageType.Regular, trap.MessageHue, trap.DisarmMessage, from.NetState );

							if ( !isOwner )
							{
								int silver = faction.AwardSilver( from, trap.SilverFromDisarm );

								if ( silver > 0 )
									from.SendLocalizedMessage( 1008113, true, silver.ToString( "N0" ) ); // You have been granted faction silver for removing the enemy trap :
							}

							trap.Delete();
						}
						else
						{
							from.SendLocalizedMessage( 502372 ); // You fail to disarm the trap... but you don't set it off
						}

						if ( !isOwner && kit != null )
							kit.ConsumeCharge( from );
					}
				}
				else
				{
					from.SendLocalizedMessage( 502373 ); // That does'nt appear to be trapped
				}
			}
		}
    }

	public class FoodInfo
	{
		private Type m_Type;
		private string m_Name;

		public Type Type{ get{ return m_Type; } set{ m_Type = value; } }
		public string Name{ get{ return m_Name; } set{ m_Name = value; } }

		public FoodInfo( Type type, string name )
		{
			m_Type = type;
			m_Name = name;
		}

		public Item Create()
		{
			Item item;

			try
			{
				item = (Item)Activator.CreateInstance( m_Type );
			}
			catch
			{
				item = null;
			}

			return item;
		}
	}
}