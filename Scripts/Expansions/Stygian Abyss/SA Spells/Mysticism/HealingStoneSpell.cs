using System;
using Server;
using Server.Items;
using Server.Mobiles;
using Server.Spells;
using Server.Targeting;

namespace Server.Spells.Mystic
{
	public class HealingStoneSpell : MysticSpell
	{
		// Conjures a Healing Stone that will instantly heal the Caster when used.

		public override int RequiredMana{ get{ return 4; } }
		public override double RequiredSkill{ get{ return 0; } }

		private static SpellInfo m_Info = new SpellInfo(
				"Healing Stone", "Kal In Mani",
				230,
				9022,
				Reagent.Bone,
				Reagent.Garlic,
				Reagent.Ginseng,
				Reagent.SpidersSilk
			);

		public HealingStoneSpell( Mobile caster, Item scroll ) : base( caster, scroll, m_Info )
		{
		}

		public override void OnCast()
		{
			if ( Caster.Backpack != null )
			{
				Item[] stones = Caster.Backpack.FindItemsByType( typeof( HealingStone ) );

				for ( int i = 0; i < stones.Length; i++ )
					stones[i].Delete();

				int amount = (int)(Caster.Skills[DamageSkill].Value / 10);
				Caster.PlaySound( 0x651 );
				Caster.Backpack.DropItem( new HealingStone( Caster, amount ) );
				Caster.SendLocalizedMessage( 1080115 ); // A Healing Stone appears in your backpack.
			}
		}
	}
}
/*




*/