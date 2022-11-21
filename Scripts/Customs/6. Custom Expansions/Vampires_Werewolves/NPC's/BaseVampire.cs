using System;
using Server.Misc;
using Server.Network;
using System.Collections;
using Server.Items;
using Server.Targeting;
using Server.Regions;
using Server.Spells;

namespace Server.Mobiles
{
	public class BaseVampire : BaseCreature
	{
		public override bool ClickTitle{ get{ return false; } }
		
		private int m_Rank;
		[CommandProperty(AccessLevel.GameMaster)]
		public int Rank
		{
			get { return m_Rank; }
			set { m_Rank = value; }
		}
		
		public BaseVampire() : base( AIType.AI_Melee, FightMode.Closest, 15, 1, 0.2, 0.4 )
		{
		}

		public BaseVampire(AIType ai, FightMode mode, int iRangePerception, int iRangeFight, double dActiveSpeed, double dPassiveSpeed): base (ai, mode, iRangePerception, iRangeFight, dActiveSpeed, dPassiveSpeed)
		{
		}
		
		public override void AlterMeleeDamageFrom( Mobile from, ref int damage )
		{ 
			Item item1 = from.FindItemOnLayer(Layer.OneHanded);
			Item item2 = from.FindItemOnLayer(Layer.TwoHanded);
			if (!((item1 is IVampireSlayer) || (item2 is IVampireSlayer)))
				damage /= 2;
			else
			{
				int spelldamage = Utility.Random( 10, 40 );
				this.DoHarmful(from);
				SpellHelper.Damage(TimeSpan.Zero, this, from, spelldamage, 0, 0, 0, 0, 100);
				this.PlaySound( 0x51D );
				this.FixedParticles( 0x37C4, 1, 25, 9922, 14, 3, EffectLayer.Head );
			}
		}
		
		public override void AlterMeleeDamageTo( Mobile to, ref int damage )
		{
			if (to.AccessLevel >= AccessLevel.GameMaster)
				damage = 0;
			else if(Utility.RandomMinMax(1,10) < 3)
			{
				if (to is PlayerMobile)
				{
					PlayerMobile pm = (PlayerMobile) to;
					if (pm.Vampire < 1 && (pm.Werewolf > 0))
					{
						pm.SendMessage(33, "You feel strange...");
						pm.Vampire = 1;
						pm.HueMod = 0x847E;
						pm.Title = "the Vampire";
						pm.AddStatMod(new StatMod(StatType.Str, "Vampire Str Bonus", 20, TimeSpan.Zero));
						pm.AddStatMod(new StatMod(StatType.Dex, "Vampire Dex Bonus", 5, TimeSpan.Zero));
						pm.AddStatMod(new StatMod(StatType.Int, "Vampire Int Bonus", 10, TimeSpan.Zero));
						Server.Items.BleedAttack.BeginBleed(pm, this, true);
						pm.PlaySound(0x19C);
						this.Location = pm.Location;
						pm.FixedParticles(0x3709, 1, 30, 9904, 1108, 6, EffectLayer.Head); 
						this.Combatant = null;
					}
				}
			}
			else
				damage += m_Rank;
		}
		
		public override void AlterSpellDamageTo( Mobile to, ref int damage )
		{
			if (to.AccessLevel >= AccessLevel.GameMaster)
				damage = 0;
			else 
				damage += m_Rank;
		}
		
		public bool YewCryptsCheck(Mobile from)
		{
			if ( (from.Map == Map.Trammel || from.Map == Map.Felucca) &&
				((from.X > 943 && from.Y > 696 && from.X < 967 && from.Y < 719) ||
				(from.X > 959 && from.Y > 687 && from.X < 965 && from.Y < 697) ||
				(from.X > 959 && from.Y > 751 && from.X < 975 && from.Y < 763) ||
				(from.X > 1011 && from.Y > 692 && from.X < 1019 && from.Y < 711) ||
				(from.X > 1005 && from.Y > 817 && from.X < 1013 && from.Y < 825) ||
				(from.X > 1013 && from.Y > 817 && from.X < 1021 && from.Y < 831) ||
				(from.X > 943 && from.Y > 687 && from.X < 1021 && from.Y < 831 && from.Z < 0)) )
			{
				return true;
			}
			return false;
		}
			
		public bool IsDungeon(Mobile from)
		{
			if ((from.Map == Map.Trammel) || (from.Map == Map.Felucca))
			{ 
				if (from.Z < -4) return false;
				if (from.X > 6912 && from.Y > 257 && from.X < 7167 && from.Y < 512) return true; //Heartwood
				if ((from.X > 5119 && from.Y < 2303) || (from.X > 6145)) return false; //Dungeon Area
				if (YewCryptsCheck(from)) return false; //Yew Crypts
				if ((from.X > 2030 && from.Y > 221 && from.X < 2054 && from.Y < 233)) return false; //wrong entrance
				/*if( from.Region.Name == "Wind" || 
					from.Region.Name == "Covetous" || 
					from.Region.Name == "Deceit" || 
					from.Region.Name == "Despise" || 
					from.Region.Name == "Destard" || 
					from.Region.Name == "Hythloth" || 
					from.Region.Name == "Shame" || 
					from.Region.Name == "Wrong" || 
					from.Region.Name == "Terathan Keep" || 
					from.Region.Name == "Fire" || 
					from.Region.Name == "Ice" ||
					from.Region.Name == "Orc Cave" ||
					from.Region.Name == "Sancurary" ||
					from.Region.Name == "Misc Dungeons" ||
					from.Region.Name == "Painted Caves" ||
					from.Region.Name == "Palace of Paroxysmus" ||
					from.Region.Name == "Khaldun" ||
					from.Region.Name == "Despise Passage" ||
					from.Region.Name == "Jail") return false;*/
						
				if (from.Region.Name == "Cave 1" ||
					from.Region.Name == "Cave 2" ||
					from.Region.Name == "Cave 3" ||
					from.Region.Name == "Cave 4" ||
					from.Region.Name == "Minoc Cave 1" ||
					from.Region.Name == "Minoc Cave 2" ||
					from.Region.Name == "Minoc Cave 3" ||
					from.Region.Name == "Minoc Mine" ||
					from.Region.Name == "Britain Mine 1" ||
					from.Region.Name == "Britain Mine 2" ||
					from.Region.Name == "Avatar Isle Cave" ||
					from.Region.Name == "Ice Isle Cave 1" ||
					from.Region.Name == "Ice Isle Cave 2" ||
					from.Region.Name == "Yew Cave" ||
					from.Region.Name == "North Territory Cave" ||
					from.Region.Name == "North Territory Mine 1" ||
					from.Region.Name == "North Territory Mine 2" ||
					from.Region.Name == "Covetous Mine") return false;
				return true;
			}
			if (from.Map == Map.Ilshenar)
			{
				if (from.Region.Name == "Rock Dungeon" || 
					from.Region.Name == "Spider Cave" || 
					from.Region.Name == "Spectre Dungeon" || 
					from.Region.Name == "Blood Dungeon" || 
					from.Region.Name == "Wisp Dungeon" || 
					from.Region.Name == "Ankh Dungeon" || 
					from.Region.Name == "Exodus Dungeon" || 
					from.Region.Name == "Sorcerer's Dungeon" ||
					from.Region.Name == "Ratman Cave" ||
					from.Region.Name == "Mushroom Cave" ||
					from.Region.Name == "Lizard Passage" ||
					from.Region.Name == "Ancient Lair") return false;
				return true;
			}
			if (from.Map == Map.Malas)
			{
				/*if (from.Region.Name == "Labyrinth" ||
					from.Region.Name == "Fan Danver's Dojo" ||
					from.Region.Name == "Yomotsu Mines" ||
					from.Region.Name == "Doom Gauntlet" ||
					from.Region.Name == "Bedlam" ||
					from.Region.Name == "Citadel" ||
					from.Region.Name == "Doom") return false;*/
				if(from.Region.Name == "Umbra") return false;
				if(from.X < 513) return false; //Dungeon Area
				return true;
			}
			if (from.Map == Map.Tokuno)
			{
				return true;
			}
			return true;
		} 
		
		public virtual void CheckNocturnal()
		{
			_LastActiveCheck = DateTime.Now;

			int hours, minutes;
			if (IsDungeon(this))
			{
       			Server.Items.Clock.GetTime(this.Map, this.X, this.Y, out hours, out minutes);

					// 00:00 AM - 00:59 AM : Witching hour
					// 01:00 AM - 03:59 AM : Middle of night
					// 04:00 AM - 07:59 AM : Early morning
					// 08:00 AM - 11:59 AM : Late morning
					// 12:00 PM - 12:59 PM : Noon
					// 01:00 PM - 03:59 PM : Afternoon
					// 04:00 PM - 07:59 PM : Early evening
					// 08:00 PM - 11:59 AM : Late at night

				if (( hours >= 20 ) || (hours <= 07))
				{
					this.AccessLevel = AccessLevel.Player;
					this.Blessed = false;
					this.Hidden = false;
					this.NameMod = null;
				}
				else if (( hours < 20 ) && (hours > 07) && (this.Combatant == null))
				{
					this.AccessLevel = AccessLevel.GameMaster;
					this.Blessed = true;
					this.Hidden = true;
					if (this.NameMod == null) this.NameMod = this.Name + " (Resting)";
				}
			}
			else
			{
				this.AccessLevel = AccessLevel.Player;
				this.Blessed = false;
				this.Hidden = false;
				this.NameMod = null;
			}
		}
		
		private DateTime _LastActiveCheck = DateTime.Now;
		private TimeSpan _ActiveCheckDelay = TimeSpan.FromSeconds( 5.0 );
		public bool CanCheckActive()
		{
		     if( _LastActiveCheck.Add( _ActiveCheckDelay ) < DateTime.Now )
		          return true;
		     return false;
		}
		
		public virtual void Transform()
		{
			if ((Body == 400) || (Body == 401)) //transforms to bat
			{
				Effects.SendLocationParticles( EffectItem.Create( this.Location, this.Map, EffectItem.DefaultDuration ), 0x3728, 8, 20, 5042 );
				Effects.PlaySound( this, this.Map, 0x201 );
				Body = 317;
				BaseSoundID = 0x270;
				Hue = 1109;
			}
			else //transforms to human form
			{
				Effects.SendLocationParticles( EffectItem.Create( this.Location, this.Map, EffectItem.DefaultDuration ), 0x3728, 8, 20, 5042 );
				Effects.PlaySound( this, this.Map, 0x201 );
				Body = (Female? 401:400);
				Hue = 1150;
				BaseSoundID = 0x4B0;
			}
		}

		public override void OnThink() 
		{ 
			if (CanCheckActive()) CheckNocturnal();
			if (Hits < (HitsMax * .10) && Body != 317) Transform();
			if (Hits >= (HitsMax * .10) && Body == 317) Transform();
			
			base.OnThink(); 
		}
		
		public override bool IsEnemy( Mobile m )
		{
			if (m is PlayerMobile)
			{
				PlayerMobile pm = (PlayerMobile) m;
				if (pm.Vampire > 0) return false;
			}
			return base.IsEnemy(m);
		}
		
		public override bool OnBeforeDeath()
		{
			Mobile killer = this.FindMostRecentDamager(true);
			if (killer is PlayerMobile)
			{
				PlayerMobile pm = (PlayerMobile) killer;
				if (pm.Vampire > 0) 
				{
					pm.Vampire -= 1;
					if (pm.Vampire == 0) pm.Title = "";
				}
				else pm.Title = "";
			}
			
			if (Body != 26)
			{
				GhostFormVampire rm = new GhostFormVampire();
				rm.Team = this.Team;
				rm.Rank = this.Rank;
				rm.Name = this.Name;
				rm.Combatant = this.Combatant;
				rm.MoveToWorld(this.Location, this.Map);
				rm.FixedParticles(0x3709, 1, 30, 9904, 1108, 6, EffectLayer.Head);
				
			}
			else return base.OnBeforeDeath();
			this.Delete();
			return false;
		}
		
		public override bool CanRummageCorpses{ get{ return true; } }
		public override bool BardImmune{ get{ return true; } }
		public override bool Unprovokable{ get{ return true; } }
		public override bool Uncalmable{ get{ return true; } }
		public override bool AutoDispel{ get{ return true; } }
		public override Poison PoisonImmune{ get{ return Poison.Deadly; } }
		public override Poison HitPoison{ get{ return Poison.Lesser; } }
		public override bool AlwaysMurderer{ get{ return true; } }
		
		public BaseVampire( Serial serial ) : base( serial )
		{
		}
		
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
			writer.Write((int)m_Rank);
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
			m_Rank = reader.ReadInt();
		}
	}
	public class BaseWerewolf : BaseCreature
	{
		public override bool ClickTitle{ get{ return false; } }
		
		private int m_Rank;
		[CommandProperty(AccessLevel.GameMaster)]
		public int Rank
		{
			get { return m_Rank; }
			set { m_Rank = value; }
		}
		
		public BaseWerewolf() : base( AIType.AI_Melee, FightMode.Closest, 15, 1, 0.2, 0.4 )
		{
		}

		public BaseWerewolf(AIType ai, FightMode mode, int iRangePerception, int iRangeFight, double dActiveSpeed, double dPassiveSpeed): base (ai, mode, iRangePerception, iRangeFight, dActiveSpeed, dPassiveSpeed)
		{
		}
		
		public override void AlterMeleeDamageFrom( Mobile from, ref int damage )
		{ 
			Item item1 = from.FindItemOnLayer(Layer.OneHanded);
			Item item2 = from.FindItemOnLayer(Layer.TwoHanded);
			if (!((item1 is IVampireSlayer) || (item2 is IVampireSlayer)))
				damage /= 2;
			else
			{
				int spelldamage = Utility.Random( 10, 40 );
				this.DoHarmful(from);
				SpellHelper.Damage(TimeSpan.Zero, this, from, spelldamage, 0, 0, 0, 0, 100);
				this.PlaySound( 0x51D );
				this.FixedParticles( 0x37C4, 1, 25, 9922, 14, 3, EffectLayer.Head );
			}
		}
		
		public override void AlterMeleeDamageTo( Mobile to, ref int damage )
		{
			if (to.AccessLevel >= AccessLevel.GameMaster)
				damage = 0;
			else if(Utility.RandomMinMax(1,10) < 3)
			{
				if (to is PlayerMobile)
				{
					PlayerMobile pm = (PlayerMobile) to;
					if (pm.Werewolf < 1 && (pm.Vampire < 1))
					{
						pm.SendMessage(33, "You feel strange...");
						pm.Werewolf = 1;
						pm.HueMod = 0x847E;
						pm.Title = "the Werewolf";
						pm.AddStatMod(new StatMod(StatType.Str, "Werewolf Str Bonus", 20, TimeSpan.Zero));
						pm.AddStatMod(new StatMod(StatType.Dex, "Werewolf Dex Bonus", 5, TimeSpan.Zero));
						pm.AddStatMod(new StatMod(StatType.Int, "Werewolf Int Bonus", 10, TimeSpan.Zero));
						Server.Items.BleedAttack.BeginBleed(pm, this, true);
						pm.PlaySound(0x19C);
						this.Location = pm.Location;
						pm.FixedParticles(0x3709, 1, 30, 9904, 1108, 6, EffectLayer.Head); 
						this.Combatant = null;
					}
				}
			}
			else
				damage += m_Rank;
		}
		
		public override void AlterSpellDamageTo( Mobile to, ref int damage )
		{
			if (to.AccessLevel >= AccessLevel.GameMaster)
				damage = 0;
			else 
				damage += m_Rank;
		}
		
		/*
		public bool YewCryptsCheck(Mobile from)
		{
			if ( (from.Map == Map.Trammel || from.Map == Map.Felucca) &&
				((from.X > 943 && from.Y > 696 && from.X < 967 && from.Y < 719) ||
				(from.X > 959 && from.Y > 687 && from.X < 965 && from.Y < 697) ||
				(from.X > 959 && from.Y > 751 && from.X < 975 && from.Y < 763) ||
				(from.X > 1011 && from.Y > 692 && from.X < 1019 && from.Y < 711) ||
				(from.X > 1005 && from.Y > 817 && from.X < 1013 && from.Y < 825) ||
				(from.X > 1013 && from.Y > 817 && from.X < 1021 && from.Y < 831) ||
				(from.X > 943 && from.Y > 687 && from.X < 1021 && from.Y < 831 && from.Z < 0)) )
			{
				return true;
			}
			return false;
		}
			
		public bool IsDungeon(Mobile from)
		{
			if ((from.Map == Map.Trammel) || (from.Map == Map.Felucca))
			{ 
				if (from.Z < -4) return false;
				if (from.X > 6912 && from.Y > 257 && from.X < 7167 && from.Y < 512) return true; //Heartwood
				if ((from.X > 5119 && from.Y < 2303) || (from.X > 6145)) return false; //Dungeon Area
				if (YewCryptsCheck(from)) return false; //Yew Crypts
				if ((from.X > 2030 && from.Y > 221 && from.X < 2054 && from.Y < 233)) return false; //wrong entrance
				// Commented out
					if( from.Region.Name == "Wind" || 
					from.Region.Name == "Covetous" || 
					from.Region.Name == "Deceit" || 
					from.Region.Name == "Despise" || 
					from.Region.Name == "Destard" || 
					from.Region.Name == "Hythloth" || 
					from.Region.Name == "Shame" || 
					from.Region.Name == "Wrong" || 
					from.Region.Name == "Terathan Keep" || 
					from.Region.Name == "Fire" || 
					from.Region.Name == "Ice" ||
					from.Region.Name == "Orc Cave" ||
					from.Region.Name == "Sancurary" ||
					from.Region.Name == "Misc Dungeons" ||
					from.Region.Name == "Painted Caves" ||
					from.Region.Name == "Palace of Paroxysmus" ||
					from.Region.Name == "Khaldun" ||
					from.Region.Name == "Despise Passage" ||
					from.Region.Name == "Jail") return false;
				// UnComment
						
				if (from.Region.Name == "Cave 1" ||
					from.Region.Name == "Cave 2" ||
					from.Region.Name == "Cave 3" ||
					from.Region.Name == "Cave 4" ||
					from.Region.Name == "Minoc Cave 1" ||
					from.Region.Name == "Minoc Cave 2" ||
					from.Region.Name == "Minoc Cave 3" ||
					from.Region.Name == "Minoc Mine" ||
					from.Region.Name == "Britain Mine 1" ||
					from.Region.Name == "Britain Mine 2" ||
					from.Region.Name == "Avatar Isle Cave" ||
					from.Region.Name == "Ice Isle Cave 1" ||
					from.Region.Name == "Ice Isle Cave 2" ||
					from.Region.Name == "Yew Cave" ||
					from.Region.Name == "North Territory Cave" ||
					from.Region.Name == "North Territory Mine 1" ||
					from.Region.Name == "North Territory Mine 2" ||
					from.Region.Name == "Covetous Mine") return false;
				return true;
			}
			if (from.Map == Map.Ilshenar)
			{
				if (from.Region.Name == "Rock Dungeon" || 
					from.Region.Name == "Spider Cave" || 
					from.Region.Name == "Spectre Dungeon" || 
					from.Region.Name == "Blood Dungeon" || 
					from.Region.Name == "Wisp Dungeon" || 
					from.Region.Name == "Ankh Dungeon" || 
					from.Region.Name == "Exodus Dungeon" || 
					from.Region.Name == "Sorcerer's Dungeon" ||
					from.Region.Name == "Ratman Cave" ||
					from.Region.Name == "Mushroom Cave" ||
					from.Region.Name == "Lizard Passage" ||
					from.Region.Name == "Ancient Lair") return false;
				return true;
			}
			if (from.Map == Map.Malas)
			{
				// Commented out
					if (from.Region.Name == "Labyrinth" ||
					from.Region.Name == "Fan Danver's Dojo" ||
					from.Region.Name == "Yomotsu Mines" ||
					from.Region.Name == "Doom Gauntlet" ||
					from.Region.Name == "Bedlam" ||
					from.Region.Name == "Citadel" ||
					from.Region.Name == "Doom") return false;
				// UnComment
				if(from.Region.Name == "Umbra") return false;
				if(from.X < 513) return false; //Dungeon Area
				return true;
			}
			if (from.Map == Map.Tokuno)
			{
				return true;
			}
			return true;
		}
		*/
		
		/*
		public virtual void CheckNocturnal()
		{
			_LastActiveCheck = DateTime.Now;

			int hours, minutes;
			if (IsDungeon(this))
			{
       			Server.Items.Clock.GetTime(this.Map, this.X, this.Y, out hours, out minutes);

					// 00:00 AM - 00:59 AM : Witching hour
					// 01:00 AM - 03:59 AM : Middle of night
					// 04:00 AM - 07:59 AM : Early morning
					// 08:00 AM - 11:59 AM : Late morning
					// 12:00 PM - 12:59 PM : Noon
					// 01:00 PM - 03:59 PM : Afternoon
					// 04:00 PM - 07:59 PM : Early evening
					// 08:00 PM - 11:59 AM : Late at night

				if (( hours >= 20 ) || (hours <= 07))
				{
					this.AccessLevel = AccessLevel.Player;
					this.Blessed = false;
					this.Hidden = false;
					this.NameMod = null;
				}
				else if (( hours < 20 ) && (hours > 07) && (this.Combatant == null))
				{
					this.AccessLevel = AccessLevel.GameMaster;
					this.Blessed = true;
					this.Hidden = true;
					if (this.NameMod == null) this.NameMod = this.Name + " (Resting)";
				}
			}
			else
			{
				this.AccessLevel = AccessLevel.Player;
				this.Blessed = false;
				this.Hidden = false;
				this.NameMod = null;
			}
		}
		*/
		
		private DateTime _LastActiveCheck = DateTime.Now;
		private TimeSpan _ActiveCheckDelay = TimeSpan.FromSeconds( 5.0 );
		public bool CanCheckActive()
		{
		     if( _LastActiveCheck.Add( _ActiveCheckDelay ) < DateTime.Now )
		          return true;
		     return false;
		}
		
		public virtual void Transform()
		{
			if ((Body == 500) || (Body == 501)) //transforms to wolf
			{
				Effects.SendLocationParticles( EffectItem.Create( this.Location, this.Map, EffectItem.DefaultDuration ), 0x3728, 8, 20, 5042 );
				Effects.PlaySound( this, this.Map, 0x201 );
				Body = 23;
				BaseSoundID = 0xE5;
				Hue = 1109;
			}
			else //transforms to human form
			{
				Effects.SendLocationParticles( EffectItem.Create( this.Location, this.Map, EffectItem.DefaultDuration ), 0x3728, 8, 20, 5042 );
				Effects.PlaySound( this, this.Map, 0x201 );
				Body = (Female? 501:500);
				Hue = 1150;
				BaseSoundID = 0x4B0;
			}
		}

		public override void OnThink() 
		{ 
			if (CanCheckActive()); //CheckNocturnal();
			if (Hits < (HitsMax * .10) && Body != 23) Transform();
			if (Hits >= (HitsMax * .10) && Body == 23) Transform();
			
			base.OnThink(); 
		}
		
		public override bool IsEnemy( Mobile m )
		{
			if (m is PlayerMobile)
			{
				PlayerMobile pm = (PlayerMobile) m;
				if (pm.Werewolf > 0) return false;
			}
			return base.IsEnemy(m);
		}
		
		public override bool OnBeforeDeath()
		{
			Mobile killer = this.FindMostRecentDamager(true);
			if (killer is PlayerMobile)
			{
				PlayerMobile pm = (PlayerMobile) killer;
				if (pm.Werewolf > 0) 
				{
					pm.Werewolf -= 1;
					if (pm.Werewolf == 0) pm.Title = "";
				}
				else pm.Title = "werewolf slayer";
			}
			
			if (Body != 26)
			{
				GhostFormVampire rm = new GhostFormVampire();
				rm.Team = this.Team;
				rm.Rank = this.Rank;
				rm.Name = this.Name;
				rm.Combatant = this.Combatant;
				rm.MoveToWorld(this.Location, this.Map);
				rm.FixedParticles(0x3709, 1, 30, 9904, 1108, 6, EffectLayer.Head);
				
			}
			else return base.OnBeforeDeath();
			this.Delete();
			return false;
		}
		
		public override bool CanRummageCorpses{ get{ return true; } }
		public override bool BardImmune{ get{ return true; } }
		public override bool Unprovokable{ get{ return true; } }
		public override bool Uncalmable{ get{ return true; } }
		public override bool AutoDispel{ get{ return true; } }
		public override Poison PoisonImmune{ get{ return Poison.Deadly; } }
		public override Poison HitPoison{ get{ return Poison.Lesser; } }
		public override bool AlwaysMurderer{ get{ return true; } }
		
		public BaseWerewolf( Serial serial ) : base( serial )
		{
		}
		
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
			writer.Write((int)m_Rank);
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
			m_Rank = reader.ReadInt();
		}
	}
}