using Server.Targeting; 
using System; 
using Server; 
using Server.Network; 
using Server.Engines.BulkOrders;
using System.Collections;

namespace Server.Items
{
    public class ShadowBulkOrderCover : Item
    {

        [Constructable]
        public ShadowBulkOrderCover()
            : base(0x2831)
        {
            Weight = 1.0;
            Movable = true;
            Hue = 2406;
            Name = "Bulk Order Book Cover (Shadow)";
        }

        public ShadowBulkOrderCover(Serial serial)
            : base(serial)
        {
        }

        public override void OnDoubleClick(Mobile from)
        {

            if (!IsChildOf(from.Backpack))
            {
                from.SendLocalizedMessage(1042001); // That must be in your pack for you to use it.
            }
            else if (from.InRange(this.GetWorldLocation(), 1))
            {
                from.SendMessage("What Bulk Order Book do you wish to dye?");
                from.Target = new ShadowBulkOrderCoverTarget(this);
            }
            else
            {
                from.SendLocalizedMessage(500446); // That is too far away. 
            }

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

        private class ShadowBulkOrderCoverTarget : Target
        {
            private Mobile m_Owner;

            private ShadowBulkOrderCover m_Cover;

            public ShadowBulkOrderCoverTarget(ShadowBulkOrderCover cover)
                : base(1, false, TargetFlags.None)
            {
                m_Cover = cover;
            }

            protected override void OnTarget(Mobile from, object targeted)
            {
                if (targeted is BulkOrderBook)
                {
                    Item book = (Item)targeted;

                    if (book.Hue == 0)
                    {
                        book.Hue = 2406;
                        book.Name = "Shadow Leather Bulk Order Book";
                        from.SendMessage("You Dyed the Book");
                        m_Cover.Delete();
                    }
                    else
                    {
                        from.SendMessage("That Book is already dyed.");
                    }
                }

                else
                {
                    from.SendMessage("This can only be used on a Bulk Order Book.");
                }
            }
        }
    }
}