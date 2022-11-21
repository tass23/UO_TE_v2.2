using System;
using Server;
using Server.Items;

namespace Server.Items
{  
	public class IndependenceDayBag : Bag
	{
        [Constructable]
        public IndependenceDayBag()
        {
           	Name = "A bag full of fun!";
			Hue = Utility.RandomList( 2, 3, 37, 38, 1150, 1153 );
        }

        [Constructable]
        public IndependenceDayBag(int amount)
        {
        }

		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );

			list.Add( "Independence Day" );
		}

        public IndependenceDayBag(Serial serial) : base( serial )
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