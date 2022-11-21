/*
 created by:
     /\            888                   888     .d8888b.   .d8888b.  
____/_ \____       888                   888    d88P  Y88b d88P  Y88b 
\  ___\ \  /       888                   888    888    888 888    888 
 \/ /  \/ /    .d88888  8888b.   8888b.  888888 Y88b. d888 Y88b. d888 
 / /\__/_/\   d88" 888     "88b     "88b 888     "Y888P888  "Y888P888 
/__\ \_____\  888  888 .d888888 .d888888 888           888        888 
    \  /      Y88b 888 888  888 888  888 Y88b.  Y88b  d88P Y88b  d88P 
     \/        "Y88888 "Y888888 "Y888888  "Y888  "Y8888P"   "Y8888P"  
*/
using System;
using Server;
using Server.Engines.Craft;

namespace Server.Items
{
	[Flipable( 0x1EB8, 0x1EB9 )]
	public class RunicTinkerTools : BaseRunicTool
	{
		public override CraftSystem CraftSystem{ get{ return DefTinkering.CraftSystem; } }

		public override int LabelNumber { get { return 1044164; } }

		public override void AddNameProperties( ObjectPropertyList list )
		{
			base.AddNameProperties( list );

			if ( !CraftResources.IsStandard( Resource ) )
				list.Add( 1060662, "{0}\t{1}", "Resource"	, CraftResources.GetName( Resource ) );
		}

		[Constructable]
		public RunicTinkerTools( CraftResource resource ) : this( resource, 50 )
		{
		}

		[Constructable]
		public RunicTinkerTools( CraftResource resource, int uses ) : base( resource, uses, 0x1EB8 )
		{
			Name = "Runic Tinker's Tools";
			Weight = 1.0;
			Hue = CraftResources.GetHue( resource );
		}

		public RunicTinkerTools( Serial serial ) : base( serial )
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