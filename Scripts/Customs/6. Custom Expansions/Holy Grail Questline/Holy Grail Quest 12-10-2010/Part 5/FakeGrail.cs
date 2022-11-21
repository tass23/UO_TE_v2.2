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

namespace Server.Items
{
	public class FakeGrail : Item
	{
		[Constructable]
		public FakeGrail() : base( 0x99A )
		{
			Weight = 1.0;
			Name = "Fake Holy Grail";
			Hue = 1154;
		}

		public FakeGrail( Serial serial ) : base( serial )
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