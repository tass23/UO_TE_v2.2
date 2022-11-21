using System;
using Server;
using Server.Items;
namespace Server.Mobiles
{
	public class NativeAmerican : BaseCreature
	{
		[Constructable]
		public NativeAmerican() : base(AIType.AI_Mage, FightMode.Aggressor, 10, 1, 0.2, 0.4)
		{
			Title = "a Native American";
			Hue = Utility.RandomSkinHue();
			
			if ( this.Female = Utility.RandomBool() )
			{
				Body = 0x191;
				Hue = 33776;
				Name = NameList.RandomName( "female" );
				
				int hue = Utility.RandomNeutralHue();
				
				Item hair = new Item( Utility.RandomList( 0x2049, 0x203D, 0x203C ) );
				hair.Hue = Utility.RandomNondyedHue();
				hair.Layer = Layer.Hair;
				hair.Movable = false;
				AddItem( hair );
				
				Item LeatherGorget = new LeatherGorget();
				LeatherGorget.LootType = LootType.Blessed;
				LeatherGorget.Movable = false;
				AddItem( LeatherGorget );
				
				Item Sandals = new Sandals();
				Sandals.LootType = LootType.Blessed;
				Sandals.Movable = false;
				AddItem( Sandals );
				
				Item LeatherSkirt = new LeatherSkirt();
				LeatherSkirt.LootType = LootType.Blessed;
				LeatherSkirt.Movable = false;
				AddItem( LeatherSkirt );
				
				Item LeatherGloves = new LeatherGloves();
				LeatherGloves.LootType = LootType.Blessed;
				LeatherGloves.Movable = false;
				AddItem( LeatherGloves );
			}
			else
			{
				Body = 0x190;
				Hue = 33776;
				Name = NameList.RandomName( "male" );
				
				int hue = Utility.RandomNeutralHue();
				
				Item hair = new Item( Utility.RandomList( 0x2044, 0x2048 ) );
				hair.Hue = Utility.RandomNondyedHue();
				hair.Layer = Layer.Hair;
				hair.Movable = false;
				AddItem( hair );
				
				Item LeatherGorget = new LeatherGorget();
				LeatherGorget.LootType = LootType.Blessed;
				LeatherGorget.Movable = false;
				AddItem( LeatherGorget );
				
				Item Sandals = new Sandals();
				Sandals.LootType = LootType.Blessed;
				Sandals.Movable = false;
				AddItem( Sandals );
				
				Item Bow = new Bow();
				Bow.LootType = LootType.Blessed;
				Bow.Movable = false;
				AddItem( Bow );
			}
			
			SetStr( 65, 100 );
			SetDex( 65, 100 );
			SetInt( 65, 100 );
			
			SetHits( 100, 350 );
			
			SetDamage( 65, 100 );
			
			SetDamageType( ResistanceType.Physical, 65, 100 );
			
			SetResistance( ResistanceType.Physical, 65, 100 );
			
			SetSkill( SkillName.Discordance, 65.0, 100.0 );
			SetSkill( SkillName.Musicianship, 65.0, 100.0 );
			SetSkill( SkillName.Peacemaking, 65.0, 100.0 );
			SetSkill( SkillName.Provocation, 65.0, 100.0 );
			SetSkill( SkillName.Archery, 65.0, 100.0 );
			SetSkill( SkillName.Anatomy, 65.0, 100.0 );
			SetSkill( SkillName.MagicResist, 65.0, 100.0 );
			SetSkill( SkillName.Tactics, 65.0, 100.0 );
			SetSkill( SkillName.Wrestling, 65.0, 100.0 );
			
			Fame = -15000;
			Karma = -15000;
			
			VirtualArmor = 90;
			
		}
		public NativeAmerican( Serial serial ) : base( serial )
		{
		}
		
		public override void OnDoubleClick( Mobile from )
		{
			this.Say( "Welcome friend! Join us in harmony!");
			
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