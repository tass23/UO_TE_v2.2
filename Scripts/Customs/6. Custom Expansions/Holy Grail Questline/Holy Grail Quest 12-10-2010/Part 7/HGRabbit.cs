using System;
using System.Collections;
using Server;
using Server.Items;
using Server.Targeting;
using Server.Engines.Quests;

namespace Server.Mobiles
{
	public class HGRabbit : BasePeerless
	{
		public override bool InitialInnocent{ get{ return true; } }

		[Constructable]
		public HGRabbit() : base( AIType.AI_Melee, FightMode.Aggressor, 10, 1, 0.2, 0.4 )
		{
			Body = 0xCD;
			Name = "a Rabbit";
			Hue = 1151;

			SetStr( 500, 1000 );
			SetDex( 500, 1000 );
			SetInt( 500, 1000 );

			SetHits( 2 );
			SetDamage( 500, 600 );
			SetDamageType( ResistanceType.Physical, 100 );
			SetResistance( ResistanceType.Physical, 10, 15 );
			SetResistance( ResistanceType.Fire, 1, 3 );
			SetResistance( ResistanceType.Poison, 10, 15 );
			SetResistance( ResistanceType.Energy, 10, 15 );
			SetSkill( SkillName.Tactics, 80.0, 100.0 );
			SetSkill( SkillName.Wrestling, 80.0, 100.0 );

			PackGold( 50, 150 );
		}

		public override Poison PoisonImmune{ get{ return Poison.Lethal; } }
		public override Poison HitPoison{ get{ return Poison.Lethal; } }
		public override bool CanAreaPoison{ get{ return true; } }
		public override Poison HitAreaPoison{ get{ return Poison.Lethal; } }

		public override void OnDamage( int amount, Mobile from, bool willKill )
		{
			base.OnDamage( amount, from, willKill );

			// eats pet or summons
			if ( from is BaseCreature )
			{
				BaseCreature creature = (BaseCreature) from;

				if ( creature.Controlled || creature.Summoned )
				{
					Heal( creature.Hits );
					creature.Kill();
					Effects.PlaySound( Location, Map, 0x574 );
				}
			}
			// teleports player near
			if ( from is PlayerMobile && !InRange( from.Location, 1 ) )
			{
				Combatant = from;
				from.MoveToWorld( GetSpawnPosition( 1 ), Map );	
				from.FixedParticles( 0x376A, 9, 32, 0x13AF, EffectLayer.Waist );
				from.PlaySound( 0x1FE );
			}
		}

		public HGRabbit( Serial serial ) : base( serial )
		{
		}

		public static bool IsMurderer( Mobile from )
		{
			if ( from != null && from is PlayerMobile )
			{
				BaseQuest quest = QuestHelper.GetQuest( (PlayerMobile) from, typeof( TimTheEnchanterQuest ) );

				if ( quest != null )
					return !quest.Completed;
			}
			return false;
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