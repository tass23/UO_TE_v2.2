using System;
using Server.Items;
using Server.Spells;
using Server.Gumps;

namespace Server.ACC.CSS.Systems.DarkForce
{
    public class SithHolocron : CSpellbook
    {
        public override School School { get { return School.DarkForce; } }

        [Constructable]
        public SithHolocron()
            : this((ulong)0, CSSettings.FullSpellbooks)
        {
        }

        [Constructable]
        public SithHolocron(bool full)
            : this((ulong)0, full)
        {
        }

        [Constructable]
        public SithHolocron(ulong content, bool full)
            : base(content, 0x239E, full)
        {
            Hue = 2945;
            Name = "a Sith Holocron";
        }

        public override void OnDoubleClick(Mobile from)
        {
            if (from.AccessLevel == AccessLevel.Player)
            {
                //Container pack = from.Backpack;
                //if (!(Parent == from || (pack != null && Parent == pack)))
                //{
                    //from.SendMessage("The holocron must be in your backpack [and not in a container within] to open.");
                    //return;
                //}
                //else
				if (SpellRestrictions.UseRestrictions && !SpellRestrictions.CheckRestrictions(from, this.School))
                {
                    return;
                }
            }

			from.CloseGump(typeof(SithHolocronMiniGump));
            from.CloseGump(typeof(SithHolocronGump));
            from.SendGump(new SithHolocronGump(this));
        }

        public SithHolocron(Serial serial)
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
