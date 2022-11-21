using System;
using System.Xml.Serialization;
using Server.Items;
using System.IO;

/*
 * This book is specifically used for Xml Serialization/Deserialization
 * The resulting xml can then be used to publish to blogs,
 * publish to your own shards website, or create additional
 * copies of books within the game.
 */

namespace Server.Engines.Publisher
{
	[Serializable, XmlRoot("book", IsNullable=false)]
	public class XmlBook
	{
		[XmlElement("id", typeof(Guid))] public Guid ID = Guid.Empty;
		[XmlElement("title", typeof(string))] public string Title = string.Empty;
		[XmlElement("author", typeof(string))] public string Author = string.Empty;
		[XmlElement("created", typeof(string))] public string FirstPublished = string.Empty;
		[XmlElement("modified", typeof(string))] public string LastPublished = string.Empty;
		[XmlArray("contributors"), XmlArrayItem("contributor", typeof(Contributor))] public Contributor[] Contributors = null;
		[XmlElement("hue", typeof(int))] public int Hue = int.MinValue;
		[XmlElement("huedItemId", typeof(int))] public int HuedItemID = int.MinValue;
		[XmlElement("itemId", typeof(int))] public int ItemID = int.MinValue;
		[XmlArray("contents"), XmlArrayItem("content", typeof(Content))] public Content[] Contents = null;
		public class Content
		{
			[XmlText(typeof(string))] public string Text = string.Empty;
			[XmlAttribute("page")] public int Page = int.MinValue;
			[XmlAttribute("line")] public int Line = int.MinValue;
		}
		public class Contributor
		{
			[System.Xml.Serialization.XmlText(typeof(string))] public string Name = string.Empty;
		}
		public static XmlBook Load(string id)
		{
			string fileName = "data/books/" + id + ".xml";
			try
			{
				XmlSerializer serializer = new XmlSerializer(typeof(XmlBook));
				FileStream fs = new FileStream(fileName, FileMode.Open);
				XmlBook book = (XmlBook)serializer.Deserialize(fs);
				fs.Close();
				return book;
			}
			catch(Exception)
			{
				return null;
			}
		}
		public static string DataPath
		{
			get
			{
				return string.Format("data{0}books{0}", Path.DirectorySeparatorChar);
			}
		}
		public static bool Save(PublishedBook publishedBook)
		{
			XmlBook book = XmlBook.FromPublishedBook(publishedBook);
			string fileName = DataPath + book.ID.ToString() + ".xml";
			try
			{
				if(!Directory.Exists(Path.GetDirectoryName(fileName)))
					Directory.CreateDirectory(Path.GetDirectoryName(fileName));
				XmlSerializer serializer = new XmlSerializer(typeof(XmlBook));
				FileStream fs = new FileStream(fileName, FileMode.Create);
				serializer.Serialize(fs, book);
				fs.Close();
				return true;
			}
			catch(Exception)
			{
				return false;
			}
		}
		private static XmlBook FromPublishedBook(PublishedBook publishedBook)
		{
			XmlBook book = new XmlBook();
			book.Title = publishedBook.Title;
			book.Author = publishedBook.Author;
			book.FirstPublished = publishedBook.FirstPublished.ToString("u").Replace(' ','T');
			book.LastPublished = publishedBook.LastPublished.ToString("u").Replace(' ','T');
			book.Hue = publishedBook.Hue;
			book.HuedItemID = publishedBook.HuedItemID;
			book.ItemID = publishedBook.ItemID;
			getBookContributors(publishedBook, book);
			getBookPages(publishedBook, book);
			try
			{
				book.ID = new Guid(publishedBook.PublishID);
			}
			catch(Exception)
			{
				book.ID = Guid.NewGuid();
				publishedBook.PublishID = book.ID.ToString();
			}
			return book;
		}
		private static void getBookContributors(PublishedBook publishedBook, XmlBook book)
		{
			if(publishedBook.Contributors != null)
			{
				book.Contributors = new XmlBook.Contributor[publishedBook.Contributors.Length];
				for(int i = 0; i < publishedBook.Contributors.Length; i++)
				{
					book.Contributors[i] = new XmlBook.Contributor();
					book.Contributors[i].Name = publishedBook.Contributors[i];
				}
			}
		}
		private static void getBookPages(PublishedBook publishedBook, XmlBook book)
		{
			int count = 0;
			for(int i = 0; i < publishedBook.Pages.Length; i++)
				for(int j = 0; j < publishedBook.Pages[i].Lines.Length; j++)
					if(publishedBook.Pages[i].Lines[j].Trim() != string.Empty)
						count++;
			book.Contents = new XmlBook.Content[count];
			count = 0;
			for(int i = 0; i < publishedBook.Pages.Length; i++)
				for(int j = 0; j < publishedBook.Pages[i].Lines.Length; j++)
					if(publishedBook.Pages[i].Lines[j].Trim() != string.Empty)
					{
						book.Contents[count] = new XmlBook.Content();
						book.Contents[count].Line = j + 1;
						book.Contents[count].Page = i + 1;
						book.Contents[count].Text = publishedBook.Pages[i].Lines[j];
						count++;
					}
		}
		public static string[] RandomBookIds(int max)
		{
			if(!Directory.Exists(DataPath))
				Directory.CreateDirectory(DataPath);
			string[] files = Directory.GetFiles(DataPath, "*.xml");
			if(max > files.Length)
			{
				for(int i = 0; i < files.Length; i++)
					files[i] = Path.GetFileNameWithoutExtension(files[i]);
				return files;
			}

			string[] chosen = new string[max];
			for(int i = 0; i < max; i++)
			{
				bool match = false;
				do
				{
					chosen[i] = Path.GetFileNameWithoutExtension(files[new Random().Next(0, files.Length-1)]);
					match = false;
					for(int n = 0; n < i; n++)
						if(chosen[i] == chosen[n])
						{
							match = true;
							break;
						}
				} while(match);
			}
			return chosen;
		}
	}
}
