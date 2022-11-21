///////////Made By Admin Maloki////////////////
//////////////Shadows of Death////////////////
/////////////////Vampire Set/////////////////

using System;
using Server;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "a vampire corpse" )]
	public class VampireDeathKnight : BaseVampire
	{
		[Constructable]
		public VampireDeathKnight () : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			if ( this.Female = Utility.RandomBool() )
			{
				Body = 0x191;
				Name = NameList.RandomName( "female" );
				AddItem( new Skirt( 0x7E3 ) );
			}
			else
			{
				Body = 0x190;
				Name = NameList.RandomName( "male" );
				AddItem( new LongPants( 0x7E3 ) );
			}
			Title = "the Vampire Death Knight";
			Hue = 0x847E;
			BaseSoundID = 357;
			Rank = 7;
			Criminal = true;

			SetStr( 251, 350 );
			SetDex( 61, 80 );
			SetInt( 101, 150 );
			SetHits( 151, 210 );
			SetDamage( 7, 9 );

			SetDamageType( ResistanceType.Physical, 50 );
			SetDamageType( ResistanceType.Fire, 25 );
			SetDamageType( ResistanceType.Energy, 25 );

			SetResistance( ResistanceType.Physical, 70, 80 );
			SetResistance( ResistanceType.Fire, 20, 30 );
			SetResistance( ResistanceType.Cold, 20, 30 );
			SetResistance( ResistanceType.Poison, 20, 30 );
			SetResistance( ResistanceType.Energy, 20, 30 );

			SetSkill( SkillName.Anatomy, 150, 150);
			SetSkill( SkillName.EvalInt, 0, 0 );
			SetSkill( SkillName.Magery, 0, 0 );
			SetSkill( SkillName.Meditation, 0, 0 );
			SetSkill( SkillName.MagicResist, 150, 150);
			SetSkill( SkillName.Tactics, 150, 150);
			SetSkill( SkillName.Wrestling, 150, 150);
			SetSkill( SkillName.Swords, 150, 150);

			new GhostlySteed().Rider = this;
			
			Fame = 2400;
			Karma = -2400;
			VirtualArmor = 90;

			int hairHue = 1109;
			Utility.AssignRandomHair( this, hairHue );
			if (!Female) Utility.AssignRandomFacialHair( this, hairHue );
			int clotheshue = 0x7E3;
			AddItem( new FancyShirt(1194));
			AddItem( new Cloak( clotheshue ));
			AddItem( new ArmorOfTheVampires() );
			AddItem( new BladeOfTheVampires() );
			int lowHue = Utility.RandomNeutralHue();
			AddItem( new Boots( lowHue ) );
			
			Container pack = new Backpack();
			pack.DropItem( new Gold( 0, 50 ) );
			pack.Movable = false;
			AddItem( pack );
		}

		public VampireDeathKnight( Serial serial ) : base( serial )
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