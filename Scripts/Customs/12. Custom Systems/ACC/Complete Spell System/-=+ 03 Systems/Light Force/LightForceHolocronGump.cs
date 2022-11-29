using System;
using System.Collections;
using Server;
using Server.Gumps;
using Server.Items;
using Server.Spells;
using Server.Network;
using Server.Prompts;

namespace Server.ACC.CSS.Systems.LightForce
{
    public class JediHolocronGump : JHolocronGump
    {
        public override string TextHue { get { return "126894"; } }
        public override int BGImage { get { return 2400; } }
        public override int SpellBtn { get { return 11400; } }
        public override int SpellBtnP { get { return 11400; } }
        public override string Label1 { get { return "FORCE"; } }
        public override string Label2 { get { return "POWERS"; } }
        public override Type GumpType { get { return typeof(JediHolocronGump); } }

        public JediHolocronGump(CSpellbook book)
            : base(book)
        {
        }
    }
}