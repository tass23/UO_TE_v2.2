using System;
using Server;
using Server.Multis;
using Server.Targeting;
using Solaris.Addons;

namespace Server.Items
{
	public class FlyingCarpetDyeTub : DyeTub
	{
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

		[Constructable] 
		public FlyingCarpetDyeTub() : base()
		{
			Name = "Flying Carpet Dye Tub";
		}

		public FlyingCarpetDyeTub( Serial serial ) : base( serial )
		{
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( from.InRange( this.GetWorldLocation(), 1 ) )
			{
				from.SendMessage( "Please select your flying carpet that you wish to dye." );
				from.Target = new DyeCarpetTarget( this );
			}
			else
			{
				from.SendLocalizedMessage( 500446 ); // That is too far away.
			}
		}

		private class DyeCarpetTarget : Target
		{
			private FlyingCarpetDyeTub _Tub;

			public DyeCarpetTarget( FlyingCarpetDyeTub tub ) : base( 10, false, TargetFlags.None )
			{
				_Tub = tub;
			}

			protected override void OnTarget( Mobile from, object targeted )
			{
				if ( targeted is FlyingCarpetComponent )
				{
					FlyingCarpet carpet = (FlyingCarpet)((FlyingCarpetComponent)targeted).Addon;
					
					if( carpet != null && carpet.HasKey( from ) )
					{
						from.PlaySound( 0x23E );
						carpet.SetHue( _Tub.DyedHue );
					}
					else
					{
						from.SendMessage( "You cannot dye this carpet" );
					}
				}
				else
				{
					from.SendMessage( "You cannot dye that." );
				}
			}
		}
	}
}