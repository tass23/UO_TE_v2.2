using Server.Targeting; 
using System; 
using Server; 
using Server.Gumps; 
using Server.Network; 
using Server.Menus; 
using Server.Menus.Questions; 
using Server.Mobiles; 
using System.Collections;
using Server.ACC.CSS.Systems; 

namespace Server.Items 
{ 
   	public class PetLeash3 : Item 
   	{ 
    	private int m_Charges = 100;

		[CommandProperty( AccessLevel.GameMaster )]
		public int Charges
		{
			get{ return m_Charges; }
			set{ m_Charges = value; InvalidateProperties(); }
		}

      	[Constructable] 
      	public PetLeash3() : base( 0x1374 ) 
      	{ 
         	Weight = 1.0;  
         	Movable = true; 
         	Name="a pet leash"; 
        } 

		public override void AddNameProperties( ObjectPropertyList list )
		{
			base.AddNameProperties( list );

			list.Add( 1060658, "Charges\t{0}", m_Charges.ToString() );
		}

		public PetLeash3( Serial serial ) : base( serial ) 
		{ 
		} 
		public override void OnDoubleClick( Mobile from ) 
		{ 

		if ( !IsChildOf( from.Backpack ) )
		{
			from.SendLocalizedMessage( 1042001 ); // That must be in your pack for you to use it.
		}
		else if ( FSATS.EnableShrinkSystem == false )
		{
			from.SendMessage( "The shrink system has been disabled. Contact your server administrator for details." );
		}
		else if ( from.Skills[SkillName.AnimalTaming].Value > 0 )
		{
				from.Target = new LeashTarget( this );
			from.SendMessage( "What do you wish to shrink?" );
		}
		else
		{
			from.SendMessage( "You must have at least 1 point in Animal Taming to use a leash." );
			//from.SendMessage( "Try using a pet shriking potion." );
		}

		} 

		public override void Serialize( GenericWriter writer ) 
		{ 
			base.Serialize( writer ); 

			writer.Write( (int) 0 );

			writer.Write( m_Charges ); 
		} 

		public override void Deserialize( GenericReader reader ) 
		{ 
			base.Deserialize( reader ); 

			int version = reader.ReadInt(); 

			m_Charges = reader.ReadInt();
		} 
		
  		private class LeashTarget : Target 
      	{ 
			private Mobile m_Owner; 

			private PetLeash3 m_Powder; 

			public LeashTarget( PetLeash3 charge ) : base ( 10, false, TargetFlags.None ) 
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
					else if ( c.Allured )
					{
						from.SendMessage( "Your hold over this pet is not strong enough to shrink it." );
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

						m_Powder.Charges -= 1;
						if ( m_Powder.Charges == 0 )
							m_Powder.Delete();
					}
				}
			} 
      	} 
   	} 
} 
