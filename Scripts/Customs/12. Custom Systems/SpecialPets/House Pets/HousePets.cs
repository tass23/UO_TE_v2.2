using System;
using System.Collections;
using System.Collections.Generic;
using Server;
using Server.Items;
using Server.Mobiles;
using Server.Multis;
using Server.Targeting;
using Server.ContextMenus;

namespace Server.Items
{
	public enum HousePetItemTypes
	{
		HouseDog, 
		HouseBird
	}
	
	public class HousePetItem : Item
	{
		private HousePetItemTypes m_PetItemType;
		public HousePetItemTypes PetItemType 
		{ 
			get { return m_PetItemType; } 
			set { m_PetItemType = value; InvalidateProperties(); }
		}

		public string m_HPetType = "";
		[CommandProperty(AccessLevel.GameMaster)]
        public string HPetType
        {
            get { return m_HPetType; }
            set { m_HPetType = value; InvalidateProperties(); }
        }

		public int PetBody;

		[Constructable]
		public HousePetItem() : base(0x0E30)
		{
			switch (Utility.Random(2))  //picks one of the following
            {
				case 0:
				{
					m_PetItemType = HousePetItemTypes.HouseDog;
					m_HPetType = "the house dog";
					Name = "a house dog";
					PetBody = RandomDog();
					Hue = Utility.RandomAnimalHue();
					break;
				}
				case 1:
				{
					m_PetItemType = HousePetItemTypes.HouseBird;
					m_HPetType = "the house bird";
					Name = "a house bird";
					PetBody = RandomBird();
					Hue = Utility.RandomBirdHue();
					break;
				}
				/*default:
				{
					m_PetItemType = HousePetItemTypes.HouseDog;
					m_HPetType = "the house dog";
					Name = "a house dog";
					PetBody = RandomDog();
					break;
				}*/
			}
			//Hue = Utility.RandomAnimalHue();
			Weight = 5.0;
		}

		public HousePetItem( Serial serial ) : base( serial )
		{
		}

		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );
			
			list.Add("Type: {0}", m_HPetType);
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( IsChildOf ( from.Backpack ) )
			{
				//Console.WriteLine( m_PetItemType.ToString() );
				from.Target = new InternalTarget( this );
				from.SendMessage( "Target the Pet Mat where this House Pet will sleep." );
			}	
			else
				from.SendLocalizedMessage( 1042004 ); // That must be in your pack for you to use it.
		}

		public int RandomDog()
		{
			if ( Utility.RandomDouble() < 0.25 )
			{
				return 27;
			}
			else if ( Utility.RandomDouble() < 0.15 )
			{
				return 217;
			}
			else if ( Utility.RandomDouble() < 0.10 )
			{
				return 277;
			}
			else
			{
				return 27;
			}
		}
		
		public int RandomBird()
		{
			if ( Utility.RandomDouble() < 0.25 )
			{
				return 0x11A;
			}
			else if ( Utility.RandomDouble() < 0.15 )
			{
				return 0x5;
			}
			else if ( Utility.RandomDouble() < 0.10 )
			{
				return 0x6;
			}
			else
			{
				return 0x11A;
			}
		}

		public class InternalTarget : Target
		{				
			private HousePetItem m_HousePet;
		
			public InternalTarget( HousePetItem housepet ) : base( 2, false, TargetFlags.None )
			{
				m_HousePet = housepet;
			}
			
			protected override void OnTarget( Mobile from, object targeted )
			{
				if ( targeted is AddonComponent )
				{
					AddonComponent component = (AddonComponent) targeted;
					
					if ( component.Addon is PetMatAddon )
					{
						PetMatAddon rug = (PetMatAddon) component.Addon;

						BaseHouse house = BaseHouse.FindHouseAt( rug );
						
						if ( house != null && house.IsCoOwner( from ) )
						{
							if ( rug.HousePet == null || rug.HousePet.Deleted )
							{
								HousePet hp = new HousePet();
								hp.Hue = m_HousePet.Hue;
								hp.HPetType = m_HousePet.HPetType;
								hp.MoveToWorld( rug.Location, rug.Map );
								hp.Z += 1;
								hp.Body = m_HousePet.PetBody;
								hp.Title = m_HousePet.m_HPetType;
								rug.HousePet = hp;
								m_HousePet.Delete();
							}
							else
								from.SendMessage( "That Pet Mat already has a House Pet assigned to it." );
						}
						else
							from.SendMessage( "House pets can only be placed on Pet Mats in houses where you are an owner or co-owner." );
					}
					else
						from.SendMessage( "You must place the House Pet on a Pet Mat." );
				}
				else
					from.SendMessage( "You must place the House Pet on a Pet Mat." );
			}
			
			protected override void OnTargetOutOfRange( Mobile from, object targeted )
			{
				base.OnTargetOutOfRange( from, targeted );
				
				from.SendMessage( "You must be closer to the Pet Mat to place a House Pet upon it." );
			}
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			
			writer.Write( (int) 0 ); // version
			
			writer.Write(m_HPetType);
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			
			int version = reader.ReadInt();

			m_HPetType = reader.ReadString();
		}
	}
}