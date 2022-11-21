using System;
using Server;

namespace Server.Items
{
	public class MerlinsStaff : BlackStaff
	{
		public override int ArtifactRarity{ get{ return 100; } }
		public override int InitMinHits{ get{ return 255; } }
		public override int InitMaxHits{ get{ return 255; } }

		#region Mondain's Legacy
		public override float MlSpeed{ get{ return 2.5f; } }
		#endregion

		[Constructable]
		public MerlinsStaff()
		{
			Name = "Merlin's Staff";
            Hue = 1154;

			Attributes.SpellChanneling = 1;
			Attributes.CastSpeed = Utility.RandomMinMax ( 1,5 );
			Attributes.CastRecovery = Utility.RandomMinMax ( 1,5 );
			WeaponAttributes.HitLightning = Utility.RandomMinMax ( 10,30 );
			WeaponAttributes.HitFireball = Utility.RandomMinMax ( 10,30 );
			Slayer = SlayerName.Repond;
			WeaponAttributes.UseBestSkill = 1;
        }

		#region Mondain's Legacy
		public override void GetDamageTypes( Mobile wielder, out int phys, out int fire, out int cold, out int pois, out int nrgy, out int chaos, out int direct )
		{
			pois = fire = cold = chaos = direct = 0;
			nrgy = phys = 50;
		}
		#endregion

		public override bool OnEquip( Mobile from )
		{
			return Validate( from ) && base.OnEquip( from );
		}

		public override void OnSingleClick( Mobile from )
		{
			if ( Validate( Parent as Mobile ) )
				base.OnSingleClick( from );
		}

		public bool Validate( Mobile m )
		{
			if ( m == null || !m.Player )
				return true;
			{
				m.FixedParticles( 0x375A, 10, 30, 5052, EffectLayer.LeftFoot );
				m.PlaySound( 543 );
				m.SendMessage( "You feel the power of Merlin's staff rush through you as you grasp it within your hand!" );
			}
			return true;
		}

		public MerlinsStaff( Serial serial ) : base( serial )
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

			if ( WeaponAttributes.MageWeapon == 0 )
				WeaponAttributes.MageWeapon = 30;

			if ( ItemID == 0xDF1 )
				ItemID = 0xDF0;
		}
	}
}