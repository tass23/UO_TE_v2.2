using System;
using System.Collections;
using Server.Items;
using Server.Targeting;
using Server.Gumps;
using Server.SkillHandlers;
using Server.ContextMenus;
using System.Collections.Generic;

namespace Server.Mobiles
{

	[CorpseName( "a corpse" )]
	public class BaseBioCreature : BaseCreature
	{
		private Mobile m_Craftedby;
		private bool m_HasDNA;

		//Counter Attacks
		private bool m_BluntAttack;
		private bool m_Healing;
		private bool m_PoisonAttack;

		//Speical Attacks
		private bool m_TrialByFire;
		private bool m_IceBlast;
		private bool m_CometAttack;
		private bool m_CallOfNature;
		private bool m_AcidRain;

		[CommandProperty( AccessLevel.GameMaster )]
		public Mobile Crafter
		{ 
			get{ return m_Craftedby; } 
			set{ m_Craftedby = value; } 
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public bool HasDNA
		{ 
			get{ return m_HasDNA; } 
			set{ m_HasDNA = value; } 
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public bool TrialByFire
		{ 
			get{ return m_TrialByFire; } 
			set{ m_TrialByFire = value; } 
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public bool IceBlast
		{ 
			get{ return m_IceBlast; } 
			set{ m_IceBlast = value; } 
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public bool CometAttack
		{ 
			get{ return m_CometAttack; } 
			set{ m_CometAttack = value; } 
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public bool CallOfNature
		{ 
			get{ return m_CallOfNature; } 
			set{ m_CallOfNature = value; } 
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public bool AcidRain
		{ 
			get{ return m_AcidRain; } 
			set{ m_AcidRain = value; } 
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public bool BluntAttack
		{ 
			get{ return m_BluntAttack; } 
			set{ m_BluntAttack = value; } 
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public bool Healing
		{ 
			get{ return m_Healing; } 
			set{ m_Healing = value; } 
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public bool PoisonAttack
		{ 
			get{ return m_PoisonAttack; } 
			set{ m_PoisonAttack = value; } 
		}

		public override WeaponAbility GetWeaponAbility()
		{
			switch ( Utility.Random( 3 ) )
			{
				default:
				case 0: return WeaponAbility.DoubleStrike;
				case 1: return WeaponAbility.WhirlwindAttack;
			}
		}

		[Constructable]
		public BaseBioCreature() : base( AIType.AI_Animal, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a bio-engineered clone";
			Body = 775;

			SetStr( 1 );
			SetDex( 1 );
			SetInt( 1 );

			SetSkill( SkillName.MagicResist, 25.0 );
			SetSkill( SkillName.Tactics, 25.0 );
			SetSkill( SkillName.Wrestling, 25.0 );
			SetSkill( SkillName.Meditation, 25.0 );
			SetSkill( SkillName.Magery, 25.0 );
			SetSkill( SkillName.EvalInt, 25.0 );
			SetSkill( SkillName.Poisoning, 25.0 );

			Fame = 0;
			Karma = 0;

			VirtualArmor = 1;

			Tamable = true;
			ControlSlots = 3;
			MinTameSkill = 100.0;

			Container pack = Backpack;

			if ( pack != null )
				pack.Delete();

			pack = new StrongBackpack();
			pack.Movable = false;

			AddItem( pack );
		}

		public void CheckResistances()
		{
			if ( this.BodyValue == 400 || this.BodyValue == 401 )
			{
				if ( this.PhysicalResistanceSeed >= 0 )
					this.PhysicalResistanceSeed = 0;

				if ( this.FireResistSeed >= 0 )
					this.FireResistSeed = 0;

				if ( this.ColdResistSeed >= 0 )
					this.ColdResistSeed = 0;

				if ( this.PoisonResistSeed >= 0 )
					this.PoisonResistSeed = 0;

				if ( this.EnergyResistSeed >= 0 )
					this.EnergyResistSeed = 0;

				if ( this.PhysicalResistanceSeed <= 0 )
					this.PhysicalResistanceSeed = 0;

				if ( this.FireResistSeed <= 0 )
					this.FireResistSeed = 0;

				if ( this.ColdResistSeed <= 0 )
					this.ColdResistSeed = 0;

				if ( this.PoisonResistSeed <= 0 )
					this.PoisonResistSeed = 0;

				if ( this.EnergyResistSeed <= 0 )
					this.EnergyResistSeed = 0;

				if ( this.PhysicalResistance >= 70 )
				{
					int neg = this.PhysicalResistance - 70;
					this.PhysicalResistanceSeed -= neg;
				}

				if ( this.FireResistance >= 70 )
				{
					int neg = this.FireResistance - 70;
					this.FireResistSeed -= neg;
				}

				if ( this.ColdResistance >= 70 )
				{
					int neg = this.ColdResistance - 70;
					this.ColdResistSeed -= neg;
				}

				if ( this.PoisonResistance >= 70 )
				{
					int neg = this.PoisonResistance - 70;
					this.PoisonResistSeed -= neg;
				}

				if ( this.EnergyResistance >= 70 )
				{
					int neg = this.EnergyResistance - 70;
					this.EnergyResistSeed -= neg;
				}
			}
		}

		public override bool OnDragDrop( Mobile from, Item item )
		{
			if ( CheckFeed( from, item ) )
				return true;

			if ( this.BodyValue == 400 || this.BodyValue == 401 && from == this.ControlMaster )
			{
				if ( item is BaseClothing && from == this.ControlMaster )
				{
					BaseClothing bc = (BaseClothing)item;
					Item equ = this.FindItemOnLayer( bc.Layer );
					
					if ( bc.LootType == LootType.Blessed || bc.LootType == LootType.Newbied )
						from.SendMessage( 53, "WARNING!!! If your pet dies while holding a blessed, insured, or newbied item. It will be lost forever." );

					if ( equ != null)
						from.AddToBackpack( equ );

					this.EquipItem( bc );
					this.CheckResistances();

					return true;
					
				}
				else if ( item is BaseArmor && from == this.ControlMaster )
				{
					BaseArmor ba = (BaseArmor)item;

					Item equ = this.FindItemOnLayer( ba.Layer );

					if ( ba.LootType == LootType.Blessed || ba.LootType == LootType.Newbied )
						from.SendMessage( 53, "WARNING!!! If your pet dies while holding a blessed, insured, or newbied item. It will be lost forever." );

					if ( equ != null)
						from.AddToBackpack( equ );

					if ( this.Str <= ba.StrRequirement )
					{
						from.SendMessage( "Your pet is not strong enough to wear this." );
						from.AddToBackpack( ba );
					}
					else
					{
						this.EquipItem( ba );
						this.CheckResistances();
					}

					return true;
				}
				else if ( item is BaseWeapon && from == this.ControlMaster )
				{
					BaseWeapon bw = (BaseWeapon)item;

					Item weap = this.FindItemOnLayer( Layer.OneHanded );
					Item shield = this.FindItemOnLayer( Layer.TwoHanded );

					if ( bw.LootType == LootType.Blessed || bw.LootType == LootType.Newbied )
						from.SendMessage( 53, "WARNING!!! If your pet dies while holding a blessed, insured, or newbied item. It will be lost forever." );

					if ( weap != null)
						from.AddToBackpack( weap );

					if ( shield != null)
						from.AddToBackpack( shield );

					if ( this.Str <= bw.StrRequirement )
					{
						from.SendMessage( "Your pet is not strong enough to wear this." );
						from.AddToBackpack( bw );
					}
					else
					{
						this.EquipItem( bw );
						this.CheckResistances();
					}

					this.EquipItem( bw );
					
					return true;
				}
				else if ( item is BaseJewel && from == this.ControlMaster )
				{
					BaseJewel bj = (BaseJewel)item;

					Item equ = this.FindItemOnLayer( bj.Layer );

					if ( bj.LootType == LootType.Blessed || bj.LootType == LootType.Newbied )
						from.SendMessage( 53, "Warning: The item you have droped on your pet is either blessed or newbied. If this pet is unbonded and dies you will lose this item." );

					if ( equ != null)
						from.AddToBackpack( equ );

					this.EquipItem( bj );
					this.CheckResistances();
	
					return true;
				}
				else if ( item is Lantern && from == this.ControlMaster )
				{
					Lantern l = (Lantern)item;

					Item equ = this.FindItemOnLayer( l.Layer );

					if ( l.LootType == LootType.Blessed || l.LootType == LootType.Newbied )
						from.SendMessage( 53, "Warning: The item you have droped on your pet is either blessed or newbied. If this pet is unbonded and dies you will lose this item." );

					if ( equ != null)
						from.AddToBackpack( equ );

					this.EquipItem( l );
					this.CheckResistances();
	
					return true;
				}
				else if ( item is FishingPole && from == this.ControlMaster )
				{
					FishingPole fp = (FishingPole)item;

					Item equ = this.FindItemOnLayer( fp.Layer );

					if ( fp.LootType == LootType.Blessed || fp.LootType == LootType.Newbied )
						from.SendMessage( 53, "Warning: The item you have droped on your pet is either blessed or newbied. If this pet is unbonded and dies you will lose this item." );

					if ( equ != null)
						from.AddToBackpack( equ );

					this.EquipItem( fp );
					this.CheckResistances();
	
					return true;
				}
				else if ( item is Torch && from == this.ControlMaster )
				{
					Torch t = (Torch)item;

					Item equ = this.FindItemOnLayer( t.Layer );

					if ( t.LootType == LootType.Blessed || t.LootType == LootType.Newbied )
						from.SendMessage( 53, "Warning: The item you have droped on your pet is either blessed or newbied. If this pet is unbonded and dies you will lose this item." );

					if ( equ != null)
						from.AddToBackpack( equ );

					this.EquipItem( t );
					this.CheckResistances();
	
					return true;
				}
				else
				{
					if ( PackAnimal.CheckAccess( this, from ) )
					{
						AddToBackpack( item );
						return true;
					}
				}
			}
			else
			{
				if ( PackAnimal.CheckAccess( this, from ) )
				{
					AddToBackpack( item );
					return true;
				}
			}

			return base.OnDragDrop( from, item );
		}

		public override void OnThink()
		{
			base.OnThink();

			if ( this.Controlled == false )
			{
				this.Delete();
			}

			this.CheckResistances();
		}

		public override void AddNameProperties( ObjectPropertyList list )
		{
			base.AddNameProperties( list );

			if ( m_Craftedby != null )
				list.Add( 1060658, "Bio-engineered by\t{0}", m_Craftedby.Name.ToString() );
		}

		public override void OnSpeech(SpeechEventArgs e)
		{
			if ( e.Speech.ToLower().IndexOf( "status" ) > -1 )
			{
				e.Handled = true;
				Mobile m = (Mobile)e.Mobile;
				if ( m == this.ControlMaster )
				{
					m.SendGump( new AnimalLoreGump( this, m ) );
				}
				else
				{
					if ( this.BodyValue == 400 || this.BodyValue == 401  )
					{
						this.SayTo( m, "You are not my master" );
					}
					else
					{
						this.SayTo( m, "*looks confused*" );
					}
				}
				
			}

			if ( e.Speech.ToLower().IndexOf( "skills" ) > -1 )
			{
				e.Handled = true;
				Mobile m = (Mobile)e.Mobile;
				if ( m == this.ControlMaster )
				{
					m.SendGump( new PetSkillsGump( this ) );
				}
				else
				{
					if ( this.BodyValue == 400 || this.BodyValue == 401  )
					{
						this.SayTo( m, "You are not my master" );
					}
					else
					{
						this.SayTo( m, "*looks confused*" );
					}
				}
				
			}

			if ( e.Speech.ToLower().IndexOf( "ride" ) > -1 )
			{
				e.Handled = true;
				Mobile m = (Mobile)e.Mobile;
				if ( m == this.ControlMaster )
				{
					if ( this.BodyValue == 400 || this.BodyValue == 401  )
					{
						if ( this.Mounted == true )
						{
							this.SayTo( m, "I am already mounted" );
						}
						else
						{
							m.Target = new MountTarget( this, m );
							m.SendMessage( "Select the mount you wish your bio human to ride" );
						}
					}
					else
					{
						this.SayTo( m, "*looks confused*" );
					}
				}
				else
				{
					if ( this.BodyValue == 400 || this.BodyValue == 401  )
					{
						this.SayTo( m, "You are not my master" );
					}
					else
					{
						this.SayTo( m, "*looks confused*" );
					}
				}
				
			}

			if ( e.Speech.ToLower().IndexOf( "dismount" ) > -1 )
			{
				e.Handled = true;
				Mobile m = (Mobile)e.Mobile;
				if ( m == this.ControlMaster )
				{
					if ( this.BodyValue == 400 || this.BodyValue == 401 )
					{
						if ( this.Mounted == true )
						{
							IMount mount = this.Mount;
							if ( mount != null )
								mount.Rider = null;
						}
						else
						{
							this.SayTo( m, "I am not mounted." );
						}

					}
					else
					{
						this.SayTo( m, "*looks confused*" );
					}
				}
				else
				{
					if ( this.BodyValue == 400 || this.BodyValue == 401  )
					{
						this.SayTo( m, "You are not my master" );
					}
					else
					{
						this.SayTo( m, "*looks confused*" );
					}
				}
				
			}

			if ( e.Speech.ToLower().IndexOf( "morph" ) > -1 )
			{
				e.Handled = true;
				Mobile m = (Mobile)e.Mobile;
				if ( m == this.ControlMaster )
				{
					if ( this.BodyValue == 400 || this.BodyValue == 401 || this.BodyValue == 469 || this.BodyValue == 447 )
					{
						if ( this.BodyValue == 400 )
						{
							if ( this.Mounted == true )
							{
								IMount mount = this.Mount;
								if ( mount != null )
									mount.Rider = null;
							}

							this.BodyValue = 469;
						}
						else if ( this.BodyValue == 401 )
						{
							if ( this.Mounted == true )
							{
								IMount mount = this.Mount;
								if ( mount != null )
									mount.Rider = null;
							}

							this.BodyValue = 447;
						}
						else if ( this.BodyValue == 469 )
						{
							this.BodyValue = 400;
						}
						else if ( this.BodyValue == 447 )
						{
							this.BodyValue = 401;
						}
					
						m.SendMessage( "Your pet has morphed" );
					}
					else
					{
						this.SayTo( m, "*looks confused*" );
					}
				}
				else
				{
					if ( this.BodyValue == 400 || this.BodyValue == 401  )
					{
						this.SayTo( m, "You are not my master" );
					}
					else
					{
						this.SayTo( m, "*looks confused*" );
					}
				}
				
			}

			if ( e.Speech.ToLower().IndexOf( "undress" ) > -1 )
			{
				e.Handled = true;
				Mobile m = (Mobile)e.Mobile;
				if ( m == this.ControlMaster )
				{
					if ( this.BodyValue == 400 || this.BodyValue == 401 )
					{
						Item one = this.FindItemOnLayer( Layer.OneHanded );
						Item two = this.FindItemOnLayer( Layer.TwoHanded );
						Item shoes = this.FindItemOnLayer( Layer.Shoes );
						Item pants = this.FindItemOnLayer( Layer.Pants );
						Item shirt = this.FindItemOnLayer( Layer.Shirt );
						Item helm = this.FindItemOnLayer( Layer.Helm );
						Item gloves = this.FindItemOnLayer( Layer.Gloves );
						Item ring = this.FindItemOnLayer( Layer.Ring );
						Item neck = this.FindItemOnLayer( Layer.Neck );
						Item waist = this.FindItemOnLayer( Layer.Waist );
						Item innert = this.FindItemOnLayer( Layer.InnerTorso );
						Item bracelet = this.FindItemOnLayer( Layer.Bracelet );
						Item middlet = this.FindItemOnLayer( Layer.MiddleTorso );
						Item earrings = this.FindItemOnLayer( Layer.Earrings );
						Item arms = this.FindItemOnLayer( Layer.Arms );
						Item cloak = this.FindItemOnLayer( Layer.Cloak );
						Item outtert = this.FindItemOnLayer( Layer.OuterTorso );
						Item outterl = this.FindItemOnLayer( Layer.OuterLegs );
						Item innerl = this.FindItemOnLayer( Layer.InnerLegs );

						if ( one == null && two == null && shoes == null && pants == null && shirt == null && helm == null && gloves == null && ring == null && neck == null && waist == null && innert == null && bracelet == null && middlet == null && earrings == null && arms == null && cloak == null && outtert == null && outterl == null && innerl == null )
						{
							this.SayTo( m, "I am already naked." );
						}
						else
						{
							if ( one != null)
								this.AddToBackpack( one );

							if ( two != null)
								this.AddToBackpack( two );

							if ( shoes != null)
								this.AddToBackpack( shoes );

							if ( pants != null)
								this.AddToBackpack( pants );

							if ( shirt != null)
								this.AddToBackpack( shirt );

							if ( helm != null)
								this.AddToBackpack( helm );

							if ( gloves != null)
								this.AddToBackpack( gloves );

							if ( ring != null)
								this.AddToBackpack( ring );

							if ( neck != null)
								this.AddToBackpack( neck );

							if ( waist != null)
								this.AddToBackpack( waist );

							if ( innert != null)
								this.AddToBackpack( innert );

							if ( bracelet != null)
								this.AddToBackpack( bracelet );

							if ( middlet != null)
								this.AddToBackpack( middlet );
	
							if ( earrings != null)
								this.AddToBackpack( earrings );

							if ( arms != null)
								this.AddToBackpack( arms );
	
							if ( cloak != null)
								this.AddToBackpack( cloak );

							if ( outtert != null)
								this.AddToBackpack( outtert );

							if ( outterl != null)
								this.AddToBackpack( outterl );

							if ( innerl != null)
								this.AddToBackpack( innerl );

							this.SayTo( m, "Ok if you insist." );
							this.SayTo( m, "Everything i was wearing is in my backpack." );

							this.CheckResistances();
						}
					}
					else
					{
						this.SayTo( m, "*looks confused*" );
					}
				}
				else
				{
					if ( this.BodyValue == 400 || this.BodyValue == 401  )
					{
						this.SayTo( m, "You are not my master" );
					}
					else
					{
						this.SayTo( m, "*looks confused*" );
					}
				}
			}

			base.OnSpeech (e);
		}

		public override bool AutoDispel{ get{ return true; } }
		public override Poison HitPoison{ get{ return (0.8 >= Utility.RandomDouble() ? Poison.Lesser : Poison.Regular); } }
		public override FoodType FavoriteFood{ get{ return FoodType.Fish | FoodType.FruitsAndVegies | FoodType.Meat; } }

		#region Pack Animal Methods
		public override bool OnBeforeDeath()
		{
			if ( !base.OnBeforeDeath() )
				return false;

			PackAnimal.CombineBackpacks( this );

			return true;
		}

		public override bool IsSnoop( Mobile from )
		{
			if ( PackAnimal.CheckAccess( this, from ) )
				return false;

			return base.IsSnoop( from );
		}

		public override bool CheckNonlocalDrop( Mobile from, Item item, Item target )
		{
			return PackAnimal.CheckAccess( this, from );
		}

		public override bool CheckNonlocalLift( Mobile from, Item item )
		{
			return PackAnimal.CheckAccess( this, from );
		}

		public override void GetContextMenuEntries( Mobile from, List<ContextMenuEntry> list )
		{
			base.GetContextMenuEntries( from, list );

			PackAnimal.GetContextMenuEntries( this, from, list );
		}
		#endregion

		public void DoCure()
		{
			if ( this.BodyValue == 400 || this.BodyValue == 401 && this.Backpack != null )
			{
				if ( this == null || this.IsDeadPet )
				{
				}
				else if ( this.ControlMaster != null )
				{
					if ( Utility.Random( 100 ) < 45 )
					{
						Item[] Bandage = this.Backpack.FindItemsByType( typeof( Bandage ) );
						Item[] BaseCurePotion = this.Backpack.FindItemsByType( typeof( BaseCurePotion ) );
		   				if ( this.Backpack.ConsumeTotal( typeof( Bandage ), 1 ) )
						{
							this.PlaySound( 0x57 );
							this.Poison = null;
						}
		   				else if ( this.Backpack.ConsumeTotal( typeof( BaseCurePotion ), 1 ) )
						{
							this.Poison = null;
							BasePotion.PlayDrinkEffect( this );
							this.FixedParticles( 0x373A, 10, 15, 5012, EffectLayer.Waist );
							this.PlaySound( 0x1E0 );
						}
					}
				}	
			}
		}

		public void DoHeal()
		{
			if ( this.BodyValue == 400 || this.BodyValue == 401 && this.Backpack != null )
			{
				if ( this == null || this.IsDeadPet )
				{
				}
				else if ( this.ControlMaster != null )
				{
					if ( Utility.Random( 100 ) < 45 )
					{
						if ( this.Poisoned || MortalStrike.IsWounded( this ) )
						{
							this.ControlMaster.SendMessage( "Your pet attempted to heal its self but could not due to its current status." );
						}
						else
						{
							Item[] Bandage = this.Backpack.FindItemsByType( typeof( Bandage ) );
							Item[] BaseHealPotion = this.Backpack.FindItemsByType( typeof( BaseHealPotion ) );
		   					if ( this.Backpack.ConsumeTotal( typeof( Bandage ), 1 ) )
							{
								this.PlaySound( 0x57 );
								this.Heal( Utility.RandomMinMax( 25, 65 ) );
							}
		   					else if ( this.Backpack.ConsumeTotal( typeof( BaseHealPotion ), 1 ) )
							{
								BasePotion.PlayDrinkEffect( this );
								this.Heal( Utility.RandomMinMax( 25, 65 ) );
							}
						}
					}
				}
			}
		}

		public override void OnActionCombat()
		{
			int damagemin = this.Hits / 20 + 25;
			int damagemax = this.Hits / 25 + 50;
			Mobile from = this.Combatant;

			if ( from != null && this.ControlMaster != null )
			{

				if ( m_TrialByFire == true )
				{
					if( Utility.Random( 2500 ) < 5 ) 
					{
						if ( this.Stam <= 50 )
						{
							if ( this.ControlMaster != null )
								this.ControlMaster.SendMessage( "You pet lacks the stamina to preform its power attack." );
						}
						else
						{
							this.Stam -= 50;

							Effects.SendMovingParticles( new Entity( Serial.Zero, new Point3D( from.X - 6, from.Y - 6, from.Z + 15 ), from.Map ), from, 0x36D4, 7, 0, false, true, 0, 0, 9502, 1, 0, (EffectLayer)255, 0x100 );
							Effects.SendMovingParticles( new Entity( Serial.Zero, new Point3D( from.X - 4, from.Y - 6, from.Z + 15 ), from.Map ), from, 0x36D4, 7, 0, false, true, 0, 0, 9502, 1, 0, (EffectLayer)255, 0x100 );
							Effects.SendMovingParticles( new Entity( Serial.Zero, new Point3D( from.X - 6, from.Y - 4, from.Z + 15 ), from.Map ), from, 0x36D4, 7, 0, false, true, 0, 0, 9502, 1, 0, (EffectLayer)255, 0x100 );
	
							Effects.SendMovingParticles( new Entity( Serial.Zero, new Point3D( from.X - 7, from.Y, from.Z ), from.Map ), from, 0x36E4, 7, 0, false, true, 0, 0, 9502, 1, 0, (EffectLayer)255, 0x100 );
							Effects.SendMovingParticles( new Entity( Serial.Zero, new Point3D( from.X + 7, from.Y, from.Z ), from.Map ), from, 0x36E4, 7, 0, false, true, 0, 0, 9502, 1, 0, (EffectLayer)255, 0x100 );
							Effects.SendMovingParticles( new Entity( Serial.Zero, new Point3D( from.X, from.Y - 7, from.Z ), from.Map ), from, 0x36E4, 7, 0, false, true, 0, 0, 9502, 1, 0, (EffectLayer)255, 0x100 );
							Effects.SendMovingParticles( new Entity( Serial.Zero, new Point3D( from.X, from.Y + 7, from.Z ), from.Map ), from, 0x36E4, 7, 0, false, true, 0, 0, 9502, 1, 0, (EffectLayer)255, 0x100 );

							from.FixedParticles( 0x3709, 10, 15, 5012, EffectLayer.Waist );
							from.FixedParticles( 0x36B0, 10, 15, 5012, EffectLayer.Waist );
							PlaySound( 776 );
							PlaySound( 477 );

							AOS.Damage( from, this, Utility.RandomMinMax( 30, 60 ), 0, 100, 0, 0, 0 );
							AOS.Damage( from, this, Utility.RandomMinMax( 10, 30 ), 0, 100, 0, 0, 0 );
							AOS.Damage( from, this, Utility.RandomMinMax( 5, 10 ), 0, 100, 0, 0, 0 );

							this.Emote( "[Power Attack]" );
							this.Emote( "[Trail By Fire]" );
						}
					}
				}

				if ( m_IceBlast == true )
				{
					Mobile cm = this.ControlMaster;
					if( Utility.Random( 2500 ) < 5 ) 
					{
						if ( this.Stam <= 50 )
						{
							if ( this.ControlMaster != null )
								this.ControlMaster.SendMessage( "You pet lacks the stamina to preform its power attack." );
						}
						else
						{
							this.Stam -= 50;

							Effects.SendMovingParticles( new Entity( Serial.Zero, new Point3D( from.X - 6, from.Y - 6, from.Z + 15 ), from.Map ), from, 0x36D4, 7, 0, false, true, 1152, 0, 9502, 1, 0, (EffectLayer)255, 0x100 );
							Effects.SendMovingParticles( new Entity( Serial.Zero, new Point3D( from.X - 4, from.Y - 6, from.Z + 15 ), from.Map ), from, 0x36D4, 7, 0, false, true, 1152, 0, 9502, 1, 0, (EffectLayer)255, 0x100 );
							Effects.SendMovingParticles( new Entity( Serial.Zero, new Point3D( from.X - 6, from.Y - 4, from.Z + 15 ), from.Map ), from, 0x36D4, 7, 0, false, true, 1152, 0, 9502, 1, 0, (EffectLayer)255, 0x100 );

							AOS.Damage( from, this, Utility.RandomMinMax( 20, 30 ), 0, 0, 100, 0, 0 );

        						IceFragments ice = new IceFragments();
        						ice.Location = from.Location;
        						ice.Map = from.Map;
							ice.PetOwner = cm;
							ice.Pet = this;
        						World.AddItem( ice );

        						IceSnow ices = new IceSnow();
							ices.X = from.X + Utility.RandomMinMax( 3, 5 );
							ices.Y = from.Y + Utility.RandomMinMax( 3, 5 );
							ices.Z = from.Z;
        						ices.Map = from.Map;
        						World.AddItem( ices );

        						IceSnow2 is2 = new IceSnow2();
							is2.X = from.X - Utility.RandomMinMax( 3, 5 );
							is2.Y = from.Y - Utility.RandomMinMax( 3, 5 );
							is2.Z = from.Z;
        						is2.Map = from.Map;
        						World.AddItem( is2 );

        						IceSnow3 is3 = new IceSnow3();
							is3.X = from.X - Utility.RandomMinMax( 3, 5 );
							is3.Y = from.Y + Utility.RandomMinMax( 3, 5 );
							is3.Z = from.Z;
        						is3.Map = from.Map;
        						World.AddItem( is3 );

        						IceSnow is4 = new IceSnow();
							is4.X = from.X + Utility.RandomMinMax( 3, 5 );
							is4.Y = from.Y - Utility.RandomMinMax( 3, 5 );
							is4.Z = from.Z;
        						is4.Map = from.Map;
        						World.AddItem( is4 );

							PlaySound( 22 );

							this.Emote( "[Power Attack]" );
							this.Emote( "[Ice Blast]" );
						}
					}
				}
				else if ( m_CometAttack == true )
				{
					if( Utility.Random( 2500 ) < 5 ) 
					{
						if ( this.Stam <= 50 )
						{
							if ( this.ControlMaster != null )
								this.ControlMaster.SendMessage( "You pet lacks the stamina to preform its power attack." );
						}
						else
						{
							this.Stam -= 50;

							Effects.SendMovingParticles( new Entity( Serial.Zero, new Point3D( from.X, from.Y, from.Z + 35 ), from.Map ), from, 0x11B6, 7, 0, false, true, 1160, 0, 9502, 1, 0, (EffectLayer)255, 0x100 );

							from.FixedParticles( 0x3709, 10, 15, 5012, EffectLayer.Waist );
							from.FixedParticles( 0x36B0, 10, 15, 5012, EffectLayer.Waist );
							PlaySound( 551 );

							ArrayList targets = new ArrayList();

							foreach ( Mobile m in this.GetMobilesInRange( 15 ) )
							{
								if ( this != m && this.ControlMaster != m )
									targets.Add( m );
							}
								
							for ( int i = 0; i < targets.Count; ++i )
							{
								Mobile m = (Mobile)targets[i];

								if ( m != null )
								{
									if ( m is BaseVendor || m is PlayerVendor || m.Blessed != false )
									{
										m.Say( "What was that?" );
									}
									else
									{
										if ( m is PlayerMobile )
										{
											PlayerMobile pm = (PlayerMobile)m;
											if ( pm.Combatant == this || pm.Combatant == this.ControlMaster || this.Combatant == pm || this.ControlMaster.Combatant == pm )
											{
												AOS.Damage( m, this, Utility.RandomMinMax( 30, 60 ), 0, 100, 0, 0, 0 );

												m.SendMessage( "You have been hit by a shock wave." );
												this.ControlMaster.DoHarmful( m );
											}
										}
										else if ( m is BaseCreature )
										{
											BaseCreature bc = (BaseCreature)m;
											if ( bc.Controlled == false && bc.ControlMaster == null )
											{
												AOS.Damage( m, this, Utility.RandomMinMax( 30, 60 ), 25, 50, 0, 25, 0 );
												this.ControlMaster.DoHarmful( m );
											}
											else if ( bc.Combatant == this || bc.Combatant == this.ControlMaster || this.Combatant == bc || this.ControlMaster.Combatant == bc )
											{
												AOS.Damage( m, this, Utility.RandomMinMax( 30, 60 ), 0, 100, 0, 0, 0 );

												bc.ControlMaster.SendMessage( "Your pet has been hit by a shock wave." );
												this.ControlMaster.DoHarmful( m );
											}
										}
									}
								}
							}
						}
						this.Emote( "[Power Attack]" );
						this.Emote( "[Comet Strike]" );
					}
				}
				else if ( m_CallOfNature == true )
				{
					if( Utility.Random( 2500 ) < 5 ) 
					{
						if ( this.Stam <= 50 )
						{
							if ( this.ControlMaster != null )
								this.ControlMaster.SendMessage( "You pet lacks the stamina to preform its power attack." );
						}
						else
						{
							this.Stam -= 50;

							PlaySound( 4 );

							ArrayList targets = new ArrayList();

							foreach ( Mobile m in this.GetMobilesInRange( 10 ) )
							{
								if ( m != this )
									targets.Add( m );
							}
								
							for ( int i = 0; i < targets.Count; ++i )
							{
								Mobile m = (Mobile)targets[i];

								if ( m != null )
								{
									if ( m is BaseVendor || m is PlayerVendor || m.Blessed != false )
									{
										m.Say( "Someone is calling for help!" );
									}
									else if ( m is BaseCreature )
									{
										BaseCreature bc = (BaseCreature)m;
										if ( bc.Controlled != true )
										{
											bc.Combatant = from;
										}
									}
									else
									{
										m.SendMessage( "You hear a creature give out a cry for help!" );
									}
								}	
							}
						}
						this.Emote( "[Power Attack]" );
						this.Emote( "[Call Of Nature]" );
					}
				}
				else if ( m_AcidRain == true )
				{
					if( Utility.Random( 2500 ) < 5 ) 
					{
						if ( this.Stam <= 50 )
						{
							if ( this.ControlMaster != null )
								this.ControlMaster.SendMessage( "You pet lacks the stamina to preform its power attack." );
						}
						else
						{
							this.Stam -= 50;

							ArrayList targets = new ArrayList();

							foreach ( Mobile m in this.GetMobilesInRange( 7 ) )
							{
								if ( this != m && this.ControlMaster != m )
									targets.Add( m );
							}
								
							for ( int i = 0; i < targets.Count; ++i )
							{
								Mobile m = (Mobile)targets[i];
					
								if ( m != null )
								{
									if ( m is BaseVendor || m is PlayerVendor || m.Blessed != false )
									{
										m.Say( "*resists the poison*" );
									}
									else
									{
										if ( m is PlayerMobile )
										{
											PlayerMobile pm = (PlayerMobile)m;
											if ( pm.Combatant == this || pm.Combatant == this.ControlMaster || this.Combatant == pm || this.ControlMaster.Combatant == pm )
											{
												AOS.Damage( m, this, Utility.RandomMinMax( 5, 25 ), 0, 0, 0, 100, 0 );

												m.SendMessage( "You have been hit by toxic rain." );

												int level;

												double total = this.Skills[SkillName.Poisoning].Value;

												if ( total >= 99.9 )
													level = 4;
												else if ( total > 75.0 )
													level = 3;
												else if ( total > 50.0)
													level = 2;
												else if ( total > 35.0)
													level = 1;
												else
													level = 0;

												m.ApplyPoison( this, Poison.GetPoison( level ) );
												this.ControlMaster.DoHarmful( m );
												Effects.SendMovingParticles( new Entity( Serial.Zero, new Point3D( m.X, m.Y, m.Z + 35 ), m.Map ), m, 0x35D4, 7, 0, false, true, 63, 0, 9502, 1, 0, (EffectLayer)255, 0x100 );
												m.PlaySound( 551 );
											}
										}
										else if ( m is BaseCreature )
										{
											BaseCreature bc = (BaseCreature)m;
											if ( bc.Controlled == false && bc.ControlMaster == null )
											{
												AOS.Damage( bc, this, Utility.RandomMinMax( 5, 25 ), 0, 0, 0, 0, 100 );

												int level;

												double total = this.Skills[SkillName.Poisoning].Value;

												if ( total >= 99.9 )
													level = 4;
												else if ( total > 75.0 )
													level = 3;
												else if ( total > 50.0)
													level = 2;
												else if ( total > 35.0)
													level = 1;
												else
													level = 0;

												bc.ApplyPoison( this, Poison.GetPoison( level ) );
												this.ControlMaster.DoHarmful( m );
												Effects.SendMovingParticles( new Entity( Serial.Zero, new Point3D( m.X, m.Y, m.Z + 35 ), m.Map ), m, 0x35D4, 7, 0, false, true, 63, 0, 9502, 1, 0, (EffectLayer)255, 0x100 );
											}
											else if ( bc.Combatant == this || bc.Combatant == this.ControlMaster || this.Combatant == bc || this.ControlMaster.Combatant == bc )
											{
												AOS.Damage( bc, this, Utility.RandomMinMax( 5, 25 ), 0, 0, 0, 0, 100 );

												bc.ControlMaster.SendMessage( "Your pet has been hit by toxic rain." );

												int level;

												double total = this.Skills[SkillName.Poisoning].Value;

												if ( total >= 99.9 )
													level = 4;
												else if ( total > 75.0 )
													level = 3;
												else if ( total > 50.0)
													level = 2;
												else if ( total > 35.0)
													level = 1;
												else
													level = 0;

												bc.ApplyPoison( this, Poison.GetPoison( level ) );
												this.ControlMaster.DoHarmful( m );
												Effects.SendMovingParticles( new Entity( Serial.Zero, new Point3D( m.X, m.Y, m.Z + 35 ), m.Map ), m, 0x35D4, 7, 0, false, true, 63, 0, 9502, 1, 0, (EffectLayer)255, 0x100 );
											}
										}
									}
								}
							}

							this.Emote( "[Power Attack]" );
							this.Emote( "[Acid Rain]" );
						}
					}
				}
			}
		}

		public override void OnDamagedBySpell( Mobile caster )
		{
			if ( caster != this && this.ControlMaster != null )
			{

				int damagemin = this.Hits / 20;
				int damagemax = this.Hits / 25;

				if ( this.Poison != null )
				{
					this.DoCure();
				}
				else if ( this.Hits <= this.HitsMax )
				{
					this.DoHeal();
				}	


				if ( m_BluntAttack == true )
				{ 
					if( Utility.Random( 100 ) < 15 )
					{
						if ( this.Stam <= 10 )
						{
							if ( this.ControlMaster != null )
								this.ControlMaster.SendMessage( "You pet lacks the stamina to preform its counter attack." );
						}
						else
						{
							this.Stam -= 10;

							this.Emote( "[Counter] [Blunt Attack]" );
							AOS.Damage( caster, this, Utility.RandomMinMax( 10, 25 ), 100, 0, 0, 0, 0 );
							caster.FixedParticles( 0x373A, 10, 15, 5012, EffectLayer.Waist );
							PlaySound( 562 );
						}
					}
				}
				if ( m_Healing == true )
				{
					if( Utility.Random( 100 ) < 15 )
					{
						if ( this.Stam <= 25 )
						{
							if ( this.ControlMaster != null )
								this.ControlMaster.SendMessage( "You pet lacks the stamina to preform its counter attack." );
						}
						else
						{
							this.Stam -= 25;

							this.Emote( "[Counter] [Healing]" );
							this.FixedParticles( 0x373A, 10, 15, 5012, EffectLayer.Waist );
							PlaySound( 498 );
							this.Hits += Utility.RandomMinMax( 75, 150 );
						}
					}
				}

				if ( m_PoisonAttack == true )
				{
					if( Utility.Random( 100 ) < 15 )
					{
						if ( this.Stam <= 10 )
						{
							if ( this.ControlMaster != null )
								this.ControlMaster.SendMessage( "You pet lacks the stamina to preform its counter attack." );
						}
						else
						{
							this.Stam -= 10;

							this.Emote( "[Counter] [Poison Attack]" );
							this.FixedParticles( 4518, 10, 15, 5012, EffectLayer.Waist );
							AOS.Damage( caster, this, Utility.RandomMinMax( 10, 25 ), 0, 0, 0, 100, 0 );
							PlaySound( 560 );

							int level;

							double total = this.Skills[SkillName.Poisoning].Value;

							if ( total >= 99.9 )
								level = 4;
							else if ( total > 75.0 )
								level = 3;
							else if ( total > 50.0)
								level = 2;
							else if ( total > 35.0)
								level = 1;
							else
								level = 0;

							caster.ApplyPoison( this, Poison.GetPoison( level ) );
						}
					}
				}
			}

			base.OnDamagedBySpell( caster );
		}

		public override void OnGotMeleeAttack( Mobile attacker )
		{

			base.OnGotMeleeAttack( attacker );

			if ( this.ControlMaster != null )
			{

				int damagemin = this.Hits / 20;
				int damagemax = this.Hits / 25;

				if ( this.Poison != null )
				{
					this.DoCure();
				}
				else if ( this.Hits <= this.HitsMax )
				{
					this.DoHeal();
				}

				if ( m_BluntAttack == true )
				{ 
					if( Utility.Random( 100 ) < 15 )
					{
						if ( this.Stam <= 10 )
						{
							if ( this.ControlMaster != null )
								this.ControlMaster.SendMessage( "You pet lacks the stamina to preform its counter attack." );
						}
						else
						{
							this.Stam -= 10;

							this.Emote( "[Counter] [Blunt Attack]" );
							AOS.Damage( attacker, this, Utility.RandomMinMax( 10, 25 ), 100, 0, 0, 0, 0 );
							attacker.FixedParticles( 0x373A, 10, 15, 5012, EffectLayer.Waist );
							PlaySound( 562 );
						}
					}
				}
				if ( m_Healing == true )
				{
					if( Utility.Random( 100 ) < 15 )
					{
						if ( this.Stam <= 25 )
						{
							if ( this.ControlMaster != null )
								this.ControlMaster.SendMessage( "You pet lacks the stamina to preform its counter attack." );
						}
						else
						{
							this.Stam -= 25;

							this.Emote( "[Counter] [Healing]" );
							this.FixedParticles( 0x373A, 10, 15, 5012, EffectLayer.Waist );
							PlaySound( 498 );
							this.Hits += Utility.RandomMinMax( 75, 150 );
						}
					}
				}

				if ( m_PoisonAttack == true )
				{
					if( Utility.Random( 100 ) < 15 )
					{
						if ( this.Stam <= 10 )
						{
							if ( this.ControlMaster != null )
								this.ControlMaster.SendMessage( "You pet lacks the stamina to preform its counter attack." );
						}
						else
						{
							this.Stam -= 10;

							this.Emote( "[Counter] [Poison Attack]" );
							this.FixedParticles( 4518, 10, 15, 5012, EffectLayer.Waist );
							AOS.Damage( attacker, this, Utility.RandomMinMax( 10, 25 ), 0, 0, 0, 100, 0 );
							PlaySound( 560 );

							int level;

							double total = this.Skills[SkillName.Poisoning].Value;

							if ( total >= 99.9 )
								level = 4;
							else if ( total > 75.0 )
								level = 3;
							else if ( total > 50.0)
								level = 2;
							else if ( total > 35.0)
								level = 1;
							else
								level = 0;

							attacker.ApplyPoison( this, Poison.GetPoison( level ) );
						}
					}
				}
			}
		}

		public BaseBioCreature( Serial serial ) : base( serial )
		{
		}

		public override int GetAngerSound()
		{
			if ( this.BaseSoundID == 0 )
			{
				if ( this.BodyValue == 400 || this.BodyValue == 401 )
				{
					if ( this.Female == true )
					{
						return 824;
					}	
					else
					{
						return 1098;
					}
				}
				else
				{
					return 768;
				}
			}
			else
			{
				return BaseSoundID;
			}
		}

		public override int GetIdleSound()
		{
			if ( this.BaseSoundID == 0 )
			{
				if ( this.BodyValue == 400 || this.BodyValue == 401 )
				{
					if ( this.Female == true )
					{
						return 822;
					}
					else
					{
						return 1096;
					}
				}
				else
				{
					return 769;
				}
			}
			else
			{
				return BaseSoundID + 1;
			}
		}

		public override int GetAttackSound()
		{
			if ( this.BaseSoundID == 0 )
			{
				if ( this.BodyValue == 400 || this.BodyValue == 401 )
				{
					if ( this.Female == true )
					{
						return 796;
					}
					else
					{
						return 1081;
					}
				}
				else
				{
					return 770;
				}
			}
			else
			{
				return BaseSoundID + 2;
			}
		}

		public override int GetHurtSound()
		{
			if ( this.BaseSoundID == 0 )
			{
				if ( this.BodyValue == 400 || this.BodyValue == 401 )
				{
					if ( this.Female == true )
					{
						return 804;
					}
					else
					{
						return 1078;
					}
				}
				else
				{
					return 771;
				}
			}
			else
			{
				return BaseSoundID + 3;
			}
		}

		public override int GetDeathSound()
		{
			if ( this.BaseSoundID == 0 )
			{
				if ( this.BodyValue == 400 || this.BodyValue == 401 )
				{
					if ( this.Female == true )
					{
						return 336;
					}
					else
					{
						return 346;
					}
				}
				else
				{
					return 772;
				}
			}
			else
			{
				return BaseSoundID + 4;
			}
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 2 );

			writer.Write( m_AcidRain );

			writer.Write( m_CallOfNature );

			writer.Write( m_HasDNA );

			writer.Write( m_Craftedby );

			writer.Write( m_BluntAttack );

			writer.Write( m_Healing );

			writer.Write( m_PoisonAttack );

			writer.Write( m_TrialByFire );

			writer.Write( m_IceBlast );

			writer.Write( m_CometAttack );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();

			switch ( version )
			{
				case 2:
				{
					m_AcidRain = reader.ReadBool();
					goto case 1;
				}
				case 1:
				{
					m_CallOfNature = reader.ReadBool();
					goto case 0;
				}
				case 0:
				{
					m_HasDNA = reader.ReadBool();
					m_Craftedby = reader.ReadMobile();
					m_BluntAttack = reader.ReadBool();
					m_Healing = reader.ReadBool();
					m_PoisonAttack = reader.ReadBool();
					m_TrialByFire = reader.ReadBool();
					m_IceBlast = reader.ReadBool();
					m_CometAttack = reader.ReadBool();

					break;
				}
			}
		}

  		private class MountTarget : Target 
      		{
			private Mobile m_Bio;
			private Mobile m_From;

         		public MountTarget( Mobile bio, Mobile from ) : base ( 10, false, TargetFlags.None ) 
         		{ 
				m_Bio = bio;
				m_From = from;
         		} 
          
         		protected override void OnTarget( Mobile from, object target ) 
         		{
				if ( target is BaseMount )
				{
					BaseMount bm = (BaseMount)target;
					if ( bm.ControlMaster == m_From )
					{
						bm.Rider = m_Bio;
					}
					else
					{
						m_From.SendMessage( "This is not your pet." );
					}
				}
				else
				{
					m_From.SendMessage( "That is not a mount" );
				}
			}
		}
	}
}