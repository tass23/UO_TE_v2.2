/*Monolith-KHzspeed 2011
ArtifactGumballRewards*/

using System;
using System.Collections;
using Server.Items;
using Server.Misc;

namespace Server
{
	public class ArtifactCapsule : Item
	{ 
		[Constructable] 
		public ArtifactCapsule() :  base( 0x1870 ) 
		{ 
			Weight = 1.0; 
			Hue = 1260; 
			Name = "A Random Artifact Capsule"; 
			Movable =  true;
            LootType = LootType.Blessed;
		}

		public override void OnDoubleClick( Mobile m ) 
		{	
			if ( !IsChildOf( m.Backpack ) )
			{
				m.SendLocalizedMessage( 1042001 );
			}
			else
			{ 	
				m.AddToBackpack(ArtifactList.RandomArtifact());
				this.Delete();
			}
		} 

		public ArtifactCapsule( Serial serial ) : base( serial ) 
		{ 
		} 
       
		public override void Serialize( GenericWriter writer ) 
		{ 
			base.Serialize( writer ); 
			writer.Write( (int) 1 ); // version 
		}

		public override void Deserialize( GenericReader reader ) 
		{ 
			base.Deserialize( reader ); 
			int version = reader.ReadInt(); 
		} 
	}
}