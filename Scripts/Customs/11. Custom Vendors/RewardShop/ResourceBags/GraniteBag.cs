using System; 
using Server; 
using Server.Items;

namespace Server.Items
{ 
	public class GraniteBag : Bag 
	{
		public override string DefaultName
		{
			get { return "a Granite Supply bag"; }
		}
		
		[Constructable] 
		public GraniteBag() : this( 500 ) 
		{ 
		} 

		[Constructable] 
		public GraniteBag( int amount ) 
		{
			Granite granite = new Granite();
			granite.Amount = 500;
			DropItem(granite);
			
			CopperGranite granite2 = new CopperGranite();
			granite2.Amount = 500;
			DropItem(granite2);
			
			ShadowIronGranite granite3 = new ShadowIronGranite();
			granite3.Amount = 500;
			DropItem(granite3);

			BronzeGranite granite4 = new BronzeGranite();
			granite4.Amount = 500;
			DropItem(granite4);
			
			GoldGranite granite5 = new GoldGranite();
			granite5.Amount = 500;
			DropItem(granite5);
			
			AgapiteGranite granite6 = new AgapiteGranite();
			granite6.Amount = 500;
			DropItem(granite6);
			
			VeriteGranite granite7 = new VeriteGranite();
			granite7.Amount = 500;
			DropItem(granite7);
			
			ValoriteGranite granite8 = new ValoriteGranite();
			granite8.Amount = 500;
			DropItem(granite8);
		} 

		public GraniteBag( Serial serial ) : base( serial ) 
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
