using System;
using Server.Gumps;
using Server.Spells;
using Server.Items;

namespace Server.ACC.CSS.Systems.Chivalry
{
	public class ChivalrySpellbook : CSpellbook
	{
		
		public override School School{ get{ return School.Chivalry; } }
/*		public override Item Dupe( int amount )
		{
			CSpellbook book = new ChivalrySpellbook();
			book.Content = this.Content;
			return base.Dupe( book, amount );
		}
*/
		[Constructable]
		public ChivalrySpellbook( Mobile from ) : this( from, (ulong)0, CSSettings.FullSpellbooks )
		{
		}

		[Constructable]
		public ChivalrySpellbook( Mobile from, bool full ) : this( from, (ulong)0, full )
		{
		}

		[Constructable]
		public ChivalrySpellbook( Mobile from, ulong content, bool full ) : base( content, 0xEFA, full )
		{
			ItemID = 8786;
			Name = "Chivalry Spellbook";
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( from.AccessLevel == AccessLevel.Player )
			{
				//Container pack = from.Backpack;
				//if( !(Parent == from || (pack != null && Parent == pack)) )
				//{
					//from.SendMessage( "The spellbook must be in your backpack [and not in a container within] to open." );
					//return;
				//}
				//else
				if( SpellRestrictions.UseRestrictions && !SpellRestrictions.CheckRestrictions( from, this.School ) )
				{
					return;
				}
			}

			from.CloseGump(typeof( ChivalrySpellbookGump ));
			from.CloseGump(typeof( ChivMiniGump ));
			from.SendGump( new ChivalrySpellbookGump( this, from ) );
		}

		public ChivalrySpellbook( Serial serial ) : base( serial )
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
