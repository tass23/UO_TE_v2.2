
using System;
using Server;
using Server.Network;
using Server.Prompts;
using Server.Items;
using Server.Mobiles;
using Server.Targeting;
using Server.Gumps;

namespace Server.Items
{
    public class CraftedResurrectionTarget : Target
    {
        private CraftedPetResurrectionDeed m_Deed;

        public CraftedResurrectionTarget(CraftedPetResurrectionDeed deed)
            : base(1, false, TargetFlags.None)
        {
            m_Deed = deed;
        }

        protected override void OnTarget(Mobile from, object target)
        {
            if (target is BaseCreature)
            {
                BaseCreature t = (BaseCreature)target;

                if (!t.IsDeadPet)
                {
                    from.SendMessage("That pet is already living!");
                }

                else if (t.ControlMaster != from)
                {
                    from.SendMessage("That is not your pet!");
                }

                else if (t.Map == null || !t.Map.CanFit(t.Location, 16, false, false))
                {
                    from.SendLocalizedMessage(501042); // Target can not be resurrected at that location.
                }

                else if (t != null && t.IsDeadPet)
                {
                    Mobile master = t.ControlMaster;

                    if (master != null && master.InRange(t, 3))
                    {
                        from.SendLocalizedMessage(503255); // You are able to resurrect the creature.
                        t.PlaySound(0x214);
                        t.FixedEffect(0x376A, 10, 16);
                        t.ResurrectPet();
                        m_Deed.Delete(); // Delete the deed 
                    }
                }
            }
            else
            {
                from.SendMessage("That is not a valid traget.");
            }
        }
    }

    public class CraftedPetResurrectionDeed : Item // Create the item class which is derived from the base item class 
    {
        [Constructable]
        public CraftedPetResurrectionDeed()
            : base(0x14F0)
        {
            Weight = 1.0;
            Name = "a pet Resurrection deed";
            LootType = LootType.Blessed;
            Hue = 2216;
        }

        public CraftedPetResurrectionDeed(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0); // version 
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            LootType = LootType.Blessed;
            int version = reader.ReadInt();
        }

        public override bool DisplayLootType { get { return false; } }

        public override void OnDoubleClick(Mobile from) // Override double click of the deed to call our target 
        {
            if (!IsChildOf(from.Backpack)) // Make sure its in their pack 
            {
                from.SendLocalizedMessage(1042001); // That must be in your pack for you to use it. 
            }
            else
            {
                from.SendMessage("Choose the pet you wish to bring back to life.");
                from.Target = new CraftedResurrectionTarget(this); // Call our target 
            }
        }
    }
}