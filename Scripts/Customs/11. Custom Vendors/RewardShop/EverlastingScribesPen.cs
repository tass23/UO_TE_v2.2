using System;
using Server;
using Server.Engines.Craft;

namespace Server.Items
{
	[FlipableAttribute( 0x0FBF, 0x0FC0 )]
	public class EverlastingScribesPen : BaseTool
	{
        [CommandProperty(AccessLevel.GameMaster)]	//Adds infinite uses
        public bool IsInfinite
        {
            get { return UsesRemaining > 9999; }
            set
            {
                UsesRemaining = value ? int.MaxValue : 50;
                ShowUsesRemaining = false;
            }
        }
		public override CraftSystem CraftSystem{ get{ return DefInscription.CraftSystem; } }

		public override int LabelNumber{ get{ return 1044168; } } // scribe's pen

		[Constructable]
		public EverlastingScribesPen() : base( 0x0FBF )
		{
			Weight = 1.0;
			Hue = 1081;
			Name = "Everlasting Scribe's Pen";
			//ShowUsesRemaining = false;
			IsInfinite = true;
		}


		[Constructable]
		public EverlastingScribesPen( int uses ) : base( uses, 0x0FBF )
		{
			Weight = 1.0;
		}


		public EverlastingScribesPen( Serial serial ) : base( serial )
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

			if ( Weight == 2.0 )
				Weight = 1.0;
		}
	}
}