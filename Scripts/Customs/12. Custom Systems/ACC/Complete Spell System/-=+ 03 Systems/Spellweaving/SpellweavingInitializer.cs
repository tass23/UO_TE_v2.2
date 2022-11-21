using System;
using Server;
using Server.Spells.Spellweaving;

namespace Server.ACC.CSS.Systems.Spellweaving
{
    public class SpellweavingInitializer : BaseInitializer
    {
		public static void Configure()
		{
            Register( typeof( ArcaneCircleSpell ), "Arcane Circle",
						"Creates an Arcane Focus crystal, which is used to enhance other Spellweaving spells. You must be standing on an Arcane Circle, Pentagram, or Abattoir in order to cast this spell.",
						null,
						"Mana: 24; Skill: 0",
						23000, 5120, School.Spellweaving );
            Register( typeof( GiftOfRenewalSpell ), "Gift Of Renewal",
						"Heals a target repeatedly for a short period of time.",
						null,
						"Mana: 24; Skill: 0",
						23001, 5120, School.Spellweaving );
            Register( typeof( ImmolatingWeaponSpell ), "Immolating Weapon",
						"Enchants the caster's melee weapon with extra fire damage for a short duration.",
						null,
						"Mana: 32; Skill: 10",
						23002, 5120, School.Spellweaving );
            Register( typeof( AttuneWeaponSpell ), "Attunement",
						"Creates a magical shield around the caster that absorbs melee damage.",
						null,
						"Mana: 24; Skill: 0",
						23003, 5120, School.Spellweaving );
            Register( typeof( ThunderstormSpell ), "Thunderstorm",
						"When cast, all enemies within range of the caster will be dealt energy damage. If one of the victims is casting a spell and is interrupted, they will receive a Faster Casting Recovery penalty for a minumum of 5 seconds.",
						null,
						"Mana: 32; Skill: 10",
						23004, 5120, School.Spellweaving );
            Register( typeof( NatureFurySpell ), "Nature's Fury",
						"Creates an uncontrollable swarm of insects that attack nearby enemies.",
						null,
						"Mana: 24; Skill: 0",
						23005, 5120, School.Spellweaving );
            Register( typeof( SummonFeySpell ), "Summon Fey",
						"Summons one or more controllable Pixies. To cast (and obtain) this spell you must first complete the Friend of the Fey Quest.",
						null,
						"Mana: 10; Skill: 38",
						23006, 5120, School.Spellweaving );
            Register( typeof( SummonFiendSpell ), "Summon Fiend",
						"Summons one or more controllable Imps. To cast (and obtain) this spell you must first complete the Fiendish Friends Quest.",
						null,
						"Mana: 10; Skill: 38",
						23007, 5120, School.Spellweaving );
            Register( typeof( ReaperFormSpell ), "Reaper Form",
						"Enhances the caster's swing speed, spell damage, and resists while penalizing fire resist and movement speed.",
						null,
						"Mana: 34; Skill: 24",
						23008, 5120, School.Spellweaving );
            Register( typeof( WildfireSpell ), "Wildfire",
						"Creates a field of fire that damages enemies within it for a short time.",
						null,
						"Mana: 50; Skill: 66",
						23009, 5120, School.Spellweaving );
            Register( typeof( EssenceOfWindSpell ), "Essence of Wind",
						"Deals cold damage and gives a swing speed and Faster Casting penalty to nearby enemies.",
						null,
						"Mana: 40; Skill: 52",
						23010, 5120, School.Spellweaving );
            Register( typeof( DryadAllureSpell ), "Dryad Allure",
						"Charms a target (non-player) humanoid into doing the caster's bidding.",
						null,
						"Mana: 40; Skill: 52",
						23011, 5120, School.Spellweaving );
            Register( typeof( EtherealVoyageSpell ), "Ethereal Voyage",
						"Prevents monsters from being able to 'see' the caster for a short time.",
						null,
						"Mana: 32; Skill: 24",
						23012, 5120, School.Spellweaving );
            Register( typeof( WordOfDeathSpell ), "Word Of Death",
						"Does massive damage to creatures low in health.",
						null,
						"Mana: 50; Skill: 83",
						23013, 5120, School.Spellweaving );
            Register( typeof( GiftOfLifeSpell ), "Gift Of Life",
						"When in effect on the caster or caster's pet, the beneficiary will be resurrected upon death.",
						null,
						"Mana: 70; Skill: 38",
						23014, 5120, School.Spellweaving );
            Register( typeof( ArcaneEmpowermentSpell ), "Arcane Empowerment",
						"Enhances the caster's healing/damaging spells and increases the toughness of summons.",
						null,
						"Mana: 50; Skill: 24",
						23015, 5120, School.Spellweaving );						
		}
	}
}
