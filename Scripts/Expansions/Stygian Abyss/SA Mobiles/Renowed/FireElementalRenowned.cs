using System;
using Server;
using Server.Items;
using Server.Spells;
using Server.Spells.Seventh;
using Server.Spells.Fifth;
using System.Collections.Generic;

namespace Server.Mobiles
{
    [CorpseName( "Fire Elemental [Renowned] corpse" )] 
    public class FireElementalRenowned : BaseRenowned
	{
		

        public override Type[] UniqueSAList{ get { return new Type[] {typeof( JadeWarAxe ) }; }}
        public override Type[] SharedSAList{ get { return new Type[] {typeof( TokenOfHolyFavor), typeof( LavaSerpentCrust ), typeof( SwordOfShatteredHopes ) }; }}

      	public override double DispelDifficulty{ get{ return 117.5; } }
		public override double DispelFocus{ get{ return 45.0; } }

		[Constructable]
		public FireElementalRenowned () : base( AIType.AI_Mage )
		{
			Name = "Fire Elemental";
			Title = "[Renowned]";
			Body = 15;
			BaseSoundID = 838;

            Hue = 1161;

			SetStr( 450, 500 );
			SetDex( 200, 250 );
			SetInt( 300, 350 );

			SetHits( 1200, 1600 );

			SetDamage( 7, 9 );

			SetDamageType( ResistanceType.Physical, 25 );
			SetDamageType( ResistanceType.Fire, 75 );

			SetResistance( ResistanceType.Physical, 45, 60 );
			SetResistance( ResistanceType.Fire, 70, 80 );
			SetResistance( ResistanceType.Cold, 5, 10 );
			SetResistance( ResistanceType.Poison, 30, 50 );
			SetResistance( ResistanceType.Energy, 40, 60 );

			SetSkill( SkillName.EvalInt, 100.1, 110.0 );
			SetSkill( SkillName.Magery, 105.1, 110.0 );
			SetSkill( SkillName.MagicResist, 110.2, 120.0 );
			SetSkill( SkillName.Tactics, 100.1, 105.0 );
			SetSkill( SkillName.Wrestling, 90.1, 100.0 );

			Fame = 4500;
			Karma = -4500;

			VirtualArmor = 40;

            PackItem( new EssencePrecision() );

			ControlSlots = 4;

			PackItem( new SulfurousAsh( 3 ) );

			AddItem( new LightSource() );
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Average );
			AddLoot( LootPack.Meager );
			AddLoot( LootPack.Gems );
		}
		
		public override bool BleedImmune{ get{ return true; } }
		public override int TreasureMapLevel{ get{ return 2; } }

		public FireElementalRenowned( Serial serial ) : base( serial )
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

			if ( BaseSoundID == 274 )
				BaseSoundID = 838;
		}
	}
}