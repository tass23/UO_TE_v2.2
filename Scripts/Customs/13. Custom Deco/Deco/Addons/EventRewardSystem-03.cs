/**************************
Event Reward System - 03
 www.landofobsidian.com
 written by X-SirSly-X

Have the players line up 
in a straight line, off
their mounts (or else 
their mounts will get one 
as well) and type one of the
following:

[area addtopack EventRewardSmall  Eventname
[area addtopack EventRewardMedium  Eventname
[area addtopack EventRewardLarge  Eventname

Then just click on one side of
the players, and then click on
the other side of the players.

**************************/

using System;
using Server;
using Server.Misc;

namespace Server.Items
{	
	public class EventRewardSmall : BaseCloak
	{

    private string EventName = "";

		[Constructable]
		public EventRewardSmall( string EventName ) : base( 0x230A )
		{
			Name = EventName+" ["+DateTime.Now+"]"; 
			Hue = Utility.Random( 1, 300 );
			Weight = 1.0;
	
			//START - Picking a item			
			switch ( Utility.Random( 33 ) )
			{
				case 0:  ItemID = 0x1540; Layer = Layer.Helm; break;
				case 1:  ItemID = 0x1541; Layer = Layer.MiddleTorso; break;
				case 2:  ItemID = 0x1F03; Layer = Layer.OuterTorso; break;
				case 3:  ItemID = 0x1719; Layer = Layer.Helm; break;
				case 4:  ItemID = 0x170B; Layer = Layer.Shoes; break;
				case 5:  ItemID = 0x1715; Layer = Layer.Helm; break;
				case 6:  ItemID = 0x1515; Layer = Layer.Cloak; break;
				case 7:  ItemID = 0x1F03; Layer = Layer.OuterTorso; break;
				case 8:  ItemID = 0x1F7B; Layer = Layer.MiddleTorso; break;
				case 9:  ItemID = 0x171A; Layer = Layer.Helm; break;
				case 10: ItemID = 0x1713; Layer = Layer.Helm; break;
				case 11: ItemID = 0x2306; Layer = Layer.Helm; break;
				case 12: ItemID = 0x153D; Layer = Layer.MiddleTorso; break;
				case 13: ItemID = 0x2307; Layer = Layer.Shoes; break;
				case 14: ItemID = 0x230A; Layer = Layer.Cloak; break;
				case 15: ItemID = 0x153B; Layer = Layer.Waist; break;
				case 16: ItemID = 0x171C; Layer = Layer.Helm; break;
				case 17: ItemID = 0x1F9F; Layer = Layer.MiddleTorso; break;
				case 18: ItemID = 0x1537; Layer = Layer.Waist; break;
				case 19: ItemID = 0x1539; Layer = Layer.Pants; break;
				case 20: ItemID = 0x1718; Layer = Layer.Helm; break;
				case 21: ItemID = 0x170D; Layer = Layer.Shoes; break;
				case 22: ItemID = 0x1517; Layer = Layer.Shirt; break;
				case 23: ItemID = 0x170F; Layer = Layer.Shoes; break;
				case 24: ItemID = 0x152E; Layer = Layer.Pants; break;
				case 25: ItemID = 0x1544; Layer = Layer.Helm; break;
				case 26: ItemID = 0x1717; Layer = Layer.Helm; break;
				case 27: ItemID = 0x1FFD; Layer = Layer.MiddleTorso; break;
				case 28: ItemID = 0x1716; Layer = Layer.Helm; break;
				case 29: ItemID = 0x1711; Layer = Layer.Shoes; break;
				case 30: ItemID = 0x171B; Layer = Layer.Helm; break;
				case 31: ItemID = 0x1FA1; Layer = Layer.MiddleTorso; break;
				case 32: ItemID = 0x1714; Layer = Layer.Helm; break;
			}
			//END

			//START - Adding bonus - Small
			switch ( Utility.Random( 19 ) )
			{
				case 0:  Attributes.BonusStr = 1;  break;
		 	 	case 1:  Attributes.BonusHits = 1;  break;
		 	 	case 2:  Attributes.BonusDex = 1;  break;
		 	 	case 3:  Attributes.BonusStam = 1;  break;
		 	 	case 4:  Attributes.BonusInt = 1;  break;
		 	 	case 5:  Attributes.BonusMana = 1;  break;
		 	 	case 6:  Attributes.RegenHits = 1;  break;
		 	 	case 7:  Attributes.RegenStam = 1;  break;
		 	 	case 8:  Attributes.RegenMana = 1;  break;
		 	 	case 9:  Attributes.AttackChance = 5;  break;
		 	 	case 10: Attributes.DefendChance = 5;  break;
		 	 	case 11: Attributes.WeaponDamage = 5;  break;
		 	 	case 12: Attributes.WeaponSpeed = 5;  break;
		 	 	case 13: Attributes.Luck = 25;  break;
		 	 	case 14: Attributes.ReflectPhysical = 5;  break;
		 	 	case 15: Attributes.EnhancePotions = 5;  break;
		 	 	case 16: Attributes.SpellDamage = 5;  break;
		 	 	case 17: Attributes.LowerManaCost = 5;  break;
		 	 	case 18: Attributes.LowerRegCost = 5;  break;			  
//		 	 	case 19: Attributes.CastSpeed = 1;  break;
//		 	 	case 20: Attributes.CastRecovery = 1;  break;

			}  
			//END
		}

		public EventRewardSmall( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 1 ); // version
			writer.Write( EventName );

		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

				switch (version)
				{
				  case 1 :
				  {
				    EventName = reader.ReadString();
				    break;
				  }
				}

		}
	}

	public class EventRewardMedium : BaseCloak
	{

    private string EventName = "";

		[Constructable]
		public EventRewardMedium( string EventName ) : base( 0x230A )
		{
			Name = EventName+" ["+DateTime.Now+"]"; 
			Hue = Utility.Random( 1, 300 );
			Weight = 1.0;
		
			//START - Picking a item			
			switch ( Utility.Random( 33 ) )
			{
				case 0:  ItemID = 0x1540; Layer = Layer.Helm; break;
				case 1:  ItemID = 0x1541; Layer = Layer.MiddleTorso; break;
				case 2:  ItemID = 0x1F03; Layer = Layer.OuterTorso; break;
				case 3:  ItemID = 0x1719; Layer = Layer.Helm; break;
				case 4:  ItemID = 0x170B; Layer = Layer.Shoes; break;
				case 5:  ItemID = 0x1715; Layer = Layer.Helm; break;
				case 6:  ItemID = 0x1515; Layer = Layer.Cloak; break;
				case 7:  ItemID = 0x1F03; Layer = Layer.OuterTorso; break;
				case 8:  ItemID = 0x1F7B; Layer = Layer.MiddleTorso; break;
				case 9:  ItemID = 0x171A; Layer = Layer.Helm; break;
				case 10: ItemID = 0x1713; Layer = Layer.Helm; break;
				case 11: ItemID = 0x2306; Layer = Layer.Helm; break;
				case 12: ItemID = 0x153D; Layer = Layer.MiddleTorso; break;
				case 13: ItemID = 0x2307; Layer = Layer.Shoes; break;
				case 14: ItemID = 0x230A; Layer = Layer.Cloak; break;
				case 15: ItemID = 0x153B; Layer = Layer.Waist; break;
				case 16: ItemID = 0x171C; Layer = Layer.Helm; break;
				case 17: ItemID = 0x1F9F; Layer = Layer.MiddleTorso; break;
				case 18: ItemID = 0x1537; Layer = Layer.Waist; break;
				case 19: ItemID = 0x1539; Layer = Layer.Pants; break;
				case 20: ItemID = 0x1718; Layer = Layer.Helm; break;
				case 21: ItemID = 0x170D; Layer = Layer.Shoes; break;
				case 22: ItemID = 0x1517; Layer = Layer.Shirt; break;
				case 23: ItemID = 0x170F; Layer = Layer.Shoes; break;
				case 24: ItemID = 0x152E; Layer = Layer.Pants; break;
				case 25: ItemID = 0x1544; Layer = Layer.Helm; break;
				case 26: ItemID = 0x1717; Layer = Layer.Helm; break;
				case 27: ItemID = 0x1FFD; Layer = Layer.MiddleTorso; break;
				case 28: ItemID = 0x1716; Layer = Layer.Helm; break;
				case 29: ItemID = 0x1711; Layer = Layer.Shoes; break;
				case 30: ItemID = 0x171B; Layer = Layer.Helm; break;
				case 31: ItemID = 0x1FA1; Layer = Layer.MiddleTorso; break;
				case 32: ItemID = 0x1714; Layer = Layer.Helm; break;
			}
			//END

			//START - Adding bonus - Small
			switch ( Utility.Random( 19 ) )
			{
				case 0:  Attributes.BonusStr = 1;  break;
		 	 	case 1:  Attributes.BonusHits = 1;  break;
		 	 	case 2:  Attributes.BonusDex = 1;  break;
		 	 	case 3:  Attributes.BonusStam = 1;  break;
		 	 	case 4:  Attributes.BonusInt = 1;  break;
		 	 	case 5:  Attributes.BonusMana = 1;  break;
		 	 	case 6:  Attributes.RegenHits = 1;  break;
		 	 	case 7:  Attributes.RegenStam = 1;  break;
		 	 	case 8:  Attributes.RegenMana = 1;  break;
		 	 	case 9:  Attributes.AttackChance = 5;  break;
		 	 	case 10: Attributes.DefendChance = 5;  break;
		 	 	case 11: Attributes.WeaponDamage = 5;  break;
		 	 	case 12: Attributes.WeaponSpeed = 5;  break;
		 	 	case 13: Attributes.Luck = 25;  break;
		 	 	case 14: Attributes.ReflectPhysical = 5;  break;
		 	 	case 15: Attributes.EnhancePotions = 5;  break;
		 	 	case 16: Attributes.SpellDamage = 5;  break;
		 	 	case 17: Attributes.LowerManaCost = 5;  break;
		 	 	case 18: Attributes.LowerRegCost = 5;  break;			  
//		 	 	case 19: Attributes.CastSpeed = 1;  break;
//		 	 	case 20: Attributes.CastRecovery = 1;  break;

			}  
			//END

			//START - Adding bonus - Medium
			switch ( Utility.Random( 19 ) )
			{
				case 0:  Attributes.BonusStr = 2;  break;
		 	 	case 1:  Attributes.BonusHits = 2;  break;
		 	 	case 2:  Attributes.BonusDex = 2;  break;
		 	 	case 3:  Attributes.BonusStam = 2;  break;
		 	 	case 4:  Attributes.BonusInt = 2;  break;
		 	 	case 5:  Attributes.BonusMana = 2;  break;
		 	 	case 6:  Attributes.RegenHits = 2;  break;
		 	 	case 7:  Attributes.RegenStam = 2;  break;
		 	 	case 8:  Attributes.RegenMana = 2;  break;
		 	 	case 9:  Attributes.AttackChance = 10;  break;
		 	 	case 10: Attributes.DefendChance = 10;  break;
		 	 	case 11: Attributes.WeaponDamage = 10;  break;
		 	 	case 12: Attributes.WeaponSpeed = 10;  break;
		 	 	case 13: Attributes.Luck = 50;  break;
		 	 	case 14: Attributes.ReflectPhysical = 10;  break;
		 	 	case 15: Attributes.EnhancePotions = 10;  break;
		 	 	case 16: Attributes.SpellDamage = 10;  break;
		 	 	case 17: Attributes.LowerManaCost = 10;  break;
		 	 	case 18: Attributes.LowerRegCost = 10;  break;			  
//		 	 	case 19: Attributes.CastSpeed = 2;  break;
//		 	 	case 20: Attributes.CastRecovery = 2;  break;
			}  
			//END
		}

		public EventRewardMedium( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 1 ); // version
			writer.Write( EventName );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

				switch (version)
				{
				  case 1 :
				  {
				    EventName = reader.ReadString();
				    break;
				  }
				}

		}
	}

	public class EventRewardLarge : BaseCloak
	{

    private string EventName = "";

		[Constructable]
		public EventRewardLarge( string EventName ) : base( 0x230A )
		{
			Name = EventName+" ["+DateTime.Now+"]"; 
			Hue = Utility.Random( 1, 300 );
			Weight = 1.0;
		
			//START - Picking a item			
			switch ( Utility.Random( 33 ) )
			{
				case 0:  ItemID = 0x1540; Layer = Layer.Helm; break;
				case 1:  ItemID = 0x1541; Layer = Layer.MiddleTorso; break;
				case 2:  ItemID = 0x1F03; Layer = Layer.OuterTorso; break;
				case 3:  ItemID = 0x1719; Layer = Layer.Helm; break;
				case 4:  ItemID = 0x170B; Layer = Layer.Shoes; break;
				case 5:  ItemID = 0x1715; Layer = Layer.Helm; break;
				case 6:  ItemID = 0x1515; Layer = Layer.Cloak; break;
				case 7:  ItemID = 0x1F03; Layer = Layer.OuterTorso; break;
				case 8:  ItemID = 0x1F7B; Layer = Layer.MiddleTorso; break;
				case 9:  ItemID = 0x171A; Layer = Layer.Helm; break;
				case 10: ItemID = 0x1713; Layer = Layer.Helm; break;
				case 11: ItemID = 0x2306; Layer = Layer.Helm; break;
				case 12: ItemID = 0x153D; Layer = Layer.MiddleTorso; break;
				case 13: ItemID = 0x2307; Layer = Layer.Shoes; break;
				case 14: ItemID = 0x230A; Layer = Layer.Cloak; break;
				case 15: ItemID = 0x153B; Layer = Layer.Waist; break;
				case 16: ItemID = 0x171C; Layer = Layer.Helm; break;
				case 17: ItemID = 0x1F9F; Layer = Layer.MiddleTorso; break;
				case 18: ItemID = 0x1537; Layer = Layer.Waist; break;
				case 19: ItemID = 0x1539; Layer = Layer.Pants; break;
				case 20: ItemID = 0x1718; Layer = Layer.Helm; break;
				case 21: ItemID = 0x170D; Layer = Layer.Shoes; break;
				case 22: ItemID = 0x1517; Layer = Layer.Shirt; break;
				case 23: ItemID = 0x170F; Layer = Layer.Shoes; break;
				case 24: ItemID = 0x152E; Layer = Layer.Pants; break;
				case 25: ItemID = 0x1544; Layer = Layer.Helm; break;
				case 26: ItemID = 0x1717; Layer = Layer.Helm; break;
				case 27: ItemID = 0x1FFD; Layer = Layer.MiddleTorso; break;
				case 28: ItemID = 0x1716; Layer = Layer.Helm; break;
				case 29: ItemID = 0x1711; Layer = Layer.Shoes; break;
				case 30: ItemID = 0x171B; Layer = Layer.Helm; break;
				case 31: ItemID = 0x1FA1; Layer = Layer.MiddleTorso; break;
				case 32: ItemID = 0x1714; Layer = Layer.Helm; break;
			}
			//END

			//START - Adding bonus - Small
			switch ( Utility.Random( 19 ) )
			{
				case 0:  Attributes.BonusStr = 1;  break;
		 	 	case 1:  Attributes.BonusHits = 1;  break;
		 	 	case 2:  Attributes.BonusDex = 1;  break;
		 	 	case 3:  Attributes.BonusStam = 1;  break;
		 	 	case 4:  Attributes.BonusInt = 1;  break;
		 	 	case 5:  Attributes.BonusMana = 1;  break;
		 	 	case 6:  Attributes.RegenHits = 1;  break;
		 	 	case 7:  Attributes.RegenStam = 1;  break;
		 	 	case 8:  Attributes.RegenMana = 1;  break;
		 	 	case 9:  Attributes.AttackChance = 5;  break;
		 	 	case 10: Attributes.DefendChance = 5;  break;
		 	 	case 11: Attributes.WeaponDamage = 5;  break;
		 	 	case 12: Attributes.WeaponSpeed = 5;  break;
		 	 	case 13: Attributes.Luck = 25;  break;
		 	 	case 14: Attributes.ReflectPhysical = 5;  break;
		 	 	case 15: Attributes.EnhancePotions = 5;  break;
		 	 	case 16: Attributes.SpellDamage = 5;  break;
		 	 	case 17: Attributes.LowerManaCost = 5;  break;
		 	 	case 18: Attributes.LowerRegCost = 5;  break;			  
//		 	 	case 19: Attributes.CastSpeed = 1;  break;
//		 	 	case 20: Attributes.CastRecovery = 1;  break;
			}  
			//END

			//START - Adding bonus - Medium
			switch ( Utility.Random( 19 ) )
			{
				case 0:  Attributes.BonusStr = 2;  break;
		 	 	case 1:  Attributes.BonusHits = 2;  break;
		 	 	case 2:  Attributes.BonusDex = 2;  break;
		 	 	case 3:  Attributes.BonusStam = 2;  break;
		 	 	case 4:  Attributes.BonusInt = 2;  break;
		 	 	case 5:  Attributes.BonusMana = 2;  break;
		 	 	case 6:  Attributes.RegenHits = 2;  break;
		 	 	case 7:  Attributes.RegenStam = 2;  break;
		 	 	case 8:  Attributes.RegenMana = 2;  break;
		 	 	case 9:  Attributes.AttackChance = 10;  break;
		 	 	case 10: Attributes.DefendChance = 10;  break;
		 	 	case 11: Attributes.WeaponDamage = 10;  break;
		 	 	case 12: Attributes.WeaponSpeed = 10;  break;
		 	 	case 13: Attributes.Luck = 50;  break;
		 	 	case 14: Attributes.ReflectPhysical = 10;  break;
		 	 	case 15: Attributes.EnhancePotions = 10;  break;
		 	 	case 16: Attributes.SpellDamage = 10;  break;
		 	 	case 17: Attributes.LowerManaCost = 10;  break;
		 	 	case 18: Attributes.LowerRegCost = 10;  break;			  
//		 	 	case 19: Attributes.CastSpeed = 2;  break;
//		 	 	case 20: Attributes.CastRecovery = 2;  break;
			}  
			//END

			//START - Adding bonus - Large
			switch ( Utility.Random( 19 ) )
			{
				case 0:  Attributes.BonusStr = 3;  break;
		 	 	case 1:  Attributes.BonusHits = 3;  break;
		 	 	case 2:  Attributes.BonusDex = 3;  break;
		 	 	case 3:  Attributes.BonusStam = 3;  break;
		 	 	case 4:  Attributes.BonusInt = 3;  break;
		 	 	case 5:  Attributes.BonusMana = 3;  break;
		 	 	case 6:  Attributes.RegenHits = 3;  break;
		 	 	case 7:  Attributes.RegenStam = 3;  break;
		 	 	case 8:  Attributes.RegenMana = 3;  break;
		 	 	case 9:  Attributes.AttackChance = 15;  break;
		 	 	case 10: Attributes.DefendChance = 15;  break;
		 	 	case 11: Attributes.WeaponDamage = 15;  break;
		 	 	case 12: Attributes.WeaponSpeed = 15;  break;
		 	 	case 13: Attributes.Luck = 75;  break;
		 	 	case 14: Attributes.ReflectPhysical = 15;  break;
		 	 	case 15: Attributes.EnhancePotions = 15;  break;
		 	 	case 16: Attributes.SpellDamage = 15;  break;
		 	 	case 17: Attributes.LowerManaCost = 15;  break;
		 	 	case 18: Attributes.LowerRegCost = 15;  break;			  
//		 	 	case 19: Attributes.CastSpeed = 3;  break;
//		 	 	case 20: Attributes.CastRecovery = 3;  break;
			}  
			//END
		}

		public EventRewardLarge( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 1 ); // version
			writer.Write( EventName );

		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

				switch (version)
				{
				  case 1 :
				  {
				    EventName = reader.ReadString();
				    break;
				  }
				}

		}
	}
	
}