using System;
using System.Collections.Generic;
using Server;

namespace Server.Items
{
    public class AlchemyInfo
    {
        private int m_Second;
        public int Second
        {
            get { return m_Second; }
            set { m_Second = value; }
        }

        private int m_Temperature;
        public int Temperature
        {
            get { return m_Temperature; }
            set { m_Temperature = value; }
        }

        private List<Item> m_ItemsInPot = new List<Item>();
        public List<Item> ItemsInPot
        {
            get { return m_ItemsInPot; }
            set { m_ItemsInPot = value; }
        }

        public AlchemyInfo(int second, int temperature, List<Item> items)
        {
            m_Second = second;
            m_Temperature = temperature;
            m_ItemsInPot = items;
        }
    }
}