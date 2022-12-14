using System;
using Server;
using Server.Items;
using Server.Mobiles;

namespace Server.Engines.Quests
{			
	public class Percolem : MondainQuester
	{
        public override Type[] Quests
        {
            get
            {
                return new Type[] 
		{ 
			typeof( PercolemTheHunterTierOneQuest )
		};
            }
        }
		
		[Constructable]
		public Percolem() : base( "Percolem", "the Hunter" )
		{
            if (!(this is MondainQuester))

            this.Name = "Percolem";
            this.Title = "the Hunter";
		}
		
		public Percolem( Serial serial ) : base( serial )
		{
		}
		
		public override void InitBody()
		{
			InitStats( 100, 100, 25 );
			
			Female = false;
			Race = Race.Human;
			
			Hue = 0x840C;
			HairItemID = 0x203C;
			HairHue = 0x3B3;
		}
		
		public override void InitOutfit()
		{
            CantWalk = true;
            
            AddItem(new Server.Items.Boots());
            AddItem(new Server.Items.Shirt(1436));
            AddItem(new Server.Items.ShortPants(1436));
            AddItem(new Server.Items.CompositeBow());
            
            Blessed = true;
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