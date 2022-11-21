using System;
using Server;

namespace Server.Items
{	
	public class ZooMemberCloak : Cloak
	{
		public override int LabelNumber{ get{ return 1073221; } } // Britannia Royal Zoo Member
	
		[Constructable]
		public ZooMemberCloak() : this( 0 )
		{
		}

		[Constructable]
		public ZooMemberCloak( int hue ) : base( hue )
		{
		}

		public ZooMemberCloak( Serial serial ) : base( serial )
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
	
	public class ZooMemberThighBoots : ThighBoots
	{
		public override int LabelNumber{ get{ return 1073221; } } // Britannia Royal Zoo Member
	
		[Constructable]
		public ZooMemberThighBoots() : this( 0 )
		{
		}

		[Constructable]
		public ZooMemberThighBoots( int hue ) : base( hue )
		{
		}

		public ZooMemberThighBoots( Serial serial ) : base( serial )
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
	
	public class ZooMemberFloppyHat : FloppyHat
	{
		public override int LabelNumber{ get{ return 1073221; } } // Britannia Royal Zoo Member
	
		[Constructable]
		public ZooMemberFloppyHat() : this( 0 )
		{
		}

		[Constructable]
		public ZooMemberFloppyHat( int hue ) : base( hue )
		{
		}

		public ZooMemberFloppyHat( Serial serial ) : base( serial )
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
	
	public class ZooMemberBonnet : Bonnet
	{
		public override int LabelNumber{ get{ return 1073221; } } // Britannia Royal Zoo Member
	
		[Constructable]
		public ZooMemberBonnet() : this( 0 )
		{
		}

		[Constructable]
		public ZooMemberBonnet( int hue ) : base( hue )
		{
		}

		public ZooMemberBonnet( Serial serial ) : base( serial )
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
	
	public class ZooMemberRobe : Robe
	{
		public override int LabelNumber{ get{ return 1073221; } } // Britannia Royal Zoo Member
	
		[Constructable]
		public ZooMemberRobe() : this( 0 )
		{
		}

		[Constructable]
		public ZooMemberRobe( int hue ) : base( hue )
		{
		}

		public ZooMemberRobe( Serial serial ) : base( serial )
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
	
	
	public class ZooMemberBodySash : BodySash
	{
		public override int LabelNumber{ get{ return 1073221; } } // Britannia Royal Zoo Member
	
		[Constructable]
		public ZooMemberBodySash() : this( 0 )
		{
		}

		[Constructable]
		public ZooMemberBodySash( int hue ) : base( hue )
		{
		}

		public ZooMemberBodySash( Serial serial ) : base( serial )
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
	
	public class ZooMemberSkirt : PlainDress
	{
		public override int LabelNumber{ get{ return 1073221; } } // Britannia Royal Zoo Member
	
		[Constructable]
		public ZooMemberSkirt() : this( 0 )
		{
		}

		[Constructable]
		public ZooMemberSkirt( int hue ) : base( hue )
		{
		}

		public ZooMemberSkirt( Serial serial ) : base( serial )
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