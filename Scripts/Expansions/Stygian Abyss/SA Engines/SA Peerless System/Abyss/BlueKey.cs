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
    public class BlueKey : AbyssKey
    {
        public override int LabelNumber { get { return 1111646; } } // Blue Key Fragment
        public override int Lifespan { get { return 21600; } }

        [Constructable]
        public BlueKey()
            : base(0x1012)
        {
            Weight = 1.0;
            Hue = 0x5D; // TODO check
            LootType = LootType.Blessed;
            Movable = false;
        }

        public override void OnDoubleClick(Mobile from)
        {
            /*if (this.RootParent == from)
            {
                Item item = from.Backpack.FindItemByType(typeof(BlueKey1));
                if (item != null )
                {
                    from.SendMessage("You already have a key!");

                    }
                    else
                    {*/

            if (from.InRange(this.GetWorldLocation(), 2) && this.IsAccessibleTo(from))
            {
                from.SendMessage("You see a key fragment.");
                from.Target = new InternalTarget(this);

            }
            else
            {
                from.SendMessage("That is too far away.");
            }
        }

        private class InternalTarget : Target
        {
            private BlueKey m_BlueKey;

            public InternalTarget(BlueKey key)
                : base(-1, false, TargetFlags.None)
            { m_BlueKey = key; }

            protected override void OnTarget(Mobile from, object obj)
            {
                if (obj is BlueKey && ((BlueKey)obj).IsAccessibleTo(from))
                {
                    from.SendMessage("You make a copy of the key in your pack");
                    ((BlueKey)obj).Delete();
                    m_BlueKey.Delete();
                    from.AddToBackpack(new BlueKey());
                }
                else
                    from.SendMessage("You have no use for that.");
            }
        }

        public BlueKey(Serial serial)
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

            int version = reader.ReadInt();
        }
    }
}


