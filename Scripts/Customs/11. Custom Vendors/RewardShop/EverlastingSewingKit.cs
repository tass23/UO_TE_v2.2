using System;
using Server;
using Server.Engines.Craft;

namespace Server.Items
{
	public class EverlastingSewingKit : BaseTool
	{
		public override CraftSystem CraftSystem{ get{ return DefTailoring.CraftSystem; } }

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
		
		[Constructable]
		public EverlastingSewingKit() : base( 0xF9D )
		{
			Weight = 2.0;
			Hue = 1081;
			Name = "Everlasting Sewing Kit";
			//ShowUsesRemaining = false;
			IsInfinite = true;
		}


		[Constructable]
		public EverlastingSewingKit( int uses ) : base( uses, 0xF9D )
		{
			Weight = 2.0;
		}


		public EverlastingSewingKit( Serial serial ) : base( serial )
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