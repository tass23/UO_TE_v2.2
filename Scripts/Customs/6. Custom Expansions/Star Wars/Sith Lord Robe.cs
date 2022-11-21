using System;
using Server;
using Server.Items;

namespace Server.Items
{
   	[FlipableAttribute( 0x2683, 0x2684 )]
   	public class SithLordRobe : BaseOuterTorso
   	{
		public override int Hue { get { return 2020; } }
        private Mobile m_Owner = null;

        [CommandProperty(AccessLevel.Counselor, AccessLevel.GameMaster)]
        public Mobile Owner
        {
            get { return m_Owner; }
            set { m_Owner = value; }
		}
		
		private int m_Fame;

		[CommandProperty( AccessLevel.GameMaster )]
		public int Fame
		{ 
			get 
			{ 
				if (m_Fame < 0)
					m_Fame = 0;
				return m_Fame; 
			} 
			set { m_Fame = value; InvalidateProperties(); }
		}

		[Constructable]
		public SithLordRobe() : base( 0x2683 )
		{
			Weight = 5.0;
			Name = "Sith Lord Robe";
			Hue = 2020;
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
					/*if (m.Fame == 0 && this.Fame > 0)
					{
						m.Fame = this.Fame;
						this.Fame = 0;
					}
					else
						this.Fame = 0;*/

						/*if( m.Kills >= 5)
						{
							m.Criminal = true;
						}
						if( m.GuildTitle != null)
						{
							m.DisplayGuildTitle = true;
						}*/
				}
				else if ( ItemID == 0x1F03 || ItemID == 0x1F04 )
				{
					m.SendMessage( "You pull the hood over your head." );
					m.PlaySound( 0x57 );
					ItemID = 0x2683;
					/*if (this.Fame == 0 && m.Fame > 0)
					{
						this.Fame = m.Fame;
						m.Fame = 0;
					}
					else
					{
						this.Fame = 0;*/
						//m.NameMod = "A Sith Lord";
						//m.ShowFameTitle = false;
						//m.DisplayGuildTitle = false;
						//m.Criminal = false;
						m.Title = "the Sith Lord";
						m.RemoveItem(this);
						m.EquipItem(this);
				}
			}
		}

		public override bool OnEquip( Mobile from )
		{
			if ( ItemID == 0x2683 )
			{
				//from.NameMod = "A Sith Lord";
				from.Title = "the Sith Lord";
				/*if (this.Fame == 0 && from.Fame > 0)
				{
					this.Fame = from.Fame;
					from.Fame = 0;
				}
				else
					this.Fame = 0;*/

				from.DisplayGuildTitle = false;
				from.Criminal = false;
			}
			
			if ( from.Karma <= -5000 )
			{
				if ( from != m_Owner )
				{
					if ( m_Owner == null )
					{
						if ( from.Karma <= -5000 )
						{
							from.SendMessage("You bind the robe to you...");
							m_Owner = from;
							this.Name = "The Sith Lord " + m_Owner.Name.ToString() + "'s Robe";
							from.FixedEffect( 0x42CF, 10, 15 );
							from.PlaySound( 1623 );
							InvalidateProperties();
						}
						else
						{
							from.SendMessage( "Only a Sith, can wear this robe." );
						}
					}
					else
					{
						from.SendMessage( "This is not your robe." );
					}
					return false;
				}
				return base.OnEquip( from );
			}
			else
			{
				from.SendMessage( "Only a Sith, can wear this robe." );
				return false;
			}
			return base.OnEquip( from );	
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

			/*if( o is Mobile && ((Mobile)o).Fame == 0 && this.Fame > 0 )
			{
				((Mobile)o).Fame = this.Fame;
				this.Fame = 0;
			}
			else
				this.Fame = 0;*/

			base.OnRemoved( o );
		}

		public SithLordRobe( Serial serial ) : base( serial )
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
				case 1:
				{
					m_Fame = reader.ReadInt();
					goto case 0;
				}
				case 0:
				{
					break;
				}
			}
      	}
	}
}