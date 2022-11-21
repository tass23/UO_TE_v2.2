using System;
using Server.Network;
using Server.Prompts;
using Server.Items;
using Server.Targeting;
using Server;

namespace Server.Items
{
    public class WinduTarget : Target
    {
        private WinduDeed m_Deed;
        public WinduTarget(WinduDeed deed): base(1, false, TargetFlags.None)
        {
            m_Deed = deed;
        }

        protected override void OnTarget(Mobile from, object target)
        {
            if (m_Deed.Deleted || m_Deed.RootParent != from)
            {
                from.SendMessage("You cannot empower that with the Windu's Guile crystal.");
                return;
            }

            if (target is Lightsaber)
            {
                Lightsaber item = (Lightsaber)target;

                if (item is Lightsaber)
                {
                    if (((Lightsaber)item).Attributes.ReflectPhysical >= 31 && ((Lightsaber)item).Attributes.WeaponDamage >= 75 && ((Lightsaber)item).WeaponAttributes.HitLeechHits >= 100 || ((Lightsaber)item).TimesEmpowered > 1)
                    {
                        from.SendMessage("That lightsaber has already been empowered.");
                    }
                    else
                    {
						((Lightsaber)item).Attributes.ReflectPhysical += Utility.RandomMinMax(5, 31);
						((Lightsaber)item).Attributes.WeaponDamage += Utility.RandomMinMax(15, 75);
						((Lightsaber)item).WeaponAttributes.HitLeechHits += Utility.RandomMinMax(20, 100);

						if (((Lightsaber)item).TimesEmpowered == 1)
						{
							((Lightsaber)item).TimesEmpowered += 2;
							from.SendMessage("Your lightsaber has now been fully empowered.");
							return;
						}
						else
						{
							((Lightsaber)item).TimesEmpowered +=1;
							from.SendMessage("Your lightsaber can only be empowered with one more focusing crystal.");
						}
                        from.SendMessage("The Windu's Guile crystal has empowered your lightsaber.");
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

    public class WinduDeed : EmpoweringDeed
    {
        [Constructable]
        public WinduDeed()
        {
            Name = "Windu's Guile";
            Hue = 1277;
        }

		public override void AddNameProperties( ObjectPropertyList list )
 		{
			base.AddNameProperties( list );
			list.Add( "Reflect Physical Damage, Damage Increase" );
 		}
		
        public WinduDeed(Serial serial): base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }

        public override void OnDoubleClick(Mobile from)
        {
            if (!IsChildOf(from.Backpack))
            {
                from.SendMessage("The lightsaber needs to be in your pack.");
            }
            else
            {
                from.SendMessage("Which lightsaber would you like to empower with the Windu's Guile crystal?");
                from.Target = new WinduTarget(this);
            }
        }
    }
}