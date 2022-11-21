using System;
using Server;
using Server.Items;
using System.Diagnostics;
using System.IO;
using System.Text;
using Server.Commands;
using Server.Misc;

namespace Server.Items
{
	public class AutoRestarter : Item
	{
		public override bool Decays{ get{ return false; } }
		public static bool b_switchon;
		[CommandProperty( AccessLevel.Owner )]
		public bool b_Switchon { get { return b_switchon; } set { b_switchon = value; if (value) Restart_Auto_Func(); } }
		public static TimeSpan t_autotime;
		[CommandProperty( AccessLevel.Owner )]
		public TimeSpan t_AutoTime { get { return t_autotime; } set { t_autotime = value; t_RestartTime = DateTime.Now.Date + t_autotime; } }
		public DateTime t_RestartTime;

		[Constructable]
		public AutoRestarter() : base(6434)
		{
			Name = "the auto restarter";
			Visible = false;
			Movable = false;
			t_RestartTime = DateTime.Now.Date + TimeSpan.FromDays( 1.0 );
		}

		public AutoRestarter( Serial serial ) : base(serial){}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
			writer.Write( (bool) b_switchon);
			writer.Write( (TimeSpan) t_autotime );
			writer.Write( (DateTime) t_RestartTime );
		}

		private void Restart_Auto_Func()
		{
			I_Auto_Restarter tmr = new I_Auto_Restarter(this);
			tmr.Start();
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			b_switchon = reader.ReadBool();
			t_autotime = reader.ReadTimeSpan();
			t_RestartTime = reader.ReadDateTime();
			if (b_switchon)
			{
				I_Auto_Restarter tmr = new I_Auto_Restarter(this);
				tmr.Start();
			}
		}
	}

	public class I_Auto_Restarter : Timer
	{
		private AutoRestarter item;
		public  I_Auto_Restarter(AutoRestarter i) : base( TimeSpan.FromSeconds( 1.0 ), TimeSpan.FromSeconds( 1.0 ) )
		{
			item = i;
			Priority = TimerPriority.FiveSeconds;
		}

		protected override void OnTick()
		{
			if (item == null || item.Deleted ) { this.Stop(); return;}
			else if (((AutoRestarter)item).b_Switchon == false) { this.Stop(); return; }
			if (DateTime.Now >= item.t_RestartTime)
			{
				World.Broadcast( 0x22, true, "The server is going down in  5 minutes." );
				item.t_RestartTime += TimeSpan.FromDays( 1.0 );
				AutoSave.Save();
				I_Auto_Restarter2 tmr2 = new I_Auto_Restarter2();
				tmr2.Start();
				this.Stop();
			}
		}
	}

	public class I_Auto_Restarter2 : Timer
	{
		private Item item;
		public  I_Auto_Restarter2() : base( TimeSpan.FromSeconds( 60.0 ) ) {}

		protected override void OnTick()
		{
			World.Broadcast( 0x22, true, "The server is going down in 4 minutes." );
			I_Auto_Restarter3 tmr3 = new I_Auto_Restarter3();
			tmr3.Start();
			this.Stop();
		}
	}

	public class I_Auto_Restarter3 : Timer
	{
		private Item item;
		public  I_Auto_Restarter3() : base( TimeSpan.FromSeconds( 60.0 ) ) {}

		protected override void OnTick()
		{
			World.Broadcast( 0x22, true, "The server is going down in  3 minutes." );
			I_Auto_Restarter4 tmr4 = new I_Auto_Restarter4();
			tmr4.Start();
			this.Stop();
		}
	}

	public class I_Auto_Restarter4 : Timer
	{
		private Item item;
		public  I_Auto_Restarter4() : base( TimeSpan.FromSeconds( 60.0 ) ) {}

		protected override void OnTick()
		{
			World.Broadcast( 0x22, true, "The server is going down in  2 minutes." );
			I_Auto_Restarter5 tmr5 = new I_Auto_Restarter5();
			tmr5.Start();
			this.Stop();
		}
	}

	public class I_Auto_Restarter5 : Timer
	{
		private Item item;
		public  I_Auto_Restarter5() : base( TimeSpan.FromSeconds( 60.0 ) ) {}

		protected override void OnTick()
		{
			World.Broadcast( 0x22, true, "The server is going down in  1 minute." );
			I_Auto_Restarter6 tmr6 = new I_Auto_Restarter6();
			tmr6.Start();
			this.Stop();
		}
	}

	public class I_Auto_Restarter6 : Timer
	{
		private Item item;
		public  I_Auto_Restarter6() : base( TimeSpan.FromSeconds( 15.0 ) ) {}

		protected override void OnTick()
		{
			World.Broadcast( 0x22, true, "The server is going down in 45 seconds." );
			I_Auto_RestarterA tmrA = new I_Auto_RestarterA();
			tmrA.Start();
			this.Stop();
		}
	}

	public class I_Auto_RestarterA : Timer
	{
		private Item item;
		public  I_Auto_RestarterA() : base( TimeSpan.FromSeconds( 15.0 ) ) {}

		protected override void OnTick()
		{
			World.Broadcast( 0x22, true, "The server is going down in 30 seconds." );
			I_Auto_RestarterB tmrB = new I_Auto_RestarterB();
			tmrB.Start();
			this.Stop();
		}
	}

	public class I_Auto_RestarterB : Timer
	{
		private Item item;
		public  I_Auto_RestarterB() : base( TimeSpan.FromSeconds( 15.0 ) ) {}

		protected override void OnTick()
		{
			World.Broadcast( 0x22, true, "The server is going down in 15 seconds." );
			AutoSave.Save();
			I_Auto_RestarterC tmrC = new I_Auto_RestarterC();
			tmrC.Start();
			this.Stop();
		}
	}

	public class I_Auto_RestarterC : Timer
	{
		private Item item;
		public  I_Auto_RestarterC() : base( TimeSpan.FromSeconds( 15.0 ) ) {}

		protected override void OnTick()
		{
			World.Broadcast( 0x22, true, "The server is Now Restarting." );
			AutoSave.Save();
			Process.Start( Core.ExePath, Core.Arguments );
			Core.Process.Kill();
		}
	}
}