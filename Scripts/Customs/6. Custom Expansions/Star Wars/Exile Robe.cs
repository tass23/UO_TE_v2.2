using System;
using Server;
using Server.Items;

namespace Server.Items
{
   	[FlipableAttribute( 0x2683, 0x2684 )]
   	public class ExileRobe : BaseOuterTorso
   	{
		public override int Hue { get { return 1627; } }
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
			set { m_Fame = value; InvalidateProperties(); } }

      		[Constructable]
      		public ExileRobe() : base( 0x2683 )
      		{
         		Weight = 5.0;
         		Name = "a Jedi Exile Robe";
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
						if (m.Fame == 0 && this.Fame > 0)
						{
							m.Fame = this.Fame;
							this.Fame = 0;
						}
						else
							this.Fame = 0;
					}
					else if ( ItemID == 0x1F03 || ItemID == 0x1F04 )
					{
						m.SendMessage( "You pull the hood over your head." );
						m.PlaySound( 0x57 );
						ItemID = 0x2683;
						if (this.Fame == 0 && m.Fame > 0)
						{
							this.Fame = m.Fame;
							m.Fame = 0;
						}
						else
							this.Fame = 0;

						m.Title = "the Jedi Exile";
						m.RemoveItem(this);
						m.EquipItem(this);

					}
         		}
      		}

       		public override bool OnEquip( Mobile m )
      		{
         		if ( ItemID == 0x2683 )
         		{
         			//m.NameMod = "A Jedi Master";
         			m.Title = "the Jedi Exile";
				if (this.Fame == 0 && m.Fame > 0)
				{
					this.Fame = m.Fame;
					m.Fame = 0;
				}
				else
					this.Fame = 0;

         			m.DisplayGuildTitle = false;
         			m.Criminal = false;
         		}
        		//return base.OnEquip(m);
				
				if ( m.Karma >= -4999 & m.Karma <= 4999 ) 
				{
					if ( m != m_Owner )
					{
						if ( m_Owner == null )
						{
							if (  m.Karma >= -4999 & m.Karma <= 4999 )
							{
								m.SendMessage("You bind the robe to you...");
								m_Owner = m;
								this.Name = "Jedi Exile " + m_Owner.Name.ToString() + "'s Robe";
								m.FixedEffect( 0x3779, 10, 15 );
								m.PlaySound( 1623 );
								InvalidateProperties();
							}
							else
							{
								m.SendMessage( "Only a Jedi Exile, can wear this robe." );
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
					m.SendMessage( "Only a Jedi Exile, can wear this robe." );
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

			if( o is Mobile && ((Mobile)o).Fame == 0 && this.Fame > 0 )
			{
				((Mobile)o).Fame = this.Fame;
				this.Fame = 0;
			}
			else
				this.Fame = 0;

      			base.OnRemoved( o );
      		}

      		public ExileRobe( Serial serial ) : base( serial )
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