using System;
using Server;
using Server.Gumps;

namespace Server.ACC.CSS.Systems.Mage
{
	public class MageSpellbookGump : MageryGump
	{
		public override string TextHue  { get{ return "CC3333"; } }
		public override int    BGImage  { get{ return 2220; } }
		public override int    SpellBtn { get{ return 2362; } }
		public override int    SpellBtnP{ get{ return 2361; } }
		public override string Label1   { get{ return "Mage"; } }
		public override string Label2   { get{ return "Spells"; } }
		public override Type   GumpType { get{ return typeof( MageSpellbookGump ); } }
		public override School School{ get{ return School.Magery; } }

		public MageSpellbookGump( CSpellbook book ) : base( book )
		{
		}
	}
}