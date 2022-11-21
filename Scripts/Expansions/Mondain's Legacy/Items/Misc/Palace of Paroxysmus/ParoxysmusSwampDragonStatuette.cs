using System;
using Server;
using Server.Mobiles;
using Server.Items;

namespace Server.Items
{
	public class ParoxysmusSwampDragonStatuette : BaseImprisonedMobile
	{
		public override int LabelNumber{ get{ return 1072084; } } // Paroxysmus' Swamp Dragon		
		public override BaseCreature Summon{ get { return new ParoxysmusSwampDragon(); } }
	
		[Constructable]
		public ParoxysmusSwampDragonStatuette() : base( 0x2619 )
		{
			Weight = 1.0;
			Hue = 0x851;
		}

		public ParoxysmusSwampDragonStatuette( Serial serial ) : base( serial )
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

namespace Server.Mobiles
{		
	public class ParoxysmusSwampDragon : SwampDragon
	{	
		public override bool DeleteOnRelease{ get{ return true; } }
		
		[Constructable]
		public ParoxysmusSwampDragon() : base()
		{
			BardingResource = CraftResource.Iron;
			BardingExceptional = true;
			BardingHP = BardingMaxHP;
			HasBarding = true;
			Hue = 0x851;			
		}

		public ParoxysmusSwampDragon( Serial serial ) : base( serial )
		{
		}
		
		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );
			
			list.Add( 1049646 ); // (summoned)
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

