using System;
using Server;

namespace Server.Items
{
	public class DirtySheets : Item
	{
        [Constructable]
		public DirtySheets() : this( 0 )
		{
		}

		[Constructable]
		public DirtySheets(int hue) : base( 0xA6C)
		{
			Name = "dirty sheets";
 			Movable = true;
            Hue = hue;
		}

        public override void OnDoubleClick(Mobile from)
        {
            from.SendMessage("You should wash these before using again.");
        }

		public DirtySheets( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}
}
