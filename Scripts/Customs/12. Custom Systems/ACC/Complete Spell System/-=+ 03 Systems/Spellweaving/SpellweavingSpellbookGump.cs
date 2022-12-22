using System;
using Server;
using Server.Gumps;

namespace Server.ACC.CSS.Systems.Spellweaving
{
	public class SpellweavingSpellbookGump : SpellweavingGump
	{
		public override string TextHue  { get{ return "336633"; } }
		public override int    BGImage  { get{ return 11055; } }
		public override int    SpellBtn { get{ return 2362; } }
		public override int    SpellBtnP{ get{ return 2361; } }
		public override string Label1   { get{ return "Index"; } }
		public override string Label2   { get{ return "Index"; } }
		public override Type   GumpType { get{ return typeof( SpellweavingSpellbookGump ); } }
		public override School School{ get{ return School.Spellweaving; } }


		public SpellweavingSpellbookGump( CSpellbook book ) : base( book )
		{
		}
	}
}