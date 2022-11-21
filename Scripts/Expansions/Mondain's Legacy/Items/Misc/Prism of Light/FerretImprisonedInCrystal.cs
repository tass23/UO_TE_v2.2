using System;
using Server;
using Server.Mobiles;

namespace Server.Items
{
	public class FerretImprisonedInCrystal : BaseImprisonedMobile
	{
		public override BaseCreature Summon{ get{ return new ShimmeringFerret(); } }
		
		[Constructable]
		public FerretImprisonedInCrystal() : base( 0x1F19 )
		{
			Name = "a ferret imprisoned in a crystal";
			Weight = 1.0;
		}

		public FerretImprisonedInCrystal( Serial serial ) : base( serial )
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
	public class ShimmeringFerret : Ferret
	{	
		public override bool DeleteOnRelease{ get{ return true; } }
		
		[Constructable]
		public ShimmeringFerret() : base()
		{
			SetSkill( SkillName.MagicResist, 100.0 );
			SetSkill( SkillName.Tactics, 100.0 );
			SetSkill( SkillName.Wrestling, 100.0 );
		}

		public ShimmeringFerret( Serial serial ) : base( serial )
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

