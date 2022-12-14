using System;
using System.Collections;
using System.Collections.Generic;

namespace Server.Engines.Craft
{
	public enum CraftMarkOption
	{
		MarkItem,
		DoNotMark,
		PromptForMark
	}

    #region SA
    public enum CraftQuestOption
    {
        QuestItem,
        NonQuestItem
    }
    #endregion

	public class CraftContext
	{
		private List<CraftItem> m_Items;
		private int m_LastResourceIndex;
		private int m_LastResourceIndex2;
		private int m_LastGroupIndex;
		private bool m_DoNotColor;
		private CraftMarkOption m_MarkOption;

        private CraftQuestOption m_QuestOption;

		#region Hue State Vars
		private bool m_CheckedHues;
		private List<int> m_Hues;
		private Item m_CompareHueTo;

		public bool CheckedHues { get { return m_CheckedHues; } set { m_CheckedHues = value; } }
		public List<int> Hues { get { return m_Hues; } set { m_Hues = value; } }
		public Item CompareHueTo { get { return m_CompareHueTo; } set { m_CompareHueTo = value; } }
		#endregion

		public List<CraftItem> Items { get { return m_Items; } }
		public int LastResourceIndex{ get{ return m_LastResourceIndex; } set{ m_LastResourceIndex = value; } }
		public int LastResourceIndex2{ get{ return m_LastResourceIndex2; } set{ m_LastResourceIndex2 = value; } }
		public int LastGroupIndex{ get{ return m_LastGroupIndex; } set{ m_LastGroupIndex = value; } }
		public bool DoNotColor{ get{ return m_DoNotColor; } set{ m_DoNotColor = value; } }
		public CraftMarkOption MarkOption{ get{ return m_MarkOption; } set{ m_MarkOption = value; } }
        #region SA
        public CraftQuestOption QuestOption { get { return m_QuestOption; } set { m_QuestOption = value; } }
        #endregion

		public CraftContext()
		{
			m_Items = new List<CraftItem>();
			m_LastResourceIndex = -1;
			m_LastResourceIndex2 = -1;
			m_LastGroupIndex = -1;

			m_CheckedHues = false;
			m_Hues = new List<int>();
			m_CompareHueTo = null;
		}

		public CraftItem LastMade
		{
			get
			{
				if ( m_Items.Count > 0 )
					return m_Items[0];

				return null;
			}
		}

		public void OnMade( CraftItem item )
		{
			m_Items.Remove( item );

			if ( m_Items.Count == 10 )
				m_Items.RemoveAt( 9 );

			m_Items.Insert( 0, item );
		}
		
		public void ResetHueStateVars()
		{
			m_CheckedHues = false;
			m_Hues = new List<int>();
			m_CompareHueTo = null;
		}
	}
}