using System; 
using Server; 

namespace Server.Items 
{
	public class KingsSatchel : BaseContainer
	{
		[Constructable]
		public KingsSatchel() : base( 0xE75 )
		{
			Weight = 1.0;
			Name = "King Arthur's Satchel";
			Hue = 1154;

			AddItem( new HolyHandgrenade2() );
			AddItem( new HolyHandgrenade2() );
			AddItem( new HolyHandgrenade2() );
			AddItem( new HolyHandgrenade2() );
			AddItem( new HolyHandgrenade2() );
		}

		public KingsSatchel( Serial serial ) : base( serial )
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