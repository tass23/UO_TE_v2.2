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
	public class BBC : BaseCreature
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
		public BBC() : base( AIType.AI_Animal, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a bio-engienered clone";
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

			Tamable = false;
			ControlSlots = 3;
			MinTameSkill = 1000.0;

			Container pack = Backpack;

			if ( pack != null )
				pack.Delete();

			pack = new StrongBackpack();
			pack.Movable = false;

			AddItem( pack );
		}

		public override void OnThink()
		{
			base.OnThink();

			Mobile m = this.ControlMaster;

			if ( m != null )
				m.SendMessage( "Your pet was removed due to a bug." );

			this.Delete();
		}

		public override bool OnDragDrop( Mobile from, Item item )
		{
			if ( CheckFeed( from, item ) )
				return true;

			if ( PackAnimal.CheckAccess( this, from ) )
			{
				AddToBackpack( item );
				return true;
			}

			return base.OnDragDrop( from, item );
		}

		public override void AddNameProperties( ObjectPropertyList list )
		{
			base.AddNameProperties( list );

			if ( m_Craftedby != null )
				list.Add( 1060658, "Bio-engineered by\t{0}", m_Craftedby.Name.ToString() );
		}

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

		public BBC( Serial serial ) : base( serial )
		{
		}

		public override int GetAngerSound()
		{
			if ( this.BaseSoundID == 0 )
			{
				return 768;
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
				return 769;
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
				return 770;
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
				return 771;
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
				return 772;
			}
			else
			{
				return BaseSoundID + 4;
			}
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );

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
				case 0:
				{
					m_AcidRain = reader.ReadBool();
					m_CallOfNature = reader.ReadBool();
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
	}
}