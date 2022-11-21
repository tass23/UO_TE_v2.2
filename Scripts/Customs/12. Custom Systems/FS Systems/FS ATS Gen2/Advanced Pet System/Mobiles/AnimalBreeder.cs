using System;
using System.Collections.Generic;
using Server;
using Server.Gumps;
using Server.Items;
using Server.Network;
using Server.Targeting;
using Server.ContextMenus;

namespace Server.Mobiles
{
	public class AnimalBreeder : BaseVendor
	{
		private List<SBInfo> m_SBInfos = new List<SBInfo>();
		protected override List<SBInfo> SBInfos{ get { return m_SBInfos; } }

		[Constructable]
		public AnimalBreeder() : base( "the animal breeder" )
		{
			SetSkill( SkillName.AnimalLore, 64.0, 100.0 );
			SetSkill( SkillName.AnimalTaming, 90.0, 100.0 );
			SetSkill( SkillName.Veterinary, 65.0, 88.0 );
		}

		public override void InitSBInfo()
		{
			m_SBInfos.Add( new SBAnimalTrainer() );
		}

		public override VendorShoeType ShoeType
		{
			get{ return Female ? VendorShoeType.ThighBoots : VendorShoeType.Boots; }
		}

		public override int GetShoeHue()
		{
			return 0;
		}

		public override void InitOutfit()
		{
			base.InitOutfit();

			AddItem( Utility.RandomBool() ? (Item)new QuarterStaff() : (Item)new ShepherdsCrook() );
		}

		public AnimalBreeder( Serial serial ) : base( serial )
		{
		}

		public override bool OnDragDrop( Mobile from, Item dropped )
		{
			if ( dropped is PetClaimTicket )
			{
				PetClaimTicket pct = (PetClaimTicket)dropped;
				if ( pct.Time <= DateTime.Now )
				{
					if ( pct.Pet == null )
					{
						from.SendMessage( "Error! Contact Gamemaster" );
						return false;
					}
					else if ( from.Followers == 0 )
					{
						Type pettype = pct.Pet.GetType();
						BaseCreature bc = (BaseCreature)pct.Pet;

						bc.IsStabled = true;
						from.Stabled.Add( bc );
						this.SayTo( from, "I have put your pet that was mating in the stable under you name." );

						BaseCreature baby = null;
	
						if ( pettype != null )
						{
							object o = Activator.CreateInstance( pettype );
        						baby = o as BaseCreature;
						}

						if ( baby == null )
						{
							from.SendMessage( 38, "There was an internal error and breeding has failed due to lack of type, Please contact a member of the staff." );
							return false;
						}
						else
						{
							if ( from == pct.Owner )
							{
								if ( Banker.Withdraw( from, pct.Price ) )
								{
									from.SendLocalizedMessage( 1060398, pct.Price.ToString() );
									from.SendLocalizedMessage( 1060022, Banker.GetBalance( from ).ToString() );

									baby.Str = pct.Str;
									baby.Dex = pct.Dex;
									baby.Int = pct.Int;
									baby.HitsMaxSeed = pct.Hits;
									baby.StamMaxSeed = pct.Stam;
									baby.ManaMaxSeed = pct.Mana;
									baby.PhysicalResistanceSeed = pct.Phys;
									baby.FireResistSeed = pct.Fire;
									baby.ColdResistSeed = pct.Cold;
									baby.EnergyResistSeed = pct.Nrgy;
									baby.PoisonResistSeed = pct.Pois;
									baby.DamageMin = pct.Dmin;
									baby.DamageMax = pct.Dmax;
									baby.MaxLevel = pct.Mlev;
									baby.Generation = pct.Gen + 1;

        								baby.Controlled = true;
        								baby.ControlMaster = from;
        								baby.Location = from.Location;
									baby.ControlOrder = OrderType.Follow;
									baby.ControlTarget = from;
        								baby.Map = from.Map;
									baby.Name = baby.Name + " baby";

									if ( pct.AI == 1 )
										baby.AI = AIType.AI_Mage;
									else if ( pct.AI == 2 )
										baby.AI = AIType.AI_Melee;

        								World.AddMobile( baby );

									pct.Delete();

									return true;
								}
								else
								{
									this.SayTo( from, "Hey! you tring to cheat me! This ain't for free buddy." );
									from.SendMessage( "You lack the gold in your banking account to do this." );
									return false;
								}
							}
							else
							{
								this.SayTo( from, "You are not the owner of this deed." );
								return false;
							}
						}
					}
					else
					{
						this.SayTo( from, "Please stable or shrink all your pets before we go on." );
						return false;
					}
				}
				else
				{
					if ( pct.Pet != null )
					{
						this.SayTo( from, "Your pet {0} is not done mating yet, Please check back later.", pct.Pet.Name );
						return false;
					}
					else
					{
						from.SendMessage( "Error in your (Pet Claim Ticket) please contact the staff." );
						return false;
					}
				}
			}
			else
			{
				return false;
			}
				
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