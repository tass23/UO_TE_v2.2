using System;

namespace Server.Items
{
	public class PerfectEmeraldRing : GoldRing
	{
		public override int LabelNumber{ get{ return 1073459; } } // perfect emerald ring

		[Constructable]
		public PerfectEmeraldRing() : base()
		{
			Weight = 1.0;
			
			BaseRunicTool.ApplyAttributesTo( this, Utility.RandomMinMax( 2, 4 ), 0, 100 );
			
			if ( Utility.RandomBool() )
				Resistances.Poison += 10;	
			else
				Attributes.SpellDamage += 5;
		}

		public PerfectEmeraldRing( Serial serial ) : base( serial )
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
