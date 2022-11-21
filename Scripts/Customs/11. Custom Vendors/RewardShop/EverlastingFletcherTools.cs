using System;
using Server;
using Server.Engines.Craft;

namespace Server.Items
{
	[FlipableAttribute( 0x1022, 0x1023 )]
	public class EverlastingFletcherTools : BaseTool
	{
	
        [CommandProperty(AccessLevel.GameMaster)]
        public bool IsInfinite
        {
            get { return UsesRemaining > 9999; }
            set
            {
                UsesRemaining = value ? int.MaxValue : 50;
                ShowUsesRemaining = false;
            }
        }
		public override CraftSystem CraftSystem{ get{ return DefBowFletching.CraftSystem; } }

		[Constructable]
		public EverlastingFletcherTools() : base( 0x1022 )
		{
			Weight = 2.0;
			Hue = 1081;
			Name = "Everlasting Fletcher Tools";
			//ShowUsesRemaining = false;
			IsInfinite = true;
		}


		[Constructable]
		public EverlastingFletcherTools( int uses ) : base( uses, 0x1022 )
		{
			Weight = 2.0;
		}


		public EverlastingFletcherTools( Serial serial ) : base( serial )
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

			if ( Weight == 1.0 )
				Weight = 2.0;
		}
	}
}