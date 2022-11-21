
using System;
using Server;
using Server.Items;
using Server.Mobiles;

namespace Server.Engines.Craft
{
	public class DefPainting : CraftSystem
	{
		public override SkillName MainSkill
		{
			get	{ return SkillName.Inscribe;	}
		}

		public override int GumpTitleNumber
		{
			get { return 0; } // Use String
		}

		public override string GumpTitleString
		{
			get { return "<basefont color=#FFFFFF><CENTER>FRAMING ASSEMBLY MENU</CENTER></basefont>"; } 
		}

		private static CraftSystem m_CraftSystem;

		public static CraftSystem CraftSystem
		{
			get
			{
				if ( m_CraftSystem == null )
					m_CraftSystem = new DefPainting();

				return m_CraftSystem;
			}
		}

		public override double GetChanceAtMin( CraftItem item )
		{
			return 0.5; // 50%
		}

		private DefPainting() : base( 1, 1, 1.25 )// base( 1, 1, 1.5 )
		{
		}

		public override bool RetainsColorFrom( CraftItem item, Type type )
		{
			return false;
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
			from.PlaySound( 0x249 );
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

			// Small Portraits //
			index = AddCraft( typeof( ManPortraitSouth ), "Small Portraits", "Man Portrait (south)", 65.0, 110.0, typeof( Canvas ), "Canvas", 1, "You need a canvas." );
			AddRes(index, typeof(Dyes), "Dyes", 2, "You don't have enough Dyes.");
			index = AddCraft( typeof( ManPortraitEast ), "Small Portraits", "Man Portrait (east)", 65.0, 110.0, typeof( Canvas ), "Canvas", 1, "You need a canvas." );
			AddRes(index, typeof(Dyes), "Dyes", 2, "You don't have enough Dyes.");
			index = AddCraft( typeof( WomanPortraitSouth ), "Small Portraits", "Woman Portrait (south)", 65.0, 110.0, typeof( Canvas ), "Canvas", 1, "You need a canvas." );
			AddRes(index, typeof(Dyes), "Dyes", 2, "You don't have enough Dyes.");
			index = AddCraft( typeof( WomanPortraitEast ), "Small Portraits", "Woman Portrait (east)", 65.0, 110.0, typeof( Canvas ), "Canvas", 1, "You need a canvas." );
			AddRes(index, typeof(Dyes), "Dyes", 2, "You don't have enough Dyes.");
			index = AddCraft( typeof( WomanPortraitSouth2 ), "Small Portraits", "Woman Portrait (south)", 65.0, 110.0, typeof( Canvas ), "Canvas", 1, "You need a canvas." );
			AddRes(index, typeof(Dyes), "Dyes", 2, "You don't have enough Dyes.");
			index = AddCraft( typeof( WomanPortraitEast2 ), "Small Portraits", "Woman Portrait (east)", 65.0, 110.0, typeof( Canvas ), "Canvas", 1, "You need a canvas." );
			AddRes(index, typeof(Dyes), "Dyes", 2, "You don't have enough Dyes.");
			index = AddCraft( typeof( WomanBluePortrait ), "Small Portraits", "Woman in Blue", 65.0, 110.0, typeof( Canvas ), "Canvas", 1, "You need a canvas." );
			AddRes(index, typeof(Dyes), "Dyes", 2, "You don't have enough Dyes.");
			index = AddCraft( typeof( WomanRedPortrait ), "Small Portraits", "Woman in Red", 65.0, 110.0, typeof( Canvas ), "Canvas", 1, "You need a canvas." );
			AddRes(index, typeof(Dyes), "Dyes", 2, "You don't have enough Dyes.");
			index = AddCraft( typeof( WomanGreenPortrait ), "Small Portraits", "Woman in Green", 65.0, 110.0, typeof( Canvas ), "Canvas", 1, "You need a canvas." );
			AddRes(index, typeof(Dyes), "Dyes", 2, "You don't have enough Dyes.");

			// Large Portraits //
			index = AddCraft( typeof( LargeWomanPortrait ), "Large Portraits", "Lady's Portrait", 80.0, 120.0, typeof( Canvas ), "Canvas", 1, "You need a canvas." );
			AddRes(index, typeof(Dyes), "Dyes", 5, "You don't have enough Dyes.");

			// Eastern Paintings //
			index = AddCraft( typeof( RedPaintingSouth ), "Eastern Paintings", "Red Painting (south)", 70.0, 110.0, typeof( Canvas ), "Canvas", 1, "You need a canvas." );
			AddRes(index, typeof(Dyes), "Dyes", 3, "You don't have enough Dyes.");
			index = AddCraft( typeof( RedPaintingEast ), "Eastern Paintings", "Red Painting (east)", 70.0, 110.0, typeof( Canvas ), "Canvas", 1, "You need a canvas." );
			AddRes(index, typeof(Dyes), "Dyes", 3, "You don't have enough Dyes.");
			index = AddCraft( typeof( TanPaintingSouth ), "Eastern Paintings", "Tan Painting (south)", 70.0, 110.0, typeof( Canvas ), "Canvas", 1, "You need a canvas." );
			AddRes(index, typeof(Dyes), "Dyes", 3, "You don't have enough Dyes.");
			index = AddCraft( typeof( TanPaintingEast ), "Eastern Paintings", "Tan Painting (east)", 70.0, 110.0, typeof( Canvas ), "Canvas", 1, "You need a canvas." );
			AddRes(index, typeof(Dyes), "Dyes", 3, "You don't have enough Dyes.");
			index = AddCraft( typeof( MountPaintingSouth ), "Eastern Paintings", "Mountain Painting (south)", 75.0, 115.0, typeof( Canvas ), "Canvas", 1, "You need a canvas." );
			AddRes(index, typeof(Dyes), "Dyes", 3, "You don't have enough Dyes.");
			index = AddCraft( typeof( MountPaintingEast ), "Eastern Paintings", "Mountain Painting (east)", 75.0, 115.0, typeof( Canvas ), "Canvas", 1, "You need a canvas." );
			AddRes(index, typeof(Dyes), "Dyes", 3, "You don't have enough Dyes.");
			index = AddCraft( typeof( WarriorPaintingSouth ), "Eastern Paintings", "Warrior Painting (south)", 75.0, 115.0, typeof( Canvas ), "Canvas", 1, "You need a canvas." );
			AddRes(index, typeof(Dyes), "Dyes", 3, "You don't have enough Dyes.");
			index = AddCraft( typeof( WarriorPaintingEast ), "Eastern Paintings", "Warrior Painting (east)", 75.0, 115.0, typeof( Canvas ), "Canvas", 1, "You need a canvas." );
			AddRes(index, typeof(Dyes), "Dyes", 3, "You don't have enough Dyes.");
			index = AddCraft( typeof( EasternPaintingSouth1 ), "Eastern Paintings", "Eastern Painting (south)", 75.0, 115.0, typeof( Canvas ), "Canvas", 1, "You need a canvas." );
			AddRes(index, typeof(Dyes), "Dyes", 3, "You don't have enough Dyes.");
			index = AddCraft( typeof( EasternPaintingEast1 ), "Eastern Paintings", "Eastern Painting (east)", 75.0, 115.0, typeof( Canvas ), "Canvas", 1, "You need a canvas." );
			AddRes(index, typeof(Dyes), "Dyes", 3, "You don't have enough Dyes.");
			index = AddCraft( typeof( EasternPaintingSouth2 ), "Eastern Paintings", "Eastern Painting (south)", 75.0, 115.0, typeof( Canvas ), "Canvas", 1, "You need a canvas." );
			AddRes(index, typeof(Dyes), "Dyes", 3, "You don't have enough Dyes.");
			index = AddCraft( typeof( EasternPaintingEast2 ), "Eastern Paintings", "Eastern Painting (east)", 75.0, 115.0, typeof( Canvas ), "Canvas", 1, "You need a canvas." );
			AddRes(index, typeof(Dyes), "Dyes", 3, "You don't have enough Dyes.");

			// Fancy Portraits //
			index = AddCraft( typeof( SmallFancyPortrait ), "Fancy Portraits", "Small Fancy Portrait", 70.0, 115.0, typeof( Canvas ), "Canvas", 1, "You need a canvas." );
			AddRes(index, typeof(Dyes), "Dyes", 4, "You don't have enough Dyes.");
			index = AddCraft( typeof( FancyPortraitSouth ), "Fancy Portraits", "Fancy Portrait (south)", 80.0, 120.0, typeof( Canvas ), "Canvas", 1, "You need a canvas." );
			AddRes(index, typeof(Dyes), "Dyes", 4, "You don't have enough Dyes.");
			index = AddCraft( typeof( FancyPortraitEast ), "Fancy Portraits", "Fancy Portrait (east)", 80.0, 120.0, typeof( Canvas ), "Canvas", 1, "You need a canvas." );
			AddRes(index, typeof(Dyes), "Dyes", 4, "You don't have enough Dyes.");
			index = AddCraft( typeof( FancyLadyPortraitSouth1 ), "Fancy Portraits", "Lady's Portrait (south)", 80.0, 120.0, typeof( Canvas ), "Canvas", 1, "You need a canvas." );
			AddRes(index, typeof(Dyes), "Dyes", 4, "You don't have enough Dyes.");
			index = AddCraft( typeof( FancyLadyPortraitEast1 ), "Fancy Portraits", "Lady's Portrait (east)", 80.0, 120.0, typeof( Canvas ), "Canvas", 1, "You need a canvas." );
			AddRes(index, typeof(Dyes), "Dyes", 4, "You don't have enough Dyes.");
			index = AddCraft( typeof( FancyLadyPortraitSouth2 ), "Fancy Portraits", "Lady's Portrait (south)", 80.0, 120.0, typeof( Canvas ), "Canvas", 1, "You need a canvas." );
			AddRes(index, typeof(Dyes), "Dyes", 4, "You don't have enough Dyes.");
			index = AddCraft( typeof( FancyLadyPortraitEast2 ), "Fancy Portraits", "Lady's Portrait (east)", 80.0, 120.0, typeof( Canvas ), "Canvas", 1, "You need a canvas." );
			AddRes(index, typeof(Dyes), "Dyes", 4, "You don't have enough Dyes.");
			index = AddCraft( typeof( YoungManPortraitSouth ), "Fancy Portraits", "Young Man's Portrait (south)", 80.0, 120.0, typeof( Canvas ), "Canvas", 1, "You need a canvas." );
			AddRes(index, typeof(Dyes), "Dyes", 4, "You don't have enough Dyes.");
			index = AddCraft( typeof( YoungManPortraitEast ), "Fancy Portraits", "Young Man's Portrait (east)", 80.0, 120.0, typeof( Canvas ), "Canvas", 1, "You need a canvas." );
			AddRes(index, typeof(Dyes), "Dyes", 4, "You don't have enough Dyes.");
			
			// Supplies //
			index = AddCraft( typeof( Canvas ), "Supplies", "Canvas", 65.0, 90.0, typeof(Cloth), 1044286, 5, 1044287);
			
			index = AddCraft( typeof( Dyes ), "Supplies", "Dyes", 50.0, 80.0, typeof( FireFeather ), "Fire Feather", 1, "You don't have enough fire feathers." );
			AddRes( index, typeof( WaterFeather ), "Water Feather", 1, "You don't have enough water feathers." );
			AddRes( index, typeof( PoisonFeather ), "Poison Feather", 1, "You don't have enough poison feathers." );

			MarkOption = true;
		}
	}
}