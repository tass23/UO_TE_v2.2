using System;
using System.Collections.Generic;
using Server.Network;
using Server.Items;
using Server.Multis;
using Server.ContextMenus;

namespace Server.Items
{
	public class YardIronGate : IronGate
	{
		public Mobile m_Placer;
		public int m_Value = 0;

		[CommandProperty( AccessLevel.GameMaster )]
		public Mobile Placer
		{
			get{ return m_Placer; }
			set{ m_Placer = value; }
		}
		[Constructable]
		public YardIronGate( Mobile from, int price, DoorFacing facing, Point3D loc) : base(facing)
		{
			m_Value = price;
			m_Placer = from;
			Movable = false;
			MoveToWorld( loc, from.Map );
			Name = from.Name + "'s Gate";
		}

		public override void Use( Mobile from )
		{
			if( ((BaseDoor)this).Locked && from == this.m_Placer )
			{
				((BaseDoor)this).Locked = false;
				from.SendMessage("You quickly unlock your gate, enter, and lock it behind you");
				base.Use(from);
				((BaseDoor)this).Locked = true;
			}
			else if( ((BaseDoor)this).Locked && from != this.m_Placer )
			{
				from.SendMessage("You are not wanted here.  Please go away!");
			}
			else
			{
				base.Use(from);
			}
		}

		public override void GetContextMenuEntries( Mobile from, List<ContextMenuEntry> list )
		{
			base.GetContextMenuEntries( from, list );
			if( m_Placer == null || from == m_Placer || from.AccessLevel >= AccessLevel.GameMaster )
			{
				list.Add( new YardSecurityEntry( from, (BaseDoor)this ));
				list.Add( new RefundEntry( from, (BaseDoor)this, m_Value ));
			}
		}

		public YardIronGate( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
			writer.Write( m_Placer );
			writer.Write( m_Value );
		}
		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			m_Placer = reader.ReadMobile();
			m_Value = reader.ReadInt();
      }
	}

	public class YardShortIronGate : IronGateShort
	{
		public Mobile m_Placer;
		public int m_Value = 0;

		[CommandProperty( AccessLevel.GameMaster )]
		public Mobile Placer
		{
			get{ return m_Placer; }
			set{ m_Placer = value; }
		}
		[Constructable]
		public YardShortIronGate( Mobile from, int price, DoorFacing facing, Point3D loc) : base(facing)
		{
			m_Value = price;
			m_Placer = from;
			Movable = false;
			MoveToWorld( loc, from.Map );
			Name = from.Name + "'s Gate";
		}

		public override void Use( Mobile from )
		{
			if( ((BaseDoor)this).Locked && from == this.m_Placer )
			{
				((BaseDoor)this).Locked = false;
				from.SendMessage("You quickly unlock your gate, enter, and lock it behind you");
				base.Use(from);
				((BaseDoor)this).Locked = true;
			}
			else if( ((BaseDoor)this).Locked && from != this.m_Placer )
			{
				from.SendMessage("You are not wanted here.  Please go away!");
			}
			else
			{
				base.Use(from);
			}
		}

		public override void GetContextMenuEntries( Mobile from, List<ContextMenuEntry> list )
		{
			base.GetContextMenuEntries( from, list );
			if( m_Placer == null || from == m_Placer || from.AccessLevel >= AccessLevel.GameMaster )
			{
				list.Add( new YardSecurityEntry( from, (BaseDoor)this ));
				list.Add( new RefundEntry( from, (BaseDoor)this, m_Value ));
			}
		}

		public YardShortIronGate( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
			writer.Write( m_Placer );
			writer.Write( m_Value );
		}
		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			m_Placer = reader.ReadMobile();
			m_Value = reader.ReadInt();
      }
	}

	public class YardLightWoodGate : LightWoodGate
	{
		public Mobile m_Placer;
		public int m_Value = 0;

		[CommandProperty( AccessLevel.GameMaster )]
		public Mobile Placer
		{
			get{ return m_Placer; }
			set{ m_Placer = value; }
		}
		[Constructable]
		public YardLightWoodGate( Mobile from, int price, DoorFacing facing, Point3D loc) : base(facing)
		{
			m_Value = price;
			m_Placer = from;
			Movable = false;
			MoveToWorld( loc, from.Map );
			Name = from.Name + "'s Gate";
		}

		public override void Use( Mobile from )
		{
			if( ((BaseDoor)this).Locked && from == this.m_Placer )
			{
				((BaseDoor)this).Locked = false;
				from.SendMessage("You quickly unlock your gate, enter, and lock it behind you");
				base.Use(from);
				((BaseDoor)this).Locked = true;
			}
			else if( ((BaseDoor)this).Locked && from != this.m_Placer )
			{
				from.SendMessage("You are not wanted here.  Please go away!");
			}
			else
			{
				base.Use(from);
			}
		}

		public override void GetContextMenuEntries( Mobile from, List<ContextMenuEntry> list )
		{
			base.GetContextMenuEntries( from, list );
			if( m_Placer == null || from == m_Placer || from.AccessLevel >= AccessLevel.GameMaster )
			{
				list.Add( new YardSecurityEntry( from, (BaseDoor)this ));
				list.Add( new RefundEntry( from, (BaseDoor)this, m_Value ));
			}
		}

		public YardLightWoodGate( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
			writer.Write( m_Placer );
			writer.Write( m_Value );
		}
		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			m_Placer = reader.ReadMobile();
			m_Value = reader.ReadInt();
      }
	}

	public class YardDarkWoodGate : DarkWoodGate
	{
		public Mobile m_Placer;
		public int m_Value = 0;

		[CommandProperty( AccessLevel.GameMaster )]
		public Mobile Placer
		{
			get{ return m_Placer; }
			set{ m_Placer = value; }
		}
		[Constructable]
		public YardDarkWoodGate( Mobile from, int price, DoorFacing facing, Point3D loc) : base(facing)
		{
			m_Value = price;
			m_Placer = from;
			Movable = false;
			MoveToWorld( loc, from.Map );
			Name = from.Name + "'s Gate";
		}

		public override void Use( Mobile from )
		{
			if( ((BaseDoor)this).Locked && from == this.m_Placer )
			{
				((BaseDoor)this).Locked = false;
				from.SendMessage("You quickly unlock your gate, enter, and lock it behind you");
				base.Use(from);
				((BaseDoor)this).Locked = true;
			}
			else if( ((BaseDoor)this).Locked && from != this.m_Placer )
			{
				from.SendMessage("You are not wanted here.  Please go away!");
			}
			else
			{
				base.Use(from);
			}
		}

		public override void GetContextMenuEntries( Mobile from, List<ContextMenuEntry> list )
		{
			base.GetContextMenuEntries( from, list );
			if( m_Placer == null || from == m_Placer || from.AccessLevel >= AccessLevel.GameMaster )
			{
				list.Add( new YardSecurityEntry( from, (BaseDoor)this ));
				list.Add( new RefundEntry( from, (BaseDoor)this, m_Value ));
			}
		}

		public YardDarkWoodGate( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
			writer.Write( m_Placer );
			writer.Write( m_Value );
		}
		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			m_Placer = reader.ReadMobile();
			m_Value = reader.ReadInt();
      }
	}
}