using System;
using Server.Gumps;
using Server.Mobiles;
using Server.Targeting;

namespace Server.Items
{
	public class CompletedClockworkAssembly : Item
	{
        public override int LabelNumber { get { return 1112879; } } // completed clockwork assembly

		public CompletedClockworkAssembly() : base( 0x1EA8 )
		{
            Weight = 1.0;
        }

        public override void GetProperties( ObjectPropertyList list )
        {
            base.GetProperties( list );
            list.Add( 1072351 ); // Quest Item
        }

        public CompletedClockworkAssembly(Serial serial) : base(serial)
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

	public class ClockworkMechanism : Item
	{
        private static int[] labeltypes = { 1112859, 1112861, 1029734, 1075914, 1075915, 1075917, 1029746 }; 

        private int m_Type;
        private int m_Lifespan;
        private Timer m_Timer;

        int m_Remaining;
        SutekResourceType m_Resource;

		[Constructable]
		public ClockworkMechanism() : base( 0x1EA8 )
		{
			Weight = 1.0;
            m_Lifespan = 3600;
            Movable = true;

            m_Type = labeltypes[Utility.Random(labeltypes.Length)];
            
            StartTimer();
		}

        public override void GetProperties( ObjectPropertyList list )
        {
            base.GetProperties( list );

            list.Add( 1072351 ); // Quest Item

            if ( m_Lifespan > 0 )
                list.Add( 1072517, m_Lifespan.ToString() ); // Lifespan: ~1_val~ seconds
        }

		public override void AddNameProperty( ObjectPropertyList list )
		{
            list.Add( 1112858, String.Concat( "#", m_Type.ToString() ) ); // ~1_TYPE~ clockwork mechanism
		}

		public override void OnDoubleClick( Mobile from )
		{
            if ( null == from || 0 == m_Lifespan )
                return;

			if ( !IsChildOf( from.Backpack ) )
			{
				from.SendLocalizedMessage( 1042001 ); // That must be in your pack for you to use it.
			}
			else
			{
                if ( from.HasGump( typeof(ClockworkStartGump) ) )
                    from.CloseGump( typeof(ClockworkStartGump) );

                from.SendGump( new ClockworkStartGump( this ) );
			}
		}

        public void BeginMechanismAssembly( Mobile from )
        {
			from.Target = new InternalTarget(from, this);
        }

        public override void OnDelete()
        {
            if ( Deleted )
                return;

              if ( null != m_Timer && m_Timer.Running )
              {
                  StopTimer();
                  m_Timer = null;
              }

            base.OnDelete();
        }

        public void StartTimer()
        {
            if ( null == m_Timer )
                m_Timer = new InternalTimer(this);

            m_Timer.Start();
            Movable = false;
        }

        public void StopTimer()
        {
            m_Timer.Stop();
            m_Lifespan = 0;
        }

        public void OnTick()
        {
            m_Lifespan -= 10;
            InvalidateProperties();
            if ( m_Lifespan <= 0 )
            {
                StopTimer();
                if ( RootParentEntity is Mobile )
                    ((Mobile)RootParentEntity).SendLocalizedMessage( 1112822 ); // You fail to find the next ingredient in time. Your clockwork assembly crumbles.
                Delete();
            }
        }

        public ClockworkMechanism(Serial serial) : base(serial)
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 1 ); // version

            writer.Write( m_Type );
            writer.Write( m_Lifespan );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

            switch (version)
            {
            case 1:
                m_Type = reader.ReadInt();
                m_Lifespan = reader.ReadInt();
                break;
            }

            StartTimer();
		}

		private class InternalTarget : Target
		{
            private ClockworkMechanism m_Item;
            private int m_Remaining;
            private SutekResourceType m_Type;
            private DateTime m_EndTime;
            private bool m_Finished;
            private double m_Delay;

			public InternalTarget( Mobile from, ClockworkMechanism item ) : this( from, item, SutekQuestResource.GetRandomResource(), Utility.RandomMinMax(10, 20), 10.0, DateTime.Now + TimeSpan.FromSeconds(10.0) )
			{
			}

			public InternalTarget( Mobile from, ClockworkMechanism item, SutekResourceType type, int remaining, double delay, DateTime endtime ) : base( 2, true, TargetFlags.None )
			{
                m_Item = item;
                m_Type = type;
                m_Remaining = remaining;
                m_EndTime = endtime;
                m_Delay = delay;

                from.SendLocalizedMessage( 1112821, String.Concat( "#", SutekQuestResource.GetLabelId(m_Type).ToString() ) ); // I need to add some ~1_INGREDIENT~.

				BeginTimeout( from, m_EndTime - DateTime.Now );
			}

            protected override void OnTargetFinish( Mobile from )
            {
                if ( m_Remaining > 0 )
                    from.Target = new InternalTarget( from, m_Item, m_Type, m_Remaining, m_Delay, m_EndTime );
            }

            protected override void OnTargetCancel( Mobile from, TargetCancelType cancelType )
            {
                if ( TargetCancelType.Timeout == cancelType )
                {
                    CancelTimeout();
                    m_Remaining = 0;
                    from.SendLocalizedMessage( 1112822 ); // You fail to find the next ingredient in time. Your clockwork assembly crumbles.
                    m_Item.Delete();
                }
            }

			protected override void OnTarget( Mobile from, object targeted )
			{
				if ( m_Item == null || m_Item.Deleted )
					return;

				if ( targeted is SutekQuestResource && ((SutekQuestResource)targeted).ActualType == m_Type )
				{
                    CancelTimeout();

                    if ( --m_Remaining > 0 )    
                    {
                        from.SendLocalizedMessage( 1112819 ); // You've successfully added this ingredient.
                        m_Type = SutekQuestResource.GetRandomResource();
                        m_Delay += 0.1 + (Utility.RandomDouble() / 4.0);
                        m_EndTime = DateTime.Now + TimeSpan.FromSeconds(m_Delay);
                    }
                    else
                    {
                        m_Item.StopTimer();
                        Item item = new CompletedClockworkAssembly();
                        if ( null == m_Item.Parent && m_Item.Parent is Container )
                        {
                            item.Location = new Point3D( m_Item.Location );
                            ((Container)m_Item.Parent).AddItem( item );
                        }
                        else
                            item.MoveToWorld( m_Item.GetWorldLocation(), m_Item.Map );

                        m_Item.Delete();

                        from.SendLocalizedMessage( 1112987 ); // The training clockwork fails and the creature vanishes.
                        from.SendLocalizedMessage( 1112872 ); // Your assembly is completed. Return it to Sutek for your reward!
                    }
                }
                else
                {
                    from.SendLocalizedMessage( 1112820 ); // That is not the right ingredient.
                    OnTargetCancel(from, TargetCancelType.Timeout);
                }
			}
		}

        private class InternalTimer : Timer
        {
            private ClockworkMechanism i_item;

            public InternalTimer( ClockworkMechanism item ) : base( TimeSpan.FromSeconds(10.0), TimeSpan.FromSeconds(10.0) )
            {
                Priority = TimerPriority.OneSecond;
                i_item = item;
            }

            protected override void OnTick()
            {
                if ( null == i_item || i_item.Deleted )
                {
                    Stop();
                    return;
                }

                i_item.OnTick();
            }
        }
	}
}
