using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class YewLogBag : Bag
	{
		public override string DefaultName
		{
			get { return "a Yew Log Bag"; }
		}

		[Constructable]
		public YewLogBag() : this( 1 )
		{
			Movable = true;
		}

		[Constructable]
		public YewLogBag( int amount )
		{
			DropItem( new DovetailSaw() );
			DropItem( new YewLog( 1000 ) );
		}
		
		public YewLogBag( Serial serial ) : base( serial )
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