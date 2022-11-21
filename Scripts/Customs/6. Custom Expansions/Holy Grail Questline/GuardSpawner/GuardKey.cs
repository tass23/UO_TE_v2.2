// GuardKey (to be used with GuardSpawner)
// a RunUO ver 1.0/2.0 Script
// Written by David 
// last edited 4/28/06

/* Version 1.1 */

using System;
using System.Collections;
using Server.Network;
using Server.Targeting;
using Server.Prompts;

namespace Server.Items
{
    public class GuardKey : Item
    {
        private string i_Description;
        private uint i_KeyVal;
        private int i_Uses;
        private int i_Max;
        private TimeSpan i_Delay;

        [CommandProperty( AccessLevel.GameMaster )]
        public string Description
        {
            get { return i_Description; }
            set
            {
                i_Description = value;
                InvalidateProperties();
            }
        }

        [CommandProperty( AccessLevel.GameMaster )]
        public uint KeyValue
        {
            get { return i_KeyVal; }
            set
            {
                i_KeyVal = value;
                InvalidateProperties();
            }
        }

        [CommandProperty( AccessLevel.GameMaster )]
        public int Uses
        {
            get { return i_Uses; }
        }

        public int codeUses
        {
            get { return i_Uses; }
            set { i_Uses = value; }
        }

        [CommandProperty( AccessLevel.GameMaster )]
        public int MaxUses
        {
            get { return i_Max; }
            set { i_Max = value; }
        }

        [CommandProperty( AccessLevel.GameMaster )]
        public TimeSpan RelockDelay
        {
            get { return i_Delay; }
            set { i_Delay = value; }
        }

        [Constructable] // for GM testing, will need to be linked to door
        public GuardKey() : this( 1, "A Guard's Key", 5, TimeSpan.FromSeconds( 2 ) ) { }

        public GuardKey( uint KeyVal, string Desc, int MaxUsage, TimeSpan DelayTime )
            : base( 0x1013 )
        {
            Weight = 1.0;
            LootType = LootType.Regular;

            i_KeyVal = KeyVal;
            i_Description = Desc;
            i_Max = MaxUsage;
            i_Delay = DelayTime;
        }

        public override void OnDoubleClick( Mobile from )
        {
            from.SendLocalizedMessage( 501662 ); // What shall I use this key on?
            from.Target = new UnlockTarget( this );
        }

        public override void GetProperties( ObjectPropertyList list )
        {
            //base.GetProperties( list );
            list.Add( i_Description );
            list.Add( 1060584, ( i_Max - i_Uses ).ToString() ); // uses remaining: ~1_val~
        }

        public override void OnSingleClick( Mobile from )
        {
            LabelTo( from, i_Description );
        }

        public GuardKey( Serial serial )
            : base( serial )
        {
        }

        public override void Serialize( GenericWriter writer )
        {
            base.Serialize( writer );
            writer.Write( (int)0 ); // version

            writer.Write( i_Description );
            writer.Write( i_KeyVal );
            writer.Write( i_Uses );
            writer.Write( i_Max );
            writer.Write( (string)i_Delay.ToString() );
        }

        public override void Deserialize( GenericReader reader )
        {
            base.Deserialize( reader );

            int version = reader.ReadInt();

            switch( version )
            {
                case 0:
                    {
                        i_Description = reader.ReadString();
                        i_KeyVal = reader.ReadUInt();
                        i_Uses = reader.ReadInt();
                        i_Max = reader.ReadInt();
                        i_Delay = TimeSpan.Parse( reader.ReadString() );
                        break;
                    }
            }
        }

        #region Target
        private class UnlockTarget : Target
        {
            private GuardKey m_Key;

            public UnlockTarget( GuardKey key )
                : base( 3, false, TargetFlags.None )
            {
                m_Key = key;
                CheckLOS = false;
            }

            protected override void OnTarget( Mobile from, object targeted )
            {
                int number;

                if( targeted is ILockable )
                {
                    number = -1;

                    ILockable o = (ILockable)targeted;

                    if( o.KeyValue == m_Key.KeyValue )
                    {
                        if( o is BaseDoor && !( (BaseDoor)o ).UseLocks() )
                        {
                            number = 501668; // This key doesn't seem to unlock that.
                        }
                        else
                        {
                            if( o.Locked )
                            {
                                o.Locked = false;

                                if( targeted is Item )
                                {
                                    Item item = (Item)targeted;
                                    item.SendLocalizedMessageTo( from, 1048001 ); // or 1048000
                                }

                                // counter
                                int remain = m_Key.MaxUses - ++m_Key.codeUses;
                                switch( remain )
                                {
                                    default:
                                        { from.SendMessage( "You force the key to turn the lock" ); break; }
                                    case 2:
                                        { from.SendMessage( "The key bends as it opens the lock" ); break; }
                                    case 1:
                                        { from.SendMessage( "The lock opens but the key is cracked" ); break; }
                                    case 0:
                                        {
                                            from.SendMessage( "The lock opens but the key breaks in your hand" );
                                            if( m_Key != null )
                                                m_Key.Delete();
                                            break;
                                        }
                                }
                                m_Key.InvalidateProperties();
                                // lock door
                                new RelockTimer( from, o, m_Key.RelockDelay ).Start();
                            }
                        }
                    }
                    else
                    {
                        number = 501668; // This key doesn't seem to unlock that.
                    }
                }
                else
                {
                    number = 501666; // You can't unlock that!
                }

                if( number != -1 )
                {
                    from.SendLocalizedMessage( number );
                }
            }
        }
        #endregion

        #region Timer
        public class RelockTimer : Timer
        {
            private ILockable o_obj;
            private Mobile m_from;

            public RelockTimer( Mobile from, ILockable o, TimeSpan delay )
                : base( delay )
            {
                Priority = TimerPriority.TwoFiftyMS;
                m_from = from;
                o_obj = o;
            }

            protected override void OnTick()
            {
                if( o_obj != null )
                {
                    if( o_obj is BaseDoor && ( (BaseDoor)o_obj ).UseLocks() )
                    {
                        ( (BaseDoor)o_obj ).Locked = true;
                        m_from.SendMessage( "The lock clicks shut" );
                    }
                    else if( o_obj is LockableContainer )
                        ( (LockableContainer)o_obj ).Locked = true;
                }
            }
        }
        #endregion
    }
}
