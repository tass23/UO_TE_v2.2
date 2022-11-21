using System;
using Server;
using Server.Mobiles;
using Server.Items;
using System.Collections;
using System.Collections.Generic;

namespace Server.Mobiles
{
	[CorpseName( "a pig corpse" )]
	public class FightingPig : BaseCreature
	{
		public override bool AlwaysMurderer{ get{ return true; } }
		private Mobile m_Owner;
		private PigFighting m_Controller;
	
		[CommandProperty( AccessLevel.Owner )]
		public Mobile Owner{ get{ return m_Owner; } set { m_Owner = value; InvalidateProperties(); } }

		[CommandProperty( AccessLevel.Owner )]
		public PigFighting Controller{ get{ return m_Controller; } set { m_Controller = value; InvalidateProperties(); } }

		[Constructable]
		public FightingPig( Mobile from, PigFighting controller ) : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a fighting pig";
			Body = 0xCB;
			Hue = 2973;
			BaseSoundID = 0xC4;
			m_Owner = from;
			m_Controller = controller;
			SetStr( 20 );
			SetDex( 20 );
			SetInt( 5 );
			SetHits( 12 );
			SetMana( 0 );
			SetDamage( 2, 4 );

			SetDamageType( ResistanceType.Physical, 100 );
			SetResistance( ResistanceType.Physical, 10, 15 );
			SetResistance( ResistanceType.Energy, 70 );
			SetResistance( ResistanceType.Fire, 70 );
			SetResistance( ResistanceType.Poison, 70 );
			SetResistance( ResistanceType.Cold, 70 );
			SetSkill( SkillName.MagicResist, 5.0 );
			SetSkill( SkillName.Tactics, 5.0 );
			SetSkill( SkillName.Wrestling, 5.0 );

			Fame = 0;
			Karma = 0;
			VirtualArmor = Utility.Random( 2, 10 );
			Tamable = false;
			ControlSlots = 10;
		}
		
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
					m_Owner.SendMessage("Your pig has died.  You lose.");
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

		public FightingPig(Serial serial) : base(serial)
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