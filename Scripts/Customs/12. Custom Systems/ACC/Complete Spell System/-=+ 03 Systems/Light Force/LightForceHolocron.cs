using System;
using Server.Items;
using Server.Spells;
using Server.Gumps;

namespace Server.ACC.CSS.Systems.LightForce
{
    public class JediHolocron : CSpellbook
    {
        public override School School { get { return School.LightForce; } }

        [Constructable]
        public JediHolocron()
            : this((ulong)0, CSSettings.FullSpellbooks)
        {
        }

        [Constructable]
        public JediHolocron(bool full)
            : this((ulong)0, full)
        {
        }

        [Constructable]
        public JediHolocron(ulong content, bool full)
            : base(content, 0x239F, full)
        {
            Hue = 0;
            Name = "a Jedi Holocron";
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

			from.CloseGump(typeof(JediHolocronMiniGump));
            from.CloseGump(typeof(JediHolocronGump));
            from.SendGump(new JediHolocronGump(this));
        }

        public JediHolocron(Serial serial)
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
