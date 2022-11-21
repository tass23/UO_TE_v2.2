/*
 * Created by SharpDevelop.
 * User: Shazzy
 * Date: 7/5/2010
 * Time: 9:10 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class BridgeOfDeathScroll : Item
	{
		public override bool ForceShowProperties{ get{ return ObjectPropertyList.Enabled; } }

		[Constructable]
		public BridgeOfDeathScroll() : base( 0x14EC )
		{
			Hue = 1154;
			Weight = 1.0;
		}

		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );
			list.Add( "Map to The Bridge of Death" );
			list.Add( "Three questions and you may cross in safety." );
			list.Add( "Or you are cast into the Gorge of Eternal Peril." );
		}

		public BridgeOfDeathScroll( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();

			if ( Hue == 2959 )
				Hue = 1154;
		}
	}
}