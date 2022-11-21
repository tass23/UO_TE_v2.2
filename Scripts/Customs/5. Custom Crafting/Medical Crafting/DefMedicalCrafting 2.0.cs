using System;
using Server.Items;

namespace Server.Engines.Craft
{
	public class DefMedicalCrafting : CraftSystem
	{
		public override SkillName MainSkill
		{
			get	{ return SkillName.Healing;	}
		}

		public override string GumpTitleString
		{
			get { return "<basefont color=#FFFFFF><CENTER>MEDICAL MALPRACTICE MENU</CENTER></basefont>"; } // <CENTER>Medical Practice MENU</CENTER>
		}

		private static CraftSystem m_CraftSystem;

		public static CraftSystem CraftSystem
		{
			get
			{
				if ( m_CraftSystem == null )
					m_CraftSystem = new DefMedicalCrafting();

				return m_CraftSystem;
			}
		}

		public override CraftECA ECA{ get{ return CraftECA.ChanceMinusSixtyToFourtyFive; } }

		public override double GetChanceAtMin( CraftItem item )
		{
			return 0.0; // 0%
		}

		private DefMedicalCrafting() : base( 1, 1, 1.25 )// base( 1, 1, 1.5 )
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
			// Books
      
			index = AddCraft( typeof( MedicalRedBook ), "Books", "Medical Red Book", 0.0, 0.4, typeof( Cloth ), "Cloth", 1 );
				AddRes( index, typeof ( ScribesPen ), "Scribe's Pen", 1 );
			
			index = AddCraft( typeof( MedicalTanBook ), "Books", "Medical Tan Book", 10.0, 10.4, typeof( Cloth ), "Cloth", 1 );
				AddRes( index, typeof ( ScribesPen ), "Scribe's Pen", 1 );
			
			index = AddCraft( typeof( MedicalBrownBook ), "Books", "Medical Brown Book", 25.0, 25.4, typeof( Cloth ), "Cloth", 1 );
				AddRes( index, typeof ( ScribesPen ), "Scribe's Pen", 1 );
      
			index = AddCraft( typeof( MedicalBlueBook ), "Books", "Medical Blue Book", 40.0, 40.4, typeof( Cloth ), "Cloth", 1 );
				AddRes( index, typeof ( ScribesPen ), "Scribe's Pen", 1 );
      
			index = AddCraft( typeof( MedicalTreatmentsBook ), "Books", "Medical Treatments Book", 100.0, 110.4, typeof( Cloth ), "Cloth", 1 );
				AddRes( index, typeof ( ScribesPen ), "Scribe's Pen", 1 );
      
			// Medical Books
			
			index = AddCraft( typeof( MedicalBook1 ), "Medical Books", "Medical Book 1", 0.0, 10.4, typeof( MedicalRedBook ), "Medical Red Book", 1 );
				AddRes( index, typeof ( ScribesPen ), "Scribe's Pen", 1 );
			
			index = AddCraft( typeof( MedicalBook2 ), "Medical Books", "Medical Book 2", 10.0, 25.4, typeof( MedicalTanBook ), "Medical Tan Book", 1 );
				AddRes( index, typeof ( ScribesPen ), "Scribe's Pen", 1 );
			
			index = AddCraft( typeof( MedicalBook3 ), "Medical Books", "Medical Book 3", 25.0, 40.4, typeof( MedicalBrownBook ), "Medical Brown Book", 1 );
				AddRes( index, typeof ( ScribesPen ), "Scribe's Pen", 1 );
			
			index = AddCraft( typeof( MedicalBook4 ), "Medical Books", "Medical Book 4", 40.0, 60.4, typeof( MedicalBlueBook ), "Medical Blue Book", 1 );
				AddRes( index, typeof ( ScribesPen ), "Scribe's Pen", 1 );
			
			index = AddCraft( typeof( CertifiedDoctorInTheArtOfHealing ), "Medical Books", "Certified Doctor", 119.9, 120.0, typeof( MedicalBlueBook ), "Medical Blue Book", 5 );
				AddRes( index, typeof ( ScribesPen ), "Scribe's Pen", 5 );
      
			// Medical Tools
      
			index = AddCraft( typeof( MedicalTools ), "Medical Tools", "Medical Tool Kit", 60.0, 62.4, typeof( IronIngot ), "Iron Ingot", 5 );
				AddRes( index, typeof ( Cloth ), "Cloth", 2 );
						
			// Pain Killers
			
			index = AddCraft( typeof( Aspirin ), "Pain Killers", "Aspirin", 50.0, 60.0, typeof( GreaterHealPotion ), "Greater Heal Potion", 1 );
				AddRes( index, typeof ( GreaterStrengthPotion ), "Greater Strength Potion", 1 );	
			
			index = AddCraft( typeof( Demerol ), "Pain Killers", "Demerol", 60.0, 70.0, typeof( GreaterHealPotion ), "Greater Heal Potion", 1 );
				AddRes( index, typeof ( GreaterStrengthPotion ), "Greater Strength Potion", 2 );
				AddRes( index, typeof ( GreaterAgilityPotion ), "Greater Agility Potion", 1 );	
			
			index = AddCraft( typeof( Vicodin ), "Pain Killers", "Vicodin", 70.0, 80.0, typeof( GreaterHealPotion ), "Greater Heal Potion", 2 );
				AddRes( index, typeof ( GreaterStrengthPotion ), "Greater Strength Potion", 2 );
				AddRes( index, typeof ( TotalRefreshPotion ), "Total Refresh Potion", 1 );
				AddRes( index, typeof ( Ginseng ), "Ginseng", 2 );	
			
			index = AddCraft( typeof( Oxycontin ), "Pain Killers", "Oxycontin", 80.0, 90.0, typeof( GreaterHealPotion ), "Greater Heal Potion", 2 );
				AddRes( index, typeof ( GreaterStrengthPotion ), "Greater Strength Potion", 1 );
				AddRes( index, typeof ( Ginseng ), "Ginseng", 3 );
				AddRes( index, typeof ( MandrakeRoot ), "MandrakeRoot", 2 );	
			
			index = AddCraft( typeof( Morphine ), "Pain Killers", "Morphine", 90.0, 100.0, typeof( GreaterHealPotion ), "Greater Heal Potion", 3 );
				AddRes( index, typeof ( GreaterStrengthPotion ), "Greater Strength Potion", 3 );
				AddRes( index, typeof ( Ginseng ), "Ginseng", 3 );
				AddRes( index, typeof ( MandrakeRoot ), "MandrakeRoot", 2 );
			
			// Prescriptions
			
			index = AddCraft( typeof( PrescriptionDemerol ), "Prescriptions", "Prescription for Demerol", 100.0, 105.4, typeof( MedicalTreatmentsBook ), "Medical Treatments Book", 3 );
				AddRes( index, typeof ( ScribesPen ), "Scribe's Pen", 1 );	
			
			index = AddCraft( typeof( PrescriptionVicodin ), "Prescriptions", "Prescription for Vicodin", 110.0, 115.4, typeof( MedicalTreatmentsBook ), "Medical Treatments Book", 3 );
				AddRes( index, typeof ( ScribesPen ), "Scribe's Pen", 1 );	
			
			index = AddCraft( typeof( PrescriptionOxycontin ), "Prescriptions", "Prescription for Oxycontin", 118.0, 119.4, typeof( MedicalTreatmentsBook ), "Medical Treatments Book", 5 );
				AddRes( index, typeof ( ScribesPen ), "Scribe's Pen", 1 );	
			
			index = AddCraft( typeof( PrescriptionMorphine ), "Prescriptions", "Prescription for Morphine", 120.0, 120.0, typeof( MedicalTreatmentsBook ), "Medical Treatments Book", 5 );
				AddRes( index, typeof ( ScribesPen ), "Scribe's Pen", 1 );	
			
			// Clothing
			
			index = AddCraft( typeof( DoctorsLabCoat ), "Clothing", "Doctor's Lab Coat", 119.0, 120.0, typeof( MedicalBlueBook ), "Medical Blue Book", 1 );
				AddRes( index, typeof ( ScribesPen ), "Scribe's Pen", 1 );
				AddRes( index, typeof ( Cloth ), "Cloth", 10 );
				AddRes( index, typeof ( GreaterHealPotion ), "Greater Heal Potion", 1 );
      
			MarkOption = false;
			Repair = Core.AOS;
		}
	}
}