using System;
using Server;
using Server.Items;
using Server.Mobiles;

namespace Server.Multis
{
	public class ElfBrigandCamp : HumanBrigandCamp
	{		
		public override Mobile Camper{ get{ return new ElfBrigand(); } }
	
		[Constructable]
		public ElfBrigandCamp() : base()
		{
		}
		public ElfBrigandCamp( Serial serial ) : base( serial )
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