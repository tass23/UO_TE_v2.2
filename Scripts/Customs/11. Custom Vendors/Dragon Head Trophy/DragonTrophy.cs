using System;
using System.Collections;
using System.Collections.Generic;
using Server.Multis;
using Server.Mobiles;
using Server.Network;

namespace Server.Items
{
	[FlipableAttribute( 8756, 8757 )]
	public abstract class BaseDragonTrophy : Item
	{
		public override double DefaultWeight
		{
			get { return 50; }
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 1 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}

		public BaseDragonTrophy() : base( 8756 )
		{
			LootType = LootType.Blessed;
			Light = LightType.Circle150;
			Hue = 0;
		}

		public BaseDragonTrophy( Serial serial ) : base( serial )
		{
		}

		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );
		}
	}

	public class FireDragonTrophy : BaseDragonTrophy
	{
		private DateTime m_TrophyTime;
		private string m_KillerName;
		[CommandProperty(AccessLevel.GameMaster)]
		public string KillerName
		{
			get { return m_KillerName; }
			set
			{
				m_KillerName = value;
				Name = m_KillerName;
			}
		}

		[Constructable]
		public FireDragonTrophy()
		{
			Name = "a Fire Dragon Trophy";
			Hue = 1357;
		}

		public FireDragonTrophy( Serial serial ) : base( serial )
		{
		}

		public override void OnDoubleClick( Mobile from )
		{
			m_TrophyTime = DateTime.Now;
			if( !IsChildOf( from.Backpack ) )
			{
				from.SendLocalizedMessage( 1042001 );
				return;
			}

			if (m_KillerName == null)
			{
				from.SendMessage( "You have accepted ownership..." );
				KillerName = from.Name;
				Name = "a Fire Dragon killed by " + KillerName + " on " + m_TrophyTime.ToString() + ".";
			}
		}

		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );

			if ( m_KillerName != null )
			list.Add( 1072304, KillerName ); 
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version

			writer.Write(m_KillerName);
		}
		
		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			m_KillerName = reader.ReadString();
		}
	}

	public class VenomDragonTrophy : BaseDragonTrophy
	{
		private DateTime m_TrophyTime;
		private string m_KillerName;
		[CommandProperty(AccessLevel.GameMaster)]
		public string KillerName
		{
			get { return m_KillerName; }
			set
			{
				m_KillerName = value;
				Name = m_KillerName;
			}
		}

		[Constructable]
		public VenomDragonTrophy()
		{
			Name = "a Venom Dragon Trophy";
			Hue = 1367;
		}

		public VenomDragonTrophy( Serial serial ) : base( serial )
		{
		}

		public override void OnDoubleClick( Mobile from )
		{
			m_TrophyTime = DateTime.Now;
			if( !IsChildOf( from.Backpack ) )
			{
				from.SendLocalizedMessage( 1042001 );
				return;
			}

			if (m_KillerName == null)
			{
				from.SendMessage( "You have accepted ownership..." );
				KillerName = from.Name;
				Name = "a Venom Dragon killed by " + KillerName + " on " + m_TrophyTime.ToString() + ".";
			}
		}

		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );

			if ( m_KillerName != null )
			list.Add( 1072304, KillerName ); 
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version

			writer.Write(m_KillerName);
		}
		
		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			m_KillerName = reader.ReadString();
		}
	}

	public class ShadowWyrmTrophy : BaseDragonTrophy
	{
		private DateTime m_TrophyTime;
		private string m_KillerName;
		[CommandProperty(AccessLevel.GameMaster)]
		public string KillerName
		{
			get { return m_KillerName; }
			set
			{
				m_KillerName = value;
				Name = m_KillerName;
			}
		}

		[Constructable]
		public ShadowWyrmTrophy()
		{
			Name = "a Shadow Wyrm Trophy";
			Hue = 2051;
		}

		public ShadowWyrmTrophy( Serial serial ) : base( serial )
		{
		}

		public override void OnDoubleClick( Mobile from )
		{
			m_TrophyTime = DateTime.Now;
			if( !IsChildOf( from.Backpack ) )
			{
				from.SendLocalizedMessage( 1042001 );
				return;
			}		

			if (m_KillerName == null)
			{
				from.SendMessage( "You have accepted ownership..." );
				KillerName = from.Name;
				Name = "a Shadow Wyrm killed by " + KillerName + " on " + m_TrophyTime.ToString() + ".";
			}
		}

		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );

			if ( m_KillerName != null )
			list.Add( 1072304, KillerName ); 
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version

			writer.Write(m_KillerName);
		}
		
		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			m_KillerName = reader.ReadString();
		}
	}

	public class OldDragonTrophy : BaseDragonTrophy
	{
		private DateTime m_TrophyTime;
		private string m_KillerName;
		[CommandProperty(AccessLevel.GameMaster)]
		public string KillerName
		{
			get { return m_KillerName; }
			set
			{
				m_KillerName = value;
				Name = m_KillerName;
			}
		}

		[Constructable]
		public OldDragonTrophy()
		{
			Name = "an Old Dragon Trophy";
			Hue = 2982;
		}

		public OldDragonTrophy( Serial serial ) : base( serial )
		{
		}

		public override void OnDoubleClick( Mobile from )
		{
			m_TrophyTime = DateTime.Now;
			if( !IsChildOf( from.Backpack ) )
			{
				from.SendLocalizedMessage( 1042001 );
				return;
			}		

			if (m_KillerName == null)
			{
				from.SendMessage( "You have accepted ownership..." );
				KillerName = from.Name;
				Name = "an Old Dragon killed by " + KillerName + " on " + m_TrophyTime.ToString() + ".";
			}
		}

		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );

			if ( m_KillerName != null )
			list.Add( 1072304, KillerName ); 
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version

			writer.Write(m_KillerName);
		}
		
		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			m_KillerName = reader.ReadString();
		}
	}

	public class MythicalDragonTrophy : BaseDragonTrophy
	{
		private DateTime m_TrophyTime;
		private string m_KillerName;
		[CommandProperty(AccessLevel.GameMaster)]
		public string KillerName
		{
			get { return m_KillerName; }
			set
			{
				m_KillerName = value;
				Name = m_KillerName;
			}
		}

		[Constructable]
		public MythicalDragonTrophy()
		{
			Name = "a Mythical Dragon Trophy";
			Hue = 2955;
		}

		public MythicalDragonTrophy( Serial serial ) : base( serial )
		{
		}

		public override void OnDoubleClick( Mobile from )
		{
			m_TrophyTime = DateTime.Now;
			if( !IsChildOf( from.Backpack ) )
			{
				from.SendLocalizedMessage( 1042001 );
				return;
			}		

			if (m_KillerName == null)
			{
				from.SendMessage( "You have accepted ownership..." );
				KillerName = from.Name;
				Name = "a Mythical Dragon killed by " + KillerName + " on " + m_TrophyTime.ToString() + ".";
			}
		}

		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );

			if ( m_KillerName != null )
			list.Add( 1072304, KillerName ); 
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version

			writer.Write(m_KillerName);
		}
		
		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			m_KillerName = reader.ReadString();
		}
	}

	public class AdolescentDragonTrophy : BaseDragonTrophy
	{
		private DateTime m_TrophyTime;
		private string m_KillerName;
		[CommandProperty(AccessLevel.GameMaster)]
		public string KillerName
		{
			get { return m_KillerName; }
			set
			{
				m_KillerName = value;
				Name = m_KillerName;
			}
		}

		[Constructable]
		public AdolescentDragonTrophy()
		{
			Name = "an Adolescent Dragon Trophy";
			Hue = 2426;
		}

		public AdolescentDragonTrophy( Serial serial ) : base( serial )
		{
		}

		public override void OnDoubleClick( Mobile from )
		{
			m_TrophyTime = DateTime.Now;
			if( !IsChildOf( from.Backpack ) )
			{
				from.SendLocalizedMessage( 1042001 );
				return;
			}		

			if (m_KillerName == null)
			{
				from.SendMessage( "You have accepted ownership..." );
				KillerName = from.Name;
				Name = "an Adolescent Dragon killed by " + KillerName + " on " + m_TrophyTime.ToString() + ".";
			}
		}

		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );

			if ( m_KillerName != null )
			list.Add( 1072304, KillerName ); 
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version

			writer.Write(m_KillerName);
		}
		
		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			m_KillerName = reader.ReadString();
		}
	}

	public class ChimeraTrophy : BaseDragonTrophy
	{
		private DateTime m_TrophyTime;
		private string m_KillerName;
		[CommandProperty(AccessLevel.GameMaster)]
		public string KillerName
		{
			get { return m_KillerName; }
			set
			{
				m_KillerName = value;
				Name = m_KillerName;
			}
		}

		[Constructable]
		public ChimeraTrophy()
		{
			Name = "a Chimera Trophy";
			Hue = 67;
		}

		public ChimeraTrophy( Serial serial ) : base( serial )
		{
		}

		public override void OnDoubleClick( Mobile from )
		{
			m_TrophyTime = DateTime.Now;
			if( !IsChildOf( from.Backpack ) )
			{
				from.SendLocalizedMessage( 1042001 );
				return;
			}		

			if (m_KillerName == null)
			{
				from.SendMessage( "You have accepted ownership..." );
				KillerName = from.Name;
				Name = "a Chimera killed by " + KillerName + " on " + m_TrophyTime.ToString() + ".";
			}
		}

		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );

			if ( m_KillerName != null )
			list.Add( 1072304, KillerName ); 
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version

			writer.Write(m_KillerName);
		}
		
		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			m_KillerName = reader.ReadString();
		}
	}

	public class ElderDragonTrophy : BaseDragonTrophy
	{
		private DateTime m_TrophyTime;
		private string m_KillerName;
		[CommandProperty(AccessLevel.GameMaster)]
		public string KillerName
		{
			get { return m_KillerName; }
			set
			{
				m_KillerName = value;
				Name = m_KillerName;
			}
		}

		[Constructable]
		public ElderDragonTrophy()
		{
			Name = "an Elder Dragon Trophy";
			Hue = 2212;
		}

		public ElderDragonTrophy( Serial serial ) : base( serial )
		{
		}

		public override void OnDoubleClick( Mobile from )
		{
			m_TrophyTime = DateTime.Now;
			if( !IsChildOf( from.Backpack ) )
			{
				from.SendLocalizedMessage( 1042001 );
				return;
			}		

			if (m_KillerName == null)
			{
				from.SendMessage( "You have accepted ownership..." );
				KillerName = from.Name;
				Name = "an Elder Dragon killed by " + KillerName + " on " + m_TrophyTime.ToString() + ".";
			}
		}

		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );

			if ( m_KillerName != null )
			list.Add( 1072304, KillerName ); 
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version

			writer.Write(m_KillerName);
		}
		
		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			m_KillerName = reader.ReadString();
		}
	}

	public class ShadowDrakeTrophy : BaseDragonTrophy
	{
		private DateTime m_TrophyTime;
		private string m_KillerName;
		[CommandProperty(AccessLevel.GameMaster)]
		public string KillerName
		{
			get { return m_KillerName; }
			set
			{
				m_KillerName = value;
				Name = m_KillerName;
			}
		}

		[Constructable]
		public ShadowDrakeTrophy()
		{
			Name = "a Shadow Drake Trophy";
			Hue = 2051;
		}

		public ShadowDrakeTrophy( Serial serial ) : base( serial )
		{
		}

		public override void OnDoubleClick( Mobile from )
		{
			m_TrophyTime = DateTime.Now;
			if( !IsChildOf( from.Backpack ) )
			{
				from.SendLocalizedMessage( 1042001 );
				return;
			}		

			if (m_KillerName == null)
			{
				from.SendMessage( "You have accepted ownership..." );
				KillerName = from.Name;
				Name = "a Shadow Drake killed by " + KillerName + " on " + m_TrophyTime.ToString() + ".";
			}
		}

		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );

			if ( m_KillerName != null )
			list.Add( 1072304, KillerName ); 
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version

			writer.Write(m_KillerName);
		}
		
		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			m_KillerName = reader.ReadString();
		}
	}

	public class JadeDragonTrophy : BaseDragonTrophy
	{
		private DateTime m_TrophyTime;
		private string m_KillerName;
		[CommandProperty(AccessLevel.GameMaster)]
		public string KillerName
		{
			get { return m_KillerName; }
			set
			{
				m_KillerName = value;
				Name = m_KillerName;
			}
		}

		[Constructable]
		public JadeDragonTrophy()
		{
			Name = "a Jade Dragon Trophy";
			Hue = 2963;
		}

		public JadeDragonTrophy( Serial serial ) : base( serial )
		{
		}

		public override void OnDoubleClick( Mobile from )
		{
			m_TrophyTime = DateTime.Now;
			if( !IsChildOf( from.Backpack ) )
			{
				from.SendLocalizedMessage( 1042001 );
				return;
			}		

			if (m_KillerName == null)
			{
				from.SendMessage( "You have accepted ownership..." );
				KillerName = from.Name;
				Name = "a Jade Dragon killed by " + KillerName + " on " + m_TrophyTime.ToString() + ".";
			}
		}

		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );

			if ( m_KillerName != null )
			list.Add( 1072304, KillerName ); 
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version

			writer.Write(m_KillerName);
		}
		
		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			m_KillerName = reader.ReadString();
		}
	}
}