using Server;
using Server.Items;

namespace Gacoperz.Bricks
{
	public class ProtoBrick : Item
	{

		[Constructable]
		public ProtoBrick(int itemID) : base(itemID)
		{
			Name = "ProtoBrick";
			Movable = false;
		}
		public ProtoBrick(Serial serial): base(serial)
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
		public override void OnDoubleClick( Mobile from)
		{
			from.AddToBackpack(new Brick(ItemID, from));
		}
	};
}