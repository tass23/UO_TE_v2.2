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
	public class ScribeStone : Item, ISecurable
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
			get { return "a Scribe Supply Stone"; }
		}

		[Constructable]
		public ScribeStone() : base( 0xED4 )
		{
			Movable = false;
			Hue = 0x105;
		}

		public override void OnDoubleClick( Mobile from )
		{
			ScribeBag scribeBag = new ScribeBag();

			if ( !from.AddToBackpack( scribeBag ) )
				scribeBag.Delete();
		}

		public ScribeStone( Serial serial ) : base( serial )
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