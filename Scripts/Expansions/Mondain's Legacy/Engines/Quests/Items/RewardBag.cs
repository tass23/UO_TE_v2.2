using System;
using Server;
using Server.Engines.Quests;
using Reward = Server.Engines.Quests.BaseReward;

namespace Server.Items
{
	public class BaseRewardBag : Bag
	{
		public virtual int ItemAmount{ get{ return 0; } }
	
		public BaseRewardBag() : base()
		{
			Hue = Reward.RewardBagHue();
			
			while ( Items.Count < ItemAmount )
			{				
				if ( 0.05 > Utility.RandomDouble() ) // check
					DropItem( Loot.RandomTalisman() );
				else 
				{
					switch ( Utility.Random( 4 ) )
					{
						case 0: DropItem( Reward.Armor() ); break;	
						case 1: DropItem( Reward.RangedWeapon() ); break;
						case 2: DropItem( Reward.Weapon() ); break;
						case 3: DropItem( Reward.Jewlery() ); break;
					}
				}
			}
		}
		
		public BaseRewardBag( Serial serial ) : base( serial )
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
	
	public class SmallTrinketBag : BaseRewardBag
	{
		public override int ItemAmount{ get{ return 1; } }
		
		[Constructable]
		public SmallTrinketBag() : base()
		{			
		}
		
		public SmallTrinketBag( Serial serial ) : base( serial )
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
	
	public class TrinketBag : BaseRewardBag
	{
		public override int ItemAmount{ get{ return 2; } }
		
		[Constructable]
		public TrinketBag() : base()
		{			
		}
		
		public TrinketBag( Serial serial ) : base( serial )
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
	
	public class TreasureBag : BaseRewardBag
	{
		public override int ItemAmount{ get{ return 3; } }
		
		[Constructable]
		public TreasureBag() : base()
		{			
		}
		
		public TreasureBag( Serial serial ) : base( serial )
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
	
	public class LargeTreasureBag : BaseRewardBag
	{
		public override int ItemAmount{ get{ return 4; } }
		
		[Constructable]
		public LargeTreasureBag() : base()
		{			
		}
		
		public LargeTreasureBag( Serial serial ) : base( serial )
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