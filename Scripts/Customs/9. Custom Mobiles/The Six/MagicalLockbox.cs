using System;
using Server.Items;
using Server.Network;
using Server.Misc;
using System.Collections;
using Server.Targeting;

namespace Server.Items
{	
	public class MagicalLockbox : MetalBox
	{
		[Constructable]
		public MagicalLockbox ()
		{
            Name = "a magical lockbox"; 
            Hue = 1165;			
			TrapLevel = 5;
			TrapPower = 5;
			
			TrapType = TrapType.PoisonTrap;
			if ( Utility.RandomDouble() < 0.15 )
				DropItem(new RewardScroll());
			if ( Utility.RandomDouble() < 0.25 )
				DropItem(new RewardScroll());
			if ( Utility.RandomDouble() < 0.05 )
				DropItem(new RewardScroll());
			if ( Utility.RandomDouble() < 0.9 )				
				DropItem( new Blight() );
			if ( Utility.RandomDouble() < 0.9 )				
				DropItem( new Scourge() );
			if ( Utility.RandomDouble() < 0.9 )				
				DropItem( new Taint() );
			if ( Utility.RandomDouble() < 0.9 )				
				DropItem( new Putrefication() );
			if ( Utility.RandomDouble() < 0.9 )				
				DropItem( new Corruption() );
			if ( Utility.RandomDouble() < 0.9 )				
				DropItem( new Muculent() );
			if ( Utility.RandomDouble() < 0.001 )
				DropItem( new DiscountCoupon() );
			if ( Utility.RandomDouble() < 0.9 )				
				DropItem( new ParrotItem() );
			if ( Utility.RandomDouble() < 0.9 )				
				DropItem( new RandomTalisman() );

			DropItem( Loot.RandomPossibleReagent() );

			if ( 0.5 > Utility.RandomDouble() )
			{
				switch ( Utility.Random( 9 ) )
				{
					case 0:	DropItem( new Boomstick() );	break;
					case 1:	DropItem( new BrightsightLenses() );	break;
					case 2:	DropItem( new HelmOfSwiftness() );	break;
					case 3:	DropItem( new QuiverOfElements() );	break;
					case 4:	DropItem( new QuiverOfRage() );	break;
					case 5:	DropItem( new TotemOfVoid() );	break;
					case 6:	DropItem( new WildfireBow() );	break;
					case 7:	DropItem( new Windsong() );	break;
					case 8:	DropItem( new BloodwoodSpirit() );	break;
				}
			}

//Strange Gems Begin

			if ( 0.9 > Utility.RandomDouble() )
			{
				switch ( Utility.Random( 3 ) )
				{
					case 0:	DropItem( new StrangeAmethyst1(  ) );	break;
					case 1:	DropItem( new StrangeAmethyst2(  ) );	break;
					case 2:	DropItem( new StrangeAmethyst3(  ) );	break;
				}
			}
			if ( 0.9 > Utility.RandomDouble() )
			{
				switch ( Utility.Random( 3 ) )
				{
					case 0:	DropItem( new StrangeCitrine1(  ) );	break;
					case 1:	DropItem( new StrangeCitrine2(  ) );	break;
					case 2:	DropItem( new StrangeCitrine3(  ) );	break;
				}
			}
			if ( 0.9 > Utility.RandomDouble() )
			{
				switch ( Utility.Random( 4 ) )
				{
					case 0:	DropItem( new StrangeDiamond1(  ) );	break;
					case 1:	DropItem( new StrangeDiamond2(  ) );	break;
					case 2:	DropItem( new StrangeDiamond3(  ) );	break;
					case 3:	DropItem( new StrangeDiamond4(  ) );	break;
				}
			}
			
			DropItem( new StrangeEmerald1(  ) );			
			if ( 0.9 > Utility.RandomDouble() )
			{
				switch ( Utility.Random( 6 ) )
				{
					case 0:	DropItem( new StrangeRuby1(  ) );	break;
					case 1:	DropItem( new StrangeRuby2(  ) );	break;
					case 2:	DropItem( new StrangeRuby3(  ) );	break;
					case 3:	DropItem( new StrangeRuby4(  ) );	break;
					case 4:	DropItem( new StrangeRuby5(  ) );	break;
					case 5:	DropItem( new StrangeRuby6(  ) );	break;
				}
			}
			if ( 0.9 > Utility.RandomDouble() )
			{
				switch ( Utility.Random( 4 ) )
				{
					case 0:	DropItem( new StrangeSapphire1(  ) );	break;
					case 1:	DropItem( new StrangeSapphire2(  ) );	break;
					case 2:	DropItem( new StrangeSapphire3(  ) );	break;
					case 3:	DropItem( new StrangeSapphire4(  ) );	break;
				}
			}
			if ( 0.9 > Utility.RandomDouble() )
			{
				switch ( Utility.Random( 2 ) )
				{
					case 0:	DropItem( new StrangeStarSapphire1(  ) );	break;
					case 1:	DropItem( new StrangeStarSapphire2(  ) );	break;
				}
			}
			if ( 0.9 > Utility.RandomDouble() )
			{
				switch ( Utility.Random( 3 ) )
				{
					case 0:	DropItem( new StrangeTourmaline1(  ) );	break;
					case 1:	DropItem( new StrangeTourmaline2(  ) );	break;
					case 2:	DropItem( new StrangeTourmaline3(  ) );	break;
				}
			}
//Strange Gems End
			
			DropItem( new Gold( 2000, 5000 ) );
			
           	if (Utility.RandomDouble() < 0.025)
                DropItem( new CrimsonCincture() );
//TODO:  Add in high level magic items and strange gems and make them spray all over the ground when opened
		}
		
		public MagicalLockbox( Serial serial ) : base( serial )
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