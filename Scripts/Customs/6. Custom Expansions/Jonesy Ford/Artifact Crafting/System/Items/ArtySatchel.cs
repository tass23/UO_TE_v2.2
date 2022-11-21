using System;
using Server;
using Server.Items;
using Server.Engines.Craft;
using Server.Engines.Quests;
using Reward = Server.Engines.Quests.BaseReward;

namespace Server.Items
{
	public class BaseArtySatchel : Backpack
	{
		public BaseArtySatchel() : base()
		{
			Hue = Reward.SatchelHue();

		}
		
		public BaseArtySatchel( Serial serial ) : base( serial )
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


	public class ArtySatchel : BaseArtySatchel
	{
		[Constructable]
		public ArtySatchel() : base()
		{
			Name = "an Artifact Satchel";
			
			DropItem( Reward.ArtyRecipe() );
			if ( 0.1 > Utility.RandomDouble() )
				DropItem( Reward.Transmogrifier() );
		}
		
		public ArtySatchel( Serial serial ) : base( serial )
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
	
	public class ArtySatchel2 : BaseArtySatchel
	{
		[Constructable]
		public ArtySatchel2() : base()
		{
			Name = "an Artifact Satchel";
			
			DropItem( Reward.ArtyRecipe() );
			DropItem( Reward.ArtyRecipe() );
			if ( 0.5 > Utility.RandomDouble() )
				DropItem( Reward.Transmogrifier() );
		}
		
		public ArtySatchel2( Serial serial ) : base( serial )
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