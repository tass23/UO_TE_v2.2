using System; 
using Server; 
using Server.Items;

namespace Server.Items 
{ 
	public class BagOfPaintBall : Bag 
	{ 
		[Constructable] 
		public BagOfPaintBall() 
		{
			Name = "Bag of Paint Ball Equipement";
			DropItem( new PaintBallGunDefense() );
			DropItem( new PaintBallGunOffense() );
			DropItem( new PaintBallGunSemiAuto() );
			DropItem( new PaintBallGunSnipper() );
			DropItem( new PaintBallGunStandard() );
			DropItem( new PaintBallPellets(1000) );
			DropItem( new PaintBallRobe() );
		} 

		public BagOfPaintBall( Serial serial ) : base( serial ) 
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
