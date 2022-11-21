//////////////////////////////////////////////////////////////////
//                     Group Dungeons 1.6 Final                 //
//                           -The Jedi-                         //
//                          RunUO 2.0 RC1                       //
//////////////////////////////////////////////////////////////////

using System;
using Server;
using Server.Network;
using Server.Mobiles;

namespace Server.Items
{
    public class GroupDungeonTeleporter : Teleporter
    {
        private GroupDungeonStone m_Stone;

        [CommandProperty(AccessLevel.GameMaster)]
        public GroupDungeonStone Stone
        {
            get { return m_Stone; }
            set { m_Stone = value; InvalidateProperties(); }
        }

        [Constructable]
		public GroupDungeonTeleporter() : this( new Point3D( 0, 0, 0 ), null, false )
		{
        }

		[Constructable]
		public GroupDungeonTeleporter( Point3D pointDest, Map mapDest ) : this( pointDest, mapDest, false )
		{
        }

        [Constructable]
		public GroupDungeonTeleporter( Point3D pointDest, Map mapDest, bool creatures ) : base( pointDest, mapDest, creatures)
		{
            Name = "an instance zone-in teleporter";
            Hue = 1157;
            Visible = true;
            base.MapDest = this.Map;
            base.PointDest = this.Location;
        }

        public GroupDungeonTeleporter(Serial serial) : base(serial)
		{
            Name = "an instance zone-in teleporter";
            Hue = 1157;
            Visible = true;
            base.MapDest = this.Map;
            base.PointDest = this.Location;
		}

        public override bool OnMoveOver(Mobile m)
        {
            if (m_Stone != null)
            {
                if (m_Stone.IRegion != null)
                {
                    if (m_Stone.IRegion.CanEnter(m))
                        return base.OnMoveOver(m);
                }
                else
                    m.SendMessage(34, "Teleporter not linked to a dungeon region. Contact staff.");
            }
            else
                m.SendMessage(34, "Teleporter not linked to a dungeon stone. Contact staff.");
            return true;
        }

        public override void DoTeleport(Mobile m)
        {
            Map map = base.MapDest;

            if (map == null || map == Map.Internal)
                map = m.Map;

            Point3D p = base.PointDest;

            if (p == Point3D.Zero)
                p = m.Location;

            // Check for AllowPets here
            if (!m_Stone.AllowPets && CountPets(m) > 0)
                m.SendMessage(34, "Pets are not allowed to enter {0}.", m_Stone.DungeonName);
            else
                Server.Mobiles.BaseCreature.TeleportPets(m, p, map);

            bool sendEffect = (!m.Hidden || m.AccessLevel == AccessLevel.Player);

            if (base.SourceEffect && sendEffect)
                Effects.SendLocationEffect(m.Location, m.Map, 0x3728, 10, 10);

            m.MoveToWorld(p, map);

            if (base.DestEffect && sendEffect)
                Effects.SendLocationEffect(m.Location, m.Map, 0x3728, 10, 10);

            if (base.SoundID > 0 && sendEffect)
                Effects.PlaySound(m.Location, m.Map, base.SoundID);
        }

        public int CountPets(Mobile master)
        {
            int count = 0;

            foreach (Mobile m in master.GetMobilesInRange(3))
            {
                if (m is BaseCreature)
                {
                    BaseCreature pet = (BaseCreature)m;

                    if (pet.Controlled && pet.ControlMaster == master)
                        count++;
                }
            }

            return count;
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version

            writer.Write(m_Stone);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();

            switch (version)
            {
                case 0:
                    {
                        m_Stone = (GroupDungeonStone)reader.ReadItem();
                        break;
                    }
            }
        }
    }

    public class GroupDungeonExit : Teleporter
    {
        [Constructable]
        public GroupDungeonExit()
            : this(new Point3D(0, 0, 0), null, false)
        {
        }

        [Constructable]
        public GroupDungeonExit(Point3D pointDest, Map mapDest)
            : this(pointDest, mapDest, false)
        {
        }

        [Constructable]
        public GroupDungeonExit(Point3D pointDest, Map mapDest, bool creatures)
            : base(pointDest, mapDest, creatures)
        {
            Name = "an instance zone-out teleporter";
            Hue = 1155;
            Visible = false;
            base.MapDest = this.Map;
            base.PointDest = this.Location;
        }

        public GroupDungeonExit(Serial serial)
            : base(serial)
        {
            Name = "an instance zone-out teleporter";
            Hue = 1155;
            Visible = false;
            base.MapDest = this.Map;
            base.PointDest = this.Location;
        }

        public override bool OnMoveOver(Mobile m)
        {
            if (m.Alive && m is PlayerMobile) // Only allow living players to leave.
                return base.OnMoveOver(m);    // This is to wait for rez or wipe.
                    
            return true;
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();

            switch (version)
            {
                case 0:
                    {
                        break;
                    }
            }
        }
    }
	
	public class DMSpiderTeleporter : GroupDungeonTeleporter		
	{
		private Point3D m_DestLoc;								
		private Map     m_DestMap;
		private bool    m_AllowCreatures;
		private bool    m_TelePets;

		[CommandProperty( AccessLevel.GameMaster )]
		public Point3D Location
		{
			get { return m_DestLoc; }
			set { m_DestLoc = value; InvalidateProperties(); }	
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public Map Map
		{
			get { return m_DestMap; }
			set { m_DestMap = value; InvalidateProperties(); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public bool AllowCreatures
		{
			get { return m_AllowCreatures; }
			set { m_AllowCreatures = value; InvalidateProperties(); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public bool TelePets
		{
			get { return m_TelePets; }
			set { m_TelePets = value; InvalidateProperties(); }
		}
		
		[Constructable]
		public DMSpiderTeleporter()		
		{
			Visible = true;						
			Hue = 33;							
			Movable = false;					
			Weight = 1.0;
			Name = "Death Maw Spider Teleporter";
		}

		public DMSpiderTeleporter( Serial serial ) : base( serial )
		{
		}

		public override bool OnMoveOver( Mobile m )
		{
			Container pack = m.Backpack;
			
			if (pack == null)
			return true;
			
			if( !m_AllowCreatures && !m.Player )
			return true;
			
			Item[] items = pack.FindItemsByType( typeof( DeathMawSpiderToken ) );
			if (items != null && items.Length > 0)
				{
					foreach (Item item in items)
					item.Consume();

					if( m_TelePets )
						{
							Server.Mobiles.BaseCreature.TeleportPets( m, m_DestLoc, m_DestMap );
						}
					m.PlaySound(0x1F7);
					
					m.MoveToWorld( m_DestLoc, m_DestMap );

					return false;
				}
			m.SendMessage( " You must have the Death Maw Spider Token if you wish to pass." );
			return true;
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version

			writer.Write( m_DestLoc );
			writer.Write( m_DestMap );
			writer.Write( m_AllowCreatures );
			writer.Write( m_TelePets );
		}


		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();

			m_DestLoc = reader.ReadPoint3D();
			m_DestMap = reader.ReadMap();
			m_AllowCreatures = reader.ReadBool();
			m_TelePets = reader.ReadBool();
		}
	}
	public class DMUnholyTeleporter : GroupDungeonTeleporter
	{
		private Point3D m_DestLoc;
		private Map     m_DestMap;
		private bool    m_AllowCreatures;
		private bool    m_TelePets;

		[CommandProperty( AccessLevel.GameMaster )]
		public Point3D Location
		{
			get { return m_DestLoc; }
			set { m_DestLoc = value; InvalidateProperties(); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public Map Map
		{
			get { return m_DestMap; }
			set { m_DestMap = value; InvalidateProperties(); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public bool AllowCreatures
		{
			get { return m_AllowCreatures; }
			set { m_AllowCreatures = value; InvalidateProperties(); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public bool TelePets
		{
			get { return m_TelePets; }
			set { m_TelePets = value; InvalidateProperties(); }
		}
		
		[Constructable]
		public DMUnholyTeleporter()
		{
			Visible = true;
			Hue = 66;
			Movable = false;
			Weight = 1.0;
			Name = "Death Maw Unholy Teleporter";
		}

		public DMUnholyTeleporter( Serial serial ) : base( serial )
		{
		}

		public override bool OnMoveOver( Mobile m )
		{
			Container pack = m.Backpack;
			
			if (pack == null)
			return true;
			
			if( !m_AllowCreatures && !m.Player )
			return true;
			
			Item[] items = pack.FindItemsByType( typeof( DeathMawUnholyToken ) );
			
			if (items != null && items.Length > 0)
				{
					foreach (Item item in items)
					item.Consume();

					if( m_TelePets )
						{
							Server.Mobiles.BaseCreature.TeleportPets( m, m_DestLoc, m_DestMap );
						}
					m.PlaySound(0x1F7);
					
					m.MoveToWorld( m_DestLoc, m_DestMap );

					return false;
				}
			m.SendMessage( " You must have the Death Maw Unholy Token if you wish to pass." );
			
			return true;
		}


		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version

			writer.Write( m_DestLoc );
			writer.Write( m_DestMap );
			writer.Write( m_AllowCreatures );
			writer.Write( m_TelePets );
		}


		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();

			m_DestLoc = reader.ReadPoint3D();
			m_DestMap = reader.ReadMap();
			m_AllowCreatures = reader.ReadBool();
			m_TelePets = reader.ReadBool();
		}
	}
	public class DMDragonTeleporter : GroupDungeonTeleporter
	{
		private Point3D m_DestLoc;
		private Map     m_DestMap;
		private bool    m_AllowCreatures;
		private bool    m_TelePets;

		[CommandProperty( AccessLevel.GameMaster )]
		public Point3D Location
		{
			get { return m_DestLoc; }
			set { m_DestLoc = value; InvalidateProperties(); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public Map Map
		{
			get { return m_DestMap; }
			set { m_DestMap = value; InvalidateProperties(); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public bool AllowCreatures
		{
			get { return m_AllowCreatures; }
			set { m_AllowCreatures = value; InvalidateProperties(); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public bool TelePets
		{
			get { return m_TelePets; }
			set { m_TelePets = value; InvalidateProperties(); }
		}
		
		[Constructable]
		public DMDragonTeleporter()
		{
			Visible = true;
			Hue = 96;
			Movable = false;
			Weight = 1.0;
			Name = "Death Maw Dragon Teleporter";
		}

		public DMDragonTeleporter( Serial serial ) : base( serial )
		{
		}

		public override bool OnMoveOver( Mobile m )
		{
			Container pack = m.Backpack;
			
			if (pack == null)
			return true;
			
			if( !m_AllowCreatures && !m.Player )
			return true;
			
			Item[] items = pack.FindItemsByType( typeof( DeathMawDragonToken ) );
			
			if (items != null && items.Length > 0)
				{
					foreach (Item item in items)
					item.Consume();

					if( m_TelePets )
						{
							Server.Mobiles.BaseCreature.TeleportPets( m, m_DestLoc, m_DestMap );
						}
					m.PlaySound(0x1F7);
					
					m.MoveToWorld( m_DestLoc, m_DestMap );

					return false;
				}
			m.SendMessage( " You must have the Death Maw Dragon Token if you wish to pass." );
			
			return true;
		}


		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version

			writer.Write( m_DestLoc );
			writer.Write( m_DestMap );
			writer.Write( m_AllowCreatures );
			writer.Write( m_TelePets );
		}


		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();

			m_DestLoc = reader.ReadPoint3D();
			m_DestMap = reader.ReadMap();
			m_AllowCreatures = reader.ReadBool();
			m_TelePets = reader.ReadBool();
		}
	}
	
	public class DMFeyTeleporter : GroupDungeonTeleporter
	{
		private Point3D m_DestLoc;
		private Map     m_DestMap;
		private bool    m_AllowCreatures;
		private bool    m_TelePets;

		[CommandProperty( AccessLevel.GameMaster )]
		public Point3D Location
		{
			get { return m_DestLoc; }
			set { m_DestLoc = value; InvalidateProperties(); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public Map Map
		{
			get { return m_DestMap; }
			set { m_DestMap = value; InvalidateProperties(); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public bool AllowCreatures
		{
			get { return m_AllowCreatures; }
			set { m_AllowCreatures = value; InvalidateProperties(); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public bool TelePets
		{
			get { return m_TelePets; }
			set { m_TelePets = value; InvalidateProperties(); }
		}
		
		[Constructable]
		public DMFeyTeleporter()
		{
			Visible = true;
			Hue = 45;
			Movable = false;
			Weight = 1.0;
			Name = "Death Maw Fey Teleporter";
		}

		public DMFeyTeleporter( Serial serial ) : base( serial )
		{
		}

		public override bool OnMoveOver( Mobile m )
		{
			Container pack = m.Backpack;
			
			if (pack == null)
			return true;
			
			if( !m_AllowCreatures && !m.Player )
			return true;
			
			Item[] items = pack.FindItemsByType( typeof( DeathMawFeyToken ) );
			
			if (items != null && items.Length > 0)
				{
					foreach (Item item in items)
					item.Consume();

					if( m_TelePets )
						{
							Server.Mobiles.BaseCreature.TeleportPets( m, m_DestLoc, m_DestMap );
						}
					m.PlaySound(0x1F7);
					
					m.MoveToWorld( m_DestLoc, m_DestMap );

					return false;
				}
			m.SendMessage( " You must have the Death Maw Fey Token if you wish to pass." );
			
			return true;
		}


		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version

			writer.Write( m_DestLoc );
			writer.Write( m_DestMap );
			writer.Write( m_AllowCreatures );
			writer.Write( m_TelePets );
		}


		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();

			m_DestLoc = reader.ReadPoint3D();
			m_DestMap = reader.ReadMap();
			m_AllowCreatures = reader.ReadBool();
			m_TelePets = reader.ReadBool();
		}
	}

	public class DMElementalTeleporter : Teleporter		
	{
		private Point3D m_DestLoc;								
		private Map     m_DestMap;
		private bool    m_AllowCreatures;
		private bool    m_TelePets;

		[CommandProperty( AccessLevel.GameMaster )]
		public Point3D Location
		{
			get { return m_DestLoc; }
			set { m_DestLoc = value; InvalidateProperties(); }	
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public Map Map
		{
			get { return m_DestMap; }
			set { m_DestMap = value; InvalidateProperties(); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public bool AllowCreatures
		{
			get { return m_AllowCreatures; }
			set { m_AllowCreatures = value; InvalidateProperties(); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public bool TelePets
		{
			get { return m_TelePets; }
			set { m_TelePets = value; InvalidateProperties(); }
		}
		
		[Constructable]
		public DMElementalTeleporter()		
		{
			Visible = true;						
			Hue = 53;							
			Movable = false;					
			Weight = 1.0;
			Name = "Death Maw Elemental Teleporter";	
		}

		public DMElementalTeleporter( Serial serial ) : base( serial )
		{
		}

		public override bool OnMoveOver( Mobile m )
		{
			Container pack = m.Backpack;
			
			if (pack == null)
			return true;
			
			if( !m_AllowCreatures && !m.Player )
			return true;
			
			Item[] items = pack.FindItemsByType( typeof( DeathMawElementalToken ) );
			if (items != null && items.Length > 0)
				{
					foreach (Item item in items)
					item.Consume();

					if( m_TelePets )
						{
							Server.Mobiles.BaseCreature.TeleportPets( m, m_DestLoc, m_DestMap );
						}
					m.PlaySound(0x1F7);
					
					m.MoveToWorld( m_DestLoc, m_DestMap );

					return false;
				}
			m.SendMessage( " You must have the Death Maw Elemental Token if you wish to pass." );
			return true;
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version

			writer.Write( m_DestLoc );
			writer.Write( m_DestMap );
			writer.Write( m_AllowCreatures );
			writer.Write( m_TelePets );
		}


		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();

			m_DestLoc = reader.ReadPoint3D();
			m_DestMap = reader.ReadMap();
			m_AllowCreatures = reader.ReadBool();
			m_TelePets = reader.ReadBool();
		}
	}
	
	public class DMDaemonTeleporter : GroupDungeonTeleporter
	{
		private Point3D m_DestLoc;
		private Map     m_DestMap;
		private bool    m_AllowCreatures;
		private bool    m_TelePets;

		[CommandProperty( AccessLevel.GameMaster )]
		public Point3D Location
		{
			get { return m_DestLoc; }
			set { m_DestLoc = value; InvalidateProperties(); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public Map Map
		{
			get { return m_DestMap; }
			set { m_DestMap = value; InvalidateProperties(); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public bool AllowCreatures
		{
			get { return m_AllowCreatures; }
			set { m_AllowCreatures = value; InvalidateProperties(); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public bool TelePets
		{
			get { return m_TelePets; }
			set { m_TelePets = value; InvalidateProperties(); }
		}
		
		[Constructable]
		public DMDaemonTeleporter()
		{
			Visible = true;
			Hue = 320;
			Movable = false;
			Weight = 1.0;
			Name = "Death Maw Daemon Teleporter";
		}

		public DMDaemonTeleporter( Serial serial ) : base( serial )
		{
		}

		public override bool OnMoveOver( Mobile m )
		{
			Container pack = m.Backpack;
			
			if (pack == null)
			return true;
			
			if( !m_AllowCreatures && !m.Player )
			return true;
			
			Item[] items = pack.FindItemsByType( typeof( DeathMawDaemonToken ) );
			
			if (items != null && items.Length > 0)
				{
					foreach (Item item in items)
					item.Consume();

					if( m_TelePets )
						{
							Server.Mobiles.BaseCreature.TeleportPets( m, m_DestLoc, m_DestMap );
						}
					m.PlaySound(0x1F7);
					
					m.MoveToWorld( m_DestLoc, m_DestMap );

					return false;
				}
			m.SendMessage( " You must have the Death Maw Daemon Token if you wish to pass." );
			
			return true;
		}


		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version

			writer.Write( m_DestLoc );
			writer.Write( m_DestMap );
			writer.Write( m_AllowCreatures );
			writer.Write( m_TelePets );
		}


		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();

			m_DestLoc = reader.ReadPoint3D();
			m_DestMap = reader.ReadMap();
			m_AllowCreatures = reader.ReadBool();
			m_TelePets = reader.ReadBool();
		}
	}
	
	public class DMTeleporter : Teleporter
	{
		private Point3D m_DestLoc;
		private Map     m_DestMap;
		private bool    m_AllowCreatures;
		private bool    m_TelePets;

		[CommandProperty( AccessLevel.GameMaster )]
		public Point3D Location
		{
			get { return m_DestLoc; }
			set { m_DestLoc = value; InvalidateProperties(); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public Map Map
		{
			get { return m_DestMap; }
			set { m_DestMap = value; InvalidateProperties(); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public bool AllowCreatures
		{
			get { return m_AllowCreatures; }
			set { m_AllowCreatures = value; InvalidateProperties(); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public bool TelePets
		{
			get { return m_TelePets; }
			set { m_TelePets = value; InvalidateProperties(); }
		}
		
		[Constructable]
		public DMTeleporter()
		{
			Visible = true;
			Hue = 92;
			Movable = false;
			Weight = 1.0;
			Name = "Death Maw Teleporter";
		}

		public DMTeleporter( Serial serial ) : base( serial )
		{
		}

		public override bool OnMoveOver( Mobile m )
		{
			Container pack = m.Backpack;
			
			if (pack == null)
			return true;
			
			if( !m_AllowCreatures && !m.Player )
			return true;
			
			Item[] items = pack.FindItemsByType( typeof( DeathMawToken ) );
			
			if (items != null && items.Length > 0)
				{
					foreach (Item item in items)
					item.Consume();

					if( m_TelePets )
						{
							Server.Mobiles.BaseCreature.TeleportPets( m, m_DestLoc, m_DestMap );
						}
					m.PlaySound(0x1F7);
					
					m.MoveToWorld( m_DestLoc, m_DestMap );

					return false;
				}
			m.SendMessage( " You must have the Death Maw Token if you wish to pass." );
			
			return true;
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version

			writer.Write( m_DestLoc );
			writer.Write( m_DestMap );
			writer.Write( m_AllowCreatures );
			writer.Write( m_TelePets );
		}


		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();

			m_DestLoc = reader.ReadPoint3D();
			m_DestMap = reader.ReadMap();
			m_AllowCreatures = reader.ReadBool();
			m_TelePets = reader.ReadBool();
		}
	}
}