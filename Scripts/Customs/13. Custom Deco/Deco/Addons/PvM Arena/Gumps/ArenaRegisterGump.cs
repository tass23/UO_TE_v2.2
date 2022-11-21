//################################################################
//By Rocko Wang
//www.medievaldream.com
//Thanks to "The world of dreams" and Drocket for the idea of this
//################################################################


using System;
using Server;
using Server.Gumps;
using Server.Network;
using Server.Items;
using Server.Mobiles;
using Server.Game.Arena;

namespace Server.Gumps
{
    public class ArenaRegisterGump : Gump
    {
        private ArenaFightType m_FightType = ArenaFightType.None;
        private ArenaControlStone m_Arena;
        //private QuestDifficulty m_Difficulty = QuestDifficulty.Normal;
        //private QuestMobGroup m_MobGroup = QuestMobGroup.Undead;
        //private bool m_IsParty = false;

        public ArenaRegisterGump(Mobile from, ArenaControlStone arena, ArenaFightType qt) //, QuestMobGroup mg, bool isParty)
            : base(200, 50)
        {
            m_FightType = qt;
            m_Arena = arena;
            //m_Difficulty = qd;
            //m_MobGroup = mg;
            //m_IsParty = isParty;

            from.CloseGump(typeof(ArenaRegisterGump));
            // from.CloseGump(typeof(PartyQuestGump));

            string toDisplay = "";
            switch (m_FightType)
            {
                case ArenaFightType.None: toDisplay = @"This is the grand arena of Mistas. Please register your preferred type of trainning."; break;
                case ArenaFightType.SingleFight: toDisplay = @"Test your skill vs a single monster. Right now the opponent is a random one among 5. Leave the arena during combat count as a lose."; break;
                case ArenaFightType.ChallangeGame: toDisplay = @"In the challange game, you will fight through a series of monsters with progression in difficulty. Your final score is based on how far you go along the chain."; break;
                case ArenaFightType.PVP: toDisplay = @"Fighting with another player is on a consensual base. Please target another player to invite him/her for a duel. Once he accepts your challange, you can start your fight inside the arena."; break;
            }


            this.Closable = true;
            this.Disposable = true;
            this.Dragable = true;
            this.Resizable = false;

            this.AddPage(0);

            this.AddBackground(0, 0, 455, 400, 9380);
            this.AddLabel(168, 57, 0, @"Grand Arena");
            this.AddImage(89, 38, 5578);
            this.AddImage(286, 38, 5578);

            this.AddButton(118, 203, m_FightType == ArenaFightType.SingleFight ? 209 : 208, 209, (int)Buttons.SingleFightButton, GumpButtonType.Reply, 0);
            this.AddButton(118, 260, m_FightType == ArenaFightType.ChallangeGame ? 209 : 208, 209, (int)Buttons.ChallangeGameButton, GumpButtonType.Reply, 0);
            this.AddButton(118, 314, m_FightType == ArenaFightType.PVP ? 209 : 208, 209, (int)Buttons.PVPButton, GumpButtonType.Reply, 0);
            this.AddHtml(36, 111, 381, 64, toDisplay, (bool)true, (bool)true);

            this.AddLabel(154, 202, 0, @"Single Fight");
            this.AddLabel(149, 260, 0, @"Challange Game");
            this.AddLabel(170, 314, 0, @"PVP");

            this.AddItem(60, 190, 8330);
            this.AddItem(60, 305, 9104);
            this.AddItem(60, 247, 8344);

            this.AddButton(269, 324, 247, 248, (int)Buttons.AcceptButton, GumpButtonType.Reply, 0);
            this.AddButton(346, 324, 241, 242, (int)Buttons.CancelButton, GumpButtonType.Reply, 0);
            this.AddLabel(270, 290, 0, @"Do you wish to accept?");


        }

        public enum Buttons
        {
            SingleFightButton,
            ChallangeGameButton,
            PVPButton,
            AcceptButton,
            CancelButton,
        }


        public override void OnResponse(NetState state, RelayInfo info)
        {
            Mobile from = state.Mobile;

            switch (info.ButtonID)
            {
                case (int)Buttons.SingleFightButton:
                    from.SendGump(new ArenaRegisterGump(from, m_Arena, ArenaFightType.SingleFight));
                    break;
                case (int)Buttons.ChallangeGameButton:
                    from.SendGump(new ArenaRegisterGump(from, m_Arena, ArenaFightType.ChallangeGame));
                    break;
                case (int)Buttons.PVPButton:
                    from.SendGump(new ArenaRegisterGump(from, m_Arena, ArenaFightType.PVP));
                    break;
                case (int)Buttons.CancelButton:
                    break;
                case (int)Buttons.AcceptButton:
                    switch (m_FightType)
                    {
                        case ArenaFightType.None:
                            ArenaRegisterGump gump = new ArenaRegisterGump(from, m_Arena, m_FightType); //, difficulty, m_MobGroup, false);
                            gump.AddHtml(36, 111, 381, 64, @"You must select a fight type first.", (bool)true, (bool)true);
                            from.SendGump(gump);
                            break;
                        case ArenaFightType.PVP:
                            from.SendMessage("The PVP has not been implemented. Please stay tuned.");
                            break;
                        case ArenaFightType.ChallangeGame:
                            from.SendMessage("The challange game has not been implemented. Please stay tuned.");
                            break;
                        case ArenaFightType.SingleFight:
                            if (!from.Alive)
                                from.SendMessage("Bring yourself back to life first is a better idea.");
                            else if (m_Arena == null || m_Arena.Deleted)
                                from.SendMessage("There is something wrong with the arena. Please report to a GM.");
                            else if (!m_Arena.Running)
                                from.SendMessage("The arena is under maintainance, please check back later.");
                            else if (m_Arena.ActiveUser != null)
                                from.SendMessage("The arena seems to be in use by someone else. Please wait until that player finishes.");
                            else
                            {
                                Type type = typeof(Lich);
                                switch (Utility.Random(5))
                                {
                                    case 0: type = typeof(Ogre); break;
                                    case 1: type = typeof(Daemon); break;
                                    case 2: type = typeof(Lich); break;
                                    case 3: type = typeof(FireElemental); break;
                                    case 4: type = typeof(Executioner); break;
                                }
                                if (m_Arena.InEvent && m_Arena.EventOpponentType != null)
                                    type = m_Arena.EventOpponentType;
                                m_Arena.PreparePVMCombat(from, type);
                            }
                            break;
                        /*
                        if (m_IsParty)
                        {
                            if (QuestCenter.LeaderOfParty(from) == null)
                                from.SendMessage("I am afraid you are not a leader of a party anymore.");
                            else
                            {
                                QuestCenter.AssignQuestParty((PlayerMobile)from, m_QuestType, difficulty, m_MobGroup);
                                from.SendMessage("Good luck, " + from.Name + ", the quest will start once all your party members accept the quest.");
                            }
                        }
                        else
                        {
                            QuestCenter.AssignQuest((PlayerMobile)from, m_QuestType, difficulty, m_MobGroup);
                            from.SendMessage("Good luck, " + from.Name + ", the safety of our village counts on you!");
                        }
                         */
                    }
                    break;
            }
        }
    }
}