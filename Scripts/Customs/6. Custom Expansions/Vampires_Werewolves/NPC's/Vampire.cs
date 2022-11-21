using System;
using Server.Misc;
using Server.Network;
using System.Collections;
using Server.Items;
using Server.Targeting;

namespace Server.Mobiles
{
	[CorpseName( "a vampire's corpse" )]
	public class Vampire : BaseVampire
	{
		[Constructable]
		public Vampire() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			if ( this.Female = Utility.RandomBool() )
			{
				Body = 0x191;
				Name = NameList.RandomName( "female" );
				AddItem( new Skirt( 1194 ) );
			}
			else
			{
				Body = 0x190;
				Name = NameList.RandomName( "male" );
				AddItem( new ShortPants( 1194 ) );
			}

			Title = "the vampire";
			Hue = 1150;
			BaseSoundID = 0x4B0;
			Rank = 1;
			Criminal = true;
	
			SetStr( 81, 105 );
			SetDex( 191, 215 );
			SetInt( 126, 150 );
			SetHits( 49, 63 );
			SetDamage( 5, 10 );

			SetDamageType( ResistanceType.Cold, 100 );

			SetResistance( ResistanceType.Physical, 30, 50 );
			SetResistance( ResistanceType.Fire, 5, 10 );
			SetResistance( ResistanceType.Cold, 30, 50 );
			SetResistance( ResistanceType.Poison, 30, 50 );
			SetResistance( ResistanceType.Energy, 30, 50 );

			SetSkill( SkillName.MagicResist, 50.0, 70.0 );
			SetSkill( SkillName.Tactics, 70.1, 80.0 );
			SetSkill( SkillName.Wrestling, 70.1, 80.1 );
			SetSkill( SkillName.Anatomy, 70.1, 80.0 );
			SetSkill( SkillName.EvalInt, 90.1, 100.0 );
			SetSkill( SkillName.Magery, 70.1, 80.0 );

			//new GhostlySteed().Rider = this;
			
			Fame = 150;
			Karma = -150;
			
			Container pack = new Backpack();
			pack.DropItem( new Gold( 0, 50 ) );
			pack.Movable = false;
			AddItem( pack );

			int hairHue = 1109;
			Utility.AssignRandomHair( this, hairHue );
			if (!Female) Utility.AssignRandomFacialHair( this, hairHue );
			int clotheshue = 0x7E3;
			AddItem( new Robe(clotheshue  ) );
			int lowHue = Utility.RandomNeutralHue();
			AddItem( new Boots( lowHue ) );
		}

		public Vampire( Serial serial ) : base( serial )
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