/*   Created by Shai'Tan Malkier aka Callandor2k   */

using System;
using Server;
using Server.Gumps;
using Server.Multis;
using Server.Network;
using Server.Mobiles;
using System.Collections.Generic;

namespace Server.Items
{
    public class SherryTheMouse : Item, ITownCrierEntryList
    {
        public override int LabelNumber { get { return 1080171; } } // Sherry the Mouse Statue

        private bool m_TurnedOn;

        [CommandProperty(AccessLevel.GameMaster)]
        public bool TurnedOn
        {
            get { return m_TurnedOn; }
            set { m_TurnedOn = value; InvalidateProperties(); }
        }

        [Constructable]
        public SherryTheMouse()
            : base(0x20D0)
        {
            Weight = 1.0;

            Hue = 0xB8F;
            LootType = LootType.Blessed;
        }

        public override void GetProperties(ObjectPropertyList list)
        {
            base.GetProperties(list);

            if (m_TurnedOn)
                list.Add(502695); // turned on
            else
                list.Add(502696); // turned off
        }

        public bool IsOwner(Mobile mob)
        {
            BaseHouse house = BaseHouse.FindHouseAt(this);

            return (house != null && house.IsOwner(mob));
        }

        public override void OnDoubleClick(Mobile from)
        {
            if (IsOwner(from))
            {
                OnOffGump onOffGump = new OnOffGump(this);
                from.SendGump(onOffGump);
            }
            else
            {
                from.SendLocalizedMessage(502691); // You must be the owner to use this.
            }
        }

        public SherryTheMouse(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version

            writer.Write((bool)m_TurnedOn);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();

            switch (version)
            {
                case 0:
                    {
                        m_TurnedOn = reader.ReadBool();
                        break;
                    }
            }
        }

        private List<TownCrierEntry> m_Entries;
        private Timer m_NewsTimer;

        public List<TownCrierEntry> Entries
        {
            get { return m_Entries; }
        }

        public override bool HandlesOnSpeech { get { return true; } }

        public override void OnSpeech(SpeechEventArgs e)
        {
            if (m_TurnedOn && IsLockedDown)
            {
                if (m_NewsTimer == null && e.HasKeyword(0x30) && e.Mobile.Alive && e.Mobile.InRange(this, 12)) // *news*
                {
                    TownCrierEntry tce = GetRandomEntry();

                    if (tce == null)
                    {
                        PublicOverheadMessage(MessageType.Regular, 0x3B2, 1005643); // I have no news at this time.
                    }
                    else
                    {
                        m_NewsTimer = Timer.DelayCall(TimeSpan.FromSeconds(1.0), TimeSpan.FromSeconds(3.0), new TimerStateCallback(ShoutNews_Callback), new object[] { tce, 0 });

                        PublicOverheadMessage(MessageType.Regular, 0x3B2, 502978); // Some of the latest news!
                    }
                }
            }
        }

        private void ShoutNews_Callback(object state)
        {
            object[] states = (object[])state;
            TownCrierEntry tce = (TownCrierEntry)states[0];
            int index = (int)states[1];

            if (index < 0 || index >= tce.Lines.Length)
            {
                if (m_NewsTimer != null)
                    m_NewsTimer.Stop();

                m_NewsTimer = null;
            }
            else
            {
                PublicOverheadMessage(MessageType.Regular, 0x3B2, false, tce.Lines[index]);
                states[1] = index + 1;
            }
        }

        public TownCrierEntry AddEntry(string[] lines, TimeSpan duration)
        {
            if (m_Entries == null)
                m_Entries = new List<TownCrierEntry>();

            TownCrierEntry tce = new TownCrierEntry(lines, duration);

            m_Entries.Add(tce);

            return tce;
        }

        public TownCrierEntry GetRandomEntry()
        {
            if (m_Entries == null || m_Entries.Count == 0)
                return GlobalTownCrierEntryList.Instance.GetRandomEntry();

            for (int i = m_Entries.Count - 1; m_Entries != null && i >= 0; --i)
            {
                if (i >= m_Entries.Count)
                    continue;

                TownCrierEntry tce = m_Entries[i];

                if (tce.Expired)
                    RemoveEntry(tce);
            }

            if (m_Entries == null || m_Entries.Count == 0)
                return GlobalTownCrierEntryList.Instance.GetRandomEntry();

            TownCrierEntry entry = GlobalTownCrierEntryList.Instance.GetRandomEntry();

            if (entry == null || Utility.RandomBool())
                entry = m_Entries[Utility.Random(m_Entries.Count)];

            return entry;
        }

        public void RemoveEntry(TownCrierEntry tce)
        {
            if (m_Entries == null)
                return;

            m_Entries.Remove(tce);

            if (m_Entries.Count == 0)
                m_Entries = null;
        }

        private class OnOffGump : Gump
        {
            private SherryTheMouse m_Sherry;

            public OnOffGump(SherryTheMouse sherry)
                : base(150, 200)
            {
                m_Sherry = sherry;

                AddBackground(0, 0, 300, 150, 0xA28);

                AddHtmlLocalized(45, 20, 300, 35, sherry.TurnedOn ? 1011035 : 1011034, false, false); // [De]Activate this item

                AddButton(40, 53, 0xFA5, 0xFA7, 1, GumpButtonType.Reply, 0);
                AddHtmlLocalized(80, 55, 65, 35, 1011036, false, false); // OKAY

                AddButton(150, 53, 0xFA5, 0xFA7, 0, GumpButtonType.Reply, 0);
                AddHtmlLocalized(190, 55, 100, 35, 1011012, false, false); // CANCEL
            }

            public override void OnResponse(NetState sender, RelayInfo info)
            {
                Mobile from = sender.Mobile;

                if (info.ButtonID == 1)
                {
                    bool newValue = !m_Sherry.TurnedOn;
                    m_Sherry.TurnedOn = newValue;

                    if (newValue && !m_Sherry.IsLockedDown)
                        from.SendLocalizedMessage(502693); // Remember, this only works when locked down.
                }
                else
                {
                    from.SendLocalizedMessage(502694); // Cancelled action.
                }
            }
        }
    }
}