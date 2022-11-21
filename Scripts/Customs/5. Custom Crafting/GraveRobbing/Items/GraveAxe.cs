using System;
using Server;
using Server.Engines.Harvest;

namespace Server.Items
{
	public class GraveAxe : BaseAxe
	{
		public override HarvestSystem HarvestSystem { get { return GraveRobbing.System; } }

		public override int AosStrengthReq{ get{ return 50; } }
		public override int AosMinDamage{ get{ return 13; } }
		public override int AosMaxDamage{ get{ return 15; } }
		public override int AosSpeed{ get{ return 35; } }
		public override float MlSpeed{ get{ return 3.00f; } }
		
		public override int OldStrengthReq{ get{ return 25; } }
		public override int OldMinDamage{ get{ return 1; } }
		public override int OldMaxDamage{ get{ return 15; } }
		public override int OldSpeed{ get{ return 35; } }

		public override WeaponAnimation DefAnimation{ get{ return WeaponAnimation.Slash1H; } }

		[Constructable]
		public GraveAxe() : this( 50 )
		{
			Name = "Grave Robbers Axe";
		}

		[Constructable]
		public GraveAxe( int uses ) : base( 0xE86 )
		{
			Weight = 11.0;
			Hue = 88;
			UsesRemaining = uses;
			ShowUsesRemaining = true;
		}

		public GraveAxe( Serial serial ) : base( serial )
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