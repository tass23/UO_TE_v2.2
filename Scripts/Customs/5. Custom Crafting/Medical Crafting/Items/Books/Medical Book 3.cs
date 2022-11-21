using System;
using Server;

namespace Server.Items
{

	public class MedicalBook3 : BaseBook
	{
		private const string TITLE = "Medical Book 3";
		private const string AUTHOR = null;
		private const int PAGES = 5;
		private const bool WRITABLE = false;
		private const int STYLE = 0xFF1;

		[Constructable]
		public MedicalBook3() : base( STYLE, TITLE, AUTHOR, PAGES, WRITABLE )
		{
			int cnt = 0;
			string[] lines;
			lines = new string[]
			{
				
			};
		}
		
		public MedicalBook3( Serial serial ) : base( serial )
		{
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int)0 ); // version
		}
	}
}