using System;
using Server.Items;
using Server.Mobiles;
using Server.Network;

namespace Server.Engines.Craft
{
	public class DefBioengineering : CraftSystem
	{
		public override SkillName MainSkill
		{
			get	{ return SkillName.AnimalTaming; }
		}

		public override string GumpTitleString
		{
			get { return "<BASEFONT COLOR=#FFFFFF><CENTER>Bio-engineering</CENTER></BASEFONT>"; }
		}

		private static CraftSystem m_CraftSystem;

		public static CraftSystem CraftSystem
		{
			get
			{
				if ( m_CraftSystem == null )
					m_CraftSystem = new DefBioengineering();

				return m_CraftSystem;
			}
		}

		public override double GetChanceAtMin( CraftItem item )
		{
			return 0.50; // 50%
		}

		private DefBioengineering() : base( 1, 1, 1.25 )// base( 1, 1, 3.0 )
		{
		}

		public override int CanCraft( Mobile from, BaseTool tool, Type typeItem )
		{
			if ( tool.Deleted || tool.UsesRemaining < 0 )
				return 1044038; // You have worn out your tool!
				
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
				return 1044154; // You create the item.
			}
		}

		public override void InitCraftList()
		{
			int index = -1;

			index = AddCraft( typeof( EmptyDNAVial ), "DNA", "Empty DNA Vial", 100.0, 125.0, typeof( Bottle ), "Bottle", 1 );
			AddSkill( index, SkillName.Tinkering, 90.0, 100.0 );
			AddRes( index, typeof( Organics ), "Organics", 1000 );
			AddRes( index, typeof( IronIngot ), "Iron Ingot", 15 );
			//SetUseAllRes( index, true );

			index = AddCraft( typeof( EmptyDNAVialSet ), "DNA", "Empty DNA Vial Set", 100.0, 125.0, typeof( Bottle ), "Bottle", 3 );
			AddSkill( index, SkillName.Tinkering, 90.0, 100.0 );
			AddRes( index, typeof( Organics ), "Organics", 5000 );
			AddRes( index, typeof( IronIngot ), "Iron Ingot", 15 );
			//SetUseAllRes( index, true ); 
		}
	}
}