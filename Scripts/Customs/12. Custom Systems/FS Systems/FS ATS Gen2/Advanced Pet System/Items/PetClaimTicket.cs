using System;
using Server;
using System.Collections;
using System.Collections.Generic;
using Server.ContextMenus;

namespace Server.Items
{
	public class PetClaimTicket : Item
	{
		private int m_AI;
		private Mobile m_Owner;
		private Mobile m_Pet;
		private int m_Str;
		private int m_Dex;
		private int m_Int;
		private int m_Hits;
		private int m_Stam;
		private int m_Mana;
		private int m_Phys;
		private int m_Fire;
		private int m_Cold;
		private int m_Nrgy;
		private int m_Pois;
		private int m_Dmin;
		private int m_Dmax;
		private int m_Mlev;
		private int m_Gen;
		private int m_Price;
		private DateTime m_Time;

		[CommandProperty( AccessLevel.Administrator )]
		public Mobile Owner
		{
			get{ return m_Owner; }
			set{ m_Owner = value; }
		}

		[CommandProperty( AccessLevel.Administrator )]
		public Mobile Pet
		{
			get{ return m_Pet; }
			set{ m_Pet = value; }
		}

		[CommandProperty( AccessLevel.Administrator )]
		public int Str
		{
			get{ return m_Str; }
			set{ m_Str = value; }
		}

		[CommandProperty( AccessLevel.Administrator )]
		public int Dex
		{
			get{ return m_Dex; }
			set{ m_Dex = value; }
		}

		[CommandProperty( AccessLevel.Administrator )]
		public int Int
		{
			get{ return m_Int; }
			set{ m_Int = value; }
		}

		[CommandProperty( AccessLevel.Administrator )]
		public int Hits
		{
			get{ return m_Hits; }
			set{ m_Hits = value; }
		}

		[CommandProperty( AccessLevel.Administrator )]
		public int Stam
		{
			get{ return m_Stam; }
			set{ m_Stam = value; }
		}

		[CommandProperty( AccessLevel.Administrator )]
		public int Mana
		{
			get{ return m_Mana; }
			set{ m_Mana = value; }
		}

		[CommandProperty( AccessLevel.Administrator )]
		public int Phys
		{
			get{ return m_Phys; }
			set{ m_Phys = value; }
		}

		[CommandProperty( AccessLevel.Administrator )]
		public int Fire
		{
			get{ return m_Fire; }
			set{ m_Fire = value; }
		}

		[CommandProperty( AccessLevel.Administrator )]
		public int Cold
		{
			get{ return m_Cold; }
			set{ m_Cold = value; }
		}

		[CommandProperty( AccessLevel.Administrator )]
		public int Nrgy
		{
			get{ return m_Nrgy; }
			set{ m_Nrgy = value; }
		}

		[CommandProperty( AccessLevel.Administrator )]
		public int Pois
		{
			get{ return m_Pois; }
			set{ m_Pois = value; }
		}

		[CommandProperty( AccessLevel.Administrator )]
		public int Dmin
		{
			get{ return m_Dmin; }
			set{ m_Dmin = value; }
		}

		[CommandProperty( AccessLevel.Administrator )]
		public int Dmax
		{
			get{ return m_Dmax; }
			set{ m_Dmax = value; }
		}

		[CommandProperty( AccessLevel.Administrator )]
		public int Mlev
		{
			get{ return m_Mlev; }
			set{ m_Mlev = value; }
		}

		[CommandProperty( AccessLevel.Administrator )]
		public int Gen
		{
			get{ return m_Gen; }
			set{ m_Gen = value; }
		}

		[CommandProperty( AccessLevel.Administrator )]
		public int Price
		{
			get{ return m_Price; }
			set{ m_Price = value; }
		}

		[CommandProperty( AccessLevel.Administrator )]
		public DateTime Time
		{
			get{ return m_Time; }
			set{ m_Time = value; }
		}

		public int AI
		{
			get{ return m_AI; }
			set{ m_AI = value; }
		}

		[Constructable]
		public PetClaimTicket() : base( 0x14F0 )
		{
			Name = "a claim ticket from an animal breeder";
			LootType = LootType.Blessed;

			this.Time = DateTime.Now + TimeSpan.FromHours( 48.0 );
		}

		public PetClaimTicket( Serial serial ) : base( serial )
		{
		}

		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );

			list.Add( 1060658, "Return Date\t{0}", m_Time.ToLongDateString() );
			list.Add( 1060659, "Return Time\t{0}", m_Time.ToLongTimeString() );

			if ( this.Pet != null )
				list.Add( 1060662, "Pet That Is Breeding\t{0}", m_Pet.Name );

			if ( this.Owner != null )
				list.Add( 1060663, "Owner\t{0}", m_Owner.Name );

			list.Add( 1060661, "Cost To Claim\t{0} gold coins", m_Price);
		}

		public override void GetContextMenuEntries( Mobile from, List<ContextMenuEntry> list )
		{
			base.GetContextMenuEntries( from, list );

			if ( from.Alive )
			{
				if ( m_Owner == from )
				{
					list.Add( new ContextMenus.BreedingCancelMenu( from, this ) );
				}
			}
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 1 );

			writer.Write( m_AI );
			writer.Write( m_Owner );
			writer.Write( m_Pet );
			writer.Write( m_Str );
			writer.Write( m_Dex );
			writer.Write( m_Int );
			writer.Write( m_Hits );
			writer.Write( m_Stam );
			writer.Write( m_Mana );
			writer.Write( m_Phys );
			writer.Write( m_Fire );
			writer.Write( m_Cold );
			writer.Write( m_Nrgy );
			writer.Write( m_Pois );
			writer.Write( m_Dmin );
			writer.Write( m_Dmax );
			writer.Write( m_Mlev );
			writer.Write( m_Gen );
			writer.Write( m_Price );
			writer.WriteDeltaTime( m_Time );
		}
		
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			switch ( version )
			{
				case 1:
				{
					m_AI = reader.ReadInt(); // AI Fix
					goto case 0;
				}
				case 0:
				{
					m_Owner = reader.ReadMobile();
					m_Pet = reader.ReadMobile();
					m_Str = reader.ReadInt();
					m_Dex = reader.ReadInt();
					m_Int = reader.ReadInt();
					m_Hits = reader.ReadInt();
					m_Stam = reader.ReadInt();
					m_Mana = reader.ReadInt();
					m_Phys = reader.ReadInt();
					m_Fire = reader.ReadInt();
					m_Cold = reader.ReadInt();
					m_Nrgy = reader.ReadInt();
					m_Pois = reader.ReadInt();
					m_Dmin = reader.ReadInt();
					m_Dmax = reader.ReadInt();
					m_Mlev = reader.ReadInt();
					m_Gen = reader.ReadInt();
					m_Price = reader.ReadInt();
					m_Time = reader.ReadDeltaTime();
					break;
				}
			}
		}
	}
}