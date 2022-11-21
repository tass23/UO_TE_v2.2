using System;
using Server.Network;
using Server.Items;

namespace Server.Items
{

	public class SwordOfShatteredHopes : GlassSword
	{
		public override int ArtifactRarity{ get{ return 10; } }
		public override int InitMinHits{ get{ return 255; } }
		public override int InitMaxHits{ get{ return 255; } }

		public override bool CanBeWornByGargoyles{ get{ return true; } }
		public override Race RequiredRace { get { return Race.Gargoyle; } }
		[Constructable]
		public SwordOfShatteredHopes() : base( 0x90C )
		{
			Name = ("Sword Of Shattered Hopes");
		
			Hue = 91;	
			
			WeaponAttributes.HitDispel = 25;
			//WeaponAttributes.SplinteringWeapon = 20;
			Attributes.WeaponSpeed = 30;	
			Attributes.WeaponDamage = 50;			
			WeaponAttributes.ResistFireBonus = 15;
		}

		public SwordOfShatteredHopes( Serial serial ) : base( serial )
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