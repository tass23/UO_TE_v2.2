using System;
using Server.Items;
using Server;
using Server.Misc;

namespace Server.Mobiles
{
	public class MadPainter : BaseCreature
	{
		public override bool CanTeach { get { return true; } }

		[Constructable]
		public MadPainter() : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			SetStr( 500, 660 );
			SetDex( 500, 500 );
			SetInt( 200, 250 );
			
			SetHits( 5000, 5000 );
			
			SetDamage( 10, 20 );
			
			SetDamageType( ResistanceType.Physical, 70 );
			SetDamageType( ResistanceType.Cold, 70 );
			SetDamageType( ResistanceType.Fire, 70 );
			
			SetResistance( ResistanceType.Physical, 50, 100 );
			SetResistance( ResistanceType.Energy, 50, 100 );
			SetResistance( ResistanceType.Poison, 50, 100 );
			SetResistance( ResistanceType.Cold, 50, 100 );
			SetResistance( ResistanceType.Fire, 50, 100 );
			
			SetSkill( SkillName.Wrestling, 95.1, 100.0 );
            SetSkill( SkillName.EvalInt, 100.1, 150.0 );
            SetSkill( SkillName.Magery, 100.1, 150.0 );
			SetSkill( SkillName.Anatomy, 95.1, 100.0 );
			SetSkill( SkillName.MagicResist, 95.1, 100.0 );
			SetSkill( SkillName.Swords, 95.1, 100.0 );
			SetSkill( SkillName.Tactics, 95.1, 100.0 );
			SetSkill( SkillName.Parry, 95.1, 100.0 );
			SetSkill( SkillName.Focus, 95.1, 100.0 );
			
			Fame = 250;
			Karma = -250;
			VirtualArmor = 40;

			SpeechHue = Utility.RandomDyedHue();
			Title = "the mad painter";
			Hue = Utility.RandomSkinHue();

			if( this.Female = Utility.RandomBool() )
			{
				this.Body = 0x191;
				this.Name = NameList.RandomName( "female" );
			}
			else
			{
				this.Body = 0x190;
				this.Name = NameList.RandomName( "male" );
			}
			
			AddItem( new Doublet( Utility.RandomDyedHue() ) );
			AddItem( new Sandals( Utility.RandomNeutralHue() ) );
			AddItem( new ShortPants( Utility.RandomNeutralHue() ) );
			AddItem( new HalfApron( Utility.RandomDyedHue() ) );

			Utility.AssignRandomHair( this );
			Container pack = new Backpack();
			pack.DropItem( new Gold( 250, 300 ) );
            pack.DropItem( new OilCloth() );
			pack.Movable = false;
			AddItem( pack );
			switch ( Utility.Random( 3 ) )
			{
				case 0: PackItem( new BodyPaint() ); break;
			}
		}

		public override bool ClickTitle { get { return false; } }

		public MadPainter( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int)0 ); // version 
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}
}