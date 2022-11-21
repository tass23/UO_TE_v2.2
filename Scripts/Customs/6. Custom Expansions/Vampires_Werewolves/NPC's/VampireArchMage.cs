///////////Made By Admin Maloki////////////////
//////////////Shadows of Death////////////////
/////////////////Vampire Set/////////////////

using System;
using Server;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "a vampire's corpse" )]
	public class VampireArchMage : BaseVampire
	{
		[Constructable]
		public VampireArchMage () : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
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

			Title = "the Vampire Arch Mage";
			Hue = 0x847E;
			BaseSoundID = 357;
			Rank = 5;
			Criminal = true;

			SetStr( 81, 105 );
			SetDex( 191, 215 );
			SetInt( 126, 150 );
			SetHits( 49, 63 );
			SetDamage( 5, 10 );

			SetDamageType( ResistanceType.Physical, 50 );
			SetDamageType( ResistanceType.Fire, 25 );
			SetDamageType( ResistanceType.Energy, 25 );

			SetResistance( ResistanceType.Physical, 20, 30 );
			SetResistance( ResistanceType.Fire, 40, 50 );
			SetResistance( ResistanceType.Cold, 40, 50 );
			SetResistance( ResistanceType.Poison, 40, 50 );
			SetResistance( ResistanceType.Energy, 40, 50 );

			SetSkill( SkillName.Anatomy, 100, 100 );
			SetSkill( SkillName.EvalInt, 150, 200 );
			SetSkill( SkillName.Magery, 200, 200 );
			SetSkill( SkillName.Meditation, 200, 200 );
			SetSkill( SkillName.MagicResist, 200, 200 );
			SetSkill( SkillName.Tactics, 100, 100 );
			SetSkill( SkillName.Wrestling, 100, 100 );

			Fame = 2400;
			Karma = -2400;

			VirtualArmor = 90;

			int hairHue = 1109;
			Utility.AssignRandomHair( this, hairHue );
			if (!Female) Utility.AssignRandomFacialHair( this, hairHue );
			int clotheshue = 0x7E3;
			AddItem( new Robe(clotheshue) );
			int lowHue = Utility.RandomNeutralHue();
			AddItem( new Sandals( lowHue ) );
			AddItem( new WizardsHat( 1194 ) );
			AddItem( new Cloak( 1194 ));
			AddItem( new StaffoftheMagister());
			
			Container pack = new Backpack();
			pack.DropItem( new Gold( 0, 50 ) );
			pack.Movable = false;
			AddItem( pack );
		}

		public VampireArchMage( Serial serial ) : base( serial )
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