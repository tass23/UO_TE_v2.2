using System;
using Server;
using Server.Mobiles;

namespace Server.Items
{
    [Flipable(0x0EE3, 0x0EE4, 0x0EE5, 0x0EE6)]
    public class NavreyParalyzingWeb : Item
    {
        private static TimeSpan duration = TimeSpan.FromSeconds( 60.0 );

        private Timer m_Timer;
        private DateTime m_End;

        public override bool BlocksFit{ get{ return true;}}

        public NavreyParalyzingWeb() : base(0x0EE3 + Utility.Random(4) )
        {
            Visible = true;
            Movable = false;

            m_Timer = new InternalTimer( this, duration );
            m_Timer.Start();

            m_End = DateTime.Now + duration;
        }

        public override void OnDelete()
        {
            base.OnDelete();

            if ( m_Timer != null )
                m_Timer.Stop();

            // remove paralyze from all chars in this location
            foreach ( Mobile m in Map.GetMobilesInRange( this.Location, 0 ) )
            {
                if ( null != m )
                    m.Paralyzed = false;
            }
        }

        public NavreyParalyzingWeb( Serial serial ) : base( serial )
        {
        }

        public override void Serialize( GenericWriter writer )
        {
            base.Serialize( writer );

            writer.Write( (int) 0 ); // version

            writer.WriteDeltaTime( m_End );
        }

        public override void Deserialize( GenericReader reader )
        {
            base.Deserialize( reader );

            int version = reader.ReadInt();

            switch ( version )
            {
            case 0:
                {
                    m_End = reader.ReadDeltaTime();

                    m_Timer = new InternalTimer( this, m_End - DateTime.Now );
                    m_Timer.Start();

                    break;
                }
            }
        }

        public override bool OnMoveOver( Mobile m )
        {
            if ( m is Navrey )
                return true;

            if ( AccessLevel.Player == m.AccessLevel )
            {
                m.Paralyze( duration );
    
                m.PlaySound( 0x204 );
                m.FixedEffect( 0x376A, 10, 16 );
            }

            return true;
        }

        private class InternalTimer : Timer
        {
            private Item m_Item;

            public InternalTimer( Item item, TimeSpan duration ) : base( duration )
            {
                Priority = TimerPriority.OneSecond;
                m_Item = item;
            }

            protected override void OnTick()
            {
                m_Item.Delete();
            }
        }
    }
}