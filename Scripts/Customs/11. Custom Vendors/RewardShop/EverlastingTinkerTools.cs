using System;
using Server;
using Server.Engines.Craft;

namespace Server.Items
{
	[Flipable( 0x1EB8, 0x1EB9 )]
	public class EverlastingTinkerTools : BaseTool
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
		public override CraftSystem CraftSystem{ get{ return DefTinkering.CraftSystem; } }

		[Constructable]
		public EverlastingTinkerTools() : base( 0x1EB8 )
		{
			Weight = 1.0;
			Hue = 1081;
			Name = "Everlasting Tinker Tools";
			//ShowUsesRemaining = false;
			IsInfinite = true;
		}


		[Constructable]
		public EverlastingTinkerTools( int uses ) : base( uses, 0x1EB8 )
		{
			Weight = 1.0;
		}


		public EverlastingTinkerTools( Serial serial ) : base( serial )
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