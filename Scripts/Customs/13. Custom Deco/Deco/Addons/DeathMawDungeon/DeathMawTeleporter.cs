using System;
using Server;
using Server.Items;
using Server.Mobiles;
using System.Collections;
using Server.Network;
using Server.Spells;

namespace Server.Items
{
	public abstract class BaseTeleporter : Item			//This is the Base Teleporter which makes adding new teleporters easier.
	{
		public BaseTeleporter() : base( 3948 ){}		//Just a regular looking moongate.
		
		public BaseTeleporter( Serial serial ) : base( serial ) { }
		
		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}
		
		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt();}
		
	}
	//New Teleporter Class
	public class DeathMawSpiderTeleporter : BaseTeleporter		//You can name the teleporter whatever you want, just be consistant through the whole class entry.
	{
		private Point3D m_DestLoc;								//These are the properties available when a GM [props the gate after it's been placed.
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
		public DeathMawSpiderTeleporter()		//This begins the constructable of the teleporter after the properties have been established.
		{
			Visible = true;						//By default, once the gate is placed it's visible.
			Hue = 33;							//The color of the teleporter.
			Movable = false;					//By default all teleporters are not moveable. This prevents GMs from picking them up and putting them in their backpack to crash the shard.
			Weight = 1.0;
			Name = "Death Maw Spider Teleporter";	//This is what players see when they single click on the teleporter.
		}

		public DeathMawSpiderTeleporter( Serial serial ) : base( serial )
		{
		}

		public override bool OnMoveOver( Mobile m )
		{
			Container pack = m.Backpack;
			
			if (pack == null)
			return true;
			
			if( !m_AllowCreatures && !m.Player )
			return true;
			
			Item[] items = pack.FindItemsByType( typeof( DeathMawSpiderToken ) );	//This is where you can change the item needed to enter the gate.
																					//Just change DeathMaw(blah blah) to whatever item name you want it to be.
																					//You have to use the REAL item name, not the one that is displayed when hovered over with a cursor.
																					//You can also change how many of the given item are consumed when the teleporter is entered.
																					//Line 610 will show you how to set a given number of a particular item to be used.
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
			m.SendMessage( " You must have the Death Maw Spider Token if you wish to pass." );	//This is the message that displays if a player is missing a token or item needed
																								//to enter the teleporter.
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
	//End of New Teleporter Class
	public class DeathMawUnholyTeleporter : BaseTeleporter
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
		public DeathMawUnholyTeleporter()
		{
			Visible = true;
			Hue = 66;
			Movable = false;
			Weight = 1.0;
			Name = "Death Maw Unholy Teleporter";
		}

		public DeathMawUnholyTeleporter( Serial serial ) : base( serial )
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
	
	public class DeathMawDragonTeleporter : BaseTeleporter
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
		public DeathMawDragonTeleporter()
		{
			Visible = true;
			Hue = 96;
			Movable = false;
			Weight = 1.0;
			Name = "Death Maw Dragon Teleporter";
		}

		public DeathMawDragonTeleporter( Serial serial ) : base( serial )
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
	
	public class DeathMawFeyTeleporter : BaseTeleporter
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
		public DeathMawFeyTeleporter()
		{
			Visible = true;
			Hue = 45;
			Movable = false;
			Weight = 1.0;
			Name = "Death Maw Fey Teleporter";
		}

		public DeathMawFeyTeleporter( Serial serial ) : base( serial )
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
	
	public class DeathMawDaemonTeleporter : BaseTeleporter
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
		public DeathMawDaemonTeleporter()
		{
			Visible = true;
			Hue = 320;
			Movable = false;
			Weight = 1.0;
			Name = "Death Maw Daemon Teleporter";
		}

		public DeathMawDaemonTeleporter( Serial serial ) : base( serial )
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

	public class NewbieTeleporter : BaseTeleporter
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
		public NewbieTeleporter()
		{
			Visible = true;
			Hue = 1777;
			Movable = false;
			Weight = 1.0;
			Name = "New Player Teleporter";
		}

		public NewbieTeleporter( Serial serial ) : base( serial )
		{
		}

		public override bool OnMoveOver( Mobile m )
		{
			if( !m_AllowCreatures && !m.Player )
				return true;

			if( m.Backpack.ConsumeTotal( typeof( NewbieToken ), 1 ) )

			{
				if( m_TelePets )
				{
					Server.Mobiles.BaseCreature.TeleportPets( m, m_DestLoc, m_DestMap );
				}

				m.PlaySound(0x1F7);
				m.MoveToWorld( m_DestLoc, m_DestMap );
				
				return false;
			}

			m.SendMessage( " You must have the New Player Token if you wish to pass." );
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
	
	public class DeathMawTeleporter : BaseTeleporter
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
		public DeathMawTeleporter()
		{
			Visible = true;
			Hue = 92;
			Movable = false;
			Weight = 1.0;
			Name = "Death Maw Teleporter";
		}

		public DeathMawTeleporter( Serial serial ) : base( serial )
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

	public class DeathMawElementalTeleporter : BaseTeleporter		//You can name the teleporter whatever you want, just be consistant through the whole class entry.
	{
		private Point3D m_DestLoc;								//These are the properties available when a GM [props the gate after it's been placed.
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
		public DeathMawElementalTeleporter()		//This begins the constructable of the teleporter after the properties have been established.
		{
			Visible = true;						//By default, once the gate is placed it's visible.
			Hue = 53;							//The color of the teleporter.
			Movable = false;					//By default all teleporters are not moveable. This prevents GMs from picking them up and putting them in their backpack to crash the shard.
			Weight = 1.0;
			Name = "Death Maw Elemental Teleporter";	//This is what players see when they single click on the teleporter.
		}

		public DeathMawElementalTeleporter( Serial serial ) : base( serial )
		{
		}

		public override bool OnMoveOver( Mobile m )
		{
			Container pack = m.Backpack;
			
			if (pack == null)
			return true;
			
			if( !m_AllowCreatures && !m.Player )
			return true;
			
			Item[] items = pack.FindItemsByType( typeof( DeathMawElementalToken ) );	//This is where you can change the item needed to enter the gate.
																					//Just change DeathMaw(blah blah) to whatever item name you want it to be.
																					//You have to use the REAL item name, not the one that is displayed when hovered over with a cursor.
																					//You can also change how many of the given item are consumed when the teleporter is entered.
																					//Line 610 will show you how to set a given number of a particular item to be used.
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
			m.SendMessage( " You must have the Death Maw Elemental Token if you wish to pass." );	//This is the message that displays if a player is missing a token or item needed
																								//to enter the teleporter.
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
	
	public class RainbowMountTeleporter : BaseTeleporter
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
		public RainbowMountTeleporter()
		{
			Visible = true;
			Hue = 1166;
			Movable = false;
			Weight = 1.0;
			Name = "Rainbow Mount Stable";
		}

		public RainbowMountTeleporter( Serial serial ) : base( serial )
		{
		}

		public override bool OnMoveOver( Mobile m )
		{
			if( !m_AllowCreatures && !m.Player )
				return true;

			if( m.Backpack.ConsumeTotal( typeof( RainbowToken ), 1 ) )

			{
				if( m_TelePets )
				{
					Server.Mobiles.BaseCreature.TeleportPets( m, m_DestLoc, m_DestMap );
				}

				m.PlaySound(0x1F7);
				m.MoveToWorld( m_DestLoc, m_DestMap );
				
				return false;
			}

			m.SendMessage( " You must have the Rainbow Token if you wish to pass." );
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
	public class NosferatuTeleporter : BaseTeleporter
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
		public NosferatuTeleporter()
		{
			Visible = true;
			Hue = 1166;
			Movable = false;
			Weight = 1.0;
			Name = "an unknown teleporter";
		}

		public NosferatuTeleporter( Serial serial ) : base( serial )
		{
		}

		public override bool OnMoveOver( Mobile m )
		{
			if( !m_AllowCreatures && !m.Player )
				return true;

			if( m.Backpack.ConsumeTotal( typeof( VampiricStaffOfPower ), 1 ) )

			{
				if( m_TelePets )
				{
					Server.Mobiles.BaseCreature.TeleportPets( m, m_DestLoc, m_DestMap );
				}

				m.PlaySound(0x1F7);
				m.MoveToWorld( m_DestLoc, m_DestMap );
				
				return false;
			}

			m.SendMessage( " You must have the Vampiric Staff Of Power if you wish to pass." );
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
	
	public class FreddysTeleporter : BaseTeleporter		//You can name the teleporter whatever you want, just be consistant through the whole class entry.
	{
		private Point3D m_DestLoc;								//These are the properties available when a GM [props the gate after it's been placed.
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
		public FreddysTeleporter()		//This begins the constructable of the teleporter after the properties have been established.
		{
			Visible = true;						//By default, once the gate is placed it's visible.
			Hue = 33;							//The color of the teleporter.
			Movable = false;					//By default all teleporters are not moveable. This prevents GMs from picking them up and putting them in their backpack to crash the shard.
			Weight = 1.0;
			Name = "Dreamworld";	//This is what players see when they single click on the teleporter.
		}

		public FreddysTeleporter( Serial serial ) : base( serial )
		{
		}

		public override bool OnMoveOver( Mobile m )
		{
			Container pack = m.Backpack;
			
			if (pack == null)
			return true;
			
			if( !m_AllowCreatures && !m.Player )
			return true;
			
			Item[] items = pack.FindItemsByType( typeof( SleepingDraught ) );	//This is where you can change the item needed to enter the gate.
																					//Just change DeathMaw(blah blah) to whatever item name you want it to be.
																					//You have to use the REAL item name, not the one that is displayed when hovered over with a cursor.
																					//You can also change how many of the given item are consumed when the teleporter is entered.
																					//Line 610 will show you how to set a given number of a particular item to be used.
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
			m.SendMessage( "You must have the sleeping draught to enter." );	//This is the message that displays if a player is missing a token or item needed
																								//to enter the teleporter.
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
	
	public class VigosTeleporter : BaseTeleporter		//You can name the teleporter whatever you want, just be consistant through the whole class entry.
	{
		private Point3D m_DestLoc;								//These are the properties available when a GM [props the gate after it's been placed.
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
		public VigosTeleporter()		//This begins the constructable of the teleporter after the properties have been established.
		{
			Visible = true;						//By default, once the gate is placed it's visible.
			Hue = 33;							//The color of the teleporter.
			Movable = false;					//By default all teleporters are not moveable. This prevents GMs from picking them up and putting them in their backpack to crash the shard.
			Weight = 1.0;
			Name = "Vigo's Painting";	//This is what players see when they single click on the teleporter.
		}

		public VigosTeleporter( Serial serial ) : base( serial )
		{
		}

		public override bool OnMoveOver( Mobile m )
		{
			Container pack = m.Backpack;
			
			if (pack == null)
			return true;
			
			if( !m_AllowCreatures && !m.Player )
			return true;
			
			Item[] items = pack.FindItemsByType( typeof( MandrakeBaby ) );	//This is where you can change the item needed to enter the gate.
																					//Just change DeathMaw(blah blah) to whatever item name you want it to be.
																					//You have to use the REAL item name, not the one that is displayed when hovered over with a cursor.
																					//You can also change how many of the given item are consumed when the teleporter is entered.
																					//Line 610 will show you how to set a given number of a particular item to be used.
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
			m.SendMessage( "You must have the Mandrake Baby to enter." );	//This is the message that displays if a player is missing a token or item needed
																								//to enter the teleporter.
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
	
	public class PyramidTeleporter : BaseTeleporter
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
		public PyramidTeleporter()
		{
			Visible = true;
			Hue = 66;
			Movable = false;
			Weight = 1.0;
			Name = "The Forgotten Pyramid Teleporter";
		}

		public PyramidTeleporter( Serial serial ) : base( serial )
		{
		}

		public override bool OnMoveOver( Mobile m )
		{
			Container pack = m.Backpack;
			
			if (pack == null)
			return true;
			
			if( !m_AllowCreatures && !m.Player )
			return true;
			
			Item[] items = pack.FindItemsByType( typeof( JonesyAxe ) );
			
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
			m.SendMessage( "You must have Jonesy's Lucky Pickaxe if you wish to pass." );
			
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
	
	public class TempleOfDoomTeleporter : BaseTeleporter
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
		public TempleOfDoomTeleporter()
		{
			Visible = true;
			Hue = 1900;
			Movable = false;
			Weight = 1.0;
			Name = "a busty statue";
		}

		public TempleOfDoomTeleporter( Serial serial ) : base( serial )
		{
		}

		public override bool OnMoveOver( Mobile m )
		{
			Container pack = m.Backpack;
			
			if (pack == null)
			return true;
			
			if( !m_AllowCreatures && !m.Player )
			return true;
			
			Item[] items = pack.FindItemsByType( typeof( JonesyLantern ) );
			
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
			m.SendMessage( "You rub your hand across the statue like Willie said Jonesy did, but nothing happens." );
			
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