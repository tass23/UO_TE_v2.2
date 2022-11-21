using System;
using Server;
using Server.Items;
using Server.Spells;
using Server.Spells.Seventh;
using Server.Spells.Fifth;
using System.Collections.Generic;
using System.Collections;
using Server.Misc;

namespace Server.Mobiles
{
    [CorpseName( "Rakktavi [Renowned] corpse" )]
    public class RakktaviRenowned : BaseRenowned
	{
        public override Type[] UniqueSAList{ get { return new Type[] {typeof( TatteredAncientScroll ) }; } }
        public override Type[] SharedSAList{ get { return new Type[] {typeof( CavalrysFolly ), typeof( ArcanicRuneStone ), typeof( CrushedGlass ), typeof( AbyssalCloth ), typeof( TorcOfTheGuardians )}; } }
		public override InhumanSpeech SpeechType{ get{ return InhumanSpeech.Ratman; } }

		[Constructable]
		public RakktaviRenowned() : base( AIType.AI_Archer )
		{
			Name = "Rakktavi";
			Title = "[Renowned]";
			Body = 0x8E;
			BaseSoundID = 437;

			SetStr( 146, 180 );
			SetDex( 240, 300 );
			SetInt( 300, 350 );

			SetHits( 45000, 50000 );

			SetDamage( 8, 10 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 20, 30 );
			SetResistance( ResistanceType.Fire, 10, 25 );
			SetResistance( ResistanceType.Cold, 30, 40 );
			SetResistance( ResistanceType.Poison, 10, 20 );
			SetResistance( ResistanceType.Energy, 10, 20 );

			SetSkill( SkillName.Anatomy, 60.2, 100.0 );
			SetSkill( SkillName.Archery, 80.1, 90.0 );
			SetSkill( SkillName.MagicResist, 65.1, 90.0 );
			SetSkill( SkillName.Tactics, 50.1, 75.0 );
			SetSkill( SkillName.Wrestling, 50.1, 75.0 );

			Fame = 6500;
			Karma = -6500;

			VirtualArmor = 56;

                        PackItem( new EssenceBalance() );
			
                        AddItem( new Bow() );
			PackItem( new Arrow( Utility.RandomMinMax( 50, 70 ) ) );
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Rich );
		}
		
		
		
		
		public override bool CanRummageCorpses{ get{ return true; } }
		public override int Hides{ get{ return 8; } }
		public override HideType HideType{ get{ return HideType.Spined; } }

		public RakktaviRenowned( Serial serial ) : base( serial )
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

			if ( Body == 42 )
			{
				Body = 0x8E;
				Hue = 0;
			}
		}
	}
}
