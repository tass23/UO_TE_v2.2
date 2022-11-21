///////////Made By Admin Maloki////////////////
//////////////Shadows of Death////////////////
/////////////////Vampire Set/////////////////

using System;
using Server;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "a vampire corpse" )]
	public class VampireSummoner : BaseVampire
	{
		[Constructable]
		public VampireSummoner () : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			if ( this.Female = Utility.RandomBool() )
			{
				Body = 0x191;
				Name = NameList.RandomName( "female" );
				AddItem( new Skirt( 1194 ) );
			}
			else
			{
				Body = 0x190;
				Name = NameList.RandomName( "male" );
				AddItem( new ShortPants( 1194 ) );
			}
			Title = "the Vampire Summoner";
			Hue = 0x847E;
			BaseSoundID = 357;
			Rank = 7;
			Criminal = true;

			SetStr( 126, 150 );
			SetDex( 96, 120 );
			SetInt( 151, 175 );
			SetHits( 76, 90 );
			SetDamage( 6, 12 );

			SetDamageType( ResistanceType.Physical, 50 );
			SetDamageType( ResistanceType.Fire, 25 );
			SetDamageType( ResistanceType.Energy, 25 );

			SetResistance( ResistanceType.Physical, 20, 30 );
			SetResistance( ResistanceType.Fire, 70, 80 );
			SetResistance( ResistanceType.Cold, 70, 80 );
			SetResistance( ResistanceType.Poison, 70, 80 );
			SetResistance( ResistanceType.Energy, 70, 80 );

			SetSkill( SkillName.Anatomy, 80, 80 );
			SetSkill( SkillName.EvalInt, 120.1, 150.0 );
			SetSkill( SkillName.Magery, 125.5, 150.0 );
			SetSkill( SkillName.Meditation, 125.1, 150.0 );
			SetSkill( SkillName.MagicResist, 90.5, 95.0 );
			SetSkill( SkillName.Tactics, 80, 80 );
			SetSkill( SkillName.Wrestling, 80, 80 );

			Fame = 2400;
			Karma = -2400;

			VirtualArmor = 90;

			int hairHue = 1109;
			Utility.AssignRandomHair( this, hairHue );
			if (!Female) Utility.AssignRandomFacialHair( this, hairHue );
			int clotheshue = 0x7E3;
			AddItem( new VampireShroud() );
			int lowHue = Utility.RandomNeutralHue();
			AddItem( new Sandals( lowHue ) );
			AddItem( new WizardsHat( clotheshue ) );

			Container pack = new Backpack();
			pack.DropItem( new Gold( 0, 50 ) );
			pack.Movable = false;
			AddItem( pack );
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
			if (Hits < HitsMax ) //* .5) && CanCast())
			{
				LastCast = DateTime.Now;
				foreach (Item item in GetItemsInRange(5))
				{
					if(item is Corpse)
					{
						Corpse cor = (Corpse) item;
						if (cor.ItemID == 0x2006)
						{
							GhostFormVampire gfv = new GhostFormVampire();
							gfv.Body = cor.Amount;
							Effects.PlaySound( cor.Location, cor.Map, 0x1FB );
							Effects.SendLocationParticles( EffectItem.Create( cor.Location, cor.Map, EffectItem.DefaultDuration ), 0x3789, 1, 40, 0x3F, 3, 9907, 0 );
							cor.ProcessDelta();
							cor.SendRemovePacket();
							cor.ItemID = Utility.Random( 0xECA, 9 ); // bone graphic
							cor.Hue = 0;
							cor.ProcessDelta();
							if (Combatant != null) gfv.Combatant = Combatant;
							gfv.MoveToWorld(cor.Location, cor.Map);
						}
					}
				}
			}

			base.OnDamage(amount, from, willKill);
		}

		public VampireSummoner( Serial serial ) : base( serial )
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