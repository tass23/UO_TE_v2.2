// Original Author Unknown
// Updated to be halloween 2007 by GreyWolf

using System;
using Server;
using Server.Items;

namespace Server.Items
{  
	public class HalloweenOuiJaBoard : Item
	{
		[Constructable]
		public HalloweenOuiJaBoard()
		{
			ItemID = 4013;
			Name = "a oui-ja board";
			LootType = LootType.Blessed;
			Hue = 1137;
		}

		[Constructable]
		public HalloweenOuiJaBoard(int amount)
		{
		}

		public HalloweenOuiJaBoard(Serial serial) : base( serial )
		{
		}

		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );

			list.Add( "Halloween insert_year" );
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write((int)0); // version 
		}

		public override void Deserialize(GenericReader reader)
      	{
			base.Deserialize(reader);

			int version = reader.ReadInt();
		}

		public override void OnDoubleClick( Mobile from )
		{
			UseOuiJa( from, true );
		}

		public void UseOuiJa( Mobile from, bool UseCharges )
		{
			Map map = from.Map;

			if ( map == null || map == Map.Internal )
				return;

			from.SendMessage( 0x21, "You place your hand on the board!" );

			Point3D ourLoc = GetWorldLocation();

			Point3D startLoc = new Point3D( ourLoc.X, ourLoc.Y, ourLoc.Z +2 );
			Point3D endLoc = new Point3D( startLoc.X, startLoc.Y, startLoc.Z +2 );

			Timer.DelayCall( TimeSpan.FromSeconds( 0.0 ), new TimerStateCallback( OuiJaEffect ), new object[]{ from, endLoc, map } );
		}

		private void OuiJaEffect( object state )
		{
			object[] states = (object[])state;

			Mobile from = (Mobile)states[0];
			Point3D endLoc = (Point3D)states[1];
			Map map = (Map)states[2];

			int hue = Utility.Random( 10 );

			if ( hue < 1 )
				hue = 1155;
			else if ( hue < 2 )
				hue = 1156;
			else if ( hue < 3 )
				hue = 1157;
			else if ( hue < 4 )
				hue = 1161;
			else if ( hue < 5 )
				hue = 1159;
			else if ( hue < 6 )
				hue = 1175;
			else if ( hue < 7 )
				hue = 1150;
			else if ( hue < 8 )
				hue = 1172;
			else if ( hue < 9 )
				hue = 1276;
			else
				hue = 1260;

			int renderMode = Utility.Random( 9 );

			if ( renderMode < 3 )
				renderMode = 0;

			else if ( renderMode < 6 )
				renderMode = 1;

			else if ( renderMode < 9 )
				renderMode = 2;
			
			
			int effect = Utility.Random( 5 );
			
			if ( effect < 1 )
				Effects.SendLocationEffect( endLoc, map, 0x3709, 350, 10, hue, renderMode );
			else if ( effect < 2 )
				Effects.SendLocationEffect( endLoc, map, 0x374A, 350, 10, hue, renderMode );
			else if ( effect < 3 )
				Effects.SendLocationEffect( endLoc, map, 0x3789, 350, 10, hue, renderMode );
			else if ( effect < 4 )
				Effects.SendLocationEffect( endLoc, map, 0x36BD, 350, 10, hue, renderMode );
			else
				Effects.SendLocationEffect( endLoc, map, 0x375A, 350, 10, hue, renderMode );
			
			int sound = Utility.Random( 10 );
			
			if ( sound < 1 )
				Effects.PlaySound( endLoc, map, 0x284 );
			else if ( sound < 2)
				Effects.PlaySound( endLoc, map, 0x47D );
			else if ( sound < 3)
				Effects.PlaySound( endLoc, map, 0x474 );
			else if ( sound < 4)
				Effects.PlaySound( endLoc, map, 0x457 );
			else if ( sound < 5)
				Effects.PlaySound( endLoc, map, 0xB3 );
			else if ( sound < 6)
				Effects.PlaySound( endLoc, map, 0x3BD );
			else if ( sound < 7)
				Effects.PlaySound( endLoc, map, 0x379 );
			else if ( sound < 8)
				Effects.PlaySound( endLoc, map, 0x37A );
			else if ( sound < 9)
				Effects.PlaySound( endLoc, map, 0x181 );
			else
				Effects.PlaySound( endLoc, map, 0x115 );
		}
	}
}
