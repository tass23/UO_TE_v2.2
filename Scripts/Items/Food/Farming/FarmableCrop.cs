using System;
using Server;
using Server.Network;
using Server.Regions;

namespace Server.Items
{
	public abstract class FarmableCrop : Item
	{
		private bool m_Picked;

		public abstract Item GetCropObject();
		public abstract int GetPickedID();

		public FarmableCrop( int itemID ) : base( itemID )
		{
			Movable = false;
		}

		public override void OnDoubleClick( Mobile from )
		{
			Map map = this.Map;
			Point3D loc = this.Location;

			if ( Parent != null || Movable || IsLockedDown || IsSecure || map == null || map == Map.Internal )
				return;

			if ( !from.InRange( loc, 2 ) || !from.InLOS( this ) )
				from.LocalOverheadMessage( MessageType.Regular, 0x3B2, 1019045 );
			else if ( !m_Picked )
				OnPicked( from, loc, map );
		}

		public virtual void OnPicked( Mobile from, Point3D loc, Map map )
		{
			ItemID = GetPickedID();

			Item spawn = GetCropObject();

			if ( spawn != null )
				spawn.MoveToWorld( loc, map );

			m_Picked = true;

			Unlink();

			Timer.DelayCall( TimeSpan.FromMinutes( 5.0 ), new TimerCallback( Delete ) );
		}

		public void Unlink()
		{
			SpawnEntry se = this.Spawner as SpawnEntry;

			if ( se != null )
			{
				this.Spawner.Remove( this );
				this.Spawner = null;
			}

		}

		public FarmableCrop( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.WriteEncodedInt( 0 );

			writer.Write( m_Picked );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadEncodedInt();

			switch ( version )
			{
				case 0:
					m_Picked = reader.ReadBool();
					break;
			}
			if ( m_Picked )
			{
				Unlink();
				Delete();
			}
		}
	}
}