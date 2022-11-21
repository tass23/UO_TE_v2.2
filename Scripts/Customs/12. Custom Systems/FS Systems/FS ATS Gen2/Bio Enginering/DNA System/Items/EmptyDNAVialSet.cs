using System;
using Server;
using Server.Items;
using Server.Mobiles;
using Server.Targeting;
using Server.Gumps;

namespace Server.Items
{
	public class EmptyDNAVialSet : Item
	{
		private DNAQuality m_DNAQuality;
		
		private bool m_IsFull;
		private bool m_Prowess;
		private bool m_Environment;
		private bool m_Mental;
		private bool m_Mimic;
		private Double m_DNAMinTame;
		private int m_DNAControlSlots;

		private int m_DNAStr; 			//Prowess
		private int m_DNADex; 			//Environment
		private int m_DNAInt; 			//Mental
		private int m_DNAHits; 			//Prowess
		private int m_DNAStam; 			//Environment
		private int m_DNAMana; 			//Mental

		private int m_DNAFireResist; 		//Environment
		private int m_DNAColdResist; 		//Environment
		private int m_DNAEnergyResist; 		//Mental
		private int m_DNAPoisonResist; 		//Mental
		private int m_DNAPhysicalResist; 	//Prowess

		private int m_DNADamageMin; 		//Prowess
		private int m_DNADamageMax; 		//Prowess

		private int m_DNAArmor; 		//Prowess

		private Double m_DNAMagery; 		//Mental
		private Double m_DNAEvalInt; 		//Mental
		private Double m_DNAMeditation; 	//Mental
		private Double m_DNAMagicResist; 	//Environment
		private Double m_DNAPoisoning; 		//Environment
		private Double m_DNAAnatomy; 		//Prowess
		private Double m_DNATactics; 		//Prowess
		private Double m_DNAWrestling; 		//Prowess

		private bool m_DNABluntAttack; 		//Mimic
		private bool m_DNAHealAttack;  		//Mimic
		private bool m_DNAPoisonAttack;  	//Mimic

		private bool m_DNATrialByFire; 		//Mimic
		private bool m_DNAIceBlast; 		//Mimic
		private bool m_DNACometAttack; 		//Mimic
		private bool m_DNACallOfNature; 	//Mimic
		private bool m_DNAAcidRain;	 	//Mimic

		private int m_DNABodyValue; 		//Mimic
		private int m_DNASoundID; 		//Mimic
		private int m_MountID;			//Minic

		[CommandProperty( AccessLevel.Administrator )]
		public Double DNAMinTame
		{
			get{ return m_DNAMinTame; }
			set{ m_DNAMinTame = value; }
		}

		[CommandProperty( AccessLevel.Administrator )]
		public int DNAControlSlots
		{
			get{ return m_DNAControlSlots; }
			set{ m_DNAControlSlots = value; }
		}

		[CommandProperty( AccessLevel.Administrator )]
		public int DNAStr
		{
			get{ return m_DNAStr; }
			set{ m_DNAStr = value; }
		}

		[CommandProperty( AccessLevel.Administrator )]
		public int DNADex
		{
			get{ return m_DNADex; }
			set{ m_DNADex = value; }
		}

		[CommandProperty( AccessLevel.Administrator )]
		public int DNAInt
		{
			get{ return m_DNAInt; }
			set{ m_DNAInt = value; }
		}

		[CommandProperty( AccessLevel.Administrator )]
		public int DNAHits
		{
			get{ return m_DNAHits; }
			set{ m_DNAHits = value; }
		}

		[CommandProperty( AccessLevel.Administrator )]
		public int DNAStam
		{
			get{ return m_DNAStam; }
			set{ m_DNAStam = value; }
		}

		[CommandProperty( AccessLevel.Administrator )]
		public int DNAMana
		{
			get{ return m_DNAMana; }
			set{ m_DNAMana = value; }
		}

		[CommandProperty( AccessLevel.Administrator )]
		public int DNAPhysicalResist
		{
			get{ return m_DNAPhysicalResist; }
			set{ m_DNAPhysicalResist = value; }
		}

		[CommandProperty( AccessLevel.Administrator )]
		public int DNAFireResist
		{
			get{ return m_DNAFireResist; }
			set{ m_DNAFireResist = value; }
		}

		[CommandProperty( AccessLevel.Administrator )]
		public int DNAColdResist
		{
			get{ return m_DNAColdResist; }
			set{ m_DNAColdResist = value; }
		}

		[CommandProperty( AccessLevel.Administrator )]
		public int DNAPoisonResist
		{
			get{ return m_DNAPoisonResist; }
			set{ m_DNAPoisonResist = value; }
		}

		[CommandProperty( AccessLevel.Administrator )]
		public int DNAEnergyResist
		{
			get{ return m_DNAEnergyResist; }
			set{ m_DNAEnergyResist = value; }
		}

		[CommandProperty( AccessLevel.Administrator )]
		public int DNADamageMin
		{
			get{ return m_DNADamageMin; }
			set{ m_DNADamageMin = value; }
		}

		[CommandProperty( AccessLevel.Administrator )]
		public int DNADamageMax
		{
			get{ return m_DNADamageMax; }
			set{ m_DNADamageMax = value; }
		}

		[CommandProperty( AccessLevel.Administrator )]
		public int DNAArmor
		{
			get{ return m_DNAArmor; }
			set{ m_DNAArmor = value; }
		}

		[CommandProperty( AccessLevel.Administrator )]
		public Double DNAAnatomy
		{
			get{ return m_DNAAnatomy; }
			set{ m_DNAAnatomy = value; }
		}

		[CommandProperty( AccessLevel.Administrator )]
		public Double DNATactics
		{
			get{ return m_DNATactics; }
			set{ m_DNATactics = value; }
		}

		[CommandProperty( AccessLevel.Administrator )]
		public Double DNAWrestling
		{
			get{ return m_DNAWrestling; }
			set{ m_DNAWrestling = value; }
		}

		[CommandProperty( AccessLevel.Administrator )]
		public Double DNAMagicResist
		{
			get{ return m_DNAMagicResist; }
			set{ m_DNAMagicResist = value; }
		}

		[CommandProperty( AccessLevel.Administrator )]
		public Double DNAPoisoning
		{
			get{ return m_DNAPoisoning; }
			set{ m_DNAPoisoning = value; }
		}

		[CommandProperty( AccessLevel.Administrator )]
		public Double DNAEvalInt
		{
			get{ return m_DNAEvalInt; }
			set{ m_DNAEvalInt = value; }
		}

		[CommandProperty( AccessLevel.Administrator )]
		public Double DNAMagery
		{
			get{ return m_DNAMagery; }
			set{ m_DNAMagery = value; }
		}

		[CommandProperty( AccessLevel.Administrator )]
		public Double DNAMeditation
		{
			get{ return m_DNAMeditation; }
			set{ m_DNAMeditation = value; }
		}

		[CommandProperty( AccessLevel.Administrator )]
		public bool DNABluntAttack
		{
			get{ return m_DNABluntAttack; }
			set{ m_DNABluntAttack = value; }
		}

		[CommandProperty( AccessLevel.Administrator )]
		public bool DNAHealAttack
		{
			get{ return m_DNAHealAttack; }
			set{ m_DNAHealAttack = value; }
		}

		[CommandProperty( AccessLevel.Administrator )]
		public bool DNAPoisonAttack
		{
			get{ return m_DNAPoisonAttack; }
			set{ m_DNAPoisonAttack = value; }
		}

		public bool DNATrialByFire
		{
			get{ return m_DNATrialByFire; }
			set{ m_DNATrialByFire = value; }
		}

		[CommandProperty( AccessLevel.Administrator )]
		public bool DNAIceBlast
		{
			get{ return m_DNAIceBlast; }
			set{ m_DNAIceBlast = value; }
		}

		[CommandProperty( AccessLevel.Administrator )]
		public bool DNACometAttack
		{
			get{ return m_DNACometAttack; }
			set{ m_DNACometAttack = value; }
		}

		[CommandProperty( AccessLevel.Administrator )]
		public bool DNACallOfNature
		{
			get{ return m_DNACallOfNature; }
			set{ m_DNACallOfNature = value; }
		}

		[CommandProperty( AccessLevel.Administrator )]
		public bool DNAAcidRain
		{
			get{ return m_DNAAcidRain; }
			set{ m_DNAAcidRain = value; }
		}

		[CommandProperty( AccessLevel.Administrator )]
		public int DNABodyValue
		{
			get{ return m_DNABodyValue; }
			set{ m_DNABodyValue = value; }
		}

		[CommandProperty( AccessLevel.Administrator )]
		public int DNASoundID
		{
			get{ return m_DNASoundID; }
			set{ m_DNASoundID = value; }
		}

		[CommandProperty( AccessLevel.Administrator )]
		public DNAQuality DNAQuality
		{
			get{ return m_DNAQuality; }
			set{ m_DNAQuality = value; InvalidateProperties(); }
		}

		[CommandProperty( AccessLevel.Administrator )]
		public bool IsFull
		{
			get{ return m_IsFull; }
			set{ m_IsFull = value; InvalidateProperties(); }
		}

		[CommandProperty( AccessLevel.Administrator )]
		public bool Prowess
		{
			get{ return m_Prowess; }
			set{ m_Prowess = value; InvalidateProperties(); }
		}

		public bool Environment
		{
			get{ return m_Environment; }
			set{ m_Environment = value; InvalidateProperties(); }
		}

		[CommandProperty( AccessLevel.Administrator )]
		public bool Mental
		{
			get{ return m_Mental; }
			set{ m_Mental = value; InvalidateProperties(); }
		}

		[CommandProperty( AccessLevel.Administrator )]
		public bool Mimic
		{
			get{ return m_Mimic; }
			set{ m_Mimic = value; InvalidateProperties(); }
		}

		[CommandProperty( AccessLevel.Administrator )]
		public int MountID
		{
			get{ return m_MountID; }
			set{ m_MountID = value; }
		}

		[Constructable]
		public EmptyDNAVialSet( )
		{
			ItemID = 6235;
			Weight = 1.0;
		}

		public override void AddNameProperty(ObjectPropertyList list)
		{
			if ( IsFull == true )
			{
				list.Add( "a full DNA vial set" );
			}
			else if ( Prowess != false || Environment != false || Mental != false || Mimic != false )
			{
				list.Add( "a partially filled DNA set" );
			}
			else
			{
				list.Add( "an empty DNA vial set" );
			}
		}

        	public override void OnDoubleClick( Mobile from )
        	{
			if ( IsChildOf( from.Backpack ) )
			{
				PlayerMobile pm = (PlayerMobile)from;
				if ( from.Skills[SkillName.AnimalTaming].Base < 100.0 && from.Skills[SkillName.Tinkering].Base < 100.0 )
				{
					from.SendMessage( "You have no clue how to use this." );
				}
				else if ( pm.Bioenginer == false )
				{
					from.SendMessage( "You have no clue how to use this." );
				}
				else if ( FSATS.EnableBioEngineer == false )
				{
					from.SendMessage( "Bio-Engineering has been disabled on this server, Please contact your server administrator for more information." );
				}
				else
				{
					from.SendMessage( "Select the DNA vial you wish to add to this set." );
           				from.Target = new AddTarget( this );
				}
			}
			else
			{
				from.SendLocalizedMessage( 1042001 ); // That must be in your pack for you to use it.
			}
		}

		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );
			if ( DNAQuality != DNAQuality.None && IsFull == true )
			{
				if ( DNAQuality == DNAQuality.VeryHigh )
				{
					list.Add( 1060663, "Overall Quality\tVery High" );
				}
				else if ( DNAQuality == DNAQuality.High )
				{
					list.Add( 1060663, "Overall Quality\tHigh" );
				}
				else if ( DNAQuality == DNAQuality.AboveAverage )
				{
					list.Add( 1060663, "Overall Quality\tAbove Average" );
				}
				else if ( DNAQuality == DNAQuality.Average )
				{
					list.Add( 1060663, "Overall Quality\tAverage" );
				}
				else if ( DNAQuality == DNAQuality.BelowAverage )
				{
					list.Add( 1060663, "Overall Quality\tBelow Average" );
				}
				else if ( DNAQuality == DNAQuality.Low )
				{
					list.Add( 1060663, "Overall Quality\tLow" );
				}
				else if ( DNAQuality == DNAQuality.VeryLow )
				{
					list.Add( 1060663, "Overall Quality\tVery Low" );
				}

				if ( DNATrialByFire != false || DNAIceBlast != false || DNACometAttack != false || DNABluntAttack != false || DNAHealAttack != false || DNAPoisonAttack != false )
				{
					if ( DNABluntAttack == true )
					{
						list.Add( 1060658, "Counter Attack\tBlunt Attack" );
					}
					else if ( DNAHealAttack == true )
					{
						list.Add( 1060658, "Counter Attack\tCounter Healing" );
					}
					else if ( DNAPoisonAttack == true )
					{
						list.Add( 1060658, "Counter Attack\tPoison Attack" );
					}
					else
					{
						list.Add( 1060658, "Counter Attack\tNone" );
					}

					if ( DNATrialByFire == true )
					{
						list.Add( 1060659, "Special Attack\tTrial By Fire" );
					}
					else if ( DNAIceBlast == true )
					{
						list.Add( 1060659, "Special Attack\tIce Blast" );
					}
					else if ( DNACometAttack == true )
					{
						list.Add( 1060659, "Special Attack\tComet Attack" );
					}
					else if ( DNACallOfNature == true )
					{
						list.Add( 1060659, "Special Attack\tCall Of Nature" );
					}
					else if ( DNAAcidRain == true )
					{
						list.Add( 1060659, "Special Attack\tAcid Rain" );
					}
					else
					{
						list.Add( 1060659, "Special Attack\tNone" );
					}
				}
			}
			else
			{
				if ( Prowess != false )
				{
					list.Add( 1060658, "Prowess DNA\t1" );
				}
				else
				{
					list.Add( 1060658, "Prowess DNA\t0" );
				}

				if ( Environment != false )
				{
					list.Add( 1060659, "Environment DNA\t1" );
				}
				else
				{
					list.Add( 1060659, "Environment DNA\t0" );
				}

				if ( Mental != false )
				{
					list.Add( 1060660, "Mental DNA\t1" );
				}
				else
				{
					list.Add( 1060660, "Mental DNA\t0" );
				}

				if ( Mimic != false )
				{
					list.Add( 1060661, "Minic DNA\t1" );
				}
				else
				{
					list.Add( 1060661, "Minic DNA\t0" );
				}
			}
		}

		public EmptyDNAVialSet( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 3 ); // version 

			writer.Write( m_MountID );

			writer.Write( m_DNAAcidRain );

			writer.Write( m_DNACallOfNature );

			writer.Write( m_IsFull );

			writer.Write( m_Prowess );

			writer.Write( m_Environment );

			writer.Write( m_Mental );

			writer.Write( m_Mimic );

			writer.Write( m_DNAMinTame );

			writer.Write( m_DNAControlSlots );

			writer.Write( (int) m_DNAQuality );

			writer.Write( m_DNAStr );

			writer.Write( m_DNADex );

			writer.Write( m_DNAInt );

			writer.Write( m_DNAHits );

			writer.Write( m_DNAStam );

			writer.Write( m_DNAMana );

			writer.Write( m_DNAFireResist );

			writer.Write( m_DNAColdResist );

			writer.Write( m_DNAEnergyResist );

			writer.Write( m_DNAPoisonResist );

			writer.Write( m_DNAPhysicalResist );

			writer.Write( m_DNADamageMin );

			writer.Write( m_DNADamageMax );

			writer.Write( m_DNAArmor );

			writer.Write( m_DNAMagery );

			writer.Write( m_DNAEvalInt );

			writer.Write( m_DNAMeditation );

			writer.Write( m_DNAMagicResist );

			writer.Write( m_DNAPoisoning );

			writer.Write( m_DNAAnatomy );

			writer.Write( m_DNATactics );

			writer.Write( m_DNAWrestling );

			writer.Write( m_DNABluntAttack );

			writer.Write( m_DNAHealAttack );

			writer.Write( m_DNAPoisonAttack );

			writer.Write( m_DNATrialByFire );

			writer.Write( m_DNAIceBlast );

			writer.Write( m_DNACometAttack );

			writer.Write( m_DNABodyValue );

			writer.Write( m_DNASoundID );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();

			switch ( version )
			{
				case 3:
				{
					m_MountID = reader.ReadInt();
					goto case 2;
				}
				case 2:
				{
					m_DNAAcidRain = reader.ReadBool();
					goto case 1;
				}
				case 1:
				{
					m_DNACallOfNature = reader.ReadBool();
					goto case 0;
				}
				case 0:
				{
					m_IsFull = reader.ReadBool();
					m_Prowess = reader.ReadBool();
					m_Environment = reader.ReadBool();
					m_Mental = reader.ReadBool();
					m_Mimic = reader.ReadBool();
					m_DNAMinTame = reader.ReadDouble();
					m_DNAControlSlots = reader.ReadInt();
					m_DNAQuality = (DNAQuality)reader.ReadInt();
					m_DNAStr = reader.ReadInt();
					m_DNADex = reader.ReadInt();
					m_DNAInt = reader.ReadInt();
					m_DNAHits = reader.ReadInt();
					m_DNAStam = reader.ReadInt();
					m_DNAMana = reader.ReadInt();
					m_DNAFireResist = reader.ReadInt();
					m_DNAColdResist = reader.ReadInt();
					m_DNAEnergyResist = reader.ReadInt();
					m_DNAPoisonResist = reader.ReadInt();
					m_DNAPhysicalResist = reader.ReadInt();
					m_DNADamageMin = reader.ReadInt();
					m_DNADamageMax = reader.ReadInt();
					m_DNAArmor = reader.ReadInt();
					m_DNAMagery = reader.ReadDouble();
					m_DNAEvalInt = reader.ReadDouble();
					m_DNAMeditation = reader.ReadDouble();
					m_DNAMagicResist = reader.ReadDouble();
					m_DNAPoisoning = reader.ReadDouble();
					m_DNAAnatomy = reader.ReadDouble();
					m_DNATactics = reader.ReadDouble();
					m_DNAWrestling = reader.ReadDouble();
					m_DNABluntAttack = reader.ReadBool();
					m_DNAHealAttack = reader.ReadBool();
					m_DNAPoisonAttack = reader.ReadBool();
					m_DNATrialByFire = reader.ReadBool();
					m_DNAIceBlast = reader.ReadBool();
					m_DNACometAttack = reader.ReadBool();
					m_DNABodyValue = reader.ReadInt();
					m_DNASoundID = reader.ReadInt();

					break;
				}
			}
		}
	}

  	public class AddTarget : Target 
      	{
		private EmptyDNAVialSet m_VialSet;

         	public AddTarget( EmptyDNAVialSet vialset ) : base ( 10, false, TargetFlags.None ) 
         	{ 
			m_VialSet = vialset;
         	} 
          
         	protected override void OnTarget( Mobile from, object target ) 
         	{
			if ( target is DNAItem )
			{
				DNAItem dna = (DNAItem)target;
				if ( dna.DNAType != DNAType.None )
				{
					if ( dna.DNAType == DNAType.Prowess && m_VialSet.Prowess == false )
					{
						m_VialSet.Prowess = true;
						from.PlaySound( 0x240 );
						from.SendMessage( "The DNA was successfully added to the DNA Set" );

						m_VialSet.DNAStr = dna.DNAStr;
						m_VialSet.DNAHits = dna.DNAHits;
						m_VialSet.DNAPhysicalResist = dna.DNAPhysicalResist;
						m_VialSet.DNADamageMin = dna.DNADamageMin;
						m_VialSet.DNADamageMax = dna.DNADamageMax;
						m_VialSet.DNAArmor = dna.DNAArmor;

						m_VialSet.DNAAnatomy = dna.DNAAnatomy;
						m_VialSet.DNATactics = dna.DNATactics;
						m_VialSet.DNAWrestling = dna.DNAWrestling;
					
						dna.Delete();
					}
					else if ( dna.DNAType == DNAType.Environment && m_VialSet.Environment == false )
					{
						m_VialSet.Environment = true;
						from.PlaySound( 0x240 );
						from.SendMessage( "The DNA was successfully added to the DNA Set" );

						m_VialSet.DNADex = dna.DNADex;
						m_VialSet.DNAStam = dna.DNAStam;
						m_VialSet.DNAFireResist = dna.DNAFireResist;
						m_VialSet.DNAColdResist = dna.DNAColdResist;

						m_VialSet.DNAPoisoning = dna.DNAPoisoning;
						m_VialSet.DNAMagicResist = dna.DNAMagicResist;

						dna.Delete();
					}
					else if ( dna.DNAType == DNAType.Mental && m_VialSet.Mental == false )
					{
						m_VialSet.Mental = true;
						from.PlaySound( 0x240 );
						from.SendMessage( "The DNA was successfully added to the DNA Set" );

						m_VialSet.DNAInt = dna.DNAInt;
						m_VialSet.DNAMana = dna.DNAMana;
						m_VialSet.DNAEnergyResist = dna.DNAEnergyResist;
						m_VialSet.DNAPoisonResist = dna.DNAPoisonResist;

						m_VialSet.DNAMagery = dna.DNAMagery;
						m_VialSet.DNAEvalInt = dna.DNAEvalInt;
						m_VialSet.DNAMeditation = dna.DNAMeditation;

						dna.Delete();
					}
					else if ( dna.DNAType == DNAType.Mimic && m_VialSet.Mimic == false )
					{
						m_VialSet.Mimic = true;
						from.PlaySound( 0x240 );
						from.SendMessage( "The DNA was successfully added to the DNA Set" );

						m_VialSet.DNABodyValue = dna.DNABodyValue;
						m_VialSet.DNASoundID = dna.DNASoundID;

						m_VialSet.DNABluntAttack = dna.DNABluntAttack;
						m_VialSet.DNAHealAttack = dna.DNAHealAttack;
						m_VialSet.DNAPoisonAttack = dna.DNAPoisonAttack;

						m_VialSet.DNATrialByFire = dna.DNATrialByFire;
						m_VialSet.DNAIceBlast = dna.DNAIceBlast;
						m_VialSet.DNACometAttack = dna.DNACometAttack;
						m_VialSet.DNACallOfNature = dna.DNACallOfNature;
						m_VialSet.DNAAcidRain = dna.DNAAcidRain;

						m_VialSet.MountID = dna.MountID;

						dna.Delete();
					}
					else
					{
						from.SendMessage( "This DNA Set already has this type of DNA." );
					}


					if ( m_VialSet.Prowess == true && m_VialSet.Environment == true && m_VialSet.Mental == true && m_VialSet.Mimic == true )
					{
						m_VialSet.ItemID = 6237;
						from.SendMessage( "You have filled this DNA vial set." );

						m_VialSet.Prowess = false;
						m_VialSet.Environment = false;
						m_VialSet.Mental = false;
						m_VialSet.Mimic = false;

						int qua = m_VialSet.DNAStr + m_VialSet.DNADex + m_VialSet.DNAInt + m_VialSet.DNAHits + m_VialSet.DNAStam + m_VialSet.DNAMana + m_VialSet.DNAPhysicalResist + m_VialSet.DNAFireResist + m_VialSet.DNAColdResist + m_VialSet.DNAEnergyResist + m_VialSet.DNAPoisonResist + m_VialSet.DNADamageMin + m_VialSet.DNADamageMax + m_VialSet.DNAArmor;

						if ( qua <= 250 )
						{
							m_VialSet.DNAQuality = DNAQuality.VeryLow;
							m_VialSet.DNAMinTame = 0;
							m_VialSet.DNAControlSlots = 3;
						}
						else if ( qua <= 500 )
						{
							m_VialSet.DNAQuality = DNAQuality.Low;
							m_VialSet.DNAMinTame = 0;
							m_VialSet.DNAControlSlots = 3;
						}
						else if ( qua <= 1250 )
						{
							m_VialSet.DNAQuality = DNAQuality.BelowAverage;
							m_VialSet.DNAMinTame = 0;
							m_VialSet.DNAControlSlots = 3;
						}
						else if ( qua <= 2500 )
						{
							m_VialSet.DNAQuality = DNAQuality.Average;
							m_VialSet.DNAMinTame = Utility.RandomMinMax( 0, 5 );
							m_VialSet.DNAControlSlots = 3;
						}
						else if ( qua <= 3500 )
						{
							m_VialSet.DNAQuality = DNAQuality.AboveAverage;
							m_VialSet.DNAMinTame = Utility.RandomMinMax( 0, 25 );
							m_VialSet.DNAControlSlots = 3;
						}
						else if ( qua <= 4500 )
						{
							m_VialSet.DNAQuality = DNAQuality.High;
							m_VialSet.DNAMinTame = Utility.RandomMinMax( 0, 50 );
							m_VialSet.DNAControlSlots = 3;
						}
						else if ( qua <= 5000000 )
						{
							m_VialSet.DNAQuality = DNAQuality.VeryHigh;
							m_VialSet.DNAMinTame = Utility.RandomMinMax( 0, 100 );
							m_VialSet.DNAControlSlots = 3;
						}

						//Cap Skills if over 100
						if ( m_VialSet.DNAMagery >= 100.0 )
							m_VialSet.DNAMagery = 100.0;

						if ( m_VialSet.DNAEvalInt >= 100.0 )
							m_VialSet.DNAEvalInt = 100.0;

						if ( m_VialSet.DNAMeditation >= 100.0 )
							m_VialSet.DNAMeditation = 100.0;

						if ( m_VialSet.DNAPoisoning >= 100.0 )
							m_VialSet.DNAPoisoning = 100.0;

						if ( m_VialSet.DNAMagicResist >= 100.0 )
							m_VialSet.DNAMagicResist = 100.0;

						if ( m_VialSet.DNAWrestling >= 100.0 )
							m_VialSet.DNAWrestling = 100.0;

						if ( m_VialSet.DNAAnatomy >= 100.0 )
							m_VialSet.DNAAnatomy = 100.0;

						if ( m_VialSet.DNATactics >= 100.0 )
							m_VialSet.DNATactics = 100.0;

						if ( m_VialSet.DNAStr >= FSATS.BioSTR )
							m_VialSet.DNAStr = FSATS.BioSTR;

						if ( m_VialSet.DNADex >= FSATS.BioDEX )
							m_VialSet.DNADex = FSATS.BioDEX;

						if ( m_VialSet.DNAInt >= FSATS.BioINT )
							m_VialSet.DNAInt = FSATS.BioINT;

						if ( m_VialSet.DNAHits >= FSATS.BioHITS )
							m_VialSet.DNAHits = FSATS.BioHITS;

						if ( m_VialSet.DNAStam >= FSATS.BioSTAM )
							m_VialSet.DNAStam = FSATS.BioSTAM;

						if ( m_VialSet.DNAMana >= FSATS.BioMANA )
							m_VialSet.DNAMana = FSATS.BioMANA;

						if ( m_VialSet.DNAFireResist >= 0 && m_VialSet.DNABodyValue == 400 ||m_VialSet.DNABodyValue == 400 )
							m_VialSet.DNAFireResist = 0;
						else if ( m_VialSet.DNAFireResist >= FSATS.BioFire )
							m_VialSet.DNAFireResist = FSATS.BioFire;

						if ( m_VialSet.DNAColdResist >= 0 && m_VialSet.DNABodyValue == 400 || m_VialSet.DNABodyValue == 400 )
							m_VialSet.DNAColdResist = 0;
						else if ( m_VialSet.DNAColdResist >= FSATS.BioCold )
							m_VialSet.DNAColdResist = FSATS.BioCold;

						if ( m_VialSet.DNAEnergyResist >= 0 && m_VialSet.DNABodyValue == 400 || m_VialSet.DNABodyValue == 400 )
							m_VialSet.DNAEnergyResist = 0;
						else if ( m_VialSet.DNAEnergyResist >= FSATS.BioEnergy )
							m_VialSet.DNAEnergyResist = FSATS.BioEnergy;

						if ( m_VialSet.DNAPoisonResist >= 0 && m_VialSet.DNABodyValue == 400 || m_VialSet.DNABodyValue == 400 )
							m_VialSet.DNAPoisonResist = 0;
						else if ( m_VialSet.DNAPoisonResist >= FSATS.BioPoison )
							m_VialSet.DNAPoisonResist = FSATS.BioPoison;

						if ( m_VialSet.DNAPhysicalResist >= 0 && m_VialSet.DNABodyValue == 400 || m_VialSet.DNABodyValue == 400 )
							m_VialSet.DNAPhysicalResist = 0;
						else if ( m_VialSet.DNAPhysicalResist >= FSATS.BioPhys )
							m_VialSet.DNAPhysicalResist = FSATS.BioPhys;

						if ( m_VialSet.DNADamageMin >= FSATS.BioMinDam )
							m_VialSet.DNADamageMin = FSATS.BioMinDam;

						if ( m_VialSet.DNADamageMax >= FSATS.BioMaxDam )
							m_VialSet.DNADamageMax = FSATS.BioMaxDam;

						if ( m_VialSet.DNAArmor >= FSATS.BioVArmor )
							m_VialSet.DNAArmor = FSATS.BioVArmor;

						m_VialSet.IsFull = true;

						if ( m_VialSet.IsFull == true )
						{
							BioPetItem bio = new BioPetItem();

							bio.DNAStr = m_VialSet.DNAStr;
							bio.DNADex = m_VialSet.DNADex;
							bio.DNAInt = m_VialSet.DNAInt;

							bio.DNAHits = m_VialSet.DNAHits;
							bio.DNAStam = m_VialSet.DNAStam;
							bio.DNAMana = m_VialSet.DNAMana;

							bio.DNADamageMin = m_VialSet.DNADamageMin;
							bio.DNADamageMax = m_VialSet.DNADamageMax;

							bio.DNAArmor = m_VialSet.DNAArmor;

							bio.DNAFireResist = m_VialSet.DNAFireResist;
							bio.DNAColdResist = m_VialSet.DNAColdResist;
							bio.DNAEnergyResist = m_VialSet.DNAEnergyResist;
							bio.DNAPoisonResist = m_VialSet.DNAPoisonResist;
							bio.DNAPhysicalResist = m_VialSet.DNAPhysicalResist;

							bio.DNAMagery = m_VialSet.DNAMagery;
							bio.DNAEvalInt = m_VialSet.DNAEvalInt;
							bio.DNAMeditation = m_VialSet.DNAMeditation;
							bio.DNAMagicResist = m_VialSet.DNAMagicResist;
							bio.DNAPoisoning = m_VialSet.DNAPoisoning;
							bio.DNAAnatomy = m_VialSet.DNAAnatomy;
							bio.DNATactics = m_VialSet.DNATactics;
							bio.DNAWrestling = m_VialSet.DNAWrestling;

							bio.DNABodyValue = m_VialSet.DNABodyValue;
							bio.DNASoundID = m_VialSet.DNASoundID;

							bio.DNABluntAttack = m_VialSet.DNABluntAttack;
							bio.DNAHealAttack = m_VialSet.DNAHealAttack;
							bio.DNAPoisonAttack = m_VialSet.DNAPoisonAttack;

							bio.DNATrialByFire = m_VialSet.DNATrialByFire;
							bio.DNAIceBlast = m_VialSet.DNAIceBlast;
							bio.DNACometAttack = m_VialSet.DNACometAttack;
							bio.DNACallOfNature = m_VialSet.DNACallOfNature;
							bio.DNAAcidRain = m_VialSet.DNAAcidRain;

							bio.DNAControlSlots = 5;
							bio.DNAMinTame = 120.0;

							bio.MountID = m_VialSet.MountID;

							bio.ItemID = ShrinkTable.Lookup( m_VialSet.DNABodyValue );

							bio.Visible = false;
							from.AddToBackpack( bio );
							m_VialSet.Delete();

							from.SendGump( new BioExpGump( -1, from, bio, 0, 0, 0, 0, 0 ) );
							
						}
					}
				}
			}
			else
			{
				from.SendMessage( "That cannot be added to this DNA set." );
			}
		}
	}
}
