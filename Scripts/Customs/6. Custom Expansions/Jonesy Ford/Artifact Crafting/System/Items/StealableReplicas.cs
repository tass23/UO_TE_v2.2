using System;
using Server.ContextMenus;
using System.Text;
using Server.Mobiles;
using System.Collections;
using System.Collections.Generic;
using Server.Engines.Craft;

namespace Server.Items
{
	public abstract class BaseArtifactReplica : Item
	{
		public abstract int ArtifactRarity{ get; }
		public override bool ForceShowProperties{ get{ return true; } }
		
        /* If this is uncommented, items crafted will retain color from resources.
		public bool RetainsColorFrom
        {
            get
            {
                return false;
            }
        }
		*/

		public BaseArtifactReplica( int itemID ) : base( itemID )
		{
			Weight = 10.0;
		}

		public override void AddNameProperties( ObjectPropertyList list )
		{
			base.AddNameProperties( list );
			list.Add( 1061078, this.ArtifactRarity.ToString() ); // artifact rarity ~1_val~
		}
		
		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );
			list.Add( 1061078, this.ArtifactRarity.ToString()); // artifact rarity ~1_val~
		}
		
		public BaseArtifactReplica( Serial serial ) : base( serial )
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

	#region Doom Stealable Artifact Replicas
	public class BackpackArtifactRep : BaseArtifactReplicaContainer
	{
		public override int ArtifactRarity{ get{ return 5; } }

		[Constructable]
		public BackpackArtifactRep() : base( 0x9B2 )
		{
			Name = "Backpack" + String.Format(" [Replica]");
		}

		public BackpackArtifactRep( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.WriteEncodedInt( 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadEncodedInt();
		}
	}

	public class BloodyWaterArtifactRep : BaseArtifactReplica
	{
		public override int ArtifactRarity{ get{ return 5; } }

		[Constructable]
		public BloodyWaterArtifactRep() : base( 0xE23 )
		{
			Name = "Bloody Water" + String.Format(" [Replica]");
		}

		public BloodyWaterArtifactRep( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.WriteEncodedInt( 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadEncodedInt();
		}
	}

	public class BooksWestArtifactRep : BaseArtifactReplica
	{
		public override int ArtifactRarity{ get{ return 3; } }

		[Constructable]
		public BooksWestArtifactRep() : base( 0x1E25 )
		{
			Name = "Books" + String.Format(" [Replica]");
		}

		public BooksWestArtifactRep( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.WriteEncodedInt( 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadEncodedInt();
		}
	}

	public class BooksNorthArtifactRep : BaseArtifactReplica
	{
		public override int ArtifactRarity{ get{ return 3; } }

		[Constructable]
		public BooksNorthArtifactRep() : base( 0x1E24 )
		{
			Name = "Books" + String.Format(" [Replica]");
		}

		public BooksNorthArtifactRep( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.WriteEncodedInt( 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadEncodedInt();
		}
	}

	public class BooksFaceDownArtifactRep : BaseArtifactReplica
	{
		public override int ArtifactRarity{ get{ return 3; } }

		[Constructable]
		public BooksFaceDownArtifactRep() : base( 0x1E21 )
		{
			Name = "Books" + String.Format(" [Replica]");
		}

		public BooksFaceDownArtifactRep( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.WriteEncodedInt( 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadEncodedInt();
		}
	}

	public class BottleArtifactRep : BaseArtifactReplica
	{
		public override int ArtifactRarity{ get{ return 1; } }

		[Constructable]
		public BottleArtifactRep() : base( 0xE28 )
		{
			Name = "Bottle" + String.Format(" [Replica]");
		}

		public BottleArtifactRep( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.WriteEncodedInt( 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadEncodedInt();
		}
	}

	public class BrazierArtifactRep : BaseArtifactReplica
	{
		public override int ArtifactRarity{ get{ return 2; } }

		[Constructable]
		public BrazierArtifactRep() : base( 0xE31 )
		{
			Name = "Brazier" + String.Format(" [Replica]");
			Light = LightType.Circle150;
		}

		public BrazierArtifactRep( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.WriteEncodedInt( 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadEncodedInt();
		}
	}

	public class CocoonArtifactRep : BaseArtifactReplica
	{
		public override int ArtifactRarity{ get{ return 7; } }

		[Constructable]
		public CocoonArtifactRep() : base( 0x10DA )
		{
			Name = "Cocoon" + String.Format(" [Replica]");
		}

		public CocoonArtifactRep( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.WriteEncodedInt( 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadEncodedInt();
		}
	}
	
	public class DamagedBooksArtifactRep : BaseArtifactReplica
	{
		public override int ArtifactRarity{ get{ return 1; } }

		[Constructable]
		public DamagedBooksArtifactRep() : base( 0xC16 )
		{
			Name = "Damaged Books" + String.Format(" [Replica]");
		}

		public DamagedBooksArtifactRep( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.WriteEncodedInt( 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadEncodedInt();
		}
	}

	public class EggCaseArtifactRep : BaseArtifactReplica
	{
		public override int ArtifactRarity{ get{ return 5; } }

		[Constructable]
		public EggCaseArtifactRep() : base( 0x10D9 )
		{
			Name = "Egg Case" + String.Format(" [Replica]");
		}

		public EggCaseArtifactRep( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.WriteEncodedInt( 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadEncodedInt();
		}
	}

	public class GruesomeStandardArtifactRep : BaseArtifactReplica
	{
		public override int ArtifactRarity{ get{ return 5; } }

		[Constructable]
		public GruesomeStandardArtifactRep() : base( 0x428 )
		{
			Name = "Gruesome Standard" + String.Format(" [Replica]");
		}

		public GruesomeStandardArtifactRep( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.WriteEncodedInt( 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadEncodedInt();
		}
	}

	public class LampPostArtifactRep : BaseArtifactReplica
	{
		public override int ArtifactRarity{ get{ return 3; } }

		[Constructable]
		public LampPostArtifactRep() : base( 0xB24 )
		{
			Name = "Lamp Post" + String.Format(" [Replica]");
			Light = LightType.Circle300;
		}

		public LampPostArtifactRep( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.WriteEncodedInt( 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadEncodedInt();
		}
	}

	public class LeatherTunicArtifactRep : BaseArtifactReplica
	{
		public override int ArtifactRarity{ get{ return 9; } }

		[Constructable]
		public LeatherTunicArtifactRep() : base( 0x13CA )
		{
			Name = "Leather Tunic" + String.Format(" [Replica]");
		}

		public LeatherTunicArtifactRep( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.WriteEncodedInt( 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadEncodedInt();
		}
	}

	public class RockArtifactRep : BaseArtifactReplica
	{
		public override int ArtifactRarity{ get{ return 1; } }

		[Constructable]
		public RockArtifactRep() : base( 0x1363 )
		{
			Name = "Rock" + String.Format(" [Replica]");
		}

		public RockArtifactRep( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.WriteEncodedInt( 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadEncodedInt();
		}
	}

	public class RuinedPaintingArtifactRep : BaseArtifactReplica
	{
		public override int ArtifactRarity{ get{ return 12; } }

		[Constructable]
		public RuinedPaintingArtifactRep() : base( 0xC2C )
		{
			Name = "Ruined Painting" + String.Format(" [Replica]");
		}

		public RuinedPaintingArtifactRep( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.WriteEncodedInt( 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadEncodedInt();
		}
	}

	public class SaddleArtifactRep : BaseArtifactReplica
	{
		public override int ArtifactRarity{ get{ return 9; } }

		[Constructable]
		public SaddleArtifactRep() : base( 0xF38 )
		{
			Name = "Saddle" + String.Format(" [Replica]");
		}

		public SaddleArtifactRep( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.WriteEncodedInt( 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadEncodedInt();
		}
	}

	public class SkinnedDeerArtifactRep : BaseArtifactReplica
	{
		public override int ArtifactRarity{ get{ return 8; } }

		[Constructable]
		public SkinnedDeerArtifactRep() : base( 0x1E91 )
		{
			Name = "Skinned Deer" + String.Format(" [Replica]");
		}

		public SkinnedDeerArtifactRep( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.WriteEncodedInt( 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadEncodedInt();
		}
	}

	public class SkinnedGoatArtifactRep : BaseArtifactReplica
	{
		public override int ArtifactRarity{ get{ return 5; } }

		[Constructable]
		public SkinnedGoatArtifactRep() : base( 0x1E88 )
		{
			Name = "Skinned Goat" + String.Format(" [Replica]");
		}

		public SkinnedGoatArtifactRep( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.WriteEncodedInt( 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadEncodedInt();
		}
	}

	public class SkullCandleArtifactRep : BaseArtifactReplica
	{
		public override int ArtifactRarity{ get{ return 1; } }

		[Constructable]
		public SkullCandleArtifactRep() : base( 0x1858 )
		{
			Name = "Skull With Candle" + String.Format(" [Replica]");
			Light = LightType.Circle150;
		}

		public SkullCandleArtifactRep( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.WriteEncodedInt( 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadEncodedInt();
		}
	}

	public class StretchedHideArtifactRep : BaseArtifactReplica
	{
		public override int ArtifactRarity{ get{ return 2; } }

		[Constructable]
		public StretchedHideArtifactRep() : base( 0x106B )
		{
			Name = "Stretched Hide" + String.Format(" [Replica]");
		}

		public StretchedHideArtifactRep( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.WriteEncodedInt( 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadEncodedInt();
		}
	}

	public class StuddedLeggingsArtifactRep : BaseArtifactReplica
	{
		public override int ArtifactRarity{ get{ return 5; } }

		[Constructable]
		public StuddedLeggingsArtifactRep() : base( 0x13D8 )
		{
			Name = "Studded Leggings" + String.Format(" [Replica]");
		}

		public StuddedLeggingsArtifactRep( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.WriteEncodedInt( 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadEncodedInt();
		}
	}

	public class StuddedTunicArtifactRep : BaseArtifactReplica
	{
		public override int ArtifactRarity{ get{ return 7; } }

		[Constructable]
		public StuddedTunicArtifactRep() : base( 0x13D9 )
		{
			Name = "Studded Tunic" + String.Format(" [Replica]");
		}

		public StuddedTunicArtifactRep( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.WriteEncodedInt( 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadEncodedInt();
		}
	}

	public class TarotCardsArtifactRep : BaseArtifactReplica
	{
		public override int ArtifactRarity{ get{ return 5; } }

		[Constructable]
		public TarotCardsArtifactRep() : base( 0x12A5 )
		{
			Name = "Tarot" + String.Format(" [Replica]");
		}

		public TarotCardsArtifactRep( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.WriteEncodedInt( 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadEncodedInt();
		}
	}

	public class ColorfulTapestryArtifactRep : BaseArtifactReplica
	{
		public override int ArtifactRarity{ get{ return 12; } }

		[Constructable]
		public ColorfulTapestryArtifactRep() : base( 0x42C4 )
		{
			Name = "Colorful Tapestry" + String.Format(" [Replica]");
		}

		public ColorfulTapestryArtifactRep( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.WriteEncodedInt( 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadEncodedInt();
		}
	}

	public class BlackBannerNorthArtifactRep : BaseArtifactReplica
	{
		public override int ArtifactRarity{ get{ return 12; } }

		[Constructable]
		public BlackBannerNorthArtifactRep() : base( 0x42CB )
		{
			Name = "Chaos Banner" + String.Format(" [Replica]");
		}

		public BlackBannerNorthArtifactRep( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.WriteEncodedInt( 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadEncodedInt();
		}
	}

	public class BlackBannerWestArtifactRep : BaseArtifactReplica
	{
		public override int ArtifactRarity{ get{ return 12; } }

		[Constructable]
		public BlackBannerWestArtifactRep() : base( 0x42CC )
		{
			Name = "Chaos Banner" + String.Format(" [Replica]");
		}

		public BlackBannerWestArtifactRep( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.WriteEncodedInt( 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadEncodedInt();
		}
	}

	public class SnakeBannerNorthArtifactRep : BaseArtifactReplica
	{
		public override int ArtifactRarity{ get{ return 12; } }

		[Constructable]
		public SnakeBannerNorthArtifactRep() : base( 0x42CD )
		{
			Name = "Order Banner" + String.Format(" [Replica]");
		}

		public SnakeBannerNorthArtifactRep( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.WriteEncodedInt( 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadEncodedInt();
		}
	}

	public class SnakeBannerWestArtifactRep : BaseArtifactReplica
	{
		public override int ArtifactRarity{ get{ return 12; } }

		[Constructable]
		public SnakeBannerWestArtifactRep() : base( 0x42CE )
		{
			Name = "Order Banner" + String.Format(" [Replica]");
		}

		public SnakeBannerWestArtifactRep( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.WriteEncodedInt( 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadEncodedInt();
		}
	}
	
	public class ZyronicClawRep : BaseArtifactReplica
	{
		public override int ArtifactRarity{ get{ return 10; } }
		
		[Constructable]
		public ZyronicClawRep() : base( 0xf45 )
		{
			Name = "Zyronic Claw" + String.Format(" [Replica]");
		}

		public ZyronicClawRep( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.WriteEncodedInt( (int) 0 );
		}
		
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );
			int version = reader.ReadEncodedInt();
		}
	}
	
	public class TitansHammerRep : BaseArtifactReplica
	{
		public override int ArtifactRarity{ get{ return 10; } }
		
		[Constructable]
		public TitansHammerRep() : base( 0x1439 )
		{
			Name = "Titan's Hammer" + String.Format(" [Replica]");
		}

		public TitansHammerRep( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.WriteEncodedInt( (int) 0 );
		}
		
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );
			int version = reader.ReadEncodedInt();
		}
	}
	
	public class BladeOfTheRighteousRep : BaseArtifactReplica
	{
		public override int ArtifactRarity{ get{ return 10; } }
		
		[Constructable]
		public BladeOfTheRighteousRep() : base( 0xF61 )
		{
			Name = "Blade Of The Righteous" + String.Format(" [Replica]");
		}

		public BladeOfTheRighteousRep( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.WriteEncodedInt( (int) 0 );
		}
		
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );
			int version = reader.ReadEncodedInt();
		}
	}
	
	public class InquisitorsResolutionRep : BaseArtifactReplica
	{
		public override int ArtifactRarity{ get{ return 10; } }
		
		[Constructable]
		public InquisitorsResolutionRep() : base( 0x1414 )
		{
			Name = "The Inquisitor's Resolution" + String.Format(" [Replica]");
		}

		public InquisitorsResolutionRep( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.WriteEncodedInt( (int) 0 );
		}
		
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );
			int version = reader.ReadEncodedInt();
		}
	}
	#endregion
	
	#region Tokuno/SE Stealable Artifact Replicas
	public class Basket1ArtifactRep : BaseArtifactReplicaContainer
	{
		public override int ArtifactRarity{ get{ return 1; } }
		
		[Constructable]
		public Basket1ArtifactRep() : base( 0x24DD )
		{
			Name = "Basket" + String.Format(" [Replica]");
		}

		public Basket1ArtifactRep( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.WriteEncodedInt( (int) 0 );
		}
		
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );
			int version = reader.ReadEncodedInt();
		}
	}
	
	public class Basket2ArtifactRep : BaseArtifactReplicaContainer
	{
		public override int ArtifactRarity{ get{ return 1; } }
		
		[Constructable]
		public Basket2ArtifactRep() : base( 0x24D7 )
		{
			Name = "Basket" + String.Format(" [Replica]");
		}

		public Basket2ArtifactRep( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.WriteEncodedInt( (int) 0 );
		}
		
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );
			int version = reader.ReadEncodedInt();
		}
	}
	
	public class Basket3WestArtifactRep : BaseArtifactReplicaContainer
	{
		public override int ArtifactRarity{ get{ return 1; } }
		
		[Constructable]
		public Basket3WestArtifactRep() : base( 0x24D9 )
		{
			Name = "Basket" + String.Format(" [Replica]");
		}

		public Basket3WestArtifactRep( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.WriteEncodedInt( (int) 0 );
		}
		
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );
			int version = reader.ReadEncodedInt();
		}
	}
	
	public class Basket3NorthArtifactRep : BaseArtifactReplicaContainer
	{
		public override int ArtifactRarity{ get{ return 1; } }
		
		[Constructable]
		public Basket3NorthArtifactRep() : base( 0x24DA )
		{
			Name = "Basket" + String.Format(" [Replica]");
		}

		public Basket3NorthArtifactRep( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.WriteEncodedInt( (int) 0 );
		}
		
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );
			int version = reader.ReadEncodedInt();
		}
	}
	
	public class Basket4ArtifactRep : BaseArtifactReplicaContainer
	{
		public override int ArtifactRarity{ get{ return 2; } }
		
		[Constructable]
		public Basket4ArtifactRep() : base( 0x24D8 )
		{
			Name = "Basket" + String.Format(" [Replica]");
		}

		public Basket4ArtifactRep( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.WriteEncodedInt( (int) 0 );
		}
		
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );
			int version = reader.ReadEncodedInt();
		}
	}
	
	public class Basket5NorthArtifactRep : BaseArtifactReplicaContainer
	{
		public override int ArtifactRarity{ get{ return 2; } }
		
		[Constructable]
		public Basket5NorthArtifactRep() : base( 0x24DB )
		{
			Name = "Basket" + String.Format(" [Replica]");
		}

		public Basket5NorthArtifactRep( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.WriteEncodedInt( (int) 0 );
		}
		
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );
			int version = reader.ReadEncodedInt();
		}
	}
	
	public class Basket5WestArtifactRep : BaseArtifactReplicaContainer
	{
		public override int ArtifactRarity{ get{ return 2; } }
		
		[Constructable]
		public Basket5WestArtifactRep() : base( 0x24DC )
		{
			Name = "Basket" + String.Format(" [Replica]");
		}

		public Basket5WestArtifactRep( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.WriteEncodedInt( (int) 0 );
		}
		
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );
			int version = reader.ReadEncodedInt();
		}
	}
	
	public class Basket6ArtifactRep : BaseArtifactReplicaContainer
	{
		public override int ArtifactRarity{ get{ return 2; } }
		
		[Constructable]
		public Basket6ArtifactRep() : base( 0x24D5 )
		{
			Name = "Basket" + String.Format(" [Replica]");
		}

		public Basket6ArtifactRep( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.WriteEncodedInt( (int) 0 );
		}
		
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );
			int version = reader.ReadEncodedInt();
		}
	}
	
	public class BowlArtifactRep : BaseArtifactReplica
	{
		public override int ArtifactRarity{ get{ return 4; } }
		
		[Constructable]
		public BowlArtifactRep() : base( 0x24DE )
		{
			Name = "Bowl" + String.Format(" [Replica]");
		}

		public BowlArtifactRep( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.WriteEncodedInt( (int) 0 );
		}
		
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );
			int version = reader.ReadEncodedInt();
		}
	}
	
	public class BowlsVerticalArtifactRep : BaseArtifactReplica
	{
		public override int ArtifactRarity{ get{ return 4; } }
		
		[Constructable]
		public BowlsVerticalArtifactRep() : base( 0x24DF )
		{
			Name = "Bowls" + String.Format(" [Replica]");
		}

		public BowlsVerticalArtifactRep( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.WriteEncodedInt( (int) 0 );
		}
		
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );
			int version = reader.ReadEncodedInt();
		}
	}
	
	public class BowlsHorizontalArtifactRep : BaseArtifactReplica
	{
		public override int ArtifactRarity{ get{ return 4; } }
		
		[Constructable]
		public BowlsHorizontalArtifactRep() : base( 0x24E0 )
		{
			Name = "Bowls" + String.Format(" [Replica]");
		}

		public BowlsHorizontalArtifactRep( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.WriteEncodedInt( (int) 0 );
		}
		
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );
			int version = reader.ReadEncodedInt();
		}
	}
	
	public class CupsArtifactRep : BaseArtifactReplica
	{
		public override int ArtifactRarity{ get{ return 4; } }
		
		[Constructable]
		public CupsArtifactRep() : base( 0x24E1 )
		{
			Name = "Cups" + String.Format(" [Replica]");
		}

		public CupsArtifactRep( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.WriteEncodedInt( (int) 0 );
		}
		
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );
			int version = reader.ReadEncodedInt();
		}
	}
	
	public class FanWestArtifactRep : BaseArtifactReplica
	{
		public override int ArtifactRarity{ get{ return 4; } }
		
		[Constructable]
		public FanWestArtifactRep() : base( 0x240A )
		{
			Name = "Fan" + String.Format(" [Replica]");
		}

		public FanWestArtifactRep( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.WriteEncodedInt( (int) 0 );
		}
		
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );
			int version = reader.ReadEncodedInt();
		}
	}
	
	public class FanNorthArtifactRep : BaseArtifactReplica
	{
		public override int ArtifactRarity{ get{ return 4; } }
		
		[Constructable]
		public FanNorthArtifactRep() : base( 0x2409 )
		{
			Name = "Fan" + String.Format(" [Replica]");
		}

		public FanNorthArtifactRep( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.WriteEncodedInt( (int) 0 );
		}
		
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );
			int version = reader.ReadEncodedInt();
		}
	}
	
	public class TripleFanWestArtifactRep : BaseArtifactReplica
	{
		public override int ArtifactRarity{ get{ return 4; } }
		
		[Constructable]
		public TripleFanWestArtifactRep() : base( 0x240C )
		{
			Name = "Fan" + String.Format(" [Replica]");
		}

		public TripleFanWestArtifactRep( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.WriteEncodedInt( (int) 0 );
		}
		
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );
			int version = reader.ReadEncodedInt();
		}
	}
	
	public class TripleFanNorthArtifactRep : BaseArtifactReplica
	{
		public override int ArtifactRarity{ get{ return 4; } }
		
		[Constructable]
		public TripleFanNorthArtifactRep() : base( 0x240B )
		{
			Name = "Fan" + String.Format(" [Replica]");
		}

		public TripleFanNorthArtifactRep( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.WriteEncodedInt( (int) 0 );
		}
		
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );
			int version = reader.ReadEncodedInt();
		}
	}
	
	public class FlowersArtifactRep : BaseArtifactReplica
	{
		public override int ArtifactRarity{ get{ return 7; } }
		
		[Constructable]
		public FlowersArtifactRep() : base( 0x284A )
		{
			Name = "Flowers" + String.Format(" [Replica]");
		}

		public FlowersArtifactRep( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.WriteEncodedInt( (int) 0 );
		}
		
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );
			int version = reader.ReadEncodedInt();
		}
	}
	
	public class Painting1WestArtifactRep : BaseArtifactReplica
	{
		public override int ArtifactRarity{ get{ return 4; } }
		
		[Constructable]
		public Painting1WestArtifactRep() : base( 0x240E )
		{
			Name = "Painting" + String.Format(" [Replica]");
		}

		public Painting1WestArtifactRep( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.WriteEncodedInt( (int) 0 );
		}
		
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );
			int version = reader.ReadEncodedInt();
		}
	}
	
	public class Painting1NorthArtifactRep : BaseArtifactReplica
	{
		public override int ArtifactRarity{ get{ return 4; } }
		
		[Constructable]
		public Painting1NorthArtifactRep() : base( 0x240D )
		{
			Name = "Painting" + String.Format(" [Replica]");
		}

		public Painting1NorthArtifactRep( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.WriteEncodedInt( (int) 0 );
		}
		
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );
			int version = reader.ReadEncodedInt();
		}
	}
	
	public class Painting2WestArtifactRep : BaseArtifactReplica
	{
		public override int ArtifactRarity{ get{ return 4; } }
		
		[Constructable]
		public Painting2WestArtifactRep() : base( 0x2410 )
		{
			Name = "Painting" + String.Format(" [Replica]");
		}

		public Painting2WestArtifactRep( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.WriteEncodedInt( (int) 0 );
		}
		
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );
			int version = reader.ReadEncodedInt();
		}
	}
	
	public class Painting2NorthArtifactRep : BaseArtifactReplica
	{
		public override int ArtifactRarity{ get{ return 4; } }
		
		[Constructable]
		public Painting2NorthArtifactRep() : base( 0x240F )
		{
			Name = "Painting" + String.Format(" [Replica]");
		}

		public Painting2NorthArtifactRep( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.WriteEncodedInt( (int) 0 );
		}
		
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );
			int version = reader.ReadEncodedInt();
		}
	}
	
	public class Painting3ArtifactRep : BaseArtifactReplica
	{
		public override int ArtifactRarity{ get{ return 5; } }
		
		[Constructable]
		public Painting3ArtifactRep() : base( 0x2411 )
		{
			Name = "Painting" + String.Format(" [Replica]");
		}

		public Painting3ArtifactRep( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.WriteEncodedInt( (int) 0 );
		}
		
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );
			int version = reader.ReadEncodedInt();
		}
	}
	
	public class Painting4WestArtifactRep : BaseArtifactReplica
	{
		public override int ArtifactRarity{ get{ return 6; } }
		
		[Constructable]
		public Painting4WestArtifactRep() : base( 0x2412 )
		{
			Name = "Painting" + String.Format(" [Replica]");
		}

		public Painting4WestArtifactRep( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.WriteEncodedInt( (int) 0 );
		}
		
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );
			int version = reader.ReadEncodedInt();
		}
	}
	
	public class Painting4NorthArtifactRep : BaseArtifactReplica
	{
		public override int ArtifactRarity{ get{ return 6; } }
		
		[Constructable]
		public Painting4NorthArtifactRep() : base( 0x2411 )
		{
			Name = "Painting" + String.Format(" [Replica]");
		}

		public Painting4NorthArtifactRep( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.WriteEncodedInt( (int) 0 );
		}
		
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );
			int version = reader.ReadEncodedInt();
		}
	}
	
	public class Painting5WestArtifactRep : BaseArtifactReplica
	{
		public override int ArtifactRarity{ get{ return 8; } }
		
		[Constructable]
		public Painting5WestArtifactRep() : base( 0x2416 )
		{
			Name = "Painting" + String.Format(" [Replica]");
		}

		public Painting5WestArtifactRep( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.WriteEncodedInt( (int) 0 );
		}
		
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );
			int version = reader.ReadEncodedInt();
		}
	}
	
	public class Painting5NorthArtifactRep : BaseArtifactReplica
	{
		public override int ArtifactRarity{ get{ return 8; } }
		
		[Constructable]
		public Painting5NorthArtifactRep() : base( 0x2415 )
		{
			Name = "Painting" + String.Format(" [Replica]");
		}

		public Painting5NorthArtifactRep( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.WriteEncodedInt( (int) 0 );
		}
		
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );
			int version = reader.ReadEncodedInt();
		}
	}
	
	public class Painting6WestArtifactRep : BaseArtifactReplica
	{
		public override int ArtifactRarity{ get{ return 9; } }
		
		[Constructable]
		public Painting6WestArtifactRep() : base( 0x2418 )
		{
			Name = "Painting" + String.Format(" [Replica]");
		}

		public Painting6WestArtifactRep( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.WriteEncodedInt( (int) 0 );
		}
		
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );
			int version = reader.ReadEncodedInt();
		}
	}
	
	public class Painting6NorthArtifactRep : BaseArtifactReplica
	{
		public override int ArtifactRarity{ get{ return 9; } }
		
		[Constructable]
		public Painting6NorthArtifactRep() : base( 0x2417 )
		{
			Name = "Painting" + String.Format(" [Replica]");
		}

		public Painting6NorthArtifactRep( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.WriteEncodedInt( (int) 0 );
		}
		
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );
			int version = reader.ReadEncodedInt();
		}
	}
	
	public class SakeArtifactRep : BaseArtifactReplica
	{
		public override int ArtifactRarity{ get{ return 4; } }
		
		[Constructable]
		public SakeArtifactRep() : base( 0x24E2 )
		{
			Name = "Sake" + String.Format(" [Replica]");
		}

		public SakeArtifactRep( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.WriteEncodedInt( (int) 0 );
		}
		
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );
			int version = reader.ReadEncodedInt();
		}
	}
	
	public class Sculpture1ArtifactRep : BaseArtifactReplica
	{
		public override int ArtifactRarity{ get{ return 3; } }
		
		[Constructable]
		public Sculpture1ArtifactRep() : base( 0x2419 )
		{
			Name = "Sculpture" + String.Format(" [Replica]");
		}

		public Sculpture1ArtifactRep( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.WriteEncodedInt( (int) 0 );
		}
		
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );
			int version = reader.ReadEncodedInt();
		}
	}
	
	public class Sculpture2ArtifactRep : BaseArtifactReplica
	{
		public override int ArtifactRarity{ get{ return 3; } }
		
		[Constructable]
		public Sculpture2ArtifactRep() : base( 0x241B )
		{
			Name = "Sculpture" + String.Format(" [Replica]");
		}

		public Sculpture2ArtifactRep( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.WriteEncodedInt( (int) 0 );
		}
		
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );
			int version = reader.ReadEncodedInt();
		}
	}
	
	public class DolphinLeftArtifactRep : BaseArtifactReplica
	{
		public override int ArtifactRarity{ get{ return 8; } }
		
		[Constructable]
		public DolphinLeftArtifactRep() : base( 0x2846 )
		{
			Name = "Sculpture" + String.Format(" [Replica]");
		}

		public DolphinLeftArtifactRep( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.WriteEncodedInt( (int) 0 );
		}
		
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );
			int version = reader.ReadEncodedInt();
		}
	}
	
	public class DolphinRightArtifactRep : BaseArtifactReplica
	{
		public override int ArtifactRarity{ get{ return 8; } }
		
		[Constructable]
		public DolphinRightArtifactRep() : base( 0x2847 )
		{
			Name = "Sculpture" + String.Format(" [Replica]");
		}

		public DolphinRightArtifactRep( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.WriteEncodedInt( (int) 0 );
		}
		
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );
			int version = reader.ReadEncodedInt();
		}
	}
	
	public class ManStatuetteSouthArtifactRep : BaseArtifactReplica
	{
		public override int ArtifactRarity{ get{ return 9; } }
		
		[Constructable]
		public ManStatuetteSouthArtifactRep() : base( 0x2848 )
		{
			Name = "Sculpture" + String.Format(" [Replica]");
		}

		public ManStatuetteSouthArtifactRep( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.WriteEncodedInt( (int) 0 );
		}
		
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );
			int version = reader.ReadEncodedInt();
		}
	}
	
	public class ManStatuetteEastArtifactRep : BaseArtifactReplica
	{
		public override int ArtifactRarity{ get{ return 9; } }
		
		[Constructable]
		public ManStatuetteEastArtifactRep() : base( 0x2849 )
		{
			Name = "Sculpture" + String.Format(" [Replica]");
		}

		public ManStatuetteEastArtifactRep( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.WriteEncodedInt( (int) 0 );
		}
		
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );
			int version = reader.ReadEncodedInt();
		}
	}
	
	public class SwordDisplay1WestArtifactRep : BaseArtifactReplica
	{
		public override int ArtifactRarity{ get{ return 5; } }
		
		[Constructable]
		public SwordDisplay1WestArtifactRep() : base( 0x2842 )
		{
			Name = "Sword Display" + String.Format(" [Replica]");
		}

		public SwordDisplay1WestArtifactRep( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.WriteEncodedInt( (int) 0 );
		}
		
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );
			int version = reader.ReadEncodedInt();
		}
	}
	
	public class SwordDisplay1NorthArtifactRep : BaseArtifactReplica
	{
		public override int ArtifactRarity{ get{ return 5; } }
		
		[Constructable]
		public SwordDisplay1NorthArtifactRep() : base( 0x2843 )
		{
			Name = "Sword Display" + String.Format(" [Replica]");
		}

		public SwordDisplay1NorthArtifactRep( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.WriteEncodedInt( (int) 0 );
		}
		
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );
			int version = reader.ReadEncodedInt();
		}
	}
	
	public class SwordDisplay2WestArtifactRep : BaseArtifactReplica
	{
		public override int ArtifactRarity{ get{ return 6; } }
		
		[Constructable]
		public SwordDisplay2WestArtifactRep() : base( 0x2844 )
		{
			Name = "Sword Display" + String.Format(" [Replica]");
		}

		public SwordDisplay2WestArtifactRep( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.WriteEncodedInt( (int) 0 );
		}
		
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );
			int version = reader.ReadEncodedInt();
		}
	}
	
	public class SwordDisplay2NorthArtifactRep : BaseArtifactReplica
	{
		public override int ArtifactRarity{ get{ return 6; } }
		
		[Constructable]
		public SwordDisplay2NorthArtifactRep() : base( 0x2845 )
		{
			Name = "Sword Display" + String.Format(" [Replica]");
		}

		public SwordDisplay2NorthArtifactRep( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.WriteEncodedInt( (int) 0 );
		}
		
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );
			int version = reader.ReadEncodedInt();
		}
	}
	
	public class SwordDisplay3SouthArtifactRep : BaseArtifactReplica
	{
		public override int ArtifactRarity{ get{ return 8; } }
		
		[Constructable]
		public SwordDisplay3SouthArtifactRep() : base( 0x2855 )
		{
			Name = "Sword Display" + String.Format(" [Replica]");
		}

		public SwordDisplay3SouthArtifactRep( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.WriteEncodedInt( (int) 0 );
		}
		
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );
			int version = reader.ReadEncodedInt();
		}
	}
	
	public class SwordDisplay3EastArtifactRep : BaseArtifactReplica
	{
		public override int ArtifactRarity{ get{ return 8; } }
		
		[Constructable]
		public SwordDisplay3EastArtifactRep() : base( 0x2856 )
		{
			Name = "Sword Display" + String.Format(" [Replica]");
		}

		public SwordDisplay3EastArtifactRep( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.WriteEncodedInt( (int) 0 );
		}
		
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );
			int version = reader.ReadEncodedInt();
		}
	}
	
	public class SwordDisplay4WestArtifactRep : BaseArtifactReplica
	{
		public override int ArtifactRarity{ get{ return 8; } }
		
		[Constructable]
		public SwordDisplay4WestArtifactRep() : base( 0x2853 )
		{
			Name = "Sword Display" + String.Format(" [Replica]");
		}

		public SwordDisplay4WestArtifactRep( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.WriteEncodedInt( (int) 0 );
		}
		
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );
			int version = reader.ReadEncodedInt();
		}
	}
	
	public class SwordDisplay4NorthArtifactRep : BaseArtifactReplica
	{
		public override int ArtifactRarity{ get{ return 9; } }
		
		[Constructable]
		public SwordDisplay4NorthArtifactRep() : base( 0x2854 )
		{
			Name = "Sword Display" + String.Format(" [Replica]");
		}

		public SwordDisplay4NorthArtifactRep( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.WriteEncodedInt( (int) 0 );
		}
		
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );
			int version = reader.ReadEncodedInt();
		}
	}
	
	public class SwordDisplay5WestArtifactRep : BaseArtifactReplica
	{
		public override int ArtifactRarity{ get{ return 9; } }
		
		[Constructable]
		public SwordDisplay5WestArtifactRep() : base( 0x2851 )
		{
			Name = "Sword Display" + String.Format(" [Replica]");
		}

		public SwordDisplay5WestArtifactRep( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.WriteEncodedInt( (int) 0 );
		}
		
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );
			int version = reader.ReadEncodedInt();
		}
	}
	
	public class SwordDisplay5NorthArtifactRep : BaseArtifactReplica
	{
		public override int ArtifactRarity{ get{ return 9; } }
		
		[Constructable]
		public SwordDisplay5NorthArtifactRep() : base( 0x2852 )
		{
			Name = "Sword Display" + String.Format(" [Replica]");
		}

		public SwordDisplay5NorthArtifactRep( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.WriteEncodedInt( (int) 0 );
		}
		
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );
			int version = reader.ReadEncodedInt();
		}
	}
	
	public class TeapotWestArtifactRep : BaseArtifactReplica
	{
		public override int ArtifactRarity{ get{ return 3; } }
		
		[Constructable]
		public TeapotWestArtifactRep() : base( 0x24E7 )
		{
			Name = "Teapot" + String.Format(" [Replica]");
		}

		public TeapotWestArtifactRep( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.WriteEncodedInt( (int) 0 );
		}
		
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );
			int version = reader.ReadEncodedInt();
		}
	}
	
	public class TeapotNorthArtifactRep : BaseArtifactReplica
	{
		public override int ArtifactRarity{ get{ return 3; } }
		
		[Constructable]
		public TeapotNorthArtifactRep() : base( 0x24E6 )
		{
			Name = "Teapot" + String.Format(" [Replica]");
		}

		public TeapotNorthArtifactRep( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.WriteEncodedInt( (int) 0 );
		}
		
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );
			int version = reader.ReadEncodedInt();
		}
	}
	
	public class TowerLanternArtifactRep : BaseArtifactReplica
	{
		public override int ArtifactRarity{ get{ return 3; } }
		
		[CommandProperty( AccessLevel.GameMaster )]
		public bool IsOn
		{
			get{ return this.ItemID == 0x24BF; }
			set{ this.ItemID = value ? 0x24BF : 0x24C0; }
		}
		
		[Constructable]
		public TowerLanternArtifactRep() : base( 0x24C0 )
		{
			Name = "Tower Lantern" + String.Format(" [Replica]");
			this.Light = LightType.Circle225;
		}
		
		public override void OnDoubleClick( Mobile from )
		{
			if ( from.InRange( this.GetWorldLocation(), 2 ) )
			{
				if ( this.IsOn )
				{
					this.IsOn = false;
					from.PlaySound( 0x3BE );
				}
				else
				{
					this.IsOn = true;
					from.PlaySound( 0x47 );
				}
			}
			else
			{
				from.LocalOverheadMessage( Network.MessageType.Regular, 0x3B2, 1019045 ); // I can't reach that.
			}
		}

		public TowerLanternArtifactRep( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.WriteEncodedInt( (int) 1 );
		}
		
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );
			int version = reader.ReadEncodedInt();
			if ( version == 0 )
				this.Light = LightType.Circle225;
		}
	}
	
	public class Urn1ArtifactRep : BaseArtifactReplica
	{
		public override int ArtifactRarity{ get{ return 3; } }
		
		[Constructable]
		public Urn1ArtifactRep() : base( 0x241D )
		{
			Name = "Urn" + String.Format(" [Replica]");
		}

		public Urn1ArtifactRep( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.WriteEncodedInt( (int) 0 );
		}
		
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );
			int version = reader.ReadEncodedInt();
		}
	}
	
	public class Urn2ArtifactRep : BaseArtifactReplica
	{
		public override int ArtifactRarity{ get{ return 3; } }
		
		[Constructable]
		public Urn2ArtifactRep() : base( 0x241E )
		{
			Name = "Urn" + String.Format(" [Replica]");
		}

		public Urn2ArtifactRep( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.WriteEncodedInt( (int) 0 );
		}
		
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );
			int version = reader.ReadEncodedInt();
		}
	}
	
	public class ZenRock1ArtifactRep : BaseArtifactReplica
	{
		public override int ArtifactRarity{ get{ return 2; } }
		
		[Constructable]
		public ZenRock1ArtifactRep() : base( 0x24E4 )
		{
			Name = "Zen Rock Garden" + String.Format(" [Replica]");
		}

		public ZenRock1ArtifactRep( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.WriteEncodedInt( (int) 0 );
		}
		
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );
			int version = reader.ReadEncodedInt();
		}
	}
	
	public class ZenRock2ArtifactRep : BaseArtifactReplica
	{
		public override int ArtifactRarity{ get{ return 3; } }
		
		[Constructable]
		public ZenRock2ArtifactRep() : base( 0x24E3 )
		{
			Name = "Zen Rock Garden" + String.Format(" [Replica]");
		}

		public ZenRock2ArtifactRep( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.WriteEncodedInt( (int) 0 );
		}
		
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );
			int version = reader.ReadEncodedInt();
		}
	}
	
	public class ZenRock3ArtifactRep : BaseArtifactReplica
	{
		public override int ArtifactRarity{ get{ return 3; } }
		
		[Constructable]
		public ZenRock3ArtifactRep() : base( 0x24E5 )
		{
			Name = "Zen Rock Garden" + String.Format(" [Replica]");
		}

		public ZenRock3ArtifactRep( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.WriteEncodedInt( (int) 0 );
		}
		
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );
			int version = reader.ReadEncodedInt();
		}
	}
	#endregion
	
	#region Ter Mur/SA Stealable Artifact Replicas
	//To Do
	#endregion
}