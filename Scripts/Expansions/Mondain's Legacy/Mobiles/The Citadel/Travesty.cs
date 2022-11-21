using System;
using System.Collections;
using System.Collections.Generic;
using Server;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "a travesty's corpse" )]
	public class Travesty : BasePeerless
	{
		[Constructable]
		public Travesty() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a travesty";
			Body = 0x108;

			SetStr( 909, 949 );
			SetDex( 901, 948 );
			SetInt( 903, 947 );

			SetHits( 35000 );

			SetDamage( 25, 30 );
			
			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 52, 67 );
			SetResistance( ResistanceType.Fire, 51, 68 );
			SetResistance( ResistanceType.Cold, 51, 69 );
			SetResistance( ResistanceType.Poison, 51, 70 );
			SetResistance( ResistanceType.Energy, 50, 68 );

			SetSkill( SkillName.Wrestling, 100.1, 119.7 );
			SetSkill( SkillName.Tactics, 102.3, 118.5 );
			SetSkill( SkillName.MagicResist, 101.2, 119.6 );
			SetSkill( SkillName.Anatomy, 100.1, 117.5 );

			Fame = 8000;
			Karma = -8000;

			VirtualArmor = 50;
			PackTalismans( 5 );
			PackResources( 8 );
		}

		public Travesty( Serial serial ) : base( serial )
		{
		}

		public override void OnDeath( Container c )
		{
			base.OnDeath( c );
			
			c.DropItem( new EyeOfTheTravesty() );
			c.DropItem( new OrdersFromMinax() );
			c.DropItem( new RewardScroll() );
			
			if ( Utility.RandomDouble() < 0.25 )
			c.DropItem( new RewardScroll() );
			if ( Utility.RandomDouble() < 0.15 )
			c.DropItem( new RewardScroll() );
			if ( Utility.RandomDouble() < 0.10 )
			c.DropItem( new RewardScroll() );
			if ( Utility.RandomDouble() < 0.05 )
			c.DropItem( new RewardScroll() );
			if ( Utility.RandomDouble() < 0.001 )
			c.DropItem( new DiscountCoupon() );

			switch ( Utility.Random( 3 ) )
			{
				case 0: c.DropItem( new TravestysSushiPreparations() ); break;
				case 1: c.DropItem( new TravestysFineTeakwoodTray() ); break;
				case 2: c.DropItem( new TravestysCollectionOfShells() ); break;
			}

			if ( Utility.RandomDouble() < 0.6 )
				c.DropItem( new ParrotItem() );

			if ( Utility.RandomDouble() < 0.1 )
				c.DropItem( new TragicRemainsOfTravesty() );

			if ( Utility.RandomDouble() < 0.05 )
				c.DropItem( new ImprisonedDog() );

			if ( Utility.RandomDouble() < 0.05 )
				c.DropItem( new MarkOfTravesty() );

			if ( Utility.RandomDouble() < 0.025 )
				c.DropItem( new CrimsonCincture() );
				
			if ( Utility.RandomDouble() < 0.025 )
				c.DropItem( new HumilityCloak() );

            if (Utility.RandomDouble() < 0.025)
            {
                switch (Utility.Random(7))
                {
                    case 0: c.DropItem(new AssassinLegs()); break;
                    case 1: c.DropItem(new AssassinArms()); break;
                    case 2: c.DropItem(new AssassinGloves()); break;
                    case 3: c.DropItem(new MalekisHonor()); break;
                    case 4: c.DropItem(new JusticeBreastplate()); break;
                    case 5: c.DropItem(new CompassionArms()); break;
                    case 6: c.DropItem(new ValorGauntlets()); break;
                }
            }
		}

		public override bool CanRummageCorpses{ get{ return true; } }
		public override Poison PoisonImmune{ get{ return Poison.Lethal; } }
		public override bool CanAnimateDead{ get{ return true; } }
		public override BaseCreature Animates{ get{ return new LichLord(); } }
		public override bool GivesMinorArtifact{ get{ return true; } }
		public override int TreasureMapLevel{ get{ return 5; } }

		public override void GenerateLoot()
		{
			AddLoot( LootPack.AosSuperBoss, 8 );
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

		private bool m_SpawnedHelpers;
		private Timer m_Timer;
		private string m_Name;
		private int m_Hue;

		public override void OnThink()
		{
			base.OnThink();

			if ( Combatant == null )
				return;

			if ( Combatant.Player && Name != Combatant.Name )
				Morph();
		}

		public virtual void Morph()
		{
			m_Name = Name;
			m_Hue = Hue;

			Body = Combatant.Body; 
			Hue = Combatant.Hue; 
			Name = Combatant.Name;
			Female = Combatant.Female;
			Title = Combatant.Title;

			foreach ( Item item in Combatant.Items )
			{
				if ( item.Layer != Layer.Backpack && item.Layer != Layer.Mount && item.Layer != Layer.Bank )
					if ( FindItemOnLayer( item.Layer ) == null )
						AddItem( new ClonedItem( item ) );
			}

			PlaySound( 0x511 );
			FixedParticles( 0x376A, 1, 14, 5045, EffectLayer.Waist );

			if ( m_Timer != null )
				m_Timer.Stop();

			m_Timer = Timer.DelayCall( TimeSpan.FromSeconds( 5 ), TimeSpan.FromSeconds( 5 ), new TimerCallback( EndMorph ) );
		}

		public void DeleteItems()
		{
			for ( int i = Items.Count - 1; i >= 0; i -- )
				if ( Items[ i ] is ClonedItem )
					Items[ i ].Delete();

			if ( Backpack != null )
			{
				for ( int i = Backpack.Items.Count - 1; i >= 0; i -- )
					if ( Backpack.Items[ i ] is ClonedItem )
						Backpack.Items[ i ].Delete();
			}
		}

		public virtual void EndMorph()
		{
			if ( Combatant != null && Name == Combatant.Name )
				return;

			DeleteItems();

			if ( m_Timer != null )
			{
				m_Timer.Stop();		
				m_Timer = null;	
			}

			if ( Combatant != null )
			{
				Morph();
				return;
			}

			Body = 264;
			Title = null;
			Name = m_Name;
			Hue = m_Hue;

			PlaySound( 0x511 );
			FixedParticles( 0x376A, 1, 14, 5045, EffectLayer.Waist );
		}

		public override bool OnBeforeDeath()
		{
			if ( m_Timer != null )
				m_Timer.Stop();

			return base.OnBeforeDeath();
		}

		public override void OnAfterDelete()
		{
			if ( m_Timer != null )
				m_Timer.Stop();

			base.OnAfterDelete();
		}

		#region Spawn Helpers
		public override bool CanSpawnHelpers{ get{ return true; } }
		public override int MaxHelpersWaves{ get{ return 1; } }

		public override bool CanSpawnWave()
		{
			if ( Hits > 1100 )
				m_SpawnedHelpers = false;

			return !m_SpawnedHelpers && Hits < 1000;
		}

		public override void SpawnHelpers()
		{
			m_SpawnedHelpers = true;

			for ( int i = 0; i < 10; i++ )
			{
				switch ( Utility.Random( 3 ) )
				{
					case 0: SpawnHelper( new DragonsFlameMage(), 25 ); break;
					case 1: SpawnHelper( new SerpentsFangAssassin(), 25 ); break;
					case 2: SpawnHelper( new TigersClawThief(), 25 ); break;
				}
			}
		}
		#endregion

		private class ClonedItem : Item
		{	
			public ClonedItem( Item oItem ) : base( oItem.ItemID )
			{
				Name = oItem.Name;
				Weight = oItem.Weight;
				Hue = oItem.Hue;
				Layer = oItem.Layer;
			}

			public override DeathMoveResult OnParentDeath( Mobile parent )
			{
				return DeathMoveResult.RemainEquiped;
			}

			public override DeathMoveResult OnInventoryDeath( Mobile parent )
			{
				Delete();
				return base.OnInventoryDeath( parent );
			}

			public ClonedItem( Serial serial ) : base( serial )
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
}