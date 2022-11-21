using System;
using Server.Network;
using Server.Prompts;
using Server.Items;
using Server.Targeting;
using Server;

namespace Server.Items
{
    public class UpariTarget : Target
    {
        private UpariDeed m_Deed;
        public UpariTarget(UpariDeed deed): base(1, false, TargetFlags.None)
        {
            m_Deed = deed;
        }

        protected override void OnTarget(Mobile from, object target)
        {
            if (m_Deed.Deleted || m_Deed.RootParent != from)
            {
                from.SendMessage("You cannot empower that with the Upari crystal.");
                return;
            }

            if (target is Lightsaber)
            {
                Lightsaber item = (Lightsaber)target;

                if (item is Lightsaber)
                {
                    if (((Lightsaber)item).WeaponAttributes.HitLeechMana >= 40 && ((Lightsaber)item).WeaponAttributes.HitLowerDefend >= 10 && ((Lightsaber)item).Attributes.RegenStam >= 2 || ((Lightsaber)item).TimesEmpowered > 1)
                    {
                        from.SendMessage("That lightsaber has already been empowered.");
                    }
                    else
                    {
                        ((Lightsaber)item).WeaponAttributes.HitLeechMana += Utility.RandomMinMax(15, 40);
						((Lightsaber)item).WeaponAttributes.HitLowerDefend += Utility.RandomMinMax(2, 10);
						((Lightsaber)item).Attributes.RegenStam += Utility.RandomMinMax(1, 2);

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
                        from.SendMessage("The Upari crystal has empowered your lightsaber.");
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

    public class UpariDeed : EmpoweringDeed
    {
        [Constructable]
        public UpariDeed()
        {
            Name = "Upari Crystal";
            Hue = 1488;
        }

		public override void AddNameProperties( ObjectPropertyList list )
 		{
			base.AddNameProperties( list );
			list.Add( "Mana Leech, Hit Lower Defense, Stamina Regeneration" );
 		}
		
        public UpariDeed(Serial serial): base(serial)
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
                from.SendMessage("Which lightsaber would you like to empower with the Upari crystal?");
                from.Target = new UpariTarget(this);
            }
        }
    }
}