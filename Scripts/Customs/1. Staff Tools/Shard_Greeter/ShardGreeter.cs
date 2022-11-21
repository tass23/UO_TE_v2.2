using System;
using System.Collections;
using Server.ContextMenus;
using System.Collections.Generic;
using Server.Misc;
using Server.Network;
using Server;
using Server.Items;
using Server.Gumps;

namespace Server.Mobiles
{
    public class ShardGreeter : BaseGuildmaster
	    {
	
		private List<SBInfo> m_SBInfos = new List<SBInfo>();
		protected override List<SBInfo> SBInfos{ get { return m_SBInfos; } }

		public override NpcGuild NpcGuild{ get{ return NpcGuild.BardsGuild; } }
		
		public override bool ClickTitle{ get{ return false; } }
		
		public override bool IsActiveVendor{ get{ return false; } }

		private static bool m_Talked; 
		
		      string[] kfcsay = new string[]  
				{ 
		         "Hello, welcome to The Expanse! Talk to me to find out the latest shard news.",   
				};
		
		
		[Constructable]
		public ShardGreeter() : base( "bard" )
		{
		Title = "the shard greeter";
        CantWalk = true;
		}
		
		public virtual void Init()
		{
           if (this.Female = Utility.RandomBool())
            {
				Female = true;
                Body = 0x191;
                Name = NameList.RandomName("female");
			}
            else
            {
				Female = false;
                Body = 0x190;
                Name = NameList.RandomName("male");
			}
			
			SpeechHue = Utility.RandomDyedHue();
            Hue = Utility.RandomSkinHue();
			Utility.AssignRandomHair( this );
		}

		public override void OnMovement( Mobile m, Point3D oldLocation ) 
		{                                                    
			    if( m_Talked == false ) 
			    { 
			            if ( m.InRange( this, 4 ) ) 
			            {                
			               m_Talked = true; 
			               SayRandom( kfcsay, this ); 
			               this.Move( GetDirectionTo( m.Location ) );

                           chatTimer t = new chatTimer(); 
			               t.Start(); 
						} 
			    } 
		} 
			
		private class chatTimer : Timer 
		    { 
				public chatTimer() : base( TimeSpan.FromSeconds( 20 ) ) 
					{ 
			        Priority = TimerPriority.OneSecond; 
			        } 
			
			    protected override void OnTick() 
			        { 
			        m_Talked = false; 
			        } 
			} 
			
		private static void SayRandom( string[] say, Mobile m ) 
			{ 
		    m.Say( say[Utility.Random( say.Length )] ); 
			}
        public ShardGreeter(Serial serial)
            : base(serial)
			{
			}
	
	
		public override void GetContextMenuEntries( Mobile from, List<ContextMenuEntry> list ) 
	        { 
	                base.GetContextMenuEntries( from, list ); 
        	        list.Add( new ShardGreeterEntry( from, this ) ); 
	        } 

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); 
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		
		}
		
		public class ShardGreeterEntry : ContextMenuEntry
		{
			private Mobile m_Mobile;
			private Mobile m_Giver;
			
			public ShardGreeterEntry( Mobile from, Mobile giver ) : base( 6146, 3 )
			{
				m_Mobile = from;
				m_Giver = giver;
			}

			public override void OnClick()
			{
			    if( !( m_Mobile is PlayerMobile ) )
				return;
				
			PlayerMobile mobile = (PlayerMobile) m_Mobile;

				{
					if ( ! mobile.HasGump( typeof( ShardGreeterGump ) ) )
				{
                    mobile.SendGump(new ShardGreeterGump(mobile));
				}
				         
				}       
				     
            }
        }
	 }  
}	
					 				       
					 				         