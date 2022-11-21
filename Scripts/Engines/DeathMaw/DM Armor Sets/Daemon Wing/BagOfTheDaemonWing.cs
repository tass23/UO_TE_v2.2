using System; 
using Server; 
using Server.Items;

namespace Server.Items 
{ 
	public class BagOfTheDaemonWing : Bag 
	{
		public override string DefaultName
		{
			get { return "a Bag of The Daemon Wing"; }
		}

		[Constructable] 
		public BagOfTheDaemonWing() 
		{  
			DropItem( new TunicOfTheDaemonWing() );
			DropItem( new LegsOfTheDaemonWing() );
			DropItem( new GlovesOfTheDaemonWing() );
			DropItem( new CapOfTheDaemonWing() );
			DropItem( new ArmsOfTheDaemonWing() );
		} 

		public BagOfTheDaemonWing( Serial serial ) : base( serial ) 
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
