using System;
using Server.Network;
using Server.Prompts;
using Server.Items;
using Server.Targeting;

namespace Server.Items
{
	public class BaasTarget : Target
	{
		private BaasDeed m_Deed;
		public BaasTarget( BaasDeed deed ) : base( 1, false, TargetFlags.None )
		{
			m_Deed = deed;
		}

		protected override void OnTarget(Mobile from, object target)
		{
            if (m_Deed.Deleted || m_Deed.RootParent != from)
            {
                from.SendMessage("You cannot empower that with the Baas' Wisdom crystal.");
                return;
            }

            if (target is Lightsaber)
            {
                Lightsaber item = (Lightsaber)target;

                if (item is Lightsaber)
                {
                    if (((Lightsaber)item).Attributes.RegenMana >= 4 && ((Lightsaber)item).WeaponAttributes.HitColdArea >= 50 || ((Lightsaber)item).TimesEmpowered > 1)
                    {
                        from.SendMessage("That lightsaber has already been empowered.");
                    }
                    else
                    {
                        ((Lightsaber)item).Attributes.RegenMana += Utility.RandomMinMax(2, 4);
						((Lightsaber)item).WeaponAttributes.HitColdArea += Utility.RandomMinMax(20, 50);

						if (((Lightsaber)item).TimesEmpowered == 1)
						{
							((Lightsaber)item).TimesEmpowered += 2;
							from.SendMessage("Your lightsaber has now been fully empowered.");
							return;
						}
						else
						{
							((Lightsaber)item).TimesEmpowered += 1;
							from.SendMessage("Your lightsaber can only be empowered with one more focusing crystal.");
						}
                        from.SendMessage("The Baas' Wisdom crystal has empowered your lightsaber.");
                        m_Deed.Delete();
                    }
                }
            }
            else
            {
                from.SendMessage("You cannot empower that.");
            }
		}
	}

	public class BaasDeed : EmpoweringDeed
	{
		[Constructable]
		public BaasDeed()
		{
            Name = "Baas' Wisdom Crystal";
            Hue = 1100;
		}

		public override void AddNameProperties( ObjectPropertyList list )
 		{
			base.AddNameProperties( list );
			list.Add( "Mana Regeneration, Hit Cold Area" );
 		}
		
		public BaasDeed( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( !IsChildOf( from.Backpack ) )
			{
				 from.SendMessage("The lightsaber needs to be in your pack.");
			}
			else
			{
                from.SendMessage("Which lightsaber would you like to empower with the Baas' Wisdom crystal?");
				from.Target = new BaasTarget( this ); 
			}
		}	
	}
}