/* Based on Neira, still to get detailed information on the Primeval Lich */
using System;
using System.Collections;
using Server;
using Server.Items;
using Server.Engines.CannedEvil;
using System.Collections.Generic;
using Server.Mobiles;
using Server.Spells;
using Server.Spells.Sixth;
using Server.Targeting;

namespace Server.Mobiles
{
	public class PrimevalLich : BaseChampion
	{
        public virtual bool CanDiscord { get { return true; } }
        public virtual int DiscordDuration { get { return 20; } }
        public virtual int DiscordMinDelay { get { return 5; } }
        public virtual int DiscordMaxDelay { get { return 22; } }
        public virtual double DiscordModifier { get { return 0.28; } }
        public virtual int PerceptionRange { get { return 8; } }

		public override ChampionSkullType SkullType{ get{ return ChampionSkullType.Infuse; } }

        public override Type[] UniqueArtifacts{ get{ return new Type[] { typeof(BansheesCall), typeof(CastOffZombieSkin), typeof(ProtectoroftheBattleMage), typeof(ChannelersDefender) }; } }

        public override Type[] SharedArtifacts{ get { return new Type[] { typeof(TokenOfHolyFavor) }; } }

        public override Type[] DecorationArtifacts{ get { return new Type[] { }; } }
 
		public override MonsterStatuetteType[] StatueTypes { get { return new MonsterStatuetteType[] { }; } }
		
		[Constructable]
		public PrimevalLich() : base( AIType.AI_Mage )
		{
			Name = "The Primeval Lich";
			Body = 830;

			SetStr( 500, 600 );
			SetDex( 130, 140 );
			SetInt( 1000, 1200 );

			SetHits( 120000 );
			SetStam( 130, 140 );
            SetMana(4500, 5500);

			SetDamage( 17, 21 );

			SetDamageType( ResistanceType.Physical, 20 );
            SetDamageType(ResistanceType.Fire, 20);
            SetDamageType(ResistanceType.Cold, 20);
            SetDamageType(ResistanceType.Energy, 20);
            SetDamageType(ResistanceType.Poison, 20);

			SetResistance( ResistanceType.Physical, 20 );
			SetResistance( ResistanceType.Fire, 20 );
			SetResistance( ResistanceType.Cold, 20 );
			SetResistance( ResistanceType.Poison, 20 );
			SetResistance( ResistanceType.Energy, 20 );

			SetSkill( SkillName.EvalInt, 90, 120.0 );
			SetSkill( SkillName.Magery, 90, 120.0 );
			SetSkill( SkillName.Meditation, 100, 120.0 );
            SetSkill( SkillName.Necromancy, 120.0 );
            SetSkill (SkillName.SpiritSpeak, 120.0 );
			SetSkill( SkillName.MagicResist, 120, 140.0 );
			SetSkill( SkillName.Tactics, 90, 120 );
			SetSkill( SkillName.Wrestling, 100, 120 );

			Fame = 22500;
			Karma = -22500;

			VirtualArmor = 80;
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.UltraRich, 3 );
			AddLoot( LootPack.Meager );
		}

        public override void OnDeath(Container c)
        {
            base.OnDeath(c);

            c.DropItem(new PrimalLichDust());
            c.DropItem(new RisingColossusScroll());

        }


		public void ChangeCombatant()
		{
			ForceReacquire();
			BeginFlee( TimeSpan.FromSeconds( 2.5 ) );
		}

		public override bool AlwaysMurderer{ get{ return true; } }
		public override bool BardImmune{ get{ return !Core.SE; } }
		public override bool Unprovokable{ get{ return Core.SE; } }
		public override bool Uncalmable{ get{ return Core.SE; } }
        public override Poison PoisonImmune { get { return Poison.Lethal; } }

		public override bool ShowFameTitle{ get{ return false; } }
		public override bool ClickTitle{ get{ return false; } }

        public override int GetIdleSound()
		{
            return 0x622;
        }

        public override int GetAngerSound()
        {
            return 0x61F;
		}

        public override int GetDeathSound()
		{
            return 0x620;
        }

        public override int GetHurtSound()
        {
            return 0x621;
        }

        public override void OnThink()
        {
            if (CanDiscord && m_NextDiscordTime <= DateTime.Now)
            {
                Mobile target = Combatant;

                if (target != null && target.InRange(this, PerceptionRange) && CanBeHarmful(target))
                    Discord(target);
            }
        }

        private DateTime m_NextDiscordTime;

        public void Discord(Mobile target)
        {
            if (Utility.RandomDouble() < 0.9)
            {
                target.AddSkillMod(new TimedSkillMod(SkillName.Magery, true, Combatant.Skills.Magery.Base * DiscordModifier * -1, TimeSpan.FromSeconds(DiscordDuration)));
                target.AddSkillMod(new TimedSkillMod(SkillName.Necromancy, true, Combatant.Skills.Necromancy.Base * DiscordModifier * -1, TimeSpan.FromSeconds(DiscordDuration)));
                target.AddSkillMod(new TimedSkillMod(SkillName.Tactics, true, Combatant.Skills.Tactics.Base * DiscordModifier * -1, TimeSpan.FromSeconds(DiscordDuration)));
                target.AddSkillMod(new TimedSkillMod(SkillName.Swords, true, Combatant.Skills.Swords.Base * DiscordModifier * -1, TimeSpan.FromSeconds(DiscordDuration)));
                target.AddSkillMod(new TimedSkillMod(SkillName.Meditation, true, Combatant.Skills.Meditation.Base * DiscordModifier * -1, TimeSpan.FromSeconds(DiscordDuration)));
                target.AddSkillMod(new TimedSkillMod(SkillName.Focus, true, Combatant.Skills.Focus.Base * DiscordModifier * -1, TimeSpan.FromSeconds(DiscordDuration)));
                target.AddSkillMod(new TimedSkillMod(SkillName.Chivalry, true, Combatant.Skills.Chivalry.Base * DiscordModifier * -1, TimeSpan.FromSeconds(DiscordDuration)));
                target.AddSkillMod(new TimedSkillMod(SkillName.Wrestling, true, Combatant.Skills.Wrestling.Base * DiscordModifier * -1, TimeSpan.FromSeconds(DiscordDuration)));
                target.AddSkillMod(new TimedSkillMod(SkillName.Spellweaving, true, Combatant.Skills.Spellweaving.Base * DiscordModifier * -1, TimeSpan.FromSeconds(DiscordDuration)));


                Timer.DelayCall(TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(1), (int)DiscordDuration, new TimerStateCallback(Animate), target);

                target.SendMessage("The Lich's touch weakens all of your fighting skills!");
                target.PlaySound(0x458);////
            }
            else
            {
                target.SendMessage("The Lich barely misses touching you, saving you from harm!"); 
                target.PlaySound(0x458);/////
            }

            m_NextDiscordTime = DateTime.Now + TimeSpan.FromSeconds(DiscordMinDelay + Utility.RandomDouble() * DiscordMaxDelay);
        }

        private void Animate(object state)
        {
            if (state is Mobile)
            {
                Mobile mob = (Mobile)state;

                mob.FixedEffect(0x376A, 1, 32);
            }
        }

        public void SpawnShadowDwellers(Mobile target)
        {
            Map map = this.Map;

            if (map == null)
                return;

            int newShadowDwellers = Utility.RandomMinMax(2, 3);

            for (int i = 0; i < newShadowDwellers; ++i)
            {
                ShadowDweller shadowdweller = new ShadowDweller();

                shadowdweller.Team = this.Team;
                shadowdweller.FightMode = FightMode.Closest;

                bool validLocation = false;
                Point3D loc = this.Location;

                for (int j = 0; !validLocation && j < 10; ++j)
                {
                    int x = X + Utility.Random(3) - 1;
                    int y = Y + Utility.Random(3) - 1;
                    int z = map.GetAverageZ(x, y);

                    if (validLocation = map.CanFit(x, y, this.Z, 16, false, false))
                        loc = new Point3D(x, y, Z);
                    else if (validLocation = map.CanFit(x, y, z, 16, false, false))
                        loc = new Point3D(x, y, z);
                }

                shadowdweller.MoveToWorld(loc, map);
                shadowdweller.Combatant = target;
            }
		}

		public override void AlterDamageScalarFrom( Mobile caster, ref double scalar )
		{
            if (0.05 >= Utility.RandomDouble())
                SpawnShadowDwellers(caster);
        }

        public override void OnGaveMeleeAttack(Mobile defender)
        {
            base.OnGaveMeleeAttack(defender);

            if (0.8 >= Utility.RandomDouble())
                Lightning();
		}

        public void Lightning()
		{
            Map map = this.Map;

            if (map == null)
				return;

            ArrayList targets = new ArrayList();

            foreach (Mobile m in this.GetMobilesInRange(15))
			{
                if (m == this || !CanBeHarmful(m))
                    continue;

                if (m is BaseCreature && (((BaseCreature)m).Controlled || ((BaseCreature)m).Summoned || ((BaseCreature)m).Team != this.Team))
                    targets.Add(m);
                else if (m.Player)
                    targets.Add(m);
			}

            PlaySound(0x2A);

            for (int i = 0; i < targets.Count; ++i)
			{
                Mobile m = (Mobile)targets[i];

                double damage = m.Hits * 0.6;

                if (damage < 100.0)
                    damage = 100.0;
                else if (damage > 200.0)
                    damage = 200.0;

                DoHarmful(m);

                AOS.Damage(m, this, (int)damage, 0, 0, 100, 0, 0);

                if (m.Alive && m.Body.IsHuman && !m.Mounted)
                    m.Animate(20, 7, 1, true, false, 0); // take hit
            }
        }
        
        public override void OnGotMeleeAttack(Mobile attacker)
        {
            base.OnGotMeleeAttack(attacker);

            if (0.05 >= Utility.RandomDouble())
                SpawnShadowDwellers(attacker);
        }
      
		
    

        public override void OnDamagedBySpell(Mobile attacker)
        {

             if (this.Map != null && attacker != this && 0.05 > Utility.RandomDouble())
            {
                Combatant = attacker;
                Map = attacker.Map;
                Location = attacker.Location;

                switch (Utility.Random(4))
                {
                    case 0: attacker.Location = new Point3D(6974, 994,-15); break;
                    case 1: attacker.Location = new Point3D(6977, 1025, -15); break;
                    case 2: attacker.Location = new Point3D(7020, 1027, -15); break;
                    case 3: attacker.Location = new Point3D(6999, 978, -15); break;
                   
                }
                attacker.SendMessage("You are teleported away, the Lich is Taunting you!");
                AOS.Damage(attacker, Utility.RandomMinMax(50, 65), 0, 100, 0, 0, 0);
                attacker.MoveToWorld(attacker.Location, attacker.Map);
                attacker.FixedParticles(0x376A, 9, 32, 0x13AF, EffectLayer.Waist);
                attacker.PlaySound(0x1FE);

			}

            base.OnDamagedBySpell(attacker);

            DoCounter(attacker);
		}

        private void DoCounter(Mobile attacker)
		{
            if (this.Map == null)
                return;

            if (attacker is BaseCreature && ((BaseCreature)attacker).BardProvoked)
                return;

            if (0.20 > Utility.RandomDouble())
			{
                Mobile target = null;

                if (attacker is BaseCreature)
                {
                    Mobile m = ((BaseCreature)attacker).GetMaster();

                    if (m != null)
                        target = m;
			}

                if (target == null || !target.InRange(this, 15))
                    target = attacker;

                this.Animate(10, 4, 1, true, false, 0);

                ArrayList targets = new ArrayList();

                foreach (Mobile m in target.GetMobilesInRange(8))
			{
                    if (m == this || !CanBeHarmful(m))
                        continue;

                    if (m is BaseCreature && (((BaseCreature)m).Controlled || ((BaseCreature)m).Summoned || ((BaseCreature)m).Team != this.Team))
                        targets.Add(m);
                    else if (m.Player && m.Alive)
                        targets.Add(m);
                }

                for (int i = 0; i < targets.Count; ++i)
				{
                    Mobile m = (Mobile)targets[i];

                    DoHarmful(m);

                    AOS.Damage(m, this, Utility.RandomMinMax(35, 45), true, 0, 0, 100, 0, 0);

                    m.FixedParticles(0x36E4, 1, 10, 0x1F78, 0x47F, 0, (EffectLayer)255);
                    m.ApplyPoison(this, Poison.Lethal);
				}
			}
		}

		public PrimevalLich( Serial serial ) : base( serial )
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