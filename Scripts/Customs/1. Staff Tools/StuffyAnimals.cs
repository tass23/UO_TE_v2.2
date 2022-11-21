/*
Script Name: StuffyAnimals.cs
Author: CEO
Version: 1.0
Public Release: 02/12/08
*/                                                            
using System; 
using Server;

namespace Server.Items
{
    public class StuffyAnimal : Item
    {
        private int m_Type = 0;
        public enum StuffyType { Mongbat, Horse, Bear, Dwagon }

        private int[] m_StuffyID = new int[] { 0x20F9, 0x211F, 0x211E, 0x20D6 }; // Mongbat, Horse, Bear, Dwagon 
        private DateTime m_LastClicked = DateTime.Now - TimeSpan.FromSeconds(20); // To protect rampant clicking....

        public override int LabelNumber { get { return 1079984 + (int)m_Type; } }

        [CommandProperty(AccessLevel.GameMaster)]
        public StuffyType Type
        {
            get { return (StuffyType) m_Type; }
            set {
                m_Type = (int) value;
                ItemID = m_StuffyID[m_Type];
            }
        }

        [Constructable]
        public StuffyAnimal()
            : base()
        {
            m_Type = Utility.Random(4);
            Weight = 1.0;
            ItemID = m_StuffyID[m_Type];
            Hue = Utility.RandomList(11, 31, 32, 665, 667, 1125, 1150);
            LootType = LootType.Blessed;
        }

        public StuffyAnimal(Serial serial)
            : base(serial)
        {
        }


        public override void OnDoubleClick(Mobile m)
        {
            if (!IsChildOf(m.Backpack))
            {
                // The item must be in your backpack to use it.
                m.SendLocalizedMessage(1060640);
            }
            else if (m_LastClicked > DateTime.Now)
            {
                int waittime = (int)((m_LastClicked - DateTime.Now).TotalSeconds) + 1;
                // You must wait xx seconds before you can use it.
                m.SendLocalizedMessage(1079263, waittime.ToString());
            }
            else
                m_LastClicked = StuffyClicked(Hue, m, m_Type);
        }

        private static DateTime StuffyClicked(int Hue, Mobile m, int stuffy)
        {
            m.SendLocalizedMessage(1079979 + Utility.Random(5));
            switch (stuffy)
            {
                case 0: // Mongbat
                    m.FixedParticles(0x376A, 10, 15, 5012, Hue - 1, 0, EffectLayer.Waist);
                    m.PlaySound(492);
                    break;
                case 1: // Horse
                    m.FixedParticles(0x37C4, 10, 15, 5012, Hue - 1, 0, EffectLayer.Waist);
                    m.PlaySound(0x1E2);
                    break;
                case 2: // Bear
                    m.FixedParticles(0x373A, 10, 15, 5012, Hue - 1, 0, EffectLayer.Waist);
                    m.PlaySound(0x1E9);
                    break;
                default: // Dwagon
                    m.FixedParticles(0x375A, 10, 15, 5012, Hue - 1, 0, EffectLayer.Waist);
                    m.PlaySound(0x213);
                    break;
            }
            return (DateTime.Now + TimeSpan.FromSeconds(59));
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0); // version 
            writer.Write(m_Type);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
            m_Type = reader.ReadInt();
        }
    }
}