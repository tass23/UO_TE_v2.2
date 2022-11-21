using System;
using Server;
using Server.Items;
using System.Collections;
using System.Collections.Generic;
using Server.Targeting;
using Server.Mobiles;
using Server.Misc;

namespace Server.Mobiles
{
	[CorpseName( "Marka Ragnos' corpse" )]
	public class MarkaRagnos : BasePeerless
	{
		[Constructable]
		public MarkaRagnos() : base (AIType.AI_Sith, FightMode.Closest, 10, 1, 0.2, 0.4)
		{
			Name = "Marka Ragnos";
			Title = "the Dark Force Master";
			Hue = 1786;
			Body = 713;
			SetStr( 967, 1145 );
			SetDex( 129, 139 );
			SetInt( 967, 1145 );
			SetHits( 45000, 50000 );
			SetMana( 10000 );

            SetDamage(10, 30);
			SetDamageType( ResistanceType.Physical, 20 );
			SetDamageType( ResistanceType.Fire, 20 );
			SetDamageType( ResistanceType.Cold, 20 );
			SetDamageType( ResistanceType.Poison, 20 );
			SetDamageType( ResistanceType.Energy, 20 );
			SetResistance( ResistanceType.Physical, 65, 75 );
			SetResistance( ResistanceType.Fire, 70, 80 );
			SetResistance( ResistanceType.Cold, 70, 80 );
			SetResistance( ResistanceType.Poison, 70, 80 );
			SetResistance( ResistanceType.Energy, 70, 80 );
			SetSkill( SkillName.Anatomy, 116.1, 120.6 );
			SetSkill( SkillName.EvalInt, 113.8, 124.7 );
			SetSkill( SkillName.Magery, 110.1, 123.2 );
            SetSkill(SkillName.Spellweaving, 110.1, 123.2);
			SetSkill( SkillName.Meditation, 118.2, 127.8 );
			SetSkill( SkillName.MagicResist, 110.0, 123.2 );
			SetSkill( SkillName.Tactics, 112.2, 122.6 );
			SetSkill( SkillName.Wrestling, 118.9, 128.6 );
			
			Timer.DelayCall( TimeSpan.FromSeconds( 60 ), new TimerCallback( SpawnSithRevenant ) );
		}

		public override void OnDeath( Container c )
		{
			base.OnDeath( c );

			if (Utility.RandomDouble() < 0.55)
			{
				switch ( Utility.Random( 45 ) )
				{
					case 0: c.DropItem( new AllyaExileDeed() ); break;
					case 1: c.DropItem( new AllyaRedemptionDeed() ); break;
					case 2: c.DropItem( new AnkarresDeed() ); break;
					case 3: c.DropItem( new BaasDeed() ); break;
					case 4: c.DropItem( new BarabDeed() ); break;
					case 5: c.DropItem( new BlackwingDeed() ); break;
					case 6: c.DropItem( new BondaraDeed() ); break;
					case 7: c.DropItem( new BondarDeed() ); break;
					case 8: c.DropItem( new DamindDeed() ); break;
					case 9: c.DropItem( new DODDeed() ); break;
					case 10: c.DropItem( new DragiteDeed() ); break;
					case 11: c.DropItem( new DurindfireDeed() ); break;
					case 12: c.DropItem( new EralamDeed() ); break;
					case 13: c.DropItem( new GreenAdeganDeed() ); break;
					case 14: c.DropItem( new HeartDeed() ); break;
					case 15: c.DropItem( new HurrikaineDeed() ); break;
					case 16: c.DropItem( new ImpactDeed() ); break;
					case 17: c.DropItem( new JenruaxDeed() ); break;
					case 18: c.DropItem( new KenobiDeed() ); break;
					case 19: c.DropItem( new KraytDeed() ); break;
					case 20: c.DropItem( new LambentDeed() ); break;
					case 21: c.DropItem( new LavaDeed() ); break;
					case 22: c.DropItem( new LignanDeed() ); break;
					case 23: c.DropItem( new LorridianDeed() ); break;
					case 24: c.DropItem( new MantleDeed() ); break;
					case 25: c.DropItem( new MeditationDeed() ); break;
					case 26: c.DropItem( new NextorDeed() ); break;
					case 27: c.DropItem( new PermafrostDeed() ); break;
					case 28: c.DropItem( new PhondDeed() ); break;
					case 29: c.DropItem( new QixoniDeed() ); break;
					case 30: c.DropItem( new RubatDeed() ); break;
					case 31: c.DropItem( new RuusanDeed() ); break;
					case 32: c.DropItem( new SapithDeed() ); break;
					case 33: c.DropItem( new SigilDeed() ); break;
					case 34: c.DropItem( new SolariDeed() ); break;
					case 35: c.DropItem( new StygiumDeed() ); break;
					case 36: c.DropItem( new SunriderDeed() ); break;
					case 37: c.DropItem( new SyntheticDeed() ); break;
					case 38: c.DropItem( new TyranusDeed() ); break;
					case 39: c.DropItem( new UlricRedemptionDeed() ); break;
					case 40: c.DropItem( new UltimaDeed() ); break;
					case 41: c.DropItem( new UpariDeed() ); break;
					case 42: c.DropItem( new VelmoriteDeed() ); break;
					case 43: c.DropItem( new VexxtalDeed() ); break;
					case 44: c.DropItem( new WinduDeed () ); break;
				}
			}
			
			if ( Utility.RandomDouble() < 0.025 )
				c.DropItem( new MarkaGauntlets() );
			if ( Utility.RandomDouble() < 0.05 )
				c.DropItem( new SithHoundImprisonedInCrystal() );
			if ( Utility.RandomDouble() < 0.10 )
				c.DropItem( new MarkaSkull() );
        }

        public override void GenerateLoot()
        {
            AddLoot(LootPack.AosSuperBoss, 4);
            AddLoot(LootPack.Gems, 8);
        }

        public override bool Unprovokable { get { return true; } }
        public override bool BardImmune { get { return true; } }

        public override void OnDamagedBySpell(Mobile caster)
        {
            if (this.Map != null && caster != this && 0.70 > Utility.RandomDouble())
            {
                Map = caster.Map;
                Location = caster.Location;
                Combatant = caster;
                Effects.PlaySound(this.Location, this.Map, 0x1FE);
            }

            base.OnDamagedBySpell(caster);
        }

        private DateTime m_NextTerror;

        public override void OnMovement(Mobile m, Point3D oldLocation)
        {
            base.OnMovement(m, oldLocation);

            if (m_NextTerror < DateTime.Now && m != null && InRange(m.Location, 10) && m.AccessLevel == AccessLevel.Player)
            {
                m.Frozen = true;
                m.SendLocalizedMessage(1080342, Name, 33); // Terror slices into your very being, destroying any chance of resisting ~1_name~ you might have had
                Timer.DelayCall(TimeSpan.FromSeconds(10), new TimerStateCallback(Terrorize), m);
            }
        }

        private void Terrorize(object o)
        {
            if (o is Mobile)
            {
                Mobile m = (Mobile)o;
                m.Frozen = false;
                m.SendLocalizedMessage(1005603); // You can move again!
                m_NextTerror = DateTime.Now + TimeSpan.FromMinutes(1);
            }
        }

        public void DrainMana()
        {
            if (this.Map == null)
                return;

            ArrayList list = new ArrayList();

            foreach (Mobile m in this.GetMobilesInRange(8))
            {
                if (m == this || !CanBeHarmful(m))
                    continue;

                if (m is BaseCreature && (((BaseCreature)m).Controlled || ((BaseCreature)m).Summoned || ((BaseCreature)m).Team != this.Team))
                    list.Add(m);
                else if (m.Player)
                    list.Add(m);
            }

            foreach (Mobile m in list)
            {
                DoHarmful(m);
                m.FixedParticles(0x374A, 10, 15, 5013, 0x496, 0, EffectLayer.Waist);
                m.PlaySound(0x231);
                m.SendMessage("You feel the mana drain out of you!");
                int toDrain = Utility.RandomMinMax(40, 60);
                Mana += toDrain;
                m.Mana -= toDrain;
            }
        }

        public override void OnGaveMeleeAttack(Mobile defender)
        {
            base.OnGaveMeleeAttack(defender);

            if (0.25 >= Utility.RandomDouble())
                DrainMana();
        }

        public override void OnGotMeleeAttack(Mobile attacker)
        {
            base.OnGotMeleeAttack(attacker);

            if (0.25 >= Utility.RandomDouble())
                DrainMana();
        }

		public MarkaRagnos( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}

		#region Helpers
		public override bool CanSpawnHelpers{ get { return true; } }
		public override int MaxHelpersWaves{ get { return 2; } }

		public override void SpawnHelpers()
		{
			int count = 4;

			if ( Altar != null )
			{
				count = Math.Min( Altar.Fighters.Count, 4 );

				for ( int i = 0; i < count; i++ )
				{
					Mobile fighter = Altar.Fighters[ i ];

					if ( CanBeHarmful( fighter ) )
					{
						SithRevenant sithrevenant = new SithRevenant();
						sithrevenant.Combatant = fighter;
						SpawnHelper( sithrevenant, GetSpawnPosition( fighter.Location, fighter.Map, 2 ) );
						fighter.SendMessage( "Marka Ragnos summons forth evil forms that are twisted by the Dark Side." ); // A twisted satyr scrambles onto the branch beside you and attacks!
					}
				}
			}
			else
			{
				for ( int i = 0; i < count; i++ )
					SpawnHelper( new SithRevenant(), 4 );
			}
		}	

		public void SpawnSithRevenant()
		{
			SpawnHelper( new SithRevenant(), 182, 275, 5 );
			SpawnHelper( new SithRevenant(), 184, 279, 7 );
			SpawnHelper( new SithRevenant(), 178, 279, 6 ); 
			SpawnHelper( new SithRevenant(), 177, 274, 4 ); 
		}
		#endregion
	}
}