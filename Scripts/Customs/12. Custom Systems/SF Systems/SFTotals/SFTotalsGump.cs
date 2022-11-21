using System;
using System.Collections;
using Server;
using Server.Items;
using Server.Guilds;
using Server.Commands;
using Server.Network;
using Server.Mobiles;
using Server.Accounting;
//namespace Server.Misc

namespace Server.Gumps
{
	public class SFTotalsGump : Gump
	{
		public SFTotalsGump( Mobile from ) : base( 200, 120 )
		{
			Mobile m_from = from;

            // Attribute Caps
            double BandageSpeedCap = 2.0;
            int SwingSpeedCap = 100;

			string myname = m_from.Name;
			int myfame = m_from.Fame;
			int mykarma = m_from.Karma;

			int mystr = m_from.Str;
			int mydex = m_from.Dex;
			int myint = m_from.Int;
			int mybonusstr = (AosAttributes.GetValue(m_from, AosAttribute.BonusStr));
			int mybonusdex = (AosAttributes.GetValue(m_from, AosAttribute.BonusDex));
			int mybonusint = (AosAttributes.GetValue(m_from, AosAttribute.BonusInt));

			int myhits = m_from.HitsMax;
			int mystam = m_from.StamMax;
			int mymana = m_from.ManaMax;
			int mybonushits = (AosAttributes.GetValue(m_from, AosAttribute.BonusHits));
			int mybonusstam = (AosAttributes.GetValue(m_from, AosAttribute.BonusStam));
			int mybonusmana = (AosAttributes.GetValue(m_from, AosAttribute.BonusMana));

			int myhunger = m_from.Hunger;
			int mythirst = m_from.Thirst;

			int myattack = (AosAttributes.GetValue(m_from, AosAttribute.AttackChance));
			int mycastrecovery = (AosAttributes.GetValue(m_from, AosAttribute.CastRecovery));
			int mycastspeed = (AosAttributes.GetValue(m_from, AosAttribute.CastSpeed));
			int mydefend = (AosAttributes.GetValue(m_from, AosAttribute.DefendChance));
			int myenhancepotion = (AosAttributes.GetValue(m_from, AosAttribute.EnhancePotions));
			int mylmc = (AosAttributes.GetValue(m_from, AosAttribute.LowerManaCost));
			int mylrc = (AosAttributes.GetValue(m_from, AosAttribute.LowerRegCost));
			int myluck = (AosAttributes.GetValue(m_from, AosAttribute.Luck));

			int mysight = (AosAttributes.GetValue(m_from, AosAttribute.NightSight));
			int myreflect = (AosAttributes.GetValue(m_from, AosAttribute.ReflectPhysical));
			int myregenhit = (AosAttributes.GetValue(m_from, AosAttribute.RegenHits));
			int myregenmana = (AosAttributes.GetValue(m_from, AosAttribute.RegenMana));
			int myregenstam = (AosAttributes.GetValue(m_from, AosAttribute.RegenStam));
			int myspelldamage = (AosAttributes.GetValue(m_from, AosAttribute.SpellDamage));
			int DamageIncrease = (AosAttributes.GetValue( m_from, AosAttribute.WeaponDamage));
			int myweaponspeed = (AosAttributes.GetValue(m_from, AosAttribute.WeaponSpeed));

			double BandageSpeed = ( 2.0 + (0.5 * ((double)(205 - from.Dex) / 10)) ) < BandageSpeedCap ? BandageSpeedCap : ( 2.0 + (0.5 * ((double)(205 - from.Dex) / 10)) );
			TimeSpan SwingSpeed = (from.Weapon as BaseWeapon).GetDelay(from) > TimeSpan.FromSeconds(SwingSpeedCap) ? TimeSpan.FromSeconds(SwingSpeedCap) : (from.Weapon as BaseWeapon).GetDelay(from);

			PlayerMobile pm_from = (PlayerMobile)from;
			int mytimemins = pm_from.GameTime.Minutes;
			int mytime = pm_from.GameTime.Hours;
			int mytimedays = pm_from.GameTime.Days;

			TimeSpan ctime=DateTime.Now - pm_from.SessionStart;
			string curr = ctime.Hours.ToString() + " : " + ctime.Minutes.ToString() + " : " + ctime.Seconds.ToString();

			this.Closable=true;
			this.Disposable=true;
			this.Dragable=true;
			this.Resizable=false;

			this.AddPage(0);
			this.AddBackground(0, 0, 460, 502, 5054);
			//this.AddAlphaRegion( 11, 11, 189, 479 );
			//this.AddAlphaRegion( 207, 11, 241, 479 );

			if ( pm_from.Female != true && pm_from.Race == Race.Human ) {  this.AddImage(16, -28, 12); }
			if ( pm_from.Female != false && pm_from.Race == Race.Human ) {  this.AddImage(16, -28, 13); }
			if ( pm_from.Female != true && pm_from.Race == Race.Elf ) {  this.AddImage(16, -28, 14); }
			if ( pm_from.Female != false && pm_from.Race == Race.Elf ) {  this.AddImage(16, -28, 15); }
			if ( pm_from.Female != true && pm_from.Race == Race.Gargoyle ) {  this.AddImage(16, -30, 666); }
			if ( pm_from.Female != false && pm_from.Race == Race.Gargoyle ) {  this.AddImage(16, -30, 665); }
            this.AddImage(16, -28, 60469, 1077);

			this.AddLabel(50, 224, 1193, "- Character Stats -" );
            this.AddItem(17, 209, 16154);
            this.AddItem(162, 209, 16153);
			this.AddLabel(335, 470, 1193, "Character Sheet" );

			this.AddLabel(85, 199, 93, myname );
			this.AddLabel(31, 257, 2005, "Strength" );
			this.AddLabel(31, 273, 2005, "Dexterity" );
			this.AddLabel(31, 289, 2005, "Intelligence" );
			this.AddLabel(31, 312, 2005, "Hit Points" );
			this.AddLabel(31, 328, 2005, "Stamina" );
			this.AddLabel(31, 344, 2005, "Mana" );

			this.AddLabel(31, 366, 2005, "Fame");
            this.AddLabel(31, 384, 2005, "Karma");

			this.AddLabel(16, 404, 300, "Game Time");
			this.AddLabel(16, 420, 300, "This Session");
			
			/*
			this.AddLabel(105, 257, 2005, ":" );
			this.AddLabel(105, 273, 2005, ":" );
			this.AddLabel(105, 289, 2005, ":" );
			this.AddLabel(105, 312, 2005, ":" );
			this.AddLabel(105, 328, 2005, ":" );
			this.AddLabel(105, 344, 2005, ":" );
			this.AddLabel(105, 366, 2005, ":" );
			this.AddLabel(105, 384, 2005, ":" );
			this.AddLabel(85, 404, 2005, ":" );
			this.AddLabel(95, 420, 2005, ":" );
			*/

			this.AddImage(212, 16, 11057, 1194);
			this.AddImage(212, 63, 11057, 1194);
			this.AddImage(212, 110, 11057, 1194);
			this.AddImage(212, 157, 11057, 1194);
			this.AddImage(212, 204, 11057, 1194);
			this.AddImage(212, 251, 11057, 1194);
			this.AddImage(212, 298, 11057, 1194);
			this.AddImage(212, 345, 11057, 1194);
			this.AddImage(212, 392, 11057, 1194);
			this.AddImage(212, 439, 11057, 1194);
			
			this.AddLabel(262, 13, 1193, "- Equipment Bonus -" );
            this.AddItem(383, 18, 7942);
            this.AddItem(221, 12, 5145);
            this.AddItem(392, 15, 7943);
            this.AddItem(383, 24, 7945);

			this.AddLabel(251, 43, 2005, "HP Regen");
			this.AddLabel(251, 63, 2005, "Stam Regen");
			this.AddLabel(251, 83, 2005, "Mana Regen");

			this.AddLabel(251, 113, 2005, "Hit Chance Inc");
			this.AddLabel(251, 133, 2005, "Def Chance Inc");

			this.AddLabel(251, 163, 2005, "Faster Casting");
			this.AddLabel(251, 183, 2005, "Faster Cast Rec");

			this.AddLabel(251, 213, 2005, "Lower Mana Cost");
			this.AddLabel(251, 233, 2005, "Lower Reg Cost");
			this.AddLabel(251, 253, 2005, "Spell Damage Inc");

			this.AddLabel(251, 283, 2005, "Damage Inc");
			this.AddLabel(251, 303, 2005, "Swing Speed Incr");

			this.AddLabel(251, 333, 2005, "Reflect Phys");
			this.AddLabel(251, 353, 2005, "Enhance Potions");

			this.AddLabel(251, 383, 2005, "Luck");

			this.AddLabel(251, 413, 2005, "Bandage Speed");
			this.AddLabel(251, 433, 2005, "Swing Speed");
			
			/*
			this.AddLabel(370, 43, 2005, ":" );
			this.AddLabel(370, 63, 2005, ":" );
			this.AddLabel(370, 83, 2005, ":" );
			this.AddLabel(370, 113, 2005, ":" );
			this.AddLabel(370, 133, 2005, ":" );
			this.AddLabel(370, 163, 2005, ":" );
			this.AddLabel(370, 183, 2005, ":" );
			this.AddLabel(370, 213, 2005, ":" );
			this.AddLabel(370, 233, 2005, ":" );
			this.AddLabel(370, 253, 2005, ":" );
			this.AddLabel(370, 283, 2005, ":" );
			this.AddLabel(370, 303, 2005, ":" );
			this.AddLabel(370, 333, 2005, ":" );
			this.AddLabel(370, 353, 2005, ":" );
			this.AddLabel(370, 383, 2005, ":" );
			this.AddLabel(370, 413, 2005, ":" );
			this.AddLabel(370, 433, 2005, ":" );
			*/

			this.AddItem(15, 452, 4155);
			this.AddItem(11, 471, 4095);

			if ( myhunger <= 1 ) { this.AddLabel(53, 449, 37, "You are starving!"); }
			if ( myhunger > 1 && myhunger <= 5 ) { this.AddLabel(53, 449, 40, "You are extremely hungry"); }
			if ( myhunger > 5 && myhunger <= 10 ) { this.AddLabel(53, 449, 50, "You feel ravenous"); }
			if ( myhunger > 10 && myhunger <= 12 ) { this.AddLabel(53, 449, 56, "You feel a bit peckish"); }
			if ( myhunger > 12 && myhunger <= 15 ) { this.AddLabel(53, 449, 65, "You feel full up"); }
			if ( myhunger > 15  ) { this.AddLabel(53, 449, 76, "You are stuffed!"); }

			if ( mythirst <= 1 ) { this.AddLabel(53, 465, 37, "You are very dehydrated!"); }
			if ( mythirst > 1 && mythirst <= 5 ) { this.AddLabel(53, 465, 40, "You are extremely thirsty"); }
			if ( mythirst > 5 && mythirst <= 10 ) { this.AddLabel(53, 465, 50, "You're feeling very dry"); }
			if ( mythirst > 10 && mythirst <= 12 ) { this.AddLabel(53, 465, 56, "You are a bit thirsty"); }
			if ( mythirst > 12 && mythirst <= 15 ) { this.AddLabel(53, 465, 65, "You feel fine"); }
			if ( mythirst > 15 ) { this.AddLabel(53, 465, 76, "You have drunk plenty"); }

			Account xx = ((Mobile)pm_from).Account as Account;

			this.AddLabel(112, 257, 1761, ": " + mystr.ToString() );
			this.AddLabel(158, 257, 1760, "(+" + mybonusstr.ToString() + ")" );
			this.AddLabel(112, 273, 1761, ": " + mydex.ToString() );
			this.AddLabel(158, 273, 1760, "(+" + mybonusdex.ToString() + ")" );
			this.AddLabel(112, 289, 1761, ": " + myint.ToString() );
			this.AddLabel(158, 289, 1760, "(+" + mybonusint.ToString() + ")" );

			this.AddLabel(112, 312, 1761, ": " + myhits.ToString() );
			this.AddLabel(158, 312, 1760, "(+" + mybonushits.ToString() + ")" );
			this.AddLabel(112, 328, 1761, ": " + mystam.ToString() );
			this.AddLabel(158, 328, 1760, "(+" + mybonusstam.ToString() + ")" );
			this.AddLabel(112, 344, 1761, ": " + mymana.ToString() );
			this.AddLabel(158, 344, 1760, "(+" + mybonusmana.ToString() + ")" );

			this.AddLabel(112, 366, 1761, ": " + myfame.ToString());
			this.AddLabel(112, 384, 1761, ": " + mykarma.ToString());

			this.AddLabel(95, 404, 1761, ": " + mytimedays.ToString() + "d ~" + mytime.ToString() + " : " + mytimemins.ToString() );
			this.AddLabel(95, 420, 1761, ": " + curr );   

			this.AddLabel(390, 43, 1761, ": " + myregenhit.ToString());
			this.AddLabel(390, 63, 1761, ": " + myregenstam.ToString());
			this.AddLabel(390, 83, 1761, ": " + myregenmana.ToString());

			this.AddLabel(390, 113, 1761, ": " + myattack.ToString());
			this.AddLabel(390, 133, 1761, ": " + mydefend.ToString());

			this.AddLabel(390, 163, 1761, ": " + mycastspeed.ToString());
			this.AddLabel(390, 183, 1761, ": " + mycastrecovery.ToString());

			this.AddLabel(390, 213, 1761, ": " + mylmc.ToString());
			this.AddLabel(390, 233, 1761, ": " + mylrc.ToString());
			this.AddLabel(390, 253, 1761, ": " + myspelldamage.ToString());

			this.AddLabel(390, 283, 1761, ": " + DamageIncrease.ToString());
			this.AddLabel(390, 303, 1761, ": " + myweaponspeed.ToString());

			this.AddLabel(390, 333, 1761, ": " + myreflect.ToString());
			this.AddLabel(390, 353, 1761, ": " + myenhancepotion.ToString());

			this.AddLabel(390, 383, 1761, ": " + myluck.ToString());

			this.AddLabel(390, 413, 1761, ": " + String.Format("{0:0.0}s", new DateTime(TimeSpan.FromSeconds( BandageSpeed ).Ticks).ToString("s.ff") ) );
			this.AddLabel(390, 433, 1761, ": " + String.Format("{0}s", new DateTime(SwingSpeed.Ticks).ToString("s.ff") ) );
		}
	}
}