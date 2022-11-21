using System;
using Server;

namespace Server.Items
{	
	public class LibraryFriendLantern : Lantern
	{
		public override int LabelNumber{ get{ return 1073339; } } // Friends of the Library Reading Lantern

		[Constructable]
		public LibraryFriendLantern() : base()
		{
		}

		public LibraryFriendLantern( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}
	
	public class LibraryFriendReadingChair : BigElvenChair
	{
		public override int LabelNumber{ get{ return 1073340; } } // Friends of the Library Reading Chair

		[Constructable]
		public LibraryFriendReadingChair() : base()
		{
		}

		public LibraryFriendReadingChair( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}
}