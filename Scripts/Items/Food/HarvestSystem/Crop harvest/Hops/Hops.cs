using System;
using Server;
using Server.Items;
using Server.Network;

namespace Server.Items
{
	public abstract class BaseHops : Item, ICommodity
	{
		int ICommodity.DescriptionNumber { get { return LabelNumber; } }
		bool ICommodity.IsDeedable { get { return true; } }
		
		private HopsVariety m_Variety;

		[CommandProperty( AccessLevel.GameMaster )]
		public HopsVariety Variety
		{
			get{ return m_Variety; }
			set{ m_Variety = value; InvalidateProperties(); }
		}

		/*string ICommodity.Description
		{
			get
			{
				return String.Format( Amount == 1 ? "{0} {1} hops" : "{0} {1} hops", Amount, BrewingResources.GetName( m_Variety ).ToLower() );
			}
		}*/

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 1 );

			writer.Write( (int) m_Variety );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			switch ( version )
			{
				case 1:
				{
					m_Variety = ( HopsVariety )reader.ReadInt();
					break;
				}
				case 0:
				{
					HopsInfo info;

					switch ( reader.ReadInt() )
					{
						case 0: info = HopsInfo.BitterHops; break;
						case 1: info = HopsInfo.SnowHops; break;
						case 2: info = HopsInfo.ElvenHops; break;
						case 3: info = HopsInfo.SweetHops; break;
						default: info = null; break;
					}

					m_Variety = BrewingResources.GetFromHopsInfo( info );
					break;
				}
			}
		}

		public BaseHops( HopsVariety variety ) : this( variety, 1 )
		{
		}

		public BaseHops( HopsVariety variety, int amount ) : base( 0x1AA2 )
		{
			Stackable = true;
			Weight = 0.1;
			Amount = amount;
			Hue = BrewingResources.GetHue( variety );

			m_Variety = variety;
		}

		public BaseHops( Serial serial ) : base( serial )
		{
		}

		public override void AddNameProperty( ObjectPropertyList list )
		{
			if ( Amount > 1 )
				list.Add( 1050039, "{0}\t{1}", Amount, "Bunches of "+BrewingResources.GetName( m_Variety )+" Cones" );
			else
				list.Add( "Bunch of "+BrewingResources.GetName( m_Variety )+" Cones" );
		}

		public override void OnSingleClick( Mobile from )
		{
			if ( Amount > 1 )
				LabelTo( from, "{0} Hops Bunches : {1}", BrewingResources.GetName( m_Variety ), Amount );
			else
				LabelTo( from, "{0} Hops Bunch", BrewingResources.GetName( m_Variety ) );
		}
	}

	public class BitterHops : BaseHops
	{
		[Constructable]
		public BitterHops() : this( 1 )
		{
		}

		[Constructable]
		public BitterHops( int amount ) : base( HopsVariety.BitterHops, amount )
		{
			Name = "Bitter Hops";
		}

		public BitterHops( Serial serial ) : base( serial )
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

	public class SnowHops : BaseHops
	{
		[Constructable]
		public SnowHops() : this( 1 )
		{
		}

		[Constructable]
		public SnowHops( int amount ) : base( HopsVariety.SnowHops, amount )
		{
		}

		public SnowHops( Serial serial ) : base( serial )
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

	public class ElvenHops : BaseHops
	{
		[Constructable]
		public ElvenHops() : this( 1 )
		{
		}

		[Constructable]
		public ElvenHops( int amount ) : base( HopsVariety.ElvenHops, amount )
		{
		}

		public ElvenHops( Serial serial ) : base( serial )
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

	public class SweetHops : BaseHops
	{
		[Constructable]
		public SweetHops() : this( 1 )
		{
		}

		[Constructable]
		public SweetHops( int amount ) : base( HopsVariety.SweetHops, amount )
		{
		}

		public SweetHops( Serial serial ) : base( serial )
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