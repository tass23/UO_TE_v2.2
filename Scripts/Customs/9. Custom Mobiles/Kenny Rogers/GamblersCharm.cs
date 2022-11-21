using System;
using Server;

namespace Server.Items
{
	public class GamblersCharm : BaseNecklace
	{
		
		public override int ArtifactRarity{ get{ return 7; } }
        
        [Constructable]
		public GamblersCharm() : base( 0x1085 ) 
		{
			Weight = 1.0; 
            Name = "Gamblers Charm"; 
            Hue = 50;

			Attributes.Luck = 777;
			
		}

		public override void OnAdded( object parent )
		{
			base.OnAdded( parent );
			if( parent is Mobile )
			{
				Mobile from = (Mobile)parent;
				from.Skills.Magery.Base += 20;
			}
		}
		public override void OnRemoved( object parent )
		{
			base.OnRemoved( parent );
			if( parent is Mobile )
			{
				Mobile from = (Mobile)parent;
				from.Skills.Magery.Base -= 20;
			}
			
		}

		public GamblersCharm( Serial serial ) : base( serial )
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