using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class RewardScrollBag : Bag
	{
		public override string DefaultName
		{
			get { return "a bag of Reward Scrolls"; }
		}

		[Constructable]
		public RewardScrollBag()
		{
			Movable = true;
			
			DropItem( new RewardScroll() );
			DropItem( new RewardScroll() );
			DropItem( new RewardScroll() );
			DropItem( new RewardScroll() );
			DropItem( new RewardScroll() );
			DropItem( new RewardScroll() );
			DropItem( new RewardScroll() );
			DropItem( new RewardScroll() );
			DropItem( new RewardScroll() );
			DropItem( new RewardScroll() );
		}

		[Constructable]
		public RewardScrollBag( int amount )
		{

		}

		public RewardScrollBag( Serial serial ) : base( serial )
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