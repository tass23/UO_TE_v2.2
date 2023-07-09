/*Script written by SunDials*/

using System;
using Server.Items;
using Server.Network;
using Server.Mobiles;
using Server.Gumps;
using System.Collections;
using Server.Misc;

namespace Server.Items
{
    [Furniture]
    [Flipable(0x2857, 0x2858)]
    public class MysticalArmoire : BaseContainer
    {
        [Constructable]
        public MysticalArmoire()
            : base(0x2857)
        {
            Name = "Mystical Armoire";
            Hue = 1164;
        }

        public override void OnDoubleClick(Mobile m)
        {
            if (m.InRange(this.GetWorldLocation(), 2))
            {
                if (this.IsSecure)
                {
                    Container armoire = this;
                    ArrayList equippedItems = new ArrayList(m.Items);
                    ArrayList armoireItems = new ArrayList(armoire.Items);
                    foreach (Item item in equippedItems)
                    {
                        if ((item.Layer != Layer.Bank) && (item.Layer != Layer.Backpack) && (item.Layer != Layer.Hair) && (item.Layer != Layer.FacialHair) && (item.Layer != Layer.Mount))
                        {
                            armoire.DropItem(item);
                        }
                    }
                    foreach (Item item in armoireItems)
                    {
                        m.EquipItem(item);
                    }
                    //DisplayTo(m);
                }
                else
                {
                    m.SendMessage("To use this item, you must secure it in a house first.");
                }
                DisplayTo(m);
            }
            else
            {
                m.LocalOverheadMessage(MessageType.Regular, 0x3B2, 1019045); // I can't reach that.
            }
        }

        public MysticalArmoire(Serial serial)
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