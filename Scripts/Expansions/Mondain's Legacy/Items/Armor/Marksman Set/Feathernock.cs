using System;
using Server.Network;
using Server.Items;

namespace Server.Items
{
	public class Feathernock : BaseQuiver
	{
		public override int LabelNumber{ get{ return 1074324; } } // Feathernock (Marksman Set)
		
		public override SetItem SetID{ get{ return SetItem.Marksman; } }
		public override int Pieces{ get{ return 2; } }

		[Constructable]
		public Feathernock() : base()
		{
			SetHue = 0x594;
			
			Attributes.WeaponDamage = 10;
			WeightReduction = 30;
						
			SetAttributes.AttackChance = 15;
			SetAttributes.BonusDex = 8;
			SetAttributes.WeaponSpeed = 30;
			SetAttributes.WeaponDamage = 20;
		}

		public Feathernock( Serial serial ) : base( serial )
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