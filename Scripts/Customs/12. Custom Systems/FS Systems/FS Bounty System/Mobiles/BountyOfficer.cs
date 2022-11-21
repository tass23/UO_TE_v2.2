using System;
using Server.Items;
using Server.Guilds;
using Server.Mobiles;
using Server.FSBountyHunterSystem;

namespace Server.Mobiles
{
	[CorpseName( "a Bounty Officer's corpse" )]
	public class BountyOfficer : BaseCreature
	{
		[Constructable]
		public BountyOfficer() : base( AIType.AI_Melee, FightMode.Aggressor, 10, 1, 0.2, 0.4 )
		{
			Title = "the bounty officer";
			Hue = Utility.RandomSkinHue();

			if ( Female = Utility.RandomBool() )
			{
				Body = 0x191;
				Name = NameList.RandomName( "female" );

				AddItem( new FemalePlateChest() );
				AddItem( new PlateArms() );
				AddItem( new PlateLegs() );

				switch( Utility.Random( 2 ) )
				{
					case 0: AddItem( new Doublet( Utility.RandomNondyedHue() ) ); break;
					case 1: AddItem( new BodySash( Utility.RandomNondyedHue() ) ); break;
				}

				switch( Utility.Random( 2 ) )
				{
					case 0: AddItem( new Skirt( Utility.RandomNondyedHue() ) ); break;
					case 1: AddItem( new Kilt( Utility.RandomNondyedHue() ) ); break;
				}
			}
			else
			{
				Body = 0x190;
				Name = NameList.RandomName( "male" );

				AddItem( new PlateChest() );
				AddItem( new PlateArms() );
				AddItem( new PlateLegs() );

				switch( Utility.Random( 3 ) )
				{
					case 0: AddItem( new Doublet( Utility.RandomNondyedHue() ) ); break;
					case 1: AddItem( new Tunic( Utility.RandomNondyedHue() ) ); break;
					case 2: AddItem( new BodySash( Utility.RandomNondyedHue() ) ); break;
				}
			}

			SetStr( 1000 );
			SetDex( 1000 );
			SetInt( 1000 );

			SetHits( 10000 );

			SetDamage( 12, 17 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetSkill( SkillName.MagicResist, 5.5 );
			SetSkill( SkillName.Tactics, 5.5 );
			SetSkill( SkillName.Wrestling, 5.5 );

			Fame = 0;
			Karma = 10000;

			VirtualArmor = 80;
		}

		public override bool OnDragDrop( Mobile m, Item item )
		{
			if ( item is Head ) // He said head...
			{
				this.Say( 500670 ); // Ah, a head! Let me check to see if there is a bounty on this.
				this.Say( "There was no bounty on this person." );
				item.Delete();
				return false;	
			}
			else if ( item is BountyHead )
			{
				this.Say( 500670 ); // Ah, a head! Let me check to see if there is a bounty on this.

				BountyHead bh = (BountyHead)item;

				Mobile owner = bh.Owner;
				Mobile killer = bh.Killer;
				DateTime tod = bh.TimeOfDeath;

				FSBountySystem.Bounty b = FSBountySystem.FindBounty( owner );

				if ( b != null )
				{
					if ( owner != null && killer != null )
					{
						if ( m == owner )
						{
							this.Say( "Ha Ha Ha, You think I'm going to give you a reward for your own head?" );
							item.Delete();
							return false;
						}
						else if ( m != killer )
						{
							this.Say( 500543 ); // I had heard this scum was taken care of...but not by you
							item.Delete();
							return false;
						}
						else if ( tod < b.Expires - TimeSpan.FromDays( 30.0 ) )
						{
							this.Say( "This head is to badly decayed to make a positive identification." );
							item.Delete();
							return false;
						}
						else if ( owner.Guild != null && killer.Guild != null && owner.Guild == killer.Guild )
						{
							this.Say( "You cannot claim a reward on one of your guildmates." );
							item.Delete();
							return false;
						}
						else if ( b.Expires < DateTime.Now )
						{
							this.Say( "It looks like this bounty has expired." );
							item.Delete();
							FSBountySystem.ClearBounty( b, owner );
							return false;
						}
						else
						{
							BankBox box = m.BankBox;

							if ( box != null )
							{
								box.DropItem( new BankCheck( b.Reward ) );
								FSBountySystem.ClearBounty( b, owner );
								this.Say( "There was a bounty for {0}, For the sum of {1} gold.", owner.Name, b.Reward );
								m.SendMessage( "A bankcheck for {0} gold has been deposited into your bank.", b.Reward );
								this.Say( "I have added the bounty reward to your bank." );
								item.Delete();
								return true;
							}
							else
							{
								item.Delete();
								return false;
							}
						}
								
					}
					else
					{
						this.Say( "I found a bounty for this person, However the bounty was invalid." );
						m.SendMessage( "The person this head belongs to no longer exists on the server, Contact a gamemaster for more details." );
						FSBountySystem.ClearBounty( b, owner );
						item.Delete();
						return false;
					}
				}
				else
				{
					if ( owner != null )
					{
						this.Say( 1042854, owner.Name ); // There was no bounty on ~1_PLAYER_NAME~.
						item.Delete();
						return false;
					}
					else
					{
						this.Say( "There was no bounty on this person." );
						item.Delete();
						return false;
					}
				}
			}
			else
			{
				this.Say( "I don't want that." );
				return false;
			}
		}

		public BountyOfficer(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write((int) 0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();
		}
	}
}