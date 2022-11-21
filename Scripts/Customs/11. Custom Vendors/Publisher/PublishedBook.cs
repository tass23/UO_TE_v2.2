using System;
using Server;
using System.Text;
using System.Security.Cryptography;
using Server.Engines.Publisher;

namespace Server.Items
{
	public class PublishedBook : BaseBook
	{
		private string m_OriginalHash = string.Empty;
		private DateTime m_FirstPublished = DateTime.Now;
		private DateTime m_LastPublished = DateTime.Now;
		private string m_PublishID = string.Empty;
		private string[] m_Contributors = null;

		[Constructable]
		public PublishedBook(BaseBook book) : base(book.ItemID, book.Title, book.Author, book.PagesCount, book.Writable)
		{
			// copy book
			Weight = book.Weight;
			Name = book.Name;
			Hue = book.Hue;
			for(int i = 0; i < Pages.Length; i++)
				Pages[i] = book.Pages[i];
			RePublish();
			m_LastPublished = m_FirstPublished;
		}
		[Constructable]
		public PublishedBook(string id, int pageCount) : base(0x0000, "", "", pageCount, false)
		{
			XmlBook book = XmlBook.Load(id);
			this.ItemID = book.ItemID;
			this.Title = book.Title;
			this.Author = book.Author;
			this.Hue = book.Hue;
			this.PublishID = book.ID.ToString();
			this.m_FirstPublished = DateTime.Parse(book.FirstPublished);
			this.m_LastPublished = DateTime.Parse(book.LastPublished);
			for(int i = 0; i < book.Contributors.Length; i++)
				AddContributor(book.Contributors[i].Name);
			for(int i = 0; i < book.Contents.Length; i++)
			{
				if(this.Pages[book.Contents[i].Page-1].Lines.Length == 0)
				{
					this.Pages[book.Contents[i].Page-1].Lines = new string[8];
					for(int j = 0; j < this.Pages[book.Contents[i].Page-1].Lines.Length; j++)
						this.Pages[book.Contents[i].Page-1].Lines[j] = string.Empty;
				}
				this.Pages[book.Contents[i].Page-1].Lines[book.Contents[i].Line-1] = book.Contents[i].Text;
			}
				
		}
			
		public void RePublish()
		{
			m_OriginalHash = Hash;
			m_LastPublished = DateTime.Now;
		}
		public bool IsPublishable()
		{
			if(!Writable) return false;
			// make sure something exists other then default information ...
			if(Title.Trim().Length < 5) return false;
			if(Author.Trim().Length < 5) return false;
			if(Title.ToLower().Trim() == "title") return false;
			if(Author.ToLower().Trim() == "author") return false;
			int count = 0;
			for(int i = 0; i < Pages.Length; i++)
				count += string.Join("", Pages[i].Lines).Trim().Length;
			return count >= 100; // 100 characters or more.
		}
		public string FullText
		{
			get
			{
				StringBuilder sb = new StringBuilder();
				sb.Append(Title.Trim());
				sb.Append("\r\n");
				sb.Append(Author.Trim());
				sb.Append("\r\n");
				for(int i = 0; i < Pages.Length; i++)
					sb.Append(string.Join("\r\n", Pages[i].Lines));
				return sb.ToString().Trim();
			}
		}
		public string PagedText
		{
			get
			{
				StringBuilder sb = new StringBuilder();
				for(int i = 0; i < Pages.Length; i++)
					sb.Append(string.Join("\r\n", Pages[i].Lines));
				return sb.ToString().Trim();
			}
		}
		private string Hash
		{
			get
			{
				byte[] buffer = Encoding.ASCII.GetBytes(FullText) ;
				MD5 m = new MD5CryptoServiceProvider();
				byte[] r = m.ComputeHash(buffer);
				return BitConverter.ToString(m.Hash);
			}
		}
		public DateTime FirstPublished
		{
			get
			{
				return m_FirstPublished;
			}
		}
		public DateTime LastPublished
		{
			get
			{
				return m_LastPublished;
			}
		}
		public string PublishID
		{
			get
			{
				return m_PublishID;
			}
			set
			{
				m_PublishID = value;
			}
		}

		public string[] Contributors
		{
			get
			{
				return m_Contributors;
			}
		}
		public void AddContributor(string name)
		{
			if(m_Contributors == null || m_Contributors.Length == 0)
			{
				m_Contributors = new string[]{name};
				return;
			}
			for(int i = 0; i < m_Contributors.Length; i++)
				if(m_Contributors[i] == name) return;
			string list = string.Join(";", m_Contributors);
			list += ";" + name;
			m_Contributors = list.Split(';');
		}
		public bool IsModified()
		{
			return m_OriginalHash != Hash;
		}
		public PublishedBook( Serial serial ) : base( serial )
		{
		}
		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			switch(version)
			{
				case 0:
				{
					string contributors = reader.ReadString();
					if(contributors.Length == 0)
						m_Contributors = new string[0];
					else
						m_Contributors = contributors.Split(';');
					m_FirstPublished = reader.ReadDateTime();
					m_LastPublished = reader.ReadDateTime();
					m_OriginalHash = reader.ReadString();
					m_PublishID = reader.ReadString();
					break;
				}
			}
		}
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int)0 ); // version
			if(m_Contributors == null || m_Contributors.Length == 0)
				writer.Write(string.Empty);
			else
				writer.Write(string.Join(";", m_Contributors));
			writer.Write(m_FirstPublished);
			writer.Write(m_LastPublished);
			writer.Write(m_OriginalHash);
			writer.Write(m_PublishID);
		}
		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );
			list.Add(1060659, "Published\t{0}", m_FirstPublished.ToString("yyyy-MM-dd HH:mm")); // ~1_val~: ~2_val~
			if(m_FirstPublished != m_LastPublished)
				list.Add(1060660, "Edited\t{0}", m_LastPublished.ToString("yyyy-MM-dd HH:mm")); // ~1_val~: ~2_val~
		}
	}
}