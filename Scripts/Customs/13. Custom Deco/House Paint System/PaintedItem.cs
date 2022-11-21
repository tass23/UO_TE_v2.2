using System;
using Server;

namespace Server.Items
{
    public class PaintedItem : Item
    {

		[Constructable]
		public PaintedItem() : this( 0 )
		{
		}

		[Constructable]
		public PaintedItem( int itemID ) : base( itemID )
		{
		}

        public PaintedItem(Serial serial)
            : base(serial)
		{
        }

        public override bool OnDragLift(Mobile from)
        {
            if (from.AccessLevel >= AccessLevel.GameMaster) return base.OnDragLift(from);
            else return false;
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
