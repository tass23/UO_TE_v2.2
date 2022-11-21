using System;
using Server;
using Server.Mobiles;
using Server.Items;
using System.Collections;
using System.Collections.Generic;

namespace Server.Mobiles
{
	[CorpseName( "a dragon corpse" )]
	public class FightingAW : BaseCreature
	{
		public override bool AlwaysMurderer{ get{ return true; } }
		private Mobile m_Owner;
		private AWFighting m_Controller;

		[CommandProperty( AccessLevel.Owner )]
		public Mobile Owner{ get{ return m_Owner; } set { m_Owner = value; InvalidateProperties(); } }

		[CommandProperty( AccessLevel.Owner )]
		public AWFighting Controller{ get{ return m_Controller; } set { m_Controller = value; InvalidateProperties(); } }
	
		[Constructable]
		public FightingAW( Mobile from, AWFighting controller ) : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a fighting ancient wyrm";
			Body = 46;
			BaseSoundID = 362;
			Hue = 2977;
			m_Owner = from;
			m_Controller = controller;
			SetStr( 1096, 1185 );
			SetDex( 86, 175 );
			SetInt( 686, 775 );
			SetHits( 658, 711 );
			SetDamage( 29, 35 );

			SetDamageType( ResistanceType.Physical, 75 );
			SetDamageType( ResistanceType.Fire, 25 );
			SetResistance( ResistanceType.Physical, 65, 75 );
			SetResistance( ResistanceType.Fire, 80, 90 );
			SetResistance( ResistanceType.Cold, 70, 80 );
			SetResistance( ResistanceType.Poison, 60, 70 );
			SetResistance( ResistanceType.Energy, 60, 70 );
			SetSkill( SkillName.EvalInt, 80.1, 100.0 );
			SetSkill( SkillName.Magery, 80.1, 100.0 );
			SetSkill( SkillName.Meditation, 52.5, 75.0 );
			SetSkill( SkillName.MagicResist, 100.5, 150.0 );
			SetSkill( SkillName.Tactics, 97.6, 100.0 );
			SetSkill( SkillName.Wrestling, 97.6, 100.0 );

			Fame = 0;
			Karma = 0;
			VirtualArmor = Utility.Random( 70, 80 );
			Tamable = false;
			ControlSlots = 10;
		}
		
		public override int GetIdleSound()
		{
			return 0x2D3;
		}

		public override int GetHurtSound()
		{
			return 0x2D1;
		}
		
		public override void AddNameProperties(ObjectPropertyList list)
		{
			base.AddNameProperties(list);
			
			if(m_Owner != null && m_Owner is PlayerMobile)
				list.Add( String.Format( "Bet On By {0}", m_Owner.Name ) );
		}
		
		public override bool HasBreath{ get{ return true; } } // fire breath enabled
		public override bool AutoDispel{ get{ return true; } }
		public override Poison HitPoison{ get{ return Utility.RandomBool() ? Poison.Lesser : Poison.Regular; } }
		
		public override bool OnBeforeDeath()
		{
			if ( m_Controller != null)
			{
				m_Controller.DeadChickens++;
				
				if( m_Controller.DeadChickens == (m_Controller.MaxPlayers - 1) )
				{
					m_Owner.SendMessage("Your Ancient Wyrm has died.  You lose.");
					Item a = Owner.Backpack.FindItemByType( typeof( RAFTRS ) );
					RAFTRS raftrs = a as RAFTRS;
					Item b = Owner.Backpack.FindItemByType( typeof( GoldVoucher ) );
					GoldVoucher gvoucher = b as GoldVoucher;

					if(raftrs != null && raftrs.CurrentBet > 0 && Owner.Backpack.FindItemByType( typeof( GoldVoucher )) != null )
					{
						raftrs.CurrentBet = 0;
						raftrs.WinningAmount = 0;
						Owner.Backpack.ConsumeTotal( typeof( GoldVoucher ), 1);
						m_Controller.EndGame(m_Controller);
					}
				}
				return base.OnBeforeDeath();
			}
			else
				this.Delete();
				return false;
		}
				
		public FightingAW(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int) 0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();
		}
	}
}