using System;
using Server.Misc;

namespace Server.Items
{
	[FlipableAttribute( 0x1515, 0x1530 )]
	public class ExileCloak : Cloak
	{
		public override int Hue { get { return 1627; } }
        private Mobile m_Owner = null;

        [CommandProperty(AccessLevel.Counselor, AccessLevel.GameMaster)]
        public Mobile Owner
        {
            get { return m_Owner; }
            set { m_Owner = value; }
		}

		[Constructable]
		public ExileCloak() : base( 0x309 )
		{
			Name = "a Jedi Exile Cloak";
			LootType = LootType.Blessed;
		}

		public override bool OnEquip( Mobile from )
		{
			if ( from.Karma <= 4999 & from.Karma >= -4999 )
			{
				if ( from != m_Owner )
				{
					if ( m_Owner == null )
					{
						if ( from.Karma <= 4999 & from.Karma >= -4999)
						{
							from.SendMessage( "The cloak binds to you..." );
							m_Owner = from;
							Name = "The Jedi Exile " + m_Owner.Name.ToString() + "'s Cloak";
							from.FixedEffect( 0x3779, 10, 15 );
							from.PlaySound( 1623 );
							return base.OnEquip( from );
						}
						else
						{
							from.SendMessage( "Only a Jedi Exile, can wear this cloak." );
						}
					}
					else
					{
						from.SendMessage( "This is not your cloak." );
					}
					return false;
				}
				return base.OnEquip( from );
			}
			else
			{
				from.SendMessage( "Only a Jedi Exile, can wear this cloak." );
				return false;
			}
			return base.OnEquip( from );			
		}

		public override bool Dye( Mobile from, DyeTub sender )
		{
			from.SendLocalizedMessage( 1042083 ); // You can not dye that.
			return false;
		}

		public override void OnAdded( object parent )
		{
			base.OnAdded( parent );

			if ( parent is Mobile )
				Misc.Titles.AwardKarma( (Mobile)parent, -100, true );
		}

		public override void OnRemoved( object parent )
		{
			if ( parent is Mobile )
			{
				if ( parent is Mobile )
					Misc.Titles.AwardKarma( (Mobile)parent, 100, true );
			}
		}

		public override void OnSingleClick( Mobile from )
		{
			this.LabelTo( from, Name );
		}

		public ExileCloak( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
			writer.Write((Mobile)m_Owner);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			m_Owner = reader.ReadMobile();
		}
	}
}