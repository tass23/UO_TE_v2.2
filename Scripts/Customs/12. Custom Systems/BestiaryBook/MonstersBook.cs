using System;
using Server;
using System.IO;
using System.Collections;
using Server.Misc;
using Server.Network;
using Server.Mobiles;
using Server.Targeting;
using Server.Targets;
using Server.Gumps;
using Server.Items;
using Server.Prompts;
using Server.Regions;
using Server.ContextMenus;
using System.Reflection;

namespace Server.Items
{
	public class MonstersBook : Item
	{
		private Mobile m_Mobile;
		private object m_Object;

		public object o
		{ 
			get{ return m_Object; } 
			set{ m_Object = value; } 
		}

		private string m_NameID = "Empty Book of Monsters";
		private int m_ItemID = 0;
		private int m_HueID = 0;
		private int m_StrID = 0;
		private int m_DexID = 0;
		private int m_IntID = 0;
		private int m_ARID = 0;
		private int m_HitsID = 0;
		private int m_ManaID = 0;
		private int m_StamID = 0;
		private int m_WGHTID = 0;
		private int m_PhyID = 0;
		private int m_FireID = 0;
		private int m_ColdID = 0;
		private int m_PoisonID = 0;
		private int m_ElecID = 0;

		[CommandProperty( AccessLevel.Administrator )]
		public string NameID
		{
			get { return m_NameID; }
			set { m_NameID = value; }
		}
		[CommandProperty( AccessLevel.Administrator )]
		public int BItemID
		{
			get { return m_ItemID; }
			set { m_ItemID = value; }
		}
		[CommandProperty( AccessLevel.Administrator )]
		public int HueID
		{
			get { return m_HueID; }
			set { m_HueID = value; }
		}
		[CommandProperty( AccessLevel.Administrator )]
		public int StrID
		{
			get { return m_StrID; }
			set { m_StrID = value; }
		}
		[CommandProperty( AccessLevel.Administrator )]
		public int DexID
		{
			get { return m_DexID; }
			set { m_DexID = value; }
		}
		[CommandProperty( AccessLevel.Administrator )]
		public int IntID
		{
			get { return m_IntID; }
			set { m_IntID = value; }
		}
		[CommandProperty( AccessLevel.Administrator )]
		public int ARID
		{
			get { return m_ARID; }
			set { m_ARID = value; }
		}
		[CommandProperty( AccessLevel.Administrator )]
		public int HitsID
		{
			get { return m_HitsID; }
			set { m_HitsID = value; }
		}
		[CommandProperty( AccessLevel.Administrator )]
		public int ManaID
		{
			get { return m_ManaID; }
			set { m_ManaID = value; }
		}
		[CommandProperty( AccessLevel.Administrator )]
		public int StamID
		{
			get { return m_StamID; }
			set { m_StamID = value; }
		}
		[CommandProperty( AccessLevel.Administrator )]
		public int WGHTID
		{
			get { return m_WGHTID; }
			set { m_WGHTID = value; }
		}
		[CommandProperty( AccessLevel.Administrator )]
		public int PhyID
		{
			get { return m_PhyID; }
			set { m_PhyID = value; }
		}
		[CommandProperty( AccessLevel.Administrator )]
		public int FireID
		{
			get { return m_FireID; }
			set { m_FireID = value; }
		}
		[CommandProperty( AccessLevel.Administrator )]
		public int ColdID
		{
			get { return m_ColdID; }
			set { m_ColdID = value; }
		}
		[CommandProperty( AccessLevel.Administrator )]
		public int PoisonID
		{
			get { return m_PoisonID; }
			set { m_PoisonID = value; }
		}
		[CommandProperty( AccessLevel.Administrator )]
		public int ElecID
		{
			get { return m_ElecID; }
			set { m_ElecID = value; }
		}

		[Constructable]
		public MonstersBook( object o ) : base( 0x2252 )
		{
			Mobile m_Object = (Mobile)o;

			Weight = 1.0;
			Hue = 17;
			Name = "Monsters of " + m_Object.Name;

			m_NameID = "" + m_Object.Name;
			m_ItemID = (ShrinkTable.Lookup( m_Object ));
			m_HueID = m_Object.Hue;
			m_StrID = m_Object.RawStr;
			m_DexID = m_Object.RawDex;
			m_IntID = m_Object.RawInt;
			m_ARID = m_Object.VirtualArmor;
			m_HitsID = m_Object.Hits;
			m_ManaID = m_Object.Mana;
			m_StamID = m_Object.Stam;
			m_WGHTID = (m_Object.TotalWeight + m_Object.RawStr);
			m_PhyID = m_Object.PhysicalResistance;
			m_FireID = m_Object.FireResistance;
			m_ColdID = m_Object.ColdResistance;
			m_PoisonID = m_Object.PoisonResistance;
			m_ElecID = m_Object.EnergyResistance;
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( from.InRange( GetWorldLocation(), 2 ) )
			{
				from.CloseGump( typeof( BbookGump ) );
				from.SendGump( new BbookGump( from, this ) );
			}
			else
			{
				from.LocalOverheadMessage( MessageType.Regular, 0x3B2, 1019045 ); // I can't reach that.
			}
		}

		public override void OnSingleClick( Mobile from ) 
		{ 
			this.LabelTo( from, this.Name ); 
		}
 
		public MonstersBook( Serial serial ) : base( serial )
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version

			writer.Write( (string) m_NameID );
			writer.Write( (int) m_ItemID );
			writer.Write( (int) m_HueID );
			writer.Write( (int) m_StrID );
			writer.Write( (int) m_DexID );
			writer.Write( (int) m_IntID );
			writer.Write( (int) m_ARID );
			writer.Write( (int) m_HitsID );
			writer.Write( (int) m_ManaID );
			writer.Write( (int) m_StamID );
			writer.Write( (int) m_WGHTID );
			writer.Write( (int) m_PhyID );
			writer.Write( (int) m_FireID );
			writer.Write( (int) m_ColdID );
			writer.Write( (int) m_PoisonID );
			writer.Write( (int) m_ElecID );
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			switch ( version )
			{
				case 0:
				{
					m_NameID = reader.ReadString();
					m_ItemID = reader.ReadInt();
					m_HueID = reader.ReadInt();
					m_StrID = reader.ReadInt();
					m_DexID = reader.ReadInt();
					m_IntID = reader.ReadInt();
					m_ARID = reader.ReadInt();
					m_HitsID = reader.ReadInt();
					m_ManaID = reader.ReadInt();
					m_StamID = reader.ReadInt();
					m_WGHTID = reader.ReadInt();
					m_PhyID = reader.ReadInt();
					m_FireID = reader.ReadInt();
					m_ColdID = reader.ReadInt();
					m_PoisonID = reader.ReadInt();
					m_ElecID = reader.ReadInt();

					break;
				}
			}
		}
	}
}
namespace Server.Targets
{
 	public class BCaptureTarget : Target
 	{
 		public BCaptureTarget() : base( 12, false, TargetFlags.Harmful )
 		{
 		}
 		protected override void OnTarget( Mobile from, object o )
 		{
 			if ( o is Mobile )
 			{
 				Mobile targ = (Mobile)o;

				if((targ.RawStr + targ.RawInt + targ.HitsMax) > (from.RawStr + from.RawInt + from.HitsMax + from.SkillsTotal))
				{
					from.SendMessage( 0x35, "That Creature is to strong to add to your Monsters!" );
 					return;
				}
				if ( !targ.InRange( from, 8 ))
 				{
 					from.SendMessage( 0x35, "You must be closer to see!" );
 					return;
 				}
 				if (targ is PlayerMobile)
 				{
					from.SendMessage( 0x35, "You can not target another player!" );
 					return;
 				}
				if ( targ is BaseVendor )
 				{
 					from.SendMessage( 0x35, "You can not target a Vendor!" );
 					return;
 				}
 				else if ( targ is BaseCreature )
 				{
 					from.PlaceInBackpack( new MonstersBook(targ) );
					from.SendMessage("Your received a book on " + targ.Name);
 				}
 			}
 			else
 			{
 				from.SendMessage( 0x35, "You can't do that" );
 			}
 		}
 	}
}
namespace Server.Gumps
{
	public class BbookGump : Gump
	{
		public BbookGump( Mobile mobile, object o ) : base( 0, 0 )
		{
			MonstersBook book = o as MonstersBook;

			Closable=true;
			Disposable=true;
			Dragable=true;
			Resizable=false;

			AddPage(0);

			AddImage(197, 76, 11010);
			AddImageTiled(247, 92, 135, 162, 200);
			AddImageTiled(412, 92, 135, 162, 200);
			AddImageTiled(264, 162, 100, 60, 2624);
			AddImage(256, 111, 93);
			AddImage(248, 92, 208);
			AddImage(282, 227, 9804);
			AddImage(448, 227, 9804);
			AddImage(248, 233, 208);
			AddImage(363, 92, 208);
			AddImage(363, 233, 208);
			AddImage(412, 92, 208);
			AddImage(412, 233, 208);
			AddImage(528, 234, 208);
			AddImage(528, 92, 208);
			AddImage(421, 87, 93);
			AddLabel(435, 93, 0, @"Monster Stats");
			AddLabel(257, 116, 33, @"" + book.NameID );
			AddLabel(421, 115, 0, @"STR: " + book.StrID );
			AddLabel(421, 128, 0, @"DEX: " + book.DexID );
			AddLabel(421, 141, 0, @"INT: " + book.IntID );
			AddLabel(421, 167, 0, @"AR: " + book.ARID );
			AddLabel(421, 154, 0, @"HITS: " + book.HitsID );
			AddLabel(421, 193, 0, @"MANA: " + book.ManaID );
			AddLabel(421, 180, 0, @"STAM: " + book.StamID );
			AddLabel(421, 207, 0, @"WGHT: " + book.WGHTID );
			AddLabel(518, 116, 0, @"" + book.PhyID );
			AddLabel(518, 130, 0, @"" + book.FireID );
			AddLabel(517, 144, 0, @"" + book.ColdID );
			AddLabel(517, 158, 0, @"" + book.PoisonID );
			AddLabel(517, 172, 0, @"" + book.ElecID );
			AddImage(503, 121, 2360, 50);
			AddImage(503, 135, 2360);
			AddImage(503, 149, 2362);
			AddImage(503, 163, 2361);
			AddImage(503, 176, 2360, 17);
			AddItem(299, 165, book.BItemID, book.HueID );
		}	
	}
}