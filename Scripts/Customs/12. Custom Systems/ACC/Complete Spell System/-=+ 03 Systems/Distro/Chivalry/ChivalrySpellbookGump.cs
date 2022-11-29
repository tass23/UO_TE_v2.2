using System;
using Server;
using Server.Gumps;
using Server.Mobiles;

namespace Server.ACC.CSS.Systems.Chivalry
{
	public class ChivalrySpellbookGump : ChivGump
	{
		private Mobile m_From;
		public override string TextHue  { get{ return "333366"; } }
		public override int    BGImage  { get{ return 11009; } }
		public override int    SpellBtn { get{ return 2511; } }
		public override int    SpellBtnP{ get{ return 2511; } }
		public override string Label1   { get{ return "INDEX"; } }
		public override string Label2   { get{ return "INDEX"; } }
		public override Type   GumpType { get{ return typeof( ChivalrySpellbookGump ); } }
		public override School School{ get{ return School.Chivalry; } }

		public ChivalrySpellbookGump( CSpellbook book, Mobile from ) : base( book, from )
		{
		}
	}
}