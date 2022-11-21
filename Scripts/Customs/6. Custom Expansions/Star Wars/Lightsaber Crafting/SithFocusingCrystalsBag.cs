using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class SithCrystalBag : Bag
	{
		public override string DefaultName
		{
			get { return "a Sith Focusing Crystal Bag"; }
		}

		[Constructable]
		public SithCrystalBag() : this( 1 )
		{
			Movable = true;
		}

		[Constructable]
		public SithCrystalBag( int amount )
		{
			DropItem( new White1FocusingCrystal() );
			DropItem( new White3FocusingCrystal() );
			DropItem( new White4FocusingCrystal() );
			DropItem( new White7FocusingCrystal() );
			DropItem( new Gray1FocusingCrystal() );
			DropItem( new Gray2FocusingCrystal() );
			DropItem( new Gray3FocusingCrystal() );
			DropItem( new Red3FocusingCrystal() );
			DropItem( new Red4FocusingCrystal() );
			DropItem( new Red6FocusingCrystal() );
			DropItem( new Red7FocusingCrystal() );
			DropItem( new Pink1FocusingCrystal() );
			DropItem( new Orange2FocusingCrystal() );
			DropItem( new Brown2FocusingCrystal() );
			DropItem( new LightsaberHiltMold() );
		}
		
		public SithCrystalBag( Serial serial ) : base( serial )
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