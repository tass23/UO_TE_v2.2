using System; 
using Server; 
using Server.Items;

namespace Server.Items 
{ 
	public class PaintBallStorageChest : MetalChest 
	{ 
		[Constructable] 
		public PaintBallStorageChest() 
		{
			Name = "Paint Ball Storage Chest";
			DropItem( new BagOfPaintBall() );
			DropItem( new BagOfPaintBall() );
			DropItem( new BagOfPaintBall() );
			DropItem( new BagOfPaintBall() );
			DropItem( new BagOfPaintBall() );
			DropItem( new BagOfPaintBall() );
			DropItem( new BagOfPaintBall() );
			DropItem( new BagOfPaintBall() );
			DropItem( new BagOfPaintBall() );
			DropItem( new BagOfPaintBall() );
			DropItem( new BagOfPaintBall() );
			DropItem( new BagOfPaintBall() );
			DropItem( new BagOfPaintBall() );
			DropItem( new BagOfPaintBall() );
			DropItem( new BagOfPaintBall() );
			DropItem( new BagOfPaintBall() );
			DropItem( new BagOfPaintBall() );
			DropItem( new BagOfPaintBall() );
			DropItem( new BagOfPaintBall() );
			DropItem( new BagOfPaintBall() );
			DropItem( new BagOfPaintBall() );
			DropItem( new BagOfPaintBall() );
			DropItem( new BagOfPaintBall() );
			DropItem( new BagOfPaintBall() );
			DropItem( new BagOfPaintBall() );
			DropItem( new BagOfPaintBall() );
		} 

		public PaintBallStorageChest( Serial serial ) : base( serial ) 
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
