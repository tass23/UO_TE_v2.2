///////////Made By Admin Maloki////////////////
//////////////Shadows of Death////////////////
/////////////////Vampire Set/////////////////

using System;
using Server;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "a vampire corpse" )]
	public class VampireKing : BaseVampire
	{
		[Constructable]
		public VampireKing () : base( AIType.AI_Mage, FightMode.Aggressor, 10, 1, 0.2, 0.4 )
		{
			if ( this.Female = Utility.RandomBool() )
			{
				Body = 0x191;
				Name = NameList.RandomName( "female" );
				Title = "the Vampire Queen";
				AddItem( new Skirt( 0x7E3 ) );
			}
			else
			{
				Body = 0x190;
				Name = NameList.RandomName( "male" );
				Title = "the Vampire King";
				AddItem( new LongPants( 0x7E3 ) );
			}
			Hue = 0x847E;
			BaseSoundID = 357;
			Rank = 10;
			Criminal = true;

			SetStr( 416, 505 );
			SetDex( 146, 165 );
			SetInt( 566, 655 );
			SetHits( 250, 303 );
			SetDamage( 11, 13 );

			SetDamageType( ResistanceType.Physical, 50 );
			SetDamageType( ResistanceType.Fire, 25 );
			SetDamageType( ResistanceType.Energy, 25 );

			SetResistance( ResistanceType.Physical, 60, 70 );
			SetResistance( ResistanceType.Fire, 60, 70 );
			SetResistance( ResistanceType.Cold, 60, 70 );
			SetResistance( ResistanceType.Poison, 60, 70 );
			SetResistance( ResistanceType.Energy, 60, 70 );

			SetSkill( SkillName.Anatomy, 200, 200 );
			SetSkill( SkillName.EvalInt, 200, 200 );
			SetSkill( SkillName.Magery, 200, 200 );
			SetSkill( SkillName.Meditation, 200, 200 );
			SetSkill( SkillName.MagicResist, 200, 200 );
			SetSkill( SkillName.Tactics, 200, 200 );
			SetSkill( SkillName.Wrestling, 200, 200 );
			SetSkill( SkillName.Swords, 200, 200 );

			Fame = 2400;
			Karma = -2400;
			VirtualArmor = 90;

			int hairHue = 1109;
			Utility.AssignRandomHair( this, hairHue );
			if (!Female) Utility.AssignRandomFacialHair( this, hairHue );
			int clotheshue = 0x7E3;
			AddItem( new Robe( 1194 ) );
			int lowHue = Utility.RandomNeutralHue();
			AddItem( new Boots( lowHue ) );	

			Container pack = new Backpack();
			pack.DropItem( new Gold( 0, 50 ) );
			pack.Movable = false;
			AddItem( pack );
		}

        public override void OnDeath(Container c)
        {
            base.OnDeath(c);
			c.DropItem(new RewardScroll());
        }

		private DateTime LastCast = DateTime.Now;
		private TimeSpan CastDelay = TimeSpan.FromSeconds( 10.0 );

		public bool CanCast()
		{
			if( LastCast.Add( CastDelay ) < DateTime.Now ) return true;
			return false;
		}

		public override void OnDamage( int amount, Mobile from, bool willKill )
		{
			if (Combatant != null && CanCast())
			{
				LastCast = DateTime.Now;
				foreach (Mobile mob in GetMobilesInRange(10))
				{
					if (mob is BaseVampire)
					{
						BaseVampire vamp = (BaseVampire) mob;
						vamp.Combatant = this.Combatant;
					}
				}
			}
			base.OnDamage(amount, from, willKill);
		}

		public VampireKing( Serial serial ) : base( serial )
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
	}
}