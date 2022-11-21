using System;
using Server;
using Server.Gumps;
using Server.Items;
using Server.Network;
using Server.Mobiles;
using Server.Spells;
using Server.Spells.Mystic;

namespace Server.Gumps
{
	public class SpellTriggerGump : Gump
	{
		public SpellTriggerGump( Mobile m ) : base( 0, 0 )
		{
			Closable = true;
			Disposable = true;
			Dragable = true;
			Resizable = false;

			int skill = (int)m.Skills[SkillName.Mysticism].Value;

			AddPage( 0 );
			AddBackground( 0, 0, 170, 400, 9270 );
			AddAlphaRegion( 10, 10, 150, 380 );

			// Listing them in skill order would have been easier, all well.

			// Skill required < 45
			AddLabel( 40, 15, 0, "Animated Weapon" );
			AddButton( 15, 15, 9702, 9703, 683, GumpButtonType.Reply, 0 ); // Animated Weapon

			if ( skill > 58 )
			{
				AddLabel( 40, 40, 0, "Bombard" );
				AddButton( 15, 40, 9702, 9703, 688, GumpButtonType.Reply, 0 ); // Bombard
			}
			else
			{
				AddLabel( 40, 40, 995, "Bombard" );
				AddImage( 15, 40, 9702, 995 );
			}

			if ( skill > 58 )
			{
				AddLabel( 40, 65, 0, "Cleansing Winds" );
				AddButton( 15, 65, 9702, 9703, 687, GumpButtonType.Reply, 0 ); // Cleansing Winds
			}
			else
			{
				AddLabel( 40, 65, 995, "Cleansing Winds" );
				AddImage( 15, 65, 9702, 995 );
			}

			// Skill required < 45
			AddLabel( 40, 90, 0, "Eagle Strike" );
			AddButton( 15, 90, 9702, 9703, 682, GumpButtonType.Reply, 0 ); // Eagle Strike

			// Skill required < 45
			AddLabel( 40, 115, 0, "Enchant" );
			AddButton( 15, 115, 9702, 9703, 680, GumpButtonType.Reply, 0 ); // Enchant

			if ( skill > 70 )
			{
				AddLabel( 40, 140, 0, "Hail Storm" );
				AddButton( 15, 140, 9702, 9703, 690, GumpButtonType.Reply, 0 ); // Hail Storm
			}
			else
			{
				AddLabel( 40, 140, 995, "Hail Storm" );
				AddImage( 15, 140, 9702, 995 );
			}

			// No skill required.
			AddLabel( 40, 165, 0, "Healing Stone" );
			AddButton( 15, 165, 9702, 9703, 678, GumpButtonType.Reply, 0 ); // Healing Stone

			// Srsly, 0 skill?
			AddLabel( 40, 190, 0, "Mass Sleep" );
			AddButton( 15, 190, 9702, 9703, 686, GumpButtonType.Reply, 0 ); // Mass Sleep

			// No skill required.
			AddLabel( 40, 215, 0, "Nether Bolt" );
			AddButton( 15, 215, 9702, 9703, 677, GumpButtonType.Reply, 0 ); // Nether Bolt

			if ( skill > 83 )
			{
				AddLabel( 40, 240, 0, "Nether Cyclone" );
				AddButton( 15, 240, 9702, 9703, 691, GumpButtonType.Reply, 0 ); // Nether Cyclone
			}
			else
			{
				AddLabel( 40, 240, 995, "Nether Cyclone" );
				AddImage( 15, 240, 9702, 995 );
			}

			// Skill required < 45
			AddLabel( 40, 265, 0, "Purge Magic" );
			AddButton( 15, 265, 9702, 9703, 679, GumpButtonType.Reply, 0 ); // Purge Magic

			if ( skill > 83 )
			{
				AddLabel( 40, 290, 0, "Rising Colossus" );
				AddButton( 15, 290, 9702, 9703, 692, GumpButtonType.Reply, 0 ); // Rising Colossus
			}
			else
			{
				AddLabel( 40, 290, 995, "Rising Colossus" );
				AddImage( 15, 290, 9702, 995 );
			}

			// Skill required < 45
			AddLabel( 40, 315, 0, "Sleep" );
			AddButton( 15, 315, 9702, 9703, 681, GumpButtonType.Reply, 0 ); // Sleep

			if ( skill > 70 )
			{
				AddLabel( 40, 340, 0, "Spell Plague" );
				AddButton( 15, 340, 9702, 9703, 689, GumpButtonType.Reply, 0 ); // Spell Plague
			}
			else
			{
				AddLabel( 40, 340, 995, "Spell Plague" );
				AddImage( 15, 340, 9702, 995 );
			}

			// Skill required < 45
			AddLabel( 40, 365, 0, "Stone Form" );
			AddButton( 15, 365, 9702, 9703, 684, GumpButtonType.Reply, 0 ); // Stone Form
		}

		public override void OnResponse( NetState sender, RelayInfo info )
		{
			Mobile from = sender.Mobile;

			if ( !(info.ButtonID >= 677 && info.ButtonID <= 692) )
				from.SendMessage( "There was an error in your spell choice, please try again or page if you have." );
			else if ( from.Backpack != null )
			{
				Item[] stones = from.Backpack.FindItemsByType( typeof( SpellStone ) );

				for ( int i = 0; i < stones.Length; i++ )
					stones[i].Delete();

				from.PlaySound( 0x659 );
				from.Backpack.DropItem( new SpellStone( from, info.ButtonID ) );
				from.SendLocalizedMessage( 1080165 ); // A Spell Stone appears in your backpack
			}
		}
	}
}