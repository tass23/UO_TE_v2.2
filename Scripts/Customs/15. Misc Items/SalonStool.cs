using System;
using System.IO;
using System.Text;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;

using Server;
using Server.Mobiles;
using Server.Gumps;
using Server.Network;

namespace Engale.Hair
{
	public class HairStylesStool : Item
	{
		public List<InternalHair> m_hair;
		public List<InternalHair> m_face;
		
		[Constructable]
		public HairStylesStool() : base( 0xA2A )
		{
			m_hair = new List<InternalHair>();
			m_face = new List<InternalHair>();
			
			HairList();
			FacialHairList();
		}
		
		public HairStylesStool( Serial serial ) : base( serial )
		{}
		
		public void HairList()
		{
			bool Male = false;
			bool Female = true;
			//List of hues for hair color baised on races, you can create a new list for any form of category you like, these lists are used as hues for different hair styles.
			//List<int> HumanHairHues = new List<int>(new int[] { 141, 147, 237 } ); //<-- Use for individual hues
			//List<int> ElfHairHues =   new List<int>(new int[] {   5,  69, 319 } ); //<-- Use for individual hues
			List<int> None = new List<int>(new int[] { 0 } );

			List<int> HumanHairHues = new List<int>(new int[] {} );
			List<int> ElfHairHues =   new List<int>(new int[] {} );

			HumanHairHues = AddHues(HumanHairHues, 1102, 1149);
			HumanHairHues = AddHues(ElfHairHues, 1102, 1149);
			
			//Adding a Hair to the list is this easy
			//                            ItemID, 	GumpID, Race, 		       Gender, Name, 		 HueList,       Price
			m_hair.Add( new InternalHair( 0x0,      0x0,    Race.Human,        Male,   "Bald",         None,          10) );
			m_hair.Add( new InternalHair( 0x0,      0x0,    Race.Human,        Female, "Bald",         None,          10) );
			
			m_hair.Add( new InternalHair( 0x203B,   0xC60C, Race.Human,        Male,   "Short",        HumanHairHues, 80) );
			m_hair.Add( new InternalHair( 0x203C,   0xc60d, Race.Human,        Male,   "Long",         HumanHairHues, 80) );
			m_hair.Add( new InternalHair( 0x203D,   0xc60e, Race.Human,        Male,   "Ponytail",     HumanHairHues, 80) );
			m_hair.Add( new InternalHair( 0x2044,   0xC60F, Race.Human,        Male,   "Mohawk",       HumanHairHues, 80) );
			m_hair.Add( new InternalHair( 0x2045,   0xED26, Race.Human,        Male,   "Pageboy",      HumanHairHues, 80) );
			m_hair.Add( new InternalHair( 0x204A,   0xED29, Race.Human,        Male,   "Topknot",      HumanHairHues, 80) );
			m_hair.Add( new InternalHair( 0x2047,   0xC6D4, Race.Human,        Male,   "Curly",        HumanHairHues, 80) );
			m_hair.Add( new InternalHair( 0x2048,   0xEDE5, Race.Human,        Male,   "Receding",     HumanHairHues, 80) );
			m_hair.Add( new InternalHair( 0x2049,   0xC6D6, Race.Human,        Male,   "2 Tails",      HumanHairHues, 80) );
			
			m_hair.Add( new InternalHair( 0x203B,   0xed1c, Race.Human,        Female, "Short",        HumanHairHues, 80) );
			m_hair.Add( new InternalHair( 0x203C,   0xed1d, Race.Human,        Female, "Long",         HumanHairHues, 80) );
			m_hair.Add( new InternalHair( 0x203D,   0xed1e, Race.Human,        Female, "Pony Tail",    HumanHairHues, 80) );
			m_hair.Add( new InternalHair( 0x2044,   0xed27, Race.Human,        Female, "Mohawk",       HumanHairHues, 80) );
			m_hair.Add( new InternalHair( 0x2045,   0xED26, Race.Human,        Female, "Pageboy",      HumanHairHues, 80) );
			m_hair.Add( new InternalHair( 0x204A,   0xED29, Race.Human,        Female, "Topknot",      HumanHairHues, 80) );
			m_hair.Add( new InternalHair( 0x2047,   0xed25, Race.Human,        Female, "Curly",        HumanHairHues, 80) );
			m_hair.Add( new InternalHair( 0x2049,   0xede6, Race.Human,        Female, "2 Tails",      HumanHairHues, 80) );
			m_hair.Add( new InternalHair( 0x2046,   0xed28, Race.Human,        Female, "Buns",         HumanHairHues, 80) );
			
			m_hair.Add( new InternalHair( 0x0,      0x0,    Race.Elf,          Male,   "Bald",         None,          10) );
			m_hair.Add( new InternalHair( 0x0,      0x0,    Race.Elf,          Female, "Bald",         None,          10) );
			
			m_hair.Add( new InternalHair( 0x2fbf,   0xc6e4, Race.Elf,          Male,   "Mid Long",     ElfHairHues,   80) );
			m_hair.Add( new InternalHair( 0x2fc0,   0xc6e5, Race.Elf,          Male,   "Long Feather", ElfHairHues,   80) );
			m_hair.Add( new InternalHair( 0x2fc1,   0xc6e6, Race.Elf,          Male,   "Short",        ElfHairHues,   80) );
			m_hair.Add( new InternalHair( 0x2fc2,   0xc6e7, Race.Elf,          Male,   "Mullet",       ElfHairHues,   80) );
			m_hair.Add( new InternalHair( 0x2fcd,   0xc6cb, Race.Elf,          Male,   "Long",         ElfHairHues,   80) );
			m_hair.Add( new InternalHair( 0x2fce,   0xc6cc, Race.Elf,          Male,   "Topknot",      ElfHairHues,   80) );
			m_hair.Add( new InternalHair( 0x2fcf,   0xc6cd, Race.Elf,          Male,   "Long Braid",   ElfHairHues,   80) );
			m_hair.Add( new InternalHair( 0x2fd1,   0xc6cf, Race.Elf,          Male,   "Spiked",       ElfHairHues,   80) );
			
			m_hair.Add( new InternalHair( 0x2fc0,   0xedf5, Race.Elf,          Female, "Long Feather", ElfHairHues,   80) );
			m_hair.Add( new InternalHair( 0x2fc1,   0xedf6, Race.Elf,          Female, "Short",        ElfHairHues,   80) );
			m_hair.Add( new InternalHair( 0x2fc2,   0xedf7, Race.Elf,          Female, "Mullet",       ElfHairHues,   80) );
			m_hair.Add( new InternalHair( 0x2fcc,   0xedda, Race.Elf,          Female, "Flower",       ElfHairHues,   80) );
			m_hair.Add( new InternalHair( 0x2fce,   0xeddc, Race.Elf,          Female, "Topknot",      ElfHairHues,   80) );
			m_hair.Add( new InternalHair( 0x2fcf,   0xeddd, Race.Elf,          Female, "Long Braid",   ElfHairHues,   80) );
			m_hair.Add( new InternalHair( 0x2fd0,   0xedde, Race.Elf,          Female, "Buns",         ElfHairHues,   80) );
			m_hair.Add( new InternalHair( 0x2fd1,   0xeddf, Race.Elf,          Female, "Spiked",       ElfHairHues,   80) );
			
			//if you have the Gargoyle Race uncomment these lines:
			m_hair.Add( new InternalHair( 0x0,      0x0,    Race.Gargoyle,   Female, "Bald",         None,          10) );
			m_hair.Add( new InternalHair( 0x0,      0x0,    Race.Gargoyle,   Female, "Bald",         None,          10) );
		}
		
		public void FacialHairList()
		{
			bool Male = false;
			bool Female = true;
			//List of hues for hair color baised on races, you can create a new list for any form of category you like, these lists are used as hues for different hair styles.
			//List<int> HumanHairHues = new List<int>(new int[] { 141, 147, 237 } ); //<-- Use for individual hues
			//List<int> ElfHairHues =   new List<int>(new int[] {   5,  69, 319 } ); //<-- Use for individual hues

			List<int> HumanHairHues = new List<int>(new int[] {} );
			List<int> ElfHairHues =   new List<int>(new int[] {} );
			List<int> None = new List<int>(new int[] { 0 } );

			HumanHairHues = AddHues(HumanHairHues, 1102, 1149);
			HumanHairHues = AddHues(ElfHairHues, 1102, 1149);
			
			//Adding a Hair to the list is this easy
			//                            ItemID, GumpID, Race,            Gender, Name,                    HueList,        Price
			m_face.Add( new InternalHair( 0x0,    0x0,    Race.Human,      Male,   "None",                  None,           10) );
			m_face.Add( new InternalHair( 0x0,    0x0,    Race.Human,      Female, "None",                  None,           10) );
			
			m_face.Add( new InternalHair( 0x2040, 0xC670, Race.Human,      Male,   "Goatee",                HumanHairHues, 	80) );
			m_face.Add( new InternalHair( 0x203E, 0xC671, Race.Human,      Male,   "Long Beard",            HumanHairHues, 	80) );
			m_face.Add( new InternalHair( 0x203F, 0xC672, Race.Human,      Male,   "Short Beard",           HumanHairHues, 	80) );
			m_face.Add( new InternalHair( 0x2041, 0xC673, Race.Human,      Male,   "Mustache",              HumanHairHues, 	80) );
			m_face.Add( new InternalHair( 0x204B, 0xC676, Race.Human,      Male,   "Short Beard, Mustache", HumanHairHues, 	80) );
			m_face.Add( new InternalHair( 0x204C, 0xC675, Race.Human,      Male,   "Long Beard, Mustache",  HumanHairHues, 	80) );
			m_face.Add( new InternalHair( 0x204D, 0xC674, Race.Human,      Male,   "Vandyke",               HumanHairHues, 	80) );
			
			m_face.Add( new InternalHair( 0x0,    0x0,    Race.Elf,        Male,   "None",                  None,           10) );
			m_face.Add( new InternalHair( 0x0,    0x0,    Race.Elf,        Female, "None",                  None,           10) );
			
			//if you have the Gargoyle Race uncomment these lines:
			m_hair.Add( new InternalHair( 0x0,    0x0,    Race.Gargoyle, Male,   "None",                  None,           10) );
			m_hair.Add( new InternalHair( 0x0,    0x0,    Race.Gargoyle, Female, "None",                  None,           10) );
		}
		
		public override bool OnMoveOver( Mobile m )
		{
			if ( m is PlayerMobile )
			{
				m.SendGump( new InternalGump(m, m_hair, m_face, -2, -2, -2, -2) ); // -2 = find the current if we can or use 0, -2 as -1 will happen via the button push.
				return true;
			}
			
			return false;
		}

//Code for HUE Range		
		private List<int> AddHues(List<int> l, int start, int end)
		{
			for(int i = start; i <= end; i++)
			{
				l.Add(i);
			}
			return l;
		}
//Code for HUE Range

		public override bool HandlesOnMovement{ get{ return true; } }

		public override void OnMovement( Mobile m, Point3D oldLocation )
		{
			if ( m is PlayerMobile )
			{
				if ( !Utility.InRange( m.Location, this.Location, 0 ) && Utility.InRange( oldLocation, this.Location, 0 ) )
					m.CloseGump( typeof( InternalGump ) );
			}
		}
		
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
			
			writer.Write(m_hair.Count);
			foreach(InternalHair h in m_hair)
			{
				h.Serialize( writer );
			}
			
			writer.Write(m_face.Count);
			foreach(InternalHair f in m_face)
			{
				f.Serialize( writer );
			}
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
			
			m_hair = new List<InternalHair>();
			m_face = new List<InternalHair>();
			
			int cnt = reader.ReadInt();
			for( int i = 0; i < cnt; i++ )
			{
				m_hair.Add( new InternalHair( reader ) );
			}
			
			cnt = reader.ReadInt();
			for( int i = 0; i < cnt; i++ )
			{
				m_face.Add( new InternalHair( reader ) );
			}
		}
		
		public class InternalHair
		{
			public Race m_Race;
			public bool m_Female;
			public int m_HairItemID;
			public int m_HairGumpID;
			public string m_HairStyleName;
			public int m_Price;
			public List<int> m_HairHue;
			
			public InternalHair(int iid, int gid, Race r, bool f, string name, List<int> Hue, int price)
			{
				m_HairItemID = iid;
				m_HairGumpID = gid;
				m_Race = r;
				m_Female = f;
				m_HairStyleName = name;
				m_Price = price;
				m_HairHue = Hue;
			}
			
			public void Serialize( GenericWriter writer )
			{
				writer.Write((int)0);
				writer.Write(m_HairItemID);
				writer.Write(m_HairGumpID);
				writer.Write(m_Race);
				writer.Write(m_Female);
				writer.Write(m_HairStyleName);
				writer.Write(m_Price);
				writer.Write(m_HairHue.Count);
				foreach(int hue in m_HairHue)
				{
					writer.Write(hue);
				}
			}
			public InternalHair( GenericReader reader )
			{
				int version = reader.ReadInt();
				m_HairItemID = reader.ReadInt();
				m_HairGumpID = reader.ReadInt();
				m_Race = reader.ReadRace();
				m_Female = reader.ReadBool();
				m_HairStyleName = reader.ReadString();
				m_Price = reader.ReadInt();
				
				m_HairHue = new List<int>();
				
				int cnt = reader.ReadInt();
				for(int i = 0; i < cnt; i++)
				{
					m_HairHue.Add(reader.ReadInt());
				}
			}
		}
		
		public class InternalGump : Gump
		{
			private List<InternalHair> m_h;
			private List<InternalHair> m_f;
			
			private int hi_current;
			private int hc_current;
			
			private int fi_current;
			private int fc_current;
			
			public InternalGump(Mobile from, List<InternalHair> h, List<InternalHair> f, int hi, int hc, int fi, int fc) : base( 0, 0 )
			{
				if(hi == -2) {
					m_h = validHair(from, h);
					int[] h_f = charvalue(from.HairItemID, from.HairHue, m_h);
				}
				
				if(fi == -2) {
					m_f = validHair(from, f);
					int[] f_f = charvalue(from.FacialHairItemID, from.FacialHairHue, m_f);
				}
				
				if(m_h == null) {
					m_h = h;
				}
				
				if(m_f == null) {
					m_f = f;
				}
				
				if(hi == -2) hi = 0;
				if(fi == -2) fi = 0;
				
				if(hc == -2) hc = 0;
				if(fc == -2) fc = 0;
				
				if(hi == 0 && hi_current != null) hi = hi_current;
				if(fi == 0 && fi_current != null) fi = fi_current;
				
				if(hc == 0 && hc_current != null) hc = hc_current;
				if(fc == 0 && fc_current != null) fc = fc_current;
				
				if(hi < 0) hi = m_h.Count - 1;
				if(fi < 0) fi = m_f.Count - 1;
				
				if(hi >= m_h.Count) hi = 0;
				if(fi >= m_f.Count) fi = 0;
				
				if(hc < 0) hc = m_h[hi].m_HairHue.Count - 1;
				if(fc < 0) fc = m_f[fi].m_HairHue.Count - 1;
				
				if(hc >= m_h[hi].m_HairHue.Count) hc = 0;
				if(fc >= m_f[fi].m_HairHue.Count) fc = 0;
				
				hi_current = hi;
				fi_current = fi;
				
				hc_current = hc;
				fc_current = fc;
				
				int price = 0;
				
				if(from.HairItemID != m_h[hi].m_HairItemID) price += m_h[hi].m_Price;
				if(from.FacialHairItemID != m_f[fi].m_HairItemID) price += m_f[fi].m_Price;
				
				if(from.HairHue != m_h[hi].m_HairHue[hc]) price += m_h[hi].m_Price;
				if(from.FacialHairHue != m_f[fi].m_HairHue[fc]) price += m_f[fi].m_Price;
				
				int bodyid = 12;
				
				if(from.Race == Race.Human && from.Female == true) bodyid = 13;
				if(from.Race == Race.Elf && from.Female == false) bodyid = 14;
				if(from.Race == Race.Elf && from.Female == true) bodyid = 15;
				
				this.Closable = true;
	
				AddPage(0);
				
				AddBackground(0, 0, 330, 460, 5054);
				AddImage(20, 20, 1800, 0);
				AddImageTiled(20, 271, 260, 111, 5058);
				AddImageTiled(240, 213, 77, 111, 5058);
				AddImageTiled(36, 270, 209, 7, 2621);
				
				//display images
				AddImage(52, 48, bodyid, 0); //body
				if(m_h[hi].m_HairGumpID != 0)
				AddImage(52, 47, m_h[hi].m_HairGumpID, m_h[hi].m_HairHue[hc] - 1); //hair
				if(m_f[fi].m_HairGumpID != 0)
				AddImage(52, 50, m_f[fi].m_HairGumpID, m_f[fi].m_HairHue[fc] - 1); //Facial hair
				
				AddLabel(20, 280, 910, "Hair Style:");
				AddLabel(19, 350, 910, "Facial Hair");
				AddLabel(200, 420, 910, "Price:");
				
				//variable lables will be changed basied on incoming information.
				AddLabel(135, 280, 910, m_h[hi].m_HairStyleName);
				AddLabel(135, 315, m_h[hi].m_HairHue[hc] - 1, "Hair Color");
				
				AddLabel(135, 350, 910, m_f[fi].m_HairStyleName);
				AddLabel(135, 385, m_f[fi].m_HairHue[fc] - 1, "Hair Color");
				
				AddLabel(250, 420, 53, price.ToString());
				
				//Buttons
				AddButton(245, 90, 241, 242, 0, GumpButtonType.Reply, 0);//cancel
				
				AddButton(95, 280, 4014, 4016, 1, GumpButtonType.Reply, 0);//style back
				AddButton(210, 280, 4005, 4007, 2, GumpButtonType.Reply, 0);//style forward
				
				AddButton(95, 315, 4014, 4016, 3, GumpButtonType.Reply, 0);//hair color back
				AddButton(210, 315, 4005, 4007, 4, GumpButtonType.Reply, 0);//fair color forward
				
				AddButton(95, 350, 4014, 4016, 5, GumpButtonType.Reply, 0);//Face style back
				AddButton(210, 350, 4005, 4007, 6, GumpButtonType.Reply, 0);//face style forward
				
				AddButton(95, 385, 4014, 4016, 7, GumpButtonType.Reply, 0);//face color back
				AddButton(210, 385, 4005, 4007, 8, GumpButtonType.Reply, 0);//face color forward
				
				AddButton(245, 60, 247, 248, 9, GumpButtonType.Reply, 0);//okay
			}
			
			private int[] charvalue(int id, int hue, List<InternalHair> m_h)
			{
				int[] ret = new int[2] {-2, -2};
				foreach(InternalHair t_h in m_h)
				{
					if(id == t_h.m_HairItemID)
					{
						ret[0] = m_h.IndexOf(t_h);
						
						//Style was found can we get the hue for it?
						foreach( int hb in t_h.m_HairHue)
						{
							if(hue == hb)
							{
								ret[1] = t_h.m_HairHue.IndexOf(hb);
							}
						}
					}
				}
				return ret;
			}
			
			private List<InternalHair> validHair(Mobile from, List<InternalHair> h)
			{
				List<InternalHair> t_h = new List<InternalHair>();
				
				foreach(InternalHair vh in h)
				{
					if( vh.m_Race == from.Race && vh.m_Female == from.Female ) {
						t_h.Add(vh);
					}
				}
				
				return t_h;
			}
			
			public override void OnResponse(NetState sender, RelayInfo info)
			{
				Mobile from = sender.Mobile;
				switch(info.ButtonID)
				{
					case 0:
					{
						break;
					}
					case 1:
					{
						//style back
						from.SendGump( new InternalGump(from, m_h, m_f, hi_current - 1, 0, fi_current, fc_current) );
						break;
					}
					case 2:
					{
						//style forward
						from.SendGump( new InternalGump(from, m_h, m_f, hi_current + 1, 0, fi_current, fc_current) );
						break;
					}
					case 3:
					{
						//Hair Color back
						from.SendGump( new InternalGump(from, m_h, m_f, hi_current, hc_current - 1, fi_current, fc_current) );
						break;
					}
					case 4:
					{
						//Hair Color forward
						from.SendGump( new InternalGump(from, m_h, m_f, hi_current, hc_current + 1, fi_current, fc_current) );
						break;
					}
					case 5:
					{
						//Face Style Back
						from.SendGump( new InternalGump(from, m_h, m_f, hi_current, hc_current, fi_current - 1, 0) );
						break;
					}
					case 6:
					{
						//Face Style Forward
						from.SendGump( new InternalGump(from, m_h, m_f, hi_current, hc_current, fi_current + 1, 0) );
						break;
					}
					case 7:
					{
						//Face Color Back
						from.SendGump( new InternalGump(from, m_h, m_f, hi_current, hc_current, fi_current, fc_current - 1) );
						break;
					}
					case 8:
					{
						//Face Color Forward
						from.SendGump( new InternalGump(from, m_h, m_f, hi_current, hc_current, fi_current, fc_current + 1) );
						break;						
					}
					case 9:
					{
						int price = 0;
				
						if(from.HairItemID != m_h[hi_current].m_HairItemID) price += m_h[hi_current].m_Price;
						if(from.FacialHairItemID != m_f[fi_current].m_HairItemID) price += m_f[fi_current].m_Price;
						
						if(from.HairHue != m_h[hi_current].m_HairHue[hc_current]) price += m_h[hi_current].m_Price;
						if(from.FacialHairHue != m_f[fi_current].m_HairHue[fc_current]) price += m_f[fi_current].m_Price;
						
						//okay
						if ( Banker.Withdraw( from, price ) )
						{
							if ( from is PlayerMobile )
							{
								PlayerMobile pm = (PlayerMobile)from;
								pm.SetHairMods( -1, -1 );
							}
							from.HairItemID = m_h[hi_current].m_HairItemID;
							from.HairHue = m_h[hi_current].m_HairHue[hc_current];
							from.FacialHairItemID = m_f[fi_current].m_HairItemID;
							from.FacialHairHue = m_f[fi_current].m_HairHue[fc_current];
							from.SendLocalizedMessage( 1060398, price.ToString() ); // ~1_AMOUNT~ gold has been withdrawn from your bank box.
						}
						else {
							from.SendMessage("You do not have the proper funds in your bankbox.");
						}
						break;
					}
				}
			}
		}
	}
}