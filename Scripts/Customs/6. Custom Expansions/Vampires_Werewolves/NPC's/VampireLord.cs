///////////Made By Admin Maloki////////////////
//////////////Shadows of Death////////////////
/////////////////Vampire Set/////////////////

using System;
using Server;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "a vampire corpse" )]
	public class VampireLord : BaseVampire
	{
		[Constructable]
		public VampireLord () : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			if ( this.Female = Utility.RandomBool() )
			{
				Body = 0x191;
				Name = NameList.RandomName( "female" );
				AddItem( new Skirt( 1194 ) );
				Title = "the Vampire Lady";
			}
			else
			{
				Body = 0x190;
				Name = NameList.RandomName( "male" );
				AddItem( new ShortPants( 1194 ) );
				Title = "the Vampire Lord";
			}
			
			Hue = 0x847E;
			BaseSoundID = 357;
			Rank = 9;
			Criminal = true;

			SetStr( 401, 500 );
			SetDex( 81, 100 );
			SetInt( 151, 200 );
			SetHits( 241, 300 );
			SetDamage( 10, 12 );

			SetDamageType( ResistanceType.Physical, 50 );
			SetDamageType( ResistanceType.Fire, 25 );
			SetDamageType( ResistanceType.Energy, 25 );

			SetResistance( ResistanceType.Physical, 90, 100 );
			SetResistance( ResistanceType.Fire, 60, 70 );
			SetResistance( ResistanceType.Cold, 60, 70 );
			SetResistance( ResistanceType.Poison, 60, 70 );
			SetResistance( ResistanceType.Energy, 60, 70 );

			SetSkill( SkillName.Anatomy, 200, 200 );
			SetSkill( SkillName.EvalInt, 0, 0 );
			SetSkill( SkillName.Magery, 0, 0 );
			SetSkill( SkillName.Meditation, 0, 0 );
			SetSkill( SkillName.MagicResist, 200, 200 );
			SetSkill( SkillName.Tactics, 200, 200 );
			SetSkill( SkillName.Wrestling, 200, 200 );
			SetSkill( SkillName.Swords, 200, 200 );

			Fame = 2400;
			Karma = -2400;
			VirtualArmor = 90;

			int hairHue = 1109;
			Utility.AssignRandomHair( this, hairHue );
			if (!Female) Utility.AssignRandomFacialHair( this, hairHue );
			int clotheshue = 0x7E3;
			AddItem( new Robe(clotheshue  ) );
			int lowHue = Utility.RandomNeutralHue();
			AddItem( new Boots( lowHue ) );			
			
			Container pack = new Backpack();
			pack.DropItem( new Gold( 0, 50 ) );
			pack.Movable = false;
			AddItem( pack );
		}
        public override void OnDeath(Container c)
        {
            base.OnDeath(c);
			c.DropItem(new RewardScroll());
        }
		public VampireLord( Serial serial ) : base( serial )
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