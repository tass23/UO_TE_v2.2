using System;
using Server;
using Server.Items;

namespace Server.ACC.CSS.Systems.Vampire
{
	public class VFlameScroll : CSpellScroll
	{

		[Constructable]
		public VFlameScroll() : base( typeof( VFlameSpell ), 0x0EF5 )
		{
			Name = "Blood Boil";
			Hue = 1464;
			Stackable = true;
		}

		public VFlameScroll( Serial serial ) : base( serial )
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
