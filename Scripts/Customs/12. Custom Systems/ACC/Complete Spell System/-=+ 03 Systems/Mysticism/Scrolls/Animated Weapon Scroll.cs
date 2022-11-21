using System;
using Server;
using Server.Items;

namespace Server.ACC.CSS.Systems.Mysticism
{
	public class AnimatedWeaponScroll : CSpellScroll
	{
		[Constructable]
		public AnimatedWeaponScroll() : this( 1 )
		{
		}

		[Constructable]
		public AnimatedWeaponScroll( int amount ) : base( typeof( AnimatedWeaponSpell ), 0x2DA4, amount )
		{
			Name = "Animated Weapon Scroll";
		}

		public AnimatedWeaponScroll( Serial serial ) : base( serial )
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
