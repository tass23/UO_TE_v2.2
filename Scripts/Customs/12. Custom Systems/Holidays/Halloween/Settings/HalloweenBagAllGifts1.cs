// Original Author Unknown
// Updated to be halloween 2007 by GreyWolf

using System;
using Server;
using Server.Items;

namespace Server.Items
{  
	public class HalloweenBagAllGifts1 : Bag
	{
		[Constructable]
		public HalloweenBagAllGifts1()
		{
			Name = "Have A Spooky Halloween insert_year";
			Hue = 1258;
			LootType = LootType.Blessed;

			DropItem (new HalloweenLantern() );    	
			DropItem(new HalloweenCloak());
			DropItem(new HalloweenTunic());
			DropItem(new HalloweenDoublet());
			DropItem(new HalloweenBoots());
			DropItem( new HalloweenOuiJaBoard() );
			//DropItem( new HalloweenGhoulStatuette() );
			//DropItem( new AmuletOfTheBoneKnight() );
			//DropItem( new AmuletOfTheGazer() );
			DropItem( new BloodyTableAddonDeed() );
			//DropItem( new CandyBag() );

			switch ( Utility.Random( 3 ) ) 
			{ 
				case 0: DropItem( new DupreCostume( ) ); 
					break; 
				case 1: DropItem( new LordBritishCostume( ) ); 
					break; 
				case 2: DropItem( new LordBlackthorneCostume( ) ); 
					break;
			}
			switch ( Utility.Random ( 21 ) )
			{
				case 0: DropItem( new AbyssmalHorrorCostume() );
					break;
				case 1: DropItem( new LizardmanCostume() );
					break;
				case 2: DropItem( new ArcaneDemonCostume() );
					break;
				case 3: DropItem( new MongbatCostume() );
					break;
				case 4: DropItem( new PixieCostume() );
					break;
				case 5: DropItem( new CentaurCostume() );
					break;
				case 6: DropItem( new CowCostume() );
					break;
				case 7: DropItem( new CyclopsCostume () );
					break;
				case 8: DropItem( new ShadeCostume () );
					break;
				case 9: DropItem( new SkeletonCostume () );
					break;
				case 10: DropItem( new DragonCostume () );
					break;
				case 11: DropItem( new EarthElementalCostume () );
					break;
				case 12: DropItem( new EtherealWarriorCostume () );
					break;
				case 13: DropItem( new EttinCostume () );
					break;
				case 14: DropItem( new FanDancerCostume () );
					break;
				case 15: DropItem( new GargoyleCostume () );
					break;
				case 16: DropItem( new GazerCostume () );
					break;
				case 17: DropItem( new GiantBlackWidowCostume () );
					break;
				case 18: DropItem( new ImpCostume () );
					break;
				case 19: DropItem( new KappaCostume () );
					break;
				case 20: DropItem( new LichCostume () );
					break;
			}
		}

		[Constructable]
		public HalloweenBagAllGifts1(int amount)
		{
		}

		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );

			list.Add( "Halloween insert_year" );
		}

		public HalloweenBagAllGifts1(Serial serial) : base( serial )
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write((int)0); // version 
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();
		}
	}
}
