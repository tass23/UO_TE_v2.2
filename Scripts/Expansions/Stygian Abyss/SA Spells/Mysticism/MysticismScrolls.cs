using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class NetherBoltScroll : SpellScroll
	{
		[Constructable]
		public NetherBoltScroll()
			: this( 1 )
		{
		}

		[Constructable]
		public NetherBoltScroll( int amount )
			: base( 677, 0x2D9E, amount )
		{
		}

		public NetherBoltScroll( Serial serial )
			: base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int)0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}

	public class HealingStoneScroll : SpellScroll
	{
		[Constructable]
		public HealingStoneScroll()
			: this( 1 )
		{
		}

		[Constructable]
		public HealingStoneScroll( int amount )
			: base( 678, 0x2D9F, amount )
		{
		}

		public HealingStoneScroll( Serial serial )
			: base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int)0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}

	public class PurgeMagicScroll : SpellScroll
	{
		[Constructable]
		public PurgeMagicScroll()
			: this( 1 )
		{
		}

		[Constructable]
		public PurgeMagicScroll( int amount )
			: base( 679, 0x2DA0, amount )
		{
		}

		public PurgeMagicScroll( Serial serial )
			: base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int)0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}

	public class EagleStrikeScroll : SpellScroll
	{
		[Constructable]
		public EagleStrikeScroll()
			: this( 1 )
		{
		}

		[Constructable]
		public EagleStrikeScroll( int amount )
			: base( 682, 0x2DA3, amount )
		{
		}

		public EagleStrikeScroll( Serial serial )
			: base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int)0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}

	public class AnimatedWeaponScroll : SpellScroll
	{
		[Constructable]
		public AnimatedWeaponScroll()
			: this( 1 )
		{
		}

		[Constructable]
		public AnimatedWeaponScroll( int amount )
			: base( 683, 0x2DA4, amount )
		{
		}

		public AnimatedWeaponScroll( Serial serial )
			: base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int)0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}

	public class StoneFormScroll : SpellScroll
	{
		[Constructable]
		public StoneFormScroll()
			: this( 1 )
		{
		}

		[Constructable]
		public StoneFormScroll( int amount )
			: base( 684, 0x2DA5, amount )
		{
		}

		public StoneFormScroll( Serial serial )
			: base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int)0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}

	public class SpellTriggerScroll : SpellScroll
	{
		[Constructable]
		public SpellTriggerScroll()
			: this( 1 )
		{
		}

		[Constructable]
		public SpellTriggerScroll( int amount )
			: base( 685, 0x2DA6, amount )
		{
		}

		public SpellTriggerScroll( Serial serial )
			: base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int)0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}

	public class CleansingWindsScroll : SpellScroll
	{
		[Constructable]
		public CleansingWindsScroll()
			: this( 1 )
		{
		}

		[Constructable]
		public CleansingWindsScroll( int amount )
			: base( 687, 0x2DA8, amount )
		{
		}

		public CleansingWindsScroll( Serial serial )
			: base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int)0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}

	public class BombardScroll : SpellScroll
	{
		[Constructable]
		public BombardScroll()
			: this( 1 )
		{
		}

		[Constructable]
		public BombardScroll( int amount )
			: base( 688, 0x2DA9, amount )
		{
		}

		public BombardScroll( Serial serial )
			: base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int)0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}

	public class SpellPlagueScroll : SpellScroll
	{
		[Constructable]
		public SpellPlagueScroll()
			: this( 1 )
		{
		}

		[Constructable]
		public SpellPlagueScroll( int amount )
			: base( 689, 0x2DAA, amount )
		{
		}

		public SpellPlagueScroll( Serial serial )
			: base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int)0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}

	public class HailStormScroll : SpellScroll
	{
		[Constructable]
		public HailStormScroll()
			: this( 1 )
		{
		}

		[Constructable]
		public HailStormScroll( int amount )
			: base( 690, 0x2DAB, amount )
		{
		}

		public HailStormScroll( Serial serial )
			: base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int)0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}

	public class NetherCycloneScroll : SpellScroll
	{
		[Constructable]
		public NetherCycloneScroll()
			: this( 1 )
		{
		}

		[Constructable]
		public NetherCycloneScroll( int amount )
			: base( 691, 0x2DAC, amount )
		{
		}

		public NetherCycloneScroll( Serial serial )
			: base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int)0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}

	public class RisingColossusScroll : SpellScroll
	{
		[Constructable]
		public RisingColossusScroll()
			: this( 1 )
		{
		}

		[Constructable]
		public RisingColossusScroll( int amount )
			: base( 692, 0x2DAD, amount )
		{
		}

		public RisingColossusScroll( Serial serial )
			: base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int)0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}
	
	public class SleepScroll : SpellScroll
	{
		[Constructable]
		public SleepScroll()
			: this( 1 )
		{
		}

		[Constructable]
		public SleepScroll( int amount )
			: base( 681, 0x2DA2, amount )
		{
		}

		public SleepScroll( Serial serial )
			: base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int)0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}
	
	public class MassSleepScroll : SpellScroll
	{
		[Constructable]
		public MassSleepScroll()
			: this( 1 )
		{
		}

		[Constructable]
		public MassSleepScroll( int amount )
			: base( 686, 0x2DA7, amount )
		{
		}

		public MassSleepScroll( Serial serial )
			: base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int)0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}
	
	public class EnchantScroll : SpellScroll
	{
		[Constructable]
		public EnchantScroll()
			: this( 1 )
		{
		}

		[Constructable]
		public EnchantScroll( int amount )
			: base( 680, 0x2DA1, amount )
		{
		}

		public EnchantScroll( Serial serial )
			: base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int)0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}
}