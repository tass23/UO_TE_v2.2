using System;

namespace Server.Items
{
	public class QuestMap : Item
	{
		[Constructable]
		public QuestMap( ) : base( 5355 )
		{
			Weight = 1.0;
			Name = "a Map to Atlantis";
			Hue = 2984;
		}
		
		public QuestMap( Serial serial ) : base( serial )
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
		
		public override void OnDoubleClick( Mobile from )
		{
			{
				from.SendMessage( "You pick up a copy of the map and put it in your backpack." );
				from.AddToBackpack( new AtlantisMap());
			}
		}
	}
}