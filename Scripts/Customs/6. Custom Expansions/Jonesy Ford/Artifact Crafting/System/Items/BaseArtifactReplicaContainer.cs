using System;
using Server;

namespace Server.Items
{
	public abstract class BaseArtifactReplicaContainer : BaseContainer
	{
		public abstract int ArtifactRarity{ get; }
		public override bool ForceShowProperties{ get{ return true; } }

		public BaseArtifactReplicaContainer( int itemID ) : base( itemID )
		{
			Weight = 10.0;
		}

		public override void AddNameProperties( ObjectPropertyList list )
		{
			base.AddNameProperties( list );
			list.Add( 1061078, this.ArtifactRarity.ToString() ); // artifact rarity ~1_val~
		}

		public BaseArtifactReplicaContainer( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.WriteEncodedInt( 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadEncodedInt();
		}
	}
}