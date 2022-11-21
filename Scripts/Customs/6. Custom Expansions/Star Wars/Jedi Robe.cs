using System;
using Server;
using Server.Items;

namespace Server.Items
{
   	[FlipableAttribute( 0x2683, 0x2684 )]
   	public class JediRobe : BaseOuterTorso
   	{
		public override int Hue { get { return 1821; } }
        private Mobile m_Owner = null;

        [CommandProperty(AccessLevel.Counselor, AccessLevel.GameMaster)]
        public Mobile Owner
        {
            get { return m_Owner; }
            set { m_Owner = value; }
		}

		[Constructable]
		public JediRobe() : base( 0x2683 )
		{
			Weight = 5.0;
			Name = "Jedi Robe";
			Hue = 1821;
			Layer = Layer.OuterTorso;
			LootType = LootType.Blessed;
		}

		public override void OnDoubleClick( Mobile m )
		{
			if( Parent != m )
			{
				m.SendMessage( "You must be wearing the robe to use it!" );
			}
			else
			{
				if ( ItemID == 0x2683 || ItemID == 0x2684 )
				{
					m.SendMessage( "You lower the hood." );
					m.PlaySound( 0x57 );
					ItemID = 0x1F03;
					m.Title = null;
					m.RemoveItem(this);
					m.EquipItem(this);
				}
				else if ( ItemID == 0x1F03 || ItemID == 0x1F04 )
				{
					m.SendMessage( "You pull the hood over your head." );
					m.PlaySound( 0x57 );
					ItemID = 0x2683;
					m.Title = "the Jedi";
					m.RemoveItem(this);
					m.EquipItem(this);
				}
			}
		}

		public override bool OnEquip( Mobile m )
		{
			if ( ItemID == 0x2683 )
			{
				m.Title = "the Jedi";
				m.DisplayGuildTitle = false;
				m.Criminal = false;
			}

			if ( m.Karma >= 5000 )
			{
				if ( m != m_Owner )
				{
					if ( m_Owner == null )
					{
						if ( m.Karma >= 5000 )
						{
							m.SendMessage("You bind the robe to you...");
							m_Owner = m;
							this.Name = "The Jedi " + m_Owner.Name.ToString() + "'s Robe";
							m.FixedEffect( 0x3779, 10, 15 );
							m.PlaySound( 1623 );
							InvalidateProperties();
						}
						else
						{
							m.SendMessage( "Only a Jedi, can wear this robe." );
						}
					}
					else
					{
						m.SendMessage( "This is not your robe." );
					}
					return false;
				}
				return base.OnEquip( m );
			}
			else
			{
				m.SendMessage( "Only a Jedi, can wear this robe." );
				return false;
			}
			return base.OnEquip( m );
		}

		public override void OnRemoved( Object o )
		{
			if( o is Mobile )
			{
				((Mobile)o).Title = null;
			}

			if( o is Mobile && ((Mobile)o).Kills >= 5)
			{
				((Mobile)o).Criminal = true;
			}

			if( o is Mobile && ((Mobile)o).GuildTitle != null )
			{
				((Mobile)o).DisplayGuildTitle = true;
			}
			base.OnRemoved( o );
		}

		public JediRobe( Serial serial ) : base( serial )
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

			switch(version)
			{
				case 0:
				{
					break;
				}
			}
		}
	}
}