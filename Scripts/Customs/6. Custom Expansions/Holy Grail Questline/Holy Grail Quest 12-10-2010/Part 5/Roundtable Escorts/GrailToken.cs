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
	public class GrailToken : Item
	{
		[Constructable]
		public GrailToken() : base( 0x2F5B )
		{
			Weight = 1.0;
			Name = "Grail Report";
			Hue = 1154;
		}

		public GrailToken( Serial serial ) : base( serial )
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