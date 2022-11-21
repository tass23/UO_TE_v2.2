using System;
using Server.Network;
using Server.Prompts;
using Server.Items;
using Server.Targeting;
using Server;

namespace Server.Items
{
    public class SapithTarget : Target
    {
        private SapithDeed m_Deed;
        public SapithTarget(SapithDeed deed): base(1, false, TargetFlags.None)
        {
            m_Deed = deed;
        }

        protected override void OnTarget(Mobile from, object target)
        {
            if (m_Deed.Deleted || m_Deed.RootParent != from)
            {
                from.SendMessage("You cannot empower that with the Sapith crystal.");
                return;
            }

            if (target is Lightsaber)
            {
                Lightsaber item = (Lightsaber)target;

                if (item is Lightsaber)
                {
                    if (((Lightsaber)item).Attributes.WeaponDamage >= 75 && ((Lightsaber)item).WeaponAttributes.HitLeechMana >= 10 || ((Lightsaber)item).TimesEmpowered > 1)
                    {
                        from.SendMessage("That lightsaber has already been empowered.");
                    }
                    else
                    {
                        ((Lightsaber)item).Attributes.WeaponDamage += Utility.RandomMinMax(15, 75);
						((Lightsaber)item).WeaponAttributes.HitLeechMana += Utility.RandomMinMax(2, 10);

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
                        from.SendMessage("The Sapith crystal has empowered your lightsaber.");
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

    public class SapithDeed : EmpoweringDeed
    {
        [Constructable]
        public SapithDeed()
        {
            Name = "Sapith Crystal";
            Hue = 2958;
        }

		public override void AddNameProperties( ObjectPropertyList list )
 		{
			base.AddNameProperties( list );
			list.Add( "Damage Increase, Mana Leech" );
 		}
		
        public SapithDeed(Serial serial): base(serial)
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
                from.SendMessage("Which lightsaber would you like to empower with the Sapith crystal?");
                from.Target = new SapithTarget(this);
            }
        }
    }
}