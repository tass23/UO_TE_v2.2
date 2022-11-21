using System;
using Server;
using Server.Engines.Craft;

namespace Server.Items
{
	[FlipableAttribute( 0x13E3, 0x13E4 )]
	public class EverlastingSmithHammer : BaseTool
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
		public override CraftSystem CraftSystem{ get{ return DefBlacksmithy.CraftSystem; } }

		[Constructable]
		public EverlastingSmithHammer() : base( 0x13E3 )
		{
			Weight = 8.0;
			Layer = Layer.OneHanded;
			Hue = 1081;
			Name = "Everlasting Smith Hammer";
			//ShowUsesRemaining = false;
			IsInfinite = true;
		}


		[Constructable]
		public EverlastingSmithHammer( int uses ) : base( uses, 0x13E3 )
		{
			Weight = 8.0;
			Layer = Layer.OneHanded;
		}


		public EverlastingSmithHammer( Serial serial ) : base( serial )
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