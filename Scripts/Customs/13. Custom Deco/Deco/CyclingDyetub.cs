//Created by Ashlar, beloved of Morrigan.
//Gump by GumpStudio 1.7

using System;
using Server;
using Server.Gumps;
using Server.Network;
using Server.Items;
using Server.Targeting;

namespace Server.Ashlar
{
    public class CyclingDyetub : Item
    {
        private InternalTimer m_Timer;

        public double m_HueDelay;
        public int m_HueStart;
        public int m_HueStop;
        public bool m_IsCycling;
        public bool m_HasCost;

        [CommandProperty( AccessLevel.Player )]
        public double HueDelay { get { return m_HueDelay; } set { m_HueDelay = value; } }

        [CommandProperty( AccessLevel.Player )]
        public bool HasCost { get { return m_HasCost; } set { m_HasCost = value; } }

        [Constructable]
        public CyclingDyetub() : base( 0xFAB )
        {
            Weight = 1.0;

            m_HueDelay = 0.3;
            m_HueStart = 0;
            m_HueStop = 900;
            m_IsCycling = false;
            m_HasCost = true;
			Name = "Cycling Dye Tub-2000gp Per Hue Change";
        }

        public override void OnDoubleClick( Mobile from )
        {
            if ( !from.InRange( this.GetWorldLocation(), 2 ) && from.AccessLevel < AccessLevel.GameMaster )
            {
                from.SendMessage( "The dyetub is too far away..." );
            }
            else
            {
                from.CloseGump( typeof( CyclingDyetubGump ) );
                from.SendGump( new CyclingDyetubGump( from, this ) );
            }
        }

        #region Serialize
        public CyclingDyetub( Serial serial ) : base( serial )
        {
        }

        public override void Serialize( GenericWriter writer )
        {
            base.Serialize( writer );
            writer.WriteEncodedInt( ( int )0 ); // version

            writer.Write( ( double )m_HueDelay );
            writer.Write( ( int )m_HueStart );
            writer.Write( ( int )m_HueStop );
            writer.Write( ( bool )m_IsCycling );
            writer.Write( ( bool )m_HasCost );

        }
        public override void Deserialize( GenericReader reader )
        {
            base.Deserialize( reader );
            int version = reader.ReadEncodedInt();

            m_HueDelay = reader.ReadDouble();
            m_HueStart = reader.ReadInt();
            m_HueStop = reader.ReadInt();
            m_IsCycling = reader.ReadBool();
            m_HasCost = reader.ReadBool();

            if ( m_IsCycling )
            {
                DoHueTimer();
            }
        }
        #endregion

        public void DoHueTimer()
        {
            TimeSpan next = TimeSpan.FromSeconds( this.m_HueDelay );
            m_Timer = new InternalTimer( this, next );
            m_Timer.Start();
        }

        #region InternalTimer
        private class InternalTimer : Timer
        {
            private CyclingDyetub m_item;

            private static TimeSpan TwoMinutes = TimeSpan.FromMinutes( 2.0 );
            private static TimeSpan ThirtySeconds = TimeSpan.FromSeconds( 30.0 );
            private static TimeSpan TenSeconds = TimeSpan.FromSeconds( 10.0 );
            private static TimeSpan OneSecond = TimeSpan.FromSeconds( 1.0 );

            public InternalTimer( CyclingDyetub item, TimeSpan next ) : base( next )
            {
                m_item = item;

                //This section of assigning timer priority came from the RunUO Distro script: 
                if ( next >= TwoMinutes )
                    Priority = TimerPriority.OneMinute;
                else if ( next >= ThirtySeconds )
                    Priority = TimerPriority.FiveSeconds;
                else if ( next >= TenSeconds )
                    Priority = TimerPriority.OneSecond;
                else if ( next >= OneSecond )
                    Priority = TimerPriority.TwoFiftyMS;
                else
                    Priority = TimerPriority.TwentyFiveMS;
            }
            protected override void OnTick()
            {
                if ( m_item.m_IsCycling )
                {
                    if ( m_item.Hue < m_item.m_HueStart )
                    {
                        m_item.Hue = m_item.m_HueStart;
                        m_item.DoHueTimer();
                        m_item.Name = "Dyetub Hue # " + m_item.Hue;
                    }
                    else if ( m_item.Hue < m_item.m_HueStop )
                    {
                        m_item.Hue++;
                        m_item.DoHueTimer();
                        m_item.Name = "Dyetub Hue # " + m_item.Hue;
                    }
                    else
                    {
                        if ( m_item.m_IsCycling )
                        {
                            m_item.m_IsCycling = false;;
                        }

                        if ( m_item.Hue != 0 )
                        {
                            m_item.Hue = 0;
                            m_item.Name = "Dyetub Hue # " + m_item.Hue;
                        }

                        if ( m_item.ItemID != 0xFAB )
                            m_item.ItemID = 0xFAB;
                    }
                }
            }
        }
        #endregion
    }


	public class CyclingDyetubGump : Gump
	{
        CyclingDyetub m_CD;

        public static int NegProtection( string numberstring )
        {
            int ns = Utility.ToInt32( numberstring );

            if ( ns < 0 )
                return 0;
            else
                return ns;
        }
        public static int ButtonID( CyclingDyetub cd )
        {
            if ( cd.m_IsCycling )
                return 2116;
            else
                return 2113;
        }
        public static bool CheckRange( Mobile from, CyclingDyetub cd )
        {
            if ( !from.InRange( cd.GetWorldLocation(), 2 ) )
            {
                from.SendMessage( "The dyetub is too far away..." );
                DoExit( from, cd );
                return false;
            }
            else
                return true;
        }

		public CyclingDyetubGump( Mobile from, CyclingDyetub cd ) : base( 0, 0 )
		{
            m_CD = cd;

			this.Closable=false;
			this.Disposable=true;
			this.Dragable=true;
			this.Resizable=false;

			this.AddPage(0);

			this.AddBackground( 71, 78, 180, 92, 5054 );
            this.AddBackground( 71, 162, 144, 84, 5054 );
            this.AddBackground( 139, 238, 76, 35, 5054 );

            this.AddLabel( 80, 90, 2416, @"Curent Hue:" );
			this.AddButton(226, 90, 250, 251, (int)Buttons.CurHuePlus, GumpButtonType.Reply, 0);
			this.AddButton(166, 90, 252, 253, (int)Buttons.CurHueMinus, GumpButtonType.Reply, 0);
            this.AddTextEntry( 187, 90, 31, 20, 2416, 1, @"" + m_CD.Hue );

            this.AddButton( 77, 116, 2124, 239, ( int )Buttons.ApplyHue, GumpButtonType.Reply, 0 );
            this.AddLabel( 135, 116, 2416, @"to Target" );

            this.AddLabel( 80, 138, 2416, @"Preview Item" );
            this.AddButton( 162, 138, 247, 247, ( int )Buttons.Preview, GumpButtonType.Reply, 0 );

            this.AddButton( 77, 173, ButtonID( m_CD ), ButtonID( m_CD ), ( int )Buttons.StopStartHueCycle, GumpButtonType.Reply, 0 );
            this.AddLabel( 137, 174, 2416, @"Cycle Hues" );

            this.AddLabel( 80, 195, 2416, @"Start Hue" );
            this.AddTextEntry( 155, 195, 33, 20, 2416, 2, @"" + m_CD.m_HueStart );//Start hue
            this.AddLabel( 80, 215, 2416, @"Stop Hue" );
            this.AddTextEntry( 155, 215, 33, 20, 2416, 3, @"" + m_CD.m_HueStop );//End hue

            this.AddButton( 73, 237, 1028, 1026, ( int )Buttons.Exit, GumpButtonType.Reply, 0 );

            this.AddLabel( 145, 245, 2416, @"Get Hue" );
            this.AddButton( 191, 249, 5300, 5300, ( int )Buttons.GetHue, GumpButtonType.Reply, 0 );

		}
		
		public enum Buttons
		{
            Exit,
			CurHuePlus,
			CurHueMinus,
			ApplyHue,
			Preview,
			StopStartHueCycle,
            GetHue,
		}

        public override void OnResponse( NetState state, RelayInfo info )
        {
            if ( m_CD == null )
                return;

            Mobile from = state.Mobile;

            #region TextRelays
            TextRelay entry1 = info.GetTextEntry( 1 );
            int CurHue = NegProtection( entry1 == null ? "" : entry1.Text.Trim() );
            if ( CurHue > 2999 )
                from.SendMessage( "There are no hues above 2999." );
            else if ( !( CurHue == m_CD.Hue ) && !m_CD.m_IsCycling )
                m_CD.Hue = CurHue;

            TextRelay entry2 = info.GetTextEntry( 2 );
            int HueStart = NegProtection( entry2 == null ? "" : entry2.Text.Trim() );
            if ( !( HueStart == m_CD.m_HueStart ) )
                m_CD.m_HueStart = HueStart;

            TextRelay entry3 = info.GetTextEntry( 3 );
            int HueStop = NegProtection( entry3 == null ? "" : entry3.Text.Trim() );
            if ( HueStop > 2999 )
                from.SendMessage( "There are no hues above 2999." );
            else if ( !( HueStop == m_CD.m_HueStop ) )
                m_CD.m_HueStop = HueStop;
            #endregion

            #region switch ( info.ButtonID )
            switch ( info.ButtonID )
            {
                case ( int )Buttons.Exit: DoExit( from, m_CD ); break;
                case ( int )Buttons.CurHuePlus: DoCurHuePlus( from, m_CD ); break;
                case ( int )Buttons.CurHueMinus: DoCurHueMinus( from, m_CD ); break;
                case ( int )Buttons.ApplyHue: AlertPlayerOfCost( from ); from.Target = new DoApplyHue( m_CD ); break;
                case ( int )Buttons.Preview: from.Target = new DoPreview( m_CD ); break;
                case ( int )Buttons.StopStartHueCycle: DoStopStartHueCycle( from, m_CD ); break;
                case ( int )Buttons.GetHue: from.Target = new DoGetHue( m_CD ); break;
            }
            #endregion
        }

        public static void DoExit( Mobile from, CyclingDyetub cd )
        {
            if ( cd.m_IsCycling )
            {
                cd.m_IsCycling = false;
                from.EndAction( typeof( CyclingDyetub ) );
            }

            if ( cd.Hue != 0 )
            {
                cd.Hue = 0;
                cd.Name = "Dyetub Hue # " + cd.Hue;
            }

            if ( cd.ItemID != 0xFAB )
                cd.ItemID = 0xFAB;

            from.CloseGump( typeof( CyclingDyetubGump ) );
        }
        public void DoCurHuePlus( Mobile from, CyclingDyetub cd )
        {
            if ( !CheckRange( from, cd ) )
                return;

            if ( cd.Hue > 2998 )
            {
                from.SendMessage( "There are no hues above 2999." );
            }
            else if ( !cd.m_IsCycling )
            {
                cd.Hue++;
            }
            else
            {
                cd.m_IsCycling = false;
                from.EndAction( typeof( CyclingDyetub ) );

                cd.Hue++;
            }

            cd.Name = "Dyetub Hue # " + cd.Hue;
            from.CloseGump( typeof( CyclingDyetubGump ) );
            from.SendGump( new CyclingDyetubGump( from, cd ) );
        }
        public void DoCurHueMinus( Mobile from, CyclingDyetub cd )
        {
            if ( !CheckRange( from, m_CD ) )
                return;

            if ( !cd.m_IsCycling )
            {
                if ( cd.Hue >= 1 )
                    cd.Hue--;
            }
            else
            {
                cd.m_IsCycling = false;
                from.EndAction( typeof( CyclingDyetub ) );

                if ( cd.Hue >= 1 )
                    cd.Hue--;
            }

            cd.Name = "Dyetub Hue # " + cd.Hue;
            from.CloseGump( typeof( CyclingDyetubGump ) );
            from.SendGump( new CyclingDyetubGump( from, cd ) );
        }
        public class DoApplyHue : Target
        {
            Item i = null;
            CyclingDyetub m_CD;

            public DoApplyHue( CyclingDyetub cd ) : base( 4, false, TargetFlags.None )
            {
                m_CD = cd;
            }
            protected override void OnTarget( Mobile from, object targeted )
            {
                if ( !CheckRange( from, m_CD ) )
                    return;

                if ( targeted is Item )
                {
                    i = ( Item )targeted;
                    if ( from.AccessLevel > AccessLevel.Player )
                    {
                        i.Hue = m_CD.Hue;
                    }
                    else if ( i.RootParent == from )
                    {
                        if ( TakePayment( from, m_CD ) )
                            i.Hue = m_CD.Hue;
                    }
                    else
                        from.SendMessage( "That is not in your posession." );
                }
                else
                    from.SendMessage( "You can only change the hue of items." );

                from.CloseGump( typeof( CyclingDyetubGump ) );
                from.SendGump( new CyclingDyetubGump( from, m_CD ) );
            }
        }
        public class DoPreview : Target
        {
            Item i = null;
            CyclingDyetub m_CD;

            public DoPreview( CyclingDyetub cd ) : base( 4, false, TargetFlags.None )
            {
                m_CD = cd;
            }
            protected override void OnTarget( Mobile from, object targeted )
            {
                if ( !CheckRange( from, m_CD ) )
                    return;

                if ( targeted is Item )
                {
                    i = ( Item )targeted;
                    if ( i == m_CD )
                    {
                        m_CD.ItemID = 0xFAB;
                    }
                    else if ( from.AccessLevel > AccessLevel.Player )
                    {
                        m_CD.ItemID = i.ItemID;
                    }
                    else if ( i.Parent == from && from.AccessLevel < AccessLevel.GameMaster )
                    {
                        m_CD.ItemID = i.ItemID;
                    }
                    else
                        from.SendMessage( "That is not in your posession." );
                }
                else
                    from.SendMessage( "You can only preview items." );

                from.CloseGump( typeof( CyclingDyetubGump ) );
                from.SendGump( new CyclingDyetubGump( from, m_CD ) );
            }
        }
        public void DoStopStartHueCycle( Mobile from, CyclingDyetub cd )
        {
            if ( !CheckRange( from, cd ) )
                return;

            if ( !cd.m_IsCycling )
            {
                cd.m_IsCycling = true;
                cd.DoHueTimer();
            }
            else
            {
                cd.m_IsCycling = false;
                from.EndAction( typeof( CyclingDyetub ) );
            }

            from.CloseGump( typeof( CyclingDyetubGump ) );
            from.SendGump( new CyclingDyetubGump( from, m_CD ) );
        }
        public class DoGetHue : Target
        {
            Item i = null;
            Mobile mob = null;
            CyclingDyetub m_CD;

            public DoGetHue( CyclingDyetub cd ) : base( 4, false, TargetFlags.None )
            {
                m_CD = cd;
            }
            protected override void OnTarget( Mobile from, object targeted )
            {
                if ( targeted is Item )
                {
                    i = ( Item )targeted;
                    if ( i.Hue != 0 )
                    {
                        m_CD.Hue = i.Hue;
                        from.SendMessage( "Hue # "+ i.Hue +" assigned to dyetub." );
                    }
                    else
                        from.SendMessage( "That is just a natural zero hue, sorry!" );
                }
                else if ( targeted is Mobile )
                {
                    mob = ( Mobile )targeted;
                    if ( mob.Hue != 0 )
                    {
                        m_CD.Hue = mob.Hue;
                        from.SendMessage( "Hue # " + mob.Hue + " assigned to dyetub." );
                    }
                    else
                        from.SendMessage( "That is just a natural zero hue, sorry!" );
                }
                else
                    from.SendMessage( "Items or Mobiles only please." );

                from.CloseGump( typeof( CyclingDyetubGump ) );
                from.SendGump( new CyclingDyetubGump( from, m_CD ) );
            }
        }


        #region HasCost functions
        public static Type PayType()
        {
            return typeof( Gold );
        }
        public static string PayTypeName()
        {
            return "gold.";
        }
        public static Int32 HueCost()
        {
            return 2000;
        }
        public static void AlertPlayerOfCost( Mobile from )
        {
            if ( from.AccessLevel < AccessLevel.GameMaster )
            {
                from.SendAsciiMessage( 240, "Hey! " + from.Name + "! Use of this dyetub will cost you " + HueCost() + " " + PayTypeName() + "." );
                from.SendAsciiMessage( 269, "Hit escape to exit this transaction." );
            }
        }
        public static bool TakePayment( Mobile from, CyclingDyetub cd )
        {
            if ( cd.m_HasCost )
            {
                Container bp = from.Backpack;
                if ( from.AccessLevel < AccessLevel.GameMaster && bp != null )
                {
                    if ( bp.ConsumeTotal( PayType(), HueCost() ) )
                    {
                        from.PlaySound( 0x32 );
                        return true;
                    }
                    else
                    {
                        from.SendMessage( "Begging thy pardon, but thou can not afford that." );
                        return false;
                    }
                }
                else
                    return true;
            }
            else
                return true;
        }
        #endregion
    }
	
	public class CyclingDyeTubDeed : Item 
	{
		[Constructable]
		public CyclingDyeTubDeed() : this( 1 )
		{
			ItemID = 5360;
			Movable = true;
			Hue = 1089;
			Name = "Cycling Dye Tub Deed";
					
		}
		
		 public override void OnDoubleClick( Mobile from )
      	{
       		from.AddToBackpack( new CyclingDyetub() );
			from.AddToBackpack( new CyclingFreetub() );
       		this.Delete();
        }

		[Constructable]
		public CyclingDyeTubDeed( int amount ) 
        {
		}		

		public CyclingDyeTubDeed( Serial serial ) : base( serial ) 
		{ 
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