using System;
using System.Collections;
using Server;
using Server.Gumps;
using Server.Items;
using Server.Spells;
using Server.Network;
using Server.Prompts;
using Server.ACC.CSS;

namespace Server.ACC.CSS.Systems.Werewolf
{
    public class LycanPrimerGump : LycanGump
    {
        public override string TextHue { get { return "999999"; } }
        public override int BGImage { get { return 2403; } }
        public override int SpellBtn { get { return 10254; } }
        public override int SpellBtnP { get { return 10254; } }
        public override string Label1 { get { return "LYCAN"; } }
        public override string Label2 { get { return "ABILITIES"; } }
        public override Type GumpType { get { return typeof(LycanPrimerGump); } }
		public override School School { get { return School.Werewolf; } }

        public LycanPrimerGump(CSpellbook book): base(book)
        {
        }
    }
}
