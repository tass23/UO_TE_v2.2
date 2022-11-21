/*
 * Created by SharpDevelop.
 * User: Sharon
 * Date: 8/18/2005
 * Time: 7:47 AM
 * 
 * Easter Earrings for Quest
 */

using System;

namespace Server.Items
{

	public class EQEarrings : GoldEarrings
	{
		[Constructable]
		public EQEarrings()
		{
			Attributes.Luck = 100;
			Attributes.NightSight = 1;
			Name = "24 Carrot Easter Bobbles";//yes the spelling of 'Carrot' is a joke
			Hue = 1360;
			Weight = 0.1;
		}

		public EQEarrings( Serial serial ) : base( serial )
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
	}
}
