using System;
using Server.Network;
using Server.Prompts;
using Server.Items;
using Server.Targeting;
using Server;

namespace Server.Items
{
    public class AllyaExileTarget : Target
    {
        private AllyaExileDeed m_Deed;
        public AllyaExileTarget(AllyaExileDeed deed): base(1, false, TargetFlags.None)
        {
            m_Deed = deed;
        }

        protected override void OnTarget(Mobile from, object target)
        {
            if (m_Deed.Deleted || m_Deed.RootParent != from)
            {
                from.SendMessage("You cannot empower that with the Allya's Exile crystal.");
                return;
            }

            if (target is Lightsaber)
            {
                Lightsaber item = (Lightsaber)target;

                if (item is Lightsaber)
                {
                    if (((Lightsaber)item).WeaponAttributes.HitFireball >= 50 && ((Lightsaber)item).WeaponAttributes.ResistFireBonus >= 70 || ((Lightsaber)item).TimesEmpowered > 1)
                    {
                        from.SendMessage("That lightsaber has already been empowered.");
                    }
                    else
                    {
                        ((Lightsaber)item).WeaponAttributes.HitFireball += Utility.RandomMinMax(25, 50);
						((Lightsaber)item).WeaponAttributes.ResistFireBonus += Utility.RandomMinMax(50, 70);

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
                        from.SendMessage("The Allya's Exile crystal has empowered your lightsaber.");
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

    public class AllyaExileDeed : EmpoweringDeed
    {
		public override bool ForceShowProperties { get { return ObjectPropertyList.Enabled; } }
		
        [Constructable]
        public AllyaExileDeed()
        {
            Name = "Allya's Exile Crystal";
            Hue = 1172;
        }
		
		public override void AddNameProperties( ObjectPropertyList list )
 		{
			base.AddNameProperties( list );
			list.Add( "Hit Fireball, Fire Resist" );
 		}

        public AllyaExileDeed(Serial serial): base(serial)
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
                from.SendMessage("Which lightsaber would you like to empower with the Allya's Exile crystal?");
                from.Target = new AllyaExileTarget(this);
            }
        }
    }
}