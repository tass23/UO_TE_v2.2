using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class OakLogBag : Bag
	{
		public override string DefaultName
		{
			get { return "an Oak Log Bag"; }
		}

		[Constructable]
		public OakLogBag() : this( 1 )
		{
			Movable = true;
		}

		[Constructable]
		public OakLogBag( int amount )
		{
			DropItem( new DovetailSaw() );
			DropItem( new OakLog( 1000 ) );
		}
		
		public OakLogBag( Serial serial ) : base( serial )
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