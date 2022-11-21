using System;
using Server;

namespace Server.Items
{
	public class StaffoftheMagister : BlackStaff
	{
		public override int ArtifactRarity{ get{ return 100; } }
		public override int InitMinHits{ get{ return 255; } }
		public override int InitMaxHits{ get{ return 255; } }

		[Constructable]
		public StaffoftheMagister()
		{
			Weight = 5;
			Name = "Staff of the Magister";
			Hue = 1194;
			WeaponAttributes.HitLeechHits = 50;
			Attributes.LowerManaCost = 10;
			Attributes.RegenMana = 2;
			Attributes.SpellChanneling = 1;
		}

		public StaffoftheMagister( Serial serial ) : base( serial )
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