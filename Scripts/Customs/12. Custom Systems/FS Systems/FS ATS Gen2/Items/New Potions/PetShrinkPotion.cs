using System;
using Server.Network;
using Server;
using Server.Targets;
using Server.Targeting;
using Server.Mobiles;
using Server.Gumps;

namespace Server.Items
{
	public class PetShrinkPotion : BasePotion
	{
		[Constructable]
		public PetShrinkPotion() : base( 3854, PotionEffect.PetShrink )
		{
			Weight = 1.0;
			Movable = true;
			Name = "shrink potion";
		}

		public PetShrinkPotion( Serial serial ) : base( serial )
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
			else if ( FSATS.EnableShrinkSystem == false )
			{
				m.SendMessage( "The shrink system has been disabled. Contact your server administrator for details." );
			}
			else if( m.InRange( this.GetWorldLocation(), 1 ) ) 
		        {
				m.Target = new PetShrinkPotionTarget( this );
				m.SendMessage( "What would you like to use this on." );
		        } 
		        else 
		        { 
		            m.SendLocalizedMessage( 500446 ); // That is too far away. 
		        }
		}
  		private class PetShrinkPotionTarget : Target 
      		{ 
         		private Mobile m_Owner; 
      
         		private PetShrinkPotion m_Powder; 

         		public PetShrinkPotionTarget( PetShrinkPotion charge ) : base ( 10, false, TargetFlags.None ) 
         		{ 
            			m_Powder=charge; 
         		} 
          
         		protected override void OnTarget( Mobile from, object target ) 
         		{ 

            			if ( target == from ) 
               				from.SendMessage( "You cant shrink yourself!" );

				else if ( target is PlayerMobile )
					from.SendMessage( "That person gives you a dirty look." );

				else if ( target is Item )
					from.SendMessage( "You can only shrink pets that you own" );

				else if ( target is BaseBioCreature && FSATS.EnableBioShrink == false )
					from.SendMessage( "Unnatural creatures cannot be shrunk" ); 

				else if ( Server.Spells.SpellHelper.CheckCombat( from ) )
					from.SendMessage( "You cannot shrink your pet while your fighting." );

          			else if ( target is BaseCreature ) 
          			{ 
          				BaseCreature c = (BaseCreature)target;	

					bool packanimal = false;
					Type typ = c.GetType();
					string nam = typ.Name;

					foreach ( string ispack in FSATS.PackAnimals )
					{
  						if ( ispack == nam )
    							packanimal = true;
					}

					if ( c.BodyValue == 400 || c.BodyValue == 401 && c.Controlled == false )
					{
						from.SendMessage( "That person gives you a dirty look." );
					}
					else if ( c.ControlMaster != from && c.Controlled == false )
					{
						from.SendMessage( "This is not your pet." );
					}
					else if ( packanimal == true && (c.Backpack != null && c.Backpack.Items.Count > 0) )
					{
						from.SendMessage( "You must unload your pets backpack first." );
					}
					else if ( c.IsDeadPet )
					{ 
						from.SendMessage( "You cannot shrink the dead." );
					}	
					else if ( c.Summoned )
					{ 
						from.SendMessage( "You cannot shrink a summoned creature." );
					}
					else if ( c.Combatant != null && c.InRange( c.Combatant, 12 ) && c.Map == c.Combatant.Map )
					{
						from.SendMessage( "Your pet is fighting, You cannot shrink it yet." );
					}
					else if ( c.BodyMod != 0 )
					{
						from.SendMessage( "You cannot shrink your pet while its polymorphed." );
					}
					//else if ( Server.Spells.LostArts.CharmBeastSpell.IsCharmed( c ) )
					//{
					//	from.SendMessage( "Your hold over this pet is not strong enough to shrink it." );
					//}
					else if ( c.Controlled == true && c.ControlMaster == from)
					{
						Type type = c.GetType();
        					ShrinkItemX si = new ShrinkItemX();
						si.MobType = type;
						si.Pet = c;
						si.PetOwner = from;

						if ( c is BaseMount )
						{
							BaseMount mount = (BaseMount)c;
							si.MountID = mount.ItemID;
						}

        					from.AddToBackpack( si );

						IEntity p1 = new Entity( Serial.Zero, new Point3D( from.X, from.Y, from.Z ), from.Map );
						IEntity p2 = new Entity( Serial.Zero, new Point3D( from.X, from.Y, from.Z + 50 ), from.Map );

						Effects.SendMovingParticles( p2, p1, ShrinkTable.Lookup( c ), 1, 0, true, false, 0, 3, 1153, 1, 0, EffectLayer.Head, 0x100 );
						from.PlaySound( 492 );

						c.Controlled = true; 
						c.ControlMaster = null;
						c.Internalize();

						c.OwnerAbandonTime = DateTime.MinValue;

						c.IsStabled = true;

						m_Powder.Delete();
					}
  
            			}
         		} 
      		} 
	}
}

