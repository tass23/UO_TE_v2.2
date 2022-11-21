using System;
using Server;
using Server.Network;
using Server.Items;
using Server.Mobiles;
using System.Collections;
using System.Text;
using Server.Targeting;
using Server.Multis;
using Server.Gumps;
namespace Server.Misc
{
	public class YardTarget : Target
	{
		private Mobile m_From;
		private int m_ItemID;
		private int m_Price;
		private int m_Page;
		YardWand m_Wand;
		public YardTarget( YardWand wand, Mobile from, int itemid, int price, int page ) : base( -1, true, TargetFlags.None )
		{
			m_Wand = wand;
			m_From = from;
			m_ItemID = itemid;
			m_Price = price;
			m_Page = page;
			CheckLOS = false;
			m_Wand.page = page;
		}
		protected override void OnTarget( Mobile from, object targeted )
		{
			IPoint3D t = targeted as IPoint3D;
			if( t == null )
				return;
			Point3D loc = new Point3D(t);
			if( t is StaticTarget )
				loc.Z -= TileData.ItemTable[((StaticTarget)t).ItemID & 0x3FFF].CalcHeight;
			if( ValidatePlacement( loc ) )
				EndPlace( loc );
			else
				GumpUp();
		}
        public bool ValidatePlacement(Point3D loc)
        {
            Map map = m_From.Map;
            if (map == null)
                return false;
            // MODS BY LOKAI ****** START
            if (m_From.AccessLevel >= AccessLevel.GameMaster) return true;
            // MODS BY LOKAI ****** END
            BaseHouse house = BaseHouse.FindHouseAt(m_From.Location, map, 20);
            if (house == null || !house.IsOwner(m_From))
            {
                m_From.SendMessage("You must be standing in your house to place this");
                return false;
            }
            // MODS BY LOKAI ****** START
            int rangeFromHouse;
            if (m_From.Region.IsPartOf(typeof(Regions.TownRegion)))
                rangeFromHouse = 2; // Change the dimensions of the yard HERE for Town Homes
            else
                rangeFromHouse = 7; // and HERE for Country Homes
            if (loc.Y > m_From.Location.Y + rangeFromHouse || loc.Y < m_From.Location.Y - rangeFromHouse || loc.X > m_From.Location.X + rangeFromHouse || loc.X < m_From.Location.X - rangeFromHouse) 
            {
                m_From.SendMessage("This is outside of your yard. You have an allowable range of {0} tiles from your house. Please stand near the edge of your house and re-try the placement.", rangeFromHouse.ToString());
                return false;
            }
            // MODS BY LOKAI ****** END
            return true;
        }
        public void EndPlace(Point3D loc)
        {
            bool fromBank = false;
            bool fromRAFTRS = false;
            bool fromBackpack = false;
            if (m_From.Backpack != null)
            {
                Item item = m_From.Backpack.FindItemByType(typeof(RAFTRS));
                RAFTRS raftrs = item as RAFTRS;
                Item[] item2 = m_From.Backpack.FindItemsByType(typeof(Gold));
                int goldint = 0;
                foreach (Item item3 in item2)
                    goldint += item3.Amount;
                if (m_From.Backpack.ConsumeTotal(typeof(Gold), m_Price))
                {
                    if (Core.Debug) m_From.SendMessage(2125, "{0} gold has been withdrawn from your backpack.", m_Price.ToString("#,0"));
                    fromBackpack = true;
                }
                else if (raftrs != null)
                {
                    if (goldint > 0 && (goldint + raftrs.CurAmount) >= m_Price)
                    {
                        m_From.Backpack.ConsumeTotal(typeof(Gold), goldint);
                        if (Core.Debug) m_From.SendMessage(2125, "{0} gold was taken from your backpack.", goldint.ToString("#,0"));
                        raftrs.CurAmount -= (m_Price - goldint);
                        if (Core.Debug) m_From.SendMessage(2125, "The balance of {0} gold was withdrawn from your RAFT.", (m_Price - goldint).ToString("#,0"));
                        fromBackpack = true;
                        fromRAFTRS = true;
                    }
                    else if (raftrs.CurAmount >= m_Price)
                    {
                        raftrs.CurAmount -= m_Price;
                        if (Core.Debug) m_From.SendMessage(2125, "{0} gold has been withdrawn from your RAFT.", m_Price.ToString("#,0"));
                        fromRAFTRS = true;
                    }
                    else
                    {
                        if (Banker.Withdraw(m_From, m_Price))
                        {
                            m_From.SendLocalizedMessage(1060398, m_Price.ToString()); // ~1_AMOUNT~ gold has been withdrawn from your bank box.
                            fromBank = true;
                        }
                    }
                }
            }
            if (fromBackpack || fromBank || fromRAFTRS)
            {
                switch (m_ItemID)
                {
                    case 2084: { new YardIronGate(m_From, m_Price, Server.Items.DoorFacing.WestCCW, loc); break; }
                    case 2085: { new YardIronGate(m_From, m_Price, Server.Items.DoorFacing.NorthCW, loc); break; }
                    case 2086: { new YardIronGate(m_From, m_Price, Server.Items.DoorFacing.EastCW, loc); break; }
                    case 2087: { new YardIronGate(m_From, m_Price, Server.Items.DoorFacing.NorthCW, loc); break; }
                    case 2088: { new YardIronGate(m_From, m_Price, Server.Items.DoorFacing.WestCW, loc); break; }
                    case 2090: { new YardIronGate(m_From, m_Price, Server.Items.DoorFacing.EastCCW, loc); break; }
                    case 2124: { new YardShortIronGate(m_From, m_Price, Server.Items.DoorFacing.WestCCW, loc); break; }
                    case 2125: { new YardShortIronGate(m_From, m_Price, Server.Items.DoorFacing.NorthCCW, loc); break; }
                    case 2126: { new YardShortIronGate(m_From, m_Price, Server.Items.DoorFacing.EastCW, loc); break; }
                    case 2127: { new YardShortIronGate(m_From, m_Price, Server.Items.DoorFacing.NorthCW, loc); break; }
                    case 2128: { new YardShortIronGate(m_From, m_Price, Server.Items.DoorFacing.WestCW, loc); break; }
                    case 2130: { new YardShortIronGate(m_From, m_Price, Server.Items.DoorFacing.EastCCW, loc); break; }
                    case 2105: { new YardLightWoodGate(m_From, m_Price, Server.Items.DoorFacing.WestCCW, loc); break; }
                    case 2106: { new YardLightWoodGate(m_From, m_Price, Server.Items.DoorFacing.NorthCCW, loc); break; }
                    case 2107: { new YardLightWoodGate(m_From, m_Price, Server.Items.DoorFacing.EastCW, loc); break; }
                    case 2108: { new YardLightWoodGate(m_From, m_Price, Server.Items.DoorFacing.NorthCW, loc); break; }                    case 2109: { new YardLightWoodGate(m_From, m_Price, Server.Items.DoorFacing.WestCW, loc); break; }
                    case 2111: { new YardLightWoodGate(m_From, m_Price, Server.Items.DoorFacing.EastCCW, loc); break; }
                    case 2150: { new YardDarkWoodGate(m_From, m_Price, Server.Items.DoorFacing.WestCCW, loc); break; }
                    case 2151: { new YardDarkWoodGate(m_From, m_Price, Server.Items.DoorFacing.NorthCCW, loc); break; }
                    case 2152: { new YardDarkWoodGate(m_From, m_Price, Server.Items.DoorFacing.EastCW, loc); break; }
                    case 2153: { new YardDarkWoodGate(m_From, m_Price, Server.Items.DoorFacing.NorthCW, loc); break; }
                    case 2154: { new YardDarkWoodGate(m_From, m_Price, Server.Items.DoorFacing.WestCW, loc); break; }
                    case 2156: { new YardDarkWoodGate(m_From, m_Price, Server.Items.DoorFacing.EastCCW, loc); break; }					
                    case 5952: { new YardFountain(m_From, m_Price, TypeOfFountain.Stone, loc); break; }
                    case 6610: { new YardFountain(m_From, m_Price, TypeOfFountain.Sand, loc); break; }
                    case 3395: { new YardTreeMulti(m_From, "Tree", m_Price, m_ItemID, 2, 1, loc); break; }
                    case 3401: { new YardTreeMulti(m_From, "Leaves", m_Price, m_ItemID, 4, 3, loc); break; }
                    case 3408: { new YardTreeMulti(m_From, "Leaves", m_Price, m_ItemID, 3, 3, loc); break; }
                    case 3417: { new YardTreeMulti(m_From, "Tree", m_Price, m_ItemID, 2, 2, loc); break; }
                    case 3423: { new YardTreeMulti(m_From, "Leaves", m_Price, m_ItemID, 3, 3, loc); break; }
                    case 3430: { new YardTreeMulti(m_From, "Leaves", m_Price, m_ItemID, 3, 3, loc); break; }
                    case 3440: { new YardTreeMulti(m_From, "Tree", m_Price, m_ItemID, 2, 2, loc); break; }
                    case 3446: { new YardTreeMulti(m_From, "Leaves", m_Price, m_ItemID, 2, 2, loc); break; }
                    case 3453: { new YardTreeMulti(m_From, "Leaves", m_Price, m_ItemID, 3, 2, loc); break; }
                    case 3461: { new YardTreeMulti(m_From, "Tree", m_Price, m_ItemID, 1, 1, loc); break; }
                    case 3465: { new YardTreeMulti(m_From, "Leaves", m_Price, m_ItemID, 2, 2, loc); break; }
                    case 3470: { new YardTreeMulti(m_From, "Leaves", m_Price, m_ItemID, 2, 2, loc); break; }
                    case 4793: { new YardTreeMulti(m_From, "Yew Tree", m_Price, m_ItemID, 3, 4, loc); break; }
                    case 7802: { new YardTreeMulti(m_From, "Yew Leaves", m_Price, m_ItemID, 4, 5, loc); break; }
                    case 3413: { new YardTreeMulti(m_From, "Vines", m_Price, m_ItemID, 1, 1, loc); break; }
                    case 3436: { new YardTreeMulti(m_From, "Vines", m_Price, m_ItemID, -2, -1, loc); break; }
                    case 3457: { new YardTreeMulti(m_From, "Vines", m_Price, m_ItemID, 1, 2, loc); break; }
                    case 3474: { new YardTreeMulti(m_From, "Vines", m_Price, m_ItemID, 1, 1, loc); break; }
                    //Stairs
                    case 1006: { new YardStair(m_From, 1006, 1006, 1006, 1006, loc, m_Price); break; }
                    case 1007: { new YardStair(m_From, 1007, 1008, 1009, 1010, loc, m_Price); break; }
                    case 1011: { new YardStair(m_From, 1011, 1012, 1013, 1014, loc, m_Price); break; }
                    case 1015: { new YardStair(m_From, 1015, 1016, 1017, 1018, loc, m_Price); break; }
                    case 1019: { new YardStair(m_From, 1019, 1020, 1021, 1022, loc, m_Price); break; }
                    case 1023: { new YardStair(m_From, 1023, 1024, 1025, 1026, loc, m_Price); break; }
                    case 1801: { new YardStair(m_From, 1801, 1801, 1801, 1801, loc, m_Price); break; }
                    case 1802: { new YardStair(m_From, 1802, 1803, 1804, 1805, loc, m_Price); break; }
                    case 1806: { new YardStair(m_From, 1806, 1807, 1808, 1809, loc, m_Price); break; }
                    case 1810: { new YardStair(m_From, 1810, 1811, 1812, 1813, loc, m_Price); break; }
                    case 1814: { new YardStair(m_From, 1814, 1815, 1816, 1817, loc, m_Price); break; }
                    case 1818: { new YardStair(m_From, 1818, 1819, 1820, 1821, loc, m_Price); break; }
                    case 1822: { new YardStair(m_From, 1822, 1822, 1822, 1822, loc, m_Price); break; }
                    case 1823: { new YardStair(m_From, 1823, 1846, 1847, 1865, loc, m_Price); break; }
                    case 1866: { new YardStair(m_From, 1866, 1867, 1868, 1869, loc, m_Price); break; }
                    case 1870: { new YardStair(m_From, 1870, 1871, 1922, 1923, loc, m_Price); break; }
                    case 1952: { new YardStair(m_From, 1952, 1953, 1954, 2010, loc, m_Price); break; }
                    case 2015: { new YardStair(m_From, 2015, 2016, 2100, 2166, loc, m_Price); break; }
                    case 1825: { new YardStair(m_From, 1825, 1825, 1825, 1825, loc, m_Price); break; }
                    case 1826: { new YardStair(m_From, 1826, 1827, 1828, 1829, loc, m_Price); break; }
                    case 1830: { new YardStair(m_From, 1830, 1831, 1832, 1833, loc, m_Price); break; }
                    case 1834: { new YardStair(m_From, 1834, 1835, 1836, 1837, loc, m_Price); break; }
                    case 1838: { new YardStair(m_From, 1838, 1839, 1840, 1841, loc, m_Price); break; }
                    case 1842: { new YardStair(m_From, 1842, 1843, 1844, 1845, loc, m_Price); break; }
                    case 1848: { new YardStair(m_From, 1848, 1848, 1848, 1848, loc, m_Price); break; }
                    case 1849: { new YardStair(m_From, 1849, 1850, 1851, 1852, loc, m_Price); break; }
                    case 1853: { new YardStair(m_From, 1853, 1854, 1855, 1856, loc, m_Price); break; }
                    case 1861: { new YardStair(m_From, 1861, 1862, 1863, 1864, loc, m_Price); break; }
                    case 1857: { new YardStair(m_From, 1857, 1858, 1859, 1860, loc, m_Price); break; }
                    case 2170: { new YardStair(m_From, 2170, 2171, 2172, 2173, loc, m_Price); break; }
                    case 1872: { new YardStair(m_From, 1872, 1872, 1872, 1872, loc, m_Price); break; }
                    case 1873: { new YardStair(m_From, 1873, 1874, 1875, 1876, loc, m_Price); break; }
                    case 1877: { new YardStair(m_From, 1877, 1878, 1879, 1880, loc, m_Price); break; }
                    case 1881: { new YardStair(m_From, 1881, 1882, 1883, 1884, loc, m_Price); break; }
                    case 1885: { new YardStair(m_From, 1885, 1886, 1887, 1888, loc, m_Price); break; }
                    case 1889: { new YardStair(m_From, 1889, 1890, 1891, 1892, loc, m_Price); break; }
                    case 1900: { new YardStair(m_From, 1900, 1900, 1900, 1900, loc, m_Price); break; }
                    case 1901: { new YardStair(m_From, 1901, 1902, 1903, 1904, loc, m_Price); break; }
                    case 1905: { new YardStair(m_From, 1905, 1906, 1907, 1908, loc, m_Price); break; }
                    case 1909: { new YardStair(m_From, 1909, 1910, 1911, 1912, loc, m_Price); break; }
                    case 1913: { new YardStair(m_From, 1913, 1914, 1915, 1916, loc, m_Price); break; }
                    case 1917: { new YardStair(m_From, 1917, 1918, 1919, 1920, loc, m_Price); break; }
                    case 1928: { new YardStair(m_From, 1928, 1928, 1928, 1928, loc, m_Price); break; }
                    case 1929: { new YardStair(m_From, 1929, 1930, 1931, 1932, loc, m_Price); break; }
                    case 1933: { new YardStair(m_From, 1933, 1934, 1935, 1936, loc, m_Price); break; }
                    case 1937: { new YardStair(m_From, 1937, 1938, 1939, 1940, loc, m_Price); break; }
                    case 1941: { new YardStair(m_From, 1941, 1942, 1943, 1944, loc, m_Price); break; }
                    case 1945: { new YardStair(m_From, 1945, 1946, 1947, 1948, loc, m_Price); break; }
                    case 1955: { new YardStair(m_From, 1955, 1955, 1955, 1955, loc, m_Price); break; }
                    case 1956: { new YardStair(m_From, 1956, 1957, 1958, 1959, loc, m_Price); break; }
                    case 1960: { new YardStair(m_From, 1960, 1961, 1962, 1963, loc, m_Price); break; }
                    case 1964: { new YardStair(m_From, 1964, 1965, 1966, 1967, loc, m_Price); break; }
                    case 1978: { new YardStair(m_From, 1978, 1978, 1978, 1978, loc, m_Price); break; }
                    case 1979: { new YardStair(m_From, 1979, 1980, 1979, 1980, loc, m_Price); break; }
                    case 1991: { new YardStair(m_From, 1991, 1992, 1991, 1992, loc, m_Price); break; }
                    case 1981: { new YardStair(m_From, 1981, 1982, 1981, 1982, loc, m_Price); break; }
                    case 1983: { new YardStair(m_From, 1983, 1984, 1985, 1986, loc, m_Price); break; }
                    case 1987: { new YardStair(m_From, 1987, 1988, 1989, 1990, loc, m_Price); break; }
                    case 1993: { new YardStair(m_From, 1993, 1994, 1995, 1996, loc, m_Price); break; }
                    case 1997: { new YardStair(m_From, 1997, 1998, 1999, 2000, loc, m_Price); break; }
                    case 1173: { new YardStair(m_From, 1173, 1179, 1180, 1181, loc, m_Price); break; }
                    case 1193: { new YardStair(m_From, 1193, 1194, 1205, 1206, loc, m_Price); break; }
                    case 1250: { new YardStair(m_From, 1250, 1276, 1317, 1327, loc, m_Price); break; }
                    case 1289: { new YardStair(m_From, 1289, 1290, 1291, 1292, loc, m_Price); break; }
                    case 1294: { new YardStair(m_From, 1294, 1295, 1297, 1299, loc, m_Price); break; }
                    case 1301: { new YardStair(m_From, 1301, 1374, 1397, 1401, loc, m_Price); break; }
                    case 1035: { new YardStair(m_From, 1035, 1036, 1037, 1038, loc, m_Price); break; }
                    case 1039: { new YardStair(m_From, 1039, 1040, 1041, 1042, loc, m_Price); break; }
                    case 1043: { new YardStair(m_From, 1043, 1044, 1045, 1046, loc, m_Price); break; }
                    case 1047: { new YardStair(m_From, 1047, 1048, 1049, 1051, loc, m_Price); break; }
                    case 1051: { new YardStair(m_From, 1051, 1052, 1053, 1054, loc, m_Price); break; }
                    case 12789: { new YardStair(m_From, 12789, 12793, 12794, 12795, loc, m_Price); break; }
                    default: { new YardItem(m_From, m_ItemID, loc, m_Price); break; }
                }
                GumpUp();
            }
            else
            {
                m_From.SendMessage("You do not have enough gold for that.");
                GumpUp();
            }
        }
		public void GumpUp()
		{
			switch( m_Page )
			{
				case 0:  {m_From.SendGump(new YardGump(m_From,m_Wand));break;}
				case 1:  {m_From.SendGump(new Ground1(m_Wand,m_From, m_ItemID, m_Price));break;}
				case 2:  {m_From.SendGump(new Ground2(m_Wand,m_From, m_ItemID, m_Price));break;}
				case 3:  {m_From.SendGump(new Ground3(m_Wand,m_From, m_ItemID, m_Price));break;}
				case 4:  {m_From.SendGump(new GroundBase(m_Wand,m_From, m_ItemID, m_Price));break;}
				case 5:  {m_From.SendGump(new Lava1(m_Wand,m_From, m_ItemID, m_Price));break;}
				case 6:  {m_From.SendGump(new Lava2(m_Wand,m_From, m_ItemID, m_Price));break;}
				case 7:  {m_From.SendGump(new Plants1(m_Wand,m_From, m_ItemID, m_Price));break;}
				case 8:  {m_From.SendGump(new Plants2(m_Wand,m_From, m_ItemID, m_Price));break;}
				case 9:  {m_From.SendGump(new Plants3(m_Wand,m_From, m_ItemID, m_Price));break;}
				case 10: {m_From.SendGump(new Plants4(m_Wand,m_From, m_ItemID, m_Price));break;}
				case 11: {m_From.SendGump(new Plants5(m_Wand,m_From, m_ItemID, m_Price));break;}
				case 12: {m_From.SendGump(new Swamp1(m_Wand,m_From, m_ItemID, m_Price));break;}
				case 13: {m_From.SendGump(new Swamp2(m_Wand,m_From, m_ItemID, m_Price));break;}
				case 14: {m_From.SendGump(new Trees1(m_Wand,m_From, m_ItemID, m_Price));break;}
				case 15: {m_From.SendGump(new Trees2(m_Wand,m_From, m_ItemID, m_Price));break;}
				case 16: {m_From.SendGump(new Trees3(m_Wand,m_From, m_ItemID, m_Price));break;}
				case 17: {m_From.SendGump(new Trees4(m_Wand,m_From, m_ItemID, m_Price));break;}
				case 18: {m_From.SendGump(new Water1(m_Wand,m_From, m_ItemID, m_Price));break;}
				case 19: {m_From.SendGump(new Water2(m_Wand,m_From, m_ItemID, m_Price));break;}
				case 20: {m_From.SendGump(new Stairs1(m_Wand,m_From, m_ItemID, m_Price));break;}
				case 21: {m_From.SendGump(new Stairs2(m_Wand,m_From, m_ItemID, m_Price));break;}
				case 22: {m_From.SendGump(new Stairs3(m_Wand,m_From, m_ItemID, m_Price));break;}
			}
		}
	}
}