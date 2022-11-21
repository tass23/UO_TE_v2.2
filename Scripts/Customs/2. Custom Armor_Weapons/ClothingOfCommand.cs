using System;
using System.Collections.Generic;
using Server;
using Server.Items;
using Server.Mobiles;

namespace Server.Items
{
    public class BaseClothingOfCommand : Item
    {
        private int m_Bonus;

        [CommandProperty(AccessLevel.GameMaster)]
        public int Bonus
        {
            get { return m_Bonus; }
            set { m_Bonus = value; InvalidateProperties(); }
        }

        public override void OnAdded(object parent)
        {
            base.OnAdded(parent);

            if (parent != null && parent is PlayerMobile)
            {
				PlayerMobile pm = parent as PlayerMobile;
                pm.FollowersMax += m_Bonus;
                pm.SendMessage(78, "You feel like you could command more creatures now!");
            }
        }

        public override bool VerifyMove(Mobile from)
        {
            if (from != null && from is PlayerMobile && Parent != null && Parent is PlayerMobile)
            {
                PlayerMobile pm = (PlayerMobile)from;

                if (pm.Followers > (pm.FollowersMax - m_Bonus))
                {
                    pm.SendMessage(37, "You will need to reduce the number of followers you have before you can remove that item!");
                    return false;
                }
            }

            return base.VerifyMove(from);
        }

        public override DeathMoveResult OnParentDeath(Mobile parent)
        {
            if (parent != null && parent is PlayerMobile)
            {
				PlayerMobile pm = parent as PlayerMobile;

                if (pm.Followers > (pm.FollowersMax - m_Bonus))
                    return DeathMoveResult.RemainEquiped;
            }

            return base.OnParentDeath(parent);
        }

        public override void OnRemoved(object parent)
        {
            if (parent != null && parent is PlayerMobile)
            {
                PlayerMobile pm = (PlayerMobile)parent;
                pm.FollowersMax -= m_Bonus;
                pm.SendMessage(37, "You feel like you cannot command as many creatures as before.");
            }

            base.OnRemoved(parent);
        }

        public BaseClothingOfCommand(int bonus, int itemID) : base(itemID)
        {
            m_Bonus = bonus;
            LootType = LootType.Regular;
        }

        public BaseClothingOfCommand(Serial serial) : base(serial)
        {
        }

        public override void GetProperties(ObjectPropertyList list)
        {
            base.GetProperties(list);

            if (m_Bonus != 0)
                list.Add(1060658, "Followers Bonus\t+{0}", m_Bonus);
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0);
            writer.Write(m_Bonus);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();

            switch (version)
            {
                case 0:
                    m_Bonus = reader.ReadInt();
                    break;
            }
        }
    }

    public class BodySashOfCommand : BaseClothingOfCommand
    {
        [Constructable]
        public BodySashOfCommand(int bonus) : base(bonus, 0x1541)
        {
            Weight = 1.0;
            Name = "Body Sash of Command";
            Hue = Utility.RandomMinMax(1150, 1175);
            Layer = Layer.MiddleTorso;
        }

        [Constructable]
        public BodySashOfCommand() : this(1)
        {
        }

        public BodySashOfCommand(Serial serial) : base(serial)
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
    }

    public class CloakOfCommand : BaseClothingOfCommand
    {
        [Constructable]
        public CloakOfCommand(int bonus) : base(bonus, 0x1515)
        {
            Weight = 5.0;
            Name = "Cloak of Command";
            Hue = Utility.RandomMinMax(1150, 1175);
            Layer = Layer.Cloak;
        }

        [Constructable]
        public CloakOfCommand() : this(1)
        {
        }

        public CloakOfCommand(Serial serial) : base(serial)
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
    }
}