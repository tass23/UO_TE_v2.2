using System;
using Server;
using Server.Network;
using Server.Items;

namespace Server.Items
{
	public class RobinhoodBow : ElvenCompositeLongbow
	{
		public override int ArtifactRarity{ get{ return 100; } }
		public override int InitMinHits{ get{ return 255; } }
		public override int InitMaxHits{ get{ return 255; } }

		#region Mondain's Legacy
		public override float MlSpeed{ get{ return 2.75f; } }
		#endregion

		[Constructable]
		public RobinhoodBow()
		{
            Name = "Robin Hood's Longbow";
            Hue = 1154;

			WeaponAttributes.HitLeechHits = Utility.RandomMinMax ( 10,30 );
			Attributes.AttackChance = 10;
			Attributes.WeaponDamage = 10;
			Slayer = SlayerName.Repond;
			WeaponAttributes.UseBestSkill = 1;
			Attributes.SpellChanneling = 1;
			Attributes.WeaponSpeed = 30;
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
				m.SendMessage( "You feel the power of Robin's bow rush through you as you grasp it within your hand!" );
			}
			return true;
		}

		public RobinhoodBow( Serial serial ) : base( serial )
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