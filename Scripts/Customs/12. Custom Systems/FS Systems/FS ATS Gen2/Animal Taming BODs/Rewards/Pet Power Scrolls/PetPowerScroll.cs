using Server.Targeting; 
using System; 
using Server; 
using Server.Gumps; 
using Server.Network; 
using Server.Menus; 
using Server.Menus.Questions; 
using Server.Mobiles; 
using System.Collections; 

namespace Server.Items 
{ 
   	public class PetPowerScroll : Item 
   	{ 

		private SkillName m_Skill;
		private double m_Value;

		private static SkillName[] m_Skills = new SkillName[]
			{
				SkillName.Magery,
				SkillName.EvalInt,
				SkillName.Meditation,
				SkillName.MagicResist,
				SkillName.Tactics,
				SkillName.Wrestling,
				SkillName.Anatomy
			};

		public static SkillName[] Skills{ get{ return ( m_Skills ); } }

		[CommandProperty( AccessLevel.GameMaster )]
		public SkillName Skill
		{
			get
			{
				return m_Skill;
			}
			set
			{
				m_Skill = value;
			}
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public double Value
		{
			get
			{
				return m_Value;
			}
			set
			{
				m_Value = value;
			}
		}

		public static PetPowerScroll CreateRandom( int min, int max )
		{
			min /= 5;
			max /= 5;

			//SkillName[] skills = PowerScroll.Skills;

			//return new PetPowerScroll( skills[Utility.Random( skills.Length )], 100 + (Utility.RandomMinMax( min, max ) * 5));
			return new PetPowerScroll( m_Skills[ Utility.Random( m_Skills.Length ) ], 100 + (Utility.RandomMinMax( min, max ) * 5));
		}
    
      		[Constructable] 
      		public PetPowerScroll( SkillName skill, double value ) : base( 0x14F0 ) 
      		{ 
			base.Hue = 0x481;
			base.Weight = 1.0;

			LootType = LootType.Cursed;
			Name = "a pet power scroll";

			m_Skill = skill;
			m_Value = value; 
      		} 

		public override void GetProperties(ObjectPropertyList list)
		{
			base.GetProperties (list);

			list.Add( 1060663, "{0}\t{1}", m_Skill, m_Value );
		}

      		public PetPowerScroll( Serial serial ) : base( serial ) 
      		{ 
      		} 
      		public override void OnDoubleClick( Mobile from ) 
      		{ 

			if ( !IsChildOf( from.Backpack ) )
			{
				from.SendLocalizedMessage( 1042001 ); // That must be in your pack for you to use it.
			}
			else if( from.InRange( this.GetWorldLocation(), 1 ) ) 
			{

        			from.SendMessage( "What pet would you like to use this on?" ); 
           			from.Target = new PSTarget( this, m_Skill, m_Value );

			} 
			else 
			{ 
				from.SendLocalizedMessage( 500446 ); // That is too far away. 
			}

      		} 

      		public override void Serialize( GenericWriter writer ) 
      		{ 
         		base.Serialize( writer ); 

         		writer.Write( (int) 0 ); 

			writer.Write( (int) m_Skill );
			writer.Write( (double) m_Value );

      		} 

      		public override void Deserialize( GenericReader reader ) 
      		{ 
         		base.Deserialize( reader ); 

         		int version = reader.ReadInt(); 

			switch ( version )
			{

				case 0:
				{
					m_Skill = (SkillName)reader.ReadInt();
					m_Value = reader.ReadDouble();
					break;
				}

			}
      		} 


  		private class PSTarget : Target 
      		{ 
         		private Mobile m_Owner; 
      
         		private PetPowerScroll m_Powder; 
			private SkillName m_Skill;
			private double m_Value;

         		public PSTarget( PetPowerScroll charge, SkillName skill, double value ) : base ( 10, false, TargetFlags.None ) 
         		{ 
            			m_Powder = charge;
            			m_Skill = skill;
            			m_Value = value; 
         		} 
          
         		protected override void OnTarget( Mobile from, object target ) 
         		{ 

            			if( target == from ) 
               				from.SendMessage( "You cant do that." );
            
          			else if( target is BaseCreature ) 
          			{ 
            
          				BaseCreature c = (BaseCreature)target;
					if( c.Controlled == false )
					{
						from.SendMessage( "That Creature is not tamed." );
					}	
					else if( c.ControlMaster != from )
					{
						from.SendMessage( "This is not your pet." );
					}

					else if ( c.Controlled == true && c.ControlMaster == from)
					{
						Skill skill = c.Skills[m_Skill];

						if ( skill != null )
						{
							if ( skill.Cap >= m_Value )
							{
								from.SendMessage( "Your pets {0} is to high for this powerscroll.", m_Skill );
							}
							else
							{
								from.SendMessage( "Your pets {0} has been caped at {1}.", m_Skill, m_Value );

								skill.Cap = m_Value;

								Effects.SendLocationParticles( EffectItem.Create( c.Location, c.Map, EffectItem.DefaultDuration ), 0, 0, 0, 0, 0, 5060, 0 );
								Effects.PlaySound( from.Location, from.Map, 0x243 );

								Effects.SendMovingParticles( new Entity( Serial.Zero, new Point3D( c.X - 6, from.Y - 6, c.Z + 15 ), c.Map ), c, 0x36D4, 7, 0, false, true, 0x497, 0, 9502, 1, 0, (EffectLayer)255, 0x100 );
								Effects.SendMovingParticles( new Entity( Serial.Zero, new Point3D( c.X - 4, from.Y - 6, c.Z + 15 ), c.Map ), c, 0x36D4, 7, 0, false, true, 0x497, 0, 9502, 1, 0, (EffectLayer)255, 0x100 );
								Effects.SendMovingParticles( new Entity( Serial.Zero, new Point3D( c.X - 6, from.Y - 4, c.Z + 15 ), c.Map ), c, 0x36D4, 7, 0, false, true, 0x497, 0, 9502, 1, 0, (EffectLayer)255, 0x100 );

								Effects.SendTargetParticles( c, 0x375A, 35, 90, 0x00, 0x00, 9502, (EffectLayer)255, 0x100 );
            							m_Powder.Delete(); 
							}
						}
					}	
            			}
         		} 
      		} 
   	} 
} 
