using System;
using Server.Network;
using Server.Prompts;
using Server.Items;
using Server.Targeting;
using Server;

namespace Server.Items
{
    public class DODTarget : Target
    {
        private DODDeed m_Deed;
        public DODTarget(DODDeed deed): base(1, false, TargetFlags.None)
        {
            m_Deed = deed;
        }

        protected override void OnTarget(Mobile from, object target)
        {
            if (m_Deed.Deleted || m_Deed.RootParent != from)
            {
                from.SendMessage("You cannot empower that with the Dawn of Dagobah crystal.");
                return;
            }

            if (target is Lightsaber)
            {
                Lightsaber item = (Lightsaber)target;

                if (item is Lightsaber)
                {
                    if (((Lightsaber)item).WeaponAttributes.HitPoisonArea >= 80 && ((Lightsaber)item).WeaponAttributes.HitLowerDefend >= 25 && ((Lightsaber)item).Attributes.WeaponDamage >= 75 || ((Lightsaber)item).TimesEmpowered > 1)
                    {
                        from.SendMessage("That lightsaber has already been empowered.");
                    }
                    else
                    {
                        ((Lightsaber)item).WeaponAttributes.HitPoisonArea += Utility.RandomMinMax(30, 80);
						((Lightsaber)item).WeaponAttributes.HitLowerDefend += Utility.RandomMinMax(15, 25);
						((Lightsaber)item).Attributes.WeaponDamage += Utility.RandomMinMax(35, 75);

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
                        from.SendMessage("The Dawn of Dagobah crystal has empowered your lightsaber.");
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

    public class DODDeed : EmpoweringDeed
    {
        [Constructable]
        public DODDeed()
        {
            Name = "Dawn of Dagobah";
            Hue = 1062;
        }

		public override void AddNameProperties( ObjectPropertyList list )
 		{
			base.AddNameProperties( list );
			list.Add( "Hit Poison Area, Hit Lower Defense" );
 		}
		
        public DODDeed(Serial serial): base(serial)
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
                from.SendMessage("Which lightsaber would you like to empower with the Dawn of Dagobah crystal?");
                from.Target = new DODTarget(this);
            }
        }
    }
}