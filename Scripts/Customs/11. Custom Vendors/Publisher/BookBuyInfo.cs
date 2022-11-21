using System;
using System.Collections;
using System.Collections.Generic;
using Server.Items;
using Server.Engines.Publisher;

namespace Server.Mobiles
{
	public class BookBuyInfo : GenericBuyInfo
	{
		private string m_ID;
		private int m_PageCount;

		public BookBuyInfo(string id) : base(typeof(PublishedBook), 30, 10, 0x0000, 0x000)
		{
			XmlBook book = XmlBook.Load(id);
			m_ID = id;
			m_PageCount = 1;
			for(int i = 0; i < book.Contents.Length; i++)
				if(m_PageCount < book.Contents[i].Page)
					m_PageCount = book.Contents[i].Page;
			this.Name = book.Title;
			this.ItemID = book.ItemID;
			this.Hue = book.Hue;
			// base the price upon the number of lines written
			this.Price = book.Contents.Length; 
			// stock between 5 and 20 items
			this.Amount = 5; 
			this.MaxAmount = 20;
		}
		/*
		public override object GetEntity()
		{
			return Activator.CreateInstance( Type, new object[]{m_ID, m_PageCount} );
		}
		*/
        public override IEntity GetEntity()
		{
			return (IEntity)Activator.CreateInstance( Type, new object[]{ m_ID, m_PageCount } );
		} 
	}
}