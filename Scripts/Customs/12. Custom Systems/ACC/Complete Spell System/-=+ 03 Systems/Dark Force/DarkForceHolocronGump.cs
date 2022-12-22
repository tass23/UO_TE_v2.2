using System;
using System.Collections;
using Server;
using Server.Gumps;
using Server.Items;
using Server.Spells;
using Server.Network;
using Server.Prompts;

namespace Server.ACC.CSS.Systems.DarkForce
{
    public class SithHolocronGump : SHolocronGump
    {
        public override string TextHue { get { return "126894"; } }
        public override int BGImage { get { return 2401; } }
        public override int SpellBtn { get { return 11410; } }
        public override int SpellBtnP { get { return 11410; } }
        public override string Label1 { get { return "Force"; } }
        public override string Label2 { get { return "Powers"; } }
        public override Type GumpType { get { return typeof(SithHolocronGump); } }

        public SithHolocronGump(CSpellbook book)
            : base(book)
        {
        }
    }
}