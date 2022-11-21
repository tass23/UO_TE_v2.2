using System;
using System.Collections;
using Server.Misc;
using Server.Items;
using Server.Targeting;
using Server.Spells;
using Server.Mobiles;

namespace Server.ACC.CSS.Systems.LightForce
{
    public class LightGuardian : BaseCreature
    {
		public override bool IsDispellable { get { return false; } }
		public override bool IsBondable { get { return false; } }
		
        [Constructable]
        public LightGuardian() : base(AIType.AI_Jedi, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = NameList.RandomName( "male" );
            Body = 0x190;
			Hue = 1285;
			Title = "the ancient Jedi Master";
			SetStr( 250, 285 );
			SetDex( 250, 290 );
			SetInt( 250, 300 );
			SetHits( 1792, 1911 );

			SetDamage( 19, 27 );
            SetDamageType(ResistanceType.Physical, 50);
            SetDamageType(ResistanceType.Energy, 50);
			SetResistance( ResistanceType.Physical, 70, 85 );
			SetResistance( ResistanceType.Fire, 60, 85 );
			SetResistance( ResistanceType.Cold, 50, 85 );
			SetResistance( ResistanceType.Poison, 50, 65 );
			SetResistance( ResistanceType.Energy, 85, 100 );
            SetSkill(SkillName.MagicResist, 95, 115.0);
            SetSkill(SkillName.Tactics, 90, 120.0);
			SetSkill(SkillName.Anatomy, 80, 120.0);
			SetSkill(SkillName.Parry, 75, 110.0);
			SetSkill(SkillName.Swords, 120.0);
			SetSkill(SkillName.Wrestling, 55, 100.0);
            SetSkill(SkillName.Meditation, 95, 120.0);
			SetSkill(SkillName.Necromancy, 85, 100.0);
			SetSkill(SkillName.Magery, 85, 100.0);
			
			AddItem( new LeatherChest() );
			AddItem( new LeatherArms() );
			AddItem( new LeatherGloves() );
			AddItem( new LeatherGorget() );
			AddItem( new LeatherLegs() );
			AddItem( new Boots() );
			AddItem( new LeatherCap() );
			AddItem( new JediMasterRobe() );
			AddItem( new JediMasterCloak() );

			Lightsaber lightsaber;
				switch ( Utility.Random( 8 ) )
				{
					case 0: lightsaber = new HurrikaineLightsaber(); break;
					case 1: lightsaber = new BondaraFollyLightsaber(); break;
					case 2: lightsaber = new BondarLightsaber(); break;
					case 3: lightsaber = new DagobahLightsaber(); break;
					case 4: lightsaber = new DragiteLightsaber(); break;
					case 5: lightsaber = new DurindfireLightsaber(); break;
					case 6: lightsaber = new AdeganLightsaber(); break;
					case 7: lightsaber = new GuardianLightsaber(); break;
					default: lightsaber = new AnkarresLightsaber(); break;
				}

			lightsaber.Movable = false;
			lightsaber.Crafter = this;
			lightsaber.Quality = WeaponQuality.Exceptional;

			AddItem( lightsaber );
			Container pack = new Backpack();
			pack.Movable = false;

            Fame = 0;
            Karma = 10000;

            VirtualArmor = 75;
            ControlSlots = 5;
        }
		
        public override Poison PoisonImmune { get { return Poison.Lethal; } }

        public LightGuardian(Serial serial) : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }
}