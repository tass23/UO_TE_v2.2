using System;

namespace Server.Items
{
	[FlipableAttribute( 0x1541, 0x1542 )]
	public class SashOfAnimalLore : BaseClothingOfAnimalLore
	{
		[Constructable]
		public SashOfAnimalLore( int bonus ) : base( bonus, 0x1541 )
		{
			Weight = 1;
			Name = "tamer sash of animal lore";
			Hue = Utility.RandomMinMax( 1150, 1175 );
			Layer = Layer.MiddleTorso;
		}

		public SashOfAnimalLore( Serial serial ) : base( serial )
		{
		}
		
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}
		
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}

	[FlipableAttribute( 0x153B, 0x153C )]
	public class HalfApronOfAnimalLore : BaseClothingOfAnimalLore
	{
		[Constructable]
		public HalfApronOfAnimalLore( int bonus ) : base( bonus, 0x153B )
		{
			Weight = 2;
			Name = "tamer half apron of animal lore";
			Hue = Utility.RandomMinMax( 1150, 1175);
			Layer = Layer.Waist;
		}

		public HalfApronOfAnimalLore( Serial serial ) : base( serial )
		{
		}
		
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}
		
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}

	[FlipableAttribute( 0x1F03, 0x1F04 )]
	public class RobeOfAnimalLore : BaseClothingOfAnimalLore
	{
		[Constructable]
		public RobeOfAnimalLore( int bonus ) : base( bonus, 0x1F03 )
		{
			Weight = 1;
			Name = "tamer robe of animal lore";
			Hue = Utility.RandomMinMax( 1150, 1175);
			Layer = Layer.OuterTorso;
		}

		public RobeOfAnimalLore( Serial serial ) : base( serial )
		{
		}
		
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}
		
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}

	public abstract class BaseClothingOfAnimalLore : Item
	{
		private int m_Bonus;
		private SkillMod m_SkillMod;

		[CommandProperty( AccessLevel.GameMaster )]
		public int Bonus
		{
			get
			{
				return m_Bonus;
			}
			set
			{
				m_Bonus = value;
				InvalidateProperties();

				if ( m_Bonus == 0 )
				{
					if ( m_SkillMod != null )
						m_SkillMod.Remove();

					m_SkillMod = null;
				}
				else if ( m_SkillMod == null && Parent is Mobile )
				{
					m_SkillMod = new DefaultSkillMod( SkillName.AnimalLore, true, m_Bonus );
					((Mobile)Parent).AddSkillMod( m_SkillMod );
				}
				else if ( m_SkillMod != null )
				{
					m_SkillMod.Value = m_Bonus;
				}
			}
		}

		public override void OnAdded( object parent )
		{
			base.OnAdded( parent );

			if ( m_Bonus != 0 && parent is Mobile )
			{
				if ( m_SkillMod != null )
					m_SkillMod.Remove();

				m_SkillMod = new DefaultSkillMod( SkillName.AnimalLore, true, m_Bonus );
				((Mobile)parent).AddSkillMod( m_SkillMod );
			}
		}

		public override void OnRemoved( object parent )
		{
			base.OnRemoved( parent );

			if ( m_SkillMod != null )
				m_SkillMod.Remove();

			m_SkillMod = null;
		}

		public BaseClothingOfAnimalLore( int bonus, int itemID ) : base( itemID )
		{
			m_Bonus = bonus;
		}

		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );

			if ( m_Bonus != 0 )
				list.Add( 1060658, "animal lore bonus\t+{0}", m_Bonus.ToString() );
		}

		public BaseClothingOfAnimalLore( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version

			writer.Write( (int) m_Bonus );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			switch ( version )
			{
				case 0:
				{
					m_Bonus = reader.ReadInt();
					break;
				}
			}

			if ( m_Bonus != 0 && Parent is Mobile )
			{
				if ( m_SkillMod != null )
					m_SkillMod.Remove();

				m_SkillMod = new DefaultSkillMod( SkillName.AnimalLore, true, m_Bonus );
				((Mobile)Parent).AddSkillMod( m_SkillMod );
			}
		}
	}
}