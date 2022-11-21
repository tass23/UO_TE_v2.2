using System;
using Server;
using Server.Items;
using Server.Mobiles;
using Server.Spells;

namespace Server.Items
{
	public interface IVampireSlayer
	{
	}
	
	[FlipableAttribute( 0xF61, 0xF60 )]
	public class SilverLongsword : Longsword, IVampireSlayer
	{
		[Constructable]
		public SilverLongsword()
		{
			Name = "a silver longsword";
			Slayer = SlayerName.Silver;
		}
		
		public override void OnHit(Mobile attacker, Mobile defender, double damageBonus)
        {
			if (defender is PlayerMobile)
			{
				PlayerMobile pm = (PlayerMobile) defender;
				if (pm.Vampire > 0)
				{
					int damage = Utility.Random( 10, 40 );
					attacker.DoHarmful(defender);
					SpellHelper.Damage(TimeSpan.Zero, defender, attacker, damage, 0, 0, 0, 0, 100);
					defender.PlaySound( 0x51D );
					defender.FixedParticles( 0x37C4, 1, 25, 9922, 14, 3, EffectLayer.Head );
					if (defender.Hits < 0) defender.Kill();
				}
			}
			base.OnHit(attacker, defender, 1.0);
		}

		public SilverLongsword ( Serial serial ) : base( serial )
		{
		}
		
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();

			if ( Slayer == SlayerName.None )
				Slayer = SlayerName.Silver;
		}
	}

	[FlipableAttribute( 0xF5E, 0xF5F )]
	public class SilverBroadsword : Broadsword, IVampireSlayer
	{
		[Constructable]
		public SilverBroadsword()
		{
			Name = "a silver broadsword";
			Slayer = SlayerName.Silver;
		}

		public override void OnHit(Mobile attacker, Mobile defender, double damageBonus)
        {
			if (defender is PlayerMobile)
			{
				PlayerMobile pm = (PlayerMobile) defender;
				if (pm.Vampire > 0)
				{
					int damage = Utility.Random( 10, 40 );
					attacker.DoHarmful(defender);
					SpellHelper.Damage(TimeSpan.Zero, defender, attacker, damage, 0, 0, 0, 0, 100);
					defender.PlaySound( 0x51D );
					defender.FixedParticles( 0x37C4, 1, 25, 9922, 14, 3, EffectLayer.Head );
					if (defender.Hits < 0) defender.Kill();
				}
			}
			base.OnHit(attacker, defender, 1.0);
		}
		
		public SilverBroadsword ( Serial serial ) : base( serial )
		{
		}
		
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();

			if ( Slayer == SlayerName.None )
				Slayer = SlayerName.Silver;
		}
	}

	[FlipableAttribute( 0x1401, 0x1400 )]
	public class SilverKryss : Kryss, IVampireSlayer
	{
		[Constructable]
		public SilverKryss()
		{
			Name = "a silver kryss";
			Slayer = SlayerName.Silver;
		}

		public override void OnHit(Mobile attacker, Mobile defender, double damageBonus)
        {
			if (defender is PlayerMobile)
			{
				PlayerMobile pm = (PlayerMobile) defender;
				if (pm.Vampire > 0)
				{
					int damage = Utility.Random( 10, 40 );
					attacker.DoHarmful(defender);
					SpellHelper.Damage(TimeSpan.Zero, defender, attacker, damage, 0, 0, 0, 0, 100);
					defender.PlaySound( 0x51D );
					defender.FixedParticles( 0x37C4, 1, 25, 9922, 14, 3, EffectLayer.Head );
					if (defender.Hits < 0) defender.Kill();
				}
			}
			base.OnHit(attacker, defender, 1.0);
		}
		
		public SilverKryss ( Serial serial ) : base( serial )
		{
		}
		
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();

			if ( Slayer == SlayerName.None )
				Slayer = SlayerName.Silver;
		}
	}

	[FlipableAttribute( 0x1441, 0x1440 )]
	public class SilverCutlass : Cutlass, IVampireSlayer
	{
		[Constructable]
		public SilverCutlass()
		{
			Name = "a silver cutlass";
			Slayer = SlayerName.Silver;
		}

		public override void OnHit(Mobile attacker, Mobile defender, double damageBonus)
        {
			if (defender is PlayerMobile)
			{
				PlayerMobile pm = (PlayerMobile) defender;
				if (pm.Vampire > 0)
				{
					int damage = Utility.Random( 10, 40 );
					attacker.DoHarmful(defender);
					SpellHelper.Damage(TimeSpan.Zero, defender, attacker, damage, 0, 0, 0, 0, 100);
					defender.PlaySound( 0x51D );
					defender.FixedParticles( 0x37C4, 1, 25, 9922, 14, 3, EffectLayer.Head );
					if (defender.Hits < 0) defender.Kill();
				}
			}
			base.OnHit(attacker, defender, 1.0);
		}
		
		public SilverCutlass ( Serial serial ) : base( serial )
		{
		}
		
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();

			if ( Slayer == SlayerName.None )
				Slayer = SlayerName.Silver;
		}
	}

	[FlipableAttribute( 0xf4b, 0xf4c )]
	public class SilverDoubleAxe : DoubleAxe, IVampireSlayer
	{
		[Constructable]
		public SilverDoubleAxe()
		{
			Name = "a silver double axe";
			Slayer = SlayerName.Silver;
		}

		public override void OnHit(Mobile attacker, Mobile defender, double damageBonus)
        {
			if (defender is PlayerMobile)
			{
				PlayerMobile pm = (PlayerMobile) defender;
				if (pm.Vampire > 0)
				{
					int damage = Utility.Random( 10, 40 );
					attacker.DoHarmful(defender);
					SpellHelper.Damage(TimeSpan.Zero, defender, attacker, damage, 0, 0, 0, 0, 100);
					defender.PlaySound( 0x51D );
					defender.FixedParticles( 0x37C4, 1, 25, 9922, 14, 3, EffectLayer.Head );
					if (defender.Hits < 0) defender.Kill();
				}
			}
			base.OnHit(attacker, defender, 1.0);
		}
		
		public SilverDoubleAxe ( Serial serial ) : base( serial )
		{
		}
		
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();

			if ( Slayer == SlayerName.None )
				Slayer = SlayerName.Silver;
		}
	}

	[FlipableAttribute( 0xF62, 0xF63 )]
	public class SilverSpear : Spear, IVampireSlayer
	{
		[Constructable]
		public SilverSpear()
		{
			Name = "a silver spear";
			Slayer = SlayerName.Silver;
		}

		public override void OnHit(Mobile attacker, Mobile defender, double damageBonus)
        {
			if (defender is PlayerMobile)
			{
				PlayerMobile pm = (PlayerMobile) defender;
				if (pm.Vampire > 0)
				{
					int damage = Utility.Random( 10, 40 );
					attacker.DoHarmful(defender);
					SpellHelper.Damage(TimeSpan.Zero, defender, attacker, damage, 0, 0, 0, 0, 100);
					defender.PlaySound( 0x51D );
					defender.FixedParticles( 0x37C4, 1, 25, 9922, 14, 3, EffectLayer.Head );
					if (defender.Hits < 0) defender.Kill();
				}
			}
			base.OnHit(attacker, defender, 1.0);
		}
		
		public SilverSpear ( Serial serial ) : base( serial )
		{
		}
		
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();

			if ( Slayer == SlayerName.None )
				Slayer = SlayerName.Silver;
		}
	}

	[FlipableAttribute( 0x26BF, 0x26C9 )]
	public class SilverHalberd : Halberd, IVampireSlayer
	{
		[Constructable]
		public SilverHalberd()
		{
			Name = "a silver halberd";
			Slayer = SlayerName.Silver;
		}
		
		public override void OnHit(Mobile attacker, Mobile defender, double damageBonus)
        {
			if (defender is PlayerMobile)
			{
				PlayerMobile pm = (PlayerMobile) defender;
				if (pm.Vampire > 0)
				{
					int damage = Utility.Random( 10, 40 );
					attacker.DoHarmful(defender);
					SpellHelper.Damage(TimeSpan.Zero, defender, attacker, damage, 0, 0, 0, 0, 100);
					defender.PlaySound( 0x51D );
					defender.FixedParticles( 0x37C4, 1, 25, 9922, 14, 3, EffectLayer.Head );
					if (defender.Hits < 0) defender.Kill();
				}
			}
			base.OnHit(attacker, defender, 1.0);
		}

		public SilverHalberd ( Serial serial ) : base( serial )
		{
		}
		
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();

			if ( Slayer == SlayerName.None )
				Slayer = SlayerName.Silver;
		}
	}
}