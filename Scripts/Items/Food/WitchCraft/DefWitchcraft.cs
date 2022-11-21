using System;
using Server.Items;

namespace Server.Engines.Craft
{
	public class DefWitchcraft : CraftSystem
	{
		public override SkillName MainSkill { get { return SkillName.Cooking; } }

		public override int GumpTitleNumber { get { return 0; } }

		public override string GumpTitleString { get { return "<basefont color=#FFFFFF><CENTER>WITCHCRAFT MENU</CENTER></basefont>"; }  }

		private static CraftSystem m_CraftSystem;

		public static CraftSystem CraftSystem { get { if ( m_CraftSystem == null ) m_CraftSystem = new DefWitchcraft(); return m_CraftSystem; } }

		public override CraftECA ECA{ get{ return CraftECA.ChanceMinusSixtyToFourtyFive; } }

		public override double GetChanceAtMin( CraftItem item ) { return 0.0; }

		private DefWitchcraft() : base( 1, 1, 1.25 ){ }

		public override int CanCraft( Mobile from, BaseTool tool, Type itemType )
		{
			if ( tool.Deleted || tool.UsesRemaining < 0 ) return 1044038;
			else if ( !BaseTool.CheckAccessible( tool, from ) ) return 1044263;
			return 0;
		}

		public override void PlayCraftEffect( Mobile from ) { from.PlaySound( 0x21 ); }

		public override int PlayEndingEffect( Mobile from, bool failed, bool lostMaterial, bool toolBroken, int quality, bool makersMark, CraftItem item )
		{
			if ( toolBroken ) from.SendLocalizedMessage( 1044038 );

			if ( failed )
			{
				if ( lostMaterial ) return 1044043;
				else return 1044157;
			}
			else
			{
				if ( quality == 0 ) return 502785;
				else if ( makersMark && quality == 2 ) return 1044156;
				else if ( quality == 2 ) return 1044155;
				else return 1044154;
			}
		}

		public override void InitCraftList()
		{
			int index = -1;

			index =  AddCraft( typeof( AlmondWrestling ), "Skill Foods", "Almond of Wrestling", 100.0, 120.0, typeof( Almond ), "Almond", 1, "You don't have enough Almond." );
			AddSkill( index, SkillName.Alchemy, 100.0, 120.0 );
			AddRes( index, typeof( MagicalJasper ), "Magical Jasper", 1, " need more Magical Jasper" );
			AddRes( index, typeof( Acacia ), "Acacia", 1, " need more Acacia" );
			AddRes( index, typeof( Myrrh ), "Myrrh", 1, " need more Myrrh" );
			AddRes( index, typeof( Chamomile ), "Chamomile", 1, " need more Chamomile" );
			SetNeedOven( index, true );
			index =  AddCraft( typeof( AppleVeterinary ), "Skill Foods", "Apple of Veterinary", 100.0, 120.0, typeof( Apple ), "Apple", 1, "You don't have enough Apple." );
			AddSkill( index, SkillName.Alchemy, 100.0, 120.0 );
			AddRes( index, typeof( MagicalJade ), "Magical Jade", 1, " need more Magical Jade" );
			AddRes( index, typeof( Anise ), "Anise", 1, " need more Anise" );
			AddRes( index, typeof( Olive ), "Olive", 1, " need more Olive" );
			AddRes( index, typeof( Cilantro ), "Cilantro", 1, " need more Cilantro" );
			SetNeedOven( index, true );
			index =  AddCraft( typeof( ApricotTracking ), "Skill Foods", "Apricot of Tracking", 100.0, 120.0, typeof( Apricot ), "Apricot", 1, "You don't have enough Apricot." );
			AddSkill( index, SkillName.Alchemy, 100.0, 120.0 );
			AddRes( index, typeof( MagicalFireOpal ), "Magical Fire Opal", 1, " need more Magical Fire Opal" );
			AddRes( index, typeof( Basil ), "Basil", 1, " need more Basil" );
			AddRes( index, typeof( Oregano ), "Oregano", 1, " need more Oregano" );
			AddRes( index, typeof( Cinnamon ), "Cinnamon", 1, " need more Cinnamon" );
			SetNeedOven( index, true );
			index =  AddCraft( typeof( AsparagusTinkering ), "Skill Foods", "Asparagus of Tinkering", 100.0, 120.0, typeof( Asparagus ), "Asparagus", 1, "You don't have enough Asparagus." );
			AddSkill( index, SkillName.Alchemy, 100.0, 120.0 );
			AddRes( index, typeof( MagicalFireEmerald ), "Magical Fire Emerald", 1, " need more Magical Fire Emerald" );
			AddRes( index, typeof( BayLeaf ), "BayLeaf", 1, " need more BayLeaf" );
			AddRes( index, typeof( Orris ), "Orris", 1, " need more Orris" );
			AddRes( index, typeof( Clove ), "Clove", 1, " need more Clove" );
			SetNeedOven( index, true );
			index =  AddCraft( typeof( AvocadoTailoring ), "Skill Foods", "Avocado of Tailoring", 100.0, 120.0, typeof( Avocado ), "Avocado", 1, "You don't have enough Avocado." );
			AddSkill( index, SkillName.Alchemy, 100.0, 120.0 );
			AddRes( index, typeof( MagicalEmerald ), "Magical Emerald", 1, " need more Magical Emerald" );
			AddRes( index, typeof( Caraway ), "Caraway", 1, " need more Caraway" );
			AddRes( index, typeof( Patchouli ), "Patchouli", 1, " need more Patchouli" );
			AddRes( index, typeof( Copal ), "Copal", 1, " need more Copal" );
			SetNeedOven( index, true );
			index =  AddCraft( typeof( BananaTactics ), "Skill Foods", "Banana of Tactics", 100.0, 120.0, typeof( Banana ), "Banana", 1, "You don't have enough Banana." );
			AddSkill( index, SkillName.Alchemy, 100.0, 120.0 );
			AddRes( index, typeof( MagicalEclipseStone ), "Magical Eclipse Stone", 1, " need more Magical Eclipse Stone" );
			AddRes( index, typeof( Chamomile ), "Chamomile", 1, " need more Chamomile" );
			AddRes( index, typeof( Peppercorn ), "Peppercorn", 1, " need more Peppercorn" );
			AddRes( index, typeof( Coriander ), "Coriander", 1, " need more Coriander" );
			SetNeedOven( index, true );
			index =  AddCraft( typeof( BeetSwords ), "Skill Foods", "Beet of Swords", 100.0, 120.0, typeof( Beet ), "Beet", 1, "You don't have enough Beet." );
			AddSkill( index, SkillName.Alchemy, 100.0, 120.0 );
			AddRes( index, typeof( MagicalDiamond ), "Magical Diamond", 1, " need more Magical Diamond" );
			AddRes( index, typeof( Cilantro ), "Cilantro", 1, " need more Cilantro" );
			AddRes( index, typeof( RoseHerb ), "RoseHerb", 1, " need more RoseHerb" );
			AddRes( index, typeof( Dill ), "Dill", 1, " need more Dill" );
			SetNeedOven( index, true );
			index =  AddCraft( typeof( BlackberryStealth ), "Skill Foods", "Blackberry of Stealth", 100.0, 120.0, typeof( Blackberry ), "Blackberry", 1, "You don't have enough Blackberry." );
			AddSkill( index, SkillName.Alchemy, 100.0, 120.0 );
			AddRes( index, typeof( MagicalCitrine ), "Magical Citrine", 1, " need more Magical Citrine" );
			AddRes( index, typeof( Cinnamon ), "Cinnamon", 1, " need more Cinnamon" );
			AddRes( index, typeof( Rosemary ), "Rosemary", 1, " need more Rosemary" );
			AddRes( index, typeof( Dragonsblood ), "Dragonsblood", 1, " need more Dragonsblood" );
			SetNeedOven( index, true );
			index =  AddCraft( typeof( BlackRaspberryStealing ), "Skill Foods", "Black Raspberry of Stealing", 100.0, 120.0, typeof( BlackRaspberry ), "Black Raspberry", 1, "You don't have enough Black Raspberry." );
			AddSkill( index, SkillName.Alchemy, 100.0, 120.0 );
			AddRes( index, typeof( MagicalBlueDiamond ), "Magical Blue Diamond", 1, " need more Magical Blue Diamond" );
			AddRes( index, typeof( Clove ), "Clove", 1, " need more Clove" );
			AddRes( index, typeof( Saffron ), "Saffron", 1, " need more Saffron" );
			AddRes( index, typeof( Frankincense ), "Frankincense", 1, " need more Frankincense" );
			SetNeedOven( index, true );
			index =  AddCraft( typeof( BlueberrySpiritSpeak ), "Skill Foods", "Blueberry of Spirit Speak", 100.0, 120.0, typeof( Blueberry ), "Blueberry", 1, "You don't have enough Blueberry." );
			AddSkill( index, SkillName.Alchemy, 100.0, 120.0 );
			AddRes( index, typeof( MagicalBloodStone ), "Magical Blood Stone", 1, " need more Magical Blood Stone" );
			AddRes( index, typeof( Copal ), "Copal", 1, " need more Copal" );
			AddRes( index, typeof( Sage ), "Sage", 1, " need more Sage" );
			AddRes( index, typeof( Lavender ), "Lavender", 1, " need more Lavender" );
			SetNeedOven( index, true );
			index =  AddCraft( typeof( BroccoliSpellweaving  ), "Skill Foods", "Broccoli of Spellweaving ", 100.0, 120.0, typeof( Broccoli ), "Broccoli", 1, "You don't have enough Broccoli." );
			AddSkill( index, SkillName.Alchemy, 100.0, 120.0 );
			AddRes( index, typeof( MagicalAmethyst ), "Magical Amethyst", 1, " need more Magical Amethyst" );
			AddRes( index, typeof( Coriander ), "Coriander", 1, " need more Coriander" );
			AddRes( index, typeof( Sandelwood ), "Sandelwood", 1, " need more Sandelwood" );
			AddRes( index, typeof( Marjoram ), "Marjoram", 1, " need more Marjoram" );
			SetNeedOven( index, true );
			index =  AddCraft( typeof( CabbageSnooping ), "Skill Foods", "Cabbage of Snooping", 100.0, 120.0, typeof( Cabbage ), "Cabbage", 1, "You don't have enough Cabbage." );
			AddSkill( index, SkillName.Alchemy, 100.0, 120.0 );
			AddRes( index, typeof( MagicalAmber ), "Magical Amber", 1, " need more Magical Amber" );
			AddRes( index, typeof( Dill ), "Dill", 1, " need more Dill" );
			AddRes( index, typeof( SlipperyElm ), "SlipperyElm", 1, " need more SlipperyElm" );
			AddRes( index, typeof( Meadowsweet ), "Meadowsweet", 1, " need more Meadowsweet" );
			SetNeedOven( index, true );
			index =  AddCraft( typeof( CantaloupeRemoveTrap ), "Skill Foods", "Cantaloupe of Remove Trap", 100.0, 120.0, typeof( Cantaloupe ), "Cantaloupe", 1, "You don't have enough Cantaloupe." );
			AddSkill( index, SkillName.Alchemy, 100.0, 120.0 );
			AddRes( index, typeof( MagicalAmber ), "Magical Amber", 1, " need more Magical Amber" );
			AddRes( index, typeof( Dragonsblood ), "Dragonsblood", 1, " need more Dragonsblood" );
			AddRes( index, typeof( Thyme ), "Thyme", 1, " need more Thyme" );
			AddRes( index, typeof( Mint ), "Mint", 1, " need more Mint" );
			SetNeedOven( index, true );
			index =  AddCraft( typeof( CarrotProvocation ), "Skill Foods", "Carrot of Provocation", 100.0, 120.0, typeof( Carrot ), "Carrot", 1, "You don't have enough Carrot." );
			AddSkill( index, SkillName.Alchemy, 100.0, 120.0 );
			AddRes( index, typeof( MagicalAmethyst ), "Magical Amethyst", 1, " need more Magical Amethyst" );
			AddRes( index, typeof( Frankincense ), "Frankincense", 1, " need more Frankincense" );
			AddRes( index, typeof( Valerian ), "Valerian", 1, " need more Valerian" );
			AddRes( index, typeof( Mugwort ), "Mugwort", 1, " need more Mugwort" );
			SetNeedOven( index, true );
			index =  AddCraft( typeof( CauliflowerPoisoning ), "Skill Foods", "Cauliflower of Poisoning", 100.0, 120.0, typeof( Cauliflower ), "Cauliflower", 1, "You don't have enough Cauliflower." );
			AddSkill( index, SkillName.Alchemy, 100.0, 120.0 );
			AddRes( index, typeof( MagicalBloodStone ), "Magical Blood Stone", 1, " need more Magical Blood Stone" );
			AddRes( index, typeof( Lavender ), "Lavender", 1, " need more Lavender" );
			AddRes( index, typeof( WillowBark ), "WillowBark", 1, " need more WillowBark" );
			AddRes( index, typeof( Mustard ), "Mustard", 1, " need more Mustard" );
			SetNeedOven( index, true );
			index =  AddCraft( typeof( CeleryPeacemaking ), "Skill Foods", "Celery of Peacemaking", 100.0, 120.0, typeof( Celery ), "Celery", 1, "You don't have enough Celery." );
			AddSkill( index, SkillName.Alchemy, 100.0, 120.0 );
			AddRes( index, typeof( MagicalBlueDiamond ), "Magical Blue Diamond", 1, " need more Magical Blue Diamond" );
			AddRes( index, typeof( Marjoram ), "Marjoram", 1, " need more Marjoram" );
			AddRes( index, typeof( Acacia ), "Acacia", 1, " need more Acacia" );
			AddRes( index, typeof( Myrrh ), "Myrrh", 1, " need more Myrrh" );
			SetNeedOven( index, true );
			index =  AddCraft( typeof( CherryParry ), "Skill Foods", "Cherry of Parry", 100.0, 120.0, typeof( Cherry ), "Cherry", 1, "You don't have enough Cherry." );
			AddSkill( index, SkillName.Alchemy, 100.0, 120.0 );
			AddRes( index, typeof( MagicalCitrine ), "Magical Citrine", 1, " need more Magical Citrine" );
			AddRes( index, typeof( Meadowsweet ), "Meadowsweet", 1, " need more Meadowsweet" );
			AddRes( index, typeof( Anise ), "Anise", 1, " need more Anise" );
			AddRes( index, typeof( Olive ), "Olive", 1, " need more Olive" );
			SetNeedOven( index, true );
			index =  AddCraft( typeof( ChiliPepperNinjitsu ), "Skill Foods", "Chili Pepper of Ninjitsu", 100.0, 120.0, typeof( ChiliPepper ), "Chili Pepper", 1, "You don't have enough Chili Pepper." );
			AddSkill( index, SkillName.Alchemy, 100.0, 120.0 );
			AddRes( index, typeof( MagicalDiamond ), "Magical Diamond", 1, " need more Magical Diamond" );
			AddRes( index, typeof( Mint ), "Mint", 1, " need more Mint" );
			AddRes( index, typeof( Basil ), "Basil", 1, " need more Basil" );
			AddRes( index, typeof( Oregano ), "Oregano", 1, " need more Oregano" );
			SetNeedOven( index, true );
			index =  AddCraft( typeof( CoconutNecromancy ), "Skill Foods", "Coconut of Necromancy", 100.0, 120.0, typeof( Coconut ), "Coconut", 1, "You don't have enough Coconut." );
			AddSkill( index, SkillName.Alchemy, 100.0, 120.0 );
			AddRes( index, typeof( MagicalEclipseStone ), "Magical Eclipse Stone", 1, " need more Magical Eclipse Stone" );
			AddRes( index, typeof( Mugwort ), "Mugwort", 1, " need more Mugwort" );
			AddRes( index, typeof( BayLeaf ), "BayLeaf", 1, " need more BayLeaf" );
			AddRes( index, typeof( Orris ), "Orris", 1, " need more Orris" );
			SetNeedOven( index, true );
			index =  AddCraft( typeof( CranberryMusicianship ), "Skill Foods", "Cranberry of Musicianship", 100.0, 120.0, typeof( Cranberry ), "Cranberry", 1, "You don't have enough Cranberry." );
			AddSkill( index, SkillName.Alchemy, 100.0, 120.0 );
			AddRes( index, typeof( MagicalEmerald ), "Magical Emerald", 1, " need more Magical Emerald" );
			AddRes( index, typeof( Mustard ), "Mustard", 1, " need more Mustard" );
			AddRes( index, typeof( Caraway ), "Caraway", 1, " need more Caraway" );
			AddRes( index, typeof( Patchouli ), "Patchouli", 1, " need more Patchouli" );
			SetNeedOven( index, true );
			index =  AddCraft( typeof( CucumberMining ), "Skill Foods", "Cucumber of Mining", 100.0, 120.0, typeof( Cucumber ), "Cucumber", 1, "You don't have enough Cucumber." );
			AddSkill( index, SkillName.Alchemy, 100.0, 120.0 );
			AddRes( index, typeof( MagicalFireEmerald ), "Magical Fire Emerald", 1, " need more Magical Fire Emerald" );
			AddRes( index, typeof( Myrrh ), "Myrrh", 1, " need more Myrrh" );
			AddRes( index, typeof( Chamomile ), "Chamomile", 1, " need more Chamomile" );
			AddRes( index, typeof( Peppercorn ), "Peppercorn", 1, " need more Peppercorn" );
			SetNeedOven( index, true );
			index =  AddCraft( typeof( EggplantMeditation ), "Skill Foods", "Eggplant of Meditation", 100.0, 120.0, typeof( Eggplant ), "Eggplant", 1, "You don't have enough Eggplant." );
			AddSkill( index, SkillName.Alchemy, 100.0, 120.0 );
			AddRes( index, typeof( MagicalFireOpal ), "Magical Fire Opal", 1, " need more Magical Fire Opal" );
			AddRes( index, typeof( Olive ), "Olive", 1, " need more Olive" );
			AddRes( index, typeof( Cilantro ), "Cilantro", 1, " need more Cilantro" );
			AddRes( index, typeof( RoseHerb ), "RoseHerb", 1, " need more RoseHerb" );
			SetNeedOven( index, true );
			index =  AddCraft( typeof( GrapefruitMagicResist ), "Skill Foods", "Grapefruit of Magic Resist", 100.0, 120.0, typeof( Grapefruit ), "Grapefruit", 1, "You don't have enough Grapefruit." );
			AddSkill( index, SkillName.Alchemy, 100.0, 120.0 );
			AddRes( index, typeof( MagicalJade ), "Magical Jade", 1, " need more Magical Jade" );
			AddRes( index, typeof( Oregano ), "Oregano", 1, " need more Oregano" );
			AddRes( index, typeof( Cinnamon ), "Cinnamon", 1, " need more Cinnamon" );
			AddRes( index, typeof( Rosemary ), "Rosemary", 1, " need more Rosemary" );
			SetNeedOven( index, true );
			index =  AddCraft( typeof( GreenBeanMagery ), "Skill Foods", "Green Bean of Magery", 100.0, 120.0, typeof( GreenBean ), "Green Bean", 1, "You don't have enough Green Bean." );
			AddSkill( index, SkillName.Alchemy, 100.0, 120.0 );
			AddRes( index, typeof( MagicalJasper ), "Magical Jasper", 1, " need more Magical Jasper" );
			AddRes( index, typeof( Orris ), "Orris", 1, " need more Orris" );
			AddRes( index, typeof( Clove ), "Clove", 1, " need more Clove" );
			AddRes( index, typeof( Saffron ), "Saffron", 1, " need more Saffron" );
			SetNeedOven( index, true );
			index =  AddCraft( typeof( GreenPepperMacing ), "Skill Foods", "Green Pepper of Macing", 100.0, 120.0, typeof( GreenPepper ), "Green Pepper", 1, "You don't have enough Green Pepper." );
			AddSkill( index, SkillName.Alchemy, 100.0, 120.0 );
			AddRes( index, typeof( MagicalMoonStoneGem ), "Magical Moon Stone", 1, " need more Magical Moon Stone" );
			AddRes( index, typeof( Patchouli ), "Patchouli", 1, " need more Patchouli" );
			AddRes( index, typeof( Copal ), "Copal", 1, " need more Copal" );
			AddRes( index, typeof( Sage ), "Sage", 1, " need more Sage" );
			SetNeedOven( index, true );
			index =  AddCraft( typeof( GreenSquashLumberjacking ), "Skill Foods", "Green Squash of Lumberjacking", 100.0, 120.0, typeof( GreenSquash ), "Green Squash", 1, "You don't have enough Green Squash." );
			AddSkill( index, SkillName.Alchemy, 100.0, 120.0 );
			AddRes( index, typeof( MagicalOnyx ), "Magical Onyx", 1, " need more Magical Onyx" );
			AddRes( index, typeof( Peppercorn ), "Peppercorn", 1, " need more Peppercorn" );
			AddRes( index, typeof( Coriander ), "Coriander", 1, " need more Coriander" );
			AddRes( index, typeof( Sandelwood ), "Sandelwood", 1, " need more Sandelwood" );
			SetNeedOven( index, true );
			index =  AddCraft( typeof( HoneydewMelonLockpicking ), "Skill Foods", "Honeydew Melon of Lockpicking", 100.0, 120.0, typeof( HoneydewMelon ), "Honeydew Melon", 1, "You don't have enough Honeydew Melon." );
			AddSkill( index, SkillName.Alchemy, 100.0, 120.0 );
			AddRes( index, typeof( MagicalOpal ), "Magical Opal", 1, " need more Magical Opal" );
			AddRes( index, typeof( RoseHerb ), "RoseHerb", 1, " need more RoseHerb" );
			AddRes( index, typeof( Dill ), "Dill", 1, " need more Dill" );
			AddRes( index, typeof( SlipperyElm ), "SlipperyElm", 1, " need more SlipperyElm" );
			SetNeedOven( index, true );
			index =  AddCraft( typeof( KiwiItemID ), "Skill Foods", "Kiwi of Item ID", 100.0, 120.0, typeof( Kiwi ), "Kiwi", 1, "You don't have enough Kiwi." );
			AddSkill( index, SkillName.Alchemy, 100.0, 120.0 );
			AddRes( index, typeof( MagicalPearl ), "Magical Pearl", 1, " need more Magical Pearl" );
			AddRes( index, typeof( Rosemary ), "Rosemary", 1, " need more Rosemary" );
			AddRes( index, typeof( Dragonsblood ), "Dragonsblood", 1, " need more Dragonsblood" );
			AddRes( index, typeof( Thyme ), "Thyme", 1, " need more Thyme" );
			SetNeedOven( index, true );
			index =  AddCraft( typeof( LettuceInscribe ), "Skill Foods", "Lettuce of Inscribe", 100.0, 120.0, typeof( Lettuce ), "Lettuce", 1, "You don't have enough Lettuce." );
			AddSkill( index, SkillName.Alchemy, 100.0, 120.0 );
			AddRes( index, typeof( MagicalRuby ), "Magical Ruby", 1, " need more Magical Ruby" );
			AddRes( index, typeof( Saffron ), "Saffron", 1, " need more Saffron" );
			AddRes( index, typeof( Frankincense ), "Frankincense", 1, " need more Frankincense" );
			AddRes( index, typeof( Valerian ), "Valerian", 1, " need more Valerian" );
			SetNeedOven( index, true );
			index =  AddCraft( typeof( MangoHiding ), "Skill Foods", "Mango of Hiding", 100.0, 120.0, typeof( Mango ), "Mango", 1, "You don't have enough Mango." );
			AddSkill( index, SkillName.Alchemy, 100.0, 120.0 );
			AddRes( index, typeof( MagicalSapphire ), "Magical Sapphire", 1, " need more Magical Sapphire" );
			AddRes( index, typeof( Sage ), "Sage", 1, " need more Sage" );
			AddRes( index, typeof( Lavender ), "Lavender", 1, " need more Lavender" );
			AddRes( index, typeof( WillowBark ), "WillowBark", 1, " need more WillowBark" );
			SetNeedOven( index, true );
			index =  AddCraft( typeof( OnionHerding ), "Skill Foods", "Onion of Herding", 100.0, 120.0, typeof( Onion ), "Onion", 1, "You don't have enough Onion." );
			AddSkill( index, SkillName.Alchemy, 100.0, 120.0 );
			AddRes( index, typeof( MagicalStarRose ), "Magical Star Rose", 1, " need more Magical Star Rose" );
			AddRes( index, typeof( Sandelwood ), "Sandelwood", 1, " need more Sandelwood" );
			AddRes( index, typeof( Marjoram ), "Marjoram", 1, " need more Marjoram" );
			AddRes( index, typeof( Acacia ), "Acacia", 1, " need more Acacia" );
			SetNeedOven( index, true );
			index =  AddCraft( typeof( OrangeHealing ), "Skill Foods", "Orange of Healing", 100.0, 120.0, typeof( Orange ), "Orange", 1, "You don't have enough Orange." );
			AddSkill( index, SkillName.Alchemy, 100.0, 120.0 );
			AddRes( index, typeof( MagicalStarRuby ), "Magical Star Ruby", 1, " need more Magical Star Ruby" );
			AddRes( index, typeof( SlipperyElm ), "SlipperyElm", 1, " need more SlipperyElm" );
			AddRes( index, typeof( Meadowsweet ), "Meadowsweet", 1, " need more Meadowsweet" );
			AddRes( index, typeof( Anise ), "Anise", 1, " need more Anise" );
			SetNeedOven( index, true );
			index =  AddCraft( typeof( OrangePepperForensics ), "Skill Foods", "Orange Pepper of Forensics", 100.0, 120.0, typeof( OrangePepper ), "Orange Pepper", 1, "You don't have enough Orange Pepper." );
			AddSkill( index, SkillName.Alchemy, 100.0, 120.0 );
			AddRes( index, typeof( MagicalStarSapphire ), "Magical Star Sapphire", 1, " need more Magical Star Sapphire" );
			AddRes( index, typeof( Thyme ), "Thyme", 1, " need more Thyme" );
			AddRes( index, typeof( Mint ), "Mint", 1, " need more Mint" );
			AddRes( index, typeof( Basil ), "Basil", 1, " need more Basil" );
			SetNeedOven( index, true );
			index =  AddCraft( typeof( PeachFocus ), "Skill Foods", "Peach of Focus", 100.0, 120.0, typeof( Peach ), "Peach", 1, "You don't have enough Peach." );
			AddSkill( index, SkillName.Alchemy, 100.0, 120.0 );
			AddRes( index, typeof( MagicalSunStone ), "Magical Sun Stone", 1, " need more Magical Sun Stone" );
			AddRes( index, typeof( Valerian ), "Valerian", 1, " need more Valerian" );
			AddRes( index, typeof( Mugwort ), "Mugwort", 1, " need more Mugwort" );
			AddRes( index, typeof( BayLeaf ), "BayLeaf", 1, " need more BayLeaf" );
			SetNeedOven( index, true );
			index =  AddCraft( typeof( PeanutFletching ), "Skill Foods", "Peanut of Fletching", 100.0, 120.0, typeof( Peanut ), "Peanut", 1, "You don't have enough Peanut." );
			AddSkill( index, SkillName.Alchemy, 100.0, 120.0 );
			AddRes( index, typeof( MagicalTopaz ), "Magical Topaz", 1, " need more Magical Topaz" );
			AddRes( index, typeof( WillowBark ), "WillowBark", 1, " need more WillowBark" );
			AddRes( index, typeof( Mustard ), "Mustard", 1, " need more Mustard" );
			AddRes( index, typeof( Caraway ), "Caraway", 1, " need more Caraway" );
			SetNeedOven( index, true );
			index =  AddCraft( typeof( PearFishing ), "Skill Foods", "Pear of Fishing", 100.0, 120.0, typeof( Pear ), "Pear", 1, "You don't have enough Pear." );
			AddSkill( index, SkillName.Alchemy, 100.0, 120.0 );
			AddRes( index, typeof( MagicalTourmaline ), "Magical Tourmaline", 1, " need more Magical Tourmaline" );
			AddRes( index, typeof( Acacia ), "Acacia", 1, " need more Acacia" );
			AddRes( index, typeof( Myrrh ), "Myrrh", 1, " need more Myrrh" );
			AddRes( index, typeof( Chamomile ), "Chamomile", 1, " need more Chamomile" );
			SetNeedOven( index, true );
			index =  AddCraft( typeof( PeasFencing ), "Skill Foods", "Peas of Fencing", 100.0, 120.0, typeof( Peas ), "Peas", 1, "You don't have enough Peas." );
			AddSkill( index, SkillName.Alchemy, 100.0, 120.0 );
			AddRes( index, typeof( MagicalTurquoise ), "Magical Turquoise", 1, " need more Magical Turquoise" );
			AddRes( index, typeof( Anise ), "Anise", 1, " need more Anise" );
			AddRes( index, typeof( Olive ), "Olive", 1, " need more Olive" );
			AddRes( index, typeof( Cilantro ), "Cilantro", 1, " need more Cilantro" );
			SetNeedOven( index, true );
			index =  AddCraft( typeof( PineappleEvalInt ), "Skill Foods", "Pineapple of EvalInt", 100.0, 120.0, typeof( Pineapple ), "Pineapple", 1, "You don't have enough Pineapple." );
			AddSkill( index, SkillName.Alchemy, 100.0, 120.0 );
			AddRes( index, typeof( MagicalTourmaline ), "Magical Tourmaline", 1, " need more Magical Tourmaline" );
			AddRes( index, typeof( Basil ), "Basil", 1, " need more Basil" );
			AddRes( index, typeof( Oregano ), "Oregano", 1, " need more Oregano" );
			AddRes( index, typeof( Cinnamon ), "Cinnamon", 1, " need more Cinnamon" );
			SetNeedOven( index, true );
			index =  AddCraft( typeof( PistacioDiscordance ), "Skill Foods", "Pistacio of Discordance", 100.0, 120.0, typeof( Pistacio ), "Pistacio", 1, "You don't have enough Pistacio." );
			AddSkill( index, SkillName.Alchemy, 100.0, 120.0 );
			AddRes( index, typeof( MagicalTopaz ), "Magical Topaz", 1, " need more Magical Topaz" );
			AddRes( index, typeof( BayLeaf ), "BayLeaf", 1, " need more BayLeaf" );
			AddRes( index, typeof( Orris ), "Orris", 1, " need more Orris" );
			AddRes( index, typeof( Clove ), "Clove", 1, " need more Clove" );
			SetNeedOven( index, true );
			index =  AddCraft( typeof( PomegranateDetectHidden ), "Skill Foods", "Pomegranate of Detect Hidden", 100.0, 120.0, typeof( Pomegranate ), "Pomegranate", 1, "You don't have enough Pomegranate." );
			AddSkill( index, SkillName.Alchemy, 100.0, 120.0 );
			AddRes( index, typeof( MagicalSunStone ), "Magical Sun Stone", 1, " need more Magical Sun Stone" );
			AddRes( index, typeof( Caraway ), "Caraway", 1, " need more Caraway" );
			AddRes( index, typeof( Patchouli ), "Patchouli", 1, " need more Patchouli" );
			AddRes( index, typeof( Copal ), "Copal", 1, " need more Copal" );
			SetNeedOven( index, true );
			index =  AddCraft( typeof( PotatoCooking ), "Skill Foods", "Potato of Cooking", 100.0, 120.0, typeof( Potato ), "Potato", 1, "You don't have enough Potato." );
			AddSkill( index, SkillName.Alchemy, 100.0, 120.0 );
			AddRes( index, typeof( MagicalStarSapphire ), "Magical Star Sapphire", 1, " need more Magical Star Sapphire" );
			AddRes( index, typeof( Chamomile ), "Chamomile", 1, " need more Chamomile" );
			AddRes( index, typeof( Peppercorn ), "Peppercorn", 1, " need more Peppercorn" );
			AddRes( index, typeof( Coriander ), "Coriander", 1, " need more Coriander" );
			SetNeedOven( index, true );
			index =  AddCraft( typeof( PumpkinChivalry ), "Skill Foods", "Pumpkin of Chivalry", 100.0, 120.0, typeof( Pumpkin ), "Pumpkin", 1, "You don't have enough Pumpkin." );
			AddSkill( index, SkillName.Alchemy, 100.0, 120.0 );
			AddRes( index, typeof( MagicalStarRuby ), "Magical Star Ruby", 1, " need more Magical Star Ruby" );
			AddRes( index, typeof( Cilantro ), "Cilantro", 1, " need more Cilantro" );
			AddRes( index, typeof( RoseHerb ), "RoseHerb", 1, " need more RoseHerb" );
			AddRes( index, typeof( Dill ), "Dill", 1, " need more Dill" );
			SetNeedOven( index, true );
			index =  AddCraft( typeof( RadishCartography ), "Skill Foods", "Radish of Cartography", 100.0, 120.0, typeof( Radish ), "Radish", 1, "You don't have enough Radish." );
			AddSkill( index, SkillName.Alchemy, 100.0, 120.0 );
			AddRes( index, typeof( MagicalStarRose ), "Magical Star Rose", 1, " need more Magical Star Rose" );
			AddRes( index, typeof( Cinnamon ), "Cinnamon", 1, " need more Cinnamon" );
			AddRes( index, typeof( Rosemary ), "Rosemary", 1, " need more Rosemary" );
			AddRes( index, typeof( Dragonsblood ), "Dragonsblood", 1, " need more Dragonsblood" );
			SetNeedOven( index, true );
			index =  AddCraft( typeof( RedPepperCarpentry ), "Skill Foods", "Red Pepper of Carpentry", 100.0, 120.0, typeof( RedPepper ), "Red Pepper", 1, "You don't have enough Red Pepper." );
			AddSkill( index, SkillName.Alchemy, 100.0, 120.0 );
			AddRes( index, typeof( MagicalSapphire ), "Magical Sapphire", 1, " need more Magical Sapphire" );
			AddRes( index, typeof( Clove ), "Clove", 1, " need more Clove" );
			AddRes( index, typeof( Saffron ), "Saffron", 1, " need more Saffron" );
			AddRes( index, typeof( Frankincense ), "Frankincense", 1, " need more Frankincense" );
			SetNeedOven( index, true );
			index =  AddCraft( typeof( RedRaspberryCamping ), "Skill Foods", "Red Raspberry of Camping", 100.0, 120.0, typeof( RedRaspberry ), "Red Raspberry", 1, "You don't have enough Red Raspberry." );
			AddSkill( index, SkillName.Alchemy, 100.0, 120.0 );
			AddRes( index, typeof( MagicalRuby ), "Magical Ruby", 1, " need more Magical Ruby" );
			AddRes( index, typeof( Copal ), "Copal", 1, " need more Copal" );
			AddRes( index, typeof( Sage ), "Sage", 1, " need more Sage" );
			AddRes( index, typeof( Lavender ), "Lavender", 1, " need more Lavender" );
			SetNeedOven( index, true );
			index =  AddCraft( typeof( SnowPeasBushido ), "Skill Foods", "Snow Peas of Bushido", 100.0, 120.0, typeof( SnowPeas ), "Snow Peas", 1, "You don't have enough Snow Peas." );
			AddSkill( index, SkillName.Alchemy, 100.0, 120.0 );
			AddRes( index, typeof( MagicalPearl ), "Magical Pearl", 1, " need more Magical Pearl" );
			AddRes( index, typeof( Coriander ), "Coriander", 1, " need more Coriander" );
			AddRes( index, typeof( Sandelwood ), "Sandelwood", 1, " need more Sandelwood" );
			AddRes( index, typeof( Marjoram ), "Marjoram", 1, " need more Marjoram" );
			SetNeedOven( index, true );
			index =  AddCraft( typeof( SpinachBlacksmith ), "Skill Foods", "Spinach of Blacksmith", 100.0, 120.0, typeof( Spinach ), "Spinach", 1, "You don't have enough Spinach." );
			AddSkill( index, SkillName.Alchemy, 100.0, 120.0 );
			AddRes( index, typeof( MagicalOpal ), "Magical Opal", 1, " need more Magical Opal" );
			AddRes( index, typeof( Dill ), "Dill", 1, " need more Dill" );
			AddRes( index, typeof( SlipperyElm ), "SlipperyElm", 1, " need more SlipperyElm" );
			AddRes( index, typeof( Meadowsweet ), "Meadowsweet", 1, " need more Meadowsweet" );
			SetNeedOven( index, true );
			index =  AddCraft( typeof( SquashBegging ), "Skill Foods", "Squash of Begging", 100.0, 120.0, typeof( Squash ), "Squash", 1, "You don't have enough Squash." );
			AddSkill( index, SkillName.Alchemy, 100.0, 120.0 );
			AddRes( index, typeof( MagicalOnyx ), "Magical Onyx", 1, " need more Magical Onyx" );
			AddRes( index, typeof( Dragonsblood ), "Dragonsblood", 1, " need more Dragonsblood" );
			AddRes( index, typeof( Thyme ), "Thyme", 1, " need more Thyme" );
			AddRes( index, typeof( Mint ), "Mint", 1, " need more Mint" );
			SetNeedOven( index, true );
			index =  AddCraft( typeof( StrawberryArmsLore ), "Skill Foods", "Strawberry of Arms Lore", 100.0, 120.0, typeof( Strawberry ), "Strawberry", 1, "You don't have enough Strawberry." );
			AddSkill( index, SkillName.Alchemy, 100.0, 120.0 );
			AddRes( index, typeof( MagicalMoonStoneGem ), "Magical Moon Stone", 1, " need more Magical Moon Stone" );
			AddRes( index, typeof( Frankincense ), "Frankincense", 1, " need more Frankincense" );
			AddRes( index, typeof( Valerian ), "Valerian", 1, " need more Valerian" );
			AddRes( index, typeof( Mugwort ), "Mugwort", 1, " need more Mugwort" );
			SetNeedOven( index, true );
			index =  AddCraft( typeof( SweetPotatoArchery ), "Skill Foods", "Sweet Potato of Archery", 100.0, 120.0, typeof( SweetPotato ), "Sweet Potato", 1, "You don't have enough Sweet Potato." );
			AddSkill( index, SkillName.Alchemy, 100.0, 120.0 );
			AddRes( index, typeof( MagicalBlueDiamond ), "Magical Blue Diamond", 1, " need more Magical Blue Diamond" );
			AddRes( index, typeof( Lavender ), "Lavender", 1, " need more Lavender" );
			AddRes( index, typeof( WillowBark ), "WillowBark", 1, " need more WillowBark" );
			AddRes( index, typeof( Mustard ), "Mustard", 1, " need more Mustard" );
			SetNeedOven( index, true );
			index =  AddCraft( typeof( TomatoAnimalTaming ), "Skill Foods", "Tomato of Animal Taming", 100.0, 120.0, typeof( Tomato ), "Tomato", 1, "You don't have enough Tomato." );
			AddSkill( index, SkillName.Alchemy, 100.0, 120.0 );
			AddRes( index, typeof( MagicalOpal ), "Magical Opal", 1, " need more Magical Opal" );
			AddRes( index, typeof( Marjoram ), "Marjoram", 1, " need more Marjoram" );
			AddRes( index, typeof( Acacia ), "Acacia", 1, " need more Acacia" );
			AddRes( index, typeof( Myrrh ), "Myrrh", 1, " need more Myrrh" );
			SetNeedOven( index, true );
			index =  AddCraft( typeof( TurnipAnimalLore ), "Skill Foods", "Turnip of Animal Lore", 100.0, 120.0, typeof( Turnip ), "Turnip", 1, "You don't have enough Turnip." );
			AddSkill( index, SkillName.Alchemy, 100.0, 120.0 );
			AddRes( index, typeof( MagicalStarRose ), "Magical Star Rose", 1, " need more Magical Star Rose" );
			AddRes( index, typeof( Meadowsweet ), "Meadowsweet", 1, " need more Meadowsweet" );
			AddRes( index, typeof( Anise ), "Anise", 1, " need more Anise" );
			AddRes( index, typeof( Olive ), "Olive", 1, " need more Olive" );
			SetNeedOven( index, true );
			index =  AddCraft( typeof( WatermelonAnatomy ), "Skill Foods", "Watermelon of Anatomy", 100.0, 120.0, typeof( Watermelon ), "Watermelon", 1, "You don't have enough Watermelon." );
			AddSkill( index, SkillName.Alchemy, 100.0, 120.0 );
			AddRes( index, typeof( MagicalTurquoise ), "Magical Turquoise", 1, " need more Magical Turquoise" );
			AddRes( index, typeof( Mint ), "Mint", 1, " need more Mint" );
			AddRes( index, typeof( Basil ), "Basil", 1, " need more Basil" );
			AddRes( index, typeof( Oregano ), "Oregano", 1, " need more Oregano" );
			SetNeedOven( index, true );
			index =  AddCraft( typeof( YellowPepperAlchemy ), "Skill Foods", "Yellow Pepper of Alchemy", 100.0, 120.0, typeof( YellowPepper ), "Yellow Pepper", 1, "You don't have enough Yellow Pepper." );
			AddSkill( index, SkillName.Alchemy, 100.0, 120.0 );
			AddRes( index, typeof( MagicalEclipseStone ), "Magical Eclipse Stone", 1, " need more Magical Eclipse Stone" );
			AddRes( index, typeof( Mugwort ), "Mugwort", 1, " need more Mugwort" );
			AddRes( index, typeof( BayLeaf ), "BayLeaf", 1, " need more BayLeaf" );
			AddRes( index, typeof( Orris ), "Orris", 1, " need more Orris" );
			SetNeedOven( index, true );

		}
	}
}