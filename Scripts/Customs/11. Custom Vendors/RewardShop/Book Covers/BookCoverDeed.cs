using System; 
using Server; 
using Server.Gumps; 
using Server.Network;
using Server.Engines.VeteranRewards;

namespace Server.Items
{

    public class BODBookCoverDeed : Item, IRewardItem
    {
        private bool m_IsRewardItem;

        [CommandProperty(AccessLevel.GameMaster)]
        public bool IsRewardItem
        {
            get { return m_IsRewardItem; }
            set { m_IsRewardItem = value; InvalidateProperties(); }
        }
        [Constructable]
        public BODBookCoverDeed()
            : this(null)
        {
        }

        [Constructable]
        public BODBookCoverDeed(string name)
            : base(0x14ef)
        {
            Movable = true;
            Hue = 3 + (Utility.Random(20) * 5);
            Name = "Bulk Order Book Cover Deed";
        }

        public BODBookCoverDeed(Serial serial)
            : base(serial)
        {
        }

        public override void OnDoubleClick(Mobile from)
        {
            if (m_IsRewardItem && !RewardSystem.CheckIsUsableBy(from, this, null))
                return;
            if (!IsChildOf(from.Backpack))
            {
                from.SendLocalizedMessage(1042001);

            }
            else
            {
                from.SendGump(new BookCoverDeedgump(from, this));
            }
        }

        public override void GetProperties(ObjectPropertyList list)
        {
            base.GetProperties(list);

            if (m_IsRewardItem)
                list.Add(1080458); // 8th Year Veteran Reward

        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);
            writer.Write((bool)m_IsRewardItem);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
            m_IsRewardItem = reader.ReadBool();
        }
    }
}
