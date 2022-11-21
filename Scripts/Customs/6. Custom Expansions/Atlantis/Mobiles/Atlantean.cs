using System;
using Server;
using Server.Accounting;
using Server.Misc;
using Server.Items;
using Server.Mobiles;

namespace Server.Mobiles
{
	[CorpseName("an Atlantean guard corpse")]
	public class AtlanteanGuard : BaseCreature
	{
		private Mobile m_Target;
		
		[Constructable]
		public AtlanteanGuard() : this(null)
		{
		}
		
		[Constructable]
		public AtlanteanGuard(Mobile target) : base(AIType.AI_Melee, FightMode.Closest, 10, 1, 0.15, 0.4)
		{
			m_Target = target;
			Name = "an Atlantean guard";
			Body = 0x190;
			Hue = 400;
			AddItem(new LightSource());
			SetStr(276, 350);
			SetDex(66, 90);
			SetInt(126, 150);
			SetHits(276, 350);
			SetDamage(29, 34);

			SetDamageType(ResistanceType.Physical, 50);
			SetDamageType(ResistanceType.Cold, 50);
			SetResistance(ResistanceType.Physical, 35, 45);
			SetResistance(ResistanceType.Fire, 60, 70);
			SetResistance(ResistanceType.Cold, 70, 80);
			SetResistance(ResistanceType.Poison, 25, 35);
			SetResistance(ResistanceType.Energy, 5, 15);
			SetSkill(SkillName.Tactics, 90.1, 100.0);
			SetSkill(SkillName.MagicResist, 100.1, 110.0);
			SetSkill(SkillName.Anatomy, 70.1, 100.0);
			SetSkill(SkillName.Magery, 95.1, 100.0);
			SetSkill(SkillName.EvalInt, 95.1, 100.0);
			SetSkill(SkillName.Swords, 75.5, 95.0);
			SetSkill(SkillName.Fencing, 80.1, 100);
			SetSkill(SkillName.Macing, 80.1, 100);

			Fame = 300;
			Karma = -300;

			CraftResource res = CraftResource.None;;
			switch (Utility.Random(3))
			{
				case 0: res = CraftResource.HornedLeather; break;
				case 1: res = CraftResource.SpinedLeather; break;
				case 2: res = CraftResource.BarbedLeather; break;
			}

			BaseWeapon melee = null;
			switch (Utility.Random(3))
			{
				case 0: melee = new Kryss(); break;
				case 1: melee = new Broadsword(); break;
				case 2: melee = new Katana(); break;
			}

			melee.Movable = false;
			AddItem(melee);

			LeatherChest Tunic = new LeatherChest();
			Tunic.Resource = res;
			Tunic.Movable = false;
			Tunic.Hue = 2976;
			AddItem(Tunic);

			LeatherLegs Legs = new LeatherLegs();
			Legs.Resource = res;
			Legs.Movable = false;
			Legs.Hue = 2976;
			AddItem(Legs);

			LeatherArms Arms = new LeatherArms();
			Arms.Resource = res;
			Arms.Movable = false;
			Arms.Hue = 2976;
			AddItem(Arms);

			LeatherGloves Gloves = new LeatherGloves();
			Gloves.Resource = res;
			Gloves.Movable = false;
			Gloves.Hue = 2976;
			AddItem(Gloves);

			LeatherCap Helm = new LeatherCap();
			Helm.Resource = res;
			Helm.Movable = false;
			Helm.Hue = 2976;
			AddItem(Helm);

			AddItem(new Boots(0x455));
			AddItem(new Shirt(Utility.RandomMetalHue()));

			RidableLargeBeetle mt = new RidableLargeBeetle();
			mt.Controlled = true;
			mt.ControlMaster = this;
			mt.ControlTarget = this;
			mt.Combatant = m_Target;
			mt.Rider = this;
		}

		public override void GenerateLoot()
		{
			AddLoot(LootPack.Rich);
			AddLoot(LootPack.Gems);
		}

		public override bool AutoDispel { get { return true; } }
		public override bool BardImmune { get { return !Core.AOS; } }
		public override bool CanRummageCorpses { get { return true; } }
		public override bool AlwaysMurderer { get { return true; } }
		public override bool ShowFameTitle { get { return false; } }

		public override bool OnBeforeDeath()
		{
			IMount mount = this.Mount;

			if ( mount != null )
			{
				if ( mount is RidableLargeBeetle )
				{
					((RidableLargeBeetle)mount).Controlled = false;
					((RidableLargeBeetle)mount).ControlMaster = null;
					((RidableLargeBeetle)mount).ControlTarget = null;
					((RidableLargeBeetle)mount).Combatant = m_Target;
				}
				mount.Rider = null;
			}
			return base.OnBeforeDeath();
		}

		public override void AlterMeleeDamageTo(Mobile to, ref int damage)
		{
			if ( to is CuSidhe || to is HellCat || to is Hiryu || to is LesserHiryu || to is Daemon )
				damage *= 3;
		}

		public override void OnThink()
		{
			if ( m_Target != null && m_Target.Alive && !Paralyzed && CanSee( m_Target) && ( (m_Target is PlayerMobile) && (m_Target.AccessLevel == AccessLevel.Player) ))
			{
				if (!InRange( m_Target, 15 ) )
				{
					Map fromMap = Map;
					Point3D from = Location;
				}

				Combatant = m_Target;
				FocusMob = m_Target;

				if ( AIObject != null )
					AIObject.Action = ActionType.Combat;

				base.OnThink();
			}

			if (Combatant == null || !CanSee( Combatant ) )
			{	
		
				IPooledEnumerable eable = GetMobilesInRange( 12 );
				foreach ( Mobile m in eable)
				{
					if(m != null && m is PlayerMobile && CanSee( m )) 
					m_Target = m;
				}
				eable.Free();
				base.OnThink();
			}
		}

		public AtlanteanGuard(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();
		}
	}
	
	[CorpseName("an Atlantean citizen's corpse")]
	public class AtlanteanCit : BaseCreature
	{
		private Mobile m_GuardHelper;
		private bool m_SpawnedGuardHelper;
		
		[Constructable]
		public AtlanteanCit() : base(AIType.AI_Melee, FightMode.Closest, 10, 1, 0.15, 0.4)
		{
			Name = "an Atlantean citizen";
			Body = 0x190;
			Hue = 400;
			AddItem(new LightSource());
			SetStr(276, 350);
			SetDex(66, 90);
			SetInt(126, 150);
			SetHits(800, 1050);
			SetDamage(29, 34);

			SetDamageType(ResistanceType.Physical, 50);
			SetDamageType(ResistanceType.Cold, 50);
			SetResistance(ResistanceType.Physical, 35, 45);
			SetResistance(ResistanceType.Fire, 60, 70);
			SetResistance(ResistanceType.Cold, 70, 80);
			SetResistance(ResistanceType.Poison, 25, 35);
			SetResistance(ResistanceType.Energy, 5, 15);
			SetSkill(SkillName.Tactics, 90.1, 100.0);
			SetSkill(SkillName.MagicResist, 100.1, 110.0);
			SetSkill(SkillName.Anatomy, 70.1, 100.0);
			SetSkill(SkillName.Magery, 95.1, 100.0);
			SetSkill(SkillName.EvalInt, 95.1, 100.0);
			SetSkill(SkillName.Swords, 75.5, 95.0);
			SetSkill(SkillName.Fencing, 80.1, 100);
			SetSkill(SkillName.Macing, 80.1, 100);

			Fame = 300;
			Karma = -300;

			CraftResource res = CraftResource.None;;
			switch (Utility.Random(3))
			{
				case 0: res = CraftResource.HornedLeather; break;
				case 1: res = CraftResource.SpinedLeather; break;
				case 2: res = CraftResource.BarbedLeather; break;
			}

			BaseWeapon melee = null;
			switch (Utility.Random(3))
			{
				case 0: melee = new Kryss(); break;
				case 1: melee = new Broadsword(); break;
				case 2: melee = new Katana(); break;
			}

			melee.Movable = false;
			AddItem(melee);

			LeatherChest Tunic = new LeatherChest();
			Tunic.Resource = res;
			Tunic.Movable = false;
			Tunic.Hue = 2996;
			AddItem(Tunic);

			LeatherLegs Legs = new LeatherLegs();
			Legs.Resource = res;
			Legs.Movable = false;
			Legs.Hue = 2996;
			AddItem(Legs);

			LeatherArms Arms = new LeatherArms();
			Arms.Resource = res;
			Arms.Movable = false;
			Arms.Hue = 2996;
			AddItem(Arms);

			LeatherGloves Gloves = new LeatherGloves();
			Gloves.Resource = res;
			Gloves.Movable = false;
			Gloves.Hue = 2996;
			AddItem(Gloves);

			LeatherCap Helm = new LeatherCap();
			Helm.Resource = res;
			Helm.Movable = false;
			Helm.Hue = 2996;
			AddItem(Helm);

			AddItem(new Boots(0x455));
			AddItem(new Shirt(Utility.RandomMetalHue()));
		}

		public override void GenerateLoot()
		{
			AddLoot(LootPack.Rich);
			AddLoot(LootPack.Gems);
		}

		public override bool AutoDispel { get { return true; } }
		public override bool BardImmune { get { return !Core.AOS; } }
		public override bool CanRummageCorpses { get { return true; } }
		public override bool AlwaysMurderer { get { return false; } }
		public override bool ShowFameTitle { get { return false; } }

		public void SpawnWaterVortex( Mobile target )
		{
			Map map = this.Map;

			if ( map == null )
				return;

			this.Say( "Amathaunta, aid my defense!" ); // You shall never defeat me as long as I have my queen!
			int newWaterVortexes = Utility.RandomMinMax( 1, 3 );

			for ( int i = 0; i < newWaterVortexes; ++i )
			{
				WaterVortex WaterVortex = new WaterVortex();
				WaterVortex.Team = this.Team;
				WaterVortex.FightMode = FightMode.Closest;
				bool validLocation = false;
				Point3D loc = this.Location;

				for ( int j = 0; !validLocation && j < 10; ++j )
				{
					int x = X + Utility.Random( 3 ) - 1;
					int y = Y + Utility.Random( 3 ) - 1;
					int z = map.GetAverageZ( x, y );

					if ( validLocation = map.CanFit( x, y, this.Z, 16, false, false ) )
						loc = new Point3D( x, y, Z );
					else if ( validLocation = map.CanFit( x, y, z, 16, false, false ) )
						loc = new Point3D( x, y, z );
				}
				WaterVortex.MoveToWorld( loc, map );
				WaterVortex.Combatant = target;
			}
		}
		
		public override bool OnBeforeDeath()
		{
			CheckGuardHelper();
			return base.OnBeforeDeath();
		}
		
		public override void OnDeath( Container c )
		{
			base.OnDeath( c );		
			if ( Utility.RandomDouble() < 0.75 )
			c.DropItem( new AtlanteanCoin() );
			if ( Utility.RandomDouble() < 0.50 )
			c.DropItem( new AtlanteanCoin() );
			if ( Utility.RandomDouble() < 0.25 )
			c.DropItem( new AtlanteanCoin() );
			if ( Utility.RandomDouble() < 0.15 )
			c.DropItem( new AtlanteanCoin() );
			if ( Utility.RandomDouble() < 0.10 )
			c.DropItem( new AtlanteanCoin() );
			if ( Utility.RandomDouble() < 0.05 )
			c.DropItem( new AtlanteanCoin() );
		}
		
		public void CheckGuardHelper()
		{
			if( this.Map == null )
				return;

			if ( !m_SpawnedGuardHelper )
			{
				this.Say( "Guards! Guards! Help me!" ); // Come forth my queen!
				m_GuardHelper = new AtlanteanGuard();
				((BaseCreature)m_GuardHelper).Team = this.Team;
				m_GuardHelper.MoveToWorld( this.Location, this.Map );
				m_SpawnedGuardHelper = true;
			}
			else if ( m_GuardHelper != null && m_GuardHelper.Deleted )
			{
				m_GuardHelper = null;
			}
		}
		
		public override void OnGotMeleeAttack( Mobile attacker )
		{
			base.OnGotMeleeAttack( attacker );
			CheckGuardHelper();

			if ( m_GuardHelper != null && 0.1 >= Utility.RandomDouble() )
				SpawnWaterVortex( attacker );

			attacker.Damage( Utility.Random( 20, 10 ), this );
			attacker.Stam -= Utility.Random( 20, 10 );
			attacker.Mana -= Utility.Random( 20, 10 );
		}

		public override void AlterMeleeDamageTo(Mobile to, ref int damage)
		{
			if ( to is CuSidhe || to is HellCat || to is Hiryu || to is LesserHiryu || to is Daemon )
				damage *= 3;
		}

		public AtlanteanCit(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0);

			writer.Write( m_GuardHelper );
			writer.Write( m_SpawnedGuardHelper );
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();
			
			switch ( version )
			{
				case 0:
				{
					m_GuardHelper = reader.ReadMobile();
					m_SpawnedGuardHelper = reader.ReadBool();

					break;
				}
			}
		}
	}
	
	[CorpseName("a puddle of water")]
	public class WaterVortex : BaseCreature
    {
        private Timer m_Timer;

        [Constructable]
        public WaterVortex() : base(AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "an infused water vortex";
            Body = 164;
			Hue = 1594;
            m_Timer = new InternalTimer(this);
            m_Timer.Start();
            AddItem(new LightSource());
            SetStr(125, 200);
            SetDex(200, 225);
            SetInt(100, 115);
            SetHits(300, 500);
            SetStam(250, 275);
            SetMana(0);
            SetDamage(16, 24);

            SetDamageType(ResistanceType.Physical, 0);
            SetDamageType(ResistanceType.Cold, 50);
			SetDamageType(ResistanceType.Energy, 50);
            SetResistance(ResistanceType.Physical, 60, 70);
            SetResistance(ResistanceType.Fire, 40, 50);
            SetResistance(ResistanceType.Cold, 40, 100);
            SetResistance(ResistanceType.Poison, 40, 50);
            SetResistance(ResistanceType.Energy, 90, 100);
            SetSkill(SkillName.MagicResist, 99.9);
            SetSkill(SkillName.Tactics, 90.0, 99.2);
            SetSkill(SkillName.Wrestling, 100.0, 115.1);

            Fame = 200;
            Karma = 200;

            VirtualArmor = 40;
            ControlSlots = 1;
			Tamable = false;
        }

        public override Poison PoisonImmune { get { return Poison.Lethal; } }

        public override int GetAngerSound()
        {
            return 0x15;
        }

        public override int GetAttackSound()
        {
            return 0x28;
        }

        public override void OnGotMeleeAttack(Mobile attacker)
        {
            base.OnGotMeleeAttack(attacker);
            attacker.BoltEffect(0);
            AOS.Damage(this, attacker, 20, 0, 0, 0, 0, 100);
        }

        public override void OnGaveMeleeAttack(Mobile attacker)
        {
            base.OnGaveMeleeAttack(attacker);
            attacker.BoltEffect(0);
            AOS.Damage(this, attacker, 20, 0, 0, 0, 0, 100);
        }

        public override void AlterDamageScalarFrom(Mobile caster, ref double scalar)
        {
            base.AlterDamageScalarFrom(caster, ref scalar);
            caster.BoltEffect(0);
            AOS.Damage(this, caster, 20, 0, 0, 0, 0, 100);

        }

        public WaterVortex(Serial serial) : base(serial)
        {
            m_Timer = new InternalTimer(this);
            m_Timer.Start();
        }

        public override void OnDelete()
        {
            m_Timer.Stop();

            base.OnDelete();
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }

        private class InternalTimer : Timer
        {
            private WaterVortex m_Owner;
            private int m_Count = 0;

            public InternalTimer(WaterVortex owner) : base(TimeSpan.FromSeconds(0.1), TimeSpan.FromSeconds(0.1))
            {
                m_Owner = owner;
            }

            protected override void OnTick()
            {
                if ((m_Count++ & 0x3) == 0)
                {
                    m_Owner.Direction = (Direction)(Utility.Random(8) | 0x80);
                }

                m_Owner.Move(m_Owner.Direction);
            }
        }
    }
}