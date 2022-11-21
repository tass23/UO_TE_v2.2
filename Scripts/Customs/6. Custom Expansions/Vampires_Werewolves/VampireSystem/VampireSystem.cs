///////////////////////////////////////////////
// Welcome in Freyd's Vampire System!        //	Adapted By Raist for UO-The Expanse with Werewolves
///////////////////////////////////////////////
using System;
using System.Collections;
using System.Collections.Generic;
using Server;
using Server.Mobiles;
using Server.Network;
using Server.Commands;
using Server.Multis;
using Server.Items;
using Server.Gumps;
using Server.Targeting;

namespace Server.Misc
{
	public class VampireSystem
	{
		//Setup Section
		private static bool m_Vampires = true;				// If False, after LOGOUT players with Vampirism or Lycanthropy
		private static bool m_Werewolves = true;			// will lose the buffs and revert back to being "regular" Human, Elf, or Gargoyle.
															// THIS IS A BASIC RESET FOR ALL PLAYERS
		private static bool m_VEnabled = true;				// If True, players can become Vampires or Werewolves.
		private static bool m_WEnabled = true;				// Setting one or the other to False prevents the spread of that disease BY PLAYERS ONLY & NOT NPCs.
		private static bool m_VSpecialBiteEnabled = true;   // If True after biting, a Vampire/Werewolf will get 10 Hit Points and Stamina
		private static bool m_WSpecialBiteEnabled = true;
		public static double VampireChance = 0.10;			// Chance to become Vampire (1.00 = 100%)
		public static double WerewolfChance = 0.10;			// Chance to become a Werewolf (1.00 = 100%)
		private static bool m_ShowTitles = true;			// Showing "the Vampire" and "the Werewolf" titles
		private static bool m_Enemies = true;				// If True, Vampires and Werewolves are an enemies to each other and this includes NPCs!
		public static double VampireBiteChance = 0.05;		// Chance to bite player on every attack (1.00 = 100%)
		public static double WerewolfBiteChance = 0.05;
		private static int m_VampireStrBonus = 4;           // Strength bonus for Vampires
		private static int m_WerewolfStr = 8;				// Strenght bonus for Werewolves
		private static int m_VampireDexBonus = 6;           // Dexterity bonus for Vampires
        private static int m_WerewolfDex = 6;				// Dexterity bonus for Werewolves
		private static int m_VampireIntBonus = 8;           // Inteligence bonus for Vampires
        private static int m_WerewolfInt = 4;				// Inteligence bonus for Werewolves
		private static int m_VamMin = 1;					// Minimum damage bonus caused TO Werewolve OPPONENTS
		private static int m_WerewolfMin = 1;				// Minimum damage bonus caused TO Vampire OPPONENTS
		private static int m_VamMax = 10;					// Maximum bonus damage caused TO Werewolve OPPONENTS
		private static int m_WerewolfMax = 10;				// Maximum bonus damage caused TO Vampire OPPONENTS

		public static bool Vampires { get { return m_Vampires; } }
		public static bool Werewolves{ get { return m_Werewolves; } }
		public static bool VEnabled { get { return m_VEnabled; } }
		public static bool WEnabled { get { return m_WEnabled; } }
		public static bool ShowTitles { get { return m_ShowTitles; } } 
		public static bool Enemies { get { return m_Enemies; } }
		public static bool VSpecialBiteEnabled { get { return m_VSpecialBiteEnabled; } }
		public static bool WSpecialBiteEnabled { get { return m_WSpecialBiteEnabled; } }
		public static int VampireStrBonus { get { return m_VampireStrBonus; } }
		public static int WerewolfStr { get { return m_WerewolfStr; } }
		public static int VampireDexBonus { get { return m_VampireDexBonus; } }
        public static int WerewolfDex { get { return m_WerewolfDex; } }
		public static int VampireIntBonus { get { return m_VampireIntBonus; } }
        public static int WerewolfInt { get { return m_WerewolfInt; } }
		public static int VamMin { get { return m_VamMin; } }
		public static int WerewolfMin { get { return m_WerewolfMin; } }
		public static int VamMax { get { return m_VamMax; } }
		public static int WerewolfMax { get { return m_WerewolfMax; } }

		public static void Initialize()
		{           
			CommandSystem.Register("Vampire", AccessLevel.Player, new CommandEventHandler(Vampire_OnCommand));
			CommandSystem.Register("Werewolf", AccessLevel.Player, new CommandEventHandler(Werewolf_OnCommand));
		}

		[Usage("Vampire")]
		[Description("Vampire Gump")]
		private static void Vampire_OnCommand(CommandEventArgs e)
		{
			PlayerMobile from = e.Mobile as PlayerMobile;
			if (from.Vampire > 0 || from.AccessLevel > AccessLevel.Player) 
			{
				from.CloseGump( typeof( VampireGump ) );
				from.SendGump( new VampireGump() );
			}
			else
			{
				from.SendMessage("Only Vampires are allowed to use this command.");
			}
		}  
		
		[Usage("Werewolf")]
		[Description("Werewolf Gump")]
		private static void Werewolf_OnCommand(CommandEventArgs e)
		{
			PlayerMobile from = e.Mobile as PlayerMobile;
			if (from.Werewolf > 0 || from.AccessLevel > AccessLevel.Player) 
			{
				from.CloseGump( typeof( WerewolfGump ) );
				from.SendGump( new WerewolfGump() );
			}
			else
			{
				from.SendMessage("Only Werewolves are allowed to use this command.");
			}
		} 
	}
	public class VampireTimer : Timer
	{
		private int m_Ticker;
		private Mobile m_Mobile;

		public VampireTimer(Mobile m, double t) : base(TimeSpan.FromMinutes(60.0), TimeSpan.FromMinutes(60.0))
		{
			m_Mobile = m;
			Priority = TimerPriority.TwoFiftyMS;
			m_Ticker = 120;
		}

		protected override void OnTick()
		{
			try
			{
				switch (m_Ticker)
				{                        
				case 120:
					PlayerMobile from = m_Mobile as PlayerMobile;
					if (from.VampireBiteTime == TimeSpan.Zero)
					{
						from.SendMessage(33, "You need drink some blood...");
						from.ApplyPoison(from, Poison.Lesser);
					}
					break;
				case 90:
					PlayerMobile froma = m_Mobile as PlayerMobile;
					if (froma.VampireBiteTime == TimeSpan.Zero)
					{
						froma.SendMessage(33, "You need drink some blood...");
						froma.ApplyPoison(froma, Poison.Regular);
					}
					break;
				case 60:
					PlayerMobile fromb = m_Mobile as PlayerMobile;
					if (fromb.VampireBiteTime == TimeSpan.Zero)
					{
						fromb.SendMessage(33, "You need drink some blood...");
						fromb.ApplyPoison(fromb, Poison.Greater);
					}
					break;
				case 30:
					PlayerMobile fromc = m_Mobile as PlayerMobile;
					if (fromc.VampireBiteTime == TimeSpan.Zero)
					{
						fromc.SendMessage(33, "You need drink some blood very soon or you will die.");
						fromc.ApplyPoison(fromc, Poison.Deadly);
					}
					break;
				case 0:
					PlayerMobile fromd = m_Mobile as PlayerMobile;
					if (fromd.VampireBiteTime == TimeSpan.Zero)
					{
						fromd.SendMessage(33, "Time is running out to drink blood before you die.");
						fromd.ApplyPoison(fromd, Poison.Lethal);
					}
					Stop();
					break;                        
				}
			}
			catch
			{
				Console.WriteLine("OnTick(), switch(m_Ticker) Try/Catch Error");
			}
			m_Ticker--;
		}        
	}
	
	public class WerewolfTimer : Timer
	{
		private int m_WTicker;
		private Mobile m_Mobile;

		public WerewolfTimer(Mobile m, double t) : base(TimeSpan.FromMinutes(60.0), TimeSpan.FromMinutes(60.0))
		{
			m_Mobile = m;
			Priority = TimerPriority.TwoFiftyMS;
			m_WTicker = 120;
		}

		protected override void OnTick()
		{
			try
			{
				switch (m_WTicker)
				{                        
				case 120:
					PlayerMobile from = m_Mobile as PlayerMobile;
					if (from.WerewolfBiteTime == TimeSpan.Zero)
					{
						from.SendMessage(33, "You should find some raw meat...");
						from.ApplyPoison(from, Poison.Lesser);
					}
					break;
				case 90:
					PlayerMobile froma = m_Mobile as PlayerMobile;
					if (froma.WerewolfBiteTime == TimeSpan.Zero)
					{
						froma.SendMessage(33, "You should find some raw meat...");
						froma.ApplyPoison(froma, Poison.Regular);
					}
					break;
				case 60:
					PlayerMobile fromb = m_Mobile as PlayerMobile;
					if (fromb.WerewolfBiteTime == TimeSpan.Zero)
					{
						fromb.SendMessage(33, "You should find some raw meat...");
						fromb.ApplyPoison(fromb, Poison.Greater);
					}
					break;
				case 30:
					PlayerMobile fromc = m_Mobile as PlayerMobile;
					if (fromc.WerewolfBiteTime == TimeSpan.Zero)
					{
						fromc.SendMessage(33, "You need to eat some meat soon or you will die.");
						fromc.ApplyPoison(fromc, Poison.Deadly);
					}
					break;
				case 0:
					PlayerMobile fromd = m_Mobile as PlayerMobile;
					if (fromd.WerewolfBiteTime == TimeSpan.Zero)
					{
						fromd.SendMessage(33, "Time is running out to eat some meat before you die.");
						fromd.ApplyPoison(fromd, Poison.Lethal);
					}
					Stop();
					break;                        
				}
			}
			catch
			{
				Console.WriteLine("OnTick(), switch(m_WTicker) Try/Catch Error");
			}
			m_WTicker--;
		}        
	}
}

//===============Gump=================	by Raist for UO-The Expanse
namespace Server.Gumps
{
	public class VampireGump : Gump
	{
		public static int[] m_IDs = new int[]
		{
			0xC7,	//Gouzen Ha body 199
			0x13D,	//Vampire Bat body 317
			0x5		//Eagle body 5
		};
		
		public VampireGump() : base( 0, 0 )
		{
            this.Closable=true;
			this.Disposable=true;
			this.Dragable=true;
			this.Resizable=false;
			AddPage(0);
			AddBackground(0, 0, 147, 117, 9200);
			AddAlphaRegion(0, 0, 147, 117);
			AddImageTiled(3, 3, 140, 21, 30074);
			AddLabel(20, 4, 0, @"Vampire Abilities");
			AddImage(5, 22, 9500);
			AddImageTiled(16, 22, 115, 12, 9501);
			AddImage(131, 22, 9502);
			AddImageTiled(5, 33, 135, 79, 9504);
			AddImage(5, 103, 9506);
			AddImage(130, 103, 9508);
			AddLabel(13, 26, 0, @"Forms");
			AddLabel(90, 42, 0, @"Bite");
			AddButton(78, 65, 2128, 2129, (int)Buttons.BiteButton, GumpButtonType.Reply, 0);
			AddLabel(27, 43, 0, @"Bat");
			AddButton(10, 47, 1209, 1210, (int)Buttons.FormButton, GumpButtonType.Reply, 0);
			AddLabel(27, 65, 0, @"Fog");
			AddButton(10, 69, 1209, 1210, (int)Buttons.FormButton2, GumpButtonType.Reply, 0);
			AddLabel(27, 88, 0, @"Raven");
			AddButton(10, 92, 1209, 1210, (int)Buttons.FormButton3, GumpButtonType.Reply, 0);
			AddImageTiled(13, 42, 39, 1, 30002);
			AddImageTiled(91, 58, 25, 1, 30002);
		}
		
		public enum Buttons
		{
			None,
			FormButton,
			FormButton2,
			FormButton3,
			BiteButton,
		}
		
		public override void OnResponse(NetState sender, RelayInfo info)
		{
			Mobile from = sender.Mobile;
			if (info.ButtonID == (int)Buttons.FormButton)
			{
				VampireForm(from);
				from.SendGump( new VampireGump() );
			}
			if (info.ButtonID == (int)Buttons.FormButton2)
			{
				VampireForm2(from);
				from.SendGump( new VampireGump() );
			}
			if (info.ButtonID == (int)Buttons.FormButton3)
			{
				VampireForm3(from);
				from.SendGump( new VampireGump() );
			}
			else if (info.ButtonID == (int)Buttons.BiteButton)
			{
				VampireGump vg = new VampireGump();
				from.SendMessage("Bite whom?");
				from.Target = new BiteTarget(vg);
				from.SendGump( vg );
			}
		}
		
		public virtual void VampireForm( Mobile mob )
		{
			PlayerMobile from = (PlayerMobile) mob;
			
			if (Factions.Sigil.ExistsOn(from))
			{
				from.SendLocalizedMessage(1010521); // You cannot polymorph while you have a Town Sigil
			}            
			else if (from.Vampire != 0 || from.AccessLevel > AccessLevel.Player)
			{
				if (from.BodyMod == 0)
				{
					if (from.Mount == null)
					{						
						from.BodyMod = 0x13D;     //Vampire Bat 317
						//from.HueMod = 1109;
						from.Send(SpeedControl.MountSpeed);
						//this drops items TO THE GROUND so the Vamp can travel lightly.
						/*{
							Backpack bag = new Backpack();
							Container pack = from.Backpack;
							ArrayList equipitems = new ArrayList(from.Items);
							ArrayList bagitems = new ArrayList( pack.Items );
				
							foreach (Item item in equipitems)
							{
								if ((item.Insured == false) && (item.LootType != LootType.Blessed) && (item.Layer != Layer.OuterTorso) && (item.Layer != Layer.Bank) && (item.Layer != Layer.Backpack) && (item.Layer != Layer.Hair) && (item.Layer != Layer.FacialHair))
								{
									pack.DropItem( item );
								}
							}
					
							foreach (Item items in bagitems)
							{
								if ((items.Insured == false) && (items.LootType != LootType.Blessed))
								{
									bag.DropItem(items);
								}
							}
							from.SendMessage("You drop everything you can't carry.");
							bag.MoveToWorld (from.Location, from.Map);
						}*/
						from.PlaySound(0x201);
						from.FixedParticles(0x3728, 1, 10, 9910, EffectLayer.Head);
						from.PlaySound(0x370);	//Genie sound 0x371
					}
					else
					{
						from.SendMessage("You cannot polymorph while mounted.");
					}
				}
				else if (from.BodyMod == 0x13D)
				{
					from.BodyMod = 0;      
					//from.HueMod = 0x847E;
					from.Send(SpeedControl.Disable);
					from.PlaySound(0x201);
					from.FixedParticles(0x3728, 1, 10, 9910, EffectLayer.Head);
				} 
				else
				{
					from.SendMessage("You cannot polymorph while mounted.");
				}
			}            
		}
		
		public virtual void VampireForm2( Mobile mob )
		{
			PlayerMobile from = (PlayerMobile) mob;
			
			if (Factions.Sigil.ExistsOn(from))
			{
				from.SendLocalizedMessage(1010521); // You cannot polymorph while you have a Town Sigil
			}            
			else if (from.Vampire != 0 || from.AccessLevel > AccessLevel.Player)
			{
				if (from.BodyMod == 0)
				{
					if (from.Mount == null)
					{						
						from.BodyMod = 0x111;     //Fetid Essence 273
						from.HueMod = 1153;
						from.Send(SpeedControl.MountSpeed);
						//this drops items TO THE GROUND so the Vamp can travel lightly.
						/*{
							Backpack bag = new Backpack();
							Container pack = from.Backpack;
							ArrayList equipitems = new ArrayList(from.Items);
							ArrayList bagitems = new ArrayList( pack.Items );
				
							foreach (Item item in equipitems)
							{
								if ((item.Insured == false) && (item.LootType != LootType.Blessed) && (item.Layer != Layer.OuterTorso) && (item.Layer != Layer.Bank) && (item.Layer != Layer.Backpack) && (item.Layer != Layer.Hair) && (item.Layer != Layer.FacialHair))
								{
									pack.DropItem( item );
								}
							}
					
							foreach (Item items in bagitems)
							{
								if ((items.Insured == false) && (items.LootType != LootType.Blessed))
								{
									bag.DropItem(items);
								}
							}
							from.SendMessage("You drop everything you can't carry.");
							bag.MoveToWorld (from.Location, from.Map);
						}*/
						from.PlaySound(0x201);
						from.FixedParticles(0x3728, 1, 10, 9910, EffectLayer.Head);
						from.PlaySound(0x2BA); //Daemon Sound 0x2BB
					}
					else
					{
						from.SendMessage("You cannot polymorph while mounted.");
					}
				}
				else if (from.BodyMod == 0x111)
				{
					from.BodyMod = 0;      
					from.HueMod = 1153;
					from.Send(SpeedControl.Disable);
					from.PlaySound(0x201);
					from.FixedParticles(0x3728, 1, 10, 9910, EffectLayer.Head);
				} 
				else
				{
					from.SendMessage("You cannot polymorph while mounted.");
				}
			}            
		}
		
		public virtual void VampireForm3( Mobile mob )
		{
			PlayerMobile from = (PlayerMobile) mob;
			
			if (Factions.Sigil.ExistsOn(from))
			{
				from.SendLocalizedMessage(1010521); // You cannot polymorph while you have a Town Sigil
			}            
			else if (from.Vampire != 0 || from.AccessLevel > AccessLevel.Player)
			{
				if (from.BodyMod == 0)
				{
					if (from.Mount == null)
					{						
						from.BodyMod = 0x5;     
						from.HueMod = 1109;
						from.Send(SpeedControl.MountSpeed);
						//this drops items TO THE GROUND so the Vamp can travel lightly.
						/*{
							Backpack bag = new Backpack();
							Container pack = from.Backpack;
							ArrayList equipitems = new ArrayList(from.Items);
							ArrayList bagitems = new ArrayList( pack.Items );
				
							foreach (Item item in equipitems)
							{
								if ((item.Insured == false) && (item.LootType != LootType.Blessed) && (item.Layer != Layer.OuterTorso) && (item.Layer != Layer.Bank) && (item.Layer != Layer.Backpack) && (item.Layer != Layer.Hair) && (item.Layer != Layer.FacialHair))
								{
									pack.DropItem( item );
								}
							}
					
							foreach (Item items in bagitems)
							{
								if ((items.Insured == false) && (items.LootType != LootType.Blessed))
								{
									bag.DropItem(items);
								}
							}
							from.SendMessage("You drop everything you can't carry.");
							bag.MoveToWorld (from.Location, from.Map);
						}*/
						from.PlaySound(0x201);
						from.FixedParticles(0x3728, 1, 10, 9910, EffectLayer.Head);
						from.PlaySound(0x2F2); //Eagle sound 0x2F3
					}
					else
					{
						from.SendMessage("You cannot polymorph while mounted.");
					}
				}
				else if (from.BodyMod == 0x5)
				{
					from.BodyMod = 0;      
					from.HueMod = 0x847E;
					from.Send(SpeedControl.Disable);
					from.PlaySound(0x201);
					from.FixedParticles(0x3728, 1, 10, 9910, EffectLayer.Head);
				} 
				else
				{
					from.SendMessage("You cannot polymorph while mounted.");
				}
			}            
		}
		
		public virtual void Bite(Mobile mob, Mobile victim)
		{
			PlayerMobile from = (PlayerMobile) mob;
			bool CanUse = from.CheckSkill( SkillName.Anatomy, 20, 80 );
			
			if ((from.Vampire > 0 || from.AccessLevel > AccessLevel.Player) && (from.Mana > 10))
			{
				if (victim != null && (!(victim is PlayerMobile)) && (!(victim is BaseVampire)))
				{
					if ((victim.Race == Race.Human ||  victim.Race == Race.Elf) && CanUse || from.AccessLevel > AccessLevel.Player)
					{
						Effects.SendLocationParticles( EffectItem.Create( from.Location, from.Map, EffectItem.DefaultDuration ), 0x3728, 8, 20, 5042 );
						Effects.PlaySound( from, from.Map, 0x201 );
						from.Location = victim.Location;
						from.PlaySound(0x19C);
						from.FixedParticles(0x3709, 1, 30, 9904, 1108, 6, EffectLayer.Head); 
						from.SendMessage(33, "You bite your enemy!");
						from.VampireBiteTime = TimeSpan.FromHours(2.0);
						Server.Items.BleedAttack.BeginBleed(victim, from, true);
						victim.Combatant = from;
						from.Mana -= 10;
						from.Karma -= 100;
						from.Fame += 10;
						//from.HueMod = 0x847E;
						if (Server.Misc.VampireSystem.VSpecialBiteEnabled)
						{
							from.Hits += 25;
							if (from.Hits > from.HitsMax)
							from.Hits = from.HitsMax;
							from.Stam += 10;
							if (from.Stam > from.StamMax)
							from.Stam = from.StamMax;         
						}
					}
					else
					{
						Effects.SendLocationParticles( EffectItem.Create( from.Location, from.Map, EffectItem.DefaultDuration ), 0x3728, 8, 20, 5042 );
						Effects.PlaySound( from, from.Map, 0x201 );
						from.Location = victim.Location;
						from.FixedParticles(0x3709, 1, 30, 9904, 1108, 6, EffectLayer.Head);
						from.SendMessage("You try to bite your enemy, but fail.");
						from.Mana -= 5;
					}
				}
				else if (victim is PlayerMobile)
				{
					PlayerMobile combatant = (PlayerMobile) victim;
					//if (from.BodyMod == 0x13D)
					//{
					if (combatant.VampireBited < 1)
					{
						if (!combatant.Young)
						{
							if (CanUse || from.AccessLevel > AccessLevel.Player)
							{
								Effects.SendLocationParticles( EffectItem.Create( from.Location, from.Map, EffectItem.DefaultDuration ), 0x3728, 8, 20, 5042 );
								Effects.PlaySound( from, from.Map, 0x201 );
								from.Location = combatant.Location;
								from.FixedParticles(0x3709, 1, 30, 9904, 1108, 6, EffectLayer.Head);
								from.PlaySound(0x19C);
								combatant.VampireBited = 1;
								from.SendMessage(33, "You bite your enemy!");
								from.VampireBiteTime = TimeSpan.FromHours(2.0);
								Server.Items.BleedAttack.BeginBleed(combatant, from, true);
								combatant.Combatant = from;
								from.Mana -= 10;
								from.Karma -= 100;
								from.Fame += 10;
								//from.HueMod = 0x847E;
								if (combatant is PlayerMobile)
								{
									combatant.NonlocalOverheadMessage(MessageType.Regular, 0x21, 1060758, combatant.Name); // ~1_NAME~ is bleeding profusely
								}
								if (Server.Misc.VampireSystem.VSpecialBiteEnabled)
								{
									from.Hits += 25;
									if (from.Hits > from.HitsMax)
									from.Hits = from.HitsMax;
									from.Stam += 10;
									if (from.Stam > from.StamMax)
									from.Stam = from.StamMax;         
								}
								combatant.SendMessage(33, "A vampire bit you and you have started bleeding!");
								if (Server.Misc.VampireSystem.VEnabled && combatant.Vampire == 0 && Server.Misc.VampireSystem.VampireChance > Utility.RandomDouble() || Server.Misc.VampireSystem.VEnabled && combatant.Vampire == 0 && from.AccessLevel > AccessLevel.Player)
								{
									combatant.SendMessage(33, "You feel strange...");
									combatant.Vampire = 1;
									combatant.HueMod = 0x847E;
									combatant.Title = "the Vampire";
									combatant.AddStatMod(new StatMod(StatType.Str, "Vampire Str Bonus", 4, TimeSpan.Zero));
									combatant.AddStatMod(new StatMod(StatType.Dex, "Vampire Dex Bonus", 6, TimeSpan.Zero));
									combatant.AddStatMod(new StatMod(StatType.Int, "Vampire Int Bonus", 8, TimeSpan.Zero));
								}
							}
							else
							{
								Effects.SendLocationParticles( EffectItem.Create( from.Location, from.Map, EffectItem.DefaultDuration ), 0x3728, 8, 20, 5042 );
								Effects.PlaySound( from, from.Map, 0x201 );
								from.Location = combatant.Location;
								from.FixedParticles(0x3709, 1, 30, 9904, 1108, 6, EffectLayer.Head);
								from.SendMessage("You fail to bite your target.");
								from.Mana -= 5;
							}
						}
						else
						{
							from.SendMessage("You cannot bite Young players.");
						}
					}
					else
					{
						from.SendMessage("You cannot bite other vampires!");
					}
					//}
					//else
					//{                        
					//    from.SendMessage("You can bite your enemy only in Vampire bat form.");
					//}
				}
				else
				{
					from.SendMessage("You cannot bite that!"); //("You can bite only other players who fights WITH you.");
				}
			}
			else
			{
				if (from.Vampire < 1) from.SendMessage("Only vampires are allowed to use this command.");
				else if (from.Vampire > 1 && from.Mana < 10) from.SendMessage("You do not have enough Mana to do that!");
			}
		} 
		
		private class BiteTarget : Target
		{
			private VampireGump t_vg;
			
			public BiteTarget(VampireGump vg) : base( 6, false, TargetFlags.None )
			{
				t_vg = vg;
			}

			protected override void OnTarget( Mobile from, object targ )
			{
				if (targ is Mobile)
				{
					Mobile mob = (Mobile) targ;
					t_vg.Bite(from, mob);
				}
				else from.SendMessage("You cannot bite that!");
			}
		}
	}
	public class WerewolfGump : Gump
	{
		public WerewolfGump() : base( 0, 0 )
		{
            this.Closable=true;
			this.Disposable=true;
			this.Dragable=true;
			this.Resizable=false;

			AddPage(0);
			AddBackground(0, 0, 147, 101, 9200);
			AddAlphaRegion(0, 0, 146, 101);
			AddImageTiled(3, 3, 140, 21, 30072);
			AddLabel(14, 4, 0, @"Werewolf Abilities");
			AddImage(5, 22, 9500);
			AddImageTiled(16, 22, 115, 12, 9501);
			AddImage(131, 22, 9502);
			AddImageTiled(4, 33, 135, 65, 9504);
			AddImage(5, 87, 9506);
			AddImage(130, 87, 9508);
			AddLabel(13, 26, 0, @"Forms");
			AddLabel(100, 26, 0, @"Bite");
			AddButton(83, 45, 2128, 2129, (int)Buttons.BiteButton, GumpButtonType.Reply, 0);
			AddLabel(27, 46, 0, @"Wolf");
			AddButton(10, 49, 1210, 1209, (int)Buttons.FormButton, GumpButtonType.Reply, 0);
			AddLabel(27, 69, 0, @"Werewolf");
			AddButton(10, 73, 1210, 1209, (int)Buttons.FormButton2, GumpButtonType.Reply, 0);
			AddImageTiled(13, 42, 39, 1, 30002);
			AddImageTiled(100, 42, 25, 1, 30002);
		}
		
		public enum Buttons
		{
			None,
			FormButton,
			FormButton2,
			BiteButton,
		}
		
		public override void OnResponse(NetState sender, RelayInfo info)
		{
			Mobile from = sender.Mobile;
			if (info.ButtonID == (int)Buttons.FormButton)
			{
				WerewolfForm(from);
				from.SendGump( new WerewolfGump() );
			}
			if (info.ButtonID == (int)Buttons.FormButton2)
			{
				WerewolfForm2(from);
				from.SendGump( new WerewolfGump() );
			}
			else if (info.ButtonID == (int)Buttons.BiteButton)
			{
				WerewolfGump wwg = new WerewolfGump();
				from.SendMessage("Bite whom?");
				from.Target = new BiteTarget(wwg);
				from.SendGump( wwg );
			}
		}
		
		public virtual void WerewolfForm( Mobile mob )
		{
			PlayerMobile from = (PlayerMobile) mob;
			
			if (Factions.Sigil.ExistsOn(from))
			{
				from.SendLocalizedMessage(1010521); // You cannot polymorph while you have a Town Sigil
			}            
			else if (from.Werewolf != 0 || from.AccessLevel > AccessLevel.Player)
			{
				if (from.BodyMod == 0)
				{
					if (from.Mount == null)
					{
						from.BodyMod = 0xE1;     
						//from.HueMod = 1109;
						from.Send(SpeedControl.MountSpeed);
						//this drops items TO THE GROUND so the Werewolf can travel lightly.
						/*{
							Backpack bag = new Backpack();
							Container pack = from.Backpack;
							ArrayList equipitems = new ArrayList(from.Items);
							ArrayList bagitems = new ArrayList( pack.Items );
				
							foreach (Item item in equipitems)
							{
								if ((item.Insured == false) && (item.LootType != LootType.Blessed) && (item.Layer != Layer.OuterTorso) && (item.Layer != Layer.Bank) && (item.Layer != Layer.Backpack) && (item.Layer != Layer.Hair) && (item.Layer != Layer.FacialHair))
								{
									pack.DropItem( item );
								}
							}
					
							foreach (Item items in bagitems)
							{
								if ((items.Insured == false) && (items.LootType != LootType.Blessed))
								{
									bag.DropItem(items);
								}
							}
							from.SendMessage("You drop everything you can't carry.");
							bag.MoveToWorld (from.Location, from.Map);
						}*/
						from.PlaySound(0xE5);
						from.FixedParticles(0x3728, 1, 10, 9910, EffectLayer.Head);
						from.PlaySound(0x4E4);
					}
					else
					{
						from.SendMessage("You cannot polymorph while mounted.");
					}
				}
				else if (from.BodyMod == 0xE1)
				{
					from.BodyMod = 0;      
					//from.HueMod = 0x847E;
					from.Send(SpeedControl.Disable);
					from.PlaySound(0x4E4);
					from.FixedParticles(0x3728, 1, 10, 9910, EffectLayer.Head);
				} 
				else
				{
					from.SendMessage("You cannot polymorph while mounted.");
				}
			}            
		}
		
		public virtual void WerewolfForm2( Mobile mob )
		{
			PlayerMobile from = (PlayerMobile) mob;
			
			if (Factions.Sigil.ExistsOn(from))
			{
				from.SendLocalizedMessage(1010521); // You cannot polymorph while you have a Town Sigil
			}            
			else if (from.Werewolf != 0 || from.AccessLevel > AccessLevel.Player)
			{
				if (from.BodyMod == 0)
				{
					if (from.Mount == null)
					{
						from.BodyMod = 719;     
						//from.HueMod = 1109;
						from.Send(SpeedControl.MountSpeed);
						//this drops items TO THE GROUND so the Werewolf can travel lightly.
						/*{
							Backpack bag = new Backpack();
							Container pack = from.Backpack;
							ArrayList equipitems = new ArrayList(from.Items);
							ArrayList bagitems = new ArrayList( pack.Items );
				
							foreach (Item item in equipitems)
							{
								if ((item.Insured == false) && (item.LootType != LootType.Blessed) && (item.Layer != Layer.OuterTorso) && (item.Layer != Layer.Bank) && (item.Layer != Layer.Backpack) && (item.Layer != Layer.Hair) && (item.Layer != Layer.FacialHair))
								{
									pack.DropItem( item );
								}
							}
					
							foreach (Item items in bagitems)
							{
								if ((items.Insured == false) && (items.LootType != LootType.Blessed))
								{
									bag.DropItem(items);
								}
							}
							from.SendMessage("You drop everything you can't carry.");
							bag.MoveToWorld (from.Location, from.Map);
						}*/
						from.PlaySound(0xE5);
						from.FixedParticles(0x3728, 1, 10, 9910, EffectLayer.Head);
						from.PlaySound(0x633);
					}
					else
					{
						from.SendMessage("You cannot polymorph while mounted.");
					}
				}
				else if (from.BodyMod == 719)
				{
					from.BodyMod = 0;      
					//from.HueMod = 0x847E;
					from.Send(SpeedControl.Disable);
					from.PlaySound(0x633);	//Slasher of Veils
					from.FixedParticles(0x3728, 1, 10, 9910, EffectLayer.Head);
				} 
				else
				{
					from.SendMessage("You cannot polymorph while mounted.");
				}
			}            
		}
		
		public virtual void Bite(Mobile mob, Mobile victim)
		{
			PlayerMobile from = (PlayerMobile) mob;
			bool CanUse = from.CheckSkill( SkillName.Anatomy, 20, 80 );
			
			if ((from.Werewolf > 0 || from.AccessLevel > AccessLevel.Player) && (from.Mana > 10))
			{
				if (victim != null && (!(victim is PlayerMobile)) && (!(victim is BaseWerewolf)))
				{
					if ((victim.Race == Race.Human ||  victim.Race == Race.Elf) && CanUse || from.AccessLevel > AccessLevel.Player)
					{
						Effects.SendLocationParticles( EffectItem.Create( from.Location, from.Map, EffectItem.DefaultDuration ), 0x3728, 8, 20, 5042 );
						Effects.PlaySound( from, from.Map, 0x201 );
						from.Location = victim.Location;
						from.PlaySound(0x19C);
						from.FixedParticles(0x3709, 1, 30, 9904, 1108, 6, EffectLayer.Head); 
						from.SendMessage(33, "You bite your enemy!");
						from.WerewolfBiteTime = TimeSpan.FromHours(2.0);
						Server.Items.BleedAttack.BeginBleed(victim, from, true);
						victim.Combatant = from;
						from.Mana -= 10;
						from.Karma -= 100;
						from.Fame += 10;
						//from.HueMod = 0x847E;
						if (Server.Misc.VampireSystem.WSpecialBiteEnabled)
						{
							from.Hits += 25;
							if (from.Hits > from.HitsMax)
							from.Hits = from.HitsMax;
							from.Stam += 10;
							if (from.Stam > from.StamMax)
							from.Stam = from.StamMax;         
						}
					}
					else
					{
						Effects.SendLocationParticles( EffectItem.Create( from.Location, from.Map, EffectItem.DefaultDuration ), 0x3728, 8, 20, 5042 );
						Effects.PlaySound( from, from.Map, 0x201 );
						from.Location = victim.Location;
						from.FixedParticles(0x3709, 1, 30, 9904, 1108, 6, EffectLayer.Head);
						from.SendMessage("You try to bite your enemy but fail.");
						from.Mana -= 5;
					}
				}
				else if (victim is PlayerMobile)
				{
					PlayerMobile combatant = (PlayerMobile) victim;
					//if (from.BodyMod == 0x13D)
					//{
					if (combatant.WerewolfBited < 1)
					{
						if (!combatant.Young)
						{
							if (CanUse || from.AccessLevel > AccessLevel.Player)
							{
								Effects.SendLocationParticles( EffectItem.Create( from.Location, from.Map, EffectItem.DefaultDuration ), 0x3728, 8, 20, 5042 );
								Effects.PlaySound( from, from.Map, 0x201 );
								from.Location = combatant.Location;
								from.FixedParticles(0x3709, 1, 30, 9904, 1108, 6, EffectLayer.Head);
								from.PlaySound(0x19C);
								combatant.WerewolfBited = 1;
								from.SendMessage(33, "You bite your enemy!");
								from.WerewolfBiteTime = TimeSpan.FromHours(2.0);
								Server.Items.BleedAttack.BeginBleed(combatant, from, true);
								combatant.Combatant = from;
								from.Mana -= 10;
								from.Karma -= 100;
								from.Fame += 10;
								//from.HueMod = 0x847E;
								if (combatant is PlayerMobile)
								{
									combatant.NonlocalOverheadMessage(MessageType.Regular, 0x21, 1060758, combatant.Name); // ~1_NAME~ is bleeding profusely
								}
								if (Server.Misc.VampireSystem.WSpecialBiteEnabled)
								{
									from.Hits += 25;
									if (from.Hits > from.HitsMax)
									from.Hits = from.HitsMax;
									from.Stam += 10;
									if (from.Stam > from.StamMax)
									from.Stam = from.StamMax;         
								}
								combatant.SendMessage(33, "A Werewolf bit you and you have started bleeding.");
								if (Server.Misc.VampireSystem.WEnabled && combatant.Werewolf == 0 && Server.Misc.VampireSystem.WerewolfChance > Utility.RandomDouble() || Server.Misc.VampireSystem.WEnabled && combatant.Werewolf == 0 && from.AccessLevel > AccessLevel.Player)
								{
									combatant.SendMessage(33, "You feel strange...");
									combatant.Werewolf = 1;
									combatant.HueMod = 0x847E;
									combatant.Title = "the Werewolf";
									combatant.AddStatMod(new StatMod(StatType.Str, "Werewolf Str Bonus", 8, TimeSpan.Zero));
									combatant.AddStatMod(new StatMod(StatType.Dex, "Werewolf Dex Bonus", 6, TimeSpan.Zero));
									combatant.AddStatMod(new StatMod(StatType.Int, "Werewolf Int Bonus", 4, TimeSpan.Zero));
								}
							}
							else
							{
								Effects.SendLocationParticles( EffectItem.Create( from.Location, from.Map, EffectItem.DefaultDuration ), 0x3728, 8, 20, 5042 );
								Effects.PlaySound( from, from.Map, 0x201 );
								from.Location = combatant.Location;
								from.FixedParticles(0x3709, 1, 30, 9904, 1108, 6, EffectLayer.Head);
								from.SendMessage("You fail to bite your target.");
								from.Mana -= 5;
							}
						}
						else
						{
							from.SendMessage("You cannot bite Young players.");
						}
					}
					else
					{
						from.SendMessage("You cannot bite other Werewolves!");
					}
					//}
					//else
					//{                        
					//    from.SendMessage("You can bite your enemy only in vampire bat form.");
					//}
				}
				else
				{
					from.SendMessage("You cannot bite that!"); //("You can bite only other players who fights WITH you.");
				}
			}
			else
			{
				if (from.Werewolf < 1) from.SendMessage("Only Werewolves are allowed to use this command.");
				else if (from.Werewolf > 1 && from.Mana < 10) from.SendMessage("You do not have enough Mana to do that!");
			}
		} 
		
		private class BiteTarget : Target
		{
			private WerewolfGump t_wwg;
			
			public BiteTarget(WerewolfGump wwg) : base( 6, false, TargetFlags.None )
			{
				t_wwg = wwg;
			}

			protected override void OnTarget( Mobile from, object targ )
			{
				if (targ is Mobile)
				{
					Mobile mob = (Mobile) targ;
					t_wwg.Bite(from, mob);
				}
				else from.SendMessage("You cannot bite that!");
			}
		}
	}
}