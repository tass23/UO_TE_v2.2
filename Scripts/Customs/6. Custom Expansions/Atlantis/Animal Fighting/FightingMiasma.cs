using System;
using Server;
using Server.Mobiles;
using Server.Items;
using System.Collections;
using System.Collections.Generic;

namespace Server.Mobiles
{
	[CorpseName( "a miasma corpse" )]
	public class FightingMiasma : BaseCreature
	{
		public override bool AlwaysMurderer{ get{ return true; } }
		private Mobile m_Owner;
		private MiasmaFighting m_Controller;
	
		[CommandProperty( AccessLevel.Owner )]
		public Mobile Owner{ get{ return m_Owner; } set { m_Owner = value; InvalidateProperties(); } }

		[CommandProperty( AccessLevel.Owner )]
		public MiasmaFighting Controller{ get{ return m_Controller; } set { m_Controller = value; InvalidateProperties(); } }

		[Constructable]
		public FightingMiasma( Mobile from, MiasmaFighting controller ) : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a fighting miasma";
			Body = 0x30;	
			Hue = 2987;		
			BaseSoundID = 0x18D;
			m_Owner = from;
			m_Controller = controller;
			SetStr( 255, 847 );
			SetDex( 145, 428 );
			SetInt( 26, 362 );
			SetHits( 490, 1871 );
			SetDamage( 20, 30 );

			SetDamageType( ResistanceType.Physical, 60 );
			SetDamageType( ResistanceType.Poison, 40 );
			SetResistance( ResistanceType.Physical, 50, 54 );
			SetResistance( ResistanceType.Fire, 70 );
			SetResistance( ResistanceType.Cold, 70 );
			SetResistance( ResistanceType.Poison, 70 );
			SetResistance( ResistanceType.Energy, 70 );
			SetSkill( SkillName.Wrestling, 64.9, 73.3 );
			SetSkill( SkillName.Tactics, 98.4, 110.6 );
			SetSkill( SkillName.MagicResist, 74.4, 77.7 );
			SetSkill( SkillName.Poisoning, 128.5, 143.6 );
			
			Fame = 0;
			Karma = 0;
			Tamable = false;
			ControlSlots = 10;
		}

		public override Poison HitPoison{ get{ return Poison.Deadly; } }
		
		public override void AddNameProperties(ObjectPropertyList list)
		{
			base.AddNameProperties(list);
			
			if(m_Owner != null && m_Owner is PlayerMobile)
				list.Add( String.Format( "Bet On By {0}", m_Owner.Name ) );
		}
		
		public override bool OnBeforeDeath()
		{
			if ( m_Controller != null)
			{
				m_Controller.DeadChickens++;
				
				if( m_Controller.DeadChickens == (m_Controller.MaxPlayers - 1) )
				{
					m_Owner.SendMessage("Your miasma has died.  You lose.");
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

		public FightingMiasma(Serial serial) : base(serial)
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