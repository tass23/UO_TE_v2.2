using System;
using Server;
using Server.Spells.Spellweaving;
using Server.Targeting;

namespace Server.Items
{
	public class WordOfDeathWand : BaseSpellWeavingWand
	{
		[Constructable]
		public WordOfDeathWand() : base( SpellWeavingWandEffect.WordOfDeath, 5, 10 )
		{
		}

		public WordOfDeathWand( Serial serial ) : base( serial )
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
			Cast( new WordOfDeathSpell( from, this ) );
		}
	}
}