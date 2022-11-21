using System;
using Server.Items;

namespace Server.Engines.Craft
{

	public class DefWeaving : CraftSystem
	{
		public override SkillName MainSkill
		{
			get	{ return SkillName.Tailoring; }
		}

		private static CraftSystem m_CraftSystem;

		public static CraftSystem CraftSystem
		{
			get
			{
				if ( m_CraftSystem == null )
					m_CraftSystem = new DefWeaving();

				return m_CraftSystem;
			}
		}

		public override CraftECA ECA{ get{ return CraftECA.ChanceMinusSixtyToFourtyFive; } }

		public override double GetChanceAtMin( CraftItem item )
		{
			return 0.5; // 50%
		}

		private DefWeaving() : base( 1, 1, 1.25 )// base( 1, 1, 4.5 )
		{
		}

		public override int CanCraft( Mobile from, BaseTool tool, Type itemType )
		{
			if( tool == null || tool.Deleted || tool.UsesRemaining < 0 )
				return 1044038; // You have worn out your tool!
			else if ( !BaseTool.CheckAccessible( tool, from ) )
				return 1044263; // The tool must be on your person to use.

			return 0;
		}

		public override void PlayCraftEffect( Mobile from )
		{
			from.PlaySound( 0x248 );
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

			#region Rugs
			index = AddCraft( typeof( BlueRug2Deed ), 1076602, "Blue rug", 105.0, 125.0, typeof( Wool ), "Wool", 24, "You don't have enough wool to make that" );
			AddRes( index, typeof( Dyes ), 1024009, 6, 1044253 );
			AddRes( index, typeof( Flax ), 1026809, 1, 1044253 );
			
			index = AddCraft( typeof( BluePlainRug2Deed ), 1076602, 1076585, 107.0, 127.0, typeof( Wool ), "Wool", 24, "You don't have enough wool to make that" );
			AddRes( index, typeof( Dyes ), 1024009, 6, 1044253 );
			AddRes( index, typeof( Flax ), 1026809, 1, 1044253 );
			
			index = AddCraft( typeof( BlueFancyRug2Deed ), 1076602, 1076273, 110.2, 130.0, typeof( Wool ), "Wool", 24, "You don't have enough wool to make that" );
			AddRes( index, typeof( Dyes ), 1024009, 6, 1044253 );
			AddRes( index, typeof( Flax ), 1026809, 1, 1044253 );
			
			index = AddCraft( typeof( BlueDecorativeRug2Deed ), 1076602, 1076589, 115.0, 130.5, typeof( Wool ), "Wool", 24, "You don't have enough wool to make that" );
			AddRes( index, typeof( Dyes ), 1024009, 6, 1044253 );
			AddRes( index, typeof( Flax ), 1026809, 1, 1044253 );
			
			index = AddCraft( typeof( RedRug2Deed ), 1076602, "Red rug", 105.0, 125.0, typeof( Wool ), "Wool", 24, "You don't have enough wool to make that" );
			AddRes( index, typeof( Dyes ), 1024009, 6, 1044253 );
			AddRes( index, typeof( Flax ), 1026809, 1, 1044253 );
			
			index = AddCraft( typeof( RedPlainRug2Deed ), 1076602, 1076588, 107.0, 127.0, typeof( Wool ), "Wool", 24, "You don't have enough wool to make that" );
			AddRes( index, typeof( Dyes ), 1024009, 6, 1044253 );
			AddRes( index, typeof( Flax ), 1026809, 1, 1044253 );
			
			index = AddCraft( typeof( CinnamonRug2Deed ), 1076602, "Large cinnamon rug", 105.0, 125.0, typeof( Wool ), "Wool", 24, "You don't have enough wool to make that" );
			AddRes( index, typeof( Dyes ), 1024009, 6, 1044253 );
			AddRes( index, typeof( Flax ), 1026809, 1, 1044253 );
			
			index = AddCraft( typeof( CinnamonFancyRug4Deed ), 1076602, 1076587, 110.2, 130.0, typeof( Wool ), "Wool", 24, "You don't have enough wool to make that" );
			AddRes( index, typeof( Dyes ), 1024009, 6, 1044253 );
			AddRes( index, typeof( Flax ), 1026809, 1, 1044253 );
			
			index = AddCraft( typeof( CinnamonFancyRug2Deed ), 1076602, "Cinnamon decorative rug", 115.0, 130.5, typeof( Wool ), "Wool", 24, "You don't have enough wool to make that" );
			AddRes( index, typeof( Dyes ), 1024009, 6, 1044253 );
			AddRes( index, typeof( Flax ), 1026809, 1, 1044253 );
			
			index = AddCraft( typeof( CinnamonFancyRug3Deed ), 1076602, "Cinnamon artisan rug", 115.0, 130.5, typeof( Wool ), "Wool", 24, "You don't have enough wool to make that" );
			AddRes( index, typeof( Dyes ), 1024009, 6, 1044253 );
			AddRes( index, typeof( Flax ), 1026809, 1, 1044253 );
			
			index = AddCraft( typeof( GoldenRug2Deed ), 1076602, "Large golden rug", 105.0, 125.0, typeof( Wool ), "Wool", 24, "You don't have enough wool to make that" );
			AddRes( index, typeof( Dyes ), 1024009, 6, 1044253 );
			AddRes( index, typeof( Flax ), 1026809, 1, 1044253 );
			
			index = AddCraft( typeof( GoldenDecorativeRug2Deed ), 1076602, 1076586, 115.0, 130.5, typeof( Wool ), "Wool", 24, "You don't have enough wool to make that" );
			AddRes( index, typeof( Dyes ), 1024009, 6, 1044253 );
			AddRes( index, typeof( Flax ), 1026809, 1, 1044253 );

			#endregion

			#region Misc.
			index = AddCraft( typeof( BlueRunnerNSDeed ), 3001016, "Blue runner N/S", 105.0, 125.0, typeof( Wool ), "Wool", 12, "You don't have enough wool to make that" );
			AddRes( index, typeof( Dyes ), 1024009, 4, 1044253 );
			AddRes( index, typeof( Flax ), 1026809, 1, 1044253 );
			
			index = AddCraft( typeof( BlueRunnerEWDeed ), 3001016, "Blue runner E/W", 105.0, 125.0, typeof( Wool ), "Wool", 12, "You don't have enough wool to make that" );
			AddRes( index, typeof( Dyes ), 1024009, 4, 1044253 );
			AddRes( index, typeof( Flax ), 1026809, 1, 1044253 );
			
			index = AddCraft( typeof( RedRunnerNSDeed ), 3001016, "Red runner N/S", 105.0, 125.0, typeof( Wool ), "Wool", 12, "You don't have enough wool to make that" );
			AddRes( index, typeof( Dyes ), 1024009, 4, 1044253 );
			AddRes( index, typeof( Flax ), 1026809, 1, 1044253 );
			
			index = AddCraft( typeof( RedRunnerEWDeed ), 3001016, "Red runner E/W", 105.0, 125.0, typeof( Wool ), "Wool", 12, "You don't have enough wool to make that" );
			AddRes( index, typeof( Dyes ), 1024009, 4, 1044253 );
			AddRes( index, typeof( Flax ), 1026809, 1, 1044253 );
			
			index = AddCraft( typeof( GoldenRunnerNSDeed ), 3001016, "Golden runner N/S", 105.0, 125.0, typeof( Wool ), "Wool", 12, "You don't have enough wool to make that" );
			AddRes( index, typeof( Dyes ), 1024009, 4, 1044253 );
			AddRes( index, typeof( Flax ), 1026809, 1, 1044253 );
			
			index = AddCraft( typeof( GoldenRunnerEWDeed ), 3001016, "Golden runner E/W", 105.0, 125.0, typeof( Wool ), "Wool", 12, "You don't have enough wool to make that" );
			AddRes( index, typeof( Dyes ), 1024009, 4, 1044253 );
			AddRes( index, typeof( Flax ), 1026809, 1, 1044253 );
			
			index = AddCraft( typeof( CinnamonRunnerNSDeed ), 3001016, "Cinnamon runner N/S", 105.0, 125.0, typeof( Wool ), "Wool", 12, "You don't have enough wool to make that" );
			AddRes( index, typeof( Dyes ), 1024009, 4, 1044253 );
			AddRes( index, typeof( Flax ), 1026809, 1, 1044253 );
			
			index = AddCraft( typeof( CinnamonRunnerEWDeed ), 3001016, "Cinnamon runner E/W", 105.0, 125.0, typeof( Wool ), "Wool", 12, "You don't have enough wool to make that" );
			AddRes( index, typeof( Dyes ), 1024009, 4, 1044253 );
			AddRes( index, typeof( Flax ), 1026809, 1, 1044253 );
	
			#endregion
			
			MarkOption = true;
			
		}
	}
}
