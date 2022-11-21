using System;
using Server.Items;
using Server.Mobiles;
using Server.Network;
using Server.Factions;
using System.Collections;

namespace Server.Items
{
   	public class BioPetItem : Item
   	{
		private Double m_DNAMinTame;
		private int m_DNAControlSlots;

		private int m_DNAStr;
		private int m_DNADex;
		private int m_DNAInt;
		private int m_DNAHits;
		private int m_DNAStam;
		private int m_DNAMana;

		private int m_DNAFireResist;
		private int m_DNAColdResist;
		private int m_DNAEnergyResist;
		private int m_DNAPoisonResist;
		private int m_DNAPhysicalResist;

		private int m_DNADamageMin;
		private int m_DNADamageMax;

		private int m_DNAArmor;

		private Double m_DNAMagery;
		private Double m_DNAEvalInt;
		private Double m_DNAMeditation;
		private Double m_DNAMagicResist;
		private Double m_DNAPoisoning;
		private Double m_DNAAnatomy;
		private Double m_DNATactics;
		private Double m_DNAWrestling;

		private bool m_DNABluntAttack;
		private bool m_DNAHealAttack;
		private bool m_DNAPoisonAttack;

		private bool m_DNATrialByFire;
		private bool m_DNAIceBlast;
		private bool m_DNACometAttack;
		private bool m_DNACallOfNature;
		private bool m_DNAAcidRain;

		private int m_DNABodyValue;
		private int m_DNASoundID;
		private int m_MountID;

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
		public int MountID
		{
			get{ return m_MountID; }
			set{ m_MountID = value; }
		}

      		[Constructable]
      		public BioPetItem() : base()
      		{
			Name = "a bio-engineered pet deed";
			ItemID = 100;
      		}

      		public override void OnDoubleClick( Mobile from )
      		{
			if ( MountID > 0 )
			{
				BioMount bio = new BioMount();

				bio.Str = this.DNAStr;
				bio.Dex = this.DNADex;
				bio.Int = this.DNAInt;

				bio.HitsMaxSeed = this.DNAHits;
				bio.StamMaxSeed = this.DNAStam;
				bio.ManaMaxSeed = this.DNAMana;

				bio.DamageMin = this.DNADamageMin;
				bio.DamageMax = this.DNADamageMax;

				bio.VirtualArmor = this.DNAArmor;

				bio.FireResistSeed = this.DNAFireResist;
				bio.ColdResistSeed = this.DNAColdResist;
				bio.EnergyResistSeed = this.DNAEnergyResist;
				bio.PoisonResistSeed = this.DNAPoisonResist;
				bio.PhysicalResistanceSeed = this.DNAPhysicalResist;

				bio.Skills[SkillName.Magery].Base = this.DNAMagery;
				bio.Skills[SkillName.EvalInt].Base = this.DNAEvalInt;
				bio.Skills[SkillName.Meditation].Base = this.DNAMeditation;
				bio.Skills[SkillName.MagicResist].Base = this.DNAMagicResist;
				bio.Skills[SkillName.Poisoning].Base = this.DNAPoisoning;
				bio.Skills[SkillName.Anatomy].Base = this.DNAAnatomy;
				bio.Skills[SkillName.Tactics].Base = this.DNATactics;
				bio.Skills[SkillName.Wrestling].Base = this.DNAWrestling;

				bio.BodyValue = this.DNABodyValue;
				bio.BaseSoundID = this.DNASoundID;

				bio.BluntAttack = this.DNABluntAttack;
				bio.Healing = this.DNAHealAttack;
				bio.PoisonAttack = this.DNAPoisonAttack;

				bio.TrialByFire = this.DNATrialByFire;
				bio.IceBlast = this.DNAIceBlast;
				bio.CometAttack = this.DNACometAttack;
				bio.CallOfNature = this.DNACallOfNature;
				bio.AcidRain = this.DNAAcidRain;

				bio.ControlSlots = this.DNAControlSlots;
				bio.MinTameSkill = this.DNAMinTame;

				bio.ControlMaster = from;
				bio.Controlled = true;

				bio.ItemID = MountID;

				IEntity p1 = new Entity( Serial.Zero, new Point3D( from.X, from.Y, from.Z ), from.Map );
				IEntity p2 = new Entity( Serial.Zero, new Point3D( from.X, from.Y, from.Z + 50 ), from.Map );

				Effects.SendMovingParticles( p1, p2, ShrinkTable.Lookup( DNABodyValue ), 1, 0, true, false, 0, 3, 1153, 1, 0, EffectLayer.Head, 0x100 );
				from.PlaySound( 492 );

				bio.MoveToWorld( from.Location, from.Map );
				this.Delete();
			}
			else
			{
				BioCreature bio = new BioCreature();

				bio.Str = this.DNAStr;
				bio.Dex = this.DNADex;
				bio.Int = this.DNAInt;

				bio.HitsMaxSeed = this.DNAHits;
				bio.StamMaxSeed = this.DNAStam;
				bio.ManaMaxSeed = this.DNAMana;

				bio.DamageMin = this.DNADamageMin;
				bio.DamageMax = this.DNADamageMax;

				bio.VirtualArmor = this.DNAArmor;

				bio.FireResistSeed = this.DNAFireResist;
				bio.ColdResistSeed = this.DNAColdResist;
				bio.EnergyResistSeed = this.DNAEnergyResist;
				bio.PoisonResistSeed = this.DNAPoisonResist;
				bio.PhysicalResistanceSeed = this.DNAPhysicalResist;

				bio.Skills[SkillName.Magery].Base = this.DNAMagery;
				bio.Skills[SkillName.EvalInt].Base = this.DNAEvalInt;
				bio.Skills[SkillName.Meditation].Base = this.DNAMeditation;
				bio.Skills[SkillName.MagicResist].Base = this.DNAMagicResist;
				bio.Skills[SkillName.Poisoning].Base = this.DNAPoisoning;
				bio.Skills[SkillName.Anatomy].Base = this.DNAAnatomy;
				bio.Skills[SkillName.Tactics].Base = this.DNATactics;
				bio.Skills[SkillName.Wrestling].Base = this.DNAWrestling;

				bio.BodyValue = this.DNABodyValue;
				bio.BaseSoundID = this.DNASoundID;

				bio.BluntAttack = this.DNABluntAttack;
				bio.Healing = this.DNAHealAttack;
				bio.PoisonAttack = this.DNAPoisonAttack;

				bio.TrialByFire = this.DNATrialByFire;
				bio.IceBlast = this.DNAIceBlast;
				bio.CometAttack = this.DNACometAttack;
				bio.CallOfNature = this.DNACallOfNature;
				bio.AcidRain = this.DNAAcidRain;

				bio.ControlSlots = this.DNAControlSlots;
				bio.MinTameSkill = this.DNAMinTame;

				bio.ControlMaster = from;
				bio.Controlled = true;

				IEntity p1 = new Entity( Serial.Zero, new Point3D( from.X, from.Y, from.Z ), from.Map );
				IEntity p2 = new Entity( Serial.Zero, new Point3D( from.X, from.Y, from.Z + 50 ), from.Map );

				Effects.SendMovingParticles( p1, p2, ShrinkTable.Lookup( DNABodyValue ), 1, 0, true, false, 0, 3, 1153, 1, 0, EffectLayer.Head, 0x100 );
				from.PlaySound( 492 );

				bio.MoveToWorld( from.Location, from.Map );
				this.Delete();
			}
      		}

		public override void AddNameProperties( ObjectPropertyList list )
		{
			base.AddNameProperties( list );

			list.Add( 1060660, "Stats\tStrength {0}, Dexterity {1}, Intelligence {2}", DNAStr, DNADex, DNAInt );
			list.Add( 1060661, "Stats\tHits {0}, Stam {1}, Mana {2}", DNAHits, DNAStam, DNAMana );

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

      		public BioPetItem( Serial serial ) : base( serial )
      		{
      		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 1 ); // version 

			writer.Write( m_MountID );

			writer.Write( m_DNAAcidRain );

			writer.Write( m_DNACallOfNature );

			writer.Write( m_DNAMinTame );

			writer.Write( m_DNAControlSlots );

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
				case 1:
				{
					m_MountID = reader.ReadInt();
					goto case 0;
				}
				case 0:
				{
					m_DNAAcidRain = reader.ReadBool();
					m_DNACallOfNature = reader.ReadBool();
					m_DNAMinTame = reader.ReadDouble();
					m_DNAControlSlots = reader.ReadInt();
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
