#region AuthorHeader
//
//	SpellCrafting version 3.0, by Xanthos and TheOutkastDev
//
//  Based on original ideas and code by TheOutkastDev
//
#endregion AuthorHeader
using System;
using System.Collections;
using System.Collections.Generic;
using Server;
using Server.Gumps;
using Server.Network;
using Server.Items;
using Server.SpellCrafting;
using Server.SpellCrafting.Items;
using Server.SpellCrafting.Crafts;
using Server.Targeting;

namespace Server.SpellCrafting.Gumps
{
	public delegate void PageMethod( int pageNumber, int indexNumber );

	public class SpellCraftBook : Gump
	{
		private int m_GumpX, m_GumpY;
		private BookOfSpellCrafts m_Book;
		private bool m_ShowHiddenText = false;
		private const int kHiddenTextColor = 2308;
		private const string kRevealText = "The candle light reveals hidden writings on the pages.";
		private const string kRevealText2 = "You surmise these are the names of crafts and their magic jewel consumption.";

		private static string [] m_PageTitles =
		{
			"",
			"Casting Crafts",
			"Resist Bonus Crafts",
			"Stat Bonus Crafts",
			"Weapon Crafts One",
			"Weapon Crafts Two",
			"Weapon Crafts Three",
			"Miscellaneous Crafts",
		};
		private const int kTotalPages = 7;

		public SpellCraftBook( Mobile owner, BookOfSpellCrafts book, int currentPage ) : base( 10, 10 )
		{
			int totalCrafts = book.Count;

			owner.CloseGump( typeof( SpellCraftBook ) );

			m_Book = book;
			Resizable = false;
			Closable = Disposable = Dragable = true;

			// Lay out the index page and then add all of the content pages. The order of
			// the index items is always the same but the order of the content pages can
			// vary since the book must re-open to the last viewed page when using a craft.

			// Create a list of page numbers then put the current page at the head.
			// One more entry than needed as page numbers begin at one.

			int [] pageOrder = new int[ kTotalPages + 1 ] { 0, 1, 2, 3, 4, 5, 6, 7 };

			if ( 1 != currentPage && currentPage > 0 && currentPage <= kTotalPages )
			{
				int temp = pageOrder[ 1 ];
				pageOrder[ 1 ] = pageOrder[ currentPage ];
				pageOrder[ currentPage ] = temp;
			}

			// Layout page zero with the index entries on it.

			AddImage( 50, 50, 0x1F4 );
			AddHtml( 76, 60, 185, 20, "<center>Spellcraft Index</center", false, false );
			m_GumpY = 85;

			for( int i = 1; i <= kTotalPages; i++ )
			{
				AddButton( 85, m_GumpY, 0x1523, 0x1523, 100 + i, GumpButtonType.Page, pageOrder[ i ] );
				AddLabel( 100, m_GumpY, 0, m_PageTitles[ i ] );
				m_GumpY += 20;
			}
			AddLabel( 85, 235, 0, "Total Spellcrafts:" );
			AddHtml( 170, 235, 80, 20,
				String.Format( "<div align=right>{0} of {1}</div>", totalCrafts, SpellCraftConfig.LastAosCraftID + 1 ), false, false );
			
			// Abracadabra!
			Candle candle = owner.FindItemOnLayer( Layer.TwoHanded ) as Candle;
			if ( m_ShowHiddenText = ( candle != null && candle.Burning ))
			{
				owner.SendMessage( kRevealText );
				owner.SendMessage( kRevealText2 );
			}

			// Add all of the content pages according to the order list
			for( int i = 1; i <= kTotalPages; i++ )
			{
				switch ( pageOrder[ i ] )
				{
					case 1: AddCastingPage( i, 1 ); break;
					case 2: AddResistPage( i, 2 ); break;
					case 3: AddStatPage( i, 3 ); break;
					case 4: AddWeaponPageOne( i, 4 ); break;
					case 5: AddWeaponPageTwo( i, 5 ); break;
					case 6: AddWeaponPageThree( i, 6 ); break;
					case 7: AddMiscellaneousPage( i, 7 ); break;
				}
			}
		}

		private void AddCastingPage( int pageNumber, int indexNumber )
		{
			AddPage( pageNumber );
			AddHtml( 262, 60, 185, 20, "<center>Casting Crafts</center>", false, false );
			m_GumpX = 275; m_GumpY = 85;

			AddCraft( 14, "Faster Cast Recovery", indexNumber );
			AddCraft( 15, "Faster Cast Speed", indexNumber );
			AddCraft( 16, "Lower Mana Cost", indexNumber );
			AddCraft( 17, "Lower Reagent Cost", indexNumber );
			AddCraft( 18, "Mage Armor", indexNumber );
			AddCraft( 19, "Mage Weapon", indexNumber );
			AddCraft( 20, "Spell Channeling", indexNumber );
			AddCraft( 21, "Spell Damage Increase", indexNumber );
		}

		private void AddStatPage( int pageNumber, int indexNumber )
		{
			AddPage( pageNumber );
			AddHtml( 262, 60, 185, 20, "<center>Stat Bonus Crafts</center>", false, false );
			m_GumpX = 275; m_GumpY = 85;

			AddCraft( 0, "Strength Bonus", indexNumber );
			AddCraft( 1, "Dexterity Bonus", indexNumber );
			AddCraft( 2, "Intelligence Bonus", indexNumber );
			AddCraft( 3, "Hit Point Bonus", indexNumber );
			AddCraft( 4, "Stamina Bonus", indexNumber );
			AddCraft( 5, "Mana Bonus", indexNumber );
			AddCraft( 11, "Hit Point Regeneration", indexNumber );
			AddCraft( 12, "Mana Regeneration", indexNumber );
			AddCraft( 13, "Stamina Regeneration", indexNumber );
		}

		private void AddResistPage( int pageNumber, int indexNumber )
		{
			AddPage( pageNumber );
			AddHtml( 262, 60, 185, 20, "<center>Resist Bonus Crafts</center>", false, false );
			m_GumpX = 275; m_GumpY = 85;

			AddCraft( 6, "Physical Resist Bonus", indexNumber );
			AddCraft( 7, "Fire Resist Bonus", indexNumber );
			AddCraft( 8, "Cold Resist Bonus", indexNumber );
			AddCraft( 9, "Poison Resist Bonus", indexNumber );
			AddCraft( 10, "Energy Resist Bonus", indexNumber );
		}

		private void AddWeaponPageOne( int pageNumber, int indexNumber )
		{
			AddPage( pageNumber );
			AddHtml( 262, 60, 185, 20, "<center>Weapon Crafts One</center>", false, false );
			m_GumpX = 275; m_GumpY = 85;

			AddCraft( 22, "Hit Cold Area", indexNumber );
			AddCraft( 23, "Hit Energy Area", indexNumber );
			AddCraft( 24, "Hit Fire Area", indexNumber );
			AddCraft( 25, "Hit Physical Area", indexNumber );
			AddCraft( 26, "Hit Poison Area", indexNumber );
			AddCraft( 27, "Hit Dispel", indexNumber );
			AddCraft( 28, "Hit Fireball", indexNumber );
			AddCraft( 29, "Hit Harm", indexNumber );
		}

		private void AddWeaponPageTwo( int pageNumber, int indexNumber )
		{
			AddPage( pageNumber );
			AddHtml( 262, 60, 185, 20, "<center>Weapon Crafts Two</center>", false, false );
			m_GumpX = 275; m_GumpY = 85;
					
			AddCraft( 30, "Hit Lightning", indexNumber );
			AddCraft( 31, "Hit Magic Arrow", indexNumber );
			AddCraft( 32, "Hit Lower Attack", indexNumber );
			AddCraft( 33, "Hit Lower Defense", indexNumber );
			AddCraft( 34, "Hit Life Leech", indexNumber );
			AddCraft( 35, "Hit Mana Leech", indexNumber );
			AddCraft( 36, "Hit Stamina Leech", indexNumber );
			AddCraft( 37, "Use Best Weapon Skill", indexNumber );
		}

		private void AddWeaponPageThree( int pageNumber, int indexNumber )
		{
			AddPage( pageNumber );
			AddHtml( 262, 60, 185, 20, "<center>Weapon Crafts Three</center>", false, false );
			m_GumpX = 275; m_GumpY = 85;

			AddCraft( 38, "Weapon Damage Increase", indexNumber );
			AddCraft( 39, "Swing Speed Increase", indexNumber );
			AddCraft( 40, "Hit Chance Increase", indexNumber );
			AddCraft( 41, "Defense Chance Increase", indexNumber );
			AddCraft( 48, "Slayer", indexNumber );
		}

		private void AddMiscellaneousPage( int pageNumber, int indexNumber )
		{
			AddPage( pageNumber );
			AddHtml( 262, 60, 185, 20, "<center>Miscellaneous Crafts</center>", false, false );
			m_GumpX = 275; m_GumpY = 85;

			AddCraft( 42, "Enhance Potions", indexNumber );
			AddCraft( 43, "Lower Requirements", indexNumber );
			AddCraft( 44, "Luck", indexNumber );
			AddCraft( 45, "Reflect Physical", indexNumber );
			AddCraft( 46, "Self Repair", indexNumber );
			AddCraft( 47, "Night Sight", indexNumber );
			AddCraft( 49, "Durability", indexNumber );
		}

		public void AddCraft( int buttonID, string text, int indexNumber )
		{
			bool hasCraft = m_Book.HasCraft( buttonID );

			if ( m_ShowHiddenText )
			{
				AddLabel( m_GumpX, m_GumpY - 2, kHiddenTextColor, SpellCraftConfig.MagicJewelRequirements[buttonID].ToString() );
				AddLabel( m_GumpX + 20, m_GumpY - 2, (hasCraft ? 0 : kHiddenTextColor), text );
			}
			else if ( hasCraft )
			{
				AddButton( m_GumpX, m_GumpY, 0x846, 0x845, buttonID + 1, GumpButtonType.Reply, indexNumber );
				AddLabel( m_GumpX + 20, m_GumpY - 2, 0, text );
			}

			m_GumpY += 20;
		}

		public override void OnResponse( NetState state, RelayInfo info )
		{
			Mobile from = state.Mobile;
			int craft = info.ButtonID - 1;

			if ( craft < 0 )
				return;

			if ( !SpellCraftConfig.CraftEnabled[craft] )
			{
				from.SendMessage( "This craft is currently not enabled." );
				return;
			}

			if ( !SpellCraft.SufficientSkillToCraft( from ) )
			{
				from.SendMessage( "You must have at least {0} alchemy and {1} inscription to use this craft.",
					SpellCraftConfig.MinimumAlchemy, SpellCraftConfig.MinimumInscription );
				return;
			}

			from.SendMessage( "Select the item to place this spellcraft on." );

			switch( craft )
			{
				case 0: from.BeginTarget( 1, false, TargetFlags.None, new TargetStateCallback( BonusStr.Callback ), new CraftState( m_Book, 0 )); break;
				case 1: from.BeginTarget( 1, false, TargetFlags.None, new TargetStateCallback( BonusDex.Callback ), new CraftState( m_Book, 1 )); break;
				case 2: from.BeginTarget( 1, false, TargetFlags.None, new TargetStateCallback( BonusInt.Callback ), new CraftState( m_Book, 2 )); break;
				case 3: from.BeginTarget( 1, false, TargetFlags.None, new TargetStateCallback( BonusHits.Callback ), new CraftState( m_Book, 3 )); break;
				case 4: from.BeginTarget( 1, false, TargetFlags.None, new TargetStateCallback( BonusStam.Callback ), new CraftState( m_Book, 4 )); break;
				case 5: from.BeginTarget( 1, false, TargetFlags.None, new TargetStateCallback( BonusMana.Callback ), new CraftState( m_Book, 5 )); break;
				case 6: from.BeginTarget( 1, false, TargetFlags.None, new TargetStateCallback( PhysicalResist.Callback ), new CraftState( m_Book, 6 )); break;
				case 7: from.BeginTarget( 1, false, TargetFlags.None, new TargetStateCallback( FireResist.Callback ), new CraftState( m_Book, 7 )); break;
				case 8: from.BeginTarget( 1, false, TargetFlags.None, new TargetStateCallback( ColdResist.Callback ), new CraftState( m_Book, 8 )); break;
				case 9: from.BeginTarget( 1, false, TargetFlags.None, new TargetStateCallback( PoisonResist.Callback ), new CraftState( m_Book, 9 )); break;
				case 10: from.BeginTarget( 1, false, TargetFlags.None, new TargetStateCallback( EnergyResist.Callback ), new CraftState( m_Book, 10 )); break;
				case 11: from.BeginTarget( 1, false, TargetFlags.None, new TargetStateCallback( RegenHits.Callback ), new CraftState( m_Book, 11 )); break;
				case 12: from.BeginTarget( 1, false, TargetFlags.None, new TargetStateCallback( RegenMana.Callback ), new CraftState( m_Book, 12 )); break;
				case 13: from.BeginTarget( 1, false, TargetFlags.None, new TargetStateCallback( RegenStam.Callback ), new CraftState( m_Book, 13 )); break;
				case 14: from.BeginTarget( 1, false, TargetFlags.None, new TargetStateCallback( CastRecovery.Callback ), new CraftState( m_Book, 14 )); break;
				case 15: from.BeginTarget( 1, false, TargetFlags.None, new TargetStateCallback( CastSpeed.Callback ), new CraftState( m_Book, 15 )); break;
				case 16: from.BeginTarget( 1, false, TargetFlags.None, new TargetStateCallback( LowerManaCost.Callback ), new CraftState( m_Book, 16 )); break;
				case 17: from.BeginTarget( 1, false, TargetFlags.None, new TargetStateCallback( LowerRegCost.Callback ), new CraftState( m_Book, 17 )); break;
				case 18: from.BeginTarget( 1, false, TargetFlags.None, new TargetStateCallback( MageArmor.Callback ), new CraftState( m_Book, 18 )); break;
				case 19: from.BeginTarget( 1, false, TargetFlags.None, new TargetStateCallback( MageWeapon.Callback ), new CraftState( m_Book, 19 )); break;
				case 20: from.BeginTarget( 1, false, TargetFlags.None, new TargetStateCallback( SpellChanneling.Callback ), new CraftState( m_Book, 20 )); break;
				case 21: from.BeginTarget( 1, false, TargetFlags.None, new TargetStateCallback( SpellDamage.Callback ), new CraftState( m_Book, 21 )); break;
				case 22: from.BeginTarget( 1, false, TargetFlags.None, new TargetStateCallback( HitColdArea.Callback ), new CraftState( m_Book, 22 )); break;
				case 23: from.BeginTarget( 1, false, TargetFlags.None, new TargetStateCallback( HitEnergyArea.Callback ), new CraftState( m_Book, 23 )); break;
				case 24: from.BeginTarget( 1, false, TargetFlags.None, new TargetStateCallback( HitFireArea.Callback ), new CraftState( m_Book, 24 )); break;
				case 25: from.BeginTarget( 1, false, TargetFlags.None, new TargetStateCallback( HitPhysicalArea.Callback ), new CraftState( m_Book, 25 )); break;
				case 26: from.BeginTarget( 1, false, TargetFlags.None, new TargetStateCallback( HitPoisonArea.Callback ), new CraftState( m_Book, 26 )); break;
				case 27: from.BeginTarget( 1, false, TargetFlags.None, new TargetStateCallback( HitDispel.Callback ), new CraftState( m_Book, 27 )); break;
				case 28: from.BeginTarget( 1, false, TargetFlags.None, new TargetStateCallback( HitFireball.Callback ), new CraftState( m_Book, 28 )); break;
				case 29: from.BeginTarget( 1, false, TargetFlags.None, new TargetStateCallback( HitHarm.Callback ), new CraftState( m_Book, 29 )); break;
				case 30: from.BeginTarget( 1, false, TargetFlags.None, new TargetStateCallback( HitLightning.Callback ), new CraftState( m_Book, 30 )); break;
				case 31: from.BeginTarget( 1, false, TargetFlags.None, new TargetStateCallback( HitMagicArrow.Callback ), new CraftState( m_Book, 31 )); break;
				case 32: from.BeginTarget( 1, false, TargetFlags.None, new TargetStateCallback( HitLowerAttack.Callback ), new CraftState( m_Book, 32 )); break;
				case 33: from.BeginTarget( 1, false, TargetFlags.None, new TargetStateCallback( HitLowerDefend.Callback ), new CraftState( m_Book, 33 )); break;
				case 34: from.BeginTarget( 1, false, TargetFlags.None, new TargetStateCallback( HitLeechHits.Callback ), new CraftState( m_Book, 34 )); break;
				case 35: from.BeginTarget( 1, false, TargetFlags.None, new TargetStateCallback( HitLeechMana.Callback ), new CraftState( m_Book, 35 )); break;
				case 36: from.BeginTarget( 1, false, TargetFlags.None, new TargetStateCallback( HitLeechStam.Callback ), new CraftState( m_Book, 36 )); break;
				case 37: from.BeginTarget( 1, false, TargetFlags.None, new TargetStateCallback( UseBestSkill.Callback ), new CraftState( m_Book, 37 )); break;
				case 38: from.BeginTarget( 1, false, TargetFlags.None, new TargetStateCallback( WeaponDamage.Callback ), new CraftState( m_Book, 38 )); break;
				case 39: from.BeginTarget( 1, false, TargetFlags.None, new TargetStateCallback( WeaponSpeed.Callback ), new CraftState( m_Book, 39 )); break;
				case 40: from.BeginTarget( 1, false, TargetFlags.None, new TargetStateCallback( AttackChance.Callback ), new CraftState( m_Book, 40 )); break;
				case 41: from.BeginTarget( 1, false, TargetFlags.None, new TargetStateCallback( DefendChance.Callback ), new CraftState( m_Book, 41 )); break;
				case 42: from.BeginTarget( 1, false, TargetFlags.None, new TargetStateCallback( EnhancePotions.Callback ), new CraftState( m_Book, 42 )); break;
				case 43: from.BeginTarget( 1, false, TargetFlags.None, new TargetStateCallback( LowerStatReq.Callback ), new CraftState( m_Book, 43 )); break;
				case 44: from.BeginTarget( 1, false, TargetFlags.None, new TargetStateCallback( Luck.Callback ), new CraftState( m_Book, 44 )); break;
				case 45: from.BeginTarget( 1, false, TargetFlags.None, new TargetStateCallback( ReflectPhysical.Callback ), new CraftState( m_Book, 45 )); break;
				case 46: from.BeginTarget( 1, false, TargetFlags.None, new TargetStateCallback( SelfRepair.Callback ), new CraftState( m_Book, 46 )); break;
				case 47: from.BeginTarget( 1, false, TargetFlags.None, new TargetStateCallback( NightSight.Callback ), new CraftState( m_Book, 47 )); break;
				case 48: from.BeginTarget( 1, false, TargetFlags.None, new TargetStateCallback( Slayer.Callback ), new CraftState( m_Book, 48 )); break;
				case 49: from.BeginTarget( 1, false, TargetFlags.None, new TargetStateCallback( Durability.Callback ), new CraftState( m_Book, 49 )); break;
				default: break;
			}

			// Retrieve the current page from the button that was used to get here, and reopen the book.

			List<Server.Gumps.GumpEntry> entries = Entries;
			int page = 1;

			for( int i = entries.Count - 1; i >= 0; i-- )
			{
				GumpEntry e = entries[ i ];
				if ( e is GumpButton && ((GumpButton)e).ButtonID == craft + 1 )
				{
					page = ((GumpButton)e).Param;
					break;
				}
			}
			from.SendGump( new SpellCraftBook( from, m_Book, page ) );
		}
	}
}