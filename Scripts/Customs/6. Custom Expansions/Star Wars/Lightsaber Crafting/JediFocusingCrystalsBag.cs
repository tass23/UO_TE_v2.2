using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class JediCrystalBag : Bag
	{
		public override string DefaultName
		{
			get { return "a Jedi Focusing Crystal Bag"; }
		}

		[Constructable]
		public JediCrystalBag() : this( 1 )
		{
			Movable = true;
		}

		[Constructable]
		public JediCrystalBag( int amount )
		{
			DropItem( new Blue1FocusingCrystal() );
			DropItem( new Blue3FocusingCrystal() );
			DropItem( new Blue4FocusingCrystal() );
			DropItem( new Blue6FocusingCrystal() );
			DropItem( new Green1FocusingCrystal() );
			DropItem( new Green3FocusingCrystal() );
			DropItem( new Green4FocusingCrystal() );
			DropItem( new Green5FocusingCrystal() );
			DropItem( new Yellow1FocusingCrystal() );
			DropItem( new Yellow2FocusingCrystal() );
			DropItem( new White2FocusingCrystal() );
			DropItem( new White5FocusingCrystal() );
			DropItem( new White8FocusingCrystal() );
			DropItem( new Red1FocusingCrystal() );
			DropItem( new Pink2FocusingCrystal() );
			DropItem( new Pink3FocusingCrystal() );
			DropItem( new Purple1FocusingCrystal() );
			DropItem( new Purple2FocusingCrystal() );
			DropItem( new Orange1FocusingCrystal() );
			DropItem( new Orange3FocusingCrystal() );
			DropItem( new Cyan1FocusingCrystal() );
			DropItem( new Cyan2FocusingCrystal() );
			DropItem( new LightsaberHiltMold() );
		}
		
		public JediCrystalBag( Serial serial ) : base( serial )
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