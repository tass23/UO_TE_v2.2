using System;
using Server;
using Server.Mobiles;
using Server.Targeting;

namespace Server.Items
{
	public class NewHitchingPostEast3 : AddonComponent
	{
		private int m_Charges;
		private NewHitchingPostEast3Addon m_Addon;

		[CommandProperty( AccessLevel.GameMaster )]
		public int Charges
		{
			get{ return m_Charges; }
			set{ m_Charges = value; InvalidateProperties(); }
		}

      		public override bool ForceShowProperties{ get{ return ObjectPropertyList.Enabled; } }

		#region Constructors
		[Constructable]
		public NewHitchingPostEast3() : this( 0x14E7 )
		{
		}

		[Constructable]
		public NewHitchingPostEast3( NewHitchingPostEast3Addon addon, int charges, int itemID ) : base( itemID )
		{
			m_Charges = charges;
			m_Addon = addon;
		}
		
		public NewHitchingPostEast3( Serial serial ) : base( serial )
		{
		}
		#endregion

		public override void OnDoubleClick( Mobile from )
		{
			if( from.InRange( this.GetWorldLocation(), 2 ) == false )
			{
				from.SendLocalizedMessage( 500486 );	//That is too far away.
			}
			else if ( FSATS.EnableShrinkSystem == false )
			{
				from.SendMessage( "The shrink system has been disabled. Contact your server administrator for details." );
			}
			else if ( from.Skills[SkillName.AnimalTaming].Value > 10 )
			{
           			from.Target = new HitchingPostTarget( m_Addon, this );
				from.SendMessage( "What do you wish to shrink?" );
			}
			else
			{
				from.SendMessage( "You must have 10 animal taming to use a hitching post." );
				from.SendMessage( "Try using a pet shriking potion." );
			}
		}

		public override void AddNameProperties( ObjectPropertyList list )
		{
			base.AddNameProperties( list );

			list.Add( 1060658, "Charges\t{0}", m_Charges.ToString() );
		}

		#region Serialization
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version

			writer.Write( m_Charges );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			m_Charges = reader.ReadInt();
		} 
		#endregion
		private class HitchingPostTarget : Target 
      	{ 
         	private Mobile m_Owner; 
      
         	private NewHitchingPostEast3 m_Powder;
			private NewHitchingPostEast3Addon m_Addon; 

         	public HitchingPostTarget( NewHitchingPostEast3Addon addon, NewHitchingPostEast3 charge ) : base ( 10, false, TargetFlags.None ) 
         	{ 
            	m_Powder = charge;
				m_Addon = addon;
         	} 
          
         	protected override void OnTarget( Mobile from, object target ) 
         	{
				if (from == null || target == null)
					return;

            	else if ( target == from ) 
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

						m_Powder.Charges -= 1;
						if ( m_Powder.Charges == 0 )
							m_Powder.Delete();
					}
            	}
         	} 
      	} 
	}

	public class NewHitchingPostEast3Addon : BaseAddon
	{
		private int m_Charges;

		[CommandProperty( AccessLevel.GameMaster )]
		public int Charges
		{
			get{ return m_Charges; }
			set{ m_Charges = value; InvalidateProperties(); }
		}

		public override BaseAddonDeed Deed{ get{ return new NewHitchingPostEast3Deed( m_Charges ); } }

		[Constructable]
		public NewHitchingPostEast3Addon( int charges)
		{
			Charges = charges;
			AddComponent( new NewHitchingPostEast3( this, m_Charges, 0x14E7 ), 0, 0, 0 );
		}

		public NewHitchingPostEast3Addon( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 1 ); // version

			writer.Write( (int) m_Charges );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			switch (version)
			{
				case 1:
				{ //Added Charges - Fatima
					m_Charges = reader.ReadInt();
					goto case 0;
				}
				case 0:
				{
					break;
				}
			}
		}
	}

	public class NewHitchingPostEast3Deed : BaseAddonDeed
	{
		public override BaseAddon Addon{ get{ return new NewHitchingPostEast3Addon( m_Charges ); } }

		private int m_Charges;

		[CommandProperty( AccessLevel.GameMaster )]
		public int Charges
		{
			get{ return m_Charges; }
			set{ m_Charges = value; InvalidateProperties(); }
		}

		[Constructable]
		public NewHitchingPostEast3Deed() : this( 100 )
		{
		}

		[Constructable]
		public NewHitchingPostEast3Deed( int charges )
		{
			Name = "hitching post east deed";
			Charges = charges;
		}

		public NewHitchingPostEast3Deed( Serial serial ) : base( serial )
		{
		}

		public override void AddNameProperties( ObjectPropertyList list )
		{
			base.AddNameProperties( list );

			list.Add( 1060658, "Charges\t{0}", this.Charges.ToString() );
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 1 ); // version

			writer.Write( (int) m_Charges );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			switch (version)
			{
				case 1:
				{ //Added Charges - Fatima
					m_Charges = reader.ReadInt();
					goto case 0;
				}
				case 0:
				{
					break;
				}
			}
		}
	}
}