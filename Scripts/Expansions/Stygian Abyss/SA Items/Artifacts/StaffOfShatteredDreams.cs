using System;
using Server.Network;
using Server.Items;

namespace Server.Items
{

	public class StaffOfShatteredDreams : GlassStaff
	{

		public override int ArtifactRarity{ get{ return 11; } }
		public override int InitMinHits{ get{ return 255; } }
		public override int InitMaxHits{ get{ return 255; } }

		public override bool CanBeWornByGargoyles{ get{ return true; } }
		public override Race RequiredRace { get { return Race.Gargoyle; } }

		[Constructable]
		public StaffOfShatteredDreams() : base(  )
		{
		Name = ("Staff Of Shattered Dreams");
		
		Hue = 1151;
		
			WeaponAttributes.HitDispel = 25;
			//WeaponAttributes.SplinteringWeapon = 20;
			Attributes.WeaponDamage = 50;			
			WeaponAttributes.ResistFireBonus = 15;
			Attributes.CastSpeed = -1;
			Attributes.SpellChanneling = 1;	
		}

		public StaffOfShatteredDreams( Serial serial ) : base( serial )
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