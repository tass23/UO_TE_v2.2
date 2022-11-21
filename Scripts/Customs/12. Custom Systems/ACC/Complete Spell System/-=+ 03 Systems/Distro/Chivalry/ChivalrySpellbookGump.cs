using System;
using Server;
using Server.Gumps;

namespace Server.ACC.CSS.Systems.Chivalry
{
	public class ChivalrySpellbookGump : ChivGump
	{
		public override string TextHue  { get{ return "333366"; } }
		public override int    BGImage  { get{ return 11009; } }
		public override int    SpellBtn { get{ return 2362; } }
		public override int    SpellBtnP{ get{ return 2361; } }
		public override string Label1   { get{ return "Chivalry"; } }
		public override string Label2   { get{ return "Spells"; } }
		public override Type   GumpType { get{ return typeof( ChivalrySpellbookGump ); } }
		public override School School{ get{ return School.Chivalry; } }

		public ChivalrySpellbookGump( CSpellbook book ) : base( book )
		{
		}
	}
}