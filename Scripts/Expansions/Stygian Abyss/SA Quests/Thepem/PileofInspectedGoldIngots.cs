/*                                                             .---.
                                                              /  .  \
                                                             |\_/|   |
                                                             |   |  /|
  .----------------------------------------------------------------' |
 /  .-.                                                              |
|  /   \         Contribute To The Orbsydia SA Project               |
| |\_.  |                                                            |
|\|  | /|                        By Lotar84                          |
| `---' |                                                            |
|       |       (Orbanised by Orb SA Core Development Team)          | 
|       |                                                           /
|       |----------------------------------------------------------'
\       |
 \     /
  `---'
*/
using System;
using Server;

namespace Server.Items
{
	public class PileofInspectedGoldIngots : Item
	{
        public override int LabelNumber { get { return 1113027; } }//Pile of Inspected Gold Ingots

		[Constructable]
		public PileofInspectedGoldIngots() : base( 0x1BEA )
		{
            Hue = 2213;
		}

		public PileofInspectedGoldIngots( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}
		
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}
}