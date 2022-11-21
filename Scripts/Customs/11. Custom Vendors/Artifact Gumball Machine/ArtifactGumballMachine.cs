/*Monolith-KHzspeed 2011
ArtifactGumballRewards*/

using System;
using Server.Items;

namespace Server.Items
{
	public class ArtifactGumballMachine : Item
	{
		public override string DefaultName
		{
			get { return "Artifact Gumball Machine"; }
		}

		[Constructable]
		public ArtifactGumballMachine() : base( 13920 )
		{
			Movable = false;
			Hue = 1492;
		}

		public override void OnDoubleClick( Mobile from )
		{
			Container pack = from.Backpack;

			if ( pack != null && pack.ConsumeTotal( typeof( GumballTicket ), 10 ) )
			{	
				from.SendMessage( 0x35, "You receive a plastic Artifact Capsule" );
				from.AddToBackpack( new ArtifactCapsule ());
			}
			else
			{
				from.SendMessage( 0x35, "You need at least 10 Gumball Ticket's in your backpack to use this." );
			}
		}
    
		public ArtifactGumballMachine( Serial serial ) : base( serial )
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