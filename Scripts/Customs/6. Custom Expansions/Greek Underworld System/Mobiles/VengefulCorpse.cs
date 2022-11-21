using System;
using System.Collections;
using Server.Items;
using Server.Mobiles;
using Server.Network;

namespace Server.Mobiles
{
	[CorpseName( "a decayed corpse" )]
	public class VengefulCorpse : BaseCreature
	{
		private DateTime m_NextAbility;

		[Constructable]
		public VengefulCorpse() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a vengeful corpse";
			Body = 3;
			Hue = 1155;
			BaseSoundID = 471;

			SetStr( 200 );
			SetDex( 60 );
			SetInt( 100 );
			SetHits( 600 );

			SetDamage( 15, 20 );
			SetDamageType( ResistanceType.Physical, 60 );
			SetResistance( ResistanceType.Physical, 30 );
			SetResistance( ResistanceType.Fire, 50 );
			SetResistance( ResistanceType.Cold, 40 );
			SetResistance( ResistanceType.Poison, 30 );
			SetResistance( ResistanceType.Energy, 40 );
			SetSkill( SkillName.MagicResist, 120.0 );
			SetSkill( SkillName.Tactics, 120.0 );
			SetSkill( SkillName.Wrestling, 120.0 );
			SetSkill( SkillName.Anatomy, 120.0 );

			Fame = 5000;
			Karma = -5000;
			VirtualArmor = 40;
		}

		public override void OnThink()
		{
			this.Str = 200 + (100 - ((this.Hits * 100) / this.HitsMax) * 4);
			this.DamageMin = 100 - ((this.Hits * 100) / this.HitsMax) / 2;
			this.DamageMax = 5 + (100 - ((this.Hits * 100) / this.HitsMax) / 2);

			if ( this.DamageMin < 10 )
				this.DamageMin = 10;

			if ( this.DamageMax < 15 )
				this.DamageMax = 15;
		}

		public VengefulCorpse( Serial serial ) : base( serial )
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