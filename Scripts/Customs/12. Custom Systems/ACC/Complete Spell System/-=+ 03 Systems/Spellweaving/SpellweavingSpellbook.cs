using System;
using Server.Gumps;
using Server.Items;
using Server.Spells;

namespace Server.ACC.CSS.Systems.Spellweaving
{
	public class SpellweavingSpellbook : CSpellbook
	{
		public override School School{ get{ return School.Spellweaving; } }

		[Constructable]
		public SpellweavingSpellbook() : this( (ulong)0, CSSettings.FullSpellbooks )
		{
		}

		[Constructable]
		public SpellweavingSpellbook( bool full ) : this( (ulong)0, full )
		{
		}

		[Constructable]
		public SpellweavingSpellbook( ulong content, bool full ) : base( content, 0x2D50, full )
		{
			Hue = 2210;
			Name = "Spellweaving Book";
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( from.AccessLevel == AccessLevel.Player )
			{
				//Container pack = from.Backpack;
				//if( !(Parent == from || (pack != null && Parent == pack)) )
				//{
					//from.SendMessage( "The book must be in your backpack to open." );
					//return;
				//}
				//else
				if( SpellRestrictions.UseRestrictions && !SpellRestrictions.CheckRestrictions( from, this.School ) )
				{
					return;
				}
			}

			from.CloseGump( typeof( SpellweavingSpellbookGump ) );
			from.CloseGump(typeof( SpellweavingMiniGump ));
			from.SendGump( new SpellweavingSpellbookGump( this ) );
		}

		public SpellweavingSpellbook( Serial serial ) : base( serial )
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