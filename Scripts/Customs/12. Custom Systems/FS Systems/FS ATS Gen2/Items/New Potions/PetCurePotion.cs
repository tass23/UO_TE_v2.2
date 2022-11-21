using System;
using Server.Network;
using Server;
using Server.Targets;
using Server.Targeting;
using Server.Mobiles;
using Server.Gumps;

namespace Server.Items
{
	public class CurePotionPet : BasePotion
	{

		[Constructable]
		public CurePotionPet() : base( 0x182B, PotionEffect.PetCure )
		{
			Weight = 1.0;
			Movable = true;
			Name = "pet cure potion";
		}

		public CurePotionPet( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}

		public override void Drink( Mobile m )
		{
			if ( !IsChildOf( m.Backpack ) )
			{
				m.SendLocalizedMessage( 1042001 ); // That must be in your pack for you to use it.
			}
			else if( m.InRange( this.GetWorldLocation(), 1 ) ) 
		        {
				m.Target = new CurePotionPetTarget( this );
				m.SendMessage( "What would you like to use this on." );
		        } 
		        else 
		        { 
		            m.SendLocalizedMessage( 500446 ); // That is too far away. 
		        }
		}
  		private class CurePotionPetTarget : Target 
      		{ 
         		private Mobile m_Owner; 
      
         		private CurePotionPet m_Powder; 

         		public CurePotionPetTarget( CurePotionPet charge ) : base ( 10, false, TargetFlags.None ) 
         		{ 
            			m_Powder=charge; 
         		} 
          
         		protected override void OnTarget( Mobile from, object target ) 
         		{ 

            			if ( target == from ) 
               				from.SendMessage( "You cannot use this on yourself!" );

				else if ( target is PlayerMobile )
					from.SendMessage( "That person gives you a dirty look." );

				else if ( target is Item )
					from.SendMessage( "You cannot use this on that." );

          			else if ( target is BaseCreature ) 
          			{ 
          				BaseCreature c = (BaseCreature)target;	
					if ( c.BodyValue == 400 || c.BodyValue == 401 && c.Controlled == false )
					{
						from.SendMessage( "That person gives you a dirty look." );
					}
					else if ( c.Poison == null )
					{
						from.SendMessage( "That creature is not poisoned." );
					}
					else if ( c.Controlled == false )
					{
						from.SendMessage( "That is not tamed." );
					}
					else
					{
						if ( c.Poison == Poison.Lesser )
						{
							c.Poison = null;
							from.SendMessage( "You have cured this creature." );
						}
						else if ( c.Poison == Poison.Regular )
						{
							if( Utility.Random( 100 ) < 90 ) 
							switch ( Utility.Random( 1 )) 
							{ 
          
         							case 0: 
								c.Poison = null; 
								from.SendMessage( "You have cured this creature." );
								break;
 
							}
							else
							{
								from.SendMessage( "You fail to cure the creature." );
							}
						}
						else if ( c.Poison == Poison.Greater )
						{
							if( Utility.Random( 100 ) < 75 ) 
							switch ( Utility.Random( 1 )) 
							{ 
          
         							case 0: 
								c.Poison = null; 
								from.SendMessage( "You have cured this creature." );
								break;
 
							}
							else
							{
								from.SendMessage( "You fail to cure the creature." );
							}
						}
						else if ( c.Poison == Poison.Deadly )
						{
							if( Utility.Random( 100 ) < 50 ) 
							switch ( Utility.Random( 1 )) 
							{ 
          
         							case 0: 
								c.Poison = null; 
								from.SendMessage( "You have cured this creature." );
								break;
 
							}
							else
							{
								from.SendMessage( "You fail to cure the creature." );
							}
						}
						else if ( c.Poison == Poison.Lethal )
						{
							if( Utility.Random( 100 ) < 25 ) 
							switch ( Utility.Random( 1 )) 
							{ 
          
         							case 0: 
								c.Poison = null; 
								from.SendMessage( "You have cured this creature." );
								break;
 
							}
							else
							{
								from.SendMessage( "You fail to cure the creature." );
							}
						}

						c.FixedParticles( 0x373A, 10, 15, 5012, EffectLayer.Waist );
						from.PlaySound( 0x1E0 );

						m_Powder.Delete();
					}
  
            			}
         		} 
      		} 
   	} 
} 

