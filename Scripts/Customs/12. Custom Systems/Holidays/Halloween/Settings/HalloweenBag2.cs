// Original Author Unknown
// Updated to be halloween 2007 by GreyWolf

using System;
using Server;
using Server.Items;

namespace Server.Items
{  
	public class HalloweenBag2 : Bag
	{
        [Constructable]
        public HalloweenBag2()
        {
			Name = "Have A Spooky Halloween insert_year";
			Hue = 1258;
        }

        [Constructable]
        public HalloweenBag2(int amount)
        {
        }

		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );

			list.Add( "Halloween insert_year" );
		}

        public HalloweenBag2(Serial serial) : base( serial )
		{
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
