using System;
using Server.Targeting;
using Server.Items;
using Server.Network;

namespace Server.Items
{
	[FlipableAttribute( 0x9BD, 0x9BE, 0x9D5, 0x9D4 )]
	public class Silverware : Item
	{
		[Constructable]
		public Silverware( int ItemId ) : base( ItemId )
		{
			this.Weight = 5.0;
			this.Stackable = false;
		}

		[Constructable]
		public Silverware() : this( Utility.RandomList( 0x9BD, 0x9BE, 0x9D5, 0x9D4 ) )
		{
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( !Movable )
				return;

			if ( from.InRange( this.GetWorldLocation(), 1 ) )
			{
				from.SendMessage( "You split up the silverware set." );
				from.AddToBackpack( new Fork() );
				from.AddToBackpack( new Knife() );
				from.AddToBackpack( new Spoon() );

				this.Delete();
			}
		}

		public Silverware( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}

	public class SilverwareN : Silverware
	{
		[Constructable]
		public SilverwareN() : base( 0x9D4 )
		{
		}

		public SilverwareN( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}

	public class SilverwareS : Silverware
	{
		[Constructable]
		public SilverwareS() : base( 0x9BE )
		{
		}

		public SilverwareS( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}

	public class SilverwareE : Silverware
	{
		[Constructable]
		public SilverwareE() : base( 0x9BD )
		{
		}

		public SilverwareE( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}

	public class SilverwareW : Silverware
	{
		[Constructable]
		public SilverwareW() : base( 0x9D5 )
		{
		}

		public SilverwareW( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}
}