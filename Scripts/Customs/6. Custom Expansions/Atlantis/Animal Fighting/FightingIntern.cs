using System;
using Server;
using Server.Mobiles;
using Server.Items;
using System.Collections;
using System.Collections.Generic;

namespace Server.Mobiles
{
	[CorpseName( "an intern's corpse" )]
	public class FightingIntern : BaseCreature
	{
		public override bool AlwaysMurderer{ get{ return true; } }
		private Mobile m_Owner;
		private AtlantisFighting m_Controller;
	
		[CommandProperty( AccessLevel.Owner )]
		public Mobile Owner{ get{ return m_Owner; } set { m_Owner = value; InvalidateProperties(); } }

		[CommandProperty( AccessLevel.Owner )]
		public AtlantisFighting Controller{ get{ return m_Controller; } set { m_Controller = value; InvalidateProperties(); } }

		[Constructable]
		public FightingIntern( Mobile from, AtlantisFighting controller ) : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "Jonesy's Converted Intern";
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
			
			Fame = 0;
			Karma = 0;
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

			LeatherCap Helm = new LeatherCap();
			Helm.Resource = res;
			Helm.Movable = false;
			Helm.Hue = 2976;
			AddItem(Helm);
			AddItem(new Boots(0x455));
		}

		public override Poison HitPoison{ get{ return Poison.Lethal; } }
		
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
					m_Owner.SendMessage("Your gladiator has died.  You lose.");
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
				
		public FightingIntern(Serial serial) : base(serial)
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