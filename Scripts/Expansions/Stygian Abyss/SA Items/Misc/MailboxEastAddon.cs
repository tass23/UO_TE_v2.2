using System;
using System.Collections;
using Server.Multis;
using Server.Mobiles;
using Server.Network;
using System.Collections.Generic;
using Server.ContextMenus;
using Server;
using Server.Items;

namespace Server.Items
{
	public class MailboxEastAddon : BaseAddonContainer
	{
		public override BaseAddonContainerDeed Deed{ get{ return new MailboxEastAddonDeed(); } }
		public override int LabelNumber{ get{ return 1113927; } } // Mailbox (east)
		public override bool RetainDeedHue{ get{ return true; } }
		public override int DefaultGumpID{ get{ return 0x11A; } }
		public override int DefaultDropSound{ get{ return 0x42; } }

        private Mobile m_Owner;
        public Mobile Owner
        {
            get { return m_Owner; }
            set { m_Owner = value; InvalidateProperties(); }
        }

		[Constructable]
		public MailboxEastAddon() : base( 0x4144 )
		{
			//AddComponent( new LocalizedContainerComponent( 1113927 ), 0, -1, 0 );
		}

        public override void OnDoubleClick(Mobile from)
        {
            if (Owner == null)// && IsChildOf(from.Backpack))
            {
                Owner = from;
                from.SendMessage("You now own this mailbox!");
                base.OnDoubleClick(from);
            }
            else if (Owner == null)
            {
                from.SendMessage("You can only set your self as owner of this mailbox if it is in your house!");
            }
            else if (!IsSecure && IsAccessibleTo(from))
                base.OnDoubleClick(from);
            else if (Owner == from || from.AccessLevel > Owner.AccessLevel)
                base.OnDoubleClick(from);
            else
                from.SendMessage("This is not your mailbox, you must be the owner to open it!");
        }

        public override void GetProperties(ObjectPropertyList list)
        {
            base.GetProperties(list);
            list.Add(1072304, Owner == null ? "nobody" : Owner.Name);
        }

		public MailboxEastAddon( Serial serial ) : base( serial )
		{
		}

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.WriteEncodedInt(1); // version
            writer.Write(m_Owner != null);
            if (m_Owner != null) writer.Write(m_Owner);

        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadEncodedInt();
            if (reader.ReadBool()) m_Owner = reader.ReadMobile();

        }
    }

	public class MailboxEastAddonDeed : BaseAddonContainerDeed
	{
		public override BaseAddonContainer Addon{ get{ return new MailboxEastAddon(); } }
		public override int LabelNumber{ get{ return 1113927; } } // Mailbox (East)

		[Constructable]
		public MailboxEastAddonDeed() : base()
		{
		}

		public MailboxEastAddonDeed( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.WriteEncodedInt( 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadEncodedInt();
		}
	}
}
