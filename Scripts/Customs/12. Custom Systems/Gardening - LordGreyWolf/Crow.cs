using System;
using Server.Items;
using Server.Mobiles;
using Server.Items.Crops;
using System.Collections;
using System.Collections.Generic;

namespace Server.Mobiles
{
	[CorpseName( "a crow corpse" )]
	public class Crow : BaseCreature
	{
		[Constructable]
		public Crow() : base( AIType.AI_Animal, FightMode.Aggressor, 10, 1, 0.2, 0.4 )
		{
			Name = "a Crow";
			Body = 5;
			BaseSoundID = 0x2EE;
			Hue = 1109;

			SetStr( 41, 53 );
			SetDex( 42, 63 );
			SetInt( 11, 25 );

			SetHits( 31, 47 );
			SetMana( 0 );

			SetDamage( 5, 10 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 20, 25 );
			SetResistance( ResistanceType.Fire, 10, 15 );
			SetResistance( ResistanceType.Cold, 20, 25 );
			SetResistance( ResistanceType.Poison, 5, 10 );
			SetResistance( ResistanceType.Energy, 5, 10 );

			SetSkill( SkillName.MagicResist, 17.8, 34.0 );
			SetSkill( SkillName.Tactics, 19.1, 38.0 );
			SetSkill( SkillName.Wrestling, 43.1, 57.0 );

			Fame = 300;
			Karma = 0;

			VirtualArmor = 22;

			Tamable = true;
			ControlSlots = 1;
			MinTameSkill = 17.1;

			PackGold( 25, 50 );
			PackItem( new BlackPearl( 4 ) );
		}

		private DateTime m_NextEat;

		public override void OnThink()
		{
			base.OnThink();
			if ( this.Alive == false ) return;
			if (this.Controlled == true) return;
			if ( DateTime.Now < m_NextEat ) return;
			m_NextEat = DateTime.Now + TimeSpan.FromSeconds( Utility.RandomMinMax( 15, 30 ) );
			ArrayList list = new ArrayList();
			foreach ( Item item in this.GetItemsInRange( 0 ) )
			{
				if ( item is BaseCrop ) list.Add( item );
			}
			if (list.Count == 0) return;
			foreach ( Item scare in this.GetItemsInRange( 12 ) )
			{
				//if ( scare is Scarecrow )
				if ( scare.ItemID == 7732 || scare.ItemID == 7733 )
				{
					this.X = this.X + Utility.Random(21) - 10;
					this.Y = this.Y + Utility.Random(21) - 10;
					this.Z = this.Z + 10;
					this.Say("Caw Caw aaakkkkkk Caw Caw Caw");
					return;
				}
			}
			
			int toEat = 0;
			for ( int i = 0; i < list.Count; ++i )
			{
				Item item = (Item)list[i];
				BaseCrop plant = item as BaseCrop;
				if ( plant == null) ; // do nothing
				else
				{
					if (plant is AsparagusCrop ) { ((AsparagusCrop)plant).Yield = 0; toEat += 1; }
					else if (plant is BananaCrop ) { ((BananaCrop)plant).Yield = 0; toEat += 1; }
					else if (plant is BeetCrop ) { ((BeetCrop)plant).Yield = 0; toEat += 1; }
					else if (plant is BitterHopsCrop ) { ((BitterHopsCrop)plant).Yield = 0; toEat += 1; }
					else if (plant is BlackberryCrop ) { ((BlackberryCrop)plant).Yield = 0; toEat += 1; }
					else if (plant is BlackRaspberryCrop ) { ((BlackRaspberryCrop)plant).Yield = 0; toEat += 1; }
					else if (plant is BlueberryCrop ) { ((BlueberryCrop)plant).Yield = 0; toEat += 1; }
					else if (plant is BroccoliCrop ) { ((BroccoliCrop)plant).Yield = 0; toEat += 1; }
					else if (plant is CabbageCrop ) { ((CabbageCrop)plant).Yield = 0; toEat += 1; }
					else if (plant is CantaloupeCrop ) { ((CantaloupeCrop)plant).Yield = 0; toEat += 1; }
					else if (plant is CarrotCrop ) { ((CarrotCrop)plant).Yield = 0; toEat += 1; }
					else if (plant is CauliflowerCrop ) { ((CauliflowerCrop)plant).Yield = 0; toEat += 1; }
					else if (plant is CeleryCrop ) { ((CeleryCrop)plant).Yield = 0; toEat += 1; }
					else if (plant is ChiliPepperCrop ) { ((ChiliPepperCrop)plant).Yield = 0; toEat += 1; }
					else if (plant is CoconutCrop ) { ((CoconutCrop)plant).Yield = 0; toEat += 1; }
					else if (plant is CornCrop ) { ((CornCrop)plant).Yield = 0; toEat += 1; }
					else if (plant is CottonCrop ) { ((CottonCrop)plant).Yield = 0; toEat += 1; }
					else if (plant is CranberryCrop ) { ((CranberryCrop)plant).Yield = 0; toEat += 1; }
					else if (plant is CucumberCrop ) { ((CucumberCrop)plant).Yield = 0; toEat += 1; }
					else if (plant is DateCrop ) { ((DateCrop)plant).Yield = 0; toEat += 1; }
					else if (plant is EggplantCrop ) { ((EggplantCrop)plant).Yield = 0; toEat += 1; }
					else if (plant is ElvenHopsCrop ) { ((ElvenHopsCrop)plant).Yield = 0; toEat += 1; }
					else if (plant is FieldCornCrop ) { ((FieldCornCrop)plant).Yield = 0; toEat += 1; }
					else if (plant is FlaxCrop ) { ((FlaxCrop)plant).Yield = 0; toEat += 1; }
					else if (plant is GarlicCrop ) { ((GarlicCrop)plant).Yield = 0; toEat += 1; }
					else if (plant is GinsengCrop ) { ((GinsengCrop)plant).Yield = 0; toEat += 1; }
					else if (plant is GreenBeanCrop ) { ((GreenBeanCrop)plant).Yield = 0; toEat += 1; }
					else if (plant is GreenPepperCrop ) { ((GreenPepperCrop)plant).Yield = 0; toEat += 1; }
					else if (plant is GreenSquashCrop ) { ((GreenSquashCrop)plant).Yield = 0; toEat += 1; }
					else if (plant is HayCrop ) { ((HayCrop)plant).Yield = 0; toEat += 1; }
					else if (plant is HoneydewMelonCrop ) { ((HoneydewMelonCrop)plant).Yield = 0; toEat += 1; }
					else if (plant is IrishRoseCrop ) { ((IrishRoseCrop)plant).Yield = 0; toEat += 1; }
					else if (plant is LettuceCrop ) { ((LettuceCrop)plant).Yield = 0; toEat += 1; }
					else if (plant is MandrakeCrop ) { ((MandrakeCrop)plant).Yield = 0; toEat += 1; }
					else if (plant is MiniAlmondCrop ) { ((MiniAlmondCrop)plant).Yield = 0; toEat += 1; }
					else if (plant is MiniAppleCrop ) { ((MiniAppleCrop)plant).Yield = 0; toEat += 1; }
					else if (plant is MiniApricotCrop ) { ((MiniApricotCrop)plant).Yield = 0; toEat += 1; }
					else if (plant is MiniAvocadoCrop ) { ((MiniAvocadoCrop)plant).Yield = 0; toEat += 1; }
					else if (plant is MiniCherryCrop ) { ((MiniCherryCrop)plant).Yield = 0; toEat += 1; }
					else if (plant is MiniCocoaCrop ) { ((MiniCocoaCrop)plant).Yield = 0; toEat += 1; }
					else if (plant is MiniCoffeeCrop ) { ((MiniCoffeeCrop)plant).Yield = 0; toEat += 1; }
					else if (plant is MiniGrapefruitCrop ) { ((MiniGrapefruitCrop)plant).Yield = 0; toEat += 1; }
					else if (plant is MiniKiwiCrop ) { ((MiniKiwiCrop)plant).Yield = 0; toEat += 1; }
					else if (plant is MiniMangoCrop ) { ((MiniMangoCrop)plant).Yield = 0; toEat += 1; }
					else if (plant is MiniOrangeCrop ) { ((MiniOrangeCrop)plant).Yield = 0; toEat += 1; }
					else if (plant is MiniPeachCrop ) { ((MiniPeachCrop)plant).Yield = 0; toEat += 1; }
					else if (plant is MiniPearCrop ) { ((MiniPearCrop)plant).Yield = 0; toEat += 1; }
					else if (plant is MiniPistacioCrop ) { ((MiniPistacioCrop)plant).Yield = 0; toEat += 1; }
					else if (plant is MiniPomegranateCrop ) { ((MiniPomegranateCrop)plant).Yield = 0; toEat += 1; }
					else if (plant is NightshadeCrop ) { ((NightshadeCrop)plant).Yield = 0; toEat += 1; }
					else if (plant is OatsCrop ) { ((OatsCrop)plant).Yield = 0; toEat += 1; }
					else if (plant is OnionCrop ) { ((OnionCrop)plant).Yield = 0; toEat += 1; }
					else if (plant is OrangePepperCrop ) { ((OrangePepperCrop)plant).Yield = 0; toEat += 1; }
					else if (plant is PansyCrop ) { ((PansyCrop)plant).Yield = 0; toEat += 1; }
					else if (plant is PeanutCrop ) { ((PeanutCrop)plant).Yield = 0; toEat += 1; }
					else if (plant is PeasCrop ) { ((PeasCrop)plant).Yield = 0; toEat += 1; }
					else if (plant is PineappleCrop ) { ((PineappleCrop)plant).Yield = 0; toEat += 1; }
					else if (plant is PinkCarnationCrop ) { ((PinkCarnationCrop)plant).Yield = 0; toEat += 1; }
					else if (plant is PoppyCrop ) { ((PoppyCrop)plant).Yield = 0; toEat += 1; }
					else if (plant is PotatoCrop ) { ((PotatoCrop)plant).Yield = 0; toEat += 1; }
					else if (plant is PumpkinCrop ) { ((PumpkinCrop)plant).Yield = 0; toEat += 1; }
					else if (plant is RadishCrop ) { ((RadishCrop)plant).Yield = 0; toEat += 1; }
					else if (plant is RedMushroomCrop ) { ((RedMushroomCrop)plant).Yield = 0; toEat += 1; }
					else if (plant is RedPepperCrop ) { ((RedPepperCrop)plant).Yield = 0; toEat += 1; }
					else if (plant is RedRaspberryCrop ) { ((RedRaspberryCrop)plant).Yield = 0; toEat += 1; }
					else if (plant is RedRoseCrop ) { ((RedRoseCrop)plant).Yield = 0; toEat += 1; }
					else if (plant is RiceCrop ) { ((RiceCrop)plant).Yield = 0; toEat += 1; }
					else if (plant is SmallBananaCrop ) { ((SmallBananaCrop)plant).Yield = 0; toEat += 1; }
					else if (plant is SnapdragonCrop ) { ((SnapdragonCrop)plant).Yield = 0; toEat += 1; }
					else if (plant is SnowHopsCrop ) { ((SnowHopsCrop)plant).Yield = 0; toEat += 1; }
					else if (plant is SnowPeasCrop ) { ((SnowPeasCrop)plant).Yield = 0; toEat += 1; }
					else if (plant is SoyCrop ) { ((SoyCrop)plant).Yield = 0; toEat += 1; }
					else if (plant is SpinachCrop ) { ((SpinachCrop)plant).Yield = 0; toEat += 1; }
					else if (plant is SpiritRoseCrop ) { ((SpiritRoseCrop)plant).Yield = 0; toEat += 1; }
					else if (plant is SquashCrop ) { ((SquashCrop)plant).Yield = 0; toEat += 1; }
					else if (plant is StrawberryCrop ) { ((StrawberryCrop)plant).Yield = 0; toEat += 1; }
					else if (plant is SugarcaneCrop ) { ((SugarcaneCrop)plant).Yield = 0; toEat += 1; }
					else if (plant is SunFlowerCrop ) { ((SunFlowerCrop)plant).Yield = 0; toEat += 1; }
					else if (plant is SweetHopsCrop ) { ((SweetHopsCrop)plant).Yield = 0; toEat += 1; }
					else if (plant is SweetPotatoCrop ) { ((SweetPotatoCrop)plant).Yield = 0; toEat += 1; }
					else if (plant is TanGingerCrop ) { ((TanGingerCrop)plant).Yield = 0; toEat += 1; }
					else if (plant is TanMushroomCrop ) { ((TanMushroomCrop)plant).Yield = 0; toEat += 1; }
					else if (plant is TeaCrop ) { ((TeaCrop)plant).Yield = 0; toEat += 1; }
					else if (plant is TomatoCrop ) { ((TomatoCrop)plant).Yield = 0; toEat += 1; }
					else if (plant is TurnipCrop ) { ((TurnipCrop)plant).Yield = 0; toEat += 1; }
					else if (plant is WatermelonCrop ) { ((WatermelonCrop)plant).Yield = 0; toEat += 1; }
					else if (plant is WheatCrop ) { ((WheatCrop)plant).Yield = 0; toEat += 1; }
					else if (plant is WhiteRoseCrop ) { ((WhiteRoseCrop)plant).Yield = 0; toEat += 1; }
					else if (plant is YellowPepperCrop ) { ((YellowPepperCrop)plant).Yield = 0; toEat += 1; }
					else if (plant is YellowRoseCrop ) { ((YellowRoseCrop)plant).Yield = 0; toEat += 1; }
				}
				if (toEat > 0) this.Say("Caw Caw Caw mmmmm Caw Caw");
			}
		}

		public override int Meat{ get{ return 1; } }
		public override MeatType MeatType{ get{ return MeatType.Bird; } }
		public override int Feathers{ get{ return 36; } }
		public override FoodType FavoriteFood{ get{ return FoodType.Meat | FoodType.Fish | FoodType.FruitsAndVegies; } }

		public Crow(Serial serial) : base(serial){}
		public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int) 0); }
		public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
	}
}