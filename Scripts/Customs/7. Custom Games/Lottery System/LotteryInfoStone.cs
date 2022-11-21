using System;
using System.Text;
using Server.Gumps;
using Server.Items;
using Server.Network;

namespace Server.Items
{
	[Flipable(0x2AED, 0x2ADD)]
	public class LotteryInfoStone : Item
	{
		[Constructable]
		public LotteryInfoStone() : base( 0x2AED )
		{
			Movable = false;
			Hue = 1154;
			Name = "*** The Expanse Lottery Information Stone ***";
		}

        public override void OnDoubleClick(Mobile from)
        {
            if (!from.InRange(this.GetWorldLocation(), 1))
            {
                from.SendMessage( 33, "Step Closer To The Stone, You Are Too Far Away");
                from.PlaySound(0x1F0);
            }
            else
            {
                from.CloseGump(typeof(LotteryInfoGump));
                from.SendGump(new LotteryInfoGump());
                from.PlaySound(0x1ED);
            }
        }

        public LotteryInfoStone(Serial serial) : base(serial)
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