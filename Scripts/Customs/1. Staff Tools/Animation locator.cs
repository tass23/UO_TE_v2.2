//**************************************//
// feel free to do what ever you choose	//
// with this script. Please just make 	//
// sure to leave this header in place.	//
//					//
// Made by: Father Time			//
// e-mail: FatherTime@TheHyperCube.net	//
// Server: The HyperCube 2		//
// ICQ: 146563794			//
// MSN: vermillion2083@hotmail.com	//
//**************************************//	


// 	Animation breakdown:
//	~~~~~~~~~~~~~~~~~~~~
// 	#1 mobile.Animate( #2 int, #3 int, #4 int, #5 bool, #5 bool, #6 int );
//	
// 	#1) What is to be animated.
//	#2) The number of the animation
//	#3) How many times to repeat the same animation
//	#4) this will increase the animation the specified amount, so if you chose 3, it would do animation 1, then 2, then 3.
//	#5) Unknown to me. Please e-mail/ICQ me if you know this items function.
//	#6) Unknown to me. Please e-mail/ICQ me if you know this items function.


using System;
using Server.Network;
using Server.Prompts;
using Server.Items;
using Server;
using Server.Gumps;
using Server.Mobiles;
using Server.Targeting;

namespace Server.Items
{
	public class AnimationLocator : Item
	{
		private int m_AnimationNumber;
		public bool m_Active = false;
		private Mobile m_TargetMobile;

		[CommandProperty( AccessLevel.GameMaster )]
		public int AnimationNumber
		{
			get{ return m_AnimationNumber; }
			set{ m_AnimationNumber = value; }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public bool Active{ get{ return m_Active; } set{ m_Active = value; InvalidateProperties(); } }

		[CommandProperty( AccessLevel.GameMaster )]
		public Mobile TargetMobile{ get{ return m_TargetMobile; } set{ m_TargetMobile = value; InvalidateProperties(); } }

		[Constructable]
		public AnimationLocator() : base( 0x14F0 )
		{
			base.Weight = 5.0;
			base.Name = "Animation locator";
			LootType = LootType.Blessed;
		}

		public AnimationLocator( Serial serial ) : base( serial )
		{
		}
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
			writer.WriteEncodedInt( (int) m_AnimationNumber );
			writer.Write( m_Active );
			writer.Write( m_TargetMobile );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			switch ( version )
			{
				case 0:
				{
					m_AnimationNumber = reader.ReadEncodedInt();
					m_Active = reader.ReadBool();
					m_TargetMobile = reader.ReadMobile() as Mobile;
					break;
				}
			}
		}

		public override void OnDoubleClick( Mobile from )
		{
			if( from.AccessLevel < AccessLevel.GameMaster )
			{
				from.SendMessage("Shame on you how did you get this!");
				Delete();
				return;
			}

			else
			{
				m_TargetMobile = from;
				from.CloseGump( typeof( AnimationLocatorGump ) );
				from.SendGump( new AnimationLocatorGump( this ) );
			}
		}

		public void StartLoop( Mobile from )
		{
			m_AnimationNumber += 1;
			from.CloseGump( typeof( AnimationLocatorGump ) );
			from.SendGump( new AnimationLocatorGump( this ) ); 
			m_TargetMobile.Emote("† animation " + m_AnimationNumber.ToString() + " †" );
			m_TargetMobile.Animate( m_AnimationNumber, 0, 1, true, false, 0 );

			Timer.DelayCall( TimeSpan.FromSeconds( 3.0 ), new TimerStateCallback( BeginAnimation_Callback ), from );
		}
		
		private void BeginAnimation_Callback( object state )
		{
			EndAnimation( (Mobile) state );
		}

		public virtual void EndAnimation( Mobile m )
		{
	
			if( m_Active == true )
				StartLoop( m );

			else
			return;
		}

		public void Use( Mobile from )
		{		
			from.Target = new ToAnimateTarget( from, this );
		}

        	public class ToAnimateTarget : Target
        	{
			AnimationLocator m_AnimationLocator;

            		public ToAnimateTarget( Mobile from, AnimationLocator AnimationLocator ) : base( 10, false, TargetFlags.None )
            		{
				m_AnimationLocator = AnimationLocator;
            		}

            		protected override void OnTarget( Mobile from /* WHO TARGETED */, object target /* WHAT'S BEEN TARGETED */ )
			{
				if ( target is Mobile )
				{
					Mobile mob = (Mobile)target;
					m_AnimationLocator.TargetMobile = mob;
				}
		
				else
				from.SendMessage("Only mobiles may be animated.");
			}
		}					
	}

	public class AnimationLocatorGump : Gump
	{
		AnimationLocator m_AnimationLocator;

		public AnimationLocatorGump( AnimationLocator AnimationLocator ) : base( 0, 0 )
		{
			m_AnimationLocator = AnimationLocator;

			this.Closable=true;
			this.Disposable=true;
			this.Dragable=true;
			this.Resizable=false;
			this.AddPage(0);
			this.AddImage(245, 240, 3508);
			this.AddImageTiled(51, 71, 198, 171, 3504);
			this.AddImage(33, 50, 3500);
			this.AddImageTiled(51, 50, 197, 24, 3501);
			this.AddImageTiled(44, 240, 202, 24, 3507);
			this.AddImageTiled(33, 75, 25, 165, 3503);
			this.AddImageTiled(245, 72, 25, 170, 3505);
			this.AddImage(165, 91, 3502);
			this.AddImage(165, 133, 3508);
			this.AddImage(116, 133, 3506);
			this.AddImage(116, 91, 3500);
			this.AddImageTiled(141, 91, 30, 24, 3501);
			this.AddImageTiled(141, 133, 24, 24, 3507);
			this.AddImageTiled(116, 115, 25, 20, 3503);
			this.AddImage(245, 50, 3502);
			this.AddImageTiled(165, 116, 25, 17, 3505);
			this.AddImageTiled(63, 191, 171, 5, 3007);
			this.AddImageTiled(69, 82, 171, 5, 3007);
			this.AddImage(33, 239, 3506);

			this.AddLabel(100, 63, 0, @"Animation locator");

			this.AddTextEntry(133, 115, 42, 20, 0, 1, m_AnimationLocator.AnimationNumber.ToString() );
			this.AddLabel(97, 151, 0, @"Current animation");

			this.AddLabel(76, 173, 0, @"Target:");
			this.AddLabel(137, 173, 0, m_AnimationLocator.TargetMobile.RawName.ToString() );

			this.AddLabel(90, 228, 0, @"Decrease");
			this.AddLabel(90, 205, 0, @"Increase");
			this.AddLabel(193, 205, 0, @"Cycle");
			this.AddLabel(193, 228, 0, @"Single");

			//INCREASE
			this.AddButton(72, 208, 2103, 2104, 1, GumpButtonType.Reply, 0);

			//DECREASE
			this.AddButton(72, 234, 2103, 2104, 2, GumpButtonType.Reply, 0);

			//CYCLE
			this.AddButton(175, 208, 2103, 2104, 3, GumpButtonType.Reply, 0);

			//SINGLE
			this.AddButton(175, 234, 2103, 2104, 4, GumpButtonType.Reply, 0);

			//CHOSE TARGER
			this.AddButton(58, 178, 2103, 2104, 5, GumpButtonType.Reply, 0);

		}

		public override void OnResponse( NetState sender, RelayInfo info )
		{
        		Mobile from = sender.Mobile;
			PlayerMobile pm = (PlayerMobile)from;

			if( m_AnimationLocator.Deleted )
			return;
			
			if ( info.ButtonID == 1 )
			{
				m_AnimationLocator.AnimationNumber += 1;
				pm.CloseGump( typeof( AnimationLocatorGump ) );
				pm.SendGump( new AnimationLocatorGump( m_AnimationLocator ) );
			}

			if ( info.ButtonID == 2 )
			{
				m_AnimationLocator.AnimationNumber -= 1;
				pm.CloseGump( typeof( AnimationLocatorGump ) );
				pm.SendGump( new AnimationLocatorGump( m_AnimationLocator ) );
			}

			if ( info.ButtonID == 3 )
			{
				TextRelay tr_AnimationNumber = info.GetTextEntry( 1 );
				if(tr_AnimationNumber != null)
				{
					int i_MaxAmount = 0;
					try
					{
						i_MaxAmount = Convert.ToInt32(tr_AnimationNumber.Text,10);
					} 
					catch
					{
						pm.SendMessage(1161, "Please only use numbers.");
					}

					m_AnimationLocator.AnimationNumber = i_MaxAmount;
				}


				if( m_AnimationLocator.Active == false )
				{
					m_AnimationLocator.TargetMobile.Emote("† Animation locator started †");
					m_AnimationLocator.StartLoop( pm );
					m_AnimationLocator.Active = true;
				}

				else
				{
					m_AnimationLocator.TargetMobile.Emote("† Animation locator stopped †");
					m_AnimationLocator.Active = false;
				}

				pm.CloseGump( typeof( AnimationLocatorGump ) );
				pm.SendGump( new AnimationLocatorGump( m_AnimationLocator ) );
			}

			if ( info.ButtonID == 4 )
			{
				TextRelay tr_AnimationNumber = info.GetTextEntry( 1 );
				if(tr_AnimationNumber != null)
				{
					int i_MaxAmount = 0;
					try
					{
						i_MaxAmount = Convert.ToInt32(tr_AnimationNumber.Text,10);
					} 
					catch
					{
						pm.SendMessage(1161, "Please make sure to only use numbers.");
					}

					m_AnimationLocator.AnimationNumber = i_MaxAmount;
				}

				m_AnimationLocator.TargetMobile.Emote("† animation " + m_AnimationLocator.AnimationNumber.ToString() + " †" );
				m_AnimationLocator.TargetMobile.Animate( m_AnimationLocator.AnimationNumber, 0, 1, true, false, 0 );

				pm.CloseGump( typeof( AnimationLocatorGump ) );
				pm.SendGump( new AnimationLocatorGump( m_AnimationLocator ) );
			}
			
			if ( info.ButtonID == 5 )
			{
				pm.SendMessage("Please target the mobile you wish to animate.");
				m_AnimationLocator.Use( pm );

				pm.CloseGump( typeof( AnimationLocatorGump ) );
				pm.SendGump( new AnimationLocatorGump( m_AnimationLocator ) );
			}
		}
	}
}