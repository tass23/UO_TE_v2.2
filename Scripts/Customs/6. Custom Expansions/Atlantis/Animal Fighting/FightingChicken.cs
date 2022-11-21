using System;
using Server;
using Server.Mobiles;
using Server.Items;
using System.Collections;
using System.Collections.Generic;

namespace Server.Mobiles
{
	[CorpseName( "a chicken corpse" )]
	public class FightingChicken : BaseCreature
	{
		public override bool AlwaysMurderer{ get{ return true; } }
		private Mobile m_Owner;
		private CockFighting m_Controller;
	
		[CommandProperty( AccessLevel.Owner )]
		public Mobile Owner{ get{ return m_Owner; } set { m_Owner = value; InvalidateProperties(); } }

		[CommandProperty( AccessLevel.Owner )]
		public CockFighting Controller{ get{ return m_Controller; } set { m_Controller = value; InvalidateProperties(); } }

		[Constructable]
		public FightingChicken( Mobile from, CockFighting controller ) : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a fighting chicken";
			Body = 0xD0;
			Hue = 2767;
			BaseSoundID = 0x6E;
			m_Owner = from;
			m_Controller = controller;
			SetStr( 20, 40);
			SetDex( 30, 45 );
			SetInt(10, 25 );
			SetHits( 40 );
			SetMana( 0 );
			SetDamage( 4, 7 );

			SetDamageType( ResistanceType.Physical, 100 );
			SetResistance( ResistanceType.Physical, 1, 3 );
			SetResistance( ResistanceType.Energy, 70 );
			SetResistance( ResistanceType.Fire, 70 );
			SetResistance( ResistanceType.Poison, 70 );
			SetResistance( ResistanceType.Cold, 70 );
			SetSkill( SkillName.MagicResist, 100.0 );
			SetSkill( SkillName.Tactics, 5.0, 10.0 );
			SetSkill( SkillName.Wrestling, 5.0, 10.0 );

			Fame = 0;
			Karma = 0;
			VirtualArmor = Utility.Random( 2, 10 );
			Tamable = false;
			ControlSlots = 10;
		}

		public override int Meat{ get{ return 0; } }
		public override MeatType MeatType{ get{ return MeatType.Bird; } }
		public override FoodType FavoriteFood{ get{ return FoodType.GrainsAndHay; } }
		public override int Feathers{ get{ return 0; } }
		
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
					m_Owner.SendMessage("Your chicken has died.  You lose.");
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

		public FightingChicken(Serial serial) : base(serial)
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