using System;
using Server;
using Server.Items;
using Server.Mobiles;
using Server.Gumps;

namespace Server.Items
{
	public class AutoFireStand : Item
	{

		[Constructable]
		public AutoFireStand() : base( 0x1183 )
		{
			Weight = 10;
			Name = "an Automatic Fireworks launcher";
			Movable = false;
			Hue = 1161;

			Timer lt = new LaunchTimer(this);
			lt.Start();
		}

		public AutoFireStand( Serial serial ) : base( serial )
		{
		}			

		public void BeginLaunch()
		{
			Map map = this.Map;

			if ( map == null || map == Map.Internal )
				return;

			Point3D ourLoc = GetWorldLocation();

			Point3D startLoc = new Point3D( ourLoc.X, ourLoc.Y, ourLoc.Z + 10 );
			Point3D endLoc = new Point3D( startLoc.X + Utility.RandomMinMax( -2, 2 ), startLoc.Y + Utility.RandomMinMax( -2, 2 ), startLoc.Z + 32 );

			Effects.SendMovingEffect( new Entity( Serial.Zero, startLoc, map ), new Entity( Serial.Zero, endLoc, map ),
				0x36E4, 5, 0, false, false );

			Timer.DelayCall( TimeSpan.FromSeconds( 1.0 ), new TimerStateCallback( FinishLaunch ), new object[]{ this, endLoc, map } );
		}

		private void FinishLaunch( object state )
		{
			object[] states = (object[])state;

			Item from = (Item)states[0];
			Point3D endLoc = (Point3D)states[1];
			Map map = (Map)states[2];

			int hue = Utility.Random( 40 );

			if ( hue < 8 )
				hue = 0x66D;
			else if ( hue < 10 )
				hue = 0x482;
			else if ( hue < 12 )
				hue = 0x47E;
			else if ( hue < 16 )
				hue = 0x480;
			else if ( hue < 20 )
				hue = 0x47F;
			else
				hue = 0;

			if ( Utility.RandomBool() )
				hue = Utility.RandomList( 0x47E, 0x47F, 0x480, 0x482, 0x66D );

			int renderMode = Utility.RandomList( 0, 2, 3, 4, 5, 7 );

			Effects.PlaySound( endLoc, map, Utility.Random( 0x11B, 4 ) );
			Effects.SendLocationEffect( endLoc, map, 0x373A + (0x10 * Utility.Random( 4 )), 16, 10, hue, renderMode );
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int)0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}

 		public class LaunchTimer : Timer 
		{ 
			private AutoFireStand fire; 
 
			public LaunchTimer(AutoFireStand fs) : base( TimeSpan.FromSeconds( 2.0 ) ) 
			{ 
				fire = fs;
			}  
 
				protected override void OnTick() 
			{
				fire.BeginLaunch();
				this.Start();
			} 
		}
	}
}