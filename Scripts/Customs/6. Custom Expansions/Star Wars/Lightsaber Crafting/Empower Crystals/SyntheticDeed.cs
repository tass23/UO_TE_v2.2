using System;
using Server.Network;
using Server.Prompts;
using Server.Items;
using Server.Targeting;
using Server;

namespace Server.Items
{
    public class SyntheticTarget : Target
    {
        private SyntheticDeed m_Deed;
        public SyntheticTarget(SyntheticDeed deed): base(1, false, TargetFlags.None)
        {
            m_Deed = deed;
        }

        protected override void OnTarget(Mobile from, object target)
        {
            if (m_Deed.Deleted || m_Deed.RootParent != from)
            {
                from.SendMessage("You cannot empower that with the Synthetic crystal.");
                return;
            }

            if (target is Lightsaber)
            {
                Lightsaber item = (Lightsaber)target;

                if (item is Lightsaber)
                {
                    if (((Lightsaber)item).Attributes.WeaponSpeed >= 55 && ((Lightsaber)item).Attributes.WeaponDamage >= 100 || ((Lightsaber)item).TimesEmpowered > 1)
                    {
                        from.SendMessage("That lightsaber has already been empowered.");
                    }
                    else
                    {
						((Lightsaber)item).Attributes.WeaponDamage += Utility.RandomMinMax(20, 100);
						((Lightsaber)item).Attributes.WeaponSpeed += Utility.RandomMinMax(15, 55);

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
                        from.SendMessage("The Synthetic crystal has empowered your lightsaber.");
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

    public class SyntheticDeed : EmpoweringDeed
    {
        [Constructable]
        public SyntheticDeed()
        {
            Name = "Synthetic Crystal";
            Hue = 1477;
        }

		public override void AddNameProperties( ObjectPropertyList list )
 		{
			base.AddNameProperties( list );
			list.Add( "Damage Increase, Swing Speed Increase" );
 		}
		
        public SyntheticDeed(Serial serial): base(serial)
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
                from.SendMessage("Which lightsaber would you like to empower with the Synthetic crystal?");
                from.Target = new SyntheticTarget(this);
            }
        }
    }
}