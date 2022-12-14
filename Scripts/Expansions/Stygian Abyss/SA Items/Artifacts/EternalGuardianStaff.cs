using System;
using Server;

namespace Server.Items
{
	public class EternalGuardianStaff : GnarledStaff
	{

		public override int InitMinHits{ get{ return 255; } }
		public override int InitMaxHits{ get{ return 255; } }

		[Constructable]
		public EternalGuardianStaff()
		{
			Name = ("Eternal Guardian Staff");
		
			Hue = 95;
			
			SkillBonuses.SetValues( 0, SkillName.Mysticism, 15.0 );		
			Attributes.SpellDamage = 10;
			Attributes.LowerManaCost = 5;	
			Attributes.SpellChanneling = 1;	

}



		public EternalGuardianStaff( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}
		
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}
}