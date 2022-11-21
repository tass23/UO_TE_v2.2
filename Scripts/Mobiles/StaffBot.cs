// Completely Automated Staff Team
//By Tresdni - www.uo-d.com (UO Dissension)
using System;
using System.Collections;
using Server.Items;
using Server.Targeting;
using Server.ContextMenus;
using Server.Gumps;
using Server.Misc;
using Server.Network;
using Server.Spells;
using Server.Accounting;
using System.Collections.Generic;



namespace Server.Mobiles
{
	[CorpseName( "a staff member's corpse" )]
    public class StaffBot : BaseCreature
	{
        private DateTime HelpBegin = DateTime.Now;
		public TimeSpan HelpTime{ get{ return TimeSpan.FromMinutes( 5 ); } }  //5 minutes help time - You can set this to whatever you want.  They delete themselves.
		private bool m_Talked;
		private bool m_Gated;

        string[] staffbotgreet = new string[] // Greeting the staff bot gives, just once :)
		    { 				
		        "Hello, how may I assist you?"       
			};

		[Constructable]
		public StaffBot() : base( AIType.AI_Animal, FightMode.None, 10, 1, 0.2, 0.4 )
		{					
			Title = "[GM]";
			CantWalk = true;
			Hue = Utility.RandomSkinHue();
			SpeechHue = Utility.RandomDyedHue();
			NameHue = 11;
			Blessed = true;
			
			if( this.Female = Utility.RandomBool() )
			{
				this.Body = 0x191;
				this.Name = NameList.RandomName( "female" );
			}
			else
			{
				this.Body = 0x190;
				this.Name = NameList.RandomName( "male" );
			}

            Skills.Cap = 9000;

            SetSkill(SkillName.Fencing, 100.0);
            SetSkill(SkillName.Macing, 100.0);
            SetSkill(SkillName.Swords, 100.0);
            SetSkill(SkillName.Tactics, 100.0);
            SetSkill(SkillName.Parry, 100.0);
            SetSkill(SkillName.Archery, 100.0);
            SetSkill(SkillName.Chivalry, 100.0);
            SetSkill(SkillName.Anatomy, 100.0);
            SetSkill(SkillName.Healing, 100.0);
			
			GMRobe rob = new GMRobe();
			rob.AccessLevel = AccessLevel.Player;
			rob.Hue = 1157;
			rob.LootType = LootType.Blessed;
			AddItem(rob);
			
			

		}

        public override void OnMovement(Mobile m, Point3D oldLocation)
        {
            if ( ( ( HelpBegin + HelpTime) <= DateTime.Now ) )
			{
				this.Delete();
			}
        
			if (m_Talked == false)
            {
                if (m.InRange(this, 4))
                {
                    m_Talked = true;
                    SayRandom(staffbotgreet, this);
					m.SendMessage ("Please use the key words list to get the help that you need.  This staff team member will disappear after five minutes.");
                }
            }
		}
		
        private static void SayRandom(string[] say, Mobile m)
        {
            m.Say(say[Utility.Random(say.Length)]);
        }
        public StaffBot(Serial serial) : base(serial)
		{
		}
        
        public override bool HandlesOnSpeech( Mobile from )
        {
            if ( from.InRange( this.Location, 5 ) )
            return true;
            
            return base.HandlesOnSpeech( from );
        }

        public override void OnSpeech(SpeechEventArgs args)  //Let's handle the interactions between the player and staff member - based on key words.
        {

			string said = args.Speech.ToLower();
            Mobile from = args.Mobile;
            switch ( said )
            {
                case ( "pull" ):
				{
					goto case "stuck";
				}
				case ( "stuck" ):
                {
					if ( m_Gated == false )
					{
						m_Gated = true;
						StuckGate sg = new StuckGate();
						sg.Location = args.Mobile.Location;
						sg.Map = args.Mobile.Map;
						Say( String.Format( "I had a feeling you couldn't make it out of here on your own {0}.", args.Mobile.Name ) );
						break;
					}
					Say( String.Format( "I've already sent you a gate out of here {0}.  My mana won't regenerate for another hour.", args.Mobile.Name ) );
					break;
                }
				case ( "donate" ):
				{
					goto case "donation";
				}
				case ( "donation" ):  //Questions about donations?
				{
					Say( String.Format( "If it's donation information your seeking {0}, you should go to www.yoursite.com and click donate.  It has all the info you need.", args.Mobile.Name ) );
					break;
				}
				case ( "spellweaving" ):  //Popular spellweaving question.
				{
					Say( String.Format( "You must complete a quest in Heartwood before you can use Spellweaving {0}.  Make sure to clear all other quests before you do so, else it may break on you.", args.Mobile.Name ) );
					args.Mobile.LaunchBrowser( "http://www.uoguide.com/Spellweaving" );
					break;
				} 
				case ( "gauntlet" ):
				{
					args.Mobile.LaunchBrowser( "http://www.uoguide.com/Doom_Gauntlet#Gauntlet" );
					break;
				}
				case ( "treasuresoftokuno" ):
				{
					goto case "tot";
				}
				case ( "tot" ):
				{
					args.Mobile.LaunchBrowser( "http://www.uoguide.com/Treasures_of_Tokuno" );
					break;
				}
				case ( "vetrewards" ):
				{
					Say( String.Format( "On Dissension, you get a veteran reward choice every 30 days {0}", args.Mobile.Name ) );
					args.Mobile.LaunchBrowser( "http://www.uoguide.com/Veteran_Rewards" );
					break;
				}
				case ( "factionkick" ):
				{
					Say( String.Format( "It takes 3 days to leave a faction {0}.  You can start the process by visiting your factions stone.", args.Mobile.Name ) );					
					break;
				}
				case ( "realperson" ):
				{
					Say( String.Format( "So you don't think I'm doing a good job {0}?  Here, send the real guys and gals an email and let them know.", args.Mobile.Name ) );
					args.Mobile.LaunchBrowser( "mailto:your@supportaddress.com" );	
					break;
				}	
				case ( "suggestion" ):
				{
					args.Mobile.SendGump (new Suggestion());
					Say( String.Format( "We would really appreciate your input {0}.", args.Mobile.Name ) );
					break;
				}
				case ( "hiring" ):
				{
					Say( String.Format( "We are not currently hiring, {0}.  Thank you for asking.", args.Mobile.Name ) );  //Or maybe you are?
					break;
				}
				case ( "harassment" ):
				{
					Say( String.Format( "Please remember to include screenshots in this email {0}.", args.Mobile.Name ) );  //change your harassment email.
					args.Mobile.LaunchBrowser( "mailto:your@harassmentemail.com" );
					break;
				}
				case ( "owner" ):
				{
					Say( String.Format( "The one whom has given me the opportunity to help you is named YourShardOwnersName.  I'm sure you've seen him on our forums {0}!", args.Mobile.Name ) );
					break;
				}
				case ( "nark" ):
				{
					Say( String.Format( "Did you find someone breaking the rules {0}?  Please send us an email!", args.Mobile.Name ) );  //change this email to whatever you want.
					args.Mobile.LaunchBrowser( "mailto:your@supportemail.com" );
					break;
				}
				case ( "report" ):
				{
					goto case "nark";
				}	
            }
        }
		
		public override bool OnDragDrop( Mobile from, Item dropped )
		{
			Mobile m = from;
			PlayerMobile mobile = m as PlayerMobile;
			
			from.SendMessage ("I appreciate the offer, but I do this job out of the love for the game.");  //As they should!  Free shard means free!
			return false;
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