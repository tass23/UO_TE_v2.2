using System;
using Server;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using Server.Gumps;
using Server.Items;
using Server.Network;
using Server.Mobiles;
using Server.Spells;
using Server.Accounting;
using Server.Commands;
using Server.Targeting;
using Server.ACC.CM;
using Server.ACC.CSS.Modules;
using Server.ACC.CSS;
using Server.ACC.CSS.Systems.Werewolf;
using Server.ACC.CSS.Systems.Vampire;
using Server.ACC.CSS.Systems.Mage;
using Server.ACC.CSS.Systems.Necromancy;
using Server.ACC.CSS.Systems.Chivalry;
using Server.ACC.CSS.Systems.Spellweaving;
using Server.ACC.CSS.Systems.Mysticism;

namespace Server.Gumps
{
    public abstract class LycanGump : Gump
    {
        private CSpellbook m_Book;
        private ArrayList m_Spells;

        private int Pages;
        public int CurrentPage;
		public string BookHue  { get{ return "999000"; } }

        public abstract string TextHue { get; }
        public abstract int BGImage { get; }
        public abstract int SpellBtn { get; }
        public abstract int SpellBtnP { get; }
        public abstract string Label1 { get; }
        public abstract string Label2 { get; }
        public abstract Type GumpType { get; }
		public abstract School School { get; }

        public LycanGump(CSpellbook book)
            : base(50, 100)
        {
            if (!CSS.Running)
                return;

            m_Book = book;
            m_Spells = book.SchoolSpells;

            Pages = (int)Math.Ceiling((book.SpellCount / 12.0));

            AddPage(0);
            AddImage(70, 100, BGImage);

            CurrentPage = 1;

            for (int i = 0; i < Pages; i++, CurrentPage++)
            {
                AddPage(CurrentPage);

                //Hidden Buttons
                for (int j = (CurrentPage - 1) * 11, C = 0; j < CurrentPage * 11 && j < m_Spells.Count; j++, C++)
                {
                    if (HasSpell((Type)m_Spells[j]))
                    {
                        AddButton((C > 5 ? 305 : 144), 155 + (C > 5 ? (C - 6) * 20 : C * 20), 2482, 2482, j + 1000, GumpButtonType.Reply, 0);
                    }
                }
				AddButton(85, 248, 207, 207, 5, GumpButtonType.Reply, 0);	//Lycantos refresher	//AddButton(85, 255, 2482, 2482, 5, GumpButtonType.Reply, 0);	//Lycantos refresher
				AddButton(82, 204, 11400, 11400, 6, GumpButtonType.Reply, 0);	//Minimize button
				AddButton(84, 110, 2384, 2384, 7, GumpButtonType.Reply, 0);	//Wolf Form button
				AddButton(84, 140, 2384, 2384, 8, GumpButtonType.Reply, 0);	//Werewolf Form button
				AddButton(84, 170, 2384, 2384, 9, GumpButtonType.Reply, 0);	//Bite button
				AddButton(450, 140, 2643, 2643, 10, GumpButtonType.Reply, 0); //Master crafting right button
                AddImage(70, 100, BGImage);
                AddHtml(161, 115, 100, 20, String.Format("<big><basefont color=#{0}><Center>{1}</Center></basefont>", TextHue, Label1), false, false);
                AddHtml(305, 115, 100, 20, String.Format("<big><basefont color=#{0}><Center>{1}</Center></basefont>", TextHue, Label2), false, false);
				AddImage(84, 110, 30021, 2976);	//Wolf Form
				AddImage(84, 140, 30049, 2976);	//Werewolf Form
				AddImage(84, 170, 30048, 2976);	//Bite
                //End Hidden Buttons

                //Prev/Next Buttons
                if (Pages > 1)
                {
                    if (CurrentPage > 1)
                        AddButton(142, 125, 2223, 2223, 0, GumpButtonType.Page, CurrentPage - 1);
                    if (CurrentPage < Pages)
                        AddButton(412, 125, 2224, 2224, 0, GumpButtonType.Page, CurrentPage + 1);
                }
                //End Prev/Next Buttons

                //Spell Buttons/Labels
                for (int j = (CurrentPage - 1) * 11, C = 0; j < CurrentPage * 11 && j < m_Spells.Count; j++, C++)
                {
                    if (HasSpell((Type)m_Spells[j]))
                    {
                        CSpellInfo info = SpellInfoRegistry.GetInfo(m_Book.School, (Type)m_Spells[j]);
                        if (info == null)
                            continue;

                        AddHtml((C > 5 ? 295 : 140), 155 + (C > 5 ? (C - 6) * 20 : C * 20), 110, 20, String.Format("<basefont color=#{0}>{1}</basefont>", BookHue, info.Name), false, false);
                        AddButton((C > 5 ? 416 : 255), 153 + (C > 5 ? (C - 6) * 21 : C * 21), SpellBtn, SpellBtnP, j + 2000, GumpButtonType.Reply, 0);
                        //AddButton((C > 5 ? 426 : 260), 153 + (C > 5 ? (C - 6) * 20 : C * 20), 5411, 5411, j + 1000, GumpButtonType.Reply, 0);
                    }
                }
                //End Spell Buttons/Labels
            }
        }

        public bool HasSpell(Type type)
        {
            return (m_Book != null && m_Book.HasSpell(type));
        }

        public override void OnResponse(NetState state, RelayInfo info)
        {
			Mobile m = state.Mobile;

            if (info.ButtonID == 0 || !CSS.Running)
                return;

			else if (info.ButtonID == 5)
			{
				if (m.Stam == m.StamMax)
				{
					m.SendMessage("You do not need fresh breath right now.");
					return;
				}
				else
				{
					m.Stam = m.StamMax;
					m.SendMessage("The Lycantos chew has refreshed your breath and your body!");
				}
			}

			else if (info.ButtonID == 6)
			{
                state.Mobile.CloseGump(typeof(LycanPrimerGump));
                state.Mobile.SendGump(new LycanPrimerMiniGump(m_Book));
			}

			else if (info.ButtonID == 7)
			{
                WerewolfForm(state.Mobile);
				state.Mobile.CloseGump(typeof(LycanPrimerGump));
				state.Mobile.SendGump( new WerewolfGump() );
			}

			else if (info.ButtonID == 8)
			{
                WerewolfForm2(state.Mobile);
				state.Mobile.CloseGump(typeof(LycanPrimerGump));
				state.Mobile.SendGump( new WerewolfGump() );
			}

			else if (info.ButtonID == 9)
			{
				state.Mobile.CloseGump(typeof(LycanPrimerGump));
                WerewolfGump wwg = new WerewolfGump();
				state.Mobile.SendMessage("Bite whom?");
				state.Mobile.Target = new BiteTarget(wwg);
				state.Mobile.SendGump( wwg );
			}

			else if (info.ButtonID == 10)
			{
				//Add Master Craft Menu to select specific crafting menu.
                m.SendMessage( "What do you want to Craft?" );
				m.SendGump( new MasterCraftGump(m) );
			}

            else if (info.ButtonID >= 1000 && info.ButtonID < (1000 + m_Spells.Count))
            {
                if (SpellRestrictions.UseRestrictions && !SpellRestrictions.CheckRestrictions(state.Mobile, m_Book.School))
                {
                    state.Mobile.SendMessage("You are not allowed to use this Werewolf Ability.");
                    return;
                }

                CSpellInfo si = SpellInfoRegistry.GetInfo(m_Book.School, (Type)m_Spells[info.ButtonID - 1000]);
                if (si == null)
                {
                    state.Mobile.SendMessage("That Werewolf Ability is disabled.");
                    return;
                }
                state.Mobile.CloseGump(typeof(Holocron));
                state.Mobile.SendGump(new Holocron(m_Book, si, TextHue, state.Mobile));
            }

            else if (info.ButtonID >= 2000 && info.ButtonID < (2000 + m_Spells.Count))
            {
                if (SpellRestrictions.UseRestrictions && !SpellRestrictions.CheckRestrictions(state.Mobile, m_Book.School))
                {
                    state.Mobile.SendMessage("You are not allowed to use this Werewolf Ability.");
                    return;
                }

                if (!CSpellbook.MobileHasSpell(state.Mobile, m_Book.School, (Type)m_Spells[info.ButtonID - 2000]))
                {
                    state.Mobile.SendMessage("You do not have that Werewolf Ability.");
                    return;
                }

                Spell spell = SpellInfoRegistry.NewSpell((Type)m_Spells[info.ButtonID - 2000], m_Book.School, state.Mobile, null);
                if (spell == null)
                    state.Mobile.SendMessage("That Werewolf Ability is disabled.");
                else
                    spell.Cast();
            }

            object[] Params = new object[1] { m_Book };
            CSpellbookGump gump = Activator.CreateInstance(GumpType, Params) as CSpellbookGump;
            if (gump != null)
                state.Mobile.SendGump(gump);

            //GumpUpTimer
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

	/*public class LycanScroll : Gump
    {
        private CSpellInfo m_Info;
        private string m_TextHue;
        private CSpellbook m_Book;
        private CastInfo m_CastInfo;
        private CastCommandsModule m_CastCommandModule;

        public LycanScroll(CSpellbook book, CSpellInfo info, string textHue, Mobile sender)
            : base(485, 175)
        {
            if (info == null || book == null || !CSS.Running)
                return;

            m_Info = info;
            m_Book = book;
            m_TextHue = textHue;
            m_CastInfo = new CastInfo(info.Type, info.School);

            Closable = true;
            Disposable = true;
            Dragable = true;
            Resizable = false;

            AddPage(0);
            AddBackground(0, 0, 200, 265, 9380);	//AddBackground(0, 0, 200, 265, 9380);

            if (info.Name != null)
                AddHtml(30, 3, 140, 20, String.Format("<basefont color=#{0}><center>{1}</center></font>", textHue, info.Name), false, false);

            AddButton(30, 40, info.Icon, info.Icon, 3, GumpButtonType.Reply, 0);

            AddButton(90, 40, 2331, 2338, 1, GumpButtonType.Reply, 0);  //Cast
            AddLabel(120, 38, 0, "Cast");

            //AddButton( 90, 65, 2338, 2331, 2, GumpButtonType.Reply, 0 );  //Scribe
            //AddLabel( 120, 63, 0, "Scribe" );

            //Info
            string InfoString = "";
            if (info.Desc != null)
                InfoString += String.Format("<basefont color=black>{0}</font><br><br>", info.Desc);

            if (info.Regs != null)
            {
                string[] Regs = info.Regs.Split(';');
                InfoString += String.Format("<basefont color=black>Reagents :</font><br><basefont color=#{0}>", textHue);
                foreach (string r in Regs)
                    InfoString += String.Format("-{0}<br>", r.TrimStart());
                InfoString += "</font><br>";
            }

            if (info.Info != null)
            {
                string[] Info = info.Info.Split(';');
                InfoString += String.Format("<basefont color=#{0}>", textHue);
                foreach (string s in Info)
                    InfoString += String.Format("{0}<br>", s.TrimStart());
                InfoString += "</font><br>";
            }
            AddHtml(30, 95, 140, 130, InfoString, false, true);
            //End Info

            #region CastInfo
            if (CentralMemory.Running)
            {
                m_CastCommandModule = (CastCommandsModule)CentralMemory.GetModule(sender.Serial, typeof(CastCommandsModule));

                AddLabel(25, 242, 0, "Key :");
                if (m_CastCommandModule == null)
                    AddTextEntry(70, 242, 100, 20, 0, 5, "");  //Key	Loc,Size,Hue,ID
                else
                    AddTextEntry(70, 242, 100, 20, 0, 5, m_CastCommandModule.GetCommandForInfo(m_CastInfo));  //Key		Loc,Size,Hue,ID
                AddButton(175, 247, 2103, 2104, 4, GumpButtonType.Reply, 0);  //KeyButton
            }
            #endregion //CastInfo
        }

        public override void OnResponse(NetState state, RelayInfo info)
        {
            if (info.ButtonID == 0 || !CSS.Running)
                return;

            else if (info.ButtonID == 1)
            {
                if (SpellRestrictions.UseRestrictions && !SpellRestrictions.CheckRestrictions(state.Mobile, m_Info.School))
                {
                    state.Mobile.SendMessage("You are not allowed to use this Force Power.");
                    return;
                }

                if (!CSpellbook.MobileHasSpell(state.Mobile, m_Info.School, m_Info.Type))
                {
                    state.Mobile.SendMessage("You do not have that Force Power.");
                    return;
                }

                Spell spell = SpellInfoRegistry.NewSpell(m_Info.Type, m_Info.School, state.Mobile, null);
                if (spell == null)
                    state.Mobile.SendMessage("That Force Power is disabled.");
                else
                    spell.Cast();
            }

            else if (info.ButtonID == 2)
            {
                //Scribe
            }

            else if (info.ButtonID == 3)
            {
                if (!CentralMemory.Running)
                    return;

                if (SpellRestrictions.UseRestrictions && !SpellRestrictions.CheckRestrictions(state.Mobile, m_Info.School))
                    return;

                state.Mobile.SendGump(new IconPlacementGump(m_Book, state.Mobile, 100, 100, 10, m_Info.Icon, m_Info.Type, m_Info.Back, m_Book.School));
            }

            else if (info.ButtonID == 4)
            {
                if (!CentralMemory.Running)
                    return;

                string command = info.GetTextEntry(5).Text;

                if (command == null || command.Length == 0)
                {
                    if (m_CastCommandModule == null)
                    {
                        state.Mobile.SendGump(new Holocron(m_Book, m_Info, m_TextHue, state.Mobile));
                        return;
                    }

                    m_CastCommandModule.RemoveCommandByInfo(m_CastInfo);
                    state.Mobile.SendGump(new Holocron(m_Book, m_Info, m_TextHue, state.Mobile));
                }
                else
                {
                    if (m_CastCommandModule == null)
                    {
                        CentralMemory.AddModule(new CastCommandsModule(state.Mobile.Serial, command, m_CastInfo));
                        state.Mobile.SendGump(new Holocron(m_Book, m_Info, m_TextHue, state.Mobile));
                        return;
                    }

                    m_CastCommandModule.Add(command, m_CastInfo);
                    state.Mobile.SendGump(new Holocron(m_Book, m_Info, m_TextHue, state.Mobile));
                }
            }
        }
    }*/

	public abstract class VampyGump : Gump
    {
        private CSpellbook m_Book;
        private ArrayList m_Spells;

        private int Pages;
        public int CurrentPage;
		public string BookHue  { get{ return "660066"; } }

        public abstract string TextHue { get; }
        public abstract int BGImage { get; }
        public abstract int SpellBtn { get; }
        public abstract int SpellBtnP { get; }
        public abstract string Label1 { get; }
        public abstract string Label2 { get; }
        public abstract Type GumpType { get; }
		public abstract School School { get; }

        public VampyGump(CSpellbook book)
            : base(50, 100)
        {
            if (!CSS.Running)
                return;

            m_Book = book;
            m_Spells = book.SchoolSpells;

            Pages = (int)Math.Ceiling((book.SpellCount / 12.0));

            AddPage(0);
            AddImage(70, 100, BGImage);

            CurrentPage = 1;

            for (int i = 0; i < Pages; i++, CurrentPage++)
            {
                AddPage(CurrentPage);

                //Hidden Buttons
                for (int j = (CurrentPage - 1) * 10, C = 0; j < CurrentPage * 10 && j < m_Spells.Count; j++, C++)
                {
                    if (HasSpell((Type)m_Spells[j]))
                    {
                        AddButton((C > 5 ? 320 : 150), 190 + (C > 5 ? (C - 6) * 20 : C * 20), 2482, 2482, j + 1000, GumpButtonType.Reply, 0);
                    }
                }
				AddButton(72, 236, 11400, 11400, 6, GumpButtonType.Reply, 0);	//Minimize button
				AddButton(104, 174, 255, 255, 7, GumpButtonType.Reply, 0);	//Bat Form button
				AddButton(104, 224, 255, 255, 8, GumpButtonType.Reply, 0);	//Fog Form button
				AddButton(104, 274, 255, 255, 9, GumpButtonType.Reply, 0);	//Raven button
				AddButton(100, 123, 254, 254, 10, GumpButtonType.Reply, 0);	//Bite button
				AddButton(476, 123, 254, 254, 10, GumpButtonType.Reply, 0);	//Bite button
				AddButton(100, 336, 254, 254, 10, GumpButtonType.Reply, 0);	//Bite button
				AddButton(476, 336, 254, 254, 10, GumpButtonType.Reply, 0);	//Bite button
				AddButton(478, 183, 2643, 2643, 11, GumpButtonType.Reply, 0); //Master crafting right button
                AddImage(70, 100, BGImage);
                AddHtml(141, 150, 100, 20, String.Format("<big><basefont color=#{0}><Center>{1}</Center></basefont>", TextHue, Label1), false, false);
                AddHtml(320, 150, 100, 20, String.Format("<big><basefont color=#{0}><Center>{1}</Center></basefont>", TextHue, Label2), false, false);
                //End Hidden Buttons

                //Prev/Next Buttons
				if (Pages > 1)
                {
                    if (CurrentPage > 1)
                        AddButton(142, 125, 2235, 2235, 0, GumpButtonType.Page, CurrentPage - 1);
                    if (CurrentPage < Pages)
                        AddButton(412, 125, 2236, 2236, 0, GumpButtonType.Page, CurrentPage + 1);
                }
                //End Prev/Next Buttons

                //Spell Buttons/Labels
                for (int j = (CurrentPage - 1) * 11, C = 0; j < CurrentPage * 11 && j < m_Spells.Count; j++, C++)
                {
                    if (HasSpell((Type)m_Spells[j]))
                    {
                        CSpellInfo info = SpellInfoRegistry.GetInfo(m_Book.School, (Type)m_Spells[j]);
                        if (info == null)
                            continue;

                        AddHtml((C > 5 ? 312 : 142), 190 + (C > 5 ? (C - 6) * 20 : C * 20), 110, 20, String.Format("<basefont color=#{0}>{1}</basefont>", BookHue, info.Name), false, false);
                        AddButton((C > 5 ? 428 : 248), 191 + (C > 5 ? (C - 6) * 21 : C * 21), SpellBtn, SpellBtnP, j + 2000, GumpButtonType.Reply, 0);
                        //AddButton((C > 5 ? 439 : 260), 173 + (C > 5 ? (C - 6) * 20 : C * 20), 5411, 5411, j + 1000, GumpButtonType.Reply, 0);
                    }
                }
                //End Spell Buttons/Labels
            }
        }

        public bool HasSpell(Type type)
        {
            return (m_Book != null && m_Book.HasSpell(type));
        }

        public override void OnResponse(NetState state, RelayInfo info)
        {
			Mobile m = state.Mobile;
			int j = (CurrentPage - 1) * 11, C = 0;

            if (info.ButtonID == 0 || !CSS.Running)
                return;

			else if (info.ButtonID == 6)
			{
                state.Mobile.CloseGump(typeof(CovenSpellbookGump));
                state.Mobile.SendGump(new AncientCovenMiniGump(m_Book));
			}

			else if (info.ButtonID == 7)
			{
                VampireForm(state.Mobile);
				state.Mobile.CloseGump(typeof(CovenSpellbookGump));
				state.Mobile.SendGump( new VampireGump() );
			}

			else if (info.ButtonID == 8)
			{
                VampireForm2(state.Mobile);
				state.Mobile.CloseGump(typeof(CovenSpellbookGump));
				state.Mobile.SendGump( new VampireGump() );
			}

			else if (info.ButtonID == 9)
			{
                VampireForm3(state.Mobile);
				state.Mobile.CloseGump(typeof(CovenSpellbookGump));
				state.Mobile.SendGump( new VampireGump() );
			}

			else if (info.ButtonID == 10)
			{
				state.Mobile.CloseGump(typeof(CovenSpellbookGump));
                VampireGump vg = new VampireGump();
				state.Mobile.SendMessage("Bite whom?");
				state.Mobile.Target = new BiteTarget(vg);
				state.Mobile.SendGump( vg );
			}

			else if (info.ButtonID == 11)
			{
				//Add Master Craft Menu to select specific crafting menu.
                m.SendMessage( "What do you want to Craft?" );
				m.SendGump( new MasterCraftGump(m) );
			}

            else if (info.ButtonID >= 1000 && info.ButtonID < (1000 + m_Spells.Count))
            {
                if (SpellRestrictions.UseRestrictions && !SpellRestrictions.CheckRestrictions(state.Mobile, m_Book.School))
                {
                    state.Mobile.SendMessage("You are not allowed to use this Vampire Ability.");
                    return;
                }

                CSpellInfo si = SpellInfoRegistry.GetInfo(m_Book.School, (Type)m_Spells[info.ButtonID - 1000]);
                if (si == null)
                {
                    state.Mobile.SendMessage("That Vampire Ability is disabled.");
                    return;
                }
				state.Mobile.CloseGump(typeof(CovenSpellbookGump));
                state.Mobile.SendGump(new Holocron(m_Book, si, TextHue, state.Mobile));
            }

            else if (info.ButtonID >= 2000 && info.ButtonID < (2000 + m_Spells.Count))
            {
                if (SpellRestrictions.UseRestrictions && !SpellRestrictions.CheckRestrictions(state.Mobile, m_Book.School))
                {
                    state.Mobile.SendMessage("You are not allowed to use this Vampire Ability.");
                    return;
                }

                if (!CSpellbook.MobileHasSpell(state.Mobile, m_Book.School, (Type)m_Spells[info.ButtonID - 2000]))
                {
                    state.Mobile.SendMessage("You do not have that Vampire Ability.");
                    return;
                }

                Spell spell = SpellInfoRegistry.NewSpell((Type)m_Spells[info.ButtonID - 2000], m_Book.School, state.Mobile, null);
                if (spell == null)
                    state.Mobile.SendMessage("That Vampire Ability is disabled.");
                else
                    spell.Cast();
            }

            object[] Params = new object[1] { m_Book };
            CSpellbookGump gump = Activator.CreateInstance(GumpType, Params) as CSpellbookGump;
            if (gump != null)
                state.Mobile.SendGump(gump);

            //GumpUpTimer
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

	/*public class VampyScroll : Gump
    {
        private CSpellInfo m_Info;
        private string m_TextHue;
        private CSpellbook m_Book;
        private CastInfo m_CastInfo;
        private CastCommandsModule m_CastCommandModule;

        public VampyScroll(CSpellbook book, CSpellInfo info, string textHue, Mobile sender)
            : base(485, 175)
        {
            if (info == null || book == null || !CSS.Running)
                return;

            m_Info = info;
            m_Book = book;
            m_TextHue = textHue;
            m_CastInfo = new CastInfo(info.Type, info.School);

            Closable = true;
            Disposable = true;
            Dragable = true;
            Resizable = false;

            AddPage(0);
            AddBackground(0, 0, 200, 265, 9380);	//AddBackground(0, 0, 200, 265, 9380);

            if (info.Name != null)
                AddHtml(30, 3, 140, 20, String.Format("<basefont color=#{0}><center>{1}</center></font>", textHue, info.Name), false, false);

            AddButton(30, 40, info.Icon, info.Icon, 3, GumpButtonType.Reply, 0);

            AddButton(90, 40, 2331, 2338, 1, GumpButtonType.Reply, 0);  //Cast
            AddLabel(120, 38, 0, "Cast");

            //AddButton( 90, 65, 2338, 2331, 2, GumpButtonType.Reply, 0 );  //Scribe
            //AddLabel( 120, 63, 0, "Scribe" );

            //Info
            string InfoString = "";
            if (info.Desc != null)
                InfoString += String.Format("<basefont color=black>{0}</font><br><br>", info.Desc);

            if (info.Regs != null)
            {
                string[] Regs = info.Regs.Split(';');
                InfoString += String.Format("<basefont color=black>Reagents :</font><br><basefont color=#{0}>", textHue);
                foreach (string r in Regs)
                    InfoString += String.Format("-{0}<br>", r.TrimStart());
                InfoString += "</font><br>";
            }

            if (info.Info != null)
            {
                string[] Info = info.Info.Split(';');
                InfoString += String.Format("<basefont color=#{0}>", textHue);
                foreach (string s in Info)
                    InfoString += String.Format("{0}<br>", s.TrimStart());
                InfoString += "</font><br>";
            }
            AddHtml(30, 95, 140, 130, InfoString, false, true);
            //End Info

            #region CastInfo
            if (CentralMemory.Running)
            {
                m_CastCommandModule = (CastCommandsModule)CentralMemory.GetModule(sender.Serial, typeof(CastCommandsModule));

                AddLabel(25, 242, 0, "Key :");
                if (m_CastCommandModule == null)
                    AddTextEntry(70, 242, 100, 20, 0, 5, "");  //Key	Loc,Size,Hue,ID
                else
                    AddTextEntry(70, 242, 100, 20, 0, 5, m_CastCommandModule.GetCommandForInfo(m_CastInfo));  //Key		Loc,Size,Hue,ID
                AddButton(175, 247, 2103, 2104, 4, GumpButtonType.Reply, 0);  //KeyButton
            }
            #endregion //CastInfo
        }

        public override void OnResponse(NetState state, RelayInfo info)
        {
            if (info.ButtonID == 0 || !CSS.Running)
                return;

            else if (info.ButtonID == 1)
            {
                if (SpellRestrictions.UseRestrictions && !SpellRestrictions.CheckRestrictions(state.Mobile, m_Info.School))
                {
                    state.Mobile.SendMessage("You are not allowed to use this Force Power.");
                    return;
                }

                if (!CSpellbook.MobileHasSpell(state.Mobile, m_Info.School, m_Info.Type))
                {
                    state.Mobile.SendMessage("You do not have that Force Power.");
                    return;
                }

                Spell spell = SpellInfoRegistry.NewSpell(m_Info.Type, m_Info.School, state.Mobile, null);
                if (spell == null)
                    state.Mobile.SendMessage("That Force Power is disabled.");
                else
                    spell.Cast();
            }

            else if (info.ButtonID == 2)
            {
                //Scribe
            }

            else if (info.ButtonID == 3)
            {
                if (!CentralMemory.Running)
                    return;

                if (SpellRestrictions.UseRestrictions && !SpellRestrictions.CheckRestrictions(state.Mobile, m_Info.School))
                    return;

                state.Mobile.SendGump(new IconPlacementGump(m_Book, state.Mobile, 100, 100, 10, m_Info.Icon, m_Info.Type, m_Info.Back, m_Book.School));
            }

            else if (info.ButtonID == 4)
            {
                if (!CentralMemory.Running)
                    return;

                string command = info.GetTextEntry(5).Text;

                if (command == null || command.Length == 0)
                {
                    if (m_CastCommandModule == null)
                    {
                        state.Mobile.SendGump(new Holocron(m_Book, m_Info, m_TextHue, state.Mobile));
                        return;
                    }

                    m_CastCommandModule.RemoveCommandByInfo(m_CastInfo);
                    state.Mobile.SendGump(new Holocron(m_Book, m_Info, m_TextHue, state.Mobile));
                }
                else
                {
                    if (m_CastCommandModule == null)
                    {
                        CentralMemory.AddModule(new CastCommandsModule(state.Mobile.Serial, command, m_CastInfo));
                        state.Mobile.SendGump(new Holocron(m_Book, m_Info, m_TextHue, state.Mobile));
                        return;
                    }

                    m_CastCommandModule.Add(command, m_CastInfo);
                    state.Mobile.SendGump(new Holocron(m_Book, m_Info, m_TextHue, state.Mobile));
                }
            }
        }
    }*/

	public abstract class MageryGump : Gump
    {
        private CSpellbook m_Book;
        private ArrayList m_Spells;

        private int Pages;
        public int CurrentPage;
		public string BookHue  { get{ return "955819"; } }
        public abstract string TextHue { get; }
        public abstract int BGImage { get; }
        public abstract int SpellBtn { get; }
        public abstract int SpellBtnP { get; }
        public abstract string Label1 { get; }
        public abstract string Label2 { get; }
        public abstract Type GumpType { get; }
		public abstract School School { get; }

        public MageryGump(CSpellbook book)
            : base(50, 100)
        {
            if (!CSS.Running)
                return;

            m_Book = book;
            m_Spells = book.SchoolSpells;

            Pages = (int)Math.Ceiling((book.SpellCount / 16.0));

            AddPage(0);
            AddImage(70, 100, BGImage);

            CurrentPage = 1;

            for (int i = 0; i < Pages; i++, CurrentPage++)
            {
                AddPage(CurrentPage);

                //Hidden Buttons
                for (int j = (CurrentPage - 1) * 16, C = 0; j < CurrentPage * 16 && j < m_Spells.Count; j++, C++)	//Each set of two pages is 16 spells, e.g. Pages 1 & 2 are spells 1 - 16, 3 & 4 are 17 - 32, etc., etc.
                {
                    if (HasSpell((Type)m_Spells[j]))
                    {
                        AddButton((C > 7 ? 292 : 130), 146 + (C > 7 ? (C - 8) * 16 : C * 16), 2482, 2482, j + 1000, GumpButtonType.Reply, 0);
                    }
                }
				AddButton(73, 202, 11400, 11400, 6, GumpButtonType.Reply, 0);	//Minimize button
				AddButton(438, 130, 2643, 2643, 7, GumpButtonType.Reply, 0); //Master crafting right button
				AddButton(74, 239, 257, 257, 8, GumpButtonType.Reply, 0); //Food/Water left button
                AddImage(70, 100, BGImage);
				AddHtml(145, 109, 100, 20, String.Format("<big><basefont color=#{0}><Center>{1}</Center></basefont>", TextHue, Label1), false, false);
                AddHtml(305, 109, 100, 20, String.Format("<big><basefont color=#{0}><Center>{1}</Center></basefont>", TextHue, Label2), false, false);
				//AddImage(73, 202, 11400, 2976);	//Minimize button
				//AddImage(74, 239, 257, 2976); //Food/Water left button
                //End Hidden Buttons

                //Prev/Next Buttons
                if (Pages > 1)
                {
                    if (CurrentPage > 1)
                        AddButton(123, 108, 2205, 2205, 0, GumpButtonType.Page, CurrentPage - 1);
                    if (CurrentPage < Pages)
                        AddButton(390, 108, 2206, 2206, 0, GumpButtonType.Page, CurrentPage + 1);

					AddButton(130, 277, 2225, 2225, 0, GumpButtonType.Page, 1);
					AddButton(165, 277, 2226, 2226, 0, GumpButtonType.Page, 1);
					AddButton(200, 277, 2227, 2227, 0, GumpButtonType.Page, 2);
					AddButton(235, 277, 2228, 2228, 0, GumpButtonType.Page, 2);
					AddButton(285, 277, 2229, 2229, 0, GumpButtonType.Page, 3);
					AddButton(320, 277, 2230, 2230, 0, GumpButtonType.Page, 3);
					AddButton(355, 277, 2231, 2231, 0, GumpButtonType.Page, 4);
					AddButton(390, 277, 2232, 2232, 0, GumpButtonType.Page, 4);
                }

				if (CurrentPage == 1)
				{
					AddHtml(132, 130, 100, 20, String.Format("<big><basefont color=#{0}>First Circle</basefont>", TextHue), false, false);
					AddHtml(285, 130, 100, 20, String.Format("<big><basefont color=#{0}>Second Circle</basefont>", TextHue), false, false);
				}
				else if (CurrentPage == 2)
				{
					AddHtml(132, 128, 100, 20, String.Format("<big><basefont color=#{0}>Third Circle</basefont>", TextHue), false, false);
					AddHtml(285, 128, 100, 20, String.Format("<big><basefont color=#{0}>Fourth Circle</basefont>", TextHue), false, false);
				}
				else if (CurrentPage == 3)
				{
					AddHtml(132, 128, 100, 20, String.Format("<big><basefont color=#{0}>Fifth Circle</basefont>", TextHue), false, false);
					AddHtml(285, 128, 100, 20, String.Format("<big><basefont color=#{0}>Sixth Circle</basefont>", TextHue), false, false);
				}
				else if (CurrentPage == 4)
				{
					AddHtml(132, 128, 120, 20, String.Format("<big><basefont color=#{0}>Seventh Circle</basefont>", TextHue), false, false);
					AddHtml(285, 128, 100, 20, String.Format("<big><basefont color=#{0}>Eighth Circle</basefont>", TextHue), false, false);
				}
                //End Prev/Next Buttons

                //Spell Buttons/Labels
                for (int j = (CurrentPage - 1) * 16, C = 0; j < CurrentPage * 16 && j < m_Spells.Count; j++, C++)
                {
                    if (HasSpell((Type)m_Spells[j]))
                    {
                        CSpellInfo info = SpellInfoRegistry.GetInfo(m_Book.School, (Type)m_Spells[j]);
                        if (info == null)
                            continue;
						

                        AddHtml((C > 7 ? 280 : 127), 146 + (C > 7 ? (C - 8) * 16 : C * 16), 110, 20, String.Format("<basefont color=#{0}>{1}</basefont>", BookHue, info.Name), false, false);
                        //AddButton((C > 7 ? 288 : 130), 138 + (C > 7 ? (C - 8) * 20 : C * 17), SpellBtn, SpellBtnP, j + 2000, GumpButtonType.Reply, 0);
                        AddButton((C > 7 ? 408 : 250), 149 + (C > 7 ? (C - 8) * 16 : C * 16), 0x4BA, 0x4BA, j + 2000, GumpButtonType.Reply, 0);
						AddImage((C > 7 ? 408 : 250), 149 + (C > 7 ? (C - 8) * 16 : C * 16), 0x4BA, 0x4BA);
                    }
                }
                //End Spell Buttons/Labels
            }
        }

        public bool HasSpell(Type type)
        {
            return (m_Book != null && m_Book.HasSpell(type));
        }

		private static FoodInfo[] m_Food = new FoodInfo[]
		{
			new FoodInfo( typeof( Grapes ), "a grape bunch" ),
			new FoodInfo( typeof( Ham ), "a ham" ),
			new FoodInfo( typeof( CheeseWedge ), "a wedge of cheese" ),
			new FoodInfo( typeof( Muffins ), "muffins" ),
			new FoodInfo( typeof( FishSteak ), "a fish steak" ),
			new FoodInfo( typeof( Ribs ), "cut of ribs" ),
			new FoodInfo( typeof( CookedBird ), "a cooked bird" ),
			new FoodInfo( typeof( Sausage ), "sausage" ),
			new FoodInfo( typeof( Apple ), "an apple" ),
			new FoodInfo( typeof( Peach ), "a peach" )
		};

        public override void OnResponse(NetState state, RelayInfo info)
        {
			Mobile m = state.Mobile;

            if (info.ButtonID == 0 || !CSS.Running)
                return;

			else if (info.ButtonID == 6)
			{
                state.Mobile.CloseGump(typeof(MageryGump));
                state.Mobile.SendGump(new MageMiniGump(m_Book));
			}

			else if (info.ButtonID == 7)
			{
				//state.Mobile.CloseGump(typeof(MageryGump));	//Add Master Craft Menu to select specific crafting menu.
                m.SendMessage( "What do you want to Craft?" );
				m.SendGump( new MasterCraftGump(m) );
			}

			else if (info.ButtonID == 8)
			{
				FoodInfo foodInfo = m_Food[Utility.Random( m_Food.Length )];
				Item food = foodInfo.Create();

				if ( food != null )
				{
					m.SendMessage("You create some food and water.");
					m.AddToBackpack(food);
					m.AddToBackpack( new Pitcher( BeverageType.Water ) );
					m.PlaySound( 0x1E2 );
					state.Mobile.CloseGump(typeof(MageryGump));
				}				
			}

            else if (info.ButtonID >= 1000 && info.ButtonID < (1000 + m_Spells.Count))
            {
                if (SpellRestrictions.UseRestrictions && !SpellRestrictions.CheckRestrictions(state.Mobile, m_Book.School))
                {
                    state.Mobile.SendMessage("You are not allowed to use this Spell.");
                    return;
                }

                CSpellInfo si = SpellInfoRegistry.GetInfo(m_Book.School, (Type)m_Spells[info.ButtonID - 1000]);
                if (si == null)
                {
                    state.Mobile.SendMessage("That Spell is disabled.");
                    return;
                }
                state.Mobile.CloseGump(typeof(MageryGump));
                state.Mobile.SendGump(new ScrollGump(m_Book, si, TextHue, state.Mobile));
            }

            else if (info.ButtonID >= 2000 && info.ButtonID < (2000 + m_Spells.Count))
            {
                if (SpellRestrictions.UseRestrictions && !SpellRestrictions.CheckRestrictions(state.Mobile, m_Book.School))
                {
                    state.Mobile.SendMessage("You are not allowed to use this Spell.");
                    return;
                }

                if (!CSpellbook.MobileHasSpell(state.Mobile, m_Book.School, (Type)m_Spells[info.ButtonID - 2000]))
                {
                    state.Mobile.SendMessage("You do not have that Spell.");
                    return;
                }

                Spell spell = SpellInfoRegistry.NewSpell((Type)m_Spells[info.ButtonID - 2000], m_Book.School, state.Mobile, null);
                if (spell == null)
                    state.Mobile.SendMessage("That Spell is disabled.");
                else
                    spell.Cast();
            }

            object[] Params = new object[1] { m_Book };
            CSpellbookGump gump = Activator.CreateInstance(GumpType, Params) as CSpellbookGump;
            if (gump != null)
                state.Mobile.SendGump(gump);

            //GumpUpTimer
        }
    }
	/*public class MageryScroll : Gump
    {
        private CSpellInfo m_Info;
        private string m_TextHue;
        private CSpellbook m_Book;
        private CastInfo m_CastInfo;
        private CastCommandsModule m_CastCommandModule;

        public MageryScroll(CSpellbook book, CSpellInfo info, string textHue, Mobile sender)
            : base(485, 175)
        {
            if (info == null || book == null || !CSS.Running)
                return;

            m_Info = info;
            m_Book = book;
            m_TextHue = textHue;
            m_CastInfo = new CastInfo(info.Type, info.School);

            Closable = true;
            Disposable = true;
            Dragable = true;
            Resizable = false;

            AddPage(0);
            AddBackground(0, 0, 200, 265, 9380);	//AddBackground(0, 0, 200, 265, 9380);

            if (info.Name != null)
                AddHtml(30, 3, 140, 20, String.Format("<basefont color=#{0}><center>{1}</center></font>", textHue, info.Name), false, false);

            AddButton(30, 40, info.Icon, info.Icon, 3, GumpButtonType.Reply, 0);

            AddButton(90, 40, 2331, 2338, 1, GumpButtonType.Reply, 0);  //Cast
            AddLabel(120, 38, 0, "Cast");

            //AddButton( 90, 65, 2338, 2331, 2, GumpButtonType.Reply, 0 );  //Scribe
            //AddLabel( 120, 63, 0, "Scribe" );

            //Info
            string InfoString = "";
            if (info.Desc != null)
                InfoString += String.Format("<basefont color=black>{0}</font><br><br>", info.Desc);

            if (info.Regs != null)
            {
                string[] Regs = info.Regs.Split(';');
                InfoString += String.Format("<basefont color=black>Reagents :</font><br><basefont color=#{0}>", textHue);
                foreach (string r in Regs)
                    InfoString += String.Format("-{0}<br>", r.TrimStart());
                InfoString += "</font><br>";
            }

            if (info.Info != null)
            {
                string[] Info = info.Info.Split(';');
                InfoString += String.Format("<basefont color=#{0}>", textHue);
                foreach (string s in Info)
                    InfoString += String.Format("{0}<br>", s.TrimStart());
                InfoString += "</font><br>";
            }
            AddHtml(30, 95, 140, 130, InfoString, false, true);
            //End Info

            #region CastInfo
            if (CentralMemory.Running)
            {
                m_CastCommandModule = (CastCommandsModule)CentralMemory.GetModule(sender.Serial, typeof(CastCommandsModule));

                AddLabel(25, 242, 0, "Key :");
                if (m_CastCommandModule == null)
                    AddTextEntry(70, 242, 100, 20, 0, 5, "");  //Key	Loc,Size,Hue,ID
                else
                    AddTextEntry(70, 242, 100, 20, 0, 5, m_CastCommandModule.GetCommandForInfo(m_CastInfo));  //Key		Loc,Size,Hue,ID
                AddButton(175, 247, 2103, 2104, 4, GumpButtonType.Reply, 0);  //KeyButton
            }
            #endregion //CastInfo
        }

        public override void OnResponse(NetState state, RelayInfo info)
        {
            if (info.ButtonID == 0 || !CSS.Running)
                return;

            else if (info.ButtonID == 1)
            {
                if (SpellRestrictions.UseRestrictions && !SpellRestrictions.CheckRestrictions(state.Mobile, m_Info.School))
                {
                    state.Mobile.SendMessage("You are not allowed to use this Spell.");
                    return;
                }

                if (!CSpellbook.MobileHasSpell(state.Mobile, m_Info.School, m_Info.Type))
                {
                    state.Mobile.SendMessage("You do not have that Spell.");
                    return;
                }

                Spell spell = SpellInfoRegistry.NewSpell(m_Info.Type, m_Info.School, state.Mobile, null);
                if (spell == null)
                    state.Mobile.SendMessage("That Spell is disabled.");
                else
                    spell.Cast();
            }

            else if (info.ButtonID == 2)
            {
                //Scribe
            }

            else if (info.ButtonID == 3)
            {
                if (!CentralMemory.Running)
                    return;

                if (SpellRestrictions.UseRestrictions && !SpellRestrictions.CheckRestrictions(state.Mobile, m_Info.School))
                    return;

                state.Mobile.SendGump(new IconPlacementGump(m_Book, state.Mobile, 100, 100, 10, m_Info.Icon, m_Info.Type, m_Info.Back, m_Book.School));
            }

            else if (info.ButtonID == 4)
            {
                if (!CentralMemory.Running)
                    return;

                string command = info.GetTextEntry(5).Text;

                if (command == null || command.Length == 0)
                {
                    if (m_CastCommandModule == null)
                    {
                        state.Mobile.SendGump(new Holocron(m_Book, m_Info, m_TextHue, state.Mobile));
                        return;
                    }

                    m_CastCommandModule.RemoveCommandByInfo(m_CastInfo);
                    state.Mobile.SendGump(new Holocron(m_Book, m_Info, m_TextHue, state.Mobile));
                }
                else
                {
                    if (m_CastCommandModule == null)
                    {
                        CentralMemory.AddModule(new CastCommandsModule(state.Mobile.Serial, command, m_CastInfo));
                        state.Mobile.SendGump(new Holocron(m_Book, m_Info, m_TextHue, state.Mobile));
                        return;
                    }

                    m_CastCommandModule.Add(command, m_CastInfo);
                    state.Mobile.SendGump(new Holocron(m_Book, m_Info, m_TextHue, state.Mobile));
                }
            }
        }
    }*/

	public abstract class NecroGump : Gump
    {
        private CSpellbook m_Book;
        private ArrayList m_Spells;

        private int Pages;
        public int CurrentPage;
		public string BookHue  { get{ return "333333"; } }
        public abstract string TextHue { get; }
        public abstract int BGImage { get; }
        public abstract int SpellBtn { get; }
        public abstract int SpellBtnP { get; }
        public abstract string Label1 { get; }
        public abstract string Label2 { get; }
        public abstract Type GumpType { get; }
		public abstract School School { get; }

        public NecroGump(CSpellbook book)
            : base(50, 100)
        {
            if (!CSS.Running)
                return;

            m_Book = book;
            m_Spells = book.SchoolSpells;

            Pages = (int)Math.Ceiling((book.SpellCount / 16.0));

            AddPage(0);
            AddImage(70, 100, BGImage);

            CurrentPage = 1;

            for (int i = 0; i < Pages; i++, CurrentPage++)
            {
                AddPage(CurrentPage);

                //Hidden Buttons
                for (int j = (CurrentPage - 1) * 16, C = 0; j < CurrentPage * 16 && j < m_Spells.Count; j++, C++)	//Each set of two pages is 16 spells, e.g. Pages 1 & 2 are spells 1 - 16, 3 & 4 are 17 - 32, etc., etc.
                {
                    if (HasSpell((Type)m_Spells[j]))
                    {
                        AddButton((C > 7 ? 292 : 130), 146 + (C > 7 ? (C - 8) * 16 : C * 16), 2482, 2482, j + 1000, GumpButtonType.Reply, 0);
                    }
                }
				AddButton(73, 202, 11400, 11400, 6, GumpButtonType.Reply, 0);	//Minimize button
				AddButton(438, 130, 2643, 2643, 7, GumpButtonType.Reply, 0); //Master crafting right button
                AddImage(70, 100, BGImage);
				AddHtml(145, 109, 100, 20, String.Format("<big><basefont color=#{0}><Center>{1}</Center></basefont>", TextHue, Label1), false, false);
                AddHtml(305, 109, 100, 20, String.Format("<big><basefont color=#{0}><Center>{1}</Center></basefont>", TextHue, Label2), false, false);
                //End Hidden Buttons

                //Prev/Next Buttons
                if (Pages > 1)
                {
                    if (CurrentPage > 1)
                        AddButton(123, 108, 2205, 2205, 0, GumpButtonType.Page, CurrentPage - 1);
                    if (CurrentPage < Pages)
                        AddButton(390, 108, 2206, 2206, 0, GumpButtonType.Page, CurrentPage + 1);
                }
                //End Prev/Next Buttons

                //Spell Buttons/Labels
                for (int j = (CurrentPage - 1) * 16, C = 0; j < CurrentPage * 16 && j < m_Spells.Count; j++, C++)
                {
                    if (HasSpell((Type)m_Spells[j]))
                    {
                        CSpellInfo info = SpellInfoRegistry.GetInfo(m_Book.School, (Type)m_Spells[j]);
                        if (info == null)
                            continue;
						

                        AddHtml((C > 7 ? 283 : 130), 146 + (C > 7 ? (C - 8) * 16 : C * 16), 110, 20, String.Format("<basefont color=#{0}>{1}</basefont>", BookHue, info.Name), false, false);
                        //AddButton((C > 7 ? 288 : 130), 138 + (C > 7 ? (C - 8) * 20 : C * 17), SpellBtn, SpellBtnP, j + 2000, GumpButtonType.Reply, 0);
                        AddButton((C > 7 ? 408 : 250), 149 + (C > 7 ? (C - 8) * 16 : C * 16), 0x4, 0x4, j + 2000, GumpButtonType.Reply, 0);
						//AddImage((C > 7 ? 408 : 250), 149 + (C > 7 ? (C - 8) * 16 : C * 16), 0x4, 0x4);
                    }
                }
                //End Spell Buttons/Labels
            }
        }

        public bool HasSpell(Type type)
        {
            return (m_Book != null && m_Book.HasSpell(type));
        }

        public override void OnResponse(NetState state, RelayInfo info)
        {
			Mobile m = state.Mobile;

            if (info.ButtonID == 0 || !CSS.Running)
                return;

			else if (info.ButtonID == 6)
			{
                state.Mobile.CloseGump(typeof(NecroGump));
                state.Mobile.SendGump(new NecroMiniGump(m_Book));
			}

			else if (info.ButtonID == 7)
			{
				//Add Master Craft Menu to select specific crafting menu.
                m.SendMessage( "What do you want to Craft?" );
				m.SendGump( new MasterCraftGump(m) );
			}

            else if (info.ButtonID >= 1000 && info.ButtonID < (1000 + m_Spells.Count))
            {
                if (SpellRestrictions.UseRestrictions && !SpellRestrictions.CheckRestrictions(state.Mobile, m_Book.School))
                {
                    state.Mobile.SendMessage("You are not allowed to use this Spell.");
                    return;
                }

                CSpellInfo si = SpellInfoRegistry.GetInfo(m_Book.School, (Type)m_Spells[info.ButtonID - 1000]);
                if (si == null)
                {
                    state.Mobile.SendMessage("That Spell is disabled.");
                    return;
                }
                state.Mobile.CloseGump(typeof(NecroGump));
                state.Mobile.SendGump(new ScrollGump(m_Book, si, TextHue, state.Mobile));
            }

            else if (info.ButtonID >= 2000 && info.ButtonID < (2000 + m_Spells.Count))
            {
                if (SpellRestrictions.UseRestrictions && !SpellRestrictions.CheckRestrictions(state.Mobile, m_Book.School))
                {
                    state.Mobile.SendMessage("You are not allowed to use this Spell.");
                    return;
                }

                if (!CSpellbook.MobileHasSpell(state.Mobile, m_Book.School, (Type)m_Spells[info.ButtonID - 2000]))
                {
                    state.Mobile.SendMessage("You do not have that Spell.");
                    return;
                }

                Spell spell = SpellInfoRegistry.NewSpell((Type)m_Spells[info.ButtonID - 2000], m_Book.School, state.Mobile, null);
                if (spell == null)
                    state.Mobile.SendMessage("That Spell is disabled.");
                else
                    spell.Cast();
            }

            object[] Params = new object[1] { m_Book };
            CSpellbookGump gump = Activator.CreateInstance(GumpType, Params) as CSpellbookGump;
            if (gump != null)
                state.Mobile.SendGump(gump);

            //GumpUpTimer
        }
    }
	/*public class NecroScroll : Gump
    {
        private CSpellInfo m_Info;
        private string m_TextHue;
        private CSpellbook m_Book;
        private CastInfo m_CastInfo;
        private CastCommandsModule m_CastCommandModule;

        public NecroScroll(CSpellbook book, CSpellInfo info, string textHue, Mobile sender)
            : base(485, 175)
        {
            if (info == null || book == null || !CSS.Running)
                return;

            m_Info = info;
            m_Book = book;
            m_TextHue = textHue;
            m_CastInfo = new CastInfo(info.Type, info.School);

            Closable = true;
            Disposable = true;
            Dragable = true;
            Resizable = false;

            AddPage(0);
            AddBackground(0, 0, 200, 265, 9380);	//AddBackground(0, 0, 200, 265, 9380);

            if (info.Name != null)
                AddHtml(30, 3, 140, 20, String.Format("<basefont color=#{0}><center>{1}</center></font>", textHue, info.Name), false, false);

            AddButton(30, 40, info.Icon, info.Icon, 3, GumpButtonType.Reply, 0);

            AddButton(90, 40, 2331, 2338, 1, GumpButtonType.Reply, 0);  //Cast
            AddLabel(120, 38, 0, "Cast");

            //AddButton( 90, 65, 2338, 2331, 2, GumpButtonType.Reply, 0 );  //Scribe
            //AddLabel( 120, 63, 0, "Scribe" );

            //Info
            string InfoString = "";
            if (info.Desc != null)
                InfoString += String.Format("<basefont color=black>{0}</font><br><br>", info.Desc);

            if (info.Regs != null)
            {
                string[] Regs = info.Regs.Split(';');
                InfoString += String.Format("<basefont color=black>Reagents :</font><br><basefont color=#{0}>", textHue);
                foreach (string r in Regs)
                    InfoString += String.Format("-{0}<br>", r.TrimStart());
                InfoString += "</font><br>";
            }

            if (info.Info != null)
            {
                string[] Info = info.Info.Split(';');
                InfoString += String.Format("<basefont color=#{0}>", textHue);
                foreach (string s in Info)
                    InfoString += String.Format("{0}<br>", s.TrimStart());
                InfoString += "</font><br>";
            }
            AddHtml(30, 95, 140, 130, InfoString, false, true);
            //End Info

            #region CastInfo
            if (CentralMemory.Running)
            {
                m_CastCommandModule = (CastCommandsModule)CentralMemory.GetModule(sender.Serial, typeof(CastCommandsModule));

                AddLabel(25, 242, 0, "Key :");
                if (m_CastCommandModule == null)
                    AddTextEntry(70, 242, 100, 20, 0, 5, "");  //Key	Loc,Size,Hue,ID
                else
                    AddTextEntry(70, 242, 100, 20, 0, 5, m_CastCommandModule.GetCommandForInfo(m_CastInfo));  //Key		Loc,Size,Hue,ID
                AddButton(175, 247, 2103, 2104, 4, GumpButtonType.Reply, 0);  //KeyButton
            }
            #endregion //CastInfo
        }

        public override void OnResponse(NetState state, RelayInfo info)
        {
            if (info.ButtonID == 0 || !CSS.Running)
                return;

            else if (info.ButtonID == 1)
            {
                if (SpellRestrictions.UseRestrictions && !SpellRestrictions.CheckRestrictions(state.Mobile, m_Info.School))
                {
                    state.Mobile.SendMessage("You are not allowed to use this Spell.");
                    return;
                }

                if (!CSpellbook.MobileHasSpell(state.Mobile, m_Info.School, m_Info.Type))
                {
                    state.Mobile.SendMessage("You do not have that Spell.");
                    return;
                }

                Spell spell = SpellInfoRegistry.NewSpell(m_Info.Type, m_Info.School, state.Mobile, null);
                if (spell == null)
                    state.Mobile.SendMessage("That Spell is disabled.");
                else
                    spell.Cast();
            }

            else if (info.ButtonID == 2)
            {
                //Scribe
            }

            else if (info.ButtonID == 3)
            {
                if (!CentralMemory.Running)
                    return;

                if (SpellRestrictions.UseRestrictions && !SpellRestrictions.CheckRestrictions(state.Mobile, m_Info.School))
                    return;

                state.Mobile.SendGump(new IconPlacementGump(m_Book, state.Mobile, 100, 100, 10, m_Info.Icon, m_Info.Type, m_Info.Back, m_Book.School));
            }

            else if (info.ButtonID == 4)
            {
                if (!CentralMemory.Running)
                    return;

                string command = info.GetTextEntry(5).Text;

                if (command == null || command.Length == 0)
                {
                    if (m_CastCommandModule == null)
                    {
                        state.Mobile.SendGump(new Holocron(m_Book, m_Info, m_TextHue, state.Mobile));
                        return;
                    }

                    m_CastCommandModule.RemoveCommandByInfo(m_CastInfo);
                    state.Mobile.SendGump(new Holocron(m_Book, m_Info, m_TextHue, state.Mobile));
                }
                else
                {
                    if (m_CastCommandModule == null)
                    {
                        CentralMemory.AddModule(new CastCommandsModule(state.Mobile.Serial, command, m_CastInfo));
                        state.Mobile.SendGump(new Holocron(m_Book, m_Info, m_TextHue, state.Mobile));
                        return;
                    }

                    m_CastCommandModule.Add(command, m_CastInfo);
                    state.Mobile.SendGump(new Holocron(m_Book, m_Info, m_TextHue, state.Mobile));
                }
            }
        }
    }*/

	public abstract class ChivGump : Gump
    {
        private CSpellbook m_Book;
        private ArrayList m_Spells;
		private Mobile m_From;

        private int Pages;
        public int CurrentPage;
		private int m_TithingPoints;
		public string BookHue  { get{ return "955819"; } }
        public abstract string TextHue { get; }
        public abstract int BGImage { get; }
        public abstract int SpellBtn { get; }
        public abstract int SpellBtnP { get; }
        public abstract string Label1 { get; }
        public abstract string Label2 { get; }
        public abstract Type GumpType { get; }
		public abstract School School { get; }
		
		public int TithingPoints
		{
			get
			{
				return m_TithingPoints;
			}
		}

        public ChivGump( CSpellbook book, Mobile from ) : base(50, 100)
        {
            if (!CSS.Running)
                return;
			
			Mobile m_from = from;
			PlayerMobile pm_from = (PlayerMobile)from;
            m_Book = book;
            m_Spells = book.SchoolSpells;

            Pages = (int)Math.Ceiling((book.SpellCount / 10.0));

            AddPage(0);
            AddImage(70, 100, BGImage);

            CurrentPage = 1;
			
			AddHtml(130, 264, 100, 20, String.Format("<basefont color=#{0}>Tithing Points</basefont>", TextHue), false, false);
			AddHtml(130, 279, 100, 20, String.Format("<basefont color=#{0}>Available: {1}</basefont>", BookHue, from.TithingPoints),  false, false);//AddHtml(130, 279, 100, 20, String.Format("<basefont color=#{0}>Available: {1}</basefont>", BookHue, Convert.ToString(m_TithingPoints)),  false, false);

            for (int i = 0; i < Pages; i++, CurrentPage++)
            {
                AddPage(CurrentPage);

                //Hidden Buttons
                for (int j = (CurrentPage - 1) * 5, C = 0; j < CurrentPage * 5 && j < m_Spells.Count; j++, C++)	//Each set of two pages is 16 spells, e.g. Pages 1 & 2 are spells 1 - 16, 3 & 4 are 17 - 32, etc., etc.
                {
                    if (HasSpell((Type)m_Spells[j]))
                    {
                        AddButton((C > 7 ? 292 : 130), 129 + (C > 7 ? (C - 8) * 10: C * 10), 2482, 2482, j + 1000, GumpButtonType.Reply, 0);
                    }
                }
				AddButton(73, 202, 11400, 11400, 6, GumpButtonType.Reply, 0);	//Minimize button
				AddButton(437, 130, 2643, 2643, 7, GumpButtonType.Reply, 0); //Master crafting right button
				AddButton(78, 244, 254, 254, 8, GumpButtonType.Reply, 0); //Tithe left button
                AddImage(70, 100, BGImage);
				AddHtml(145, 109, 100, 20, String.Format("<big><basefont color=#{0}><Center>{1}</Center></basefont>", TextHue, Label1), false, false);
                AddHtml(305, 109, 100, 20, String.Format("<big><basefont color=#{0}><Center>{1}</Center></basefont>", TextHue, Label2), false, false);
                //End Hidden Buttons

                //Prev/Next Buttons
                if (Pages > 1)
                {
                    if (CurrentPage > 1)
                        AddButton(123, 108, 2205, 2205, 0, GumpButtonType.Page, CurrentPage - 1);
                    if (CurrentPage < Pages)
                        AddButton(390, 108, 2206, 2206, 0, GumpButtonType.Page, CurrentPage + 1);
                }
                //End Prev/Next Buttons

                //Spell Buttons/Labels
                for (int j = (CurrentPage - 1) * 10, C = 0; j < CurrentPage * 10 && j < m_Spells.Count; j++, C++)
                {
                    if (HasSpell((Type)m_Spells[j]))
                    {
                        CSpellInfo info = SpellInfoRegistry.GetInfo(m_Book.School, (Type)m_Spells[j]);
                        if (info == null)
                            continue;
						

                        AddHtml((C > 7 ? 282 : 128), 130 + (C > 7 ? (C - 8) * 16 : C * 16), 110, 20, String.Format("<basefont color=#{0}>{1}</basefont>", BookHue, info.Name), false, false);
                        AddButton((C > 7 ? 405 : 248), 130 + (C > 7 ? (C - 8) * 17 : C * 17), SpellBtn, SpellBtnP, j + 2000, GumpButtonType.Reply, 0);
                        //AddButton((C > 7 ? 408 : 250), 129 + (C > 7 ? (C - 8) * 16 : C * 16), 9272, 9272, j + 2000, GumpButtonType.Reply, 0);
						//AddImage((C > 7 ? 408 : 250), 129 + (C > 7 ? (C - 8) * 16 : C * 16), 9272, 2973);
                    }
                }
                //End Spell Buttons/Labels
            }
        }

        public bool HasSpell(Type type)
        {
            return (m_Book != null && m_Book.HasSpell(type));
        }

        public override void OnResponse(NetState state, RelayInfo info)
        {
			Mobile m = state.Mobile;

            if (info.ButtonID == 0 || !CSS.Running)
                return;

			else if (info.ButtonID == 6)
			{
                state.Mobile.CloseGump(typeof(ChivGump));
                state.Mobile.SendGump(new ChivMiniGump(m_Book));
			}

			else if (info.ButtonID == 7)
			{
				//Add Master Craft Menu to select specific crafting menu.
                m.SendMessage( "What do you want to Craft?" );
				m.SendGump( new MasterCraftGump(m) );
			}

			else if (info.ButtonID == 8)
			{
				if ( state.Mobile.CheckAlive() )
					state.Mobile.SendGump( new TithingGump( state.Mobile, 0 ) );
			}

            else if (info.ButtonID >= 1000 && info.ButtonID < (1000 + m_Spells.Count))
            {
                if (SpellRestrictions.UseRestrictions && !SpellRestrictions.CheckRestrictions(state.Mobile, m_Book.School))
                {
                    state.Mobile.SendMessage("You are not allowed to use this Spell.");
                    return;
                }

                CSpellInfo si = SpellInfoRegistry.GetInfo(m_Book.School, (Type)m_Spells[info.ButtonID - 1000]);
                if (si == null)
                {
                    state.Mobile.SendMessage("That Spell is disabled.");
                    return;
                }
                state.Mobile.CloseGump(typeof(ChivGump));
                state.Mobile.SendGump(new ScrollGump(m_Book, si, TextHue, state.Mobile));
            }

            else if (info.ButtonID >= 2000 && info.ButtonID < (2000 + m_Spells.Count))
            {
                if (SpellRestrictions.UseRestrictions && !SpellRestrictions.CheckRestrictions(state.Mobile, m_Book.School))
                {
                    state.Mobile.SendMessage("You are not allowed to use this Spell.");
                    return;
                }

                if (!CSpellbook.MobileHasSpell(state.Mobile, m_Book.School, (Type)m_Spells[info.ButtonID - 2000]))
                {
                    state.Mobile.SendMessage("You do not have that Spell.");
                    return;
                }

                Spell spell = SpellInfoRegistry.NewSpell((Type)m_Spells[info.ButtonID - 2000], m_Book.School, state.Mobile, null);
                if (spell == null)
                    state.Mobile.SendMessage("That Spell is disabled.");
                else
                    spell.Cast();
            }

            object[] Params = new object[2] { m_Book, m };
            CSpellbookGump gump = Activator.CreateInstance(GumpType, Params) as CSpellbookGump;
            if (gump != null)
                state.Mobile.SendGump(gump);

            //GumpUpTimer
        }
    }
	/*public class ChivScroll : Gump
    {
        private CSpellInfo m_Info;
        private string m_TextHue;
        private CSpellbook m_Book;
        private CastInfo m_CastInfo;
        private CastCommandsModule m_CastCommandModule;

        public ChivScroll(CSpellbook book, CSpellInfo info, string textHue, Mobile sender)
            : base(485, 175)
        {
            if (info == null || book == null || !CSS.Running)
                return;

            m_Info = info;
            m_Book = book;
            m_TextHue = textHue;
            m_CastInfo = new CastInfo(info.Type, info.School);

            Closable = true;
            Disposable = true;
            Dragable = true;
            Resizable = false;

            AddPage(0);
            AddBackground(0, 0, 200, 265, 9380);	//AddBackground(0, 0, 200, 265, 9380);

            if (info.Name != null)
                AddHtml(30, 3, 140, 20, String.Format("<basefont color=#{0}><center>{1}</center></font>", textHue, info.Name), false, false);

            AddButton(30, 40, info.Icon, info.Icon, 3, GumpButtonType.Reply, 0);

            AddButton(90, 40, 2331, 2338, 1, GumpButtonType.Reply, 0);  //Cast
            AddLabel(120, 38, 0, "Cast");

            //AddButton( 90, 65, 2338, 2331, 2, GumpButtonType.Reply, 0 );  //Scribe
            //AddLabel( 120, 63, 0, "Scribe" );

            //Info
            string InfoString = "";
            if (info.Desc != null)
                InfoString += String.Format("<basefont color=black>{0}</font><br><br>", info.Desc);

            if (info.Regs != null)
            {
                string[] Regs = info.Regs.Split(';');
                InfoString += String.Format("<basefont color=black>Reagents :</font><br><basefont color=#{0}>", textHue);
                foreach (string r in Regs)
                    InfoString += String.Format("-{0}<br>", r.TrimStart());
                InfoString += "</font><br>";
            }

            if (info.Info != null)
            {
                string[] Info = info.Info.Split(';');
                InfoString += String.Format("<basefont color=#{0}>", textHue);
                foreach (string s in Info)
                    InfoString += String.Format("{0}<br>", s.TrimStart());
                InfoString += "</font><br>";
            }
            AddHtml(30, 95, 140, 130, InfoString, false, true);
            //End Info

            #region CastInfo
            if (CentralMemory.Running)
            {
                m_CastCommandModule = (CastCommandsModule)CentralMemory.GetModule(sender.Serial, typeof(CastCommandsModule));

                AddLabel(25, 242, 0, "Key :");
                if (m_CastCommandModule == null)
                    AddTextEntry(70, 242, 100, 20, 0, 5, "");  //Key	Loc,Size,Hue,ID
                else
                    AddTextEntry(70, 242, 100, 20, 0, 5, m_CastCommandModule.GetCommandForInfo(m_CastInfo));  //Key		Loc,Size,Hue,ID
                AddButton(175, 247, 2103, 2104, 4, GumpButtonType.Reply, 0);  //KeyButton
            }
            #endregion //CastInfo
        }

        public override void OnResponse(NetState state, RelayInfo info)
        {
            if (info.ButtonID == 0 || !CSS.Running)
                return;

            else if (info.ButtonID == 1)
            {
                if (SpellRestrictions.UseRestrictions && !SpellRestrictions.CheckRestrictions(state.Mobile, m_Info.School))
                {
                    state.Mobile.SendMessage("You are not allowed to use this Spell.");
                    return;
                }

                if (!CSpellbook.MobileHasSpell(state.Mobile, m_Info.School, m_Info.Type))
                {
                    state.Mobile.SendMessage("You do not have that Spell.");
                    return;
                }

                Spell spell = SpellInfoRegistry.NewSpell(m_Info.Type, m_Info.School, state.Mobile, null);
                if (spell == null)
                    state.Mobile.SendMessage("That Spell is disabled.");
                else
                    spell.Cast();
            }

            else if (info.ButtonID == 2)
            {
                //Scribe
            }

            else if (info.ButtonID == 3)
            {
                if (!CentralMemory.Running)
                    return;

                if (SpellRestrictions.UseRestrictions && !SpellRestrictions.CheckRestrictions(state.Mobile, m_Info.School))
                    return;

                state.Mobile.SendGump(new IconPlacementGump(m_Book, state.Mobile, 100, 100, 10, m_Info.Icon, m_Info.Type, m_Info.Back, m_Book.School));
            }

            else if (info.ButtonID == 4)
            {
                if (!CentralMemory.Running)
                    return;

                string command = info.GetTextEntry(5).Text;

                if (command == null || command.Length == 0)
                {
                    if (m_CastCommandModule == null)
                    {
                        state.Mobile.SendGump(new Holocron(m_Book, m_Info, m_TextHue, state.Mobile));
                        return;
                    }

                    m_CastCommandModule.RemoveCommandByInfo(m_CastInfo);
                    state.Mobile.SendGump(new Holocron(m_Book, m_Info, m_TextHue, state.Mobile));
                }
                else
                {
                    if (m_CastCommandModule == null)
                    {
                        CentralMemory.AddModule(new CastCommandsModule(state.Mobile.Serial, command, m_CastInfo));
                        state.Mobile.SendGump(new Holocron(m_Book, m_Info, m_TextHue, state.Mobile));
                        return;
                    }

                    m_CastCommandModule.Add(command, m_CastInfo);
                    state.Mobile.SendGump(new Holocron(m_Book, m_Info, m_TextHue, state.Mobile));
                }
            }
        }
    }*/

	/*
	public abstract class BushidoGump : Gump
    {
        private CSpellbook m_Book;
        private ArrayList m_Spells;

        private int Pages;
        public int CurrentPage;
		public string BookHue  { get{ return "333333"; } }
        public abstract string TextHue { get; }
        public abstract int BGImage { get; }
        public abstract int SpellBtn { get; }
        public abstract int SpellBtnP { get; }
        public abstract string Label1 { get; }
        public abstract string Label2 { get; }
        public abstract Type GumpType { get; }
		public abstract School School { get; }

        public BushidoGump(CSpellbook book)
            : base(50, 100)
        {
            if (!CSS.Running)
                return;

            m_Book = book;
            m_Spells = book.SchoolSpells;

            Pages = (int)Math.Ceiling((book.SpellCount / 6.0));

            AddPage(0);
            AddImage(70, 100, BGImage);

            CurrentPage = 1;

            for (int i = 0; i < Pages; i++, CurrentPage++)
            {
                AddPage(CurrentPage);

                //Hidden Buttons
                for (int j = (CurrentPage - 1) * 6, C = 0; j < CurrentPage * 6 && j < m_Spells.Count; j++, C++)	//Each set of two pages is 6 spells, e.g. Pages 1 & 2 are spells 1 - 16, 3 & 4 are 17 - 32, etc., etc.
                {
                    if (HasSpell((Type)m_Spells[j]))
                    {
                        AddButton((C > 2 ? 292 : 130), 146 + (C > 2 ? (C - 3) * 16 : C * 16), 2482, 2482, j + 1000, GumpButtonType.Reply, 0);
                    }
                }
				AddButton(73, 202, 11400, 11400, 6, GumpButtonType.Reply, 0);	//Minimize button
				AddButton(438, 130, 2643, 2643, 7, GumpButtonType.Reply, 0); //Master crafting right button
                AddImage(70, 100, BGImage);
				AddHtml(145, 109, 100, 20, String.Format("<big><basefont color=#{0}><Center>{1}</Center></basefont>", TextHue, Label1), false, false);
                AddHtml(305, 109, 100, 20, String.Format("<big><basefont color=#{0}><Center>{1}</Center></basefont>", TextHue, Label2), false, false);
				//AddImage(73, 202, 11400, 2976);	//Minimize button
                //End Hidden Buttons

                //Prev/Next Buttons
                if (Pages > 1)
                {
                    if (CurrentPage > 1)
                        AddButton(123, 108, 2205, 2205, 0, GumpButtonType.Page, CurrentPage - 1);
                    if (CurrentPage < Pages)
                        AddButton(390, 108, 2206, 2206, 0, GumpButtonType.Page, CurrentPage + 1);
                }
                //End Prev/Next Buttons

                //Spell Buttons/Labels
                for (int j = (CurrentPage - 1) * 6, C = 0; j < CurrentPage * 6 && j < m_Spells.Count; j++, C++)
                {
                    if (HasSpell((Type)m_Spells[j]))
                    {
                        CSpellInfo info = SpellInfoRegistry.GetInfo(m_Book.School, (Type)m_Spells[j]);
                        if (info == null)
                            continue;
						

                        AddHtml((C > 2 ? 283 : 130), 146 + (C > 2 ? (C - 3) * 16 : C * 16), 150, 20, String.Format("<basefont color=#{0}>{1}</basefont>", BookHue, info.Name), false, false);
                        //AddButton((C > 7 ? 288 : 130), 138 + (C > 7 ? (C - 8) * 20 : C * 17), SpellBtn, SpellBtnP, j + 2000, GumpButtonType.Reply, 0);
                        AddButton((C > 2 ? 408 : 250), 149 + (C > 2 ? (C - 3) * 16 : C * 16), 9272, 9272, j + 2000, GumpButtonType.Reply, 0);
						AddImage((C > 2 ? 408 : 250), 149 + (C > 2 ? (C - 3) * 16 : C * 16), 9272, 2973);
                    }
                }
                //End Spell Buttons/Labels
            }
        }

        public bool HasSpell(Type type)
        {
            return (m_Book != null && m_Book.HasSpell(type));
        }

        public override void OnResponse(NetState state, RelayInfo info)
        {
			Mobile m = state.Mobile;

            if (info.ButtonID == 0 || !CSS.Running)
                return;

			else if (info.ButtonID == 6)
			{
                state.Mobile.CloseGump(typeof(BushidoGump));
                state.Mobile.SendGump(new BushidoMiniGump(m_Book));
			}

			else if (info.ButtonID == 7)
			{
				//Add Master Craft Menu to select specific crafting menu.
                m.SendMessage( "What do you want to Craft?" );
				m.SendGump( new MasterCraftGump(m) );
			}

            else if (info.ButtonID >= 1000 && info.ButtonID < (1000 + m_Spells.Count))
            {
                if (SpellRestrictions.UseRestrictions && !SpellRestrictions.CheckRestrictions(state.Mobile, m_Book.School))
                {
                    state.Mobile.SendMessage("You are not allowed to use this Spell.");
                    return;
                }

                CSpellInfo si = SpellInfoRegistry.GetInfo(m_Book.School, (Type)m_Spells[info.ButtonID - 1000]);
                if (si == null)
                {
                    state.Mobile.SendMessage("That Spell is disabled.");
                    return;
                }
                state.Mobile.CloseGump(typeof(BushidoGump));
                state.Mobile.SendGump(new ScrollGump(m_Book, si, TextHue, state.Mobile));
            }

            else if (info.ButtonID >= 2000 && info.ButtonID < (2000 + m_Spells.Count))
            {
                if (SpellRestrictions.UseRestrictions && !SpellRestrictions.CheckRestrictions(state.Mobile, m_Book.School))
                {
                    state.Mobile.SendMessage("You are not allowed to use this Spell.");
                    return;
                }

                if (!CSpellbook.MobileHasSpell(state.Mobile, m_Book.School, (Type)m_Spells[info.ButtonID - 2000]))
                {
                    state.Mobile.SendMessage("You do not have that Spell.");
                    return;
                }

                Spell spell = SpellInfoRegistry.NewSpell((Type)m_Spells[info.ButtonID - 2000], m_Book.School, state.Mobile, null);
                if (spell == null)
                    state.Mobile.SendMessage("That Spell is disabled.");
                else
                    spell.Cast();
            }

            object[] Params = new object[1] { m_Book };
            CSpellbookGump gump = Activator.CreateInstance(GumpType, Params) as CSpellbookGump;
            if (gump != null)
                state.Mobile.SendGump(gump);

            //GumpUpTimer
        }
    }
	*/
	/*
	public class BushidoScroll : Gump
    {
        private CSpellInfo m_Info;
        private string m_TextHue;
        private CSpellbook m_Book;
        private CastInfo m_CastInfo;
        private CastCommandsModule m_CastCommandModule;

        public BushidoScroll(CSpellbook book, CSpellInfo info, string textHue, Mobile sender)
            : base(485, 175)
        {
            if (info == null || book == null || !CSS.Running)
                return;

            m_Info = info;
            m_Book = book;
            m_TextHue = textHue;
            m_CastInfo = new CastInfo(info.Type, info.School);

            Closable = true;
            Disposable = true;
            Dragable = true;
            Resizable = false;

            AddPage(0);
            AddBackground(0, 0, 200, 265, 9380);	//AddBackground(0, 0, 200, 265, 9380);

            if (info.Name != null)
                AddHtml(30, 3, 140, 20, String.Format("<basefont color=#{0}><center>{1}</center></font>", textHue, info.Name), false, false);

            AddButton(30, 40, info.Icon, info.Icon, 3, GumpButtonType.Reply, 0);

            AddButton(90, 40, 2331, 2338, 1, GumpButtonType.Reply, 0);  //Cast
            AddLabel(120, 38, 0, "Cast");

            //AddButton( 90, 65, 2338, 2331, 2, GumpButtonType.Reply, 0 );  //Scribe
            //AddLabel( 120, 63, 0, "Scribe" );

            //Info
            string InfoString = "";
            if (info.Desc != null)
                InfoString += String.Format("<basefont color=black>{0}</font><br><br>", info.Desc);

            if (info.Regs != null)
            {
                string[] Regs = info.Regs.Split(';');
                InfoString += String.Format("<basefont color=black>Reagents :</font><br><basefont color=#{0}>", textHue);
                foreach (string r in Regs)
                    InfoString += String.Format("-{0}<br>", r.TrimStart());
                InfoString += "</font><br>";
            }

            if (info.Info != null)
            {
                string[] Info = info.Info.Split(';');
                InfoString += String.Format("<basefont color=#{0}>", textHue);
                foreach (string s in Info)
                    InfoString += String.Format("{0}<br>", s.TrimStart());
                InfoString += "</font><br>";
            }
            AddHtml(30, 95, 140, 130, InfoString, false, true);
            //End Info

            #region CastInfo
            if (CentralMemory.Running)
            {
                m_CastCommandModule = (CastCommandsModule)CentralMemory.GetModule(sender.Serial, typeof(CastCommandsModule));

                AddLabel(25, 242, 0, "Key :");
                if (m_CastCommandModule == null)
                    AddTextEntry(70, 242, 100, 20, 0, 5, "");  //Key	Loc,Size,Hue,ID
                else
                    AddTextEntry(70, 242, 100, 20, 0, 5, m_CastCommandModule.GetCommandForInfo(m_CastInfo));  //Key		Loc,Size,Hue,ID
                AddButton(175, 247, 2103, 2104, 4, GumpButtonType.Reply, 0);  //KeyButton
            }
            #endregion //CastInfo
        }

        public override void OnResponse(NetState state, RelayInfo info)
        {
            if (info.ButtonID == 0 || !CSS.Running)
                return;

            else if (info.ButtonID == 1)
            {
                if (SpellRestrictions.UseRestrictions && !SpellRestrictions.CheckRestrictions(state.Mobile, m_Info.School))
                {
                    state.Mobile.SendMessage("You are not allowed to use this Spell.");
                    return;
                }

                if (!CSpellbook.MobileHasSpell(state.Mobile, m_Info.School, m_Info.Type))
                {
                    state.Mobile.SendMessage("You do not have that Spell.");
                    return;
                }

                Spell spell = SpellInfoRegistry.NewSpell(m_Info.Type, m_Info.School, state.Mobile, null);
                if (spell == null)
                    state.Mobile.SendMessage("That Spell is disabled.");
                else
                    spell.Cast();
            }

            else if (info.ButtonID == 2)
            {
                //Scribe
            }

            else if (info.ButtonID == 3)
            {
                if (!CentralMemory.Running)
                    return;

                if (SpellRestrictions.UseRestrictions && !SpellRestrictions.CheckRestrictions(state.Mobile, m_Info.School))
                    return;

                state.Mobile.SendGump(new IconPlacementGump(m_Book, state.Mobile, 100, 100, 10, m_Info.Icon, m_Info.Type, m_Info.Back, m_Book.School));
            }

            else if (info.ButtonID == 4)
            {
                if (!CentralMemory.Running)
                    return;

                string command = info.GetTextEntry(5).Text;

                if (command == null || command.Length == 0)
                {
                    if (m_CastCommandModule == null)
                    {
                        state.Mobile.SendGump(new Holocron(m_Book, m_Info, m_TextHue, state.Mobile));
                        return;
                    }

                    m_CastCommandModule.RemoveCommandByInfo(m_CastInfo);
                    state.Mobile.SendGump(new Holocron(m_Book, m_Info, m_TextHue, state.Mobile));
                }
                else
                {
                    if (m_CastCommandModule == null)
                    {
                        CentralMemory.AddModule(new CastCommandsModule(state.Mobile.Serial, command, m_CastInfo));
                        state.Mobile.SendGump(new Holocron(m_Book, m_Info, m_TextHue, state.Mobile));
                        return;
                    }

                    m_CastCommandModule.Add(command, m_CastInfo);
                    state.Mobile.SendGump(new Holocron(m_Book, m_Info, m_TextHue, state.Mobile));
                }
            }
        }
    }
	*/

	/*
	public abstract class NinjitsuGump : Gump
    {
        private CSpellbook m_Book;
        private ArrayList m_Spells;

        private int Pages;
        public int CurrentPage;
		public string BookHue  { get{ return "333333"; } }
        public abstract string TextHue { get; }
        public abstract int BGImage { get; }
        public abstract int SpellBtn { get; }
        public abstract int SpellBtnP { get; }
        public abstract string Label1 { get; }
        public abstract string Label2 { get; }
        public abstract Type GumpType { get; }
		public abstract School School { get; }

        public NinjitsuGump(CSpellbook book)
            : base(50, 100)
        {
            if (!CSS.Running)
                return;

            m_Book = book;
            m_Spells = book.SchoolSpells;

            Pages = (int)Math.Ceiling((book.SpellCount / 8.0));

            AddPage(0);
            AddImage(70, 100, BGImage);

            CurrentPage = 1;

            for (int i = 0; i < Pages; i++, CurrentPage++)
            {
                AddPage(CurrentPage);

                //Hidden Buttons
                for (int j = (CurrentPage - 1) * 8, C = 0; j < CurrentPage * 8 && j < m_Spells.Count; j++, C++)	//Each set of two pages is 8 spells, e.g. Pages 1 & 2 are spells 1 - 16, 3 & 4 are 17 - 32, etc., etc.
                {
                    if (HasSpell((Type)m_Spells[j]))
                    {
                        AddButton((C > 3 ? 292 : 130), 146 + (C > 3 ? (C - 4) * 16 : C * 16), 2482, 2482, j + 1000, GumpButtonType.Reply, 0);
                    }
                }
				AddButton(73, 202, 11400, 11400, 6, GumpButtonType.Reply, 0);	//Minimize button
				AddButton(438, 130, 2643, 2643, 7, GumpButtonType.Reply, 0); //Master crafting right button
                AddImage(70, 100, BGImage);
				AddHtml(145, 109, 100, 20, String.Format("<big><basefont color=#{0}><Center>{1}</Center></basefont>", TextHue, Label1), false, false);
                AddHtml(305, 109, 100, 20, String.Format("<big><basefont color=#{0}><Center>{1}</Center></basefont>", TextHue, Label2), false, false);
				//AddImage(73, 202, 11400, 2976);	//Minimize button
                //End Hidden Buttons

                //Prev/Next Buttons
                if (Pages > 1)
                {
                    if (CurrentPage > 1)
                        AddButton(123, 108, 2205, 2205, 0, GumpButtonType.Page, CurrentPage - 1);
                    if (CurrentPage < Pages)
                        AddButton(390, 108, 2206, 2206, 0, GumpButtonType.Page, CurrentPage + 1);
                }
                //End Prev/Next Buttons

                //Spell Buttons/Labels
                for (int j = (CurrentPage - 1) * 8, C = 0; j < CurrentPage * 8 && j < m_Spells.Count; j++, C++)
                {
                    if (HasSpell((Type)m_Spells[j]))
                    {
                        CSpellInfo info = SpellInfoRegistry.GetInfo(m_Book.School, (Type)m_Spells[j]);
                        if (info == null)
                            continue;
						

                        AddHtml((C > 3 ? 283 : 130), 146 + (C > 3 ? (C - 4) * 16 : C * 16), 110, 20, String.Format("<basefont color=#{0}>{1}</basefont>", BookHue, info.Name), false, false);
                        //AddButton((C > 7 ? 288 : 130), 138 + (C > 7 ? (C - 8) * 20 : C * 17), SpellBtn, SpellBtnP, j + 2000, GumpButtonType.Reply, 0);
                        AddButton((C > 3 ? 408 : 250), 149 + (C > 3 ? (C - 4) * 16 : C * 16), 9272, 9272, j + 2000, GumpButtonType.Reply, 0);
						AddImage((C > 3 ? 408 : 250), 149 + (C > 3 ? (C - 4) * 16 : C * 16), 9272, 2973);
                    }
                }
                //End Spell Buttons/Labels
            }
        }

        public bool HasSpell(Type type)
        {
            return (m_Book != null && m_Book.HasSpell(type));
        }

        public override void OnResponse(NetState state, RelayInfo info)
        {
			Mobile m = state.Mobile;

            if (info.ButtonID == 0 || !CSS.Running)
                return;

			else if (info.ButtonID == 6)
			{
                state.Mobile.CloseGump(typeof(NinjitsuGump));
                state.Mobile.SendGump(new NinjitsuMiniGump(m_Book));
			}

			else if (info.ButtonID == 7)
			{
				//Add Master Craft Menu to select specific crafting menu.
                m.SendMessage( "What do you want to Craft?" );
				m.SendGump( new MasterCraftGump(m) );
			}

            else if (info.ButtonID >= 1000 && info.ButtonID < (1000 + m_Spells.Count))
            {
                if (SpellRestrictions.UseRestrictions && !SpellRestrictions.CheckRestrictions(state.Mobile, m_Book.School))
                {
                    state.Mobile.SendMessage("You are not allowed to use this Spell.");
                    return;
                }

                CSpellInfo si = SpellInfoRegistry.GetInfo(m_Book.School, (Type)m_Spells[info.ButtonID - 1000]);
                if (si == null)
                {
                    state.Mobile.SendMessage("That Spell is disabled.");
                    return;
                }
                state.Mobile.CloseGump(typeof(NinjitsuGump));
                state.Mobile.SendGump(new ScrollGump(m_Book, si, TextHue, state.Mobile));
            }

            else if (info.ButtonID >= 2000 && info.ButtonID < (2000 + m_Spells.Count))
            {
                if (SpellRestrictions.UseRestrictions && !SpellRestrictions.CheckRestrictions(state.Mobile, m_Book.School))
                {
                    state.Mobile.SendMessage("You are not allowed to use this Spell.");
                    return;
                }

                if (!CSpellbook.MobileHasSpell(state.Mobile, m_Book.School, (Type)m_Spells[info.ButtonID - 2000]))
                {
                    state.Mobile.SendMessage("You do not have that Spell.");
                    return;
                }

                Spell spell = SpellInfoRegistry.NewSpell((Type)m_Spells[info.ButtonID - 2000], m_Book.School, state.Mobile, null);
                if (spell == null)
                    state.Mobile.SendMessage("That Spell is disabled.");
                else
                    spell.Cast();
            }

            object[] Params = new object[1] { m_Book };
            CSpellbookGump gump = Activator.CreateInstance(GumpType, Params) as CSpellbookGump;
            if (gump != null)
                state.Mobile.SendGump(gump);

            //GumpUpTimer
        }
    }
	*/
	/*
	public class NinjitsuScroll : Gump
    {
        private CSpellInfo m_Info;
        private string m_TextHue;
        private CSpellbook m_Book;
        private CastInfo m_CastInfo;
        private CastCommandsModule m_CastCommandModule;

        public NinjitsuScroll(CSpellbook book, CSpellInfo info, string textHue, Mobile sender)
            : base(485, 175)
        {
            if (info == null || book == null || !CSS.Running)
                return;

            m_Info = info;
            m_Book = book;
            m_TextHue = textHue;
            m_CastInfo = new CastInfo(info.Type, info.School);

            Closable = true;
            Disposable = true;
            Dragable = true;
            Resizable = false;

            AddPage(0);
            AddBackground(0, 0, 200, 265, 9380);	//AddBackground(0, 0, 200, 265, 9380);

            if (info.Name != null)
                AddHtml(30, 3, 140, 20, String.Format("<basefont color=#{0}><center>{1}</center></font>", textHue, info.Name), false, false);

            AddButton(30, 40, info.Icon, info.Icon, 3, GumpButtonType.Reply, 0);

            AddButton(90, 40, 2331, 2338, 1, GumpButtonType.Reply, 0);  //Cast
            AddLabel(120, 38, 0, "Cast");

            //AddButton( 90, 65, 2338, 2331, 2, GumpButtonType.Reply, 0 );  //Scribe
            //AddLabel( 120, 63, 0, "Scribe" );

            //Info
            string InfoString = "";
            if (info.Desc != null)
                InfoString += String.Format("<basefont color=black>{0}</font><br><br>", info.Desc);

            if (info.Regs != null)
            {
                string[] Regs = info.Regs.Split(';');
                InfoString += String.Format("<basefont color=black>Reagents :</font><br><basefont color=#{0}>", textHue);
                foreach (string r in Regs)
                    InfoString += String.Format("-{0}<br>", r.TrimStart());
                InfoString += "</font><br>";
            }

            if (info.Info != null)
            {
                string[] Info = info.Info.Split(';');
                InfoString += String.Format("<basefont color=#{0}>", textHue);
                foreach (string s in Info)
                    InfoString += String.Format("{0}<br>", s.TrimStart());
                InfoString += "</font><br>";
            }
            AddHtml(30, 95, 140, 130, InfoString, false, true);
            //End Info

            #region CastInfo
            if (CentralMemory.Running)
            {
                m_CastCommandModule = (CastCommandsModule)CentralMemory.GetModule(sender.Serial, typeof(CastCommandsModule));

                AddLabel(25, 242, 0, "Key :");
                if (m_CastCommandModule == null)
                    AddTextEntry(70, 242, 100, 20, 0, 5, "");  //Key	Loc,Size,Hue,ID
                else
                    AddTextEntry(70, 242, 100, 20, 0, 5, m_CastCommandModule.GetCommandForInfo(m_CastInfo));  //Key		Loc,Size,Hue,ID
                AddButton(175, 247, 2103, 2104, 4, GumpButtonType.Reply, 0);  //KeyButton
            }
            #endregion //CastInfo
        }

        public override void OnResponse(NetState state, RelayInfo info)
        {
            if (info.ButtonID == 0 || !CSS.Running)
                return;

            else if (info.ButtonID == 1)
            {
                if (SpellRestrictions.UseRestrictions && !SpellRestrictions.CheckRestrictions(state.Mobile, m_Info.School))
                {
                    state.Mobile.SendMessage("You are not allowed to use this Spell.");
                    return;
                }

                if (!CSpellbook.MobileHasSpell(state.Mobile, m_Info.School, m_Info.Type))
                {
                    state.Mobile.SendMessage("You do not have that Spell.");
                    return;
                }

                Spell spell = SpellInfoRegistry.NewSpell(m_Info.Type, m_Info.School, state.Mobile, null);
                if (spell == null)
                    state.Mobile.SendMessage("That Spell is disabled.");
                else
                    spell.Cast();
            }

            else if (info.ButtonID == 2)
            {
                //Scribe
            }

            else if (info.ButtonID == 3)
            {
                if (!CentralMemory.Running)
                    return;

                if (SpellRestrictions.UseRestrictions && !SpellRestrictions.CheckRestrictions(state.Mobile, m_Info.School))
                    return;

                state.Mobile.SendGump(new IconPlacementGump(m_Book, state.Mobile, 100, 100, 10, m_Info.Icon, m_Info.Type, m_Info.Back, m_Book.School));
            }

            else if (info.ButtonID == 4)
            {
                if (!CentralMemory.Running)
                    return;

                string command = info.GetTextEntry(5).Text;

                if (command == null || command.Length == 0)
                {
                    if (m_CastCommandModule == null)
                    {
                        state.Mobile.SendGump(new Holocron(m_Book, m_Info, m_TextHue, state.Mobile));
                        return;
                    }

                    m_CastCommandModule.RemoveCommandByInfo(m_CastInfo);
                    state.Mobile.SendGump(new Holocron(m_Book, m_Info, m_TextHue, state.Mobile));
                }
                else
                {
                    if (m_CastCommandModule == null)
                    {
                        CentralMemory.AddModule(new CastCommandsModule(state.Mobile.Serial, command, m_CastInfo));
                        state.Mobile.SendGump(new Holocron(m_Book, m_Info, m_TextHue, state.Mobile));
                        return;
                    }

                    m_CastCommandModule.Add(command, m_CastInfo);
                    state.Mobile.SendGump(new Holocron(m_Book, m_Info, m_TextHue, state.Mobile));
                }
            }
        }
    }
	*/
	
	public abstract class MysticismGump : Gump
    {
        private CSpellbook m_Book;
        private ArrayList m_Spells;

        private int Pages;
        public int CurrentPage;
		public string BookHue  { get{ return "333333"; } }
        public abstract string TextHue { get; }
        public abstract int BGImage { get; }
        public abstract int SpellBtn { get; }
        public abstract int SpellBtnP { get; }
        public abstract string Label1 { get; }
        public abstract string Label2 { get; }
        public abstract Type GumpType { get; }
		public abstract School School { get; }

        public MysticismGump(CSpellbook book)
            : base(50, 100)
        {
            if (!CSS.Running)
                return;

            m_Book = book;
            m_Spells = book.SchoolSpells;

            Pages = (int)Math.Ceiling((book.SpellCount / 16.0));

            AddPage(0);
            AddImage(70, 100, BGImage);

            CurrentPage = 1;

            for (int i = 0; i < Pages; i++, CurrentPage++)
            {
                AddPage(CurrentPage);

                //Hidden Buttons
                for (int j = (CurrentPage - 1) * 16, C = 0; j < CurrentPage * 16 && j < m_Spells.Count; j++, C++)	//Each set of two pages is 16 spells, e.g. Pages 1 & 2 are spells 1 - 16, 3 & 4 are 17 - 32, etc., etc.
                {
                    if (HasSpell((Type)m_Spells[j]))
                    {
                        AddButton((C > 7 ? 292 : 130), 146 + (C > 7 ? (C - 8) * 16 : C * 16), 2482, 2482, j + 1000, GumpButtonType.Reply, 0);
                    }
                }
				AddButton(73, 202, 11400, 11400, 6, GumpButtonType.Reply, 0);	//Minimize button
				AddButton(437, 130, 2643, 2643, 7, GumpButtonType.Reply, 0); //Master crafting right button
				AddButton(75, 230, 254, 254, 8, GumpButtonType.Reply, 0); //Tithe left button
                AddImage(70, 100, BGImage);
				AddHtml(145, 109, 100, 20, String.Format("<big><basefont color=#{0}><Center>{1}</Center></basefont>", TextHue, Label1), false, false);
                AddHtml(305, 109, 100, 20, String.Format("<big><basefont color=#{0}><Center>{1}</Center></basefont>", TextHue, Label2), false, false);
                //End Hidden Buttons

                //Prev/Next Buttons
                if (Pages > 1)
                {
                    if (CurrentPage > 1)
                        AddButton(123, 108, 2205, 2205, 0, GumpButtonType.Page, CurrentPage - 1);
                    if (CurrentPage < Pages)
                        AddButton(390, 108, 2206, 2206, 0, GumpButtonType.Page, CurrentPage + 1);
                }
                //End Prev/Next Buttons

                //Spell Buttons/Labels
                for (int j = (CurrentPage - 1) * 16, C = 0; j < CurrentPage * 16 && j < m_Spells.Count; j++, C++)
                {
                    if (HasSpell((Type)m_Spells[j]))
                    {
                        CSpellInfo info = SpellInfoRegistry.GetInfo(m_Book.School, (Type)m_Spells[j]);
                        if (info == null)
                            continue;
						

                        AddHtml((C > 7 ? 280 : 127), 140 + (C > 7 ? (C - 8) * 16 : C * 16), 110, 20, String.Format("<basefont color=#{0}>{1}</basefont>", BookHue, info.Name), false, false);
                        //AddButton((C > 7 ? 288 : 130), 138 + (C > 7 ? (C - 8) * 20 : C * 17), SpellBtn, SpellBtnP, j + 2000, GumpButtonType.Reply, 0);
                        AddButton((C > 7 ? 408 : 250), 143 + (C > 7 ? (C - 8) * 16 : C * 16), 0x938, 0x938, j + 2000, GumpButtonType.Reply, 0);
						//AddImage((C > 7 ? 408 : 250), 143 + (C > 7 ? (C - 8) * 16 : C * 16), 0x938, 0x938);
                    }
                }
                //End Spell Buttons/Labels
            }
        }

        public bool HasSpell(Type type)
        {
            return (m_Book != null && m_Book.HasSpell(type));
        }

        public override void OnResponse(NetState state, RelayInfo info)
        {
			Mobile m = state.Mobile;

            if (info.ButtonID == 0 || !CSS.Running)
                return;

			else if (info.ButtonID == 6)
			{
                state.Mobile.CloseGump(typeof(MysticismGump));
                state.Mobile.SendGump(new MysticismMiniGump(m_Book));
			}

			else if (info.ButtonID == 7)
			{
				//Add Master Craft Menu to select specific crafting menu.
                m.SendMessage( "What do you want to Craft?" );
				m.SendGump( new MasterCraftGump(m) );
			}

			else if (info.ButtonID == 8)
			{
				if ( state.Mobile.CheckAlive() )
					state.Mobile.SendGump( new TithingGump( state.Mobile, 0 ) );
			}

            else if (info.ButtonID >= 1000 && info.ButtonID < (1000 + m_Spells.Count))
            {
                if (SpellRestrictions.UseRestrictions && !SpellRestrictions.CheckRestrictions(state.Mobile, m_Book.School))
                {
                    state.Mobile.SendMessage("You are not allowed to use this Spell.");
                    return;
                }

                CSpellInfo si = SpellInfoRegistry.GetInfo(m_Book.School, (Type)m_Spells[info.ButtonID - 1000]);
                if (si == null)
                {
                    state.Mobile.SendMessage("That Spell is disabled.");
                    return;
                }
                state.Mobile.CloseGump(typeof(MysticismGump));
                state.Mobile.SendGump(new ScrollGump(m_Book, si, TextHue, state.Mobile));
            }

            else if (info.ButtonID >= 2000 && info.ButtonID < (2000 + m_Spells.Count))
            {
                if (SpellRestrictions.UseRestrictions && !SpellRestrictions.CheckRestrictions(state.Mobile, m_Book.School))
                {
                    state.Mobile.SendMessage("You are not allowed to use this Spell.");
                    return;
                }

                if (!CSpellbook.MobileHasSpell(state.Mobile, m_Book.School, (Type)m_Spells[info.ButtonID - 2000]))
                {
                    state.Mobile.SendMessage("You do not have that Spell.");
                    return;
                }

                Spell spell = SpellInfoRegistry.NewSpell((Type)m_Spells[info.ButtonID - 2000], m_Book.School, state.Mobile, null);
                if (spell == null)
                    state.Mobile.SendMessage("That Spell is disabled.");
                else
                    spell.Cast();
            }

            object[] Params = new object[1] { m_Book };
            CSpellbookGump gump = Activator.CreateInstance(GumpType, Params) as CSpellbookGump;
            if (gump != null)
                state.Mobile.SendGump(gump);

            //GumpUpTimer
        }
    }
	/*public class MysticismScroll : Gump
    {
        private CSpellInfo m_Info;
        private string m_TextHue;
        private CSpellbook m_Book;
        private CastInfo m_CastInfo;
        private CastCommandsModule m_CastCommandModule;

        public MysticismScroll(CSpellbook book, CSpellInfo info, string textHue, Mobile sender)
            : base(485, 175)
        {
            if (info == null || book == null || !CSS.Running)
                return;

            m_Info = info;
            m_Book = book;
            m_TextHue = textHue;
            m_CastInfo = new CastInfo(info.Type, info.School);

            Closable = true;
            Disposable = true;
            Dragable = true;
            Resizable = false;

            AddPage(0);
            AddBackground(0, 0, 200, 265, 9380);	//AddBackground(0, 0, 200, 265, 9380);

            if (info.Name != null)
                AddHtml(30, 3, 140, 20, String.Format("<basefont color=#{0}><center>{1}</center></font>", textHue, info.Name), false, false);

            AddButton(30, 40, info.Icon, info.Icon, 3, GumpButtonType.Reply, 0);

            AddButton(90, 40, 2331, 2338, 1, GumpButtonType.Reply, 0);  //Cast
            AddLabel(120, 38, 0, "Cast");

            //AddButton( 90, 65, 2338, 2331, 2, GumpButtonType.Reply, 0 );  //Scribe
            //AddLabel( 120, 63, 0, "Scribe" );

            //Info
            string InfoString = "";
            if (info.Desc != null)
                InfoString += String.Format("<basefont color=black>{0}</font><br><br>", info.Desc);

            if (info.Regs != null)
            {
                string[] Regs = info.Regs.Split(';');
                InfoString += String.Format("<basefont color=black>Reagents :</font><br><basefont color=#{0}>", textHue);
                foreach (string r in Regs)
                    InfoString += String.Format("-{0}<br>", r.TrimStart());
                InfoString += "</font><br>";
            }

            if (info.Info != null)
            {
                string[] Info = info.Info.Split(';');
                InfoString += String.Format("<basefont color=#{0}>", textHue);
                foreach (string s in Info)
                    InfoString += String.Format("{0}<br>", s.TrimStart());
                InfoString += "</font><br>";
            }
            AddHtml(30, 95, 140, 130, InfoString, false, true);
            //End Info

            #region CastInfo
            if (CentralMemory.Running)
            {
                m_CastCommandModule = (CastCommandsModule)CentralMemory.GetModule(sender.Serial, typeof(CastCommandsModule));

                AddLabel(25, 242, 0, "Key :");
                if (m_CastCommandModule == null)
                    AddTextEntry(70, 242, 100, 20, 0, 5, "");  //Key	Loc,Size,Hue,ID
                else
                    AddTextEntry(70, 242, 100, 20, 0, 5, m_CastCommandModule.GetCommandForInfo(m_CastInfo));  //Key		Loc,Size,Hue,ID
                AddButton(175, 247, 2103, 2104, 4, GumpButtonType.Reply, 0);  //KeyButton
            }
            #endregion //CastInfo
        }

        public override void OnResponse(NetState state, RelayInfo info)
        {
            if (info.ButtonID == 0 || !CSS.Running)
                return;

            else if (info.ButtonID == 1)
            {
                if (SpellRestrictions.UseRestrictions && !SpellRestrictions.CheckRestrictions(state.Mobile, m_Info.School))
                {
                    state.Mobile.SendMessage("You are not allowed to use this Spell.");
                    return;
                }

                if (!CSpellbook.MobileHasSpell(state.Mobile, m_Info.School, m_Info.Type))
                {
                    state.Mobile.SendMessage("You do not have that Spell.");
                    return;
                }

                Spell spell = SpellInfoRegistry.NewSpell(m_Info.Type, m_Info.School, state.Mobile, null);
                if (spell == null)
                    state.Mobile.SendMessage("That Spell is disabled.");
                else
                    spell.Cast();
            }

            else if (info.ButtonID == 2)
            {
                //Scribe
            }

            else if (info.ButtonID == 3)
            {
                if (!CentralMemory.Running)
                    return;

                if (SpellRestrictions.UseRestrictions && !SpellRestrictions.CheckRestrictions(state.Mobile, m_Info.School))
                    return;

                state.Mobile.SendGump(new IconPlacementGump(m_Book, state.Mobile, 100, 100, 10, m_Info.Icon, m_Info.Type, m_Info.Back, m_Book.School));
            }

            else if (info.ButtonID == 4)
            {
                if (!CentralMemory.Running)
                    return;

                string command = info.GetTextEntry(5).Text;

                if (command == null || command.Length == 0)
                {
                    if (m_CastCommandModule == null)
                    {
                        state.Mobile.SendGump(new Holocron(m_Book, m_Info, m_TextHue, state.Mobile));
                        return;
                    }

                    m_CastCommandModule.RemoveCommandByInfo(m_CastInfo);
                    state.Mobile.SendGump(new Holocron(m_Book, m_Info, m_TextHue, state.Mobile));
                }
                else
                {
                    if (m_CastCommandModule == null)
                    {
                        CentralMemory.AddModule(new CastCommandsModule(state.Mobile.Serial, command, m_CastInfo));
                        state.Mobile.SendGump(new Holocron(m_Book, m_Info, m_TextHue, state.Mobile));
                        return;
                    }

                    m_CastCommandModule.Add(command, m_CastInfo);
                    state.Mobile.SendGump(new Holocron(m_Book, m_Info, m_TextHue, state.Mobile));
                }
            }
        }
    }*/
	public abstract class SpellweavingGump : Gump
    {
        private CSpellbook m_Book;
        private ArrayList m_Spells;

        private int Pages;
        public int CurrentPage;
		public string BookHue  { get{ return "144898"; } }
        public abstract string TextHue { get; }
        public abstract int BGImage { get; }
        public abstract int SpellBtn { get; }
        public abstract int SpellBtnP { get; }
        public abstract string Label1 { get; }
        public abstract string Label2 { get; }
        public abstract Type GumpType { get; }
		public abstract School School { get; }

        public SpellweavingGump(CSpellbook book)
            : base(50, 100)
        {
            if (!CSS.Running)
                return;

            m_Book = book;
            m_Spells = book.SchoolSpells;

            Pages = (int)Math.Ceiling((book.SpellCount / 16.0));

            AddPage(0);
            AddImage(70, 100, BGImage);

            CurrentPage = 1;

            for (int i = 0; i < Pages; i++, CurrentPage++)
            {
                AddPage(CurrentPage);

                //Hidden Buttons
                for (int j = (CurrentPage - 1) * 16, C = 0; j < CurrentPage * 16 && j < m_Spells.Count; j++, C++)	//Each set of two pages is 16 spells, e.g. Pages 1 & 2 are spells 1 - 16, 3 & 4 are 17 - 32, etc., etc.
                {
                    if (HasSpell((Type)m_Spells[j]))
                    {
                        AddButton((C > 7 ? 292 : 130), 146 + (C > 7 ? (C - 8) * 16 : C * 16), 2482, 2482, j + 1000, GumpButtonType.Reply, 0);
                    }
                }
				AddButton(73, 202, 11400, 11400, 6, GumpButtonType.Reply, 0);	//Minimize button
				AddButton(437, 130, 2643, 2643, 7, GumpButtonType.Reply, 0); //Master crafting right button
				AddButton(77, 238, 254, 254, 8, GumpButtonType.Reply, 0); //Tithe left button
                AddImage(70, 100, BGImage);
				AddHtml(145, 112, 100, 20, String.Format("<big><basefont color=#{0}><Center>{1}</Center></basefont>", TextHue, Label1), false, false);
                AddHtml(305, 112, 100, 20, String.Format("<big><basefont color=#{0}><Center>{1}</Center></basefont>", TextHue, Label2), false, false);
                //End Hidden Buttons

                //Prev/Next Buttons
                if (Pages > 1)
                {
                    if (CurrentPage > 1)
                        AddButton(123, 108, 2205, 2205, 0, GumpButtonType.Page, CurrentPage - 1);
                    if (CurrentPage < Pages)
                        AddButton(390, 108, 2206, 2206, 0, GumpButtonType.Page, CurrentPage + 1);
                }
                //End Prev/Next Buttons

                //Spell Buttons/Labels
                for (int j = (CurrentPage - 1) * 16, C = 0; j < CurrentPage * 16 && j < m_Spells.Count; j++, C++)
                {
                    if (HasSpell((Type)m_Spells[j]))
                    {
                        CSpellInfo info = SpellInfoRegistry.GetInfo(m_Book.School, (Type)m_Spells[j]);
                        if (info == null)
                            continue;
						
						
                        AddHtml((C > 7 ? 277 : 124), 140 + (C > 7 ? (C - 8) * 16 : C * 16), 150, 20, String.Format("<basefont color=#{0}>{1}</basefont>", BookHue, info.Name), false, false);
                        //AddButton((C > 7 ? 288 : 130), 138 + (C > 7 ? (C - 8) * 20 : C * 17), SpellBtn, SpellBtnP, j + 2000, GumpButtonType.Reply, 0);
                        AddButton((C > 7 ? 412 : 253), 140 + (C > 7 ? (C - 8) * 16 : C * 16), 0x827, 0x827, j + 2000, GumpButtonType.Reply, 0);
						AddImage((C > 7 ? 412 : 253), 140 + (C > 7 ? (C - 8) * 16 : C * 16), 0x827, 0x827);
                    }
                }
                //End Spell Buttons/Labels
            }
        }

        public bool HasSpell(Type type)
        {
            return (m_Book != null && m_Book.HasSpell(type));
        }

        public override void OnResponse(NetState state, RelayInfo info)
        {
			Mobile m = state.Mobile;

            if (info.ButtonID == 0 || !CSS.Running)
                return;

			else if (info.ButtonID == 6)
			{
                state.Mobile.CloseGump(typeof(SpellweavingGump));
                state.Mobile.SendGump(new SpellweavingMiniGump(m_Book));
			}

			else if (info.ButtonID == 7)
			{
				//Add Master Craft Menu to select specific crafting menu.
                m.SendMessage( "What do you want to Craft?" );
				m.SendGump( new MasterCraftGump(m) );
			}

			else if (info.ButtonID == 8)
			{
				if ( state.Mobile.CheckAlive() )
					state.Mobile.SendGump( new TithingGump( state.Mobile, 0 ) );
			}

            else if (info.ButtonID >= 1000 && info.ButtonID < (1000 + m_Spells.Count))
            {
                if (SpellRestrictions.UseRestrictions && !SpellRestrictions.CheckRestrictions(state.Mobile, m_Book.School))
                {
                    state.Mobile.SendMessage("You are not allowed to use this Spell.");
                    return;
                }

                CSpellInfo si = SpellInfoRegistry.GetInfo(m_Book.School, (Type)m_Spells[info.ButtonID - 1000]);
                if (si == null)
                {
                    state.Mobile.SendMessage("That Spell is disabled.");
                    return;
                }
                state.Mobile.CloseGump(typeof(SpellweavingGump));
                state.Mobile.SendGump(new ScrollGump(m_Book, si, TextHue, state.Mobile));
            }

            else if (info.ButtonID >= 2000 && info.ButtonID < (2000 + m_Spells.Count))
            {
                if (SpellRestrictions.UseRestrictions && !SpellRestrictions.CheckRestrictions(state.Mobile, m_Book.School))
                {
                    state.Mobile.SendMessage("You are not allowed to use this Spell.");
                    return;
                }

                if (!CSpellbook.MobileHasSpell(state.Mobile, m_Book.School, (Type)m_Spells[info.ButtonID - 2000]))
                {
                    state.Mobile.SendMessage("You do not have that Spell.");
                    return;
                }

                Spell spell = SpellInfoRegistry.NewSpell((Type)m_Spells[info.ButtonID - 2000], m_Book.School, state.Mobile, null);
                if (spell == null)
                    state.Mobile.SendMessage("That Spell is disabled.");
                else
                    spell.Cast();
            }

            object[] Params = new object[1] { m_Book };
            CSpellbookGump gump = Activator.CreateInstance(GumpType, Params) as CSpellbookGump;
            if (gump != null)
                state.Mobile.SendGump(gump);

            //GumpUpTimer
        }
    }
	/*public class SpellweavingScroll : Gump
    {
        private CSpellInfo m_Info;
        private string m_TextHue;
        private CSpellbook m_Book;
        private CastInfo m_CastInfo;
        private CastCommandsModule m_CastCommandModule;

        public MysticismScroll(CSpellbook book, CSpellInfo info, string textHue, Mobile sender)
            : base(485, 175)
        {
            if (info == null || book == null || !CSS.Running)
                return;

            m_Info = info;
            m_Book = book;
            m_TextHue = textHue;
            m_CastInfo = new CastInfo(info.Type, info.School);

            Closable = true;
            Disposable = true;
            Dragable = true;
            Resizable = false;

            AddPage(0);
            AddBackground(0, 0, 200, 265, 9380);	//AddBackground(0, 0, 200, 265, 9380);

            if (info.Name != null)
                AddHtml(30, 3, 140, 20, String.Format("<basefont color=#{0}><center>{1}</center></font>", textHue, info.Name), false, false);

            AddButton(30, 40, info.Icon, info.Icon, 3, GumpButtonType.Reply, 0);

            AddButton(90, 40, 2331, 2338, 1, GumpButtonType.Reply, 0);  //Cast
            AddLabel(120, 38, 0, "Cast");

            //AddButton( 90, 65, 2338, 2331, 2, GumpButtonType.Reply, 0 );  //Scribe
            //AddLabel( 120, 63, 0, "Scribe" );

            //Info
            string InfoString = "";
            if (info.Desc != null)
                InfoString += String.Format("<basefont color=black>{0}</font><br><br>", info.Desc);

            if (info.Regs != null)
            {
                string[] Regs = info.Regs.Split(';');
                InfoString += String.Format("<basefont color=black>Reagents :</font><br><basefont color=#{0}>", textHue);
                foreach (string r in Regs)
                    InfoString += String.Format("-{0}<br>", r.TrimStart());
                InfoString += "</font><br>";
            }

            if (info.Info != null)
            {
                string[] Info = info.Info.Split(';');
                InfoString += String.Format("<basefont color=#{0}>", textHue);
                foreach (string s in Info)
                    InfoString += String.Format("{0}<br>", s.TrimStart());
                InfoString += "</font><br>";
            }
            AddHtml(30, 95, 140, 130, InfoString, false, true);
            //End Info

            #region CastInfo
            if (CentralMemory.Running)
            {
                m_CastCommandModule = (CastCommandsModule)CentralMemory.GetModule(sender.Serial, typeof(CastCommandsModule));

                AddLabel(25, 242, 0, "Key :");
                if (m_CastCommandModule == null)
                    AddTextEntry(70, 242, 100, 20, 0, 5, "");  //Key	Loc,Size,Hue,ID
                else
                    AddTextEntry(70, 242, 100, 20, 0, 5, m_CastCommandModule.GetCommandForInfo(m_CastInfo));  //Key		Loc,Size,Hue,ID
                AddButton(175, 247, 2103, 2104, 4, GumpButtonType.Reply, 0);  //KeyButton
            }
            #endregion //CastInfo
        }

        public override void OnResponse(NetState state, RelayInfo info)
        {
            if (info.ButtonID == 0 || !CSS.Running)
                return;

            else if (info.ButtonID == 1)
            {
                if (SpellRestrictions.UseRestrictions && !SpellRestrictions.CheckRestrictions(state.Mobile, m_Info.School))
                {
                    state.Mobile.SendMessage("You are not allowed to use this Spell.");
                    return;
                }

                if (!CSpellbook.MobileHasSpell(state.Mobile, m_Info.School, m_Info.Type))
                {
                    state.Mobile.SendMessage("You do not have that Spell.");
                    return;
                }

                Spell spell = SpellInfoRegistry.NewSpell(m_Info.Type, m_Info.School, state.Mobile, null);
                if (spell == null)
                    state.Mobile.SendMessage("That Spell is disabled.");
                else
                    spell.Cast();
            }

            else if (info.ButtonID == 2)
            {
                //Scribe
            }

            else if (info.ButtonID == 3)
            {
                if (!CentralMemory.Running)
                    return;

                if (SpellRestrictions.UseRestrictions && !SpellRestrictions.CheckRestrictions(state.Mobile, m_Info.School))
                    return;

                state.Mobile.SendGump(new IconPlacementGump(m_Book, state.Mobile, 100, 100, 10, m_Info.Icon, m_Info.Type, m_Info.Back, m_Book.School));
            }

            else if (info.ButtonID == 4)
            {
                if (!CentralMemory.Running)
                    return;

                string command = info.GetTextEntry(5).Text;

                if (command == null || command.Length == 0)
                {
                    if (m_CastCommandModule == null)
                    {
                        state.Mobile.SendGump(new Holocron(m_Book, m_Info, m_TextHue, state.Mobile));
                        return;
                    }

                    m_CastCommandModule.RemoveCommandByInfo(m_CastInfo);
                    state.Mobile.SendGump(new Holocron(m_Book, m_Info, m_TextHue, state.Mobile));
                }
                else
                {
                    if (m_CastCommandModule == null)
                    {
                        CentralMemory.AddModule(new CastCommandsModule(state.Mobile.Serial, command, m_CastInfo));
                        state.Mobile.SendGump(new Holocron(m_Book, m_Info, m_TextHue, state.Mobile));
                        return;
                    }

                    m_CastCommandModule.Add(command, m_CastInfo);
                    state.Mobile.SendGump(new Holocron(m_Book, m_Info, m_TextHue, state.Mobile));
                }
            }
        }
    }*/
}