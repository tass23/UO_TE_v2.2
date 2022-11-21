using System;
using System.Collections;
using System.Collections.Generic;
using Server;
using Server.Items;
using Server.Multis;
using Server.Network;
using Server.ContextMenus;
using Server.Gumps;

namespace Server.Items
{
	public class WoodStone : Item, ISecurable
	{
		private SecureLevel m_Level;

		[CommandProperty( AccessLevel.GameMaster )]
		public SecureLevel Level
		{
			get{ return m_Level; }
			set{ m_Level = value; }
		}
		
		public override string DefaultName
		{
			get { return "a Carpenter Supply Stone"; }
		}

		[Constructable]
		public WoodStone() : base( 0xED4 )
		{
			Movable = false;
			Hue = 1095;
		}

		public override void OnDoubleClick( Mobile from )
		{
			WoodBag WoodBag = new WoodBag( 5000 );

			if ( !from.AddToBackpack( WoodBag ) )
				WoodBag.Delete();
		}

		public WoodStone( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 1 ); // version
			writer.Write( (int)m_Level );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
			if ( version == 1 )
				m_Level = (SecureLevel)reader.ReadInt();
		}
	}
}