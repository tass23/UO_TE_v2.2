using System;
using System.Collections;
using System.Collections.Generic;
using Server.Items;
using Server.Mobiles;
using Server.Targeting;

namespace Server.Mobiles
{
	[CorpseName( "a wolf corpse" )]
	public class LeatherWolf : BaseCreature
	{
		public override WeaponAbility GetWeaponAbility()
		{
			return WeaponAbility.BleedAttack;
		}

		[Constructable]
		public LeatherWolf() : base( AIType.AI_Animal, FightMode.Aggressor, 10, 1, 0.2, 0.4 )
		{
			Body = 739;
			Name = "a leather wolf";

			SetStr( 104, 104 );
			SetDex( 111, 111 );
			SetInt( 22, 22 );

			SetHits( 221, 221 );
			SetStam( 111, 111);
			SetMana( 22, 22);

			SetDamage( 9, 20 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 0, 40 );
			SetResistance( ResistanceType.Fire, 0, 19 );
			SetResistance( ResistanceType.Cold, 0, 25 );
			SetResistance( ResistanceType.Poison, 0, 16 );
			SetResistance( ResistanceType.Energy, 0, 11 );

			SetSkill( SkillName.Anatomy, 0.0, 0.0 );
			SetSkill( SkillName.MagicResist, 65.2, 70.1 );
			SetSkill( SkillName.Tactics, 55.2, 71.5 );
			SetSkill( SkillName.Wrestling, 60.7, 70.9 );

			ControlSlots = 1;
		}

		public override int GetIdleSound()
		{
			return 1545;
		} 
		public override int GetAngerSound()
		{
			return 1542;
		} 
		public override int GetHurtSound()
		{
			return 1544;
		} 
		public override int GetDeathSound()
		{
			return 1543;
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Meager, 2 );
		}

		private DateTime m_SummonDelay;
		private List<Mobile> m_Summons = new List<Mobile>();

		public override void OnDelete()
		{
			TryToRemovePets();
			base.OnDelete();
		}

		public void TryToRemovePets()
		{
			if ( m_Summons.Count > 0 )
			{
				for ( int i = m_Summons.Count; i > -1; i-- )
					m_Summons[i].Delete();
			}
		}

		public override void OnThink()
		{
			if ( DateTime.Now > m_SummonDelay )
			{
				TryToRemovePets();
				m_SummonDelay = DateTime.Now + TimeSpan.FromSeconds( 10 );
			}

			base.OnThink();
		}

		public override void OnActionCombat()
		{
			if ( DateTime.Now > m_SummonDelay )
			{
				if ( SummonMaster == null && ControlMaster == null )
				{
					PlaySound( 0x0E6 );
					PublicOverheadMessage( Server.Network.MessageType.Regular, 0x3B2, 1113132 );

					Point3D p = Location;

					for ( int i = 0; i < 4; i++ )
					{
						Server.Spells.SpellHelper.FindValidSpawnLocation( Map, ref p, true );

						LeatherWolf lw = new LeatherWolf();
						lw.SetControlMaster( this );
						lw.MoveToWorld( p, Map );
						m_Summons.Add( lw );
					}
				}

				m_SummonDelay = DateTime.Now + TimeSpan.FromMinutes( 5 );
			}

			base.OnActionCombat();
		}

		public override int Meat{ get{ return 1; } }
		public override PackInstinct PackInstinct{ get { return PackInstinct.Canine; } }
		public override int Hides{ get{ return 7; } }
		public override FoodType FavoriteFood{ get{ return FoodType.Meat; } }

		public override bool IsScaredOfScaryThings{ get{ return false;} }
		public override bool IsScaryToPets{ get{ return true;} }

		public override bool IsBondable{ get{ return false;} }
		public override bool DeleteOnRelease{ get{ return true;} }

		public LeatherWolf( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
			writer.Write( m_Summons, true );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
			m_Summons = reader.ReadStrongMobileList();

			m_SummonDelay = DateTime.Now;
		}
	}
}