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
	public class GalahadReport : Item
	{
		[Constructable]
		public GalahadReport() : base( 0x2258 )
		{
			Weight = 1.0;
			Name = "Galahad's Report";
			Hue = 1166;
		}

		public GalahadReport( Serial serial ) : base( serial )
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
		}
	}
}