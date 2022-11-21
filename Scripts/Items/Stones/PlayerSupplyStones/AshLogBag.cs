using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class AshLogBag : Bag
	{
		public override string DefaultName
		{
			get { return "an Ash Log Bag"; }
		}

		[Constructable]
		public AshLogBag() : this( 1 )
		{
			Movable = true;
		}

		[Constructable]
		public AshLogBag( int amount )
		{
			DropItem( new DovetailSaw() );
			DropItem( new AshLog( 1000 ) );
		}
		
		public AshLogBag( Serial serial ) : base( serial )
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