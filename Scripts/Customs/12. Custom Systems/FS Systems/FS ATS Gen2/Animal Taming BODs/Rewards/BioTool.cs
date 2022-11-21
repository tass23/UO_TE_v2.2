using System;
using Server;
using Server.Mobiles;
using Server.Engines.Craft;

namespace Server.Items
{
	public class BioTool : BaseTool
	{
		public override CraftSystem CraftSystem{ get{ return DefBioengineering.CraftSystem; } }

		[Constructable]
		public BioTool() : base( 0x1373 )
		{
			Weight = 2.0;
			Name = "Bio-engineers Tool";
			Hue = 1175;
		}

		[Constructable]
		public BioTool( int uses ) : base( uses, 0x1022 )
		{
			Weight = 2.0;
		}

      		public override void OnDoubleClick( Mobile from ) 
      		{

			PlayerMobile pm = from as PlayerMobile;

			if ( pm.Bioenginer == false )
				from.SendMessage( "You have not learned bio-enginering." );
			else if ( from.Skills[SkillName.AnimalTaming].Base < 100.0 && from.Skills[SkillName.Tinkering].Base < 100.0 )
				from.SendMessage( "You lack the skill to use this tool." );
			else if ( FSATS.EnableBioEngineer == false )
				from.SendMessage( "Bio-Engineering has been disabled on this server, Please contact your server administrator for more information." );
			else
				base.OnDoubleClick( from );
      		}

		public BioTool( Serial serial ) : base( serial )
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

			if ( Weight == 1.0 )
				Weight = 2.0;
		}
	}
}