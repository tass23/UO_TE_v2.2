using System;
using Server;
using Server.Items;
using Server.Mobiles;
using Server.Targeting;

namespace Server.Mobiles
{
	[CorpseName( "a vampire slayer corpse" )]
	public class VampireSlayer : BaseHire
	{
		public override bool ShowFameTitle{ get{ return false; } }
		public bool BattleCryYelled = false;
		
		[Constructable]
		public VampireSlayer()
		{
            Hue = Utility.RandomSkinHue();
            if ( this.Female = Utility.RandomBool() )
            {
				Body = 0x191;
				Name = NameList.RandomName( "female" );
			}
			else
			{
				Body = 0x190;
				Name = NameList.RandomName( "male" );
			}

			Title = "the Vampire Slayer";
			HairItemID = Race.RandomHair( Female );
			if (Utility.RandomBool()) FacialHairItemID = Race.RandomFacialHair( Female );
			int hhue = Race.RandomHairHue();
			HairHue = hhue;
			FacialHairHue = hhue;
			Criminal = true;

			SetStr( 120 );
			SetDex( 120 );
			SetInt( 10 );
			SetHits( 150 );
			SetDamage( 16, 20 );

			SetDamageType( ResistanceType.Physical, 50 );
			SetDamageType( ResistanceType.Fire, 50 );

			SetResistance( ResistanceType.Physical, 50, 75 );
			SetResistance( ResistanceType.Fire, 25, 50 );
			SetResistance( ResistanceType.Cold, 25, 50 );
			SetResistance( ResistanceType.Poison, 25, 50 );
			SetResistance( ResistanceType.Energy, 25, 50 );

			SetSkill( SkillName.Fencing, 80.0, 100.0 );
			SetSkill( SkillName.Macing, 80.0, 100.0 );
			SetSkill( SkillName.Swords, 80.0, 100.0 );
			SetSkill( SkillName.Tactics, 80.0, 100.0 );
			SetSkill( SkillName.Parry, 80.0, 100.0 );
			SetSkill( SkillName.Wrestling, 80.0, 100.0 );

    		Fame = 250;
    		Karma = 250;

    		FloppyHat hat = new FloppyHat();
			hat.Hue = 1109;
			AddItem( hat );

			Sandals sandals = new Sandals();
			sandals.Hue = 1109;
			AddItem( sandals );

    		//shield ( if they spawn with 1 handed wpn )
    		if ( this.FindItemOnLayer( Layer.TwoHanded ) == null )
    		{
	    		switch( Utility.Random( 3 ) )
	    		{
	    			case 0:
					{
	    				MetalShield shield = new MetalShield();
						shield.Hue = 1109;
						AddItem( shield );
						break;
					}
					case 1:
					{
	    				HeaterShield shield1 = new HeaterShield();
						shield1.Hue = 1109;
						AddItem( shield1 );
						break;
					}
					case 2:
					{
	    				MetalKiteShield shield2 = new MetalKiteShield();
						shield2.Hue = 1109;
						AddItem( shield2 );
						break;
					}
				}
			}
			
			//armor
			StuddedGorget gorget = new StuddedGorget();
			gorget.Hue = 1109;
			AddItem( gorget );
			
			StuddedGloves gloves = new StuddedGloves();
			gloves.Hue = 1109;
			AddItem( gloves );
			
			StuddedArms arms = new StuddedArms();
			arms.Hue = 1109;
			AddItem( arms );
			
			StuddedLegs legs = new StuddedLegs();
			legs.Hue = 1109;
			AddItem( legs );
			
			if( this.Female )
			{
				FemaleStuddedChest fchest = new FemaleStuddedChest();
				fchest.Hue = 1109;
				AddItem( fchest );
			}
			else
			{
				StuddedChest chest = new StuddedChest();
				chest.Hue = 1109;
				AddItem( chest );
			}
			
			Container pack = new Backpack();
			pack.DropItem( new Gold( 0, 50 ) );
			pack.Movable = false;
			AddItem( pack );
    	}
    		
    	public override void AlterMeleeDamageFrom( Mobile from, ref int damage )
		{
			if ( from is BaseVampire )
			{
				damage /= 2;
			}
			if (from is PlayerMobile)
			{
				PlayerMobile pm = (PlayerMobile) from;
				if (pm.Vampire > 0)
				{
					damage /= 2;
				}
			}
		}
		
		public override void AlterMeleeDamageTo( Mobile to, ref int damage )
		{
			if ( to is BaseVampire )
			{
				damage *= 2;
			}
			if (to is PlayerMobile)
			{
				PlayerMobile pm = (PlayerMobile) to;
				if (pm.Vampire > 0)
				{
					damage *= 2;
				}
			}
		}
    		
    	public override void AggressiveAction( Mobile aggressor, bool criminal )
		{
			if ( aggressor is BaseVampire )
			{
				this.Combatant = aggressor;
				AIObject.Action = ActionType.Combat;
				
				if ( BattleCryYelled == false )
				{
					this.Say( "Die vampire!" );
					BattleCryYelled = true;
				}
			}
		}
    		
    	public override void OnActionCombat()
    	{
    		Mobile combatant = this.Combatant;

    		if ( combatant is BaseVampire )
    		{
    			if ( BattleCryYelled == false )
    			{
    				this.Say( "Die vampire" );
    				BattleCryYelled = true;
    			}

    			if ( ((Mobiles.BaseVampire)combatant).Hidden == true )
    			{
    				int skill = Utility.Random( 80, 100 );
    				if ( skill > Utility.Random( 100 ) )
    				{
    					((Mobiles.BaseVampire)combatant).Hidden = false;
    				}
    			}
			}
    	}

    	public VampireSlayer( Serial serial ) : base( serial )
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
}