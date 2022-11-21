using System;
using Server;
using Server.Network;
using Server.Targeting;
using Server.Items;
using Server.SkillHandlers;

namespace Server.Items 
{ 

   public class GinsengFirework : Item 
   { 
      [Constructable] 
      public GinsengFirework() : base( 0xA13 ) 
      { 
		Hue = 1281;
		Name = "a Ginseng Firework";
      } 

      public GinsengFirework( Serial serial ) : base( serial ) 
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

	public override void OnDoubleClick( Mobile from )
	{
			BeginLaunch( from, true );
			this.Delete();
	}

		public void BeginLaunch( Mobile from, bool useCharges )
		{
			Map map = from.Map;

			if ( map == null || map == Map.Internal )
				return;

			from.SendLocalizedMessage( 502615 ); // You launch a firework!

			Point3D ourLoc = GetWorldLocation();

			Point3D startLoc = new Point3D( ourLoc.X, ourLoc.Y, ourLoc.Z + 10 );
			Point3D endLoc = new Point3D( startLoc.X + Utility.RandomMinMax( -2, 2 ), startLoc.Y + Utility.RandomMinMax( -2, 2 ), startLoc.Z + 32 );

			Effects.SendMovingEffect( new Entity( Serial.Zero, startLoc, map ), new Entity( Serial.Zero, endLoc, map ),
				0x36E4, 5, 0, false, false );

			Timer.DelayCall( TimeSpan.FromSeconds( 1.0 ), new TimerStateCallback( FinishLaunch ), new object[]{ from, endLoc, map } );
		}

		private void FinishLaunch( object state )
		{
			object[] states = (object[])state;

			Mobile from = (Mobile)states[0];
			Point3D endLoc = (Point3D)states[1];
			Map map = (Map)states[2];

			//int hue = Utility.Random( 40 );

			//if ( hue < 8 )
				//hue = 0x66D;
			//else if ( hue < 10 )
				//hue = 0x482;
			//else if ( hue < 12 )
				//hue = 0x47E;
			//else if ( hue < 16 )
				//hue = 0x480;
			//else if ( hue < 20 )
				//hue = 0x47F;
			//else
				int hue = 1280;

			//if ( Utility.RandomBool() )
				//hue = Utility.RandomList( 0x47E, 0x47F, 0x480, 0x482, 0x66D );

			int renderMode = Utility.RandomList( 0, 2, 3, 4, 5, 7 );

			Effects.PlaySound( endLoc, map, Utility.Random( 0x11B, 4 ) );
			Effects.SendLocationEffect( endLoc, map, 0x373A + (0x10 * Utility.Random( 4 )), 16, 10, hue, renderMode );
		}
   } 
    
} 
