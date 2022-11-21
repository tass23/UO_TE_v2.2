using System;
using Server;
using Server.Items;
using Server.Misc;
using Server.Mobiles;
using System.Reflection;

namespace Server.Mobiles
{
	[CorpseName( "a chameleon corpse" )]
	public class Chameleon : BaseCreature
	{
		[Constructable]
		public Chameleon () : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "Chameleon";
			Body = 51;
			BaseSoundID = 362;

			SetStr( 1211, 1385 );
			SetDex( 195, 205 );
			SetInt( 906, 1175 );

			SetHits( 15000 );

			SetDamage( 90, 100 );

			SetDamageType( ResistanceType.Physical, 75 );
			SetDamageType( ResistanceType.Fire, 25 );

			SetResistance( ResistanceType.Physical, 65, 75 );
			SetResistance( ResistanceType.Fire, 80, 90 );
			SetResistance( ResistanceType.Cold, 70, 80 );
			SetResistance( ResistanceType.Poison, 60, 70 );
			SetResistance( ResistanceType.Energy, 60, 70 );

			SetSkill( SkillName.EvalInt, 120.0, 130.0 );
			SetSkill( SkillName.Magery, 120.1, 130.0 );
			SetSkill( SkillName.Meditation, 122.5, 130.0 );
			SetSkill( SkillName.MagicResist, 120.5, 150.0 );
			SetSkill( SkillName.Tactics, 117.6, 130.0 );
			SetSkill( SkillName.Wrestling, 119.6, 130.0 );

			Fame = 22500;
			Karma = -22500;
			Tamable = true;
			ControlSlots = 4;//set the control slots required here
			MinTameSkill = 115.0;//Set min taming skill here
			
			VirtualArmor = 70;
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.UltraRich, 3 );
			AddLoot( LootPack.Gems, 5 );
		}
		
		public override void OnDamage(int amount, Mobile from, bool willKill)
        {
            base.OnDamage(amount, from, willKill);

            if (Hits < 15000)
            {
                TurnIntoAttacker(from);
            }
        }

		public override int GetIdleSound()
		{
			return 0x2D3;
		}

		public override int GetHurtSound()
		{
			return 0x2D1;
		}
		
		public void TurnIntoAttacker(Mobile from)
		{
			if (from == null)
				return;
				
            Map map = Map;

            if (map == null)
                return;

            if (from.Combatant == null)
                return;

            Mobile m = from.Combatant;

            if (m.Body == 51)
                m.Say("Your soul is now mine!");

            if (m.Body != from.Body)
            {
                if (m.Mounted)
                {
                    Item mount = m.FindItemOnLayer(Layer.Mount);
                    mount.Delete();
                }

                m.BoltEffect(0);

                m.Body = from.Body;
                m.Hue = from.Hue;
                
                Warmode = true;

                m.BoltEffect(0);
            }
            switch (Utility.Random(5))
            {

                case 0:
                    m.Say("Your existance shall be mine!!");
                    break;
                case 1:
                    m.Say("Your attacks have no effect on me!");
                    break;
                case 2:
                    m.Say("Your end is near weakling!");
                    break;
                case 3:
                    m.Say("Admit defeat and I will make your end quick!");
                    break;
                case 4:
                    m.Say("You are no match for me!");
                    break;
            }
		}

		public override bool ReacquireOnMovement{ get{ return true; } }
		public override bool HasBreath{ get{ return false; } } // fire breath enabled
		public override bool AutoDispel{ get{ return false; } }
		public override int Meat{ get{ return 2; } }
		public override Poison PoisonImmune{ get{ return Poison.Lethal; } }
		public override int TreasureMapLevel{ get{ return 5; } }
		
		public override void OnSpeech(SpeechEventArgs e)
        {
            if (!e.Handled && e.Mobile == ControlMaster && e.Mobile.InRange(this.Location, 5))//Edit/add what ever color you want below:
            {
	            if (e.Speech == "change help")
                {
                    this.Say("I change by saying change xxxx, i.e change orcbrute...");
                    this.Say("I can change into antlion, solenqueen, solenwarrior,"); 
                    this.Say("solenworker, abyssmalhorror, bonedemon,"); 
                    this.Say("crystalelemental, darknightcreeper, demonknight,"); 
                    this.Say("devourer, fleshgolem, fleshrenderer,"); 
                    this.Say("gibberling, gorefiend, impaler,"); 
                    this.Say("moundofmaggots, patchworkskeleton, ravager,"); 
                    this.Say("shadowknight, skitteringhopper, treefellow,"); 
                    this.Say("vampirebat, wailingbanshee, wandererofthevoid,"); 
                    this.Say("dreadspider, terathranavenger, terathranmatriarch,"); 
                    this.Say("giantspider, terathrandrone, terathranwarrior,"); 
                    this.Say("lich, balron, betrayer,"); 
                    this.Say("bonemagi, daemon, evilmagelord,"); 
                    this.Say("firegargoyle, gargoyle, gazer,"); 
                    this.Say("icefiend, imp, ratmanmage,"); 
                    this.Say("shade, succubus, titan,"); 
                    this.Say("boneknight, chaosdemon, cyclops,"); 
                    this.Say("ettin, gazerlarva, headlessone,"); 
                    this.Say("hordeminion, mongbat, mummy,"); 
                    this.Say("ogre, orc, orcbrute,"); 
                    this.Say("orcishlord, rottingcorpse, skeleton,"); 
                    this.Say("spectralarmor, troll, zombie,"); 
                    this.Say("jukalord, jukamage, jukawarrior,"); 
                    this.Say("meercaptain, meereternal, meermage,"); 
                    this.Say("meerwarrior, etherealwarrior, pixie,"); 
                    this.Say("shadowwisp, bladespirits, centuar,"); 
                    this.Say("energyvortex, golem, plaguebeast,"); 
                    this.Say("bogling, bogthing, whippingvine,"); 
                    this.Say("ancientwyrm, dragon, ophidianmage,"); 
                    this.Say("ophidianmatriarch, serpentinedragon, skeletaldragon,"); 
                    this.Say("harrower, mephitis, semidar,"); 
                    this.Say("silvani, bear, polarbear,"); 
                    this.Say("chicken, eagle, direwolf,"); 
                    this.Say("bull, cow, panther,"); 
                    this.Say("boar, gianttoad, goat,"); 
                    this.Say("gorilla, greathart,"); 
                    this.Say("hind, llama, sheep,"); 
                    this.Say("walrus, beetle, ostard,"); 
                    this.Say("hellsteed, horse, kirin,"); 
                    this.Say("ridgeback, swampdragon, unicorn,"); 
                    this.Say("alligator, giantserpent, snake,"); 
                    this.Say("giantrat, rabbit, rat,"); 
                    this.Say("bird, cat, dog, normal."); 
                    this.Say("Check your journal for everything I just said!");
                }
                if (e.Speech == "change color")
                {
                    this.Say("As you wish!");
                    this.Hue = Utility.Random(2, 1200);//Edit which colors you want him to cycle through here.
                }
                if (e.Speech == "change color normal")
                {
                    this.Say("As you wish!");
                    this.Hue = 0;
                }
	//Start Ant monsters            
		if (e.Speech == "change antlion")
                {
                    this.Say("As you wish!");
                    this.Body = 787;
                }
                if (e.Speech == "change solenqueen")
                {
                    this.Say("As you wish!");
                    this.Body = 807;
                }
                if (e.Speech == "change solenwarrior")
                {
                    this.Say("As you wish!");
                    this.Body = 806;
                }
                if (e.Speech == "change solenworker")
                {
                    this.Say("As you wish!");
                    this.Body = 805;
                }
	//Start AOS monsters
		if (e.Speech == "change abyssmalhorror")
                {
                    this.Say("As you wish!");
                    this.Body = 312;
                }
                if (e.Speech == "change bonedemon")
                {
                    this.Say("As you wish!");
                    this.Body = 308;
                }
                if (e.Speech == "change crystalelemental")
                {
                    this.Say("As you wish!");
                    this.Body = 300;
                }
                if (e.Speech == "change darknightcreeper")
                {
                    this.Say("As you wish!");
                    this.Body = 313;
                }
                if (e.Speech == "change demonknight")
                {
                    this.Say("As you wish!");
                    this.Body = 318;
                }
                if (e.Speech == "change devourer")
                {
                    this.Say("As you wish!");
                    this.Body = 303;
                }
                if (e.Speech == "change fleshgolem")
                {
                    this.Say("As you wish!");
                    this.Body = 304;
                }
		if (e.Speech == "change fleshrenderer")
                {
                    this.Say("As you wish!");
                    this.Body = 315;
                }
                if (e.Speech == "change gibberling")
                {
                    this.Say("As you wish!");
                    this.Body = 307;
                }
                if (e.Speech == "change gorefiend")
                {
                    this.Say("As you wish!");
                    this.Body = 305;
                }
                if (e.Speech == "change impaler")
                {
                    this.Say("As you wish!");
                    this.Body = 306;
                }
                if (e.Speech == "change moundofmaggots")
                {
                    this.Say("As you wish!");
                    this.Body = 319;
                }
                if (e.Speech == "change patchworkskeleton")
                {
                    this.Say("As you wish!");
                    this.Body = 309;
                }
                if (e.Speech == "change ravager")
                {
                    this.Say("As you wish!");
                    this.Body = 314;
                }
                if (e.Speech == "change shadowknight")
                {
                    this.Say("As you wish!");
                    this.Body = 311;
                }
		if (e.Speech == "change skitteringhopper")
                {
                    this.Say("As you wish!");
                    this.Body = 302;
                }
		if (e.Speech == "change treefellow")
                {
                    this.Say("As you wish!");
                    this.Body = 301;
                }
		if (e.Speech == "change vampirebat")
                {
                    this.Say("As you wish!");
                    this.Body = 317;
                }
                if (e.Speech == "change wailingbanshee")
                {
                    this.Say("As you wish!");
                    this.Body = 310;
                }
                if (e.Speech == "change wandererofthevoid")
                {
                    this.Say("As you wish!");
                    this.Body = 316;
                }
	//Start Arachnid monsters
                if (e.Speech == "change dreadspider")
                {
                    this.Say("As you wish!");
                    this.Body = 11;
                }
                if (e.Speech == "change terathranavenger")
                {
                    this.Say("As you wish!");
                    this.Body = 152;
                }
                if (e.Speech == "change terathranmatriarch")
                {
                    this.Say("As you wish!");
                    this.Body = 72;
                }
                if (e.Speech == "giantspider")
                {
                    this.Say("As you wish!");
                    this.Body = 28;
                }
                if (e.Speech == "change terathrandrone")
                {
                    this.Say("As you wish!");
                    this.Body = 71;
                }
		if (e.Speech == "change terathranwarrior")
                {
                    this.Say("As you wish!");
                    this.Body = 70;
                }
	//Start Humanoid Monsters
                if (e.Speech == "change lich")
                {
                    this.Say("As you wish!");
                    this.Body = 24;
                }
                if (e.Speech == "change balron")
                {
                    this.Say("As you wish!");
                    this.Body = 40;
                }
                if (e.Speech == "change betrayer")
                {
                    this.Say("As you wish!");
                    this.Body = 767;
                }
                if (e.Speech == "change bonemagi")
                {
                    this.Say("As you wish!");
                    this.Body = 148;
                }
                if (e.Speech == "change daemon")
                {
                    this.Say("As you wish!");
                    this.Body = 9;
                }
                if (e.Speech == "change evilmagelord")
                {
                    this.Say("As you wish!");
                    this.Body = 126;
                }
                if (e.Speech == "change firegargoyle")
                {
                    this.Say("As you wish!");
                    this.Body = 130;
                }
		if (e.Speech == "change gargoyle")
                {
                    this.Say("As you wish!");
                    this.Body = 4;
                }
                if (e.Speech == "change gazer")
                {
                    this.Say("As you wish!");
                    this.Body = 22;
                }
                if (e.Speech == "change icefiend")
                {
                    this.Say("As you wish!");
                    this.Body = 43;
                }
                if (e.Speech == "change imp")
                {
                    this.Say("As you wish!");
                    this.Body = 74;
                }
                if (e.Speech == "change ratmanmage")
                {
                    this.Say("As you wish!");
                    this.Body = 143;
                }
                if (e.Speech == "change shade")
                {
                    this.Say("As you wish!");
                    this.Body = 26;
                }
                if (e.Speech == "change succubus")
                {
                    this.Say("As you wish!");
                    this.Body = 149;
                }
		if (e.Speech == "change titan")
                {
                    this.Say("As you wish!");
                    this.Body = 76;
                }
                if (e.Speech == "change boneknight")
                {
                    this.Say("As you wish!");
                    this.Body = 57;
                }
                if (e.Speech == "change chaosdemon")
                {
                    this.Say("As you wish!");
                    this.Body = 792;
                }
                if (e.Speech == "change cyclops")
                {
                    this.Say("As you wish!");
                    this.Body = 75;
                }
                if (e.Speech == "change ettin")
                {
                    this.Say("As you wish!");
                    this.Body = 18;
                }
                if (e.Speech == "change gazerlarva")
                {
                    this.Say("As you wish!");
                    this.Body = 778;
                }
                if (e.Speech == "change headlessone")
                {
                    this.Say("As you wish!");
                    this.Body = 31;
                }
                if (e.Speech == "change hordeminion")
                {
                    this.Say("As you wish!");
                    this.Body = 776;
                }
		if (e.Speech == "change mongbat")
                {
                    this.Say("As you wish!");
                    this.Body = 39;
                }
		if (e.Speech == "change mummy")
                {
                    this.Say("As you wish!");
                    this.Body = 154;
                }
		if (e.Speech == "change ogre")
                {
                    this.Say("As you wish!");
                    this.Body = 1;
                }
                if (e.Speech == "change orc")
                {
                    this.Say("As you wish!");
                    this.Body = 17;
                }
                if (e.Speech == "change orcbrute")
                {
                    this.Say("As you wish!");
                    this.Body = 189;
                }
                if (e.Speech == "change orcishlord")
                {
                    this.Say("As you wish!");
                    this.Body = 138;
                }
                if (e.Speech == "change rottingcorpse")
                {
                    this.Say("As you wish!");
                    this.Body = 155;
                }
                if (e.Speech == "change skeleton")
                {
                    this.Say("As you wish!");
                    this.Body = 56;
                }
                if (e.Speech == "spectralarmor")
                {
                    this.Say("As you wish!");
                    this.Body = 637;
                }
                if (e.Speech == "change troll")
                {
                    this.Say("As you wish!");
                    this.Body = 53;
                }
		if (e.Speech == "change zombie")
                {
                    this.Say("As you wish!");
                    this.Body = 3;
                }
	//Start LBR Monsters
                if (e.Speech == "change jukalord")
                {
                    this.Say("As you wish!");
                    this.Body = 766;
                }
                if (e.Speech == "change jukamage")
                {
                    this.Say("As you wish!");
                    this.Body = 765;
                }
                if (e.Speech == "change jukawarrior")
                {
                    this.Say("As you wish!");
                    this.Body = 764;
                }
                if (e.Speech == "change meercaptain")
                {
                    this.Say("As you wish!");
                    this.Body = 773;
                }
                if (e.Speech == "change meereternal")
                {
                    this.Say("As you wish!");
                    this.Body = 772;
                }
                if (e.Speech == "change meermage")
                {
                    this.Say("As you wish!");
                    this.Body = 770;
                }
                if (e.Speech == "change meerwarrior")
                {
                    this.Say("As you wish!");
                    this.Body = 771;
                }
	//Start Misc monsters
		if (e.Speech == "change etherealwarrior")
                {
                    this.Say("As you wish!");
                    this.Body = 123;
                }
                if (e.Speech == "change pixie")
                {
                    this.Say("As you wish!");
                    this.Body = 128;
                }
                if (e.Speech == "change shadowwisp")
                {
                    this.Say("As you wish!");
                    this.Body = 165;
                }
                if (e.Speech == "wisp")
                {
                    this.Say("As you wish!");
                    this.Body = 58;
                }
                if (e.Speech == "change bladespirits")
                {
                    this.Say("As you wish!");
                    this.Body = 574;
                }
		if (e.Speech == "change centaur")
                {
                    this.Say("As you wish!");
                    this.Body = 101;
                }
                if (e.Speech == "change energyvortex")
                {
                    this.Say("As you wish!");
                    this.Body = 164;
                }
                if (e.Speech == "change golem")
                {
                    this.Say("As you wish!");
                    this.Body = 752;
                }
                if (e.Speech == "change plaguebeast")
                {
                    this.Say("As you wish!");
                    this.Body = 775;
                }
	//Start Plant monsters
                if (e.Speech == "change bogling")
                {
                    this.Say("As you wish!");
                    this.Body = 779;
                }
                if (e.Speech == "change bogthing")
                {
                    this.Say("As you wish!");
                    this.Body = 780;
                }
                if (e.Speech == "change whippingvine")
                {
                    this.Say("As you wish!");
                    this.Body = 8;
                }
	//Start Reptile monsters
                if (e.Speech == "change ancientwyrm")
                {
                    this.Say("As you wish!");
                    this.Body = 46;
                }
		if (e.Speech == "change dragon")
                {
                    this.Say("As you wish!");
                    this.Body = 12;
                }
                if (e.Speech == "change ophidianmage")
                {
                    this.Say("As you wish!");
                    this.Body = 85;
                }
                if (e.Speech == "change ophidianmatriarch")
                {
                    this.Say("As you wish!");
                    this.Body = 87;
                }
                if (e.Speech == "serpentinedragon")
                {
                    this.Say("As you wish!");
                    this.Body = 103;
                }
                if (e.Speech == "change skeletaldragon")
                {
                    this.Say("As you wish!");
                    this.Body = 104;
                }
	//Start Special monsters
		if (e.Speech == "change harrower")
                {
                    this.Say("As you wish!");
                    this.Body = 146;
                }
                if (e.Speech == "change mephitis")
                {
                    this.Say("As you wish!");
                    this.Body = 173;
                }
                if (e.Speech == "change semidar")
                {
                    this.Say("As you wish!");
                    this.Body = 174;
                }
                if (e.Speech == "change silvani")
                {
                    this.Say("As you wish!");
                    this.Body = 176;
                }
	//Start Animals
                if (e.Speech == "change bear")
                {
                    this.Say("As you wish!");
                    this.Body = 212;
                }
                if (e.Speech == "change polarbear")
                {
                    this.Say("As you wish!");
                    this.Body = 213;
                }
                if (e.Speech == "change chicken")
                {
                    this.Say("As you wish!");
                    this.Body = 208;
                }
                if (e.Speech == "change eagle")
                {
                    this.Say("As you wish!");
                    this.Body = 5;
                }
		if (e.Speech == "change direwolf")
                {
                    this.Say("As you wish!");
                    this.Body = 23;
                }
                if (e.Speech == "change bull")
                {
                    this.Say("As you wish!");
                    this.Body = 232;
                }
                if (e.Speech == "change cow")
                {
                    this.Say("As you wish!");
                    this.Body = 216;
                }
                if (e.Speech == "change panther")
                {
                    this.Say("As you wish!");
                    this.Body = 214;
                }
		if (e.Speech == "change boar")
                {
                    this.Say("As you wish!");
                    this.Body = 209;
                }
                if (e.Speech == "change gianttoad")
                {
                    this.Say("As you wish!");
                    this.Body = 80;
                }
                if (e.Speech == "change goat")
                {
                    this.Say("As you wish!");
                    this.Body = 209;
                }
                if (e.Speech == "change gorilla")
                {
                    this.Say("As you wish!");
                    this.Body = 29;
                }
		if (e.Speech == "change greathart")
                {
                    this.Say("As you wish!");
                    this.Body = 234;
                }
                if (e.Speech == "change hind")
                {
                    this.Say("As you wish!");
                    this.Body = 237;
                }
                if (e.Speech == "change llama")
                {
                    this.Say("As you wish!");
                    this.Body = 220;
                }
                if (e.Speech == "change sheep")
                {
                    this.Say("As you wish!");
                    this.Body = 207;
                }
		if (e.Speech == "change walrus")
                {
                    this.Say("As you wish!");
                    this.Body = 221;
                }
                if (e.Speech == "change beetle")
                {
                    this.Say("As you wish!");
                    this.Body = 791;
                }
                if (e.Speech == "change ostard")
                {
                    this.Say("As you wish!");
                    this.Body = 210;
                }
                if (e.Speech == "change hellsteed")
                {
                    this.Say("As you wish!");
                    this.Body = 793;
                }
		if (e.Speech == "change horse")
                {
                    this.Say("As you wish!");
                    this.Body = 23;
                }
                if (e.Speech == "change kirin")
                {
                    this.Say("As you wish!");
                    this.Body = 232;
                }
                if (e.Speech == "change ridgeback")
                {
                    this.Say("As you wish!");
                    this.Body = 216;
                }
                if (e.Speech == "change swampdragon")
                {
                    this.Say("As you wish!");
                    this.Body = 214;
                }
		if (e.Speech == "change unicorn")
                {
                    this.Say("As you wish!");
                    this.Body = 122;
                }
                if (e.Speech == "change alligator")
                {
                    this.Say("As you wish!");
                    this.Body = 202;
                }
                if (e.Speech == "change giantserpent")
                {
                    this.Say("As you wish!");
                    this.Body = 21;
                }
                if (e.Speech == "change snake")
                {
                    this.Say("As you wish!");
                    this.Body = 52;
                }
		if (e.Speech == "change giantrat")
                {
                    this.Say("As you wish!");
                    this.Body = 215;
                }
                if (e.Speech == "change rabbit")
                {
                    this.Say("As you wish!");
                    this.Body = 205;
                }
                if (e.Speech == "change rat")
                {
                    this.Say("As you wish!");
                    this.Body = 238;
                }
                if (e.Speech == "change bird")
                {
                    this.Say("As you wish!");
                    this.Body = 6;
                }
		if (e.Speech == "change cat")
                {
                    this.Say("As you wish!");
                    this.Body = 201;
                }
                if (e.Speech == "change dog")
                {
                    this.Say("As you wish!");
                    this.Body = 217;
                }
		if (e.Speech == "change normal")
                {
                    this.Say("As you wish!");
                    this.Body = 51;
                }
                base.OnSpeech(e);
            }
        }

		public Chameleon( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
		
		private static void CopyFromLayer( Mobile from, Mobile mimic, Layer layer ) 
        {
            if (from.FindItemOnLayer(layer) != null)
            {
                Item copy = from.FindItemOnLayer(layer);
                Type t = copy.GetType();

                ConstructorInfo[] info = t.GetConstructors();

                foreach ( ConstructorInfo c in info )
                {
                    ParameterInfo[] paramInfo = c.GetParameters();

                    if ( paramInfo.Length == 0 )
                    {
                        object[] objParams = new object[0];

                        try 
                        {
                            Item newItem=null;
                            object o = c.Invoke( objParams );

                            if ( o != null && o is Item )
                            {
                                newItem = (Item)o;
                                CopyProperties( newItem, copy );//copy.Dupe( item, copy.Amount );
                                newItem.Parent = null;

                                mimic.EquipItem(newItem);
                            }
                                
                            if ( newItem!=null)
                            {
                                /*
                                if ( newItem is BaseWeapon&& o is BaseWeapon)
                                {
                                    BaseWeapon weapon=newItem as BaseWeapon;
                                    BaseWeapon oweapon=o as BaseWeapon;
                                    //weapon.Attributes=oweapon.Attributes;
                                    //weapon.WeaponAttributes=oweapon.WeaponAttributes;
                                    
                                }
                                */
                                if ( newItem is BaseArmor&& o is BaseArmor)
                                {
                                    BaseArmor armor=newItem as BaseArmor;
                                    BaseArmor oarmor=o as BaseArmor;
                                    armor.Attributes=oarmor.Attributes;
                                    armor.ArmorAttributes=oarmor.ArmorAttributes;
                                    armor.SkillBonuses=oarmor.SkillBonuses;
                                }
                                mimic.EquipItem(newItem);
                            }
                        }
                        catch
                        {
                            from.Say( "Error!" );
                            return;
                        }
                    }
                }
            }
            if (mimic.FindItemOnLayer(layer) != null && mimic.FindItemOnLayer(layer).LootType != LootType.Blessed)
                mimic.FindItemOnLayer(layer).LootType = LootType.Newbied;
        
        }
        /*
        private void DupeFromLayer( Mobile from, Mobile mimic, Layer layer ) 
        {
            if (mimic.FindItemOnLayer(layer) != null && mimic.FindItemOnLayer(layer).LootType != LootType.Blessed)
                mimic.FindItemOnLayer(layer).LootType = LootType.Newbied;
        
        }
        */
        private static void CopyProperties ( Item dest, Item src ) 
        { 
            PropertyInfo[] props = src.GetType().GetProperties(); 

            for ( int i = 0; i < props.Length; i++ ) 
            { 
                try
                {
                    if ( props[i].CanRead && props[i].CanWrite )
                    {
                        //Console.WriteLine( "Setting {0} = {1}", props[i].Name, props[i].GetValue( src, null ) );
                        props[i].SetValue( dest, props[i].GetValue( src, null ), null ); 
                    }
                }
                catch
                {
                    //Console.WriteLine( "Denied" );
                }
            }
        }
	}
}
