using System;
using Server;

namespace Server.Items
{
	public class CBookOnceUponaMog : BaseBook
	{
		public static readonly BookContent Content = new BookContent
		(
			"Once Upon a Mog", "Mogster",
			
			new BookPageInfo
			( 
				"Expecting to find a", 
				"literary wonder, you", 
				"disdainfully leaf", 
				"through only to find", 
				"crayon scrawlings of", 
				"nudey stickmen and", 
				"women apparently", 
				"wrestling."
			)
		);
		public override BookContent DefaultContent{ get{ return Content; } }
	
		[Constructable]
		public CBookOnceUponaMog() : base( 0x2259, false )
		{
			Hue = 2;
		}
		public CBookOnceUponaMog( Serial serial ) : base( serial )
		{
		}
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.WriteEncodedInt( (int)0 );
		}
		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadEncodedInt();
		}
	}
}