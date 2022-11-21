using System;
using Server;
using Server.Engines.Quests;
using Reward = Server.Engines.Quests.BaseReward;

namespace Server.Items
{
	public class BaseCraftsmanSatchel : Backpack
	{
		public BaseCraftsmanSatchel() : base()
		{
			Hue = Reward.SatchelHue();
			
			int count = 1;
			
			if ( 0.015 > Utility.RandomDouble() )
				count = 2;
			
			bool equipment = false;
			bool jewlery = false;
			bool talisman = false;
			
			while ( Items.Count < count )
			{				
				if ( 0.25 > Utility.RandomDouble() && !talisman )
				{
					DropItem( Loot.RandomTalisman() );
					talisman = true;					
				}
				else if ( 0.4 > Utility.RandomDouble() && !equipment )
				{
					DropItem( RandomItem() );		
					equipment = true;		
				}
				else if ( 0.88 > Utility.RandomDouble() && !jewlery )
				{
					DropItem( Reward.Jewlery() );
					jewlery = true;
				}
			}
		}
		
		public BaseCraftsmanSatchel( Serial serial ) : base( serial )
		{
		}
		
		public virtual Item RandomItem()
		{
			return null;
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


	public class FletcherCraftsmanSatchel : BaseCraftsmanSatchel
	{
		[Constructable]
		public FletcherCraftsmanSatchel() : base()
		{
			Name = "a Fletcher Craftsman Satchel";
			
			if ( Items.Count < 2 && 0.5 > Utility.RandomDouble() )
				DropItem( Reward.FletcherRecipe() );
				
			if ( 0.01 > Utility.RandomDouble() )
				DropItem( Reward.FletcherRunic() );
		}
		
		public FletcherCraftsmanSatchel( Serial serial ) : base( serial )
		{
		}
		
		public override Item RandomItem()
		{
			return Reward.RangedWeapon();
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
	
	public class TailorsCraftsmanSatchel : BaseCraftsmanSatchel
	{
		[Constructable]
		public TailorsCraftsmanSatchel() : base()
		{	
			Name = "a Tailor Craftsman Satchel";
			
			if ( Items.Count < 2 && 0.5 > Utility.RandomDouble() )
				DropItem( Reward.TailorRecipe() );
		}
		
		public TailorsCraftsmanSatchel( Serial serial ) : base( serial )
		{
		}
		
		public override Item RandomItem()
		{
			return Reward.Armor();
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
	
	public class SmithsCraftsmanSatchel : BaseCraftsmanSatchel
	{
		[Constructable]
		public SmithsCraftsmanSatchel() : base()
		{
			Name = "a Blacksmith Craftsman Satchel";
			
			if ( Items.Count < 2 && 0.5 > Utility.RandomDouble() )
				DropItem( Reward.SmithRecipe() );
		}
		
		public SmithsCraftsmanSatchel( Serial serial ) : base( serial )
		{
		}
		
		public override Item RandomItem()
		{
			return Reward.Weapon();
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
	
	public class TinkersCraftsmanSatchel : BaseCraftsmanSatchel
	{
		[Constructable]
		public TinkersCraftsmanSatchel() : base()
		{
			Name = "a Tinker Craftsman Satchel";
			
			if ( Items.Count < 2 && 0.5 > Utility.RandomDouble() )
				DropItem( Reward.TinkerRecipe() );
		}
		
		public TinkersCraftsmanSatchel( Serial serial ) : base( serial )
		{
		}
		
		public override Item RandomItem()
		{
			return Reward.Weapon();
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
	
	public class CarpentersCraftsmanSatchel : BaseCraftsmanSatchel
	{
		[Constructable]
		public CarpentersCraftsmanSatchel() : base()
		{
			Name = "a Carpenter Craftsman Satchel";
			
			if ( Items.Count < 2 && 0.5 > Utility.RandomDouble() )
				DropItem( Reward.CarpRecipe() );				
			
			if ( 0.01 > Utility.RandomDouble() )
				DropItem( Reward.CarpRunic() );
		}
		
		public CarpentersCraftsmanSatchel( Serial serial ) : base( serial )
		{
		}
		
		public override Item RandomItem()
		{
			return Reward.Weapon();
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