using System;
using Server.Network;
using Server;
using Server.Targets;
using Server.Targeting;
using Server.Mobiles;
using Server.Gumps;

namespace Server.Items
{
	public class GreaterHealPotionPet : BasePotion
	{

		[Constructable]
		public GreaterHealPotionPet() : base( 0xF0C, PotionEffect.PetGreaterHeal )
		{
			Weight = 1.0;
			Movable = true;
			Name = "pet greater heal potion";
		}

		public GreaterHealPotionPet( Serial serial ) : base( serial )
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
				m.Target = new GreaterHealPotionPetTarget( this );
				m.SendMessage( "What would you like to use this on." );
		        } 
		        else 
		        { 
		            m.SendLocalizedMessage( 500446 ); // That is too far away. 
		        }
		}
  		private class GreaterHealPotionPetTarget : Target 
      		{ 
         		private Mobile m_Owner; 
      
         		private GreaterHealPotionPet m_Powder; 

         		public GreaterHealPotionPetTarget( GreaterHealPotionPet charge ) : base ( 10, false, TargetFlags.None ) 
         		{ 
            			m_Powder=charge; 
         		} 
          
         		protected override void OnTarget( Mobile from, object target ) 
         		{ 

            			if ( target == from ) 
               				from.SendMessage( "You cannot use this on yourself!" );

				else if ( target is PlayerMobile )
					from.SendMessage( "You cannot heal that." );

				else if ( target is Item )
					from.SendMessage( "You cannot use this on that." );

          			else if ( target is BaseCreature ) 
          			{ 
          				BaseCreature c = (BaseCreature)target;	
					if ( c.BodyValue == 400 || c.BodyValue == 401 && c.Controlled == false )
					{
						from.SendMessage( "You cannot heal that." );
					}
					else if ( c.Controlled == false )
					{
						from.SendMessage( "That is not tamed." );
					}
					else if ( c.Hits == c.HitsMax )
					{
						from.SendMessage( "This creature does not require healing." );
					}
					else
					{
						c.Hits += Utility.RandomList( 30, 50 );
						c.FixedParticles( 0x376A, 9, 32, 5005, EffectLayer.Waist );
						from.PlaySound( 0x1F2 );

						m_Powder.Delete();
					}
  
            			}
         		} 
      		} 
   	} 
} 

