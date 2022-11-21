using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class EmpoweringCrystalBag : Bag
	{
		public override string DefaultName
		{
			get { return "an Empowering Crystal Bag"; }
		}

		[Constructable]
		public EmpoweringCrystalBag() : this( 1 )
		{
			Movable = true;
		}

		[Constructable]
		public EmpoweringCrystalBag( int amount )
		{
			DropItem( new AllyaExileDeed() );
			DropItem( new AllyaRedemptionDeed() );
			DropItem( new AnkarresDeed() );
			DropItem( new BaasDeed() );
			DropItem( new BarabDeed() );
			DropItem( new BlackwingDeed() );
			DropItem( new BondaraDeed() );
			DropItem( new BondarDeed() );
			DropItem( new DamindDeed() );
			DropItem( new DODDeed() );
			DropItem( new DragiteDeed() );
			DropItem( new DurindfireDeed() );
			DropItem( new EralamDeed() );
			DropItem( new GreenAdeganDeed() );
			DropItem( new HeartDeed() );
			DropItem( new HurrikaineDeed() );
			DropItem( new ImpactDeed() );
			DropItem( new JenruaxDeed() );
			DropItem( new KenobiDeed() );
			DropItem( new KraytDeed() );
			DropItem( new LambentDeed() );
			DropItem( new LavaDeed() );
			DropItem( new LignanDeed() );
			DropItem( new LorridianDeed() );
			DropItem( new MantleDeed() );
			DropItem( new MeditationDeed() );
			DropItem( new NextorDeed() );
			DropItem( new PermafrostDeed() );
			DropItem( new PhondDeed() );
			DropItem( new QixoniDeed() );
			DropItem( new RubatDeed() );
			DropItem( new RuusanDeed() );
			DropItem( new SapithDeed() );
			DropItem( new SigilDeed() );
			DropItem( new SolariDeed() );
			DropItem( new StygiumDeed() );
			DropItem( new SunriderDeed() );
			DropItem( new SyntheticDeed() );
			DropItem( new TyranusDeed() );
			DropItem( new UlricRedemptionDeed() );
			DropItem( new UltimaDeed() );
			DropItem( new UpariDeed() );
			DropItem( new VelmoriteDeed() );
			DropItem( new VexxtalDeed() );
			DropItem( new WinduDeed() );
			DropItem( new LightsaberHiltMold() );
		}
		
		public EmpoweringCrystalBag( Serial serial ) : base( serial )
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