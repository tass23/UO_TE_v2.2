using System;
using Server;
using Server.Mobiles;
using Server.ACC.CSS.Systems.Mysticism;
using Server.Items;

namespace Server.Items
{
	public class MysticismHealingStone : Item
	{
		private Mobile m_Caster;
		private int m_Amount;

		[Constructable]
		public MysticismHealingStone( Mobile caster, int amount ) : base( 0x4078 )
		{
			m_Caster = caster;
			m_Amount = amount;
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( !from.InRange( this.GetWorldLocation(), 1 ) )
			{
				from.SendLocalizedMessage( 502138 ); // That is too far away for you to use
				return;
			}
			else if ( from != m_Caster )
			{
				// from.SendLocalizedMessage( ); // 
				return;
			}

			BaseWeapon weapon = from.FindItemOnLayer( Layer.OneHanded ) as BaseWeapon;

			if ( weapon == null )
				weapon = from.FindItemOnLayer( Layer.TwoHanded ) as BaseWeapon;

			if ( weapon != null )
			{
				from.SendLocalizedMessage( 1080116 ); // You must have a free hand to use a Healing Stone.
			}
			else if ( from.BeginAction( typeof( BaseHealPotion ) ) )
			{
				from.Heal( Utility.RandomMinMax( BasePotion.Scale( from, 13 ) , BasePotion.Scale( from, 16 ) ) );
				this.Consume();
				Timer.DelayCall( TimeSpan.FromSeconds( 8.0 ), new TimerStateCallback( ReleaseHealLock ), from );
			}
			else
				from.SendLocalizedMessage( 1095172 ); // You must wait a few seconds before using another Healing Stone.
		}

		public override bool DropToWorld( Mobile from, Point3D p )
		{
			Delete();
			return false;
		}

		public override bool AllowSecureTrade( Mobile from, Mobile to, Mobile newOwner, bool accepted )
		{
			return false;
		}

		private static void ReleaseHealLock( object state )
		{
			((Mobile)state).EndAction( typeof( BaseHealPotion ) );
		}

		public MysticismHealingStone( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
}