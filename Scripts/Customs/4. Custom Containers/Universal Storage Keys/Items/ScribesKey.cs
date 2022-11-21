using System;
using System.Collections;
using System.Collections.Generic;
using Server;
using Solaris.ItemStore;							//for connection to resource store data objects

namespace Server.Items
{
	//item derived from BaseResourceKey
	public class ScribesKey : BaseStoreKey
	{
		public override int DisplayColumns{ get{ return 3; } }
		
		public override List<StoreEntry> EntryStructure
		{
			get
			{
				List<StoreEntry> entry = base.EntryStructure;
				
				entry.Add( new ToolEntry( typeof( ScribesPen ), "Scribe's Pen" ) );
				entry.Add( new ResourceEntry( typeof( BlankScroll ), "Blank Scrolls" ) );
				entry.Add( new ResourceEntry( typeof( RecallRune ), "Blank Runes" ) );
				entry.Add( new ColumnSeparationEntry() );
				//entry.Add( new ListEntry( typeof( Spellbook ), typeof( SpellbookListEntry ), "Spell Books" ) );
				entry.Add( new ResourceEntry( typeof( ClumsyScroll ), "Clumsy" ) );
				entry.Add( new ResourceEntry( typeof( CreateFoodScroll ), "Create Food" ) );
				entry.Add( new ResourceEntry( typeof( FeeblemindScroll ), "Feeblemind" ) );
				entry.Add( new ResourceEntry( typeof( HealScroll ), "Heal" ) );
				entry.Add( new ResourceEntry( typeof( MagicArrowScroll ), "Magic Arrow" ) );
				entry.Add( new ResourceEntry( typeof( NightSightScroll ), "Night Sight" ) );
				entry.Add( new ResourceEntry( typeof( ReactiveArmorScroll ), "Reactive Armor" ) );
				entry.Add( new ResourceEntry( typeof( WeakenScroll ), "Weaken" ) );
				entry.Add( new ColumnSeparationEntry() );
				entry.Add( new ResourceEntry( typeof( AgilityScroll ), "Agility" ) );
				entry.Add( new ResourceEntry( typeof( CunningScroll ), "Cunning" ) );
				entry.Add( new ResourceEntry( typeof( CureScroll ), "Cure" ) );
				entry.Add( new ResourceEntry( typeof( HarmScroll ), "Harm" ) );
				entry.Add( new ResourceEntry( typeof( MagicTrapScroll ), "Magic Trap" ) );
				entry.Add( new ResourceEntry( typeof( MagicUnTrapScroll ), "Magic Untrap" ) );
				entry.Add( new ResourceEntry( typeof( ProtectionScroll ), "Protection" ) );
				entry.Add( new ResourceEntry( typeof( StrengthScroll ), "Strength" ) );
				entry.Add( new ColumnSeparationEntry() );
				entry.Add( new ResourceEntry( typeof( BlessScroll ), "Bless" ) );
				entry.Add( new ResourceEntry( typeof( FireballScroll ), "Fireball" ) );
				entry.Add( new ResourceEntry( typeof( MagicLockScroll ), "Magic Lock" ) );
				entry.Add( new ResourceEntry( typeof( PoisonScroll ), "Poison" ) );
				entry.Add( new ResourceEntry( typeof( TelekinisisScroll ), "Telekinisis" ) );
				entry.Add( new ResourceEntry( typeof( TeleportScroll ), "Teleport" ) );
				entry.Add( new ResourceEntry( typeof( UnlockScroll ), "Unlock" ) );
				entry.Add( new ResourceEntry( typeof( WallOfStoneScroll ), "Wall of Stone" ) );
				entry.Add( new ColumnSeparationEntry() );
				entry.Add( new ResourceEntry( typeof( ArchCureScroll ), "Arch Cure" ) );
				entry.Add( new ResourceEntry( typeof( ArchProtectionScroll ), "Arch Protection" ) );
				entry.Add( new ResourceEntry( typeof( CurseScroll ), "Curse" ) );
				entry.Add( new ResourceEntry( typeof( FireFieldScroll ), "Fire Field" ) );
				entry.Add( new ResourceEntry( typeof( GreaterHealScroll ), "Greater Heal" ) );
				entry.Add( new ResourceEntry( typeof( LightningScroll ), "Lightning" ) );
				entry.Add( new ResourceEntry( typeof( ManaDrainScroll ), "Mana Drain" ) );
				entry.Add( new ResourceEntry( typeof( RecallScroll ), "Recall" ) );
				entry.Add( new ColumnSeparationEntry() );
				entry.Add( new ResourceEntry( typeof( BladeSpiritsScroll ), "Blade Spirits" ) );
				entry.Add( new ResourceEntry( typeof( DispelFieldScroll ), "Dispel Field" ) );
				entry.Add( new ResourceEntry( typeof( IncognitoScroll ), "Incognito" ) );
				entry.Add( new ResourceEntry( typeof( MagicReflectScroll ), "Magic Reflection" ) );
				entry.Add( new ResourceEntry( typeof( MindBlastScroll ), "Mind Blast" ) );
				entry.Add( new ResourceEntry( typeof( ParalyzeScroll ), "Paralyze" ) );
				entry.Add( new ResourceEntry( typeof( PoisonFieldScroll ), "Poison Field" ) );
				entry.Add( new ResourceEntry( typeof( SummonCreatureScroll ), "Summ. Creature" ) );
				entry.Add( new ColumnSeparationEntry() );
				entry.Add( new ResourceEntry( typeof( DispelScroll ), "Dispel" ) );
				entry.Add( new ResourceEntry( typeof( EnergyBoltScroll ), "EnergyBolt" ) );
				entry.Add( new ResourceEntry( typeof( ExplosionScroll ), "Explosion" ) );
				entry.Add( new ResourceEntry( typeof( InvisibilityScroll ), "Invisibility" ) );
				entry.Add( new ResourceEntry( typeof( MarkScroll ), "Mark" ) );
				entry.Add( new ResourceEntry( typeof( MassCurseScroll ), "Mass Curse" ) );
				entry.Add( new ResourceEntry( typeof( ParalyzeFieldScroll ), "Paralyze Field" ) );
				entry.Add( new ResourceEntry( typeof( RevealScroll ), "Reveal" ) );
				entry.Add( new ColumnSeparationEntry() );
				entry.Add( new ResourceEntry( typeof( ChainLightningScroll ), "Chain Lightning" ) );
				entry.Add( new ResourceEntry( typeof( EnergyFieldScroll ), "Energy Field" ) );
				entry.Add( new ResourceEntry( typeof( FlamestrikeScroll ), "Flame Strike" ) );
				entry.Add( new ResourceEntry( typeof( GateTravelScroll ), "Gate Travel" ) );
				entry.Add( new ResourceEntry( typeof( ManaVampireScroll ), "ManaVampire" ) );
				entry.Add( new ResourceEntry( typeof( MassDispelScroll ), "Mass Dispel" ) );
				entry.Add( new ResourceEntry( typeof( MeteorSwarmScroll ), "Meteor Swarm" ) );
				entry.Add( new ResourceEntry( typeof( PolymorphScroll ), "Polymorph" ) );
				entry.Add( new ColumnSeparationEntry() );
				entry.Add( new ResourceEntry( typeof( EarthquakeScroll ), "Earthquake" ) );
				entry.Add( new ResourceEntry( typeof( EnergyVortexScroll ), "Energy Vortex" ) );
				entry.Add( new ResourceEntry( typeof( ResurrectionScroll ), "Resurrection" ) );
				entry.Add( new ResourceEntry( typeof( SummonAirElementalScroll ), "Air Elemental" ) );
				entry.Add( new ResourceEntry( typeof( SummonDaemonScroll ), "Summ. Daemon" ) );
				entry.Add( new ResourceEntry( typeof( SummonEarthElementalScroll ), "Earth Elemental" ) );
				entry.Add( new ResourceEntry( typeof( SummonFireElementalScroll ), "Fire Elemental" ) );
				entry.Add( new ResourceEntry( typeof( SummonWaterElementalScroll ), "Water Elemental" ) );
				entry.Add( new ColumnSeparationEntry() );
				entry.Add( new ResourceEntry( typeof( AnimateDeadScroll ), "Animate Dead" ) );
				entry.Add( new ResourceEntry( typeof( BloodOathScroll ), "Blood Oath" ) );
				entry.Add( new ResourceEntry( typeof( CorpseSkinScroll ), "Corpse Skin" ) );
				entry.Add( new ResourceEntry( typeof( CurseWeaponScroll ), "Curse Weapon" ) );
				entry.Add( new ResourceEntry( typeof( EvilOmenScroll ), "Evil Omen" ) );
				entry.Add( new ResourceEntry( typeof( ExorcismScroll ), "Exorcism" ) );
				entry.Add( new ResourceEntry( typeof( HorrificBeastScroll ), "Horrific Beast" ) );
				entry.Add( new ResourceEntry( typeof( LichFormScroll ), "Lich Form" ) );
				entry.Add( new ColumnSeparationEntry() );
				entry.Add( new ResourceEntry( typeof( MindRotScroll ), "Mind Rot" ) );
				entry.Add( new ResourceEntry( typeof( PainSpikeScroll ), "Pain Spike" ) );
				entry.Add( new ResourceEntry( typeof( PoisonStrikeScroll ), "Poison Strike" ) );
				entry.Add( new ResourceEntry( typeof( StrangleScroll ), "Strangle" ) );
				entry.Add( new ResourceEntry( typeof( SummonFamiliarScroll ), "Summ. Familiar" ) );
				entry.Add( new ResourceEntry( typeof( VampiricEmbraceScroll ), "Vamp. Embrace" ) );
				entry.Add( new ResourceEntry( typeof( VengefulSpiritScroll ), "Vengeful Spirit" ) );
				entry.Add( new ResourceEntry( typeof( WitherScroll ), "Wither" ) );
				entry.Add( new ResourceEntry( typeof( WraithFormScroll ), "Wraith Form" ) );
				
				return entry;
			}
		}
		
		[Constructable]
		public ScribesKey() : base( 0 )		//hue 0
		{
			ItemID = 0xFBE;			//open book
			Name = "Scribe's Tome";
		}
		
		//this loads properties specific to the store, like the gump label, and whether it's a dynamic storage device
		protected override ItemStore GenerateItemStore()
		{
			//load the basic store info
			ItemStore store = base.GenerateItemStore();

			//properties of this storage device
			store.Label = "Scribe's Storage";
			
			store.Dynamic = false;
			store.OfferDeeds = true;
			return store;
		}
		
		//serial constructor
		public ScribesKey( Serial serial ) : base( serial )
		{
		}
		
		//events
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			
			writer.Write( 0 );
		}
		
		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			
			int version = reader.ReadInt();
		}
	}
}