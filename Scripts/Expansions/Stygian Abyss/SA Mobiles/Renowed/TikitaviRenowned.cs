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
    [CorpseName( "Tikitavi [Renowned] corpse" )] 
    public class TikitaviRenowned : BaseRenowned
	{
        public override Type[] UniqueSAList { get { return new Type[] { typeof(BasiliskHideBreastplate), typeof( CrystallineBlackrock ) }; } }
        public override Type[] SharedSAList {get{return new Type[] {typeof( LegacyOfDespair ), typeof( CrystalShards ), typeof( MysticsGarb )}; } }
		public override InhumanSpeech SpeechType{ get{ return InhumanSpeech.Ratman; } }

		[Constructable]
		public TikitaviRenowned() : base( AIType.AI_Melee )
		{
			Name = "Tikitavi";
			Title = "[Renowned]";
			Body = 42;
			BaseSoundID = 437;

			SetStr( 300, 350 );
			SetDex( 100, 150 );
			SetInt( 200, 250 );

			SetHits( 45000, 50000 );

			SetDamage( 7, 9 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 25, 30 );
			SetResistance( ResistanceType.Fire, 20, 30 );
			SetResistance( ResistanceType.Cold, 20, 35 );
			SetResistance( ResistanceType.Poison, 10, 20 );
			SetResistance( ResistanceType.Energy, 10, 20 );

			SetSkill( SkillName.MagicResist, 35.1, 60.0 );
			SetSkill( SkillName.Tactics, 50.1, 75.0 );
			SetSkill( SkillName.Wrestling, 50.1, 75.0 );

			Fame = 1500;
			Karma = -1500;

			VirtualArmor = 28;

                        PackItem( new EssenceBalance() );
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Meager );
			// TODO: weapon, misc
		}
		
		
		
		public override bool CanRummageCorpses{ get{ return true; } }
		public override int Hides{ get{ return 8; } }
		public override HideType HideType{ get{ return HideType.Spined; } }

		public TikitaviRenowned( Serial serial ) : base( serial )
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