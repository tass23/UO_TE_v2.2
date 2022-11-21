using System;
using System.Collections;
using Server.Network;

namespace Server.Items
{
	public class Pizza : Food
	{
		private string m_Desc;

		[CommandProperty( AccessLevel.Counselor )]
                public string Desc {
                        get { return m_Desc; }
                        set {
				m_Desc = value;
				Name = "cooked " + m_Desc + " pizza";
				InvalidateProperties();
			}
                }

		[Constructable]
		public Pizza() : this( "cheese", 0 )
		{
		}

		[Constructable]
		public Pizza( int color ) : this( "cheese", color )
		{
		}

		[Constructable]
		public Pizza( string desc ) : this( desc, 0 )
		{
		}

		[Constructable]
		public Pizza( string desc, int color ) : base( 0x1040 )
		{
			this.Weight = 1.0;
			this.FillFactor = 6;

			if ( desc != "" && desc != null )
			{
				Desc = desc;
			}
			else
			{
				Desc = "cheese";
			}

			Hue = color;
		}

		public Pizza( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 1 );

			writer.Write( (string) m_Desc );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			switch ( version )
			{
				case 1:
				m_Desc = reader.ReadString();
				break;
			}
		}
	}

	public class CheesePizza : Food
	{
		public override int LabelNumber{ get{ return 1044516; } }

		[Constructable]
		public CheesePizza() : base( 0x1040 )
		{
			this.Weight = 1.0;
			this.FillFactor = 6;
		}

		public CheesePizza( Serial serial ) : base( serial )
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

	public class SausagePizza : Food
	{
		public override int LabelNumber{ get{ return 1044517; } }

		[Constructable]
		public SausagePizza() : base( 0x1040 )
		{
			this.Weight = 1.0;
			this.FillFactor = 6;
		}

		public SausagePizza( Serial serial ) : base( serial )
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