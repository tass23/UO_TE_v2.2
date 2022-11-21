using System;
using Server;
using Server.Mobiles;

namespace Server.Items
{
	public class PetMatAddon : BaseAddon
	{
		public override BaseAddonDeed Deed{ get{ return new PetMatDeed( m_HousePet ); } }
		public override bool RetainDeedHue{ get{ return true; } }

		private HousePet m_HousePet;
		
		[CommandProperty( AccessLevel.GameMaster)]
		public HousePet HousePet
		{
			get { return m_HousePet; }
			set { m_HousePet = value; }
		}

		[Constructable]
		public PetMatAddon() : this( null )
		{			
		}

		public PetMatAddon( HousePet HousePet )
		{
			Name = "a pet mat";
			
			switch ( Utility.Random( 6 ) )  // modify as necessary
			{
				case 0:
				{
					AddComplexComponent( (BaseAddon) this, 2755, -1, -1, 0, 0, -1, "a pet mat", 1);// 1
					AddComplexComponent( (BaseAddon) this, 2757, 1, -1, 0, 0, -1, "a pet mat", 1);// 2
					AddComplexComponent( (BaseAddon) this, 2807, 0, -1, 0, 0, -1, "a pet mat", 1);// 3
					AddComplexComponent( (BaseAddon) this, 2754, 1, 1, 0, 0, -1, "a pet mat", 1);// 4
					AddComplexComponent( (BaseAddon) this, 2756, -1, 1, 0, 0, -1, "a pet mat", 1);// 5
					AddComplexComponent( (BaseAddon) this, 2806, -1, 0, 0, 0, -1, "a pet mat", 1);// 6
					AddComplexComponent( (BaseAddon) this, 2808, 1, 0, 0, 0, -1, "a pet mat", 1);// 7
					AddComplexComponent( (BaseAddon) this, 2809, 0, 1, 0, 0, -1, "a pet mat", 1);// 8
					AddComplexComponent( (BaseAddon) this, 2810, 0, 0, 0, 0, -1, "a pet mat", 1);// 9
					break;
				}
				case 1:
				{
					AddComplexComponent( (BaseAddon) this, 2755, -1, -1, 0, 0, -1, "a pet mat", 1);// 1
					AddComplexComponent( (BaseAddon) this, 2807, 0, -1, 0, 0, -1, "a pet mat", 1);// 2
					AddComplexComponent( (BaseAddon) this, 2757, 1, -1, 0, 0, -1, "a pet mat", 1);// 3
					AddComplexComponent( (BaseAddon) this, 2806, -1, 0, 0, 0, -1, "a pet mat", 1);// 4
					AddComplexComponent( (BaseAddon) this, 2753, 0, 0, 0, 0, -1, "a pet mat", 1);// 5
					AddComplexComponent( (BaseAddon) this, 2808, 1, 0, 0, 0, -1, "a pet mat", 1);// 6
					AddComplexComponent( (BaseAddon) this, 2756, -1, 1, 0, 0, -1, "a pet mat", 1);// 7
					AddComplexComponent( (BaseAddon) this, 2809, 0, 1, 0, 0, -1, "a pet mat", 1);// 8
					AddComplexComponent( (BaseAddon) this, 2754, 1, 1, 0, 0, -1, "a pet mat", 1);// 9
					break;
				}
				case 2:
				{
					AddComplexComponent( (BaseAddon) this, 2755, -1, -1, 0, 0, -1, "a pet mat", 1);// 1
					AddComplexComponent( (BaseAddon) this, 2807, 0, -1, 0, 0, -1, "a pet mat", 1);// 2
					AddComplexComponent( (BaseAddon) this, 2757, 1, -1, 0, 0, -1, "a pet mat", 1);// 3
					AddComplexComponent( (BaseAddon) this, 2806, -1, 0, 0, 0, -1, "a pet mat", 1);// 4
					AddComplexComponent( (BaseAddon) this, 2750, 0, 0, 0, 0, -1, "a pet mat", 1);// 5
					AddComplexComponent( (BaseAddon) this, 2808, 1, 0, 0, 0, -1, "a pet mat", 1);// 6
					AddComplexComponent( (BaseAddon) this, 2756, -1, 1, 0, 0, -1, "a pet mat", 1);// 7
					AddComplexComponent( (BaseAddon) this, 2809, 0, 1, 0, 0, -1, "a pet mat", 1);// 8
					AddComplexComponent( (BaseAddon) this, 2754, 1, 1, 0, 0, -1, "a pet mat", 1);// 9
					break;
				}
				case 3:
				{
					AddComplexComponent( (BaseAddon) this, 2761, 1, 1, 0, 0, -1, "a pet mat", 1);// 1
					AddComplexComponent( (BaseAddon) this, 2764, 1, -1, 0, 0, -1, "a pet mat", 1);// 2
					AddComplexComponent( (BaseAddon) this, 2766, 0, -1, 0, 0, -1, "a pet mat", 1);// 3
					AddComplexComponent( (BaseAddon) this, 2767, 1, 0, 0, 0, -1, "a pet mat", 1);// 4
					AddComplexComponent( (BaseAddon) this, 2768, 0, 1, 0, 0, -1, "a pet mat", 1);// 5
					AddComplexComponent( (BaseAddon) this, 2758, 0, 0, 0, 0, -1, "a pet mat", 1);// 6
					AddComplexComponent( (BaseAddon) this, 2762, -1, -1, 0, 0, -1, "a pet mat", 1);// 7
					AddComplexComponent( (BaseAddon) this, 2765, -1, 0, 0, 0, -1, "a pet mat", 1);// 8
					AddComplexComponent( (BaseAddon) this, 2763, -1, 1, 0, 0, -1, "a pet mat", 1);// 9
					break;
				}
				case 4:
				{
					AddComplexComponent( (BaseAddon) this, 2762, -1, -1, 0, 0, -1, "a pet mat", 1);// 1
					AddComplexComponent( (BaseAddon) this, 2765, -1, 0, 0, 0, -1, "a pet mat", 1);// 2
					AddComplexComponent( (BaseAddon) this, 2763, -1, 1, 0, 0, -1, "a pet mat", 1);// 3
					AddComplexComponent( (BaseAddon) this, 2766, 0, -1, 0, 0, -1, "a pet mat", 1);// 4
					AddComplexComponent( (BaseAddon) this, 2764, 1, -1, 0, 0, -1, "a pet mat", 1);// 5
					AddComplexComponent( (BaseAddon) this, 2759, 0, 0, 0, 0, -1, "a pet mat", 1);// 6
					AddComplexComponent( (BaseAddon) this, 2767, 1, 0, 0, 0, -1, "a pet mat", 1);// 7
					AddComplexComponent( (BaseAddon) this, 2768, 0, 1, 0, 0, -1, "a pet mat", 1);// 8
					AddComplexComponent( (BaseAddon) this, 2761, 1, 1, 0, 0, -1, "a pet mat", 1);// 9
					break;
				}
				case 5:
				{
					AddComplexComponent( (BaseAddon) this, 2755, -1, -1, 0, 0, -1, "a pet mat", 1);// 1
					AddComplexComponent( (BaseAddon) this, 2756, -1, 1, 0, 0, -1, "a pet mat", 1);// 2
					AddComplexComponent( (BaseAddon) this, 2806, -1, 0, 0, 0, -1, "a pet mat", 1);// 3
					AddComplexComponent( (BaseAddon) this, 2807, 0, -1, 0, 0, -1, "a pet mat", 1);// 4
					AddComplexComponent( (BaseAddon) this, 2809, 0, 1, 0, 0, -1, "a pet mat", 1);// 5
					AddComplexComponent( (BaseAddon) this, 2752, 0, 0, 0, 0, -1, "a pet mat", 1);// 6
					AddComplexComponent( (BaseAddon) this, 2754, 1, 1, 0, 0, -1, "a pet mat", 1);// 7
					AddComplexComponent( (BaseAddon) this, 2808, 1, 0, 0, 0, -1, "a pet mat", 1);// 8
					AddComplexComponent( (BaseAddon) this, 2757, 1, -1, 0, 0, -1, "a pet mat", 1);// 9
					break;
				}
			}
			m_HousePet = HousePet;
		}

		public override void OnLocationChange( Point3D oldLocation )
		{
			base.OnLocationChange( oldLocation );
			
			if ( m_HousePet != null )
				m_HousePet.Location = new Point3D( X, Y, Z + 1 );
		}		
		
		public override void OnMapChange()
		{
			base.OnMapChange();
			
			if ( m_HousePet != null )
				m_HousePet.Map = Map;
		}
		
		public override void OnAfterDelete()
		{
			base.OnAfterDelete();
			
			if ( m_HousePet != null )
				m_HousePet.Internalize();
		}

		public PetMatAddon( Serial serial ) : base( serial )
		{
		}

		private static void AddComplexComponent(BaseAddon addon, int item, int xoffset, int yoffset, int zoffset, int hue, int lightsource)
        {
            AddComplexComponent(addon, item, xoffset, yoffset, zoffset, hue, lightsource, null, 1);
        }

		private static void AddComplexComponent(BaseAddon addon, int item, int xoffset, int yoffset, int zoffset, int hue, int lightsource, string name, int amount)
        {
            AddonComponent ac;
            ac = new AddonComponent(item);
            if (name != null && name.Length > 0)
                ac.Name = name;
            if (hue != 0)
                ac.Hue = hue;
            if (amount > 1)
            {
                ac.Stackable = true;
                ac.Amount = amount;
            }
            if (lightsource != -1)
                ac.Light = (LightType) lightsource;
            addon.AddComponent(ac, xoffset, yoffset, zoffset);
        }

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version

			writer.Write( m_HousePet );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			m_HousePet = (HousePet) reader.ReadMobile();
		}
	}

	public class PetMatDeed : BaseAddonDeed
	{
		public override BaseAddon Addon{ get{ return new PetMatAddon( m_HousePet ); } }

		private HousePet m_HousePet;
		
		[CommandProperty( AccessLevel.GameMaster)]
		public HousePet HousePet
		{
			get { return m_HousePet; }
			set { m_HousePet = value; InvalidateProperties(); }
		} 

		[Constructable]
		public PetMatDeed() : this( null )
		{
		}

		public PetMatDeed( HousePet HousePet )
		{
			LootType = LootType.Blessed;
			Name = "a deed for a pet mat";
			m_HousePet = HousePet;
		}

		public PetMatDeed( Serial serial ) : base( serial )
		{
		}

		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );

			if ( m_HousePet != null )
			{
				if ( m_HousePet.Name != null )
					list.Add( "Includes a House Pet named: {0} {1}", m_HousePet.Name, m_HousePet.HPetType );
				else
					list.Add( "Includes a House Pet" );
					
				int weeks = HousePet.GetWeeks( m_HousePet.Birth );
				
				if ( weeks == 1 )
					list.Add( 1072626 ); // 1 week old
				else if ( weeks > 1 )
					list.Add( 1072627, weeks.ToString() ); // ~1_AGE~ weeks old
			}
		}

		private bool m_Safety;
		
		public override void DeleteDeed()
		{
			m_Safety = true;
			
			base.DeleteDeed();
		}
		
		public override void OnAfterDelete()
		{				
			base.OnAfterDelete();
			
			if ( !m_Safety && m_HousePet != null )
				m_HousePet.Delete();
				
			m_Safety = false;
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version

			writer.Write( m_HousePet );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			switch ( version )
			{
				case 0:
				{
					m_HousePet = reader.ReadMobile() as HousePet;
					break;
				}
			}
		}
	}
}