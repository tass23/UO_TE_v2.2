using System;
using Server;
using Server.Mobiles;
using Server.Items;

namespace Server.Items
{
	public enum DNAType
	{
		None,
		Prowess,
		Environment,
		Mental,
		Mimic
	}

	public enum DNAQuality
	{
		None,
		VeryHigh,
		High,
		AboveAverage,
		Average,
		BelowAverage,
		Low,
		VeryLow
	}

	public class DNAItem : Item
	{
		private DNAType m_DNAType;
		private DNAQuality m_DNAQuality;
		private string m_DNAName;

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
		private bool m_DNAAcidRain; 		//Mimic

		private int m_DNABodyValue; 		//Mimic
		private int m_DNASoundID; 		//Mimic
		private int m_MountID;			//Minic

		[CommandProperty( AccessLevel.Administrator )]
		public DNAType DNAType
		{
			get{ return m_DNAType; }
			set{ m_DNAType = value; InvalidateProperties(); }
		}

		[CommandProperty( AccessLevel.Administrator )]
		public DNAQuality DNAQuality
		{
			get{ return m_DNAQuality; }
			set{ m_DNAQuality = value; InvalidateProperties(); }
		}

		[CommandProperty( AccessLevel.Administrator )]
		public string DNAName
		{
			get{ return m_DNAName; }
			set{ m_DNAName = value; }
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

		[CommandProperty( AccessLevel.Administrator )]
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
		public int MountID
		{
			get{ return m_MountID; }
			set{ m_MountID = value; }
		}

		[Constructable]
		public DNAItem( )
		{
			ItemID = Utility.RandomList( 3965, 3970 );
			Weight = 1.0;
		}

		public override void AddNameProperty(ObjectPropertyList list)
		{
			if ( DNAName != null )
			{
				list.Add( "a vial of DNA from {0}", DNAName );
			}
			else
			{
				list.Add( "a DNA vial" );
			}
		}
			
		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );
			if ( DNAType != DNAType.None )
			{
				if ( DNAType == DNAType.Prowess )
				{
					list.Add( 1060662, "DNA Type\tProwess" );
				}
				else if ( DNAType == DNAType.Environment )
				{
					list.Add( 1060662, "DNA Type\tEnvironment" );
				}
				else if ( DNAType == DNAType.Mental )
				{
					list.Add( 1060662, "DNA Type\tMental" );
				}
				else if ( DNAType == DNAType.Mimic )
				{
					list.Add( 1060662, "DNA Type\tMimic" );
				}
			}

			if ( DNAQuality != DNAQuality.None )
			{
				if ( DNAQuality == DNAQuality.VeryHigh )
				{
					list.Add( 1060663, "DNA Quality\tVery High" );
				}
				else if ( DNAQuality == DNAQuality.High )
				{
					list.Add( 1060663, "DNA Quality\tHigh" );
				}
				else if ( DNAQuality == DNAQuality.AboveAverage )
				{
					list.Add( 1060663, "DNA Quality\tAbove Average" );
				}
				else if ( DNAQuality == DNAQuality.Average )
				{
					list.Add( 1060663, "DNA Quality\tAverage" );
				}
				else if ( DNAQuality == DNAQuality.BelowAverage )
				{
					list.Add( 1060663, "DNA Quality\tBelow Average" );
				}
				else if ( DNAQuality == DNAQuality.Low )
				{
					list.Add( 1060663, "DNA Quality\tLow" );
				}
				else if ( DNAQuality == DNAQuality.VeryLow )
				{
					list.Add( 1060663, "DNA Quality\tVery Low" );
				}
			}
			else if ( DNAType == DNAType.Mimic && DNAQuality == DNAQuality.None )
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

		public DNAItem( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 3 ); // version

			writer.Write( m_MountID );

			writer.Write( m_DNAAcidRain );

			writer.Write( m_DNACallOfNature );

			writer.Write( (int) m_DNAType );

			writer.Write( (int) m_DNAQuality );

			writer.Write( m_DNAName );

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
					m_DNAType = (DNAType)reader.ReadInt();
					m_DNAQuality = (DNAQuality)reader.ReadInt();
					m_DNAName = reader.ReadString();
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
}
