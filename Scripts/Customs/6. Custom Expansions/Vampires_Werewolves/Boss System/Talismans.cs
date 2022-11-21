using System;
using Server;
using Server.Mobiles;

namespace Server.Items
{	
	public class VampireTalisman : BaseTalisman
	{
		public override bool ForceShowName{ get{ return true; } }
	
		[Constructable]
		public VampireTalisman() : base( 0x2F58 )
		{	
			Name = "Braided Garlic Talisman of Vampire Protection";
			Hue = 1169;
			Blessed = Blessed;
			Protection = new TalismanAttribute( typeof( Vampire5 ), "Vampire", 10);
		}
		
		public VampireTalisman( Serial serial ) :  base( serial )
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
	
	public class WerewolfTalisman : BaseTalisman
	{
		public override bool ForceShowName{ get{ return true; } }
	
		[Constructable]
		public WerewolfTalisman() : base( 0x2F58 )
		{	
			Name = "Solid Silver Talisman of Werewolf Protection";
			Hue = 2036;
			Blessed = Blessed;
			Protection = new TalismanAttribute( typeof( Werewolf5 ), "Werewolf", 10);
		}
		
		public WerewolfTalisman( Serial serial ) :  base( serial )
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