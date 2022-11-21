using System;
using System.Collections.Generic;
using Server;
using Server.Items;
using Server.Spells;
using Server.Engines.CannedEvil;

namespace Server.Mobiles
{
    public class Mephitis : BaseChampion
    {
        public override ChampionSkullType SkullType { get { return ChampionSkullType.Venom; } }

        public override Type[] UniqueArtifacts{ get { return new Type[] {
			typeof( Calm ) }; } }

         public override Type[] SharedArtifacts{ get { return new Type[] {
			typeof( ANecromancerShroud ),
			typeof( EmbroideredOakLeafCloak ),
			typeof( TheMostKnowledgePerson ),
			typeof( OblivionsNeedle ) }; } }

         public override Type[] DecorationArtifacts{ get { return new Type[] {
            typeof( Spiderweb ),
            typeof( EggCaseWeb ),
			typeof( MonsterStatuette ) }; } }

         public override MonsterStatuetteType[] StatueTypes{ get{ return new MonsterStatuetteType[] { 	
            MonsterStatuetteType.Spider }; } }

		[Constructable]
		public Mephitis() : base( AIType.AI_Melee )
		{
			Body = 173;
			Name = "Mephitis";

			BaseSoundID = 0x183;

			SetStr( 505, 1000 );
			SetDex( 102, 300 );
			SetInt( 402, 600 );

			SetHits( 3000 );
			SetStam( 105, 600 );

			SetDamage( 21, 33 );

			SetDamageType( ResistanceType.Physical, 50 );
			SetDamageType( ResistanceType.Poison, 50 );

			SetResistance( ResistanceType.Physical, 75, 80 );
			SetResistance( ResistanceType.Fire, 60, 70 );
			SetResistance( ResistanceType.Cold, 60, 70 );
			SetResistance( ResistanceType.Poison, 100 );
			SetResistance( ResistanceType.Energy, 60, 70 );

			SetSkill( SkillName.MagicResist, 70.7, 140.0 );
			SetSkill( SkillName.Tactics, 97.6, 100.0 );
			SetSkill( SkillName.Wrestling, 97.6, 100.0 );

			Fame = 22500;
			Karma = -22500;

			VirtualArmor = 80;
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.UltraRich, 4 );
		}
 
		public override void OnThink()
		{
			base.OnThink();

			if ( Combatant != null && m_NextWeb < DateTime.Now )
				DoWebAttack();
		}

		public override Poison PoisonImmune{ get{ return Poison.Lethal; } }
		public override Poison HitPoison{ get{ return Poison.Lethal; } }

		#region Web Attack
		private DateTime m_NextWeb;

        public void DoWebAttack()
        {
            List<Mobile> targets = new List<Mobile>();

            foreach (Mobile m in GetMobilesInRange(RangePerception))
                if (CanBeHarmful(m) && m.Player && !InRange(m, 1) && !m.Paralyzed)
                    targets.Add(m);

            if (targets.Count > 0)
            {
                Mobile target = targets[Utility.Random(targets.Count)];
                TimeSpan delay = TimeSpan.FromSeconds(GetDistanceToSqrt(target) / 15.0);
                Effects.SendMovingEffect(this, target, 0x10D2, 20, 1, false, false);
                Timer.DelayCall<Mobile>(delay, new TimerStateCallback<Mobile>(Entangle), target);
            }

            m_NextWeb = DateTime.Now + TimeSpan.FromSeconds(Utility.RandomMinMax(5, 15));
        }

        public void Entangle( Mobile m )
		{
			Point3D p = Location;

			if ( SpellHelper.FindValidSpawnLocation( Map, ref p, true ) )
			{
				TimeSpan delay = TimeSpan.FromSeconds( Utility.RandomMinMax( 3, 6 ) );
				m.MoveToWorld( p, Map );
				m.Freeze( delay );
				m.SendLocalizedMessage( 1042555 ); // You become entangled in the spider web.

				SpiderWeb web = new SpiderWeb( delay );
				p.Z += 2;
				web.MoveToWorld( p, Map );

				Combatant = m;
			}
		}

		private class SpiderWeb : Static
		{
			public SpiderWeb( TimeSpan delay ) : base( 0x10D2 )
			{
				Timer.DelayCall( delay, new TimerCallback( Delete ) );
			}

			public SpiderWeb( Serial serial ) : base( serial )
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

        public Mephitis(Serial serial)
            : base(serial)
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