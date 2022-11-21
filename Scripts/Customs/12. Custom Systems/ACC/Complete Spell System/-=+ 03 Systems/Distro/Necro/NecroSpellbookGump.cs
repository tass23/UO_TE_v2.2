using System;
using Server;
using Server.Gumps;

namespace Server.ACC.CSS.Systems.Necromancy
{
	public class NecroSpellbookGump : NecroGump
	{
		public override string TextHue  { get{ return "333333"; } }
		public override int    BGImage  { get{ return 11008; } }
		public override int    SpellBtn { get{ return 2362; } }
		public override int    SpellBtnP{ get{ return 2361; } }
		public override string Label1   { get{ return "Necro"; } }
		public override string Label2   { get{ return "Spells"; } }
		public override Type   GumpType { get{ return typeof( NecroSpellbookGump ); } }
		public override School School{ get{ return School.Necro; } }

		public NecroSpellbookGump( CSpellbook book ) : base( book )
		{
		}
	}
}