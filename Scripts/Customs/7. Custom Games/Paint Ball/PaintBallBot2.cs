using System;
using System.Collections;
using Server.Items;
using Server.Targeting;

namespace Server.Mobiles
{
	[CorpseName( "a Paint Ball Bot corpse" )]
	public class PaintBallBot2 : BaseCreature
	{
		[Constructable]
		public PaintBallBot2() : base( AIType.AI_Archer, FightMode.Closest, 10, 1, 0.1, 0.2 )
		{
			Name = "Paint Ball Bot";
			Body = 401;
			Hue = 33770;
			
			SetStr( 100 );
			SetDex( 100 );
			SetInt( 100 );

			SetHits( 100 );

			SetDamage( 1, 1 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 0 );
			SetResistance( ResistanceType.Fire, 0 );
			SetResistance( ResistanceType.Cold, 0 );
			SetResistance( ResistanceType.Poison, 0 );
			SetResistance( ResistanceType.Energy, 0 );
			
			SetSkill( SkillName.Tactics, 100.0 );
			SetSkill( SkillName.Anatomy, 100.0 );
			SetSkill( SkillName.Archery, 100.0 );
			SetSkill( SkillName.Parry, 100.0 );

			Fame = 0;
			Karma = 0;

			VirtualArmor = 0;

			AddItem( new PaintBallGunBotMed() );
			AddItem( ItemSet( new FemaleLeatherChest() ) );
			AddItem( ItemSet( new LeatherShorts() ) );
			AddItem( ItemSet( new Sandals() ) );
			
			Item hair = new Item( Utility.RandomList( 0x203C ) );
			hair.Hue = Utility.RandomNondyedHue();
			hair.Layer = Layer.Hair;
			hair.Movable = false;
			AddItem( hair );
		}

		public static Item ItemSet( Item item )
		{
			item.Hue = 62;
			return item;
		}

		public override bool AlwaysMurderer{ get{ return true; } }
		public override bool Unprovokable{ get{ return true; } }
		public override bool Uncalmable{ get{ return true; } }


		public PaintBallBot2( Serial serial ) : base( serial )
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
