using System;
using Server.Items;
using Server.Spells;
using Server.Gumps;

namespace Server.ACC.CSS.Systems.Vampire
{
    public class CovenSpellbook : CSpellbook
    {
        public override School School { get { return School.Vampire; } }

        [Constructable]
        public CovenSpellbook()
            : this((ulong)0, CSSettings.FullSpellbooks)
        {
        }

        [Constructable]
        public CovenSpellbook(bool full)
            : this((ulong)0, full)
        {
        }

        [Constructable]
        public CovenSpellbook(ulong content, bool full)
            : base(content, 0xEFA, full)
        {
            Hue = 1464;
            Name = "an Ancient Coven Spellbook";
        }

        public override void OnDoubleClick(Mobile from)
        {
            if (from.AccessLevel == AccessLevel.Player)
            {
                //Container pack = from.Backpack;
                //if (!(Parent == from || (pack != null && Parent == pack)))
                //{
                    //from.SendMessage("The spellbook must be in your backpack [and not in a container within] to open.");
                    //return;
                //}
                //else
				if (SpellRestrictions.UseRestrictions && !SpellRestrictions.CheckRestrictions(from, this.School))
                {
                    return;
                }
            }

			from.CloseGump(typeof(AncientCovenMiniGump));
            from.CloseGump(typeof(CovenSpellbookGump));
            from.SendGump(new CovenSpellbookGump(this));
        }

        public CovenSpellbook(Serial serial)
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
