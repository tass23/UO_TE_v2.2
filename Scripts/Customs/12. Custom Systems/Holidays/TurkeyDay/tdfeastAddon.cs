using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class tgfeastAddon : BaseAddon
	{
		public override BaseAddonDeed Deed
		{
			get
			{
				return new tgfeastAddonDeed();
			}
		}

		[ Constructable ]
		public tgfeastAddon()
		{
			AddComponent( new AddonComponent( 1114 ), -1, 4, 0 );
			AddComponent( new AddonComponent( 7619 ), 1, 4, 0 );
			AddComponent( new AddonComponent( 2462 ), 1, 4, 7 );
			AddComponent( new AddonComponent( 9008 ), 1, 6, 6 );
			AddComponent( new AddonComponent( 7617 ), 1, 6, 0 );
			AddComponent( new AddonComponent( 1114 ), 3, 2, 0 );
			AddComponent( new AddonComponent( 4164 ), 1, 2, 7 );
			AddComponent( new AddonComponent( 7619 ), 1, 2, 0 );
			AddComponent( new AddonComponent( 2450 ), 1, 2, 6 );
			AddComponent( new AddonComponent( 7711 ), 1, 2, 7 );
			AddComponent( new AddonComponent( 1114 ), 3, 6, 0 );
			AddComponent( new AddonComponent( 1114 ), 3, 4, 0 );
			AddComponent( new AddonComponent( 2854 ), 5, 4, 0 );
			AddComponent( new AddonComponent( 4553 ), 5, 2, 0 );
			AddComponent( new AddonComponent( 2493 ), 0, -6, 6 );
			AddComponent( new AddonComponent( 2523 ), 0, -6, 6 );
			AddComponent( new AddonComponent( 7618 ), 0, -6, 0 );
			AddComponent( new AddonComponent( 7832 ), -5, 0, 0 );
			AddComponent( new AddonComponent( 7619 ), 1, -5, 0 );
			AddComponent( new AddonComponent( 2445 ), 1, -5, 6 );
			AddComponent( new AddonComponent( 2854 ), -3, 0, 0 );
			AddComponent( new AddonComponent( 4553 ), -3, -2, 0 );
			AddComponent( new AddonComponent( 4553 ), -3, -6, 0 );
			AddComponent( new AddonComponent( 7831 ), -5, -2, 0 );
			AddComponent( new AddonComponent( 2855 ), -3, -4, 0 );
			AddComponent( new AddonComponent( 7618 ), 0, -3, 0 );
			AddComponent( new AddonComponent( 7619 ), 0, -5, 0 );
			AddComponent( new AddonComponent( 7619 ), 0, -1, 0 );
			AddComponent( new AddonComponent( 1114 ), -1, 0, 0 );
			AddComponent( new AddonComponent( 7619 ), 1, -4, 0 );
			AddComponent( new AddonComponent( 7709 ), 1, -4, 7 );
			AddComponent( new AddonComponent( 2450 ), 1, -4, 6 );
			AddComponent( new AddonComponent( 9008 ), 1, -6, 6 );
			AddComponent( new AddonComponent( 7618 ), 1, -6, 0 );
			AddComponent( new AddonComponent( 7619 ), 0, -4, 0 );
			AddComponent( new AddonComponent( 2493 ), 0, -4, 6 );
			AddComponent( new AddonComponent( 2523 ), 0, -4, 6 );
			AddComponent( new AddonComponent( 2523 ), 0, 0, 6 );
			AddComponent( new AddonComponent( 7618 ), 0, 0, 0 );
			AddComponent( new AddonComponent( 7619 ), 0, 0, 0 );
			AddComponent( new AddonComponent( 2493 ), 0, 0, 6 );
			AddComponent( new AddonComponent( 1114 ), -1, -2, 0 );
			AddComponent( new AddonComponent( 2492 ), 1, -2, 6 );
			AddComponent( new AddonComponent( 7619 ), 1, -2, 0 );
			AddComponent( new AddonComponent( 1114 ), -1, -6, 0 );
			AddComponent( new AddonComponent( 2493 ), 0, -2, 6 );
			AddComponent( new AddonComponent( 7619 ), 0, -2, 0 );
			AddComponent( new AddonComponent( 7619 ), 0, -2, 0 );
			AddComponent( new AddonComponent( 2523 ), 0, -2, 6 );
			AddComponent( new AddonComponent( 7618 ), 2, -6, 0 );
			AddComponent( new AddonComponent( 2520 ), 2, -6, 6 );
			AddComponent( new AddonComponent( 2517 ), 2, -6, 6 );
			AddComponent( new AddonComponent( 7831 ), -5, -1, 0 );
			AddComponent( new AddonComponent( 1114 ), -1, -4, 0 );
			AddComponent( new AddonComponent( 7618 ), 2, -3, 0 );
			AddComponent( new AddonComponent( 8079 ), 2, -3, 6 );
			AddComponent( new AddonComponent( 7619 ), 1, -1, 0 );
			AddComponent( new AddonComponent( 7619 ), 1, -1, 0 );
			AddComponent( new AddonComponent( 2500 ), 1, -1, 6 );
			AddComponent( new AddonComponent( 7618 ), 1, -3, 0 );
			AddComponent( new AddonComponent( 7618 ), 2, 0, 0 );
			AddComponent( new AddonComponent( 2520 ), 2, 0, 6 );
			AddComponent( new AddonComponent( 2517 ), 2, 0, 6 );
			AddComponent( new AddonComponent( 2842 ), 2, 0, 14 );
			AddComponent( new AddonComponent( 7619 ), 2, -4, 0 );
			AddComponent( new AddonComponent( 2520 ), 2, -4, 6 );
			AddComponent( new AddonComponent( 2517 ), 2, -4, 6 );
			AddComponent( new AddonComponent( 2842 ), 2, -4, 12 );
			AddComponent( new AddonComponent( 4160 ), 1, 0, 6 );
			AddComponent( new AddonComponent( 7619 ), 1, 0, 0 );
			AddComponent( new AddonComponent( 7618 ), 1, 0, 0 );
			AddComponent( new AddonComponent( 1114 ), 3, -2, 0 );
			AddComponent( new AddonComponent( 7619 ), 2, -1, 0 );
			AddComponent( new AddonComponent( 8079 ), 2, -1, 6 );
			AddComponent( new AddonComponent( 7619 ), 2, -5, 0 );
			AddComponent( new AddonComponent( 8079 ), 2, -5, 6 );
			AddComponent( new AddonComponent( 2854 ), 5, 0, 0 );
			AddComponent( new AddonComponent( 1114 ), 3, -6, 0 );
			AddComponent( new AddonComponent( 7619 ), 2, -2, 0 );
			AddComponent( new AddonComponent( 2520 ), 2, -2, 6 );
			AddComponent( new AddonComponent( 2517 ), 2, -2, 6 );
			AddComponent( new AddonComponent( 1114 ), 3, 0, 0 );
			AddComponent( new AddonComponent( 1114 ), 3, -4, 0 );
			AddComponent( new AddonComponent( 2855 ), 5, -4, 0 );
			AddComponent( new AddonComponent( 4553 ), 5, -6, 0 );
			AddComponent( new AddonComponent( 4553 ), 5, -2, 0 );
			AddComponent( new AddonComponent( 4553 ), 5, 6, 0 );
			AddComponent( new AddonComponent( 7617 ), 2, 6, 0 );
			AddComponent( new AddonComponent( 2520 ), 2, 6, 6 );
			AddComponent( new AddonComponent( 2517 ), 2, 6, 6 );
			AddComponent( new AddonComponent( 8079 ), 2, 6, 8 );
			AddComponent( new AddonComponent( 7619 ), 2, 5, 0 );
			AddComponent( new AddonComponent( 8079 ), 2, 5, 6 );
			AddComponent( new AddonComponent( 2842 ), 2, 5, 15 );
			AddComponent( new AddonComponent( 7619 ), 2, 4, 0 );
			AddComponent( new AddonComponent( 2520 ), 2, 4, 6 );
			AddComponent( new AddonComponent( 2517 ), 2, 4, 6 );
			AddComponent( new AddonComponent( 7618 ), 2, 3, 0 );
			AddComponent( new AddonComponent( 8079 ), 2, 3, 6 );
			AddComponent( new AddonComponent( 7619 ), 2, 2, 0 );
			AddComponent( new AddonComponent( 2520 ), 2, 2, 6 );
			AddComponent( new AddonComponent( 2517 ), 2, 2, 6 );
			AddComponent( new AddonComponent( 7619 ), 2, 1, 0 );
			AddComponent( new AddonComponent( 8079 ), 2, 1, 6 );
			AddComponent( new AddonComponent( 7619 ), 0, 2, 0 );
			AddComponent( new AddonComponent( 2520 ), 0, 2, 6 );
			AddComponent( new AddonComponent( 2493 ), 0, 2, 6 );
			AddComponent( new AddonComponent( 7619 ), 0, 1, 0 );
			AddComponent( new AddonComponent( 7619 ), 0, 4, 0 );
			AddComponent( new AddonComponent( 7619 ), 0, 4, 0 );
			AddComponent( new AddonComponent( 2520 ), 0, 4, 6 );
			AddComponent( new AddonComponent( 2493 ), 0, 4, 6 );
			AddComponent( new AddonComponent( 1114 ), -1, 2, 0 );
			AddComponent( new AddonComponent( 7617 ), 0, 6, 0 );
			AddComponent( new AddonComponent( 2520 ), 0, 6, 6 );
			AddComponent( new AddonComponent( 2493 ), 0, 6, 6 );
			AddComponent( new AddonComponent( 4553 ), -3, 2, 0 );
			AddComponent( new AddonComponent( 2854 ), -3, 4, 0 );
			AddComponent( new AddonComponent( 2537 ), 1, 1, 6 );
			AddComponent( new AddonComponent( 7619 ), 1, 1, 0 );
			AddComponent( new AddonComponent( 4553 ), -3, 6, 0 );
			AddComponent( new AddonComponent( 5643 ), 1, 3, 6 );
			AddComponent( new AddonComponent( 7618 ), 1, 3, 0 );
			AddComponent( new AddonComponent( 5644 ), 0, 3, 7 );
			AddComponent( new AddonComponent( 7618 ), 0, 3, 0 );
			AddComponent( new AddonComponent( 6274 ), 0, 3, 6 );
			AddComponent( new AddonComponent( 7619 ), 1, 5, 0 );
			AddComponent( new AddonComponent( 4161 ), 1, 5, 8 );
			AddComponent( new AddonComponent( 7619 ), 0, 5, 0 );
			AddComponent( new AddonComponent( 1114 ), -1, 6, 0 );
			AddonComponent ac = null;
			ac = new AddonComponent( 2493 );
			AddComponent( ac, 0, -6, 6 );
			ac = new AddonComponent( 2493 );
			AddComponent( ac, 0, -2, 6 );
			ac = new AddonComponent( 2492 );
			AddComponent( ac, 1, -2, 6 );
			ac = new AddonComponent( 2523 );
			AddComponent( ac, 0, -6, 6 );
			ac = new AddonComponent( 2523 );
			AddComponent( ac, 0, 0, 6 );
			ac = new AddonComponent( 4160 );
			AddComponent( ac, 1, 0, 6 );
			ac = new AddonComponent( 7618 );
			AddComponent( ac, 0, -6, 0 );
			ac = new AddonComponent( 7619 );
			AddComponent( ac, 0, -5, 0 );
			ac = new AddonComponent( 7619 );
			AddComponent( ac, 0, -4, 0 );
			ac = new AddonComponent( 7618 );
			AddComponent( ac, 1, -3, 0 );
			ac = new AddonComponent( 7619 );
			AddComponent( ac, 1, 0, 0 );
			ac = new AddonComponent( 7619 );
			AddComponent( ac, 1, -2, 0 );
			ac = new AddonComponent( 7619 );
			AddComponent( ac, 0, -1, 0 );
			ac = new AddonComponent( 7618 );
			AddComponent( ac, 0, -3, 0 );
			ac = new AddonComponent( 4553 );
			AddComponent( ac, -3, -6, 0 );
			ac = new AddonComponent( 9008 );
			AddComponent( ac, 1, -6, 6 );
			ac = new AddonComponent( 7619 );
			AddComponent( ac, 1, -5, 0 );
			ac = new AddonComponent( 7619 );
			AddComponent( ac, 1, -4, 0 );
			ac = new AddonComponent( 7618 );
			AddComponent( ac, 0, 0, 0 );
			ac = new AddonComponent( 7619 );
			AddComponent( ac, 0, 0, 0 );
			ac = new AddonComponent( 7619 );
			AddComponent( ac, 1, -1, 0 );
			ac = new AddonComponent( 7619 );
			AddComponent( ac, 0, -2, 0 );
			ac = new AddonComponent( 2493 );
			AddComponent( ac, 0, -4, 6 );
			ac = new AddonComponent( 2493 );
			AddComponent( ac, 0, 0, 6 );
			ac = new AddonComponent( 7619 );
			AddComponent( ac, 0, -2, 0 );
			ac = new AddonComponent( 2523 );
			AddComponent( ac, 0, -4, 6 );
			ac = new AddonComponent( 2523 );
			AddComponent( ac, 0, -2, 6 );
			ac = new AddonComponent( 7618 );
			AddComponent( ac, 1, -6, 0 );
			ac = new AddonComponent( 7618 );
			AddComponent( ac, 1, 0, 0 );
			ac = new AddonComponent( 7619 );
			AddComponent( ac, 1, -1, 0 );
			ac = new AddonComponent( 7831 );
			AddComponent( ac, -5, -2, 0 );
			ac = new AddonComponent( 7831 );
			AddComponent( ac, -5, -1, 0 );
			ac = new AddonComponent( 7709 );
			AddComponent( ac, 1, -4, 7 );
			ac = new AddonComponent( 7832 );
			AddComponent( ac, -5, 0, 0 );
			ac = new AddonComponent( 2500 );
			AddComponent( ac, 1, -1, 6 );
			ac = new AddonComponent( 2450 );
			AddComponent( ac, 1, -4, 6 );
			ac = new AddonComponent( 2445 );
			AddComponent( ac, 1, -5, 6 );
			ac = new AddonComponent( 1114 );
			AddComponent( ac, -1, -6, 0 );
			ac = new AddonComponent( 1114 );
			AddComponent( ac, -1, -4, 0 );
			ac = new AddonComponent( 1114 );
			AddComponent( ac, -1, -2, 0 );
			ac = new AddonComponent( 1114 );
			AddComponent( ac, -1, 0, 0 );
			ac = new AddonComponent( 2855 );
			ac.Light = LightType.Circle225;
			AddComponent( ac, -3, -4, 0 );
			ac = new AddonComponent( 2854 );
			ac.Light = LightType.Circle225;
			AddComponent( ac, -3, 0, 0 );
			ac = new AddonComponent( 4553 );
			AddComponent( ac, -3, -2, 0 );
			ac = new AddonComponent( 5643 );
			AddComponent( ac, 1, 3, 6 );
			ac = new AddonComponent( 2537 );
			AddComponent( ac, 1, 1, 6 );
			ac = new AddonComponent( 5644 );
			AddComponent( ac, 0, 3, 7 );
			ac = new AddonComponent( 9008 );
			AddComponent( ac, 1, 6, 6 );
			ac = new AddonComponent( 7617 );
			AddComponent( ac, 0, 6, 0 );
			ac = new AddonComponent( 7619 );
			AddComponent( ac, 0, 5, 0 );
			ac = new AddonComponent( 7619 );
			AddComponent( ac, 0, 4, 0 );
			ac = new AddonComponent( 7618 );
			AddComponent( ac, 0, 3, 0 );
			ac = new AddonComponent( 7619 );
			AddComponent( ac, 0, 2, 0 );
			ac = new AddonComponent( 4164 );
			AddComponent( ac, 1, 2, 7 );
			ac = new AddonComponent( 6274 );
			AddComponent( ac, 0, 3, 6 );
			ac = new AddonComponent( 7618 );
			AddComponent( ac, 1, 3, 0 );
			ac = new AddonComponent( 7619 );
			AddComponent( ac, 1, 2, 0 );
			ac = new AddonComponent( 7619 );
			AddComponent( ac, 1, 1, 0 );
			ac = new AddonComponent( 2450 );
			AddComponent( ac, 1, 2, 6 );
			ac = new AddonComponent( 7617 );
			AddComponent( ac, 1, 6, 0 );
			ac = new AddonComponent( 7619 );
			AddComponent( ac, 1, 5, 0 );
			ac = new AddonComponent( 7619 );
			AddComponent( ac, 0, 4, 0 );
			ac = new AddonComponent( 7619 );
			AddComponent( ac, 1, 4, 0 );
			ac = new AddonComponent( 7619 );
			AddComponent( ac, 0, 1, 0 );
			ac = new AddonComponent( 7711 );
			AddComponent( ac, 1, 2, 7 );
			ac = new AddonComponent( 2520 );
			AddComponent( ac, 0, 2, 6 );
			ac = new AddonComponent( 2520 );
			AddComponent( ac, 0, 4, 6 );
			ac = new AddonComponent( 2520 );
			AddComponent( ac, 0, 6, 6 );
			ac = new AddonComponent( 2493 );
			AddComponent( ac, 0, 2, 6 );
			ac = new AddonComponent( 2493 );
			AddComponent( ac, 0, 4, 6 );
			ac = new AddonComponent( 2493 );
			AddComponent( ac, 0, 6, 6 );
			ac = new AddonComponent( 2462 );
			AddComponent( ac, 1, 4, 7 );
			ac = new AddonComponent( 4161 );
			AddComponent( ac, 1, 5, 8 );
			ac = new AddonComponent( 1114 );
			AddComponent( ac, -1, 2, 0 );
			ac = new AddonComponent( 1114 );
			AddComponent( ac, -1, 4, 0 );
			ac = new AddonComponent( 1114 );
			AddComponent( ac, -1, 6, 0 );
			ac = new AddonComponent( 2854 );
			ac.Light = LightType.Circle225;
			AddComponent( ac, -3, 4, 0 );
			ac = new AddonComponent( 4553 );
			AddComponent( ac, -3, 2, 0 );
			ac = new AddonComponent( 4553 );
			AddComponent( ac, -3, 6, 0 );
			ac = new AddonComponent( 4553 );
			AddComponent( ac, 5, -6, 0 );
			ac = new AddonComponent( 7618 );
			AddComponent( ac, 2, 0, 0 );
			ac = new AddonComponent( 7619 );
			AddComponent( ac, 2, -1, 0 );
			ac = new AddonComponent( 7619 );
			AddComponent( ac, 2, -2, 0 );
			ac = new AddonComponent( 7618 );
			AddComponent( ac, 2, -3, 0 );
			ac = new AddonComponent( 7619 );
			AddComponent( ac, 2, -4, 0 );
			ac = new AddonComponent( 7619 );
			AddComponent( ac, 2, -5, 0 );
			ac = new AddonComponent( 7618 );
			AddComponent( ac, 2, -6, 0 );
			ac = new AddonComponent( 2520 );
			AddComponent( ac, 2, 0, 6 );
			ac = new AddonComponent( 2520 );
			AddComponent( ac, 2, -2, 6 );
			ac = new AddonComponent( 2520 );
			AddComponent( ac, 2, -4, 6 );
			ac = new AddonComponent( 2520 );
			AddComponent( ac, 2, -6, 6 );
			ac = new AddonComponent( 2517 );
			AddComponent( ac, 2, 0, 6 );
			ac = new AddonComponent( 2517 );
			AddComponent( ac, 2, -2, 6 );
			ac = new AddonComponent( 2517 );
			AddComponent( ac, 2, -4, 6 );
			ac = new AddonComponent( 2517 );
			AddComponent( ac, 2, -6, 6 );
			ac = new AddonComponent( 8079 );
			AddComponent( ac, 2, -1, 6 );
			ac = new AddonComponent( 8079 );
			AddComponent( ac, 2, -5, 6 );
			ac = new AddonComponent( 8079 );
			AddComponent( ac, 2, -3, 6 );
			ac = new AddonComponent( 1114 );
			AddComponent( ac, 3, 0, 0 );
			ac = new AddonComponent( 1114 );
			AddComponent( ac, 3, -2, 0 );
			ac = new AddonComponent( 1114 );
			AddComponent( ac, 3, -4, 0 );
			ac = new AddonComponent( 1114 );
			AddComponent( ac, 3, -6, 0 );
			ac = new AddonComponent( 2855 );
			ac.Light = LightType.Circle225;
			AddComponent( ac, 5, -4, 0 );
			ac = new AddonComponent( 2854 );
			ac.Light = LightType.Circle225;
			AddComponent( ac, 5, 0, 0 );
			ac = new AddonComponent( 4553 );
			AddComponent( ac, 5, -2, 0 );
			ac = new AddonComponent( 2842 );
			ac.Light = LightType.Circle225;
			AddComponent( ac, 2, 0, 14 );
			ac = new AddonComponent( 2842 );
			ac.Light = LightType.Circle225;
			AddComponent( ac, 2, -4, 12 );
			ac = new AddonComponent( 7617 );
			AddComponent( ac, 2, 6, 0 );
			ac = new AddonComponent( 7619 );
			AddComponent( ac, 2, 5, 0 );
			ac = new AddonComponent( 7618 );
			AddComponent( ac, 2, 3, 0 );
			ac = new AddonComponent( 7619 );
			AddComponent( ac, 2, 4, 0 );
			ac = new AddonComponent( 7619 );
			AddComponent( ac, 2, 2, 0 );
			ac = new AddonComponent( 7619 );
			AddComponent( ac, 2, 1, 0 );
			ac = new AddonComponent( 2520 );
			AddComponent( ac, 2, 6, 6 );
			ac = new AddonComponent( 2520 );
			AddComponent( ac, 2, 4, 6 );
			ac = new AddonComponent( 2520 );
			AddComponent( ac, 2, 2, 6 );
			ac = new AddonComponent( 2517 );
			AddComponent( ac, 2, 6, 6 );
			ac = new AddonComponent( 2517 );
			AddComponent( ac, 2, 4, 6 );
			ac = new AddonComponent( 2517 );
			AddComponent( ac, 2, 2, 6 );
			ac = new AddonComponent( 8079 );
			AddComponent( ac, 2, 5, 6 );
			ac = new AddonComponent( 8079 );
			AddComponent( ac, 2, 3, 6 );
			ac = new AddonComponent( 8079 );
			AddComponent( ac, 2, 1, 6 );
			ac = new AddonComponent( 8079 );
			AddComponent( ac, 2, 6, 8 );
			ac = new AddonComponent( 1114 );
			AddComponent( ac, 3, 6, 0 );
			ac = new AddonComponent( 1114 );
			AddComponent( ac, 3, 4, 0 );
			ac = new AddonComponent( 1114 );
			AddComponent( ac, 3, 2, 0 );
			ac = new AddonComponent( 4553 );
			AddComponent( ac, 5, 2, 0 );
			ac = new AddonComponent( 2854 );
			ac.Light = LightType.Circle225;
			AddComponent( ac, 5, 4, 0 );
			ac = new AddonComponent( 4553 );
			AddComponent( ac, 5, 6, 0 );
			ac = new AddonComponent( 2842 );
			ac.Light = LightType.Circle225;
			AddComponent( ac, 2, 5, 15 );
		}

		public tgfeastAddon( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( 0 ); // Version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}

	public class tgfeastAddonDeed : BaseAddonDeed
	{
		public override BaseAddon Addon
		{
			get
			{
				return new tgfeastAddon();
			}
		}

		[Constructable]
		public tgfeastAddonDeed()
		{
			Name = "Turkey Day Feast";
		}

		public tgfeastAddonDeed( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( 0 ); // Version
		}

		public override void	Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
}