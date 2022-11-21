using System;
using Server;
using Server.Network;
using Server.Targeting;
using Server.Items;
using Server.SkillHandlers;

namespace Server.Items 
{ 

   public class SpiderSilkFountain : Item 
   { 
      [Constructable] 
      public SpiderSilkFountain() : base( 0x182D ) 
      { 
		Hue = 1170;
		Name = "a Spider Silk Fountain";
      } 

      public SpiderSilkFountain( Serial serial ) : base( serial ) 
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
	}

		public void BeginLaunch( Mobile from, bool useCharges )
		{
			Map map = from.Map;

			if ( map == null || map == Map.Internal )
				return;

			from.SendLocalizedMessage( 502615 ); // You launch a firework!

			Point3D ourLoc = GetWorldLocation();

			Point3D startLoc = new Point3D( ourLoc.X, ourLoc.Y, ourLoc.Z +5 );
			Point3D endLoc = new Point3D( startLoc.X, startLoc.Y, startLoc.Z +5 );

			Timer.DelayCall( TimeSpan.FromSeconds( 0.0 ), new TimerStateCallback( FinishLaunch ), new object[]{ from, endLoc, map } );
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
				int hue = 1169;

			//if ( Utility.RandomBool() )
				//hue = Utility.RandomList( 0x47E, 0x47F, 0x480, 0x482, 0x66D );

			int renderMode = 2;

			Effects.PlaySound( endLoc, map, 0x4D0 );
			Effects.SendLocationEffect( endLoc, map, 0x3709, 350, 10, hue, renderMode );

			//this.Timer( TimeSpan.FromSeconds( 5.0 ) );
			this.Movable = false;
			Timer.DelayCall( TimeSpan.FromSeconds( 5.0 ), new TimerStateCallback( RemoveFountain ), new object[]{ from, 5 } );
			//this.Delete();
		}

		private void RemoveFountain( object state )
		{
			this.Delete();
		}
   } 
    
} 
