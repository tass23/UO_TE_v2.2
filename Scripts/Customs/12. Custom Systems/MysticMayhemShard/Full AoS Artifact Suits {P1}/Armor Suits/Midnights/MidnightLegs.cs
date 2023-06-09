using System;
using Server;

namespace Server.Items
{
	public class MidnightLegs : BoneLegs
	{
		public override int LabelNumber{ get{ return 1061093; } } // Midnight Legs
		public override SetItem SetID{ get{ return SetItem.Midnights; } }
		public override int Pieces{ get{ return 4; } }
		public override int ArtifactRarity{ get{ return 11; } }

		public override int BasePhysicalResistance{ get{ return 21; } }

		public override int InitMinHits{ get{ return 255; } }
		public override int InitMaxHits{ get{ return 255; } }

		[Constructable]
		public MidnightLegs()
		{
			Name = "Midnight's Leggings";
			Hue = 0x455;
			
			SetSkillBonuses.SetValues( 0, SkillName.Necromancy, 15.0 );
			SetAttributes.SpellDamage = 10;
			ArmorAttributes.MageArmor = 1;
		}

		public MidnightLegs( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 1 );
		}
		
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			if ( version < 1 )
				PhysicalBonus = 0;
		}
	}
}