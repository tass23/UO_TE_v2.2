using System;
using Server;
using Server.Items;

namespace Server.ACC.CSS.Systems.Vampire
{
	public class VConcealmentScroll : CSpellScroll
	{

		[Constructable]
		public VConcealmentScroll() : base( typeof( VConcealmentSpell ), 0x0EF5 )
		{
			Name = "Shadow Vale";
			Hue = 1464;
			Stackable = true;
		}

		public VConcealmentScroll( Serial serial ) : base( serial )
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
