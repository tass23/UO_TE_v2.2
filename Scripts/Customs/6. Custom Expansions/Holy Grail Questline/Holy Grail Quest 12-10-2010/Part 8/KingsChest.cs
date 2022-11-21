using System; 
using Server; 

namespace Server.Items 
{
	[DynamicFliping]
	[Flipable( 0xE41, 0xE40 )]
	public class KingsChest : LockableContainer
	{
		[Constructable]
		public KingsChest() : base( 0xE41 )
		{
			Weight = 1.0;
			Name = "King Arthur's Chest";
			Hue = 1154;

			AddItem( new BankCheck( 100000 ) );
			AddItem( new RewardScroll() );
			AddItem( new RewardScroll() );

			switch ( Utility.Random( 10 ) )
			{
				case 0: AddItem( new Excalibur() ); break;
				case 1: AddItem( new KingArthurCrown() ); break;
				case 2: AddItem( new RobinhoodBow() ); break;
				case 3: AddItem( new MerlinsStaff() ); break;
			}
		}

		public KingsChest( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
}