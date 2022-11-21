using System;
using System.Collections.Generic;
using Server;
using Server.Items;
using Server.Spells;
using Server.Engines.CannedEvil;

namespace Server.Mobiles
{
    public class Amathaunta : BaseExpanseBoss
    {
		[Constructable]
		public Amathaunta() : base( AIType.AI_Melee )
		{
			if ( Utility.RandomDouble() > 0.55 )
			{
				Body = 1068;
			}
			else if ( Utility.RandomDouble() < 0.30 )
			{
				Body = 1071;
			}
			else
			{
				Body = 774;
			}

			Name = "Amathaunta";
			Title = "the ancient water goddess";
			Hue = 400;
			
			AddItem(new LightSource());
			//BaseSoundID = 0x183;
			SetStr( 1505, 2000 );
			SetDex( 102, 300 );
			SetInt( 602, 800 );
			SetHits( 65000 );
			SetStam( 105, 600 );
			SetDamage( 25, 35 );

			SetDamageType( ResistanceType.Physical, 20 );
			SetDamageType( ResistanceType.Cold, 40 );
			SetDamageType( ResistanceType.Energy, 40 );
			SetResistance( ResistanceType.Physical, 75, 80 );
			SetResistance( ResistanceType.Fire, 60, 70 );
			SetResistance( ResistanceType.Cold, 60, 70 );
			SetResistance( ResistanceType.Poison, 50 );
			SetResistance( ResistanceType.Energy, 60, 70 );
			SetSkill( SkillName.MagicResist, 70.7, 140.0 );
			SetSkill( SkillName.Tactics, 97.6, 100.0 );
			SetSkill( SkillName.Wrestling, 97.6, 100.0 );

			Fame = 1250;
			Karma = -1250;
			VirtualArmor = 80;
		}
		
		public override bool NoGoodies{ get{ return true; } }

		public override void GenerateLoot()
		{
			AddLoot( LootPack.AosSuperBoss, 5 );
			AddLoot( LootPack.UltraRich, 2 );
		}
 
		public override void OnDeath( Container c )
		{
			base.OnDeath( c );		
			c.DropItem( new RewardScroll() );
			if ( Utility.RandomDouble() < 0.95 )
			c.DropItem( new RewardScroll() );
			if ( Utility.RandomDouble() < 0.85 )
			c.DropItem( new RewardScroll() );
			if ( Utility.RandomDouble() < 0.75 )
			c.DropItem( new RewardScroll() );
			if ( Utility.RandomDouble() < 0.50 )
			c.DropItem( new RewardScroll() );
			if ( Utility.RandomDouble() < 0.25 )
			c.DropItem( new RewardScroll() );
			if ( Utility.RandomDouble() < 0.15 )
			c.DropItem( new RewardScroll() );
			if ( Utility.RandomDouble() < 0.10 )
			c.DropItem( new RewardScroll() );
			if ( Utility.RandomDouble() < 0.05 )
			c.DropItem( new RewardScroll() );
			if ( Utility.RandomDouble() < 0.6 )				
				c.DropItem( new ParrotItem() );
		}
		
		public override void OnThink()
		{
			base.OnThink();
			if ( Combatant != null && m_NextWV < DateTime.Now )
				DoWVAttack();
		}

		public override bool GivesMinorArtifact{ get{ return true; } }
		public override Poison PoisonImmune{ get{ return Poison.Lethal; } }
		public override Poison HitPoison{ get{ return Poison.Lethal; } }
		#region Water Vortex Attack
		private DateTime m_NextWV;
        public void DoWVAttack()
        {
            List<Mobile> targets = new List<Mobile>();

            foreach (Mobile m in GetMobilesInRange(RangePerception))
                if (CanBeHarmful(m) && m.Player && !InRange(m, 1) && !m.Paralyzed)
                    targets.Add(m);

            if (targets.Count > 0)
            {
                Mobile target = targets[Utility.Random(targets.Count)];
                TimeSpan delay = TimeSpan.FromSeconds(GetDistanceToSqrt(target) / 15.0);
                Effects.SendMovingEffect(this, target, 0x20ED, 10, 1, false, false, 0x43D, 0 );	//0x10D2
                Timer.DelayCall<Mobile>(delay, new TimerStateCallback<Mobile>(Entangle), target);
            }

            m_NextWV = DateTime.Now + TimeSpan.FromSeconds(Utility.RandomMinMax(5, 15));
        }

        public void Entangle( Mobile m )
		{
			Point3D p = Location;

			if ( SpellHelper.FindValidSpawnLocation( Map, ref p, true ) )
			{
				TimeSpan delay = TimeSpan.FromSeconds( Utility.RandomMinMax( 3, 6 ) );
				m.MoveToWorld( p, Map );
				m.Freeze( delay );
				m.SendMessage( "You are trapped within a powerful water vortex." ); // You become entangled in the spider web.
				WaterVortex vortex = new WaterVortex( delay );
				p.Z += 2;
				vortex.MoveToWorld( p, Map );
				Combatant = m;
			}
		}

		private class WaterVortex : Static
		{
			public WaterVortex( TimeSpan delay ) : base( 0x20ED )
			{
				Hue = 1085;
				Timer.DelayCall( delay, new TimerCallback( Delete ) );
			}

			public WaterVortex( Serial serial ) : base( serial )
			{
			}

			public override void Serialize( GenericWriter writer )
			{
				base.Serialize( writer );
				writer.WriteEncodedInt( 0 ); // version
			}

			public override void Deserialize( GenericReader reader )
			{
				base.Deserialize( reader );
				int version = reader.ReadEncodedInt();

				Delete();
			}
		}
		#endregion

        public Amathaunta(Serial serial): base(serial)
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