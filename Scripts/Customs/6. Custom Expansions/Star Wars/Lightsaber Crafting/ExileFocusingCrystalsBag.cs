using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class ExileCrystalBag : Bag
	{
		public override string DefaultName
		{
			get { return "an Exile Focusing Crystal Bag"; }
		}

		[Constructable]
		public ExileCrystalBag() : this( 1 )
		{
			Movable = true;
		}

		[Constructable]
		public ExileCrystalBag( int amount )
		{
			DropItem( new Blue2FocusingCrystal() );
			DropItem( new Blue5FocusingCrystal() );
			DropItem( new Green2FocusingCrystal() );
			DropItem( new Yellow3FocusingCrystal() );
			DropItem( new White6FocusingCrystal() );
			DropItem( new Red2FocusingCrystal() );
			DropItem( new Red5FocusingCrystal() );
			DropItem( new Orange4FocusingCrystal() );
			DropItem( new Brown1FocusingCrystal() );
			DropItem( new LightsaberHiltMold() );
		}
		
		public ExileCrystalBag( Serial serial ) : base( serial )
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