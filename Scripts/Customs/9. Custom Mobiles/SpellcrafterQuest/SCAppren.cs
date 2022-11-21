using System;
using System.Collections;
using Server.Items;
using Server.Targeting;

namespace Server.Mobiles
{
	[CorpseName( "A apprentice's corpse" )]
	public class SCAppren : BaseCreature
	{
		[Constructable]
		public SCAppren () : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 ) 
		{
			Name = NameList.RandomName( "male" );
			Title = "An Evil Spell Crafter Apprentince";
			Body = 400;
			Hue = Utility.RandomSkinHue ();
			
		    SetStr( 50 );
			SetDex( 75 );
			SetInt( 150 );

			SetHits( 300 );

			SetDamage( 10, 16 );

			SetDamageType( ResistanceType.Physical, 100 );
			

			SetResistance( ResistanceType.Physical, 20 );
			SetResistance( ResistanceType.Fire, 20 );
			SetResistance( ResistanceType.Cold, 20 );
			SetResistance( ResistanceType.Poison, 20 );
			SetResistance( ResistanceType.Energy, 20 );

			SetSkill( SkillName.EvalInt, 75 );
			SetSkill( SkillName.Magery, 85 );
			SetSkill( SkillName.MagicResist, 50 );
			SetSkill( SkillName.Meditation, 85 );
			

			Fame = 0;
			Karma = 0;

			VirtualArmor = 25;
			AddItem (new Robe (1392));
			AddItem (new BlackStaff());
			AddItem (new Shoes ());
			int hairHue = 2018;
			
			switch (Utility.Random (1) )
			{
				case 0: AddItem (new LongHair ( hairHue ) ) ; break;
				
			}
			
			PackGem();
			PackGold( 50, 150  );
//			PackItem( new MagicJewel () );
			PackPotion();
			

			
		}


		public override bool CanRummageCorpses{ get{ return true; } }
		public override bool AlwaysMurderer{ get{ return true; } }
		

		public SCAppren( Serial serial ) : base( serial )
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


