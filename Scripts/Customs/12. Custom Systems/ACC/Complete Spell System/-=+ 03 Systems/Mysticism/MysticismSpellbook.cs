using System;
using Server.Gumps;
using Server.Items;
using Server.Spells;

namespace Server.ACC.CSS.Systems.Mysticism
{
	public class MysticismSpellbook : CSpellbook
	{
		public override School School{ get{ return School.Mysticism; } }

		[Constructable]
		public MysticismSpellbook() : this( (ulong)0, CSSettings.FullSpellbooks )
		{
		}

		[Constructable]
		public MysticismSpellbook( bool full ) : this( (ulong)0, full )
		{
		}

		[Constructable]
		public MysticismSpellbook( ulong content, bool full ) : base( content, 0x2D9D, full )
		{
			Name = "Mysticism Spellbook";
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( from.AccessLevel == AccessLevel.Player )
			{
				//Container pack = from.Backpack;
				//if( !(Parent == from || (pack != null && Parent == pack)) )
				//{
					//from.SendMessage( "The spellbook must be in your backpack to open." );
					//return;
				//}
				//else
				if( SpellRestrictions.UseRestrictions && !SpellRestrictions.CheckRestrictions( from, this.School ) )
				{
					return;
				}
			}

			from.CloseGump( typeof( MysticismSpellbookGump ) );
			from.CloseGump(typeof( MysticismMiniGump ));
			from.SendGump( new MysticismSpellbookGump( this ) );
		}

		public MysticismSpellbook( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
}