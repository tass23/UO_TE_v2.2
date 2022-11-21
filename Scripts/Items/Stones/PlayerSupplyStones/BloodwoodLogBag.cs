using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class BloodwoodLogBag : Bag
	{
		public override string DefaultName
		{
			get { return "a Bloodwood Log Bag"; }
		}

		[Constructable]
		public BloodwoodLogBag() : this( 1 )
		{
			Movable = true;
		}

		[Constructable]
		public BloodwoodLogBag( int amount )
		{
			DropItem( new DovetailSaw() );
			DropItem( new BloodwoodLog( 1000 ) );
		}
		
		public BloodwoodLogBag( Serial serial ) : base( serial )
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