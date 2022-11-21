/*
Script Name: HorseCoconuts2.cs
Author: CEO
Version: 1.0
Public Release: 02/12/08
*/                                                            
using System; 
using Server;

namespace Server.Items
{
    public class HorseCoconuts2 : Item
    {
        private int m_Type = 0;
        public enum StuffyType { Coconuts }
        private int[] m_StuffyID = new int[] { 0x1725 }; // Coconuts of Clapping
        private DateTime m_LastClicked = DateTime.Now - TimeSpan.FromSeconds(10); // To protect rampant clicking....

        [CommandProperty(AccessLevel.GameMaster)]
        public StuffyType Type
        {
            get { return (StuffyType) m_Type; }
            set
			{
                m_Type = (int) value;
                ItemID = m_StuffyID[m_Type];
            }
        }

        [Constructable]
        public HorseCoconuts2(): base()
        {
            m_Type = Utility.Random(1);
            Weight = 1.0;
            ItemID = m_StuffyID[m_Type];
            Name = "Coconuts of Clapping";
            Hue = Utility.RandomList(1052, 1053, 1054, 1055, 1056, 1057, 1058);
            LootType = LootType.Blessed;
        }

        public HorseCoconuts2(Serial serial): base(serial)
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
            switch (stuffy)
            {
                case 0: // Mongbat
                    m.FixedParticles(0x376A, 10, 15, 5012, Hue - 1, 0, EffectLayer.Waist);
                    m.PlaySound(871);
                    break;
                case 1: // Horse
                    m.FixedParticles(0x37C4, 10, 15, 5012, Hue - 1, 0, EffectLayer.Waist);
                    m.PlaySound(169);
                    break;
                case 2: // Bear
                    m.FixedParticles(0x373A, 10, 15, 5012, Hue - 1, 0, EffectLayer.Waist);
                    m.PlaySound(170);
                    break;
                case 3: // Horse
                    m.FixedParticles(0x37C4, 10, 15, 5012, Hue - 1, 0, EffectLayer.Waist);
                    m.PlaySound(1213);
                    break;
                case 4: // Bear
                    m.FixedParticles(0x373A, 10, 15, 5012, Hue - 1, 0, EffectLayer.Waist);
                    m.PlaySound(1212);
                    break;
                default: // Dwagon
                    m.FixedParticles(0x375A, 10, 15, 5012, Hue - 1, 0, EffectLayer.Waist);
                    m.PlaySound(874);
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