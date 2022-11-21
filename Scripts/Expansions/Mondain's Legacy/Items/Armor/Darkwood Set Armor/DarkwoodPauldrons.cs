using System;
using Server;
using Server.Engines.Craft;

namespace Server.Items
{
	[FlipableAttribute( 0x2B6C, 0x3163 )]
	public class DarkwoodPauldrons : WoodlandArms
	{
		public override int LabelNumber{ get{ return 1073485; } } // Darkwood Pauldrons
		
		public override SetItem SetID{ get{ return SetItem.Darkwood; } }
		public override int Pieces{ get{ return 6; } }
		
		public override int BasePhysicalResistance{ get{ return 8; } }
		public override int BaseFireResistance{ get{ return 5; } }
		public override int BaseColdResistance{ get{ return 5; } }
		public override int BasePoisonResistance{ get{ return 7; } }
		public override int BaseEnergyResistance{ get{ return 5; } }
				
		[Constructable]
		public DarkwoodPauldrons() : base()
		{
			Hue = 0x455;				
			SetHue = 0x494;
						
			Attributes.BonusHits = 2;		
			Attributes.DefendChance = 5;
			
			SetAttributes.ReflectPhysical = 25;
			SetAttributes.BonusStr = 10;		
			SetAttributes.NightSight = 1;		
			
			SetSelfRepair = 3;
			
			SetPhysicalBonus = 2;
			SetFireBonus = 5;
			SetColdBonus = 5;
			SetPoisonBonus = 3;
			SetEnergyBonus = 5;
		}

		public DarkwoodPauldrons( Serial serial ) : base( serial )
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
		
		public override int OnCraft( int quality, bool makersMark, Mobile from, CraftSystem craftSystem, Type typeRes, BaseTool tool, CraftItem craftItem, int resHue )
		{
			if ( resHue > 0 )
				Hue = resHue;
				
			Type resourceType = typeRes;

			if ( resourceType == null )
				resourceType = craftItem.Resources.GetAt( 0 ).ItemType;

			Resource = CraftResources.GetFromType( resourceType );
			
			switch ( Resource )
			{
				case CraftResource.Bloodwood: Attributes.RegenHits = 2; break;
				case CraftResource.Heartwood: Attributes.Luck = 40; break;
				case CraftResource.YewWood: Attributes.RegenHits = 1; break;
			}				
			
			return 0;
		}
	}
}