using System;
using Server;
using System.IO;
using Server.Gumps;
using Server.Items;
using Server.Network;
using Server.Mobiles;
using Server.Spells;
using Server.Accounting;
using System.Collections;
using Server.Commands;
using System.Text;
using Server.ACC.CM;
using Server.ACC.CSS.Modules;
using Server.ACC.CSS;
using Server.ACC.CSS.Systems.Werewolf;
using Server.ACC.CSS.Systems.Vampire;
using Server.ACC.CSS.Systems.LightForce;
using Server.ACC.CSS.Systems.DarkForce;
using Server.ACC.CSS.Systems.Mage;
using Server.ACC.CSS.Systems.Necromancy;
using Server.ACC.CSS.Systems.Chivalry;
using Server.ACC.CSS.Systems.Mysticism;
using Server.ACC.CSS.Systems.Spellweaving;

namespace Server.Gumps
{
	public abstract class ACCMiniGump : Gump
    {
		private CSpellbook m_Book;
        private ArrayList m_Spells;
        private int Pages;
        public int CurrentPage;

        public abstract string TextHue { get; }
        public abstract int BGImage { get; }
        public abstract int SpellBtn { get; }
        public abstract int SpellBtnP { get; }
        public abstract string Label3 { get; }
        public abstract string Label4 { get; }
        public abstract Type GumpType { get; }
		public abstract School School { get; }

        public ACCMiniGump(CSpellbook book) : base( 200, 200 )
		{
			m_Book = book;
            m_Spells = book.SchoolSpells;
		}
    }

	public class LycanPrimerMiniGump : ACCMiniGump
    {
		public CSpellbook m_Book;
        public ArrayList m_Spells;
		public override string TextHue { get { return "999999"; } }	//999000 Yellow-ish	990000 Dark Red
        public override int BGImage { get { return 2403; } }
        public override int SpellBtn { get { return 2104; } }
        public override int SpellBtnP { get { return 2103; } }
        public override string Label3 { get { return "Lycan"; } }
        public override string Label4 { get { return "Primer"; } }
        public override Type GumpType { get { return typeof(LycanPrimerGump); } }
		public override School School { get { return School.Werewolf; } }

        public LycanPrimerMiniGump(CSpellbook book) : base(book)
		{
			m_Book = book;
            m_Spells = book.SchoolSpells;
			AddPage(0);
			AddButton(90, 0, 10450, 10450, 1, GumpButtonType.Reply, 0);	//Lycan Primer Mini
			AddImage(90, 0, 10450, 1094);	//Gump image with gold dot
            AddLabel( 65, 50, 1166, "Ancient Lycan" );
			AddLabel( 85, 65, 1166, "Primer" );
		}

		public bool HasSpell(Type type)
        {
            return (m_Book != null && m_Book.HasSpell(type));
        }

		public override void OnResponse(NetState state, RelayInfo info)
        {
			Mobile m = state.Mobile;

            if (info.ButtonID == 0 || !CSS.Running)
                return;

			else if (info.ButtonID == 1)
			{
                state.Mobile.CloseGump(typeof(LycanPrimerMiniGump));
                state.Mobile.SendGump(new LycanPrimerGump(m_Book));
			}

            object[] Params = new object[1] { m_Book };
            CSpellbookGump gump = Activator.CreateInstance(GumpType, Params) as CSpellbookGump;
            if (gump != null)
                state.Mobile.SendGump(gump);
        }
    }

	public class AncientCovenMiniGump : ACCMiniGump
    {
		public CSpellbook m_Book;
        public ArrayList m_Spells;
		public override string TextHue { get { return "670404"; } }
        public override int BGImage { get { return 2404; } }
        public override int SpellBtn { get { return 2104; } }
        public override int SpellBtnP { get { return 2103; } }
        public override string Label3 { get { return "Vampiric"; } }
        public override string Label4 { get { return "Gifts"; } }
        public override Type GumpType { get { return typeof(CovenSpellbookGump); } }
		public override School School { get { return School.Vampire; } }

        public AncientCovenMiniGump(CSpellbook book) : base(book)
		{
			m_Book = book;
            m_Spells = book.SchoolSpells;
			AddPage(0);
			AddButton(95, 0, 9793, 9793, 1, GumpButtonType.Reply, 0);	//Ancient Coven Mini
			AddButton(95, 27, 9792, 9793, 1, GumpButtonType.Reply, 0);	//Ancient Coven Mini
			AddButton(95, 54, 9793, 9793, 1, GumpButtonType.Reply, 0);	//Ancient Coven Mini
			AddImage(95, 0, 9793, 1463);	//Gump image with gold dot
			AddImage(95, 27, 9792, 2984);	//Gump image with gold dot
			AddImage(95, 54, 9793, 1463);	//Gump image with gold dot
            AddLabel(62, 79, 1166, "Ancient Coven" );
			AddLabel(82, 94, 1166, "Spellbook" );
		}

		public bool HasSpell(Type type)
        {
            return (m_Book != null && m_Book.HasSpell(type));
        }

		public override void OnResponse(NetState state, RelayInfo info)
        {
			Mobile m = state.Mobile;

            if (info.ButtonID == 0 || !CSS.Running)
                return;

			else if (info.ButtonID == 1)
			{
                state.Mobile.CloseGump(typeof(AncientCovenMiniGump));
                state.Mobile.SendGump(new CovenSpellbookGump(m_Book));
			}

            object[] Params = new object[1] { m_Book };
            CSpellbookGump gump = Activator.CreateInstance(GumpType, Params) as CSpellbookGump;
            if (gump != null)
                state.Mobile.SendGump(gump);
        }
    }

	public class JediHolocronMiniGump : ACCMiniGump
    {
		public CSpellbook m_Book;
        public ArrayList m_Spells;
		public override string TextHue { get { return "126894"; } }	//126900 Green	670404 Maroon 126894 Light Blue/Green
        public override int BGImage { get { return 2400; } }
        public override int SpellBtn { get { return 2104; } }
        public override int SpellBtnP { get { return 2103; } }
        public override string Label3 { get { return "Force"; } }
        public override string Label4 { get { return "Powers"; } }
        public override Type GumpType { get { return typeof(JediHolocronGump); } }
		public override School School { get { return School.LightForce; } }

        public JediHolocronMiniGump(CSpellbook book) : base(book)
		{
			m_Book = book;
            m_Spells = book.SchoolSpells;
			AddPage(0);
			AddButton(95, 0, 2152, 2152, 1, GumpButtonType.Reply, 0);	//Light Force Holocron
			AddImage(95, 0, 2152, 2965);	//Blue diamond button
            AddLabel(69, 31, 1166, "Jedi Holocron" );
		}

		public bool HasSpell(Type type)
        {
            return (m_Book != null && m_Book.HasSpell(type));
        }

		public override void OnResponse(NetState state, RelayInfo info)
        {
			Mobile m = state.Mobile;

            if (info.ButtonID == 0 || !CSS.Running)
                return;

			else if (info.ButtonID == 1)
			{
                state.Mobile.CloseGump(typeof(JediHolocronMiniGump));
                state.Mobile.SendGump(new JediHolocronGump(m_Book));
			}

            object[] Params = new object[1] { m_Book };
            CSpellbookGump gump = Activator.CreateInstance(GumpType, Params) as CSpellbookGump;
            if (gump != null)
                state.Mobile.SendGump(gump);
        }
    }

	public class SithHolocronMiniGump : ACCMiniGump
    {
		public CSpellbook m_Book;
        public ArrayList m_Spells;
		public override string TextHue { get { return "999999"; } }	//100000 Black	300000 Dark Red 400000 Maroon 800000 Bright Red
        public override int BGImage { get { return 2401; } }
        public override int SpellBtn { get { return 2104; } }
        public override int SpellBtnP { get { return 2103; } }
        public override string Label3 { get { return "Force"; } }
        public override string Label4 { get { return "Powers"; } }
        public override Type GumpType { get { return typeof(SithHolocronGump); } }
		public override School School { get { return School.DarkForce; } }

        public SithHolocronMiniGump(CSpellbook book) : base(book)
		{
			m_Book = book;
            m_Spells = book.SchoolSpells;
			AddPage(0);
			AddButton(95, 0, 4500, 4500, 1, GumpButtonType.Reply, 0);	//Dark Force Holocron
			AddImage(95, 0, 4500, 2944);	//Gump image with gold dot
			AddImage(104, 18, 2152, 2944);	//Blue diamond button
            AddLabel(77, 48, 1166, "Sith Holocron" );
		}

		public bool HasSpell(Type type)
        {
            return (m_Book != null && m_Book.HasSpell(type));
        }

		public override void OnResponse(NetState state, RelayInfo info)
        {
			Mobile m = state.Mobile;

            if (info.ButtonID == 0 || !CSS.Running)
                return;

			else if (info.ButtonID == 1)
			{
                state.Mobile.CloseGump(typeof(SithHolocronMiniGump));
                state.Mobile.SendGump(new SithHolocronGump(m_Book));
			}

            object[] Params = new object[1] { m_Book };
            CSpellbookGump gump = Activator.CreateInstance(GumpType, Params) as CSpellbookGump;
            if (gump != null)
                state.Mobile.SendGump(gump);
        }
    }

	public class MageMiniGump : ACCMiniGump
    {
		public CSpellbook m_Book;
        public ArrayList m_Spells;
		public override string TextHue  { get{ return "CC3333"; } }
		public override int    BGImage  { get{ return 2220; } }
		public override int    SpellBtn { get{ return 2362; } }
		public override int    SpellBtnP{ get{ return 2361; } }
		public override string Label3   { get{ return "Mage"; } }
		public override string Label4   { get{ return "Spells"; } }
		public override Type   GumpType { get{ return typeof( MageSpellbookGump ); } }
		public override School School{ get{ return School.Magery; } }

        public MageMiniGump(CSpellbook book) : base(book)
		{
			m_Book = book;
            m_Spells = book.SchoolSpells;
			AddPage(0);
			AddButton(95, 0, 2234, 2234, 1, GumpButtonType.Reply, 0);	//Magery Spellbook
			AddImage(95, 0, 2234, 0);	//Blue diamond button
            AddLabel(69, 48, 1166, "Mage Spellbook" );
		}

		public bool HasSpell(Type type)
        {
            return (m_Book != null && m_Book.HasSpell(type));
        }

		public override void OnResponse(NetState state, RelayInfo info)
        {
			Mobile m = state.Mobile;

            if (info.ButtonID == 0 || !CSS.Running)
                return;

			else if (info.ButtonID == 1)
			{
                state.Mobile.CloseGump(typeof(MageMiniGump));
                state.Mobile.SendGump(new MageSpellbookGump(m_Book));
			}

            object[] Params = new object[1] { m_Book };
            CSpellbookGump gump = Activator.CreateInstance(GumpType, Params) as CSpellbookGump;
            if (gump != null)
                state.Mobile.SendGump(gump);
        }
    }

	public class NecroMiniGump : ACCMiniGump
    {
		public CSpellbook m_Book;
        public ArrayList m_Spells;
		public override string TextHue  { get{ return "CC3333"; } }
		public override int    BGImage  { get{ return 2220; } }
		public override int    SpellBtn { get{ return 2362; } }
		public override int    SpellBtnP{ get{ return 2361; } }
		public override string Label3   { get{ return "Necromancy"; } }
		public override string Label4   { get{ return "Spells"; } }
		public override Type   GumpType { get{ return typeof( NecroSpellbookGump ); } }
		public override School School{ get{ return School.Necro; } }

        public NecroMiniGump(CSpellbook book) : base(book)
		{
			m_Book = book;
            m_Spells = book.SchoolSpells;
			AddPage(0);
			AddButton(95, 0, 11011, 11011, 1, GumpButtonType.Reply, 0);
			AddImage(95, 0, 11011, 0);	//Blue diamond button
            AddLabel(58, 48, 1166, "Necromancy Spellbook" );
		}

		public bool HasSpell(Type type)
        {
            return (m_Book != null && m_Book.HasSpell(type));
        }

		public override void OnResponse(NetState state, RelayInfo info)
        {
			Mobile m = state.Mobile;

            if (info.ButtonID == 0 || !CSS.Running)
                return;

			else if (info.ButtonID == 1)
			{
                state.Mobile.CloseGump(typeof(NecroMiniGump));
                state.Mobile.SendGump(new NecroSpellbookGump(m_Book));
			}

            object[] Params = new object[1] { m_Book };
            CSpellbookGump gump = Activator.CreateInstance(GumpType, Params) as CSpellbookGump;
            if (gump != null)
                state.Mobile.SendGump(gump);
        }
    }

	public class ChivMiniGump : ACCMiniGump
    {
		public CSpellbook m_Book;
        public ArrayList m_Spells;
		
		public override string TextHue  { get{ return "CC3333"; } }
		public override int    BGImage  { get{ return 2220; } }
		public override int    SpellBtn { get{ return 2362; } }
		public override int    SpellBtnP{ get{ return 2361; } }
		public override string Label3   { get{ return "Chivalry"; } }
		public override string Label4   { get{ return "Spells"; } }
		public override Type   GumpType { get{ return typeof( ChivalrySpellbookGump ); } }
		public override School School{ get{ return School.Chivalry; } }

        public ChivMiniGump(CSpellbook book) : base(book)
		{
			m_Book = book;
            m_Spells = book.SchoolSpells;
			AddPage(0);
			AddButton(95, 0, 11012, 11012, 1, GumpButtonType.Reply, 0);
			AddImage(95, 0, 11012, 0);	//Blue diamond button
            AddLabel(60, 48, 1166, "Chivalry Spellbook" );
		}

		public bool HasSpell(Type type)
        {
            return (m_Book != null && m_Book.HasSpell(type));
        }

		public override void OnResponse(NetState state, RelayInfo info)
        {
			Mobile m = state.Mobile;

            if (info.ButtonID == 0 || !CSS.Running)
                return;

			else if (info.ButtonID == 1)
			{
                state.Mobile.CloseGump(typeof(ChivMiniGump));
                state.Mobile.SendGump(new ChivalrySpellbookGump(m_Book, m));
			}

            object[] Params = new object[1] { m_Book };
            CSpellbookGump gump = Activator.CreateInstance(GumpType, Params) as CSpellbookGump;
            if (gump != null)
                state.Mobile.SendGump(gump);
        }
    }

	/*
	public class BushidoMiniGump : ACCMiniGump
    {
		public CSpellbook m_Book;
        public ArrayList m_Spells;
		public override string TextHue  { get{ return "CC3333"; } }
		public override int    BGImage  { get{ return 2220; } }
		public override int    SpellBtn { get{ return 2362; } }
		public override int    SpellBtnP{ get{ return 2361; } }
		public override string Label3   { get{ return "Bushido"; } }
		public override string Label4   { get{ return "Spells"; } }
		public override Type   GumpType { get{ return typeof( BushidoSpellbookGump ); } }
		public override School School{ get{ return School.Bushido; } }

        public BushidoMiniGump(CSpellbook book) : base(book)
		{
			m_Book = book;
            m_Spells = book.SchoolSpells;
			AddPage(0);
			AddButton(95, 0, 11017, 11017, 1, GumpButtonType.Reply, 0);
			AddImage(95, 0, 11017, 0);	//Blue diamond button
            AddLabel(60, 48, 1166, "Book Of Bushido" );
		}

		public bool HasSpell(Type type)
        {
            return (m_Book != null && m_Book.HasSpell(type));
        }

		public override void OnResponse(NetState state, RelayInfo info)
        {
			Mobile m = state.Mobile;

            if (info.ButtonID == 0 || !CSS.Running)
                return;

			else if (info.ButtonID == 1)
			{
                state.Mobile.CloseGump(typeof(BushidoMiniGump));
                state.Mobile.SendGump(new BushidoSpellbookGump(m_Book));
			}

            object[] Params = new object[1] { m_Book };
            CSpellbookGump gump = Activator.CreateInstance(GumpType, Params) as CSpellbookGump;
            if (gump != null)
                state.Mobile.SendGump(gump);
        }
    }
	*/

	/*
	public class NinjitsuMiniGump : ACCMiniGump
    {
		public CSpellbook m_Book;
        public ArrayList m_Spells;
		public override string TextHue  { get{ return "CC3333"; } }
		public override int    BGImage  { get{ return 2220; } }
		public override int    SpellBtn { get{ return 2362; } }
		public override int    SpellBtnP{ get{ return 2361; } }
		public override string Label3   { get{ return "Ninjitsu"; } }
		public override string Label4   { get{ return "Spells"; } }
		public override Type   GumpType { get{ return typeof( NinjitsuSpellbookGump ); } }
		public override School School{ get{ return School.Ninjitsu; } }

        public NinjitsuMiniGump(CSpellbook book) : base(book)
		{
			m_Book = book;
            m_Spells = book.SchoolSpells;
			AddPage(0);
			AddButton(95, 0, 11016, 11016, 1, GumpButtonType.Reply, 0);
			AddImage(95, 0, 11016, 0);	//Blue diamond button
            AddLabel(60, 48, 1166, "Book Of Ninjitsu" );
		}

		public bool HasSpell(Type type)
        {
            return (m_Book != null && m_Book.HasSpell(type));
        }

		public override void OnResponse(NetState state, RelayInfo info)
        {
			Mobile m = state.Mobile;

            if (info.ButtonID == 0 || !CSS.Running)
                return;

			else if (info.ButtonID == 1)
			{
                state.Mobile.CloseGump(typeof(NinjitsuMiniGump));
                state.Mobile.SendGump(new NinjitsuSpellbookGump(m_Book));
			}

            object[] Params = new object[1] { m_Book };
            CSpellbookGump gump = Activator.CreateInstance(GumpType, Params) as CSpellbookGump;
            if (gump != null)
                state.Mobile.SendGump(gump);
        }
    }
	*/
	
	public class MysticismMiniGump : ACCMiniGump
    {
		public CSpellbook m_Book;
        public ArrayList m_Spells;
		public override string TextHue { get { return "670404"; } }
        public override int BGImage { get { return 2220; } }
        public override int SpellBtn { get { return 2104; } }
        public override int SpellBtnP { get { return 2103; } }
        public override string Label3 { get { return "Mysticism"; } }
        public override string Label4 { get { return "Spells"; } }
        public override Type GumpType { get { return typeof(MysticismSpellbookGump); } }
		public override School School { get { return School.Mysticism; } }

        public MysticismMiniGump(CSpellbook book) : base(book)
		{
			m_Book = book;
            m_Spells = book.SchoolSpells;
			AddPage(0);
			AddButton(95, 0, 11056, 11056, 1, GumpButtonType.Reply, 0);	//Mysticism Spellbook
			AddImage(95, 0, 11056, 0);	//Blue diamond button
            AddLabel(60, 48, 1166, "Mysticism Spellbook" );
		}

		public bool HasSpell(Type type)
        {
            return (m_Book != null && m_Book.HasSpell(type));
        }

		public override void OnResponse(NetState state, RelayInfo info)
        {
			Mobile m = state.Mobile;

            if (info.ButtonID == 0 || !CSS.Running)
                return;

			else if (info.ButtonID == 1)
			{
                state.Mobile.CloseGump(typeof(MysticismMiniGump));
                state.Mobile.SendGump(new MysticismSpellbookGump(m_Book));
			}

            object[] Params = new object[1] { m_Book };
            CSpellbookGump gump = Activator.CreateInstance(GumpType, Params) as CSpellbookGump;
            if (gump != null)
                state.Mobile.SendGump(gump);
        }
    }
	
	public class SpellweavingMiniGump : ACCMiniGump
    {
		public CSpellbook m_Book;
        public ArrayList m_Spells;
		public override string TextHue { get { return "670404"; } }
        public override int BGImage { get { return 11056; } }
        public override int SpellBtn { get { return 2104; } }
        public override int SpellBtnP { get { return 2103; } }
        public override string Label3 { get { return "Spellweaving"; } }
        public override string Label4 { get { return "Spells"; } }
        public override Type GumpType { get { return typeof(SpellweavingSpellbookGump); } }
		public override School School { get { return School.Spellweaving; } }

        public SpellweavingMiniGump(CSpellbook book) : base(book)
		{
			m_Book = book;
            m_Spells = book.SchoolSpells;
			AddPage(0);
			AddButton(95, 0, 11056, 11053, 1, GumpButtonType.Reply, 0);	//Spellweaving Spellbook
			AddImage(95, 0, 11053, 0);	//Blue diamond button
            AddLabel(56, 48, 1166, "Spellweaving Spellbook" );
		}

		public bool HasSpell(Type type)
        {
            return (m_Book != null && m_Book.HasSpell(type));
        }

		public override void OnResponse(NetState state, RelayInfo info)
        {
			Mobile m = state.Mobile;

            if (info.ButtonID == 0 || !CSS.Running)
                return;

			else if (info.ButtonID == 1)
			{
                state.Mobile.CloseGump(typeof(SpellweavingMiniGump));
                state.Mobile.SendGump(new SpellweavingSpellbookGump(m_Book));
			}

            object[] Params = new object[1] { m_Book };
            CSpellbookGump gump = Activator.CreateInstance(GumpType, Params) as CSpellbookGump;
            if (gump != null)
                state.Mobile.SendGump(gump);
        }
    }
}