using System;
using System.Collections;
using Server;
using Server.Gumps;
using Server.Items;
using Server.Spells;
using Server.Network;
using Server.Prompts;
using Server.ACC.CSS;

namespace Server.ACC.CSS.Systems.Vampire
{
    public class CovenSpellbookGump : VampyGump
    {
        public override string TextHue { get { return "300000"; } }
        public override int BGImage { get { return 2404; } }
        public override int SpellBtn { get { return 1252; } }
        public override int SpellBtnP { get { return 1252; } }
        public override string Label1 { get { return "Vampiric"; } }
        public override string Label2 { get { return "Gifts"; } }
        public override Type GumpType { get { return typeof(CovenSpellbookGump); } }
		public override School School { get { return School.Vampire; } }

        public CovenSpellbookGump(CSpellbook book) : base(book)
        {
        }
    }
}
