using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class PlainLogBag : Bag
	{
		public override string DefaultName
		{
			get { return "a Plain Log Bag"; }
		}

		[Constructable]
		public PlainLogBag() : this( 1 )
		{
			Movable = true;
		}

		[Constructable]
		public PlainLogBag( int amount )
		{
			DropItem( new DovetailSaw() );
			DropItem( new Log( 1000 ) );
		}
		
		public PlainLogBag( Serial serial ) : base( serial )
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