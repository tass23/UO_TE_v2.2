using System;

namespace Server.Items
{
	public class Pulp : Item
	{
		[Constructable]
		public Pulp() :  this(100){}

		[Constructable]
		public Pulp(int amount) : base( 0x101F )
		{
		      	Weight = 0.0;
			Stackable = true;
			Hue = 0x481;
			Name = "Clump of Pulp";
			Amount = amount;
		}

		public Pulp( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt();}
	}
}