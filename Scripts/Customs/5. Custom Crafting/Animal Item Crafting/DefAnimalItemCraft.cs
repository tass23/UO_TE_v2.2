using System;
using Server.Items;

namespace Server.Engines.Craft
{
	public class DefAnimalItemCrafting : CraftSystem
	{
		public override SkillName MainSkill
		{
			get	{ return SkillName.AnimalTaming; }
		}

		public override int GumpTitleNumber
		{
			get { return 1044007; } // <CENTER>Animal Item Crafting</CENTER>
		}

		private static CraftSystem m_CraftSystem;

		public static CraftSystem CraftSystem
		{
			get
			{
				if ( m_CraftSystem == null )
					m_CraftSystem = new DefAnimalItemCrafting();

				return m_CraftSystem;
			}
		}

		private DefAnimalItemCrafting() : base( 1, 1, 1.25 )// base( 1, 1, 3.0 )
		{
		}

		public override double GetChanceAtMin( CraftItem item )
		{
			if ( item.NameNumber == 1044258 ) // potion keg
				return 0.5; // 50%

			return 0.0; // 0%
		}

		public override int CanCraft( Mobile from, BaseTool tool, Type itemType )
		{
			if ( tool.Deleted || tool.UsesRemaining < 0 )
				return 1044038; // You have worn out your tool!
			else if ( !BaseTool.CheckAccessible( tool, from ) )
				return 1044263; // The tool must be on your person to use.

			return 0;
		}

		public override void PlayCraftEffect( Mobile from )
		{
			// no sound
			//from.PlaySound( 0x241 );
		}

		public override int PlayEndingEffect( Mobile from, bool failed, bool lostMaterial, bool toolBroken, int quality, bool makersMark, CraftItem item )
		{
			if ( toolBroken )
				from.SendLocalizedMessage( 1044038 ); // You have worn out your tool

			if ( failed )
			{
				if ( lostMaterial )
					return 1044043; // You failed to create the item, and some of your materials are lost.
				else
					return 1044157; // You failed to create the item, but no materials were lost.
			}
			else
			{
				if ( quality == 0 )
					return 502785; // You were barely able to make this item.  It's quality is below average.
				else if ( makersMark && quality == 2 )
					return 1044156; // You create an exceptional quality item and affix your maker's mark.
				else if ( quality == 2 )
					return 1044155; // You create an exceptional quality item.
				else				
					return 1044154; // You create the item.
			}
		}

		public override void InitCraftList()
		{
      int index = -1;

			// Items
			AddCraft(typeof(HorseShoe), "Items","Horse Shoes", 0.0, 50.4, typeof(IronIngot), "IronIngot", 10);
			AddCraft( typeof( TrainingDragonDeed ), "Items","a Training Dragon Deed", 118.0, 125.0, typeof( Log ), 1044041, 60 , 1044351 );
			AddCraft(typeof(HorseSaddle1), "Items","Horse Saddle 1", 119.0, 126.4, typeof( Log ), 1044041, 20 , 1044351 );
			AddCraft(typeof(HorseSaddle2), "Items","Horse Saddle 2", 119.0, 126.4, typeof( Log ), 1044041, 20 , 1044351 );
			AddCraft( typeof( Hay1 ), "Items","Hay", 124.0, 135.0, typeof( Log ), 1044041, 10 , 1044351 );
			
			// Statuette
			AddCraft(typeof(CraftableEtherealHorse), "Statuette", "a craftable ethereal Horse", 50.0, 65.4, typeof(IronIngot), "IronIngot", 25);
			AddCraft(typeof(CraftableEtherealLlama), "Statuette", "a craftable ethereal Llama", 60.0, 70.4, typeof(IronIngot), "IronIngot", 25);
			AddCraft(typeof(CraftableEtherealOstard), "Statuette", "a craftable ethereal Ostard", 68.0, 80.4, typeof(IronIngot), "IronIngot", 25);
			AddCraft(typeof(CraftableEtherealRidgeback), "Statuette", "a craftable ethereal Ridgeback", 78.0, 90.4, typeof(IronIngot), "IronIngot", 50);
			AddCraft(typeof(CraftableEtherealUnicorn), "Statuette", "a craftable ethereal Unicorn", 89.0, 100.4, typeof(IronIngot), "IronIngot", 50);
			AddCraft(typeof(CraftableEtherealBeetle), "Statuette", "a craftable ethereal Beetle", 98.0, 105.4, typeof(IronIngot), "IronIngot", 65);
			AddCraft(typeof(CraftableEtherealKirin), "Statuette", "a craftable ethereal Kirin", 104.0, 110.4, typeof(IronIngot), "IronIngot", 65);
			AddCraft(typeof(CraftableEtherealSwampDragon), "Statuette", "a craftable ethereal Swamp Dragon", 109.0, 120.4, typeof(IronIngot), "IronIngot", 65);
			
			// Deeds
			AddCraft(typeof(EtherealHorseDeed), "Deeds", "Ethereal Horse Deed", 120.0, 135.4, typeof(IronIngot), "IronIngot", 100);
			AddCraft(typeof(EtherealLlamaDeed), "Deeds", "Ethereal Llama Deed", 130.0, 145.4, typeof(IronIngot), "IronIngot", 100);
			AddCraft(typeof(EtherealOstardDeed), "Deeds", "Ethereal Ostard Deed", 140.0, 155.4, typeof(IronIngot), "IronIngot", 100);
			AddCraft(typeof(EtherealRidgebackDeed), "Deeds", "Ethereal Ridgeback Deed", 150.0, 168.4, typeof(IronIngot), "IronIngot", 100);
			AddCraft(typeof(EtherealUnicornDeed), "Deeds", "Ethereal Unicorn Deed", 165.0, 175.4, typeof(IronIngot), "IronIngot", 100);
			AddCraft(typeof(EtherealKirinDeed), "Deeds", "Ethereal Kirin Deed", 170.0, 180.4, typeof(IronIngot), "IronIngot", 100);
			AddCraft(typeof(EtherealBeetleDeed), "Deeds", "Ethereal Beetle Deed", 178.0, 188.4, typeof(IronIngot), "IronIngot", 100);
			AddCraft(typeof(EtherealSwampDragonDeed), "Deeds", "Ethereal Swamp Dragon Deed", 185.0, 200.4, typeof(IronIngot), "IronIngot", 100);
			
			// Tools
			AddCraft(typeof(AnimalItemCraftingTools), "Animal Item Tool", "Animal Item Tool", 50.0, 65.4, typeof(IronIngot), "IronIngot", 40);
			

			// Set the overidable material
			SetSubRes( typeof( IronIngot ), 1044022 );

			// Add every material you want the player to be able to chose from
			// This will overide the overidable material
			AddSubRes( typeof( IronIngot ),			1044022, 00.0, 1044036, 1044267 );
			AddSubRes( typeof( DullCopperIngot ),	1044023, 65.0, 1044036, 1044268 );
			AddSubRes( typeof( ShadowIronIngot ),	1044024, 70.0, 1044036, 1044268 );
			AddSubRes( typeof( CopperIngot ),		1044025, 75.0, 1044036, 1044268 );
			AddSubRes( typeof( BronzeIngot ),		1044026, 80.0, 1044036, 1044268 );
			AddSubRes( typeof( GoldIngot ),			1044027, 85.0, 1044036, 1044268 );
			AddSubRes( typeof( AgapiteIngot ),		1044028, 90.0, 1044036, 1044268 );
			AddSubRes( typeof( VeriteIngot ),		1044029, 95.0, 1044036, 1044268 );
			AddSubRes( typeof( ValoriteIngot ),		1044030, 99.0, 1044036, 1044268 );

			MarkOption = true;
			Repair = true;
		}
	}
}