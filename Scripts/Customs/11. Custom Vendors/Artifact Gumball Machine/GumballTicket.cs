/*Monolith-KHzspeed 2011
ArtifactGumballRewards*/

using System;

namespace Server.Items
{
	public class GumballTicket : Item
	{
	
		public override double DefaultWeight
		{
			get { return 0.0; }
		}
	
		[Constructable]
		public GumballTicket() : this( 1 )
		{
		}

		[Constructable]
		public GumballTicket( int amount ) : base( 0x2D51 )
		{
			Name = "Gumball Machine Ticket";
			LootType=LootType.Blessed;
			Hue = 1260 ;
			Stackable = true;
			Amount = amount;
		}

		public GumballTicket( Serial serial ) : base( serial )
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