using System;
using Server;
using Server.Items;

namespace Server.ACC.CSS.Systems.Vampire
{
	public class VStrengthScroll : CSpellScroll
	{

		[Constructable]
		public VStrengthScroll() : base( typeof( VStrengthSpell ), 0x0EF5 )
		{
			Name = "Super Strength";
			Hue = 1464;
			Stackable = true;
		}

		public VStrengthScroll( Serial serial ) : base( serial )
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
