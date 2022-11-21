using System;
using Server;

namespace Server.Items
{
	public class BladeOfTheVampires : Longsword
	{
		public override int ArtifactRarity{ get{ return 100; } }
		public override int InitMinHits{ get{ return 255; } }
		public override int InitMaxHits{ get{ return 255; } }

		[Constructable]
		public BladeOfTheVampires()
		{
			Weight = 5;
			Name = "Blade of the Vampires";
			Hue = 1194;
              
			WeaponAttributes.HitHarm = 20;
			WeaponAttributes.HitLeechHits = 50;  
			Attributes.AttackChance = 15;
			Attributes.SpellChanneling = 1;
		}

		public BladeOfTheVampires( Serial serial ) : base( serial )
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