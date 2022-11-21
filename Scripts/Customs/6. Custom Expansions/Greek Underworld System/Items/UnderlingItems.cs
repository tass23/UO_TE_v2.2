using System;
using System.Collections;
using Server;
using Server.Items;
using Server.Mobiles;
using Server.Network;

namespace Server.Items
{
	public class CursedEarrings : GoldEarrings
	{
		public override int ArtifactRarity{ get{ return 100; } }

		[Constructable]
		public CursedEarrings()
		{
			Name = "Cursed Earrings";
			Hue = 1194;
			Resistances.Physical = 5;
			Resistances.Fire = 5;
			Resistances.Cold = 5;
			Resistances.Poison= 5;
			Resistances.Energy = 5;
		}

		public CursedEarrings( Serial serial ) : base( serial )
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

	public class LamiaeRing : GoldRing
	{
		public override int ArtifactRarity{ get{ return 100; } }

		[Constructable]
		public LamiaeRing()
		{
			Name = "Ring of the Lamiae";
			Hue = 1156;
			Attributes.BonusMana = 15;
			Resistances.Physical = 5;
			Resistances.Energy = 5;
		}

		public LamiaeRing( Serial serial ) : base( serial )
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

	public class CacodemonBracelet : GoldBracelet
	{
		public override int ArtifactRarity{ get{ return 100; } }

		[Constructable]
		public CacodemonBracelet()
		{
			Name = "Bracelet of the Cacodemon";
			Hue = 1175;
			Resistances.Physical = 12;
			Resistances.Cold = 5;
			Resistances.Energy = 10;
		}

		public CacodemonBracelet( Serial serial ) : base( serial )
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