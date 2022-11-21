using System;
using Server.Network;
using Server.Prompts;
using Server.Items;
using Server.Targeting;
using Server;

namespace Server.Items
{
    public class TyranusTarget : Target
    {
        private TyranusDeed m_Deed;
        public TyranusTarget(TyranusDeed deed): base(1, false, TargetFlags.None)
        {
            m_Deed = deed;
        }

        protected override void OnTarget(Mobile from, object target)
        {
            if (m_Deed.Deleted || m_Deed.RootParent != from)
            {
                from.SendMessage("You cannot empower that with the Cunning of Tyranus crystal.");
                return;
            }

            if (target is Lightsaber)
            {
                Lightsaber item = (Lightsaber)target;

                if (item is Lightsaber)
                {
                    if (((Lightsaber)item).WeaponAttributes.HitHarm >= 10 && ((Lightsaber)item).WeaponAttributes.HitMagicArrow >= 10 || ((Lightsaber)item).TimesEmpowered > 1)
                    {
                        from.SendMessage("That lightsaber has already been empowered.");
                    }
                    else
                    {
                        ((Lightsaber)item).WeaponAttributes.HitHarm += Utility.RandomMinMax(5, 10);
						((Lightsaber)item).WeaponAttributes.HitMagicArrow += Utility.RandomMinMax(5, 10);

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
                        from.SendMessage("The Cunning of Tyranus crystal has empowered your lightsaber.");
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

    public class TyranusDeed : EmpoweringDeed
    {
        [Constructable]
        public TyranusDeed()
        {
            Name = "Cunning of Tyranus";
            Hue = 1481;
        }

		public override void AddNameProperties( ObjectPropertyList list )
 		{
			base.AddNameProperties( list );
			list.Add( "Hit Harm, Hit Magic Arrow" );
 		}
		
        public TyranusDeed(Serial serial): base(serial)
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
                from.SendMessage("Which lightsaber would you like to empower with the Cunning of Tyranus crystal?");
                from.Target = new TyranusTarget(this);
            }
        }
    }
}