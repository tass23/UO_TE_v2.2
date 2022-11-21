using System;
using Server;
using Server.Engines.XmlSpawner2;

namespace Server.Items
{
	public class BladeOfAres : Longsword
	{
		private DateTime nextabil;

		public override int LabelNumber{ get{ return 1061106; } } // Axe of the Heavens
		public override int ArtifactRarity{ get{ return 50; } }

		public override int InitMinHits{ get{ return 1000; } }
		public override int InitMaxHits{ get{ return 1000; } }

		[Constructable]
		public BladeOfAres()
		{
			Hue = 2949;
			Name = "The Blade of Ares";
			nextabil = DateTime.Now;
			Attributes.AttackChance = 25;
			Attributes.DefendChance = 25;
			Attributes.BonusHits = 200;
		}

		public override void OnHit( Mobile attacker, Mobile defender, double db )
		{
			if ( (defender.Hits * 100) / defender.HitsMax < 30 )
				db += 10;
			base.OnHit( attacker, defender, db );
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( nextabil <= DateTime.Now )
			{
				from.AddStatMod( new StatMod( StatType.Str, "Godlike Strength", 25, TimeSpan.FromMinutes( 2.0 ) ) );
				nextabil = DateTime.Now + TimeSpan.FromMinutes( 5.0 );
			}
		}

		public BladeOfAres( Serial serial ) : base( serial )
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