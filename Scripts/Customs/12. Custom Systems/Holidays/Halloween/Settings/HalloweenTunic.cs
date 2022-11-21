// Original Author Unknown
// Updated to be halloween 2007 by GreyWolf

using System;
using Server;
using Server.Items;

namespace Server.Items
{  
	public class HalloweenTunic : Tunic
	{
		[Constructable]
		public HalloweenTunic()
		{
			Name = "a Spectral Doublet";
			Hue = 0x4001;
			LootType = LootType.Blessed;
		}

		[Constructable]
		public HalloweenTunic(int amount)
		{
		}

		public HalloweenTunic(Serial serial) : base( serial )
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
	}
}
