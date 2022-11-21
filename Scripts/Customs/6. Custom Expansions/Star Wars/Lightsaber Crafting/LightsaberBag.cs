using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class LightsaberBag : Backpack
	{
		public override string DefaultName
		{
			get { return "a Lightsaber Bag"; }
		}

		[Constructable]
		public LightsaberBag() : this( 1 )
		{
			Movable = true;
		}

		[Constructable]
		public LightsaberBag( int amount )
		{
			DropItem( new AllyaExileLightsaber() );			// Exile
			DropItem( new AllyaRedemptionLightsaber() );	// Exile
			DropItem( new AnkarresLightsaber() );			// Jedi
			DropItem( new BaasWisdomLightsaber() );			// Exile
			DropItem( new BarabLightsaber() );				// Sith
			DropItem( new BlackwingLightsaber() );			// Sith
			DropItem( new BondaraFollyLightsaber() );		// Jedi
			DropItem( new BondarLightsaber() );				// Jedi
			DropItem( new DamindLightsaber() );				// Sith
			DropItem( new DagobahLightsaber() );			// Jedi
			DropItem( new DragiteLightsaber() );			// Jedi
			DropItem( new DurindfireLightsaber() );			// Jedi
			DropItem( new EralamLightsaber() );				// Sith
			DropItem( new AdeganLightsaber() );				// Jedi
			DropItem( new GuardianLightsaber() );			// Jedi
			DropItem( new HurrikaineLightsaber() );			// Jedi
			DropItem( new ImpactLightsaber() );				// Exile
			DropItem( new JenruaxLightsaber() );			// Jedi
			DropItem( new KenobiLegacyLightsaber() );		// Jedi
			DropItem( new KraytDragonLightsaber() );		// Jedi
			DropItem( new LambentLightsaber() );			// Jedi
			DropItem( new LavaLightsaber() );				// Sith
			DropItem( new LignanLightsaber() );				// Sith
			DropItem( new LorridianLightsaber() );			// Jedi
			DropItem( new MantleLightsaber() );				// Jedi
			DropItem( new MeditationLightsaber() );			// Jedi
			DropItem( new NextorLightsaber() );				// Sith
			DropItem( new PermafrostLightsaber() );			// Exile
			DropItem( new PhondLightsaber() );				// Sith
			DropItem( new QixoniLightsaber() );				// Exile
			DropItem( new RubatLightsaber() );				// Exile
			DropItem( new RuusanLightsaber() );				// Jedi
			DropItem( new SapithLightsaber() );				// Sith
			DropItem( new SigilLightsaber() );				// Sith
			DropItem( new SolariLightsaber() );				// Jedi
			DropItem( new StygiumLightsaber() );			// Sith
			DropItem( new SunriderLightsaber() );			// Jedi
			DropItem( new SyntheticLightsaber() );			// Sith
			DropItem( new TyranusLightsaber() );			// Sith
			DropItem( new UlricRedemptionLightsaber() );	// Exile
			DropItem( new UltimaLightsaber() );				// Jedi
			DropItem( new UpariLightsaber() );				// Jedi
			DropItem( new VelmoriteLightsaber() );			// Exile
			DropItem( new VexxtalLightsaber() );			// Sith
			DropItem( new WinduLightsaber() );				// Jedi
			DropItem( new LightsaberHiltMold() );
		}
		
		public LightsaberBag( Serial serial ) : base( serial )
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