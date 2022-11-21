using System; 
using System.Collections; 
using Server.Items; 
using Server.ContextMenus; 
using Server.Misc; 
using Server.Network; 

namespace Server.Mobiles 
{ 
	public class lazyrabbit : BaseCreature 
	{ 
		[Constructable] 
		public lazyrabbit() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 ) 
		{ 
			Name = "Lazy Rabbit";
			Hue = 1723; 
			Body = 302;
                         

			SetStr( 200 );
			SetDex( 200 );
			SetInt( 220 );

			SetHits( 1200 );
			SetDamage( 25, 35 );

			SetDamageType( ResistanceType.Physical, 70 );
			SetDamageType( ResistanceType.Cold, 60 );
			SetDamageType( ResistanceType.Fire, 80 );
			SetDamageType( ResistanceType.Energy, 70 );
			SetDamageType( ResistanceType.Poison, 90 );

			SetResistance( ResistanceType.Physical, 90 );
			SetResistance( ResistanceType.Fire, 90 );
			SetResistance( ResistanceType.Cold, 88 );
			SetResistance( ResistanceType.Poison, 90 );
			SetResistance( ResistanceType.Energy, 90 );

			SetSkill( SkillName.Anatomy, 100.0 );
			SetSkill( SkillName.Archery, 100.0 );
			SetSkill( SkillName.Wrestling, 110.0 );
			SetSkill( SkillName.MagicResist, 115.0 );
			SetSkill( SkillName.Tactics, 110.0 );


			Fame = 4000;
			Karma = 1000;

			VirtualArmor = 30;
            

			
			}

		public override void GenerateLoot()
		{

			switch ( Utility.Random( 3 ))
			{
				case 0: PackItem( new eggeaster() ); break;
						
			}
			PackGold( 100, 200);
            AddLoot( LootPack.Gems, 5 );  
        }
 
		public override bool AlwaysMurderer{ get{ return true; } }

		public lazyrabbit( Serial serial ) : base( serial ) 
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