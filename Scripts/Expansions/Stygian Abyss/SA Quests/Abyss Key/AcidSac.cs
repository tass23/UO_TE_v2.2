using System;
using Server;
using Server.Targeting;
using Server.Mobiles;
using Server.Items;
using System.Collections;
using System.Collections.Generic;
using Server.ContextMenus;

namespace Server.Items
{
    public class AcidSac : Item
    {
        public override int LabelNumber { get { return 1111654; } } // acid sac

        [Constructable]
        public AcidSac()
            : this(1)
        {
        }

        [Constructable]
        public AcidSac(int amount)
            : base(0xDCF)
        {
            Hue = 2406;
            Stackable = true;
            Amount = amount;
        }

        public AcidSac(Serial serial)
            : base(serial)
        {
        }

        public override void OnDoubleClick(Mobile from)
        {
            if (IsChildOf(from.Backpack))
                from.Target = new InternalTargetAcidSac(from, this);
            else
                from.SendLocalizedMessage(1042001);
        }

        private class InternalTargetAcidSac : Target
        {
            private AcidSac m_AcidSac;
            public InternalTargetAcidSac(Mobile from, AcidSac acidsac)
                : base(1, false, TargetFlags.None)
            {
                m_AcidSac = acidsac;
            }

            private static Queue m_ToDelete = new Queue();

            protected override void OnTarget(Mobile from, object targeted)
            {
                if (m_AcidSac.Deleted)
                    return;

                if (targeted is Mobile)
                    from.SendMessage("You can't use that on that!");

                else if (targeted is AcidVine)
                {
                    AcidVine acidvine = (AcidVine)targeted;
                    
                    IPooledEnumerable eable = acidvine.GetItemsInRange(1);
                    
                    foreach (Item item in eable)
                    {       
                        if (item == null)
                            continue;

                        if (item is AcidWall)
                            m_ToDelete.Enqueue(item);

                        if (item is AcidWall1)
                            m_ToDelete.Enqueue(item);
                                 
                    }
                        eable.Free();

                        while (m_ToDelete.Count > 0)
                            ((Item)m_ToDelete.Dequeue()).Delete();

                        from.SendMessage("You use the acid sac to burn the vines away, to reveal a hidden passage!");
                        m_AcidSac.Delete();
                        acidvine.Delete();
                   
                }
                else
                {
                    from.SendMessage("You can't use that on that!");

                }
            }
        }
                   

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }
}
    