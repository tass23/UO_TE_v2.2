using System;

namespace Server.Items
{
	public class AlmondWrestling : Almond
	{
		private TimedSkillMod skillMod;

		[Constructable]
		public AlmondWrestling()
		{
			DefineMods(); Name = "Almond of Wrestling"; Stackable = false;
		}

		private void DefineMods()
		{
			DateTime duration = DateTime.Now + TimeSpan.FromSeconds( 60.0 );
			skillMod = new TimedSkillMod( SkillName.Wrestling, true, 10.0, duration );
			skillMod.ObeyCap = false;
		}

		private void SetMods( Mobile m ) { m.AddSkillMod( skillMod ); }

		public override void OnDoubleClick( Mobile from )
		{
			if ( !from.InRange( GetWorldLocation(), 1 ) ) { from.SendLocalizedMessage( 500446 ); }
			else
			{
				SetMods( from );
				from.PlaySound( 0x3A );
				Delete();
				base.OnDoubleClick( from );
			}
		}

		public AlmondWrestling( Serial serial ) : base( serial ) { DefineMods(); }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}

	public class AppleVeterinary : Apple
	{
		private TimedSkillMod skillMod;

		[Constructable]
		public AppleVeterinary()
		{
			DefineMods(); Name = "Apple of Veterinary"; Stackable = false;
		}

		private void DefineMods()
		{
			DateTime duration = DateTime.Now + TimeSpan.FromSeconds( 60.0 );
			skillMod = new TimedSkillMod( SkillName.Veterinary, true, 10.0, duration );
			skillMod.ObeyCap = false;
		}

		private void SetMods( Mobile m ) { m.AddSkillMod( skillMod ); }

		public override void OnDoubleClick( Mobile from )
		{
			if ( !from.InRange( GetWorldLocation(), 1 ) ) { from.SendLocalizedMessage( 500446 ); }
			else
			{
				SetMods( from );
				from.PlaySound( 0x3A );
				Delete();
				base.OnDoubleClick( from );
			}
		}

		public AppleVeterinary( Serial serial ) : base( serial ) { DefineMods(); }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}

	public class ApricotTracking : Apricot
	{
		private TimedSkillMod skillMod;

		[Constructable]
		public ApricotTracking()
		{
			DefineMods(); Name = "Apricot of Tracking"; Stackable = false;
		}

		private void DefineMods()
		{
			DateTime duration = DateTime.Now + TimeSpan.FromSeconds( 60.0 );
			skillMod = new TimedSkillMod( SkillName.Tracking, true, 10.0, duration );
			skillMod.ObeyCap = false;
		}

		private void SetMods( Mobile m ) { m.AddSkillMod( skillMod ); }

		public override void OnDoubleClick( Mobile from )
		{
			if ( !from.InRange( GetWorldLocation(), 1 ) ) { from.SendLocalizedMessage( 500446 ); }
			else
			{
				SetMods( from );
				from.PlaySound( 0x3A );
				Delete();
				base.OnDoubleClick( from );
			}
		}

		public ApricotTracking( Serial serial ) : base( serial ) { DefineMods(); }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}

	public class AsparagusTinkering : Asparagus
	{
		private TimedSkillMod skillMod;

		[Constructable]
		public AsparagusTinkering()
		{
			DefineMods(); Name = "Asparagus of Tinkering"; Stackable = false;
		}

		private void DefineMods()
		{
			DateTime duration = DateTime.Now + TimeSpan.FromSeconds( 60.0 );
			skillMod = new TimedSkillMod( SkillName.Tinkering, true, 10.0, duration );
			skillMod.ObeyCap = false;
		}

		private void SetMods( Mobile m ) { m.AddSkillMod( skillMod ); }

		public override void OnDoubleClick( Mobile from )
		{
			if ( !from.InRange( GetWorldLocation(), 1 ) ) { from.SendLocalizedMessage( 500446 ); }
			else
			{
				SetMods( from );
				from.PlaySound( 0x3A );
				Delete();
				base.OnDoubleClick( from );
			}
		}

		public AsparagusTinkering( Serial serial ) : base( serial ) { DefineMods(); }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}

	public class AvocadoTailoring : Avocado
	{
		private TimedSkillMod skillMod;

		[Constructable]
		public AvocadoTailoring()
		{
			DefineMods(); Name = "Avocado of Tailoring"; Stackable = false;
		}

		private void DefineMods()
		{
			DateTime duration = DateTime.Now + TimeSpan.FromSeconds( 60.0 );
			skillMod = new TimedSkillMod( SkillName.Tailoring, true, 10.0, duration );
			skillMod.ObeyCap = false;
		}

		private void SetMods( Mobile m ) { m.AddSkillMod( skillMod ); }

		public override void OnDoubleClick( Mobile from )
		{
			if ( !from.InRange( GetWorldLocation(), 1 ) ) { from.SendLocalizedMessage( 500446 ); }
			else
			{
				SetMods( from );
				from.PlaySound( 0x3A );
				Delete();
				base.OnDoubleClick( from );
			}
		}

		public AvocadoTailoring( Serial serial ) : base( serial ) { DefineMods(); }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}

	public class BananaTactics : Banana
	{
		private TimedSkillMod skillMod;

		[Constructable]
		public BananaTactics()
		{
			DefineMods(); Name = "Banana of Tactics"; Stackable = false;
		}

		private void DefineMods()
		{
			DateTime duration = DateTime.Now + TimeSpan.FromSeconds( 60.0 );
			skillMod = new TimedSkillMod( SkillName.Tactics, true, 10.0, duration );
			skillMod.ObeyCap = false;
		}

		private void SetMods( Mobile m ) { m.AddSkillMod( skillMod ); }

		public override void OnDoubleClick( Mobile from )
		{
			if ( !from.InRange( GetWorldLocation(), 1 ) ) { from.SendLocalizedMessage( 500446 ); }
			else
			{
				SetMods( from );
				from.PlaySound( 0x3A );
				Delete();
				base.OnDoubleClick( from );
			}
		}

		public BananaTactics( Serial serial ) : base( serial ) { DefineMods(); }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}

	public class BeetSwords : Beet
	{
		private TimedSkillMod skillMod;

		[Constructable]
		public BeetSwords()
		{
			DefineMods(); Name = "Beet of Swords"; Stackable = false;
		}

		private void DefineMods()
		{
			DateTime duration = DateTime.Now + TimeSpan.FromSeconds( 60.0 );
			skillMod = new TimedSkillMod( SkillName.Swords, true, 10.0, duration );
			skillMod.ObeyCap = false;
		}

		private void SetMods( Mobile m ) { m.AddSkillMod( skillMod ); }

		public override void OnDoubleClick( Mobile from )
		{
			if ( !from.InRange( GetWorldLocation(), 1 ) ) { from.SendLocalizedMessage( 500446 ); }
			else
			{
				SetMods( from );
				from.PlaySound( 0x3A );
				Delete();
				base.OnDoubleClick( from );
			}
		}

		public BeetSwords( Serial serial ) : base( serial ) { DefineMods(); }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}

	public class BlackberryStealth : Blackberry
	{
		private TimedSkillMod skillMod;

		[Constructable]
		public BlackberryStealth()
		{
			DefineMods(); Name = "Blackberry of Stealth"; Stackable = false;
		}

		private void DefineMods()
		{
			DateTime duration = DateTime.Now + TimeSpan.FromSeconds( 60.0 );
			skillMod = new TimedSkillMod( SkillName.Stealth, true, 10.0, duration );
			skillMod.ObeyCap = false;
		}

		private void SetMods( Mobile m ) { m.AddSkillMod( skillMod ); }

		public override void OnDoubleClick( Mobile from )
		{
			if ( !from.InRange( GetWorldLocation(), 1 ) ) { from.SendLocalizedMessage( 500446 ); }
			else
			{
				SetMods( from );
				from.PlaySound( 0x3A );
				Delete();
				base.OnDoubleClick( from );
			}
		}

		public BlackberryStealth( Serial serial ) : base( serial ) { DefineMods(); }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}

	public class BlackRaspberryStealing : BlackRaspberry
	{
		private TimedSkillMod skillMod;

		[Constructable]
		public BlackRaspberryStealing()
		{
			DefineMods(); Name = "Black Raspberry of Stealing"; Stackable = false;
		}

		private void DefineMods()
		{
			DateTime duration = DateTime.Now + TimeSpan.FromSeconds( 60.0 );
			skillMod = new TimedSkillMod( SkillName.Stealing, true, 10.0, duration );
			skillMod.ObeyCap = false;
		}

		private void SetMods( Mobile m ) { m.AddSkillMod( skillMod ); }

		public override void OnDoubleClick( Mobile from )
		{
			if ( !from.InRange( GetWorldLocation(), 1 ) ) { from.SendLocalizedMessage( 500446 ); }
			else
			{
				SetMods( from );
				from.PlaySound( 0x3A );
				Delete();
				base.OnDoubleClick( from );
			}
		}

		public BlackRaspberryStealing( Serial serial ) : base( serial ) { DefineMods(); }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}

	public class BlueberrySpiritSpeak : Blueberry
	{
		private TimedSkillMod skillMod;

		[Constructable]
		public BlueberrySpiritSpeak()
		{
			DefineMods(); Name = "Blueberry of Spirit Speak"; Stackable = false;
		}

		private void DefineMods()
		{
			DateTime duration = DateTime.Now + TimeSpan.FromSeconds( 60.0 );
			skillMod = new TimedSkillMod( SkillName.SpiritSpeak, true, 10.0, duration );
			skillMod.ObeyCap = false;
		}

		private void SetMods( Mobile m ) { m.AddSkillMod( skillMod ); }

		public override void OnDoubleClick( Mobile from )
		{
			if ( !from.InRange( GetWorldLocation(), 1 ) ) { from.SendLocalizedMessage( 500446 ); }
			else
			{
				SetMods( from );
				from.PlaySound( 0x3A );
				Delete();
				base.OnDoubleClick( from );
			}
		}

		public BlueberrySpiritSpeak( Serial serial ) : base( serial ) { DefineMods(); }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}

	public class BroccoliSpellweaving : Broccoli
	{
		private TimedSkillMod skillMod;

		[Constructable]
		public BroccoliSpellweaving ()
		{
			DefineMods(); Name = "Broccoli of Spellweaving "; Stackable = false;
		}

		private void DefineMods()
		{
			DateTime duration = DateTime.Now + TimeSpan.FromSeconds( 60.0 );
			skillMod = new TimedSkillMod( SkillName.Spellweaving , true, 10.0, duration );
			skillMod.ObeyCap = false;
		}

		private void SetMods( Mobile m ) { m.AddSkillMod( skillMod ); }

		public override void OnDoubleClick( Mobile from )
		{
			if ( !from.InRange( GetWorldLocation(), 1 ) ) { from.SendLocalizedMessage( 500446 ); }
			else
			{
				SetMods( from );
				from.PlaySound( 0x3A );
				Delete();
				base.OnDoubleClick( from );
			}
		}

		public BroccoliSpellweaving ( Serial serial ) : base( serial ) { DefineMods(); }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}

	public class CabbageSnooping : Cabbage
	{
		private TimedSkillMod skillMod;

		[Constructable]
		public CabbageSnooping()
		{
			DefineMods(); Name = "Cabbage of Snooping"; Stackable = false;
		}

		private void DefineMods()
		{
			DateTime duration = DateTime.Now + TimeSpan.FromSeconds( 60.0 );
			skillMod = new TimedSkillMod( SkillName.Snooping, true, 10.0, duration );
			skillMod.ObeyCap = false;
		}

		private void SetMods( Mobile m ) { m.AddSkillMod( skillMod ); }

		public override void OnDoubleClick( Mobile from )
		{
			if ( !from.InRange( GetWorldLocation(), 1 ) ) { from.SendLocalizedMessage( 500446 ); }
			else
			{
				SetMods( from );
				from.PlaySound( 0x3A );
				Delete();
				base.OnDoubleClick( from );
			}
		}

		public CabbageSnooping( Serial serial ) : base( serial ) { DefineMods(); }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}

	public class CantaloupeRemoveTrap : Cantaloupe
	{
		private TimedSkillMod skillMod;

		[Constructable]
		public CantaloupeRemoveTrap()
		{
			DefineMods(); Name = "Cantaloupe of Remove Trap"; Stackable = false;
		}

		private void DefineMods()
		{
			DateTime duration = DateTime.Now + TimeSpan.FromSeconds( 60.0 );
			skillMod = new TimedSkillMod( SkillName.RemoveTrap, true, 10.0, duration );
			skillMod.ObeyCap = false;
		}

		private void SetMods( Mobile m ) { m.AddSkillMod( skillMod ); }

		public override void OnDoubleClick( Mobile from )
		{
			if ( !from.InRange( GetWorldLocation(), 1 ) ) { from.SendLocalizedMessage( 500446 ); }
			else
			{
				SetMods( from );
				from.PlaySound( 0x3A );
				Delete();
				base.OnDoubleClick( from );
			}
		}

		public CantaloupeRemoveTrap( Serial serial ) : base( serial ) { DefineMods(); }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}

	public class CarrotProvocation : Carrot
	{
		private TimedSkillMod skillMod;

		[Constructable]
		public CarrotProvocation()
		{
			DefineMods(); Name = "Carrot of Provocation"; Stackable = false;
		}

		private void DefineMods()
		{
			DateTime duration = DateTime.Now + TimeSpan.FromSeconds( 60.0 );
			skillMod = new TimedSkillMod( SkillName.Provocation, true, 10.0, duration );
			skillMod.ObeyCap = false;
		}

		private void SetMods( Mobile m ) { m.AddSkillMod( skillMod ); }

		public override void OnDoubleClick( Mobile from )
		{
			if ( !from.InRange( GetWorldLocation(), 1 ) ) { from.SendLocalizedMessage( 500446 ); }
			else
			{
				SetMods( from );
				from.PlaySound( 0x3A );
				Delete();
				base.OnDoubleClick( from );
			}
		}

		public CarrotProvocation( Serial serial ) : base( serial ) { DefineMods(); }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}

	public class CauliflowerPoisoning : Cauliflower
	{
		private TimedSkillMod skillMod;

		[Constructable]
		public CauliflowerPoisoning()
		{
			DefineMods(); Name = "Cauliflower of Poisoning"; Stackable = false;
		}

		private void DefineMods()
		{
			DateTime duration = DateTime.Now + TimeSpan.FromSeconds( 60.0 );
			skillMod = new TimedSkillMod( SkillName.Poisoning, true, 10.0, duration );
			skillMod.ObeyCap = false;
		}

		private void SetMods( Mobile m ) { m.AddSkillMod( skillMod ); }

		public override void OnDoubleClick( Mobile from )
		{
			if ( !from.InRange( GetWorldLocation(), 1 ) ) { from.SendLocalizedMessage( 500446 ); }
			else
			{
				SetMods( from );
				from.PlaySound( 0x3A );
				Delete();
				base.OnDoubleClick( from );
			}
		}

		public CauliflowerPoisoning( Serial serial ) : base( serial ) { DefineMods(); }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}

	public class CeleryPeacemaking : Celery
	{
		private TimedSkillMod skillMod;

		[Constructable]
		public CeleryPeacemaking()
		{
			DefineMods(); Name = "Celery of Peacemaking"; Stackable = false;
		}

		private void DefineMods()
		{
			DateTime duration = DateTime.Now + TimeSpan.FromSeconds( 60.0 );
			skillMod = new TimedSkillMod( SkillName.Peacemaking, true, 10.0, duration );
			skillMod.ObeyCap = false;
		}

		private void SetMods( Mobile m ) { m.AddSkillMod( skillMod ); }

		public override void OnDoubleClick( Mobile from )
		{
			if ( !from.InRange( GetWorldLocation(), 1 ) ) { from.SendLocalizedMessage( 500446 ); }
			else
			{
				SetMods( from );
				from.PlaySound( 0x3A );
				Delete();
				base.OnDoubleClick( from );
			}
		}

		public CeleryPeacemaking( Serial serial ) : base( serial ) { DefineMods(); }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}

	public class CherryParry : Cherry
	{
		private TimedSkillMod skillMod;

		[Constructable]
		public CherryParry()
		{
			DefineMods(); Name = "Cherry of Parry"; Stackable = false;
		}

		private void DefineMods()
		{
			DateTime duration = DateTime.Now + TimeSpan.FromSeconds( 60.0 );
			skillMod = new TimedSkillMod( SkillName.Parry, true, 10.0, duration );
			skillMod.ObeyCap = false;
		}

		private void SetMods( Mobile m ) { m.AddSkillMod( skillMod ); }

		public override void OnDoubleClick( Mobile from )
		{
			if ( !from.InRange( GetWorldLocation(), 1 ) ) { from.SendLocalizedMessage( 500446 ); }
			else
			{
				SetMods( from );
				from.PlaySound( 0x3A );
				Delete();
				base.OnDoubleClick( from );
			}
		}

		public CherryParry( Serial serial ) : base( serial ) { DefineMods(); }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}

	public class ChiliPepperNinjitsu : ChiliPepper
	{
		private TimedSkillMod skillMod;

		[Constructable]
		public ChiliPepperNinjitsu()
		{
			DefineMods(); Name = "Chili Pepper of Ninjitsu"; Stackable = false;
		}

		private void DefineMods()
		{
			DateTime duration = DateTime.Now + TimeSpan.FromSeconds( 60.0 );
			skillMod = new TimedSkillMod( SkillName.Ninjitsu, true, 10.0, duration );
			skillMod.ObeyCap = false;
		}

		private void SetMods( Mobile m ) { m.AddSkillMod( skillMod ); }

		public override void OnDoubleClick( Mobile from )
		{
			if ( !from.InRange( GetWorldLocation(), 1 ) ) { from.SendLocalizedMessage( 500446 ); }
			else
			{
				SetMods( from );
				from.PlaySound( 0x3A );
				Delete();
				base.OnDoubleClick( from );
			}
		}

		public ChiliPepperNinjitsu( Serial serial ) : base( serial ) { DefineMods(); }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}

	public class CoconutNecromancy : Coconut
	{
		private TimedSkillMod skillMod;

		[Constructable]
		public CoconutNecromancy()
		{
			DefineMods(); Name = "Coconut of Necromancy"; Stackable = false;
		}

		private void DefineMods()
		{
			DateTime duration = DateTime.Now + TimeSpan.FromSeconds( 60.0 );
			skillMod = new TimedSkillMod( SkillName.Necromancy, true, 10.0, duration );
			skillMod.ObeyCap = false;
		}

		private void SetMods( Mobile m ) { m.AddSkillMod( skillMod ); }

		public override void OnDoubleClick( Mobile from )
		{
			if ( !from.InRange( GetWorldLocation(), 1 ) ) { from.SendLocalizedMessage( 500446 ); }
			else
			{
				SetMods( from );
				from.PlaySound( 0x3A );
				Delete();
				base.OnDoubleClick( from );
			}
		}

		public CoconutNecromancy( Serial serial ) : base( serial ) { DefineMods(); }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}

	public class CranberryMusicianship : Cranberry
	{
		private TimedSkillMod skillMod;

		[Constructable]
		public CranberryMusicianship()
		{
			DefineMods(); Name = "Cranberry of Musicianship"; Stackable = false;
		}

		private void DefineMods()
		{
			DateTime duration = DateTime.Now + TimeSpan.FromSeconds( 60.0 );
			skillMod = new TimedSkillMod( SkillName.Musicianship, true, 10.0, duration );
			skillMod.ObeyCap = false;
		}

		private void SetMods( Mobile m ) { m.AddSkillMod( skillMod ); }

		public override void OnDoubleClick( Mobile from )
		{
			if ( !from.InRange( GetWorldLocation(), 1 ) ) { from.SendLocalizedMessage( 500446 ); }
			else
			{
				SetMods( from );
				from.PlaySound( 0x3A );
				Delete();
				base.OnDoubleClick( from );
			}
		}

		public CranberryMusicianship( Serial serial ) : base( serial ) { DefineMods(); }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}

	public class CucumberMining : Cucumber
	{
		private TimedSkillMod skillMod;

		[Constructable]
		public CucumberMining()
		{
			DefineMods(); Name = "Cucumber of Mining"; Stackable = false;
		}

		private void DefineMods()
		{
			DateTime duration = DateTime.Now + TimeSpan.FromSeconds( 60.0 );
			skillMod = new TimedSkillMod( SkillName.Mining, true, 10.0, duration );
			skillMod.ObeyCap = false;
		}

		private void SetMods( Mobile m ) { m.AddSkillMod( skillMod ); }

		public override void OnDoubleClick( Mobile from )
		{
			if ( !from.InRange( GetWorldLocation(), 1 ) ) { from.SendLocalizedMessage( 500446 ); }
			else
			{
				SetMods( from );
				from.PlaySound( 0x3A );
				Delete();
				base.OnDoubleClick( from );
			}
		}

		public CucumberMining( Serial serial ) : base( serial ) { DefineMods(); }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}

	public class EggplantMeditation : Eggplant
	{
		private TimedSkillMod skillMod;

		[Constructable]
		public EggplantMeditation()
		{
			DefineMods(); Name = "Eggplant of Meditation"; Stackable = false;
		}

		private void DefineMods()
		{
			DateTime duration = DateTime.Now + TimeSpan.FromSeconds( 60.0 );
			skillMod = new TimedSkillMod( SkillName.Meditation, true, 10.0, duration );
			skillMod.ObeyCap = false;
		}

		private void SetMods( Mobile m ) { m.AddSkillMod( skillMod ); }

		public override void OnDoubleClick( Mobile from )
		{
			if ( !from.InRange( GetWorldLocation(), 1 ) ) { from.SendLocalizedMessage( 500446 ); }
			else
			{
				SetMods( from );
				from.PlaySound( 0x3A );
				Delete();
				base.OnDoubleClick( from );
			}
		}

		public EggplantMeditation( Serial serial ) : base( serial ) { DefineMods(); }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}

	public class GrapefruitMagicResist : Grapefruit
	{
		private TimedSkillMod skillMod;

		[Constructable]
		public GrapefruitMagicResist()
		{
			DefineMods(); Name = "Grapefruit of Magic Resist"; Stackable = false;
		}

		private void DefineMods()
		{
			DateTime duration = DateTime.Now + TimeSpan.FromSeconds( 60.0 );
			skillMod = new TimedSkillMod( SkillName.MagicResist, true, 10.0, duration );
			skillMod.ObeyCap = false;
		}

		private void SetMods( Mobile m ) { m.AddSkillMod( skillMod ); }

		public override void OnDoubleClick( Mobile from )
		{
			if ( !from.InRange( GetWorldLocation(), 1 ) ) { from.SendLocalizedMessage( 500446 ); }
			else
			{
				SetMods( from );
				from.PlaySound( 0x3A );
				Delete();
				base.OnDoubleClick( from );
			}
		}

		public GrapefruitMagicResist( Serial serial ) : base( serial ) { DefineMods(); }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}

	public class GreenBeanMagery : GreenBean
	{
		private TimedSkillMod skillMod;

		[Constructable]
		public GreenBeanMagery()
		{
			DefineMods(); Name = "Green Bean of Magery"; Stackable = false;
		}

		private void DefineMods()
		{
			DateTime duration = DateTime.Now + TimeSpan.FromSeconds( 60.0 );
			skillMod = new TimedSkillMod( SkillName.Magery, true, 10.0, duration );
			skillMod.ObeyCap = false;
		}

		private void SetMods( Mobile m ) { m.AddSkillMod( skillMod ); }

		public override void OnDoubleClick( Mobile from )
		{
			if ( !from.InRange( GetWorldLocation(), 1 ) ) { from.SendLocalizedMessage( 500446 ); }
			else
			{
				SetMods( from );
				from.PlaySound( 0x3A );
				Delete();
				base.OnDoubleClick( from );
			}
		}

		public GreenBeanMagery( Serial serial ) : base( serial ) { DefineMods(); }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}

	public class GreenPepperMacing : GreenPepper
	{
		private TimedSkillMod skillMod;

		[Constructable]
		public GreenPepperMacing()
		{
			DefineMods(); Name = "Green Pepper of Macing"; Stackable = false;
		}

		private void DefineMods()
		{
			DateTime duration = DateTime.Now + TimeSpan.FromSeconds( 60.0 );
			skillMod = new TimedSkillMod( SkillName.Macing, true, 10.0, duration );
			skillMod.ObeyCap = false;
		}

		private void SetMods( Mobile m ) { m.AddSkillMod( skillMod ); }

		public override void OnDoubleClick( Mobile from )
		{
			if ( !from.InRange( GetWorldLocation(), 1 ) ) { from.SendLocalizedMessage( 500446 ); }
			else
			{
				SetMods( from );
				from.PlaySound( 0x3A );
				Delete();
				base.OnDoubleClick( from );
			}
		}

		public GreenPepperMacing( Serial serial ) : base( serial ) { DefineMods(); }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}

	public class GreenSquashLumberjacking : GreenSquash
	{
		private TimedSkillMod skillMod;

		[Constructable]
		public GreenSquashLumberjacking()
		{
			DefineMods(); Name = "Green Squash of Lumberjacking"; Stackable = false;
		}

		private void DefineMods()
		{
			DateTime duration = DateTime.Now + TimeSpan.FromSeconds( 60.0 );
			skillMod = new TimedSkillMod( SkillName.Lumberjacking, true, 10.0, duration );
			skillMod.ObeyCap = false;
		}

		private void SetMods( Mobile m ) { m.AddSkillMod( skillMod ); }

		public override void OnDoubleClick( Mobile from )
		{
			if ( !from.InRange( GetWorldLocation(), 1 ) ) { from.SendLocalizedMessage( 500446 ); }
			else
			{
				SetMods( from );
				from.PlaySound( 0x3A );
				Delete();
				base.OnDoubleClick( from );
			}
		}

		public GreenSquashLumberjacking( Serial serial ) : base( serial ) { DefineMods(); }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}

	public class HoneydewMelonLockpicking : HoneydewMelon
	{
		private TimedSkillMod skillMod;

		[Constructable]
		public HoneydewMelonLockpicking()
		{
			DefineMods(); Name = "Honeydew Melon of Lockpicking"; Stackable = false;
		}

		private void DefineMods()
		{
			DateTime duration = DateTime.Now + TimeSpan.FromSeconds( 60.0 );
			skillMod = new TimedSkillMod( SkillName.Lockpicking, true, 10.0, duration );
			skillMod.ObeyCap = false;
		}

		private void SetMods( Mobile m ) { m.AddSkillMod( skillMod ); }

		public override void OnDoubleClick( Mobile from )
		{
			if ( !from.InRange( GetWorldLocation(), 1 ) ) { from.SendLocalizedMessage( 500446 ); }
			else
			{
				SetMods( from );
				from.PlaySound( 0x3A );
				Delete();
				base.OnDoubleClick( from );
			}
		}

		public HoneydewMelonLockpicking( Serial serial ) : base( serial ) { DefineMods(); }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}

	public class KiwiItemID : Kiwi
	{
		private TimedSkillMod skillMod;

		[Constructable]
		public KiwiItemID()
		{
			DefineMods(); Name = "Kiwi of Item ID"; Stackable = false;
		}

		private void DefineMods()
		{
			DateTime duration = DateTime.Now + TimeSpan.FromSeconds( 60.0 );
			skillMod = new TimedSkillMod( SkillName.ItemID, true, 10.0, duration );
			skillMod.ObeyCap = false;
		}

		private void SetMods( Mobile m ) { m.AddSkillMod( skillMod ); }

		public override void OnDoubleClick( Mobile from )
		{
			if ( !from.InRange( GetWorldLocation(), 1 ) ) { from.SendLocalizedMessage( 500446 ); }
			else
			{
				SetMods( from );
				from.PlaySound( 0x3A );
				Delete();
				base.OnDoubleClick( from );
			}
		}

		public KiwiItemID( Serial serial ) : base( serial ) { DefineMods(); }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}

	public class LettuceInscribe : Lettuce
	{
		private TimedSkillMod skillMod;

		[Constructable]
		public LettuceInscribe()
		{
			DefineMods(); Name = "Lettuce of Inscribe"; Stackable = false;
		}

		private void DefineMods()
		{
			DateTime duration = DateTime.Now + TimeSpan.FromSeconds( 60.0 );
			skillMod = new TimedSkillMod( SkillName.Inscribe, true, 10.0, duration );
			skillMod.ObeyCap = false;
		}

		private void SetMods( Mobile m ) { m.AddSkillMod( skillMod ); }

		public override void OnDoubleClick( Mobile from )
		{
			if ( !from.InRange( GetWorldLocation(), 1 ) ) { from.SendLocalizedMessage( 500446 ); }
			else
			{
				SetMods( from );
				from.PlaySound( 0x3A );
				Delete();
				base.OnDoubleClick( from );
			}
		}

		public LettuceInscribe( Serial serial ) : base( serial ) { DefineMods(); }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}

	public class MangoHiding : Mango
	{
		private TimedSkillMod skillMod;

		[Constructable]
		public MangoHiding()
		{
			DefineMods(); Name = "Mango of Hiding"; Stackable = false;
		}

		private void DefineMods()
		{
			DateTime duration = DateTime.Now + TimeSpan.FromSeconds( 60.0 );
			skillMod = new TimedSkillMod( SkillName.Hiding, true, 10.0, duration );
			skillMod.ObeyCap = false;
		}

		private void SetMods( Mobile m ) { m.AddSkillMod( skillMod ); }

		public override void OnDoubleClick( Mobile from )
		{
			if ( !from.InRange( GetWorldLocation(), 1 ) ) { from.SendLocalizedMessage( 500446 ); }
			else
			{
				SetMods( from );
				from.PlaySound( 0x3A );
				Delete();
				base.OnDoubleClick( from );
			}
		}

		public MangoHiding( Serial serial ) : base( serial ) { DefineMods(); }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}

	public class OnionHerding : Onion
	{
		private TimedSkillMod skillMod;

		[Constructable]
		public OnionHerding()
		{
			DefineMods(); Name = "Onion of Herding"; Stackable = false;
		}

		private void DefineMods()
		{
			DateTime duration = DateTime.Now + TimeSpan.FromSeconds( 60.0 );
			skillMod = new TimedSkillMod( SkillName.Herding, true, 10.0, duration );
			skillMod.ObeyCap = false;
		}

		private void SetMods( Mobile m ) { m.AddSkillMod( skillMod ); }

		public override void OnDoubleClick( Mobile from )
		{
			if ( !from.InRange( GetWorldLocation(), 1 ) ) { from.SendLocalizedMessage( 500446 ); }
			else
			{
				SetMods( from );
				from.PlaySound( 0x3A );
				Delete();
				base.OnDoubleClick( from );
			}
		}

		public OnionHerding( Serial serial ) : base( serial ) { DefineMods(); }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}

	public class OrangeHealing : Orange
	{
		private TimedSkillMod skillMod;

		[Constructable]
		public OrangeHealing()
		{
			DefineMods(); Name = "Orange of Healing"; Stackable = false;
		}

		private void DefineMods()
		{
			DateTime duration = DateTime.Now + TimeSpan.FromSeconds( 60.0 );
			skillMod = new TimedSkillMod( SkillName.Healing, true, 10.0, duration );
			skillMod.ObeyCap = false;
		}

		private void SetMods( Mobile m ) { m.AddSkillMod( skillMod ); }

		public override void OnDoubleClick( Mobile from )
		{
			if ( !from.InRange( GetWorldLocation(), 1 ) ) { from.SendLocalizedMessage( 500446 ); }
			else
			{
				SetMods( from );
				from.PlaySound( 0x3A );
				Delete();
				base.OnDoubleClick( from );
			}
		}

		public OrangeHealing( Serial serial ) : base( serial ) { DefineMods(); }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}

	public class OrangePepperForensics : OrangePepper
	{
		private TimedSkillMod skillMod;

		[Constructable]
		public OrangePepperForensics()
		{
			DefineMods(); Name = "Orange Pepper of Forensics"; Stackable = false;
		}

		private void DefineMods()
		{
			DateTime duration = DateTime.Now + TimeSpan.FromSeconds( 60.0 );
			skillMod = new TimedSkillMod( SkillName.Forensics, true, 10.0, duration );
			skillMod.ObeyCap = false;
		}

		private void SetMods( Mobile m ) { m.AddSkillMod( skillMod ); }

		public override void OnDoubleClick( Mobile from )
		{
			if ( !from.InRange( GetWorldLocation(), 1 ) ) { from.SendLocalizedMessage( 500446 ); }
			else
			{
				SetMods( from );
				from.PlaySound( 0x3A );
				Delete();
				base.OnDoubleClick( from );
			}
		}

		public OrangePepperForensics( Serial serial ) : base( serial ) { DefineMods(); }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}

	public class PeachFocus : Peach
	{
		private TimedSkillMod skillMod;

		[Constructable]
		public PeachFocus()
		{
			DefineMods(); Name = "Peach of Focus"; Stackable = false;
		}

		private void DefineMods()
		{
			DateTime duration = DateTime.Now + TimeSpan.FromSeconds( 60.0 );
			skillMod = new TimedSkillMod( SkillName.Focus, true, 10.0, duration );
			skillMod.ObeyCap = false;
		}

		private void SetMods( Mobile m ) { m.AddSkillMod( skillMod ); }

		public override void OnDoubleClick( Mobile from )
		{
			if ( !from.InRange( GetWorldLocation(), 1 ) ) { from.SendLocalizedMessage( 500446 ); }
			else
			{
				SetMods( from );
				from.PlaySound( 0x3A );
				Delete();
				base.OnDoubleClick( from );
			}
		}

		public PeachFocus( Serial serial ) : base( serial ) { DefineMods(); }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}

	public class PeanutFletching : Peanut
	{
		private TimedSkillMod skillMod;

		[Constructable]
		public PeanutFletching()
		{
			DefineMods(); Name = "Peanut of Fletching"; Stackable = false;
		}

		private void DefineMods()
		{
			DateTime duration = DateTime.Now + TimeSpan.FromSeconds( 60.0 );
			skillMod = new TimedSkillMod( SkillName.Fletching, true, 10.0, duration );
			skillMod.ObeyCap = false;
		}

		private void SetMods( Mobile m ) { m.AddSkillMod( skillMod ); }

		public override void OnDoubleClick( Mobile from )
		{
			if ( !from.InRange( GetWorldLocation(), 1 ) ) { from.SendLocalizedMessage( 500446 ); }
			else
			{
				SetMods( from );
				from.PlaySound( 0x3A );
				Delete();
				base.OnDoubleClick( from );
			}
		}

		public PeanutFletching( Serial serial ) : base( serial ) { DefineMods(); }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}

	public class PearFishing : Pear
	{
		private TimedSkillMod skillMod;

		[Constructable]
		public PearFishing()
		{
			DefineMods(); Name = "Pear of Fishing"; Stackable = false;
		}

		private void DefineMods()
		{
			DateTime duration = DateTime.Now + TimeSpan.FromSeconds( 60.0 );
			skillMod = new TimedSkillMod( SkillName.Fishing, true, 10.0, duration );
			skillMod.ObeyCap = false;
		}

		private void SetMods( Mobile m ) { m.AddSkillMod( skillMod ); }

		public override void OnDoubleClick( Mobile from )
		{
			if ( !from.InRange( GetWorldLocation(), 1 ) ) { from.SendLocalizedMessage( 500446 ); }
			else
			{
				SetMods( from );
				from.PlaySound( 0x3A );
				Delete();
				base.OnDoubleClick( from );
			}
		}

		public PearFishing( Serial serial ) : base( serial ) { DefineMods(); }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}

	public class PeasFencing : Peas
	{
		private TimedSkillMod skillMod;

		[Constructable]
		public PeasFencing()
		{
			DefineMods(); Name = "Peas of Fencing"; Stackable = false;
		}

		private void DefineMods()
		{
			DateTime duration = DateTime.Now + TimeSpan.FromSeconds( 60.0 );
			skillMod = new TimedSkillMod( SkillName.Fencing, true, 10.0, duration );
			skillMod.ObeyCap = false;
		}

		private void SetMods( Mobile m ) { m.AddSkillMod( skillMod ); }

		public override void OnDoubleClick( Mobile from )
		{
			if ( !from.InRange( GetWorldLocation(), 1 ) ) { from.SendLocalizedMessage( 500446 ); }
			else
			{
				SetMods( from );
				from.PlaySound( 0x3A );
				Delete();
				base.OnDoubleClick( from );
			}
		}

		public PeasFencing( Serial serial ) : base( serial ) { DefineMods(); }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}

	public class PineappleEvalInt : Pineapple
	{
		private TimedSkillMod skillMod;

		[Constructable]
		public PineappleEvalInt()
		{
			DefineMods(); Name = "Pineapple of EvalInt"; Stackable = false;
		}

		private void DefineMods()
		{
			DateTime duration = DateTime.Now + TimeSpan.FromSeconds( 60.0 );
			skillMod = new TimedSkillMod( SkillName.EvalInt, true, 10.0, duration );
			skillMod.ObeyCap = false;
		}

		private void SetMods( Mobile m ) { m.AddSkillMod( skillMod ); }

		public override void OnDoubleClick( Mobile from )
		{
			if ( !from.InRange( GetWorldLocation(), 1 ) ) { from.SendLocalizedMessage( 500446 ); }
			else
			{
				SetMods( from );
				from.PlaySound( 0x3A );
				Delete();
				base.OnDoubleClick( from );
			}
		}

		public PineappleEvalInt( Serial serial ) : base( serial ) { DefineMods(); }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}

	public class PistacioDiscordance : Pistacio
	{
		private TimedSkillMod skillMod;

		[Constructable]
		public PistacioDiscordance()
		{
			DefineMods(); Name = "Pistacio of Discordance"; Stackable = false;
		}

		private void DefineMods()
		{
			DateTime duration = DateTime.Now + TimeSpan.FromSeconds( 60.0 );
			skillMod = new TimedSkillMod( SkillName.Discordance, true, 10.0, duration );
			skillMod.ObeyCap = false;
		}

		private void SetMods( Mobile m ) { m.AddSkillMod( skillMod ); }

		public override void OnDoubleClick( Mobile from )
		{
			if ( !from.InRange( GetWorldLocation(), 1 ) ) { from.SendLocalizedMessage( 500446 ); }
			else
			{
				SetMods( from );
				from.PlaySound( 0x3A );
				Delete();
				base.OnDoubleClick( from );
			}
		}

		public PistacioDiscordance( Serial serial ) : base( serial ) { DefineMods(); }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}

	public class PomegranateDetectHidden : Pomegranate
	{
		private TimedSkillMod skillMod;

		[Constructable]
		public PomegranateDetectHidden()
		{
			DefineMods(); Name = "Pomegranate of Detect Hidden"; Stackable = false;
		}

		private void DefineMods()
		{
			DateTime duration = DateTime.Now + TimeSpan.FromSeconds( 60.0 );
			skillMod = new TimedSkillMod( SkillName.DetectHidden, true, 10.0, duration );
			skillMod.ObeyCap = false;
		}

		private void SetMods( Mobile m ) { m.AddSkillMod( skillMod ); }

		public override void OnDoubleClick( Mobile from )
		{
			if ( !from.InRange( GetWorldLocation(), 1 ) ) { from.SendLocalizedMessage( 500446 ); }
			else
			{
				SetMods( from );
				from.PlaySound( 0x3A );
				Delete();
				base.OnDoubleClick( from );
			}
		}

		public PomegranateDetectHidden( Serial serial ) : base( serial ) { DefineMods(); }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}

	public class PotatoCooking : Potato
	{
		private TimedSkillMod skillMod;

		[Constructable]
		public PotatoCooking()
		{
			DefineMods(); Name = "Potato of Cooking"; Stackable = false;
		}

		private void DefineMods()
		{
			DateTime duration = DateTime.Now + TimeSpan.FromSeconds( 60.0 );
			skillMod = new TimedSkillMod( SkillName.Cooking, true, 10.0, duration );
			skillMod.ObeyCap = false;
		}

		private void SetMods( Mobile m ) { m.AddSkillMod( skillMod ); }

		public override void OnDoubleClick( Mobile from )
		{
			if ( !from.InRange( GetWorldLocation(), 1 ) ) { from.SendLocalizedMessage( 500446 ); }
			else
			{
				SetMods( from );
				from.PlaySound( 0x3A );
				Delete();
				base.OnDoubleClick( from );
			}
		}

		public PotatoCooking( Serial serial ) : base( serial ) { DefineMods(); }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}

	public class PumpkinChivalry : Pumpkin
	{
		private TimedSkillMod skillMod;

		[Constructable]
		public PumpkinChivalry()
		{
			DefineMods(); Name = "Pumpkin of Chivalry"; Stackable = false;
		}

		private void DefineMods()
		{
			DateTime duration = DateTime.Now + TimeSpan.FromSeconds( 60.0 );
			skillMod = new TimedSkillMod( SkillName.Chivalry, true, 10.0, duration );
			skillMod.ObeyCap = false;
		}

		private void SetMods( Mobile m ) { m.AddSkillMod( skillMod ); }

		public override void OnDoubleClick( Mobile from )
		{
			if ( !from.InRange( GetWorldLocation(), 1 ) ) { from.SendLocalizedMessage( 500446 ); }
			else
			{
				SetMods( from );
				from.PlaySound( 0x3A );
				Delete();
				base.OnDoubleClick( from );
			}
		}

		public PumpkinChivalry( Serial serial ) : base( serial ) { DefineMods(); }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}

	public class RadishCartography : Radish
	{
		private TimedSkillMod skillMod;

		[Constructable]
		public RadishCartography()
		{
			DefineMods(); Name = "Radish of Cartography"; Stackable = false;
		}

		private void DefineMods()
		{
			DateTime duration = DateTime.Now + TimeSpan.FromSeconds( 60.0 );
			skillMod = new TimedSkillMod( SkillName.Cartography, true, 10.0, duration );
			skillMod.ObeyCap = false;
		}

		private void SetMods( Mobile m ) { m.AddSkillMod( skillMod ); }

		public override void OnDoubleClick( Mobile from )
		{
			if ( !from.InRange( GetWorldLocation(), 1 ) ) { from.SendLocalizedMessage( 500446 ); }
			else
			{
				SetMods( from );
				from.PlaySound( 0x3A );
				Delete();
				base.OnDoubleClick( from );
			}
		}

		public RadishCartography( Serial serial ) : base( serial ) { DefineMods(); }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}

	public class RedPepperCarpentry : RedPepper
	{
		private TimedSkillMod skillMod;

		[Constructable]
		public RedPepperCarpentry()
		{
			DefineMods(); Name = "Red Pepper of Carpentry"; Stackable = false;
		}

		private void DefineMods()
		{
			DateTime duration = DateTime.Now + TimeSpan.FromSeconds( 60.0 );
			skillMod = new TimedSkillMod( SkillName.Carpentry, true, 10.0, duration );
			skillMod.ObeyCap = false;
		}

		private void SetMods( Mobile m ) { m.AddSkillMod( skillMod ); }

		public override void OnDoubleClick( Mobile from )
		{
			if ( !from.InRange( GetWorldLocation(), 1 ) ) { from.SendLocalizedMessage( 500446 ); }
			else
			{
				SetMods( from );
				from.PlaySound( 0x3A );
				Delete();
				base.OnDoubleClick( from );
			}
		}

		public RedPepperCarpentry( Serial serial ) : base( serial ) { DefineMods(); }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}

	public class RedRaspberryCamping : RedRaspberry
	{
		private TimedSkillMod skillMod;

		[Constructable]
		public RedRaspberryCamping()
		{
			DefineMods(); Name = "Red Raspberry of Camping"; Stackable = false;
		}

		private void DefineMods()
		{
			DateTime duration = DateTime.Now + TimeSpan.FromSeconds( 60.0 );
			skillMod = new TimedSkillMod( SkillName.Camping, true, 10.0, duration );
			skillMod.ObeyCap = false;
		}

		private void SetMods( Mobile m ) { m.AddSkillMod( skillMod ); }

		public override void OnDoubleClick( Mobile from )
		{
			if ( !from.InRange( GetWorldLocation(), 1 ) ) { from.SendLocalizedMessage( 500446 ); }
			else
			{
				SetMods( from );
				from.PlaySound( 0x3A );
				Delete();
				base.OnDoubleClick( from );
			}
		}

		public RedRaspberryCamping( Serial serial ) : base( serial ) { DefineMods(); }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}

	public class SnowPeasBushido : SnowPeas
	{
		private TimedSkillMod skillMod;

		[Constructable]
		public SnowPeasBushido()
		{
			DefineMods(); Name = "Snow Peas of Bushido"; Stackable = false;
		}

		private void DefineMods()
		{
			DateTime duration = DateTime.Now + TimeSpan.FromSeconds( 60.0 );
			skillMod = new TimedSkillMod( SkillName.Bushido, true, 10.0, duration );
			skillMod.ObeyCap = false;
		}

		private void SetMods( Mobile m ) { m.AddSkillMod( skillMod ); }

		public override void OnDoubleClick( Mobile from )
		{
			if ( !from.InRange( GetWorldLocation(), 1 ) ) { from.SendLocalizedMessage( 500446 ); }
			else
			{
				SetMods( from );
				from.PlaySound( 0x3A );
				Delete();
				base.OnDoubleClick( from );
			}
		}

		public SnowPeasBushido( Serial serial ) : base( serial ) { DefineMods(); }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}

	public class SpinachBlacksmith : Spinach
	{
		private TimedSkillMod skillMod;

		[Constructable]
		public SpinachBlacksmith()
		{
			DefineMods(); Name = "Spinach of Blacksmith"; Stackable = false;
		}

		private void DefineMods()
		{
			DateTime duration = DateTime.Now + TimeSpan.FromSeconds( 60.0 );
			skillMod = new TimedSkillMod( SkillName.Blacksmith, true, 10.0, duration );
			skillMod.ObeyCap = false;
		}

		private void SetMods( Mobile m ) { m.AddSkillMod( skillMod ); }

		public override void OnDoubleClick( Mobile from )
		{
			if ( !from.InRange( GetWorldLocation(), 1 ) ) { from.SendLocalizedMessage( 500446 ); }
			else
			{
				SetMods( from );
				from.PlaySound( 0x3A );
				Delete();
				base.OnDoubleClick( from );
			}
		}

		public SpinachBlacksmith( Serial serial ) : base( serial ) { DefineMods(); }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}

	public class SquashBegging : Squash
	{
		private TimedSkillMod skillMod;

		[Constructable]
		public SquashBegging()
		{
			DefineMods(); Name = "Squash of Begging"; Stackable = false;
		}

		private void DefineMods()
		{
			DateTime duration = DateTime.Now + TimeSpan.FromSeconds( 60.0 );
			skillMod = new TimedSkillMod( SkillName.Begging, true, 10.0, duration );
			skillMod.ObeyCap = false;
		}

		private void SetMods( Mobile m ) { m.AddSkillMod( skillMod ); }

		public override void OnDoubleClick( Mobile from )
		{
			if ( !from.InRange( GetWorldLocation(), 1 ) ) { from.SendLocalizedMessage( 500446 ); }
			else
			{
				SetMods( from );
				from.PlaySound( 0x3A );
				Delete();
				base.OnDoubleClick( from );
			}
		}

		public SquashBegging( Serial serial ) : base( serial ) { DefineMods(); }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}

	public class StrawberryArmsLore : Strawberry
	{
		private TimedSkillMod skillMod;

		[Constructable]
		public StrawberryArmsLore()
		{
			DefineMods(); Name = "Strawberry of Arms Lore"; Stackable = false;
		}

		private void DefineMods()
		{
			DateTime duration = DateTime.Now + TimeSpan.FromSeconds( 60.0 );
			skillMod = new TimedSkillMod( SkillName.ArmsLore, true, 10.0, duration );
			skillMod.ObeyCap = false;
		}

		private void SetMods( Mobile m ) { m.AddSkillMod( skillMod ); }

		public override void OnDoubleClick( Mobile from )
		{
			if ( !from.InRange( GetWorldLocation(), 1 ) ) { from.SendLocalizedMessage( 500446 ); }
			else
			{
				SetMods( from );
				from.PlaySound( 0x3A );
				Delete();
				base.OnDoubleClick( from );
			}
		}

		public StrawberryArmsLore( Serial serial ) : base( serial ) { DefineMods(); }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}

	public class SweetPotatoArchery : SweetPotato
	{
		private TimedSkillMod skillMod;

		[Constructable]
		public SweetPotatoArchery()
		{
			DefineMods(); Name = "Sweet Potato of Archery"; Stackable = false;
		}

		private void DefineMods()
		{
			DateTime duration = DateTime.Now + TimeSpan.FromSeconds( 60.0 );
			skillMod = new TimedSkillMod( SkillName.Archery, true, 10.0, duration );
			skillMod.ObeyCap = false;
		}

		private void SetMods( Mobile m ) { m.AddSkillMod( skillMod ); }

		public override void OnDoubleClick( Mobile from )
		{
			if ( !from.InRange( GetWorldLocation(), 1 ) ) { from.SendLocalizedMessage( 500446 ); }
			else
			{
				SetMods( from );
				from.PlaySound( 0x3A );
				Delete();
				base.OnDoubleClick( from );
			}
		}

		public SweetPotatoArchery( Serial serial ) : base( serial ) { DefineMods(); }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}

	public class TomatoAnimalTaming : Tomato
	{
		private TimedSkillMod skillMod;

		[Constructable]
		public TomatoAnimalTaming()
		{
			DefineMods(); Name = "Tomato of Animal Taming"; Stackable = false;
		}

		private void DefineMods()
		{
			DateTime duration = DateTime.Now + TimeSpan.FromSeconds( 60.0 );
			skillMod = new TimedSkillMod( SkillName.AnimalTaming, true, 10.0, duration );
			skillMod.ObeyCap = false;
		}

		private void SetMods( Mobile m ) { m.AddSkillMod( skillMod ); }

		public override void OnDoubleClick( Mobile from )
		{
			if ( !from.InRange( GetWorldLocation(), 1 ) ) { from.SendLocalizedMessage( 500446 ); }
			else
			{
				SetMods( from );
				from.PlaySound( 0x3A );
				Delete();
				base.OnDoubleClick( from );
			}
		}

		public TomatoAnimalTaming( Serial serial ) : base( serial ) { DefineMods(); }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}

	public class TurnipAnimalLore : Turnip
	{
		private TimedSkillMod skillMod;

		[Constructable]
		public TurnipAnimalLore()
		{
			DefineMods(); Name = "Turnip of Animal Lore"; Stackable = false;
		}

		private void DefineMods()
		{
			DateTime duration = DateTime.Now + TimeSpan.FromSeconds( 60.0 );
			skillMod = new TimedSkillMod( SkillName.AnimalLore, true, 10.0, duration );
			skillMod.ObeyCap = false;
		}

		private void SetMods( Mobile m ) { m.AddSkillMod( skillMod ); }

		public override void OnDoubleClick( Mobile from )
		{
			if ( !from.InRange( GetWorldLocation(), 1 ) ) { from.SendLocalizedMessage( 500446 ); }
			else
			{
				SetMods( from );
				from.PlaySound( 0x3A );
				Delete();
				base.OnDoubleClick( from );
			}
		}

		public TurnipAnimalLore( Serial serial ) : base( serial ) { DefineMods(); }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}

	public class WatermelonAnatomy : Watermelon
	{
		private TimedSkillMod skillMod;

		[Constructable]
		public WatermelonAnatomy()
		{
			DefineMods(); Name = "Watermelon of Anatomy"; Stackable = false;
		}

		private void DefineMods()
		{
			DateTime duration = DateTime.Now + TimeSpan.FromSeconds( 60.0 );
			skillMod = new TimedSkillMod( SkillName.Anatomy, true, 10.0, duration );
			skillMod.ObeyCap = false;
		}

		private void SetMods( Mobile m ) { m.AddSkillMod( skillMod ); }

		public override void OnDoubleClick( Mobile from )
		{
			if ( !from.InRange( GetWorldLocation(), 1 ) ) { from.SendLocalizedMessage( 500446 ); }
			else
			{
				SetMods( from );
				from.PlaySound( 0x3A );
				Delete();
				base.OnDoubleClick( from );
			}
		}

		public WatermelonAnatomy( Serial serial ) : base( serial ) { DefineMods(); }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}

	public class YellowPepperAlchemy : YellowPepper
	{
		private TimedSkillMod skillMod;

		[Constructable]
		public YellowPepperAlchemy()
		{
			DefineMods(); Name = "Yellow Pepper of Alchemy"; Stackable = false;
		}

		private void DefineMods()
		{
			DateTime duration = DateTime.Now + TimeSpan.FromSeconds( 60.0 );
			skillMod = new TimedSkillMod( SkillName.Alchemy, true, 10.0, duration );
			skillMod.ObeyCap = false;
		}

		private void SetMods( Mobile m ) { m.AddSkillMod( skillMod ); }

		public override void OnDoubleClick( Mobile from )
		{
			if ( !from.InRange( GetWorldLocation(), 1 ) ) { from.SendLocalizedMessage( 500446 ); }
			else
			{
				SetMods( from );
				from.PlaySound( 0x3A );
				Delete();
				base.OnDoubleClick( from );
			}
		}

		public YellowPepperAlchemy( Serial serial ) : base( serial ) { DefineMods(); }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}
}