using System;
using Server;
using Server.Network;
using Server.Prompts;
using Server.Items;
using Server.Guilds;
using Server.Gumps;
using Server.Mobiles;
using Server.Targeting;
using Server.ACC.CSS;
using Server.ACC.CSS.Systems.Chivalry;
using Server.ACC.CSS.Systems.Mage;
using Server.ACC.CSS.Systems.Necromancy;
using Server.ACC.CSS.Systems.Mysticism;
using Server.ACC.CSS.Systems.Spellweaving;
using Server.ACC.CSS.Systems.LightForce;
using Server.ACC.CSS.Systems.DarkForce;
using Server.ACC.CSS.Systems.Vampire;
using Server.ACC.CSS.Systems.Werewolf;

namespace Server.Misc
{
    public class AdvancedSpellbookDeed : Item // Create the item class which is derived from the base item class
    {
        [Constructable]
        public AdvancedSpellbookDeed()
            : base(0x14F0)
        {
            Weight = 1.0;
            Name = "Advanced Spellbook Deed";
            Hue = 0x2B;
        }

        public AdvancedSpellbookDeed(Serial serial)
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

        public override bool DisplayLootType { get { return false; } }

        public override void OnDoubleClick(Mobile from) // Override double click of the deed to call our target
        {
            if (!IsChildOf(from.Backpack)) // Make sure its in their pack
            {
                from.SendLocalizedMessage(1042001); // That must be in your pack for you to use it.
            }
            else
            {
                from.SendGump(new AdvancedSpellbookGump(from, this));
            }
        }
    }
    public class AdvancedSpellbookGump : Gump
    {
        private Item m_Deed;
        private Mobile m_Mobile;

        public AdvancedSpellbookGump(Mobile from, Item deed)
            : base(20, 20)
        {
            m_Mobile = from;
            m_Deed = deed;
            {
                this.Closable = true;
                this.Disposable = false;
                this.Dragable = true;
                this.Resizable = false;
                this.AddPage(0);
                this.AddBackground(34, 17, 350, 500, 5054);
                this.AddLabel(128, 38, 0, @"Choose your Advanced Spellbook");
                this.AddTextEntry(100, 132, 226, 20, 0, (int)Buttons.TextEntry1, @"Magery       ");
                this.AddTextEntry(100, 212, 213, 20, 0, (int)Buttons.TextEntry2, @"Chivalry    ");
                this.AddTextEntry(100, 292, 213, 20, 0, (int)Buttons.TextEntry3, @"Mysticism    ");
                this.AddTextEntry(100, 372, 213, 20, 0, (int)Buttons.TextEntry4, @"Vampire     ");
                this.AddTextEntry(100, 452, 213, 20, 0, (int)Buttons.TextEntry5, @"Jedi        ");
                this.AddTextEntry(225, 132, 90, 20, 0, (int)Buttons.TextEntry6, @"Necromancy   ");
                this.AddTextEntry(225, 212, 90, 20, 0, (int)Buttons.TextEntry7, @"Spellweaving");
                this.AddTextEntry(225, 372, 86, 19, 0, (int)Buttons.TextEntry8, @"Werewolf    ");
                this.AddTextEntry(225, 452, 78, 20, 0, (int)Buttons.TextEntry9, @"Sith        ");
                this.AddButton(118, 77, 2234, 248, 1, GumpButtonType.Reply, 0);//Magery
                this.AddButton(250, 77, 11011, 248, 2, GumpButtonType.Reply, 0);//Necromancy
                this.AddButton(118, 160, 11012, 248, 3, GumpButtonType.Reply, 0);//Chivalry
                this.AddButton(118, 246, 11056, 248, 4, GumpButtonType.Reply, 0);//Mysticism
                this.AddButton(250, 160, 11053, 248, 5, GumpButtonType.Reply, 0);//Spellweaving
                this.AddButton(118, 326, 2234, 248, 6, GumpButtonType.Reply, 0);//Vampire
                this.AddButton(118, 406, 2234, 248, 8, GumpButtonType.Reply, 0);//Jedi
                this.AddButton(250, 326, 10450, 248, 7, GumpButtonType.Reply, 0);//Werewolf
                this.AddButton(250, 406, 2234, 248, 9, GumpButtonType.Reply, 0);//Sith
				this.AddImage(118, 326, 11011, 2983); //Vampire
				this.AddImage(118, 406, 11011, 2941); //Jedi
				this.AddImage(250, 326, 10450, 2983); //Werewolf
				this.AddImage(250, 406, 2234, 2944); //Sith
                this.AddButton(298, 460, 247, 248, 0,  GumpButtonType.Reply, 0);//Close
            }
        }

        public enum Buttons
        {
            TextEntry1,
            TextEntry2,
            TextEntry3,
			TextEntry4,
			TextEntry5,
			TextEntry6,
			TextEntry7,
			TextEntry8,
			TextEntry9,
            Button0,
            Button1,
            Button2,
            Button3,
            Button4,
            Button5,
            Button6,
            Button7,
            Button8,
            Button9,
        }
        public override void OnResponse(NetState state, RelayInfo info)
        {
            Mobile from = state.Mobile;
            //EthyDeed ed = ed.Delete();
            switch (info.ButtonID)
            {
                case 0:
                    {
                        from.CloseGump(typeof(AdvancedSpellbookGump));
                        break;
                    }
                case 1:
                    {
                        Item item = new MageSpellbook();
                        from.AddToBackpack(item);
                        from.CloseGump(typeof(AdvancedSpellbookGump));
						m_Deed.Delete();
                        break;
                    }

                case 2:
                    {
                        Item item = new NecroSpellbook();
                        from.AddToBackpack(item);
                        from.CloseGump(typeof(AdvancedSpellbookGump));
						m_Deed.Delete();
                        break;
                    }
                case 3:
                    {
                        Item item = new ChivalrySpellbook( from );
                        from.AddToBackpack(item);
                        from.CloseGump(typeof(AdvancedSpellbookGump));
						m_Deed.Delete();
                        break;
                    }
                case 4:
                    {
                        Item item = new MysticismSpellbook();
                        from.AddToBackpack(item);
                        from.CloseGump(typeof(AdvancedSpellbookGump));
						m_Deed.Delete();
                        break;
                    }
                case 5:
                    {
                        Item item = new SpellweavingSpellbook();
                        from.AddToBackpack(item);
                        from.CloseGump(typeof(AdvancedSpellbookGump));
						m_Deed.Delete();
                        break;
                    }
                case 6:
                    {
                        Item item = new CovenSpellbook();
                        from.AddToBackpack(item);
                        from.CloseGump(typeof(AdvancedSpellbookGump));
						m_Deed.Delete();
                        break;
                    }
                case 7:
                    {
                        Item item = new LycanPrimer();
                        from.AddToBackpack(item);
                        from.CloseGump(typeof(AdvancedSpellbookGump));
						m_Deed.Delete();
                        break;
                    }
                case 8:
                    {
                        Item item = new JediHolocron();
                        from.AddToBackpack(item);
                        from.CloseGump(typeof(AdvancedSpellbookGump));
						m_Deed.Delete();
                        break;
                    }
                case 9:
                    {
                        Item item = new SithHolocron();
                        from.AddToBackpack(item);
                        from.CloseGump(typeof(AdvancedSpellbookGump));
						m_Deed.Delete();
                        break;
                    }
            }
        }
    }
}
