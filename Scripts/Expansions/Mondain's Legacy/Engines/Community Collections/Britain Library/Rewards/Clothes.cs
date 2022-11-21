using System;
using Server;

namespace Server.Items
{	
	public class LibraryFriendCloak : Cloak
	{
		public override int LabelNumber{ get{ return 1073350; } } // Friends of the Library Cloak
	
		[Constructable]
		public LibraryFriendCloak() : this( 0 )
		{
		}

		[Constructable]
		public LibraryFriendCloak( int hue ) : base( hue )
		{
		}

		public LibraryFriendCloak( Serial serial ) : base( serial )
		{
		}
		
		public override bool Dye( Mobile from, DyeTub sender )
		{
			from.SendLocalizedMessage( sender.FailMessage );
			return false;
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
	
	public class LibraryFriendPants : LongPants
	{
		public override int LabelNumber{ get{ return 1073349; } } // Friends of the Library Pants
	
		[Constructable]
		public LibraryFriendPants() : this( 0 )
		{
		}

		[Constructable]
		public LibraryFriendPants( int hue ) : base( hue )
		{
		}

		public LibraryFriendPants( Serial serial ) : base( serial )
		{
		}
		
		public override bool Dye( Mobile from, DyeTub sender )
		{
			from.SendLocalizedMessage( sender.FailMessage );
			return false;
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
	
	public class LibraryFriendSurcoat : Surcoat
	{
		public override int LabelNumber{ get{ return 1073348; } } // Friends of the Library Surcoat
	
		[Constructable]
		public LibraryFriendSurcoat() : this( 0 )
		{
		}

		[Constructable]
		public LibraryFriendSurcoat( int hue ) : base( hue )
		{
		}

		public LibraryFriendSurcoat( Serial serial ) : base( serial )
		{
		}
		
		public override bool Dye( Mobile from, DyeTub sender )
		{
			from.SendLocalizedMessage( sender.FailMessage );
			return false;
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
	
	public class LibraryFriendFeatheredHat : FeatheredHat
	{
		public override int LabelNumber{ get{ return 1073347; } } // Friends of the Library Feathered Hat
	
		[Constructable]
		public LibraryFriendFeatheredHat() : this( 0 )
		{
		}

		[Constructable]
		public LibraryFriendFeatheredHat( int hue ) : base( hue )
		{
		}

		public LibraryFriendFeatheredHat( Serial serial ) : base( serial )
		{
		}
		
		public override bool Dye( Mobile from, DyeTub sender )
		{
			from.SendLocalizedMessage( sender.FailMessage );
			return false;
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
	
	public class LibraryFriendDoublet : Doublet
	{
		public override int LabelNumber{ get{ return 1073351; } } // Friends of the Library Doublet
	
		[Constructable]
		public LibraryFriendDoublet() : this( 0 )
		{
		}

		[Constructable]
		public LibraryFriendDoublet( int hue ) : base( hue )
		{
		}

		public LibraryFriendDoublet( Serial serial ) : base( serial )
		{
		}
		
		public override bool Dye( Mobile from, DyeTub sender )
		{
			from.SendLocalizedMessage( sender.FailMessage );
			return false;
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
	
	
	public class LibraryFriendBodySash : BodySash
	{
		public override int LabelNumber{ get{ return 1073346; } } // Friends of the Library Sash
	
		[Constructable]
		public LibraryFriendBodySash() : this( 0 )
		{
		}

		[Constructable]
		public LibraryFriendBodySash( int hue ) : base( hue )
		{
		}

		public LibraryFriendBodySash( Serial serial ) : base( serial )
		{
		}
		
		public override bool Dye( Mobile from, DyeTub sender )
		{
			from.SendLocalizedMessage( sender.FailMessage );
			return false;
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
	
	public class LibraryFriendSkirt : Kilt
	{
		public override int LabelNumber{ get{ return 1073352; } } // Friends of the Library Kilt
	
		[Constructable]
		public LibraryFriendSkirt() : this( 0 )
		{
		}

		[Constructable]
		public LibraryFriendSkirt( int hue ) : base( hue )
		{
		}

		public LibraryFriendSkirt( Serial serial ) : base( serial )
		{
		}
		
		public override bool Dye( Mobile from, DyeTub sender )
		{
			from.SendLocalizedMessage( sender.FailMessage );
			return false;
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