using System;
using System.Collections;
using Server;
using Server.Gumps;
using Server.Items;
using Server.Spells;
using Server.Network;
using Server.Prompts;
using Server.ACC.CSS;

namespace Server.ACC.CSS.Systems.LightForce
{
    public class JediHolocronGump : JHolocronGump
    {
        public override string TextHue { get { return "126894"; } }	//126900 Green	670404 Maroon 126894 Light Blue/Green
        public override int BGImage { get { return 2400; } }	//public override int BGImage { get { return 2219; } }
        public override int SpellBtn { get { return 2104; } }
        public override int SpellBtnP { get { return 2103; } }
        public override string Label1 { get { return "Force"; } }
        public override string Label2 { get { return "Powers"; } }
        public override Type GumpType { get { return typeof(JediHolocronGump); } }

        public JediHolocronGump(CSpellbook book)
            : base(book)
        {
        }
    }
}