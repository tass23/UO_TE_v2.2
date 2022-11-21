using System;
using Server;
using Server.Network;
using Server.Mobiles;

namespace Server.Items
{
	public class EvilClawStatuette : BaseStatuette
	{
		[Constructable]
		public EvilClawStatuette() : base( 11704 )
		{
			Name = "Ash's Possessed Hand";
			Hue = 1761;
		}

		public EvilClawStatuette( Serial serial ) : base( serial )
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
	
	public class EvilZombiStatuette : BaseStatuette
	{
		[Constructable]
		public EvilZombiStatuette() : base( 8428 )
		{
			Name = "Deadlight Zombie";
			Hue = 963;
		}

		public EvilZombiStatuette( Serial serial ) : base( serial )
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