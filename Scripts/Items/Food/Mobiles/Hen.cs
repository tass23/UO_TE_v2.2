using System;
using Server.Mobiles;
using Server.Items;
using Server.Misc;
using Server.Multis;
using Server.Network;
using Server.Targeting;
using System.Collections;

namespace Server.Mobiles
{
	[CorpseName( "a hen corpse" )]
	public class Hen : BaseCreature
	{
		private Timer m_Timer;

		[Constructable]
		public Hen() : base( AIType.AI_Animal, FightMode.Aggressor, 10, 1, 0.2, 0.4 )
		{
			Name = "a hen";
			Body = 0xD0;
			BaseSoundID = 0x6E;

			SetStr( 5 );
			SetDex( 15 );
			SetInt( 5 );

			SetHits( 3 );
			SetMana( 0 );

			SetDamage( 1 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 1, 5 );

			SetSkill( SkillName.MagicResist, 4.0 );
			SetSkill( SkillName.Tactics, 5.0 );
			SetSkill( SkillName.Wrestling, 5.0 );

			Fame = 150;
			Karma = 0;

			VirtualArmor = 2;

			Tamable = true;
			ControlSlots = 1;
			MinTameSkill = -0.9;

			m_Timer = new InternalTimer(this);
			m_Timer.Start();
		}

		public override int Meat{ get{ return 1; } }
		public override MeatType MeatType{ get{ return MeatType.Bird; } }
		public override FoodType FavoriteFood{ get{ return FoodType.FruitsAndVegies | FoodType.GrainsAndHay; } }

		public override int Feathers{ get{ return 25; } }

		public Hen(Serial serial) : base(serial){}

		public class InternalTimer: Timer
		{
			private Hen m_Owner;

			public InternalTimer(Mobile mob): base( TimeSpan.FromMinutes( 20.0 ), TimeSpan.FromMinutes( 30.0 ))
			{
				Priority = TimerPriority.OneMinute;
				m_Owner = ((Hen) mob);
			}

			protected override void OnTick()
			{
				if ( m_Owner.Deleted )
				{
					Stop();
					return;
				}
				Map map = m_Owner.Map;
				if ( map != null && map != Map.Internal && m_Owner.Controlled && ( ! m_Owner.IsDeadPet ) )
				{
					Eggs eggs = new Eggs();
					eggs.MoveToWorld( m_Owner.Location, map );
					Effects.PlaySound( m_Owner.Location, map, 1064);
				}
			}
		}

		public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int) 0); }

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();
			m_Timer = new InternalTimer( this );
			m_Timer.Start();
		}
	}
}