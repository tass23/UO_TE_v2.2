using System;
using Server;

namespace Server.Items
{
	public class EssenceOfLove : Item
	{

		[Constructable]
		public EssenceOfLove() : base( 0x1C18 )
		{
			Name = "Essence Of Love";
			Weight = 0.5;
			Hue = 1157;
		}

		public EssenceOfLove( Serial serial ) : base( serial )
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