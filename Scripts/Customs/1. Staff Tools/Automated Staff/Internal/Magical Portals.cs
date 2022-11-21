
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
using Server.Spells;
using Server.Commands;
using Server.Gumps;
using Server.Mobiles;
using Server.Accounting;
using Server.Misc;
using Server.Network;


#region Moonglow Gate

namespace Server.Items
{
    public class MoonglowGate : Moongate
    {
        private bool m_Decays;
        private DateTime m_DecayTime;
        private Timer m_Timer;

        [Constructable]
        public MoonglowGate(): this(true)
        {
        }

        [Constructable]
        public MoonglowGate(bool decays, Point3D loc, Map map): this(decays)
        {
            MoveToWorld(loc, map);
            Effects.PlaySound(loc, map, 0x20E);
        }

        [Constructable]
        public MoonglowGate(bool decays): base(new Point3D(4467, 1283, 5), Map.Trammel)
        {
            Dispellable = false;
            ItemID = 0x1FD4;
            Name = "Gate To Moonglow";
            Hue = 0x26;  //It's Red.

            if (decays)
            {
                m_Decays = true;
                m_DecayTime = DateTime.Now + TimeSpan.FromSeconds(30);

                m_Timer = new InternalTimer(this, m_DecayTime);
                m_Timer.Start();
            }
        }

        public MoonglowGate(Serial serial): base(serial)
        {
        }

        public override void OnAfterDelete()
        {
            if (m_Timer != null)
                m_Timer.Stop();

            base.OnAfterDelete();
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);

            writer.Write(m_Decays);

            if (m_Decays)
                writer.WriteDeltaTime(m_DecayTime);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();

            switch (version)
            {
                case 0:
                    {
                        m_Decays = reader.ReadBool();

                        if (m_Decays)
                        {
                            m_DecayTime = reader.ReadDeltaTime();

                            m_Timer = new InternalTimer(this, m_DecayTime);
                            m_Timer.Start();
                        }

                        break;
                    }
            }
        }

        private class InternalTimer : Timer
        {
            private Item m_Item;

            public InternalTimer(Item item, DateTime end): base(end - DateTime.Now)
            {
                m_Item = item;
            }

            protected override void OnTick()
            {
                m_Item.Delete();
            }
        }
    }
}

#endregion Edited By: A.A.R

#region Britain Gate

namespace Server.Items
{
	public class BritainGate : Moongate
	{
		private bool m_Decays;
		private DateTime m_DecayTime;
		private Timer m_Timer;

		[Constructable]
		public BritainGate() : this( true )
		{
		}

		[Constructable]
		public BritainGate( bool decays, Point3D loc, Map map ) : this( decays )
		{
			MoveToWorld( loc, map );
			Effects.PlaySound( loc, map, 0x20E );
		}

		[Constructable]
		public BritainGate( bool decays ) : base( new Point3D( 1336, 1997, 5 ), Map.Trammel)
		{
			Dispellable = false;
			ItemID = 0x1FD4;
            Name = "Gate To Britain";
            Hue = 0x26;  //It's Red.

			if ( decays )
			{
				m_Decays = true;
				m_DecayTime = DateTime.Now + TimeSpan.FromSeconds( 30 );

				m_Timer = new InternalTimer( this, m_DecayTime );
				m_Timer.Start();
			}
		}

        public BritainGate(Serial serial) : base(serial)
		{
		}

		public override void OnAfterDelete()
		{
			if ( m_Timer != null )
				m_Timer.Stop();

			base.OnAfterDelete();
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); 

			writer.Write( m_Decays );

			if ( m_Decays )
				writer.WriteDeltaTime( m_DecayTime );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			switch ( version )
			{
				case 0:
				{
					m_Decays = reader.ReadBool();

					if ( m_Decays )
					{
						m_DecayTime = reader.ReadDeltaTime();

						m_Timer = new InternalTimer( this, m_DecayTime );
						m_Timer.Start();
					}

					break;
				}
			}
		}

		private class InternalTimer : Timer
		{
			private Item m_Item;

			public InternalTimer( Item item, DateTime end ) : base( end - DateTime.Now )
			{
				m_Item = item;
			}

			protected override void OnTick()
			{
				m_Item.Delete();
			}
		}
	}
}

#endregion Edited By: A.A.R

#region Jhelom Gate

namespace Server.Items
{
    public class JhelomGate : Moongate
    {
        private bool m_Decays;
        private DateTime m_DecayTime;
        private Timer m_Timer;

        [Constructable]
        public JhelomGate(): this(true)
        {
        }

        [Constructable]
        public JhelomGate(bool decays, Point3D loc, Map map): this(decays)
        {
            MoveToWorld(loc, map);
            Effects.PlaySound(loc, map, 0x20E);
        }

        [Constructable]
        public JhelomGate(bool decays): base(new Point3D(1499, 3771, 5), Map.Trammel)
        {
            Dispellable = false;
            ItemID = 0x1FD4;
            Name = "Gate To Jhelom";
            Hue = 0x26;  //It's Red.

            if (decays)
            {
                m_Decays = true;
                m_DecayTime = DateTime.Now + TimeSpan.FromSeconds(30);

                m_Timer = new InternalTimer(this, m_DecayTime);
                m_Timer.Start();
            }
        }

        public JhelomGate(Serial serial): base(serial)
        {
        }

        public override void OnAfterDelete()
        {
            if (m_Timer != null)
                m_Timer.Stop();

            base.OnAfterDelete();
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);

            writer.Write(m_Decays);

            if (m_Decays)
                writer.WriteDeltaTime(m_DecayTime);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();

            switch (version)
            {
                case 0:
                    {
                        m_Decays = reader.ReadBool();

                        if (m_Decays)
                        {
                            m_DecayTime = reader.ReadDeltaTime();

                            m_Timer = new InternalTimer(this, m_DecayTime);
                            m_Timer.Start();
                        }

                        break;
                    }
            }
        }

        private class InternalTimer : Timer
        {
            private Item m_Item;

            public InternalTimer(Item item, DateTime end): base(end - DateTime.Now)
            {
                m_Item = item;
            }

            protected override void OnTick()
            {
                m_Item.Delete();
            }
        }
    }
}

#endregion Edited By: A.A.R

#region Yew Gate

namespace Server.Items
{
    public class YewGate : Moongate
    {
        private bool m_Decays;
        private DateTime m_DecayTime;
        private Timer m_Timer;

        [Constructable]
        public YewGate(): this(true)
        {
        }

        [Constructable]
        public YewGate(bool decays, Point3D loc, Map map): this(decays)
        {
            MoveToWorld(loc, map);
            Effects.PlaySound(loc, map, 0x20E);
        }

        [Constructable]
        public YewGate(bool decays): base(new Point3D(771,  752, 5), Map.Trammel)
        {
            Dispellable = false;
            ItemID = 0x1FD4;
            Name = "Gate To Yew";
            Hue = 0x26;  //It's Red.

            if (decays)
            {
                m_Decays = true;
                m_DecayTime = DateTime.Now + TimeSpan.FromSeconds(30);

                m_Timer = new InternalTimer(this, m_DecayTime);
                m_Timer.Start();
            }
        }

        public YewGate(Serial serial): base(serial)
        {
        }

        public override void OnAfterDelete()
        {
            if (m_Timer != null)
                m_Timer.Stop();

            base.OnAfterDelete();
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);

            writer.Write(m_Decays);

            if (m_Decays)
                writer.WriteDeltaTime(m_DecayTime);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();

            switch (version)
            {
                case 0:
                    {
                        m_Decays = reader.ReadBool();

                        if (m_Decays)
                        {
                            m_DecayTime = reader.ReadDeltaTime();

                            m_Timer = new InternalTimer(this, m_DecayTime);
                            m_Timer.Start();
                        }

                        break;
                    }
            }
        }

        private class InternalTimer : Timer
        {
            private Item m_Item;

            public InternalTimer(Item item, DateTime end): base(end - DateTime.Now)
            {
                m_Item = item;
            }

            protected override void OnTick()
            {
                m_Item.Delete();
            }
        }
    }
}

#endregion Edited By: A.A.R

#region Minoc Gate

namespace Server.Items
{
    public class MinocGate : Moongate
    {
        private bool m_Decays;
        private DateTime m_DecayTime;
        private Timer m_Timer;

        [Constructable]
        public MinocGate(): this(true)
        {
        }

        [Constructable]
        public MinocGate(bool decays, Point3D loc, Map map): this(decays)
        {
            MoveToWorld(loc, map);
            Effects.PlaySound(loc, map, 0x20E);
        }

        [Constructable]
        public MinocGate(bool decays): base(new Point3D(2701,  692, 5), Map.Trammel)
        {
            Dispellable = false;
            ItemID = 0x1FD4;
            Name = "Gate To Minoc";
            Hue = 0x26;  //It's Red.

            if (decays)
            {
                m_Decays = true;
                m_DecayTime = DateTime.Now + TimeSpan.FromSeconds(30);

                m_Timer = new InternalTimer(this, m_DecayTime);
                m_Timer.Start();
            }
        }

        public MinocGate(Serial serial): base(serial)
        {
        }

        public override void OnAfterDelete()
        {
            if (m_Timer != null)
                m_Timer.Stop();

            base.OnAfterDelete();
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);

            writer.Write(m_Decays);

            if (m_Decays)
                writer.WriteDeltaTime(m_DecayTime);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();

            switch (version)
            {
                case 0:
                    {
                        m_Decays = reader.ReadBool();

                        if (m_Decays)
                        {
                            m_DecayTime = reader.ReadDeltaTime();

                            m_Timer = new InternalTimer(this, m_DecayTime);
                            m_Timer.Start();
                        }

                        break;
                    }
            }
        }

        private class InternalTimer : Timer
        {
            private Item m_Item;

            public InternalTimer(Item item, DateTime end): base(end - DateTime.Now)
            {
                m_Item = item;
            }

            protected override void OnTick()
            {
                m_Item.Delete();
            }
        }
    }
}

#endregion Edited By: A.A.R

#region Trinsic Gate

namespace Server.Items
{
    public class TrinsicGate : Moongate
    {
        private bool m_Decays;
        private DateTime m_DecayTime;
        private Timer m_Timer;

        [Constructable]
        public TrinsicGate(): this(true)
        {
        }

        [Constructable]
        public TrinsicGate(bool decays, Point3D loc, Map map): this(decays)
        {
            MoveToWorld(loc, map);
            Effects.PlaySound(loc, map, 0x20E);
        }

        [Constructable]
        public TrinsicGate(bool decays): base(new Point3D(1828, 2948,-20), Map.Trammel)
        {
            Dispellable = false;
            ItemID = 0x1FD4;
            Name = "Gate To Trinsic";
            Hue = 0x26;  //It's Red.

            if (decays)
            {
                m_Decays = true;
                m_DecayTime = DateTime.Now + TimeSpan.FromSeconds(30);

                m_Timer = new InternalTimer(this, m_DecayTime);
                m_Timer.Start();
            }
        }

        public TrinsicGate(Serial serial): base(serial)
        {
        }

        public override void OnAfterDelete()
        {
            if (m_Timer != null)
                m_Timer.Stop();

            base.OnAfterDelete();
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);

            writer.Write(m_Decays);

            if (m_Decays)
                writer.WriteDeltaTime(m_DecayTime);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();

            switch (version)
            {
                case 0:
                    {
                        m_Decays = reader.ReadBool();

                        if (m_Decays)
                        {
                            m_DecayTime = reader.ReadDeltaTime();

                            m_Timer = new InternalTimer(this, m_DecayTime);
                            m_Timer.Start();
                        }

                        break;
                    }
            }
        }

        private class InternalTimer : Timer
        {
            private Item m_Item;

            public InternalTimer(Item item, DateTime end): base(end - DateTime.Now)
            {
                m_Item = item;
            }

            protected override void OnTick()
            {
                m_Item.Delete();
            }
        }
    }
}

#endregion Edited By: A.A.R

#region SkaraBrae Gate

namespace Server.Items
{
    public class SkaraBraeGate : Moongate
    {
        private bool m_Decays;
        private DateTime m_DecayTime;
        private Timer m_Timer;

        [Constructable]
        public SkaraBraeGate(): this(true)
        {
        }

        [Constructable]
        public SkaraBraeGate(bool decays, Point3D loc, Map map): this(decays)
        {
            MoveToWorld(loc, map);
            Effects.PlaySound(loc, map, 0x20E);
        }

        [Constructable]
        public SkaraBraeGate(bool decays): base(new Point3D(643, 2067, 5), Map.Trammel)
        {
            Dispellable = false;
            ItemID = 0x1FD4;
            Name = "Gate To Skara Brae";
            Hue = 0x26;  //It's Red.

            if (decays)
            {
                m_Decays = true;
                m_DecayTime = DateTime.Now + TimeSpan.FromSeconds(30);

                m_Timer = new InternalTimer(this, m_DecayTime);
                m_Timer.Start();
            }
        }

        public SkaraBraeGate(Serial serial): base(serial)
        {
        }

        public override void OnAfterDelete()
        {
            if (m_Timer != null)
                m_Timer.Stop();

            base.OnAfterDelete();
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);

            writer.Write(m_Decays);

            if (m_Decays)
                writer.WriteDeltaTime(m_DecayTime);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();

            switch (version)
            {
                case 0:
                    {
                        m_Decays = reader.ReadBool();

                        if (m_Decays)
                        {
                            m_DecayTime = reader.ReadDeltaTime();

                            m_Timer = new InternalTimer(this, m_DecayTime);
                            m_Timer.Start();
                        }

                        break;
                    }
            }
        }

        private class InternalTimer : Timer
        {
            private Item m_Item;

            public InternalTimer(Item item, DateTime end): base(end - DateTime.Now)
            {
                m_Item = item;
            }

            protected override void OnTick()
            {
                m_Item.Delete();
            }
        }
    }
}

#endregion Edited By: A.A.R

#region Magincia Gate

namespace Server.Items
{
    public class MaginciaGate : Moongate
    {
        private bool m_Decays;
        private DateTime m_DecayTime;
        private Timer m_Timer;

        [Constructable]
        public MaginciaGate(): this(true)
        {
        }

        [Constructable]
        public MaginciaGate(bool decays, Point3D loc, Map map): this(decays)
        {
            MoveToWorld(loc, map);
            Effects.PlaySound(loc, map, 0x20E);
        }

        [Constructable]
        public MaginciaGate(bool decays): base(new Point3D(3563, 2139, 34), Map.Trammel)
        {
            Dispellable = false;
            ItemID = 0x1FD4;
            Name = "Gate To Magincia";
            Hue = 0x26;  //It's Red.

            if (decays)
            {
                m_Decays = true;
                m_DecayTime = DateTime.Now + TimeSpan.FromSeconds(30);

                m_Timer = new InternalTimer(this, m_DecayTime);
                m_Timer.Start();
            }
        }

        public MaginciaGate(Serial serial): base(serial)
        {
        }

        public override void OnAfterDelete()
        {
            if (m_Timer != null)
                m_Timer.Stop();

            base.OnAfterDelete();
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);

            writer.Write(m_Decays);

            if (m_Decays)
                writer.WriteDeltaTime(m_DecayTime);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();

            switch (version)
            {
                case 0:
                    {
                        m_Decays = reader.ReadBool();

                        if (m_Decays)
                        {
                            m_DecayTime = reader.ReadDeltaTime();

                            m_Timer = new InternalTimer(this, m_DecayTime);
                            m_Timer.Start();
                        }

                        break;
                    }
            }
        }

        private class InternalTimer : Timer
        {
            private Item m_Item;

            public InternalTimer(Item item, DateTime end): base(end - DateTime.Now)
            {
                m_Item = item;
            }

            protected override void OnTick()
            {
                m_Item.Delete();
            }
        }
    }
}

#endregion Edited By: A.A.R

#region NewHaven Gate

namespace Server.Items
{
    public class NewHavenGate : Moongate
    {
        private bool m_Decays;
        private DateTime m_DecayTime;
        private Timer m_Timer;

        [Constructable]
        public NewHavenGate(): this(true)
        {
        }

        [Constructable]
        public NewHavenGate(bool decays, Point3D loc, Map map): this(decays)
        {
            MoveToWorld(loc, map);
            Effects.PlaySound(loc, map, 0x20E);
        }

        [Constructable]
        public NewHavenGate(bool decays): base(new Point3D(3450, 2677, 25), Map.Trammel)
        {
            Dispellable = false;
            ItemID = 0x1FD4;
            Name = "Gate To New Haven";
            Hue = 0x26;  //It's Red.

            if (decays)
            {
                m_Decays = true;
                m_DecayTime = DateTime.Now + TimeSpan.FromSeconds(30);

                m_Timer = new InternalTimer(this, m_DecayTime);
                m_Timer.Start();
            }
        }

        public NewHavenGate(Serial serial): base(serial)
        {
        }

        public override void OnAfterDelete()
        {
            if (m_Timer != null)
                m_Timer.Stop();

            base.OnAfterDelete();
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);

            writer.Write(m_Decays);

            if (m_Decays)
                writer.WriteDeltaTime(m_DecayTime);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();

            switch (version)
            {
                case 0:
                    {
                        m_Decays = reader.ReadBool();

                        if (m_Decays)
                        {
                            m_DecayTime = reader.ReadDeltaTime();

                            m_Timer = new InternalTimer(this, m_DecayTime);
                            m_Timer.Start();
                        }

                        break;
                    }
            }
        }

        private class InternalTimer : Timer
        {
            private Item m_Item;

            public InternalTimer(Item item, DateTime end): base(end - DateTime.Now)
            {
                m_Item = item;
            }

            protected override void OnTick()
            {
                m_Item.Delete();
            }
        }
    }
}

#endregion Edited By: A.A.R

#region BucsDen Gate

namespace Server.Items
{
    public class BucsDenGate : Moongate
    {
        private bool m_Decays;
        private DateTime m_DecayTime;
        private Timer m_Timer;

        [Constructable]
        public BucsDenGate(): this(true)
        {
        }

        [Constructable]
        public BucsDenGate(bool decays, Point3D loc, Map map): this(decays)
        {
            MoveToWorld(loc, map);
            Effects.PlaySound(loc, map, 0x20E);
        }

        [Constructable]
        public BucsDenGate(bool decays): base(new Point3D(2711, 2234, 0), Map.Trammel)
        {
            Dispellable = false;
            ItemID = 0x1FD4;
            Name = "Gate To Buccaneer's Den";
            Hue = 0x26;  //It's Red.

            if (decays)
            {
                m_Decays = true;
                m_DecayTime = DateTime.Now + TimeSpan.FromSeconds(30);

                m_Timer = new InternalTimer(this, m_DecayTime);
                m_Timer.Start();
            }
        }

        public BucsDenGate(Serial serial): base(serial)
        {
        }

        public override void OnAfterDelete()
        {
            if (m_Timer != null)
                m_Timer.Stop();

            base.OnAfterDelete();
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);

            writer.Write(m_Decays);

            if (m_Decays)
                writer.WriteDeltaTime(m_DecayTime);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();

            switch (version)
            {
                case 0:
                    {
                        m_Decays = reader.ReadBool();

                        if (m_Decays)
                        {
                            m_DecayTime = reader.ReadDeltaTime();

                            m_Timer = new InternalTimer(this, m_DecayTime);
                            m_Timer.Start();
                        }

                        break;
                    }
            }
        }

        private class InternalTimer : Timer
        {
            private Item m_Item;

            public InternalTimer(Item item, DateTime end): base(end - DateTime.Now)
            {
                m_Item = item;
            }

            protected override void OnTick()
            {
                m_Item.Delete();
            }
        }
    }
}

#endregion Edited By: A.A.R