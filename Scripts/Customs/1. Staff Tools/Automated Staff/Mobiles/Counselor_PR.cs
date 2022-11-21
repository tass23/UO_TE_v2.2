
#region Automated Server Staff Acknowledgements
/*
    This Automated NPC System Idea Originated
    With A Script Coded By: Tresdni from the
    RunUO (www.runuo.com) Forums; It Is Named:
    
       **Completely Automated Staff Team** 
 http://www.runuo.com/community/threads/completely-automated-staff-team-oh-yes-i-did.460720/
 
    This Released Version Of The Script Named
    Above Is My Own Variation On What I Think
    Might Complete The System Tresdni Started.
    
    The Code In This Script File Is Annoted. I
    Have Regioned Out Most Areas And Outlined
    Others So That You Know What Code Can Be
    Copy And Pasted To Other Scripts To Add The
    Same Functionality For Another System. 
 
    The Author Of Each Line Of Code Varies, I
    Got The Shell Of This Script From Tresdni,
    However A Lot Has Come From Many Other 
    Sources Over The Years; I Have A Library
    Of Annotated Methods I've Been Working On,
    That Help Me Build The Scripts I Upload.
 
    A Special Thank You Goes Out To The Following
    People For Helping Me Complete This System
    Addition To The Completely Automated Staff Team,
    Written By: Tresdni:
 
    THANK YOU GUYS!! THE HELP WAS MUCH APPRECIATED
                   -Sythen (A.A.R)-
    ______________________________________________
    ** JamzeMcC | Morexton | Soteric | James420 **
    ----------------------------------------------
 */
#endregion Edited By: A.A.R

using System;
using System.Text;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Server;
using Server.Targeting;
using Server.Items;
using Server.ContextMenus;
using Server.Multis;
using Server.Regions;
using Server.Engines.CannedEvil;
using Server.Spells;
using Server.Commands;
using Server.Gumps;
using Server.Mobiles;
using Server.Accounting;
using Server.Misc;
using Server.Network;

namespace Server.Mobiles
{
    public class Counselor_PR : BaseGuildmaster
    {
        private List<SBInfo> m_SBInfos = new List<SBInfo>();
        protected override List<SBInfo> SBInfos { get { return m_SBInfos; } }

        public override NpcGuild NpcGuild { get { return NpcGuild.MerchantsGuild; } }

        #region Interactions: Based On Keywords

        private bool m_Gated;

        #endregion Edited By: A.A.R

        #region NPC Auto-Deletion Timer

        private DateTime m_npcAutoDelete;

        #endregion Edited By: A.A.R
		
        #region Automated Greetings For Players

        private static bool m_Talked;

        string[] npcSpeech = new string[]
        { 
            "Welcome traveller! I am a member of the automated staff, how may I assist thee?",     
        };

        #endregion Edited By: A.A.R

        [Constructable]
        public Counselor_PR(): base("merchant")
        {

//----------This Randomizes The Sex Of The NPC--------------------//

            if (this.Female = Utility.RandomBool())
            {
                Body = 0x191;
                Name = NameList.RandomName("female");
            }
            else
            {
                Body = 0x190;
                Name = NameList.RandomName("male");
            }

//----------This Creates A Random Look To The NPC-----------------//

            Title = "[PR]";
            NameHue = 11;

            VendorAccessLevel = AccessLevel.Administrator;
            AccessLevel = AccessLevel.Counselor;

            SpeechHue = Utility.RandomDyedHue();
            Hue = Utility.RandomSkinHue();

            Item hair = new Item(Utility.RandomList(0x203B, 0x2049, 0x2048, 0x204A));
            hair.Hue = Utility.RandomNondyedHue();
            hair.Layer = Layer.Hair;
            hair.Movable = false;
            AddItem(hair);

            if (Utility.RandomBool() && !this.Female)
            {
                Item beard = new Item(Utility.RandomList(0x203E, 0x203F, 0x2040, 0x2041, 0x204B, 0x204C, 0x204D));

                beard.Hue = hair.Hue;
                beard.Layer = Layer.FacialHair;
                beard.Movable = false;

                AddItem(beard);
            }

//----------This Toggles The NPC Movement: On Or Off--------------//

            CantWalk = true;

//----------This Makes The NPC Equip HandHeld Items---------------//

            switch (Utility.Random(3))
            {
                case 0: AddItem(new BookOfNinjitsu()); break;
                case 1: AddItem(new BookOfBushido()); break;
                case 2: AddItem(new BookOfChivalry()); break;
            }

//----------This Sets What Cloth The NPC Will Wear----------------//

            Counselor_PR_Robe robe = new Counselor_PR_Robe();
            robe.AccessLevel = AccessLevel.Counselor;
            robe.Movable = false;
            robe.Hue = 0x3;
            robe.LootType = LootType.Blessed;
            AddItem(robe);

//----------NPC Auto-Deletion Timer (Timer Set At 5min)----------//

            m_npcAutoDelete = DateTime.Now + TimeSpan.FromSeconds(300);
        }

        public override void OnThink()
        {
            if (m_npcAutoDelete <= DateTime.Now)
            {
                this.Delete();
            }
            base.OnThink();
        }
		
//------This Gives The NPC Some Life In The Game------------------//

        public void Emote()
        {
            switch (Utility.Random(2))
            {
                case 0:
                    PlaySound(Female ? 785 : 1056);
                    Say("*cough!*");
                    break;
                case 1:
                    PlaySound(Female ? 818 : 1092);
                    Say("*sniff*");
                    break;
                default:
                    break;
            }
        }

//-------This Code Makes This NPC Behave As An NPC Vendor---------//

        /*
		public override void InitSBInfo()
        {
            m_SBInfos.Add(new SBCounselor());
        }
		*/

//----------------------------------------------------------------//

        #region Automated Greetings For Players

        public override void OnMovement(Mobile m, Point3D oldLocation)
        {
            if (m.InRange(this, 3) && m is PlayerMobile)
            {
                if (!m.HasGump(typeof(PR_StaffKeywords)))
                    m.SendGump(new PR_StaffKeywords());
            }
            if (!m.InRange(this, 3) && m is PlayerMobile)
            {
                if (m.HasGump(typeof(PR_StaffKeywords)))
                    m.CloseGump(typeof(PR_StaffKeywords));
            }

            if (m_Talked == false)
            {
                if (m.InRange(this, 4))
                {
                    m_Talked = true;
                    SayRandom(npcSpeech, this);
                    this.Move(GetDirectionTo(m.Location));
                    m.SendMessage("Okay, you got me here. Use the list I provided you for more information.");

                    // Start timer to prevent spam 
                    SpamTimer_PR t = new SpamTimer_PR();
                    t.Start();
                }
            }
        }

        private class SpamTimer_PR : Timer
        {
            public SpamTimer_PR()
                : base(TimeSpan.FromSeconds(20))
            {
                Priority = TimerPriority.OneSecond;
            }

            protected override void OnTick()
            {
                m_Talked = false;
            }
        }

        private static void SayRandom(string[] say, Mobile m)
        {
            m.Say(say[Utility.Random(say.Length)]);
        }

        #endregion Edited By: A.A.R

        #region Interactions: Based On Keywords

        #region Keyword Listing - A Quick Reference
        /*   
            > serverinfo....... //launch browser
            > tosagreement..... //launch browser          
            > serverrules...... //launch browser
            > meetourstaff..... //launch browser
            > showcredits...... //launch browser
                 
            > livesupport...... //page livestaff
                 
            > skillcap..........//text displayed
            > skills........... //launch browser
            > statcap.......... //text displayed 
            > playerguide...... //launch browser
            > bestiary......... //launch browser
                    
            > events........... //launch browser
            > eventrequest..... //gump displayed
            > hiring........... //submition gump
            > suggestion....... //submition gump
            > donations........ //submition gump                          
        */
        #endregion Edited By: A.A.R

        #region NPC Counselors - Unacceptable Words

        public override bool HandlesOnSpeech(Mobile from)
        {
            if (from.InRange(this.Location, 5))
                return true;
            return base.HandlesOnSpeech(from);
        }

        #region Unacceptable Word List

        private List<string> m_UnacceptableWords = new List<string>(new string[]{"ass","asshole","blowjob","bitch","bitches","biatch","biatches","breasts","chinc","chink","cunnilingus","cum","cumstain","cocksucker","clit",
                "chigaboo","cunt","clitoris","cock","dick","dickhead","dyke","dildo","fuck","fucktard","felatio","fag","faggot","hitler","jigaboo","jizzm","jizz","jiz","jism","jiss","jis","jerkoff","jackoff", "kyke","kike",
                "klit","lezbo","lesbo","nigga","niggas","nigger","piss","penis","prick","pussy","retard","retarded","spic","shit","spunk","spunker","smeg","smegg","twat","tit","tits","titties", "tittys","titie","tities",
                "tity","tard","vagina","wop","wigger","wiger"});

        #endregion Edited By: A.A.R

        private bool ContainsUnacceptableWords(string speech)
        {
            string[] speechArray = speech.Split(' ');

            foreach (string word in speechArray)
            {
                if (m_UnacceptableWords.Contains(word.ToLower()))
                {
                    return true;
                }
            }

            return false;
        }

        public override void OnSpeech(SpeechEventArgs args )
        {
            string said = args.Speech.ToLower();
            Mobile from = args.Mobile;

            if (ContainsUnacceptableWords(said))
            {
                from.MoveToWorld(new Point3D(1483, 1617, 20), Map.Trammel); //Location To Send Players If They Say Unacceptable Words
                from.SendMessage("You Have Been Jailed For Using Inappropriate Language And/Or Out Of Character, Real-World, References In Front Of A Staff Member");
                return;
            }

        #endregion Edited By: Morxeton

            switch (said)
            {
                //General Information

                #region serverinfo
                //Some People Are Interested About How Your Server Came To Be. Tell Them!

                case ("serverinfo"):
                    {
                        Say(String.Format("Ahhh! Inquisitive minds want to know?! Allow me to redirect your request.", args.Mobile.Name));
                        args.Mobile.LaunchBrowser("http://www.uoexpanse.com/connect.php");
                        break;
                    }

                #endregion Edited By: A.A.R

                #region tosagreement
                //A Players Inability To Understand The Consequences For Breaking The Servers Rules Makes Them Stupid.

                case ("tosagreement"):
                    {
                        Say(String.Format("It's not really a TOS Agreement, they are more like guidelines. Here, let me open the website.", args.Mobile.Name));
                        args.Mobile.LaunchBrowser("http://www.uoexpanse.com/play.php");
                        break;
                    }

                #endregion Edited By: A.A.R

                #region serverrules
                //Some People Want To Play By The Rules And/Or Learn To Get Around Them! Either Way, This Should Help.

                case ("serverrules"):
                    {
                        Say(String.Format("A good law abiding adventurer! 'Tis a pleasure to meet you.", args.Mobile.Name));
                        args.Mobile.LaunchBrowser("http://www.uoexpanse.com/play.php");
                        break;
                    }

                #endregion Edited By: A.A.R

                #region meetourstaff
                //Every Server Website Should Have A Page Devoted To Staff Introductions!

                case ("meetourstaff"):
                    {
                        Say(String.Format("Raist is the Owner of The Expanse. Mogster is an Administrator and Player Liason.", args.Mobile.Name));
                        break;
                    }

                #endregion Edited By: A.A.R

                #region showcredits
                //Someone Aside From You Has Also Worked Their Ass Of To Make Your Server What It Is, Give Them Credit!

                case ("showcredits"):
                    {
                        Say(String.Format("Sure! Please be patient while I redirect you to our website. Thank you.", args.Mobile.Name));
                        args.Mobile.LaunchBrowser("http://www.uoexpanse.com/credits.php");
                        break;
                    }

                #endregion Edited By: A.A.R

                //Player Reporting

                #region livesupport
                //Allows Players To Page Real Staff Members Online

                case ("livestaff"):
                    {
                        args.Mobile.SendGump(new Server.Engines.Help.HelpGump(args.Mobile));
                        break;
                    }

                #endregion Edited By: A.A.R

                //MMORPG Help Desk

                #region skillcap
                //Helps Players Figure Out What The Skill Cap Is

                case ("skillcap"):
                    {
                        Say(String.Format("Our server currently has no skillcap. There are no plans to change this. Having no skillcap allows players to fully experience all that UO has to offer.", args.Mobile.Name));
                        break;
                    }

                #endregion Edited By: A.A.R

                #region skills
                //Sometimes Players Need Information On Skills And Skill Gain

                case ("skills"):
                    {
                        Say(String.Format("My apologies {0}, I am forbidden to assist thee with skill training. However, If you tell me the name of the skill you're having issues with, then I'll be more than happy to redirect you to an online skill guide. We suggest using UOGuides.", args.Mobile.Name));
                        break;
                    }

                #region Player Skill Guide References

                case ("alchemy"):
                    {
                        Say(String.Format("Thank you {0}, allow me to redirect you to an online playguide.", args.Mobile.Name));
                        args.Mobile.LaunchBrowser("http://www.uoguide.com/Alchemy");
                        break;
                    }

                case ("anatomy"):
                    {
                        Say(String.Format("Thank you {0}, allow me to redirect you to an online playguide.", args.Mobile.Name));
                        args.Mobile.LaunchBrowser("http://www.uoguide.com/Anatomy");
                        break;
                    }

                case ("animal lore"):
                    {
                        Say(String.Format("Thank you {0}, allow me to redirect you to an online playguide.", args.Mobile.Name));
                        args.Mobile.LaunchBrowser("http://www.uoguide.com/Animal_Lore");
                        break;
                    }

                case ("animal taming"):
                    {
                        Say(String.Format("Thank you {0}, allow me to redirect you to an online playguide.", args.Mobile.Name));
                        args.Mobile.LaunchBrowser("http://www.uoguide.com/Animal_Taming");
                        break;
                    }

                case ("archery"):
                    {
                        Say(String.Format("Thank you {0}, allow me to redirect you to an online playguide.", args.Mobile.Name));
                        args.Mobile.LaunchBrowser("http://www.uoguide.com/Archery");
                        break;
                    }

                case ("armslore"):
                    {
                        Say(String.Format("Thank you {0}, allow me to redirect you to an online playguide.", args.Mobile.Name));
                        args.Mobile.LaunchBrowser("http://www.uoguide.com/Arms_Lore");
                        break;
                    }

                case ("begging"):
                    {
                        Say(String.Format("Thank you {0}, allow me to redirect you to an online playguide.", args.Mobile.Name));
                        args.Mobile.LaunchBrowser("http://www.uoguide.com/Begging");
                        break;
                    }

                case ("blacksmithy"):
                    {
                        Say(String.Format("Thank you {0}, allow me to redirect you to an online playguide.", args.Mobile.Name));
                        args.Mobile.LaunchBrowser("http://www.uoguide.com/Blacksmithy");
                        break;
                    }

                case ("bowcraft"):
                    {
                        Say(String.Format("Thank you {0}, allow me to redirect you to an online playguide.", args.Mobile.Name));
                        args.Mobile.LaunchBrowser("http://www.uoguide.com/Bowcraft_%26_Fletching");
                        break;
                    }

                case ("fletching"):
                    {
                        Say(String.Format("Thank you {0}, allow me to redirect you to an online playguide.", args.Mobile.Name));
                        args.Mobile.LaunchBrowser("http://www.uoguide.com/Bowcraft_%26_Fletching");
                        break;
                    }

                case ("bushido"):
                    {
                        Say(String.Format("Thank you {0}, allow me to redirect you to an online playguide.", args.Mobile.Name));
                        args.Mobile.LaunchBrowser("http://www.uoguide.com/Bushido");
                        break;
                    }

                case ("camping"):
                    {
                        Say(String.Format("Thank you {0}, allow me to redirect you to an online playguide.", args.Mobile.Name));
                        args.Mobile.LaunchBrowser("http://www.uoguide.com/Camping");
                        break;
                    }

                case ("carpentry"):
                    {
                        Say(String.Format("Thank you {0}, allow me to redirect you to an online playguide.", args.Mobile.Name));
                        args.Mobile.LaunchBrowser("http://www.uoguide.com/Carpentry");
                        break;
                    }

                case ("cartography"):
                    {
                        Say(String.Format("Thank you {0}, allow me to redirect you to an online playguide.", args.Mobile.Name));
                        args.Mobile.LaunchBrowser("http://www.uoguide.com/Cartography");
                        break;
                    }

                case ("chivalry"):
                    {
                        Say(String.Format("Thank you {0}, allow me to redirect you to an online playguide.", args.Mobile.Name));
                        args.Mobile.LaunchBrowser("http://www.uoguide.com/Chivalry");
                        break;
                    }

                case ("cooking"):
                    {
                        Say(String.Format("Thank you {0}, allow me to redirect you to an online playguide.", args.Mobile.Name));
                        args.Mobile.LaunchBrowser("http://www.uoguide.com/Cooking");
                        break;
                    }

                case ("detect hidden"):
                    {
                        Say(String.Format("Thank you {0}, allow me to redirect you to an online playguide.", args.Mobile.Name));
                        args.Mobile.LaunchBrowser("http://www.uoguide.com/Detecting_Hidden");
                        break;
                    }

                case ("discordance"):
                    {
                        Say(String.Format("Thank you {0}, allow me to redirect you to an online playguide.", args.Mobile.Name));
                        args.Mobile.LaunchBrowser("http://www.uoguide.com/Discordance");
                        break;
                    }

                case ("evaluating intelligence"):
                    {
                        Say(String.Format("Thank you {0}, allow me to redirect you to an online playguide.", args.Mobile.Name));
                        args.Mobile.LaunchBrowser("http://www.uoguide.com/Evaluating_Intelligence");
                        break;
                    }

                case ("fencing"):
                    {
                        Say(String.Format("Thank you {0}, allow me to redirect you to an online playguide.", args.Mobile.Name));
                        args.Mobile.LaunchBrowser("http://www.uoguide.com/Fencing");
                        break;
                    }

                case ("fishing"):
                    {
                        Say(String.Format("Thank you {0}, allow me to redirect you to an online playguide.", args.Mobile.Name));
                        args.Mobile.LaunchBrowser("http://www.uoguide.com/Fishing");
                        break;
                    }

                case ("focus"):
                    {
                        Say(String.Format("Thank you {0}, allow me to redirect you to an online playguide.", args.Mobile.Name));
                        args.Mobile.LaunchBrowser("http://www.uoguide.com/Focus");
                        break;
                    }

                case ("forensic evaluation"):
                    {
                        Say(String.Format("Thank you {0}, allow me to redirect you to an online playguide.", args.Mobile.Name));
                        args.Mobile.LaunchBrowser("http://www.uoguide.com/Forensic_Evaluation");
                        break;
                    }

                case ("healing"):
                    {
                        Say(String.Format("Thank you {0}, allow me to redirect you to an online playguide.", args.Mobile.Name));
                        args.Mobile.LaunchBrowser("http://www.uoguide.com/Healing");
                        break;
                    }

                case ("herding"):
                    {
                        Say(String.Format("Thank you {0}, allow me to redirect you to an online playguide.", args.Mobile.Name));
                        args.Mobile.LaunchBrowser("http://www.uoguide.com/Herding");
                        break;
                    }

                case ("hiding"):
                    {
                        Say(String.Format("Thank you {0}, allow me to redirect you to an online playguide.", args.Mobile.Name));
                        args.Mobile.LaunchBrowser("http://www.uoguide.com/Hiding");
                        break;
                    }

                case ("imbuing"):
                    {
                        Say(String.Format("Sorry, we don't have imbuing on this shard at this time. Here's information about an alternative for it, Spellcrafting.", args.Mobile.Name));
						args.Mobile.LaunchBrowser("http://www.uoexpanse.com/spell.php");
                        break;
                    }

                case ("inscription"):
                    {
                        Say(String.Format("Thank you {0}, allow me to redirect you to an online playguide.", args.Mobile.Name));
                        args.Mobile.LaunchBrowser("http://www.uoguide.com/Inscription");
                        break;
                    }

                case ("item identification"):
                    {
                        Say(String.Format("Thank you {0}, allow me to redirect you to an online playguide.", args.Mobile.Name));
                        args.Mobile.LaunchBrowser("http://www.uoguide.com/Item_Identification");
                        break;
                    }

                case ("lockpicking"):
                    {
                        Say(String.Format("Thank you {0}, allow me to redirect you to an online playguide.", args.Mobile.Name));
                        args.Mobile.LaunchBrowser("http://www.uoguide.com/Lockpicking");
                        break;
                    }

                case ("lumberjacking"):
                    {
                        Say(String.Format("Thank you {0}, allow me to redirect you to an online playguide.", args.Mobile.Name));
                        args.Mobile.LaunchBrowser("http://www.uoguide.com/Lumberjacking");
                        break;
                    }

                case ("macefighting"):
                    {
                        Say(String.Format("Thank you {0}, allow me to redirect you to an online playguide.", args.Mobile.Name));
                        args.Mobile.LaunchBrowser("http://www.uoguide.com/Mace_Fighting");
                        break;
                    }

                case ("magery"):
                    {
                        Say(String.Format("Thank you {0}, allow me to redirect you to an online playguide.", args.Mobile.Name));
                        args.Mobile.LaunchBrowser("http://www.uoguide.com/Magery");
                        break;
                    }

                case ("meditation"):
                    {
                        Say(String.Format("Thank you {0}, allow me to redirect you to an online playguide.", args.Mobile.Name));
                        args.Mobile.LaunchBrowser("http://www.uoguide.com/Meditation");
                        break;
                    }

                case ("mining"):
                    {
                        Say(String.Format("Thank you {0}, allow me to redirect you to an online playguide.", args.Mobile.Name));
                        args.Mobile.LaunchBrowser("http://www.uoguide.com/Mining");
                        break;
                    }
					
				case ("firerock"):
                    {
                        Say(String.Format("Fire rock crafting is a new crafting feature here, allow me to redirect you to an online playguide.", args.Mobile.Name));
                        args.Mobile.LaunchBrowser("http://www.uoexpanse.com/firerock.php");
                        break;
                    }

                case ("musicianship"):
                    {
                        Say(String.Format("Thank you {0}, allow me to redirect you to an online playguide.", args.Mobile.Name));
                        args.Mobile.LaunchBrowser("http://www.uoguide.com/Musicianship");
                        break;
                    }

                case ("mysticism"):
                    {
                        Say(String.Format("Thank you {0}, allow me to redirect you to an online playguide.", args.Mobile.Name));
                        args.Mobile.LaunchBrowser("http://www.uoguide.com/Mysticism");
                        break;
                    }

                case ("necromancy"):
                    {
                        Say(String.Format("Thank you {0}, allow me to redirect you to an online playguide.", args.Mobile.Name));
                        args.Mobile.LaunchBrowser("http://www.uoguide.com/Necromancy");
                        break;
                    }

                case ("ninjitsu"):
                    {
                        Say(String.Format("Thank you {0}, allow me to redirect you to an online playguide.", args.Mobile.Name));
                        args.Mobile.LaunchBrowser("http://www.uoguide.com/Ninjitsu");
                        break;
                    }

                case ("parrying"):
                    {
                        Say(String.Format("Thank you {0}, allow me to redirect you to an online playguide.", args.Mobile.Name));
                        args.Mobile.LaunchBrowser("http://www.uoguide.com/Parrying");
                        break;
                    }

                case ("peacemaking"):
                    {
                        Say(String.Format("Thank you {0}, allow me to redirect you to an online playguide.", args.Mobile.Name));
                        args.Mobile.LaunchBrowser("http://www.uoguide.com/Peacemaking");
                        break;
                    }

                case ("poisoning"):
                    {
                        Say(String.Format("Thank you {0}, allow me to redirect you to an online playguide.", args.Mobile.Name));
                        args.Mobile.LaunchBrowser("http://www.uoguide.com/Poisoning");
                        break;
                    }

                case ("provocation"):
                    {
                        Say(String.Format("Thank you {0}, allow me to redirect you to an online playguide.", args.Mobile.Name));
                        args.Mobile.LaunchBrowser("http://www.uoguide.com/Provocation");
                        break;
                    }

                case ("removetrap"):
                    {
                        Say(String.Format("Thank you {0}, allow me to redirect you to an online playguide.", args.Mobile.Name));
                        args.Mobile.LaunchBrowser("http://www.uoguide.com/Remove_Trap");
                        break;
                    }

                case ("resisting spells"):
                    {
                        Say(String.Format("Thank you {0}, allow me to redirect you to an online playguide.", args.Mobile.Name));
                        args.Mobile.LaunchBrowser("http://www.uoguide.com/Resisting_Spells");
                        break;
                    }

                case ("snooping"):
                    {
                        Say(String.Format("Thank you {0}, allow me to redirect you to an online playguide.", args.Mobile.Name));
                        args.Mobile.LaunchBrowser("http://www.uoguide.com/Snooping");
                        break;
                    }

                case ("spellweaving"):
                    {
                        Say(String.Format("Thank you {0}, allow me to redirect you to an online playguide.", args.Mobile.Name));
                        args.Mobile.LaunchBrowser("http://www.uoguide.com/Spellweaving");
                        break;
                    }

                case ("spiritspeak"):
                    {
                        Say(String.Format("Thank you {0}, allow me to redirect you to an online playguide.", args.Mobile.Name));
                        args.Mobile.LaunchBrowser("http://www.uoguide.com/Spirit_Speak");
                        break;
                    }

                case ("stealing"):
                    {
                        Say(String.Format("Thank you {0}, allow me to redirect you to an online playguide.", args.Mobile.Name));
                        args.Mobile.LaunchBrowser("http://www.uoguide.com/Stealing");
                        break;
                    }

                case ("stealth"):
                    {
                        Say(String.Format("Thank you {0}, allow me to redirect you to an online playguide.", args.Mobile.Name));
                        args.Mobile.LaunchBrowser("http://www.uoguide.com/Stealth");
                        break;
                    }

                case ("swordsmanship"):
                    {
                        Say(String.Format("Thank you {0}, allow me to redirect you to an online playguide.", args.Mobile.Name));
                        args.Mobile.LaunchBrowser("http://www.uoguide.com/Swordsmanship");
                        break;
                    }

                case ("tactics"):
                    {
                        Say(String.Format("Thank you {0}, allow me to redirect you to an online playguide.", args.Mobile.Name));
                        args.Mobile.LaunchBrowser("http://www.uoguide.com/Tactics");
                        break;
                    }

                case ("tailoring"):
                    {
                        Say(String.Format("Thank you {0}, allow me to redirect you to an online playguide.", args.Mobile.Name));
                        args.Mobile.LaunchBrowser("http://www.uoguide.com/Tailoring");
                        break;
                    }

                case ("taste identification"):
                    {
                        Say(String.Format("Thank you {0}, allow me to redirect you to an online playguide.", args.Mobile.Name));
                        args.Mobile.LaunchBrowser("http://www.uoguide.com/Taste_Identification");
                        break;
                    }

                case ("throwing"):
                    {
                        Say(String.Format("Thank you {0}, allow me to redirect you to an online playguide.", args.Mobile.Name));
                        args.Mobile.LaunchBrowser("http://www.uoguide.com/Throwing");
                        break;
                    }

                case ("tinkering"):
                    {
                        Say(String.Format("Thank you {0}, allow me to redirect you to an online playguide.", args.Mobile.Name));
                        args.Mobile.LaunchBrowser("http://www.uoguide.com/Tinkering");
                        break;
                    }

                case ("tracking"):
                    {
                        Say(String.Format("Thank you {0}, allow me to redirect you to an online playguide.", args.Mobile.Name));
                        args.Mobile.LaunchBrowser("http://www.uoguide.com/Tracking");
                        break;
                    }

                case ("veterinary"):
                    {
                        Say(String.Format("Thank you {0}, allow me to redirect you to an online playguide.", args.Mobile.Name));
                        args.Mobile.LaunchBrowser("http://www.uoguide.com/Veterinary");
                        break;
                    }

                case ("wrestling"):
                    {
                        Say(String.Format("Thank you {0}, allow me to redirect you to an online playguide.", args.Mobile.Name));
                        args.Mobile.LaunchBrowser("http://www.uoguide.com/Wrestling");
                        break;
                    }

                #endregion Edited By: A.A.R

                #endregion Edited By: A.A.R

                #region statcap
                //Helps Players Figure Out What The Stat Cap Is

                case ("statcap"):
                    {
                        Say(String.Format("Our server currently has a maximum statcap of 375. There are no plans to increase this number. A stat cap of 375 allows players to evenly set their strength, dexterity, and intelligence to 125, or any combination that adds to 375.", args.Mobile.Name));
                        break;
                    }

                #endregion Edited By: A.A.R

                #region playerguide
                //Directs Players To Your Servers Online PlayGuide For Assistance

                case ("playerguide"):
                    {
                        Say(String.Format("'Tis good to keep up-to-date on things {0}. Allow me to redirect you to our forums for the latest information.", args.Mobile.Name));
                        args.Mobile.LaunchBrowser("http://www.uoexpanse.com/forum/");
                        break;
                    }

                #endregion Edited By: A.A.R

                #region bestiary

                case ("bestiary"):
                    {
                        Say(String.Format("We've got a lot of creatures on the server {0}. We have put together a Bestiary for all your hunting needs.", args.Mobile.Name));
                        args.Mobile.LaunchBrowser("http://www.uoexpanse.com/bestiary.php");
                        break;
                    }

                #endregion Edited By: A.A.R

                //Player Involvment

                #region events
                //Some People Are Interested About How Your Server Came To Be. Tell Them!

                case ("events"):
                    {
                        Say(String.Format("Ahhh! Inquisitive minds want to know?! Allow me to redirect you to our forums.", args.Mobile.Name));
                        args.Mobile.LaunchBrowser("http://www.uoexpanse.com/forum/");
                        break;
                    }

                #endregion Edited By: A.A.R

                #region eventrequest
                //Some People Are Interested About How Your Server Came To Be. Tell Them!

                case ("eventrequest"):
                    {
                        args.Mobile.SendGump(new PR_EventRequest());
                        Say(String.Format("Event requests are always welcome! {0}. Thank you for being proactive. If you have any other event ideas, please let us know!", args.Mobile.Name));
                        break;
                    }

                #endregion Edited By: A.A.R

                #region hiring
                //A Toggle! Just Uncomment Which Response You'd Like To Give Your Players

                #region Yes, Staff Is Hiring
                //An Easy Way Of Directing Your Players To Your Staff Member Application

                case ("hiring"):
                    {
                        Say(String.Format("Absolutely {0}! Staffing positions are now available. Please fill out the Staff Application under the Guidelines and Info tab located on the website. Raist will contact you after reviewing your application.", args.Mobile.Name));
                        args.Mobile.LaunchBrowser("http://www.uoexpanse.com/staffapp.php");
                        break;
                    }

                #endregion Edited By: A.A.R

                #region No, We're Not Hiring
                //A Nice Way Of Saying, "Dude! Stop Asking If You Can Be A Staff Member!!"
				/*
                case ("hiring"):
                    {
                        Say(String.Format("Our apologies {0}, We're just not hiring at this time. We'll post available positions on our website, as soon as they open up, please be patient and check back soon. Thank you.", args.Mobile.Name));
                        break;
                    }
				*/
                #endregion Edited By: A.A.R

                #endregion Edited By: A.A.R

                #region suggestion
                //Everyone Has Their Own Ideas On How They Think Things Should Be

                case ("suggestion"):
                    {
                        args.Mobile.SendGump(new SuggestionBox());
                        Say(String.Format("We would really appreciate your input {0}, since you think you know better than we do. Thank you for making us feel small and insignificant. If you have any other suggestions, please let us know!", args.Mobile.Name));
                        break;
                    }

                #endregion Edited By: A.A.R

                #region donations
                //Makes It Easier For Players To Donate Funds To Your Server

                case ("gamedonate"):
                    {
                        Say(String.Format("The Expanse offers a chance for up and coming veteran players to give back to the community through an in-game donation system. Allow me to open the webpage with more information.", args.Mobile.Name));
                        args.Mobile.LaunchBrowser("http://www.uoexpanse.com/donate.php");
                        break;
                    }
					
                case ("serverdonate"):
                    {
                        Say(String.Format("If you would like to make a monetary donation to support the upkeep and maintenance of the server, I'll open the webpage where you can make such a donation. 100% of the purchases through the online store also go to support the server as well. ", args.Mobile.Name));
                        args.Mobile.LaunchBrowser("http://www.uoexpanse.com/");
                        break;
                    }

                #endregion Edited By: A.A.R
				
                #region website
                //Provides easy access to the website homepage

                case ("website"):
                    {
                        Say(String.Format("A lot can be learned by reading the information found on the website, including information about custom content.", args.Mobile.Name));
                        args.Mobile.LaunchBrowser("http://www.uoexpanse.com");
                        break;
                    }

                #endregion Edited By: A.A.R
				
                #region requestgm
                //Allows the players to request an automated GM for further help

                case ("requestgm"):
                    {
                        Say(String.Format("If you would like to escalate your problem to a GM I shall summon one for you.", args.Mobile.Name));
						#region Shadows Edge - Automated Server Staff

							PlayerMobile pm = (PlayerMobile)args.Mobile;
			
							if ( AutoStaffTeam.Enabled )  //If automated staff team enabled, begin the new gump process.
							{                
									if (args.Mobile.HasGump(typeof(GM_StaffKeywords)))
									{
										args.Mobile.CloseGump(typeof(GM_StaffKeywords));
										args.Mobile.SendMessage ( "Please close the keywords window before pressing the help button." );
										return;
									}
						
									GameMaster_GM sm = new GameMaster_GM();
						
						
									switch (Utility.Random(2))
									{
										case 0: sm.MoveToWorld(new Point3D(args.Mobile.X + 2, args.Mobile.Y, args.Mobile.Z), args.Mobile.Map); break;
										case 1: sm.MoveToWorld(new Point3D(args.Mobile.X, args.Mobile.Y + 2, args.Mobile.Z), args.Mobile.Map); break;
									}
								

								/*
								args.Mobile.SendMessage("You may only page a staff member once every thirty minutes. If you need assistance now, please visit our website at: www.uoexpanse.com/forum");
								return;
								*/
							}
						

							#endregion Edited By: A.A.R
							break;
                    }

                #endregion Edited By: A.A.R

            }
        }

        #endregion Edited By: A.A.R

        #region Click The NPC To Open Up A Gump

        public override void GetContextMenuEntries(Mobile from, List<ContextMenuEntry> list)
        {
            base.GetContextMenuEntries(from, list);
            list.Add(new Counselor_Entry(from, this));
        }

        #endregion Edited By: A.A.R

        public override bool ClickTitle { get { return false; } }
        public override bool IsActiveVendor { get { return false; } }

        public override bool OnDragDrop(Mobile from, Item dropped)
        {
            Mobile m = from;
            PlayerMobile mobile = m as PlayerMobile;

            from.SendMessage("I appreciate the offer, but I do this job out of the love for the game. Or at least that's what I'm supposed to tell you in case Raist is watching us.");
            return false;
        }

        public Counselor_PR(Serial serial): base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
            #region NPC Auto-Deletion Timer

            this.Delete();

            #endregion Edited By: A.A.R
        }

        #region Click The NPC To Open Up A Gump

        public class Counselor_Entry : ContextMenuEntry
        {
            private Mobile m_Mobile;
            private Mobile m_Giver;

            public Counselor_Entry(Mobile from, Mobile giver): base(6146, 3)
            {
                m_Mobile = from;
                m_Giver = giver;
            }

            public override void OnClick()
            {
                if (!(m_Mobile is PlayerMobile))
                    return;

                PlayerMobile mobile = (PlayerMobile)m_Mobile;
                {
                    if (!mobile.HasGump(typeof(Counselor_Talk)))
                    {
                        mobile.SendGump(new Counselor_Talk());
                    }
                }
            }

        #endregion Edited By: A.A.R

        }
    }
}

