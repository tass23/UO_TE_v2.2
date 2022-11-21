using System;
using Server;
using Server.Items;
namespace Server.Mobiles
{
	public class LecherousInvader : BaseCreature
	{
		[Constructable]
		public LecherousInvader() : base(AIType.AI_Mage, FightMode.Aggressor, 10, 1, 0.2, 0.4)
		{
			Title = "a Lecherous Invader";
			Hue = Utility.RandomSkinHue();
			
			if ( this.Female = Utility.RandomBool() )
			{
				Body = 0x191;
				Name = NameList.RandomName( "female" );
				
				int hue = Utility.RandomNeutralHue();
				
				Item hair = new Item( Utility.RandomList( 0x2049, 0x203D, 0x203C, 0x2046 ) );
				hair.Hue = Utility.RandomNondyedHue();
				hair.Layer = Layer.Hair;
				hair.Movable = false;
				AddItem( hair );
				
				Item Tunic = new Tunic();
				Tunic.Hue = 1;
				Tunic.LootType = LootType.Blessed;
				Tunic.Movable = false;
				AddItem( Tunic );
				
				Item Sandals = new Sandals();
				Sandals.Hue = 903;
				Sandals.LootType = LootType.Blessed;
				Sandals.Movable = false;
				AddItem( Sandals );
				
				Item HalfApron = new HalfApron();
				HalfApron.Hue = 903;
				HalfApron.LootType = LootType.Blessed;
				HalfApron.Movable = false;
				AddItem( HalfApron );
				
				Item Skirt = new Skirt();
				Skirt.Hue = 1;
				Skirt.LootType = LootType.Blessed;
				Skirt.Movable = false;
				AddItem( Skirt );
			}
			else
			{
				Body = 0x190;
				Name = NameList.RandomName( "male" );
				
				int hue = Utility.RandomNeutralHue();
				
				Item hair = new Item( Utility.RandomList( 0x203B, 0x2048, 0x2045 ) );
				hair.Hue = Utility.RandomNondyedHue();
				hair.Layer = Layer.Hair;
				hair.Movable = false;
				AddItem( hair );
				
				Item FancyShirt = new FancyShirt();
				FancyShirt.Hue = 0;
				FancyShirt.Layer = Layer.MiddleTorso;
				FancyShirt.LootType = LootType.Blessed;
				FancyShirt.Movable = false;
				AddItem( FancyShirt );
				
				Item Boots = new Boots();
				Boots.Hue = 0;
				Boots.LootType = LootType.Blessed;
				Boots.Movable = false;
				AddItem( Boots );
				
				Item FeatheredHat = new FeatheredHat();
				FeatheredHat.Hue = 1;
				FeatheredHat.LootType = LootType.Blessed;
				FeatheredHat.Movable = false;
				AddItem( FeatheredHat );
				
				Item LongPants = new LongPants();
				LongPants.Hue = 1;
				LongPants.LootType = LootType.Blessed;
				LongPants.Movable = false;
				AddItem( LongPants );
				
				Item Shirt = new Shirt();
				Shirt.Hue = 1;
				Shirt.Layer = Layer.OuterTorso;
				Shirt.LootType = LootType.Blessed;
				Shirt.Movable = false;
				AddItem( Shirt );
				
				Item Obi = new Obi();
				Obi.Hue = 253;
				Obi.LootType = LootType.Blessed;
				Obi.Movable = false;
				AddItem( Obi );
				
				Item Kryss = new Kryss();
				Kryss.Hue = 0;
				Kryss.LootType = LootType.Blessed;
				Kryss.Movable = false;
				AddItem( Kryss );
			}
			
			SetStr( 65, 100 );
			SetDex( 65, 100 );
			SetInt( 65, 100 );
			
			SetHits( 100, 350 );
			
			SetDamage( 65, 100 );
			
			SetDamageType( ResistanceType.Physical, 65, 100 );
			
			SetResistance( ResistanceType.Physical, 65, 100 );
			
			SetSkill( SkillName.Swords, 65.0, 100.0 );
			SetSkill( SkillName.Fencing, 65.0, 100.0 );
			SetSkill( SkillName.Anatomy, 65.0, 100.0 );
			SetSkill( SkillName.MagicResist, 65.0, 100.0 );
			SetSkill( SkillName.Tactics, 65.0, 100.0 );
			SetSkill( SkillName.Wrestling, 65.0, 100.0 );
			
			Fame = 5000;
			Karma = 5000;
			
			VirtualArmor = 90;
		}
		public LecherousInvader( Serial serial ) : base( serial )
		{
		}
		
		public override void OnDoubleClick( Mobile from )
		{
			this.Say( "We come as friends, but leave as enemies.");
			
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