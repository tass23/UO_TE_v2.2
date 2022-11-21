using System;
using Server;
using Server.Spells.Spellweaving;
using Server.Targeting;

namespace Server.Items
{
	public class WildfireWand : BaseSpellWeavingWand
	{
		[Constructable]
		public WildfireWand() : base( SpellWeavingWandEffect.Wildfire, 5, 10 )
		{
		}

		public WildfireWand( Serial serial ) : base( serial )
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

		public override void OnSpellWeavingWandUse( Mobile from )
		{
			Cast( new WildfireSpell( from, this ) );
		}
	}
}