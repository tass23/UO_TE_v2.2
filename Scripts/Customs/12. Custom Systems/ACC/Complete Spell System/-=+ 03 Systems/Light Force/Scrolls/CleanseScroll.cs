using System;
using Server;
using Server.Items;

namespace Server.ACC.CSS.Systems.LightForce
{
	public class CleanseDisc : CSpellScroll
	{
		[Constructable]
		public CleanseDisc() : this( 1 )
		{
		}

		[Constructable]
		public CleanseDisc( int amount ) : base( typeof( CleanseSpell ), 0x01CB, amount )
		{
			Name = "Force Cleanse";
			Hue = 1185;
		}

		public CleanseDisc( Serial serial ) : base( serial )
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
