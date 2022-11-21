using System;
using Server.Items;
using Server.Network;
using Server.Mobiles;
using Server.Gumps;
using System.Collections;
using Server.Misc;

namespace Server.Items
{
    public class MysticalBlessedBackpack : BaseContainer
    {
        public override int DefaultMaxItems { get { return 30; } }
		private Mobile m_Owner;
		
		[CommandProperty( AccessLevel.GameMaster )]
		public Mobile Owner
		{
			get{ return m_Owner; }
			set{ m_Owner = value; }
		}
		
        [Constructable]
        public MysticalBlessedBackpack(): base(0x9b2)
        {
            Name = "Mystical Blessed Backpack";
            Hue = 1164;
			LootType = LootType.Blessed;
			Weight = 2.0;
            this.LootType = LootType.Newbied;
        }
		
        public override void OnItemLifted(Mobile from, Item item)
        {
            base.OnItemLifted(from, item);

            if (this.Owner == null && from is PlayerMobile)
                this.Owner = from;
        }

        public override void OnDoubleClick(Mobile from)
        {
            if (from.InRange(this.GetWorldLocation(), 2))
            {
				if (Owner != null && from != null)
				{
					if (Owner == from || from.AccessLevel > Owner.AccessLevel)
					{
						Container armoire = this;
						ArrayList equippedItems = new ArrayList(from.Items);
						ArrayList armoireItems = new ArrayList(armoire.Items);
						foreach (Item item in equippedItems)
						{
							if ((item.Layer != Layer.Bank) && (item.Layer != Layer.Backpack) && (item.Layer != Layer.Hair) && (item.Layer != Layer.FacialHair) && (item.Layer != Layer.Mount))
							{
								armoire.DropItem(item);
							}
						}
						foreach (Item item in armoireItems)
						{
							from.EquipItem(item);
						}
						//DisplayTo(m);
					}
					else
					DisplayTo(from);
				}
				else
				{
					from.SendMessage(238, "This is not your bag, So now you Die!"); //appears to player not owner change as desired
					Effects.SendBoltEffect(from);
					//Effects.PlaySound( 0x51E );

					Effects.PlaySound(from.Location, from.Map, 0x51E);
				}
            }
            else
            {
                from.LocalOverheadMessage(MessageType.Regular, 0x3B2, 1019045); // I can't reach that.
            }
        }

        public MysticalBlessedBackpack(Serial serial)
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