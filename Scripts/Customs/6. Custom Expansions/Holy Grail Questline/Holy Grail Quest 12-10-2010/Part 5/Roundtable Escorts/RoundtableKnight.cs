using System;
using Server;
using Server.Items;
using Server.Mobiles;

namespace Server.Engines.Quests
{
	public class RoundtableKnight : BaseEscort
	{
		public override Type[] Quests
		{
			get{ return new Type[]
			{
				typeof( RoundtableQuest )		
			};}
		}

		[Constructable]
		public RoundtableKnight() : base()
		{
			Name = NameList.RandomName( "grailknights" );
			Title = "a Knight of the Round Table";
			NameHue = 68;
		}

		public RoundtableKnight( Serial serial ) : base( serial )
		{
		}

		public override void InitBody()
		{
			Female = false;
			CantWalk = false;
			Race = Race.Human;
			Hue = 0x8400;
			Utility.AssignRandomHair( this );
		}

		public override void InitOutfit()
		{
			AddItem( new Backpack() );

			PlateArms arms = new PlateArms();
			arms.Hue = 2968;
			AddItem( arms );

			PlateGloves gloves = new PlateGloves();
			gloves.Hue = 2968;
			AddItem( gloves );

			PlateLegs legs = new PlateLegs();
			legs.Hue = 2968;
			AddItem( legs );

			PlateGorget neck = new PlateGorget();
			neck.Hue = 2968;
			AddItem( neck );

			PlateChest chest = new PlateChest();
			chest.Hue = 2968;
			AddItem( chest );

			Broadsword weapon = new Broadsword();
			weapon.Hue = 2968;
			weapon.Name = "an annointed broadsword";
			weapon.EngravedText = "of Righteous Justice";
			weapon.Movable = false;
			AddItem( weapon );
			
			HeaterShield shield = new HeaterShield();
			shield.Hue = 2968;
			shield.Name = "Heater Shield of Righteousness";
			shield.Movable = false;
			AddItem( shield );
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