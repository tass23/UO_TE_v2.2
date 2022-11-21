using System;
using Server;
using Server.Engines.Harvest;

namespace Server.Items
{
	public class ExilePick : BaseAxe, IUsesRemaining
	{
		private Mobile m_Owner = null;

        [CommandProperty(AccessLevel.Counselor, AccessLevel.GameMaster)]
        public Mobile Owner
        {
            get { return m_Owner; }
            set { m_Owner = value; }
		}

		public override int LabelNumber{ get{ return 1045126; } } // sturdy pickaxe
		public override HarvestSystem HarvestSystem{ get{ return Mining.System; } }
		public override WeaponAbility PrimaryAbility{ get{ return WeaponAbility.DoubleStrike; } }
		public override WeaponAbility SecondaryAbility{ get{ return WeaponAbility.Disarm; } }
		public override int AosStrengthReq{ get{ return 50; } }
		public override int AosMinDamage{ get{ return 13; } }
		public override int AosMaxDamage{ get{ return 15; } }
		public override int AosSpeed{ get{ return 35; } }
		public override float MlSpeed{ get{ return 3.00f; } }
		public override int OldStrengthReq{ get{ return 25; } }
		public override int OldMinDamage{ get{ return 1; } }
		public override int OldMaxDamage{ get{ return 15; } }
		public override int OldSpeed{ get{ return 35; } }
		public override WeaponAnimation DefAnimation{ get{ return WeaponAnimation.Slash1H; } }

		[Constructable]
		public ExilePick() : this( 180 )
		{
			Name = "a Jedi Exile Crystal Harvester";
		}

		[Constructable]
		public ExilePick( int uses ) : base( 0xE86 )
		{
			Weight = 11.0;
			Hue = 2407;
			UsesRemaining = uses;
			ShowUsesRemaining = false;
			Name = "a Jedi Exile Crystal Harvester";
		}

		public override bool OnEquip( Mobile from )
		{
			if ( from.Karma >= -5000 && from.Karma <= 5000 )
			{
				if ( from != m_Owner )
				{
					if ( m_Owner == null )
					{
						if ( from.Karma >= -5000 && from.Karma <= 5000 )
						{
							from.SendMessage( "The pickaxe binds to you..." );
							m_Owner = from;
							from.FixedEffect( 0x375A, 10, 15 );
							from.PlaySound( 0x1E7 );
							return base.OnEquip( from );
						}
						else
						{
							from.SendMessage( "Only a Jedi Exile, can wield this pickaxe." );
						}
					}
					else
					{
						from.SendMessage( "This is not your pickaxe." );
					}
					return false;
				}
				return base.OnEquip( from );
			}
			else
			{
				from.SendMessage( "Only a Jedi Exile, can wield this pickaxe." );
				return false;
			}
			return base.OnEquip( from );
        }

		public ExilePick( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
			writer.Write((Mobile)m_Owner);
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			m_Owner = reader.ReadMobile();
		}
	}
}