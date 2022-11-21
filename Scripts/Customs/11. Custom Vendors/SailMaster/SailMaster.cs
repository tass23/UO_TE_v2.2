using System; 
using System.Collections;
using System.Collections.Generic;
using Server;
using Server.Items;
using Server.ContextMenus;
using Server.Misc;
using Server.Network;
using Server.Gumps;

namespace Server.Mobiles 
{ 
	public class SailMaster : BaseVendor 
	{
		private List<SBInfo> m_SBInfos = new List<SBInfo>();
		protected override List<SBInfo> SBInfos{ get { return m_SBInfos; } }
		private static bool m_Talked;  

		[Constructable]
		public SailMaster() : base( "the SailMaster" ) 
		{ 
			CantWalk = true;
		} 

		public override void InitSBInfo() 
		{ 
			m_SBInfos.Add( new SBSailMaster() ); 
		}

		public override void InitOutfit()
		{
			base.InitOutfit();
		}

		public SailMaster( Serial serial ) : base( serial ) 
		{ 
		} 

		public override bool HandlesOnSpeech( Mobile from )
		{
			if ( from.InRange( this.Location, 12 ) )
				return true;

			return base.HandlesOnSpeech( from );
		}

		public override void OnMovement(Mobile m, Point3D oldLocation) 
		{   
		  if( m_Talked == false ) 
			{ 
				if ( m.InRange( this, 1 ) ) 
				{	
					m_Talked = true;
					this.Say("Hail and well met.");
					this.Say("When thee are ready to Sail, just say 'Sail' and we will set sail...");
					this.Move( GetDirectionTo( m.Location ) ); 
					SpamTimer t = new SpamTimer(); 
					t.Start();
				}
			}
		}
 
		private class SpamTimer : Timer   
		{
			public SpamTimer() : base( TimeSpan.FromSeconds( 15 ) )  
			{ 
			  Priority = TimerPriority.OneSecond;
			} 
			
			protected override void OnTick()
			{
					m_Talked = false;
			}
		}

		public override void OnSpeech(SpeechEventArgs e )
		{
			if ( !e.Handled && e.Mobile.InRange( this.Location, 12 ) )
			{
				if (e.Speech == "sail")
				{
					e.Mobile.SendGump(new Gumps.SailGump(e.Mobile, this.Location));
					this.Move( GetDirectionTo( e.Mobile.Location ) );
				}
				
			}

			base.OnSpeech( e );
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