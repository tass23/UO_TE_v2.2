using System;
using System.Collections;
using System.Collections.Generic;
using Server.Multis;
using Server;
using Server.Items;
using Server.Targeting;
using Server.Mobiles;
using Server.Network;
using Server.Gumps;
using Server.Spells;

namespace Server.Items
{
	public class DarkBoneTable : Item
	{
		[Constructable]
		public DarkBoneTable() : base( 10844 )
		{
			Name = "Dark Bone Table";
			Hue = 1175;
			Weight = 1.0;
			Movable = true;
		}		

		public DarkBoneTable( Serial serial ) : base( serial )
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

	[Flipable( 10840, 10841 )]
	public class BoneThrone : Item
	{
		[Constructable]
		public BoneThrone() : base( 10840 )
		{
			Name = "Bone Throne";
			Hue = 1175;
			Weight = 1.0;
			Movable = true;
		}		

		public BoneThrone( Serial serial ) : base( serial )
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

	[Flipable( 10947, 10944 )]
	public class FountainOfBlood : Item
	{
		private DateTime m_NextUse;

		[Constructable]
		public FountainOfBlood() : base( 10947 )
		{
			Name = "Fountain of Blood";
			Hue = 1157;
			Weight = 1.0;
			Movable = true;
		}

		public override void OnDoubleClick( Mobile from )
		{
			from.SendMessage( 0, "Lock this down in your House and it will resurrect those nearby." );
		}

		public override bool HandlesOnMovement{ get{ return IsLockedDown; } }

		public override void OnMovement( Mobile m, Point3D oldLocation )
		{
			if ( IsLockedDown && !m.Alive && m.InRange( this.Location, 4 ) )
			{
				m.SendGump( new ResurrectGump( m, 1000 ));
			}

			base.OnMovement( m, oldLocation );
		}

		public FountainOfBlood( Serial serial ) : base( serial )
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

	public class DragonBrazier : BaseLight
	{
		private DateTime m_NextUse;
		public override int LitItemID{ get { return 6477; } }
		public override int UnlitItemID{ get { return 6478; } }
		
		[Constructable]
		public DragonBrazier() : base( 6478 )
		{
			Name = "Dragon Brazier";
			Light = LightType.Circle300;
			Weight = 1.0;
			Movable = true;
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( !from.InRange( this.GetWorldLocation(), 2 ) )
			{
				from.LocalOverheadMessage( MessageType.Regular, 0x3B2, 1019045 ); // I can't reach that.
			}
			
			else if ( IsLockedDown )
			{
				BaseHouse house = BaseHouse.FindHouseAt( from );
				
				if ( this.ItemID == 6478 && house.IsCoOwner( from ))
				{
					this.ItemID = 6477;
					from.SendMessage( 0, "You activate the Dragon Brazier." );
					from.PlaySound( 84 );
				}
				else if ( this.ItemID == 6477 && IsLockedDown && house.IsCoOwner( from ))
				{
					this.ItemID = 6478;
					from.PlaySound( 958 );
					from.SendMessage( 0, "You deactivate the Dragon brazier." );
				}
				else
					from.SendLocalizedMessage( 502436 ); // That is not accessible.
			}
			else
				from.SendMessage( 0, "You must lock this down in your house to use it." );
		}

		public override bool HandlesOnMovement{ get{ return IsLockedDown && IsOn(); } }

		public bool IsOn()
		{
			if ( this.ItemID == 6477 )
				return true;
			return false;
		}

		public override void OnMovement( Mobile m, Point3D oldLocation )
		{
			if ( IsLockedDown && this.ItemID == 6477 && m_NextUse < DateTime.Now && m.InRange( this.Location, 2 ))
			{
				m.SendMessage( 0, "The Dragon Brazier lends you Strength." );
				m.AddStatMod( new StatMod( StatType.Str, "Dragon Brazier", 45, TimeSpan.FromMinutes( 5.0 )));
				m_NextUse = DateTime.Now + TimeSpan.FromHours( 24.0 );
			}

			base.OnMovement( m, oldLocation );
		}

		public DragonBrazier( Serial serial ) : base( serial )
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

	public class MiniCoffin : Item
	{
		public static void Initialize()
		{
			EventSink.PlayerDeath += new PlayerDeathEventHandler(EventSink_Death);
		}

		[Constructable]
		public MiniCoffin() : base( 16142 )
		{
			Name = "Mini Coffin";
			Weight = 1.0;
			Movable = true;
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( this.IsChildOf( from.Backpack ))
				from.SendMessage( 0, "This item will resurrect you and your pets at full health when you die. One time use." );
		}

		private static void EventSink_Death(PlayerDeathEventArgs e)
		{
			PlayerMobile owner = e.Mobile as PlayerMobile;

			if (owner != null && !owner.Deleted)
			{
				if (owner.Alive)
					return;

				if (owner.Backpack == null || owner.Backpack.Deleted)
					return;

				MiniCoffin stone = owner.Backpack.FindItemByType(typeof(MiniCoffin)) as MiniCoffin;

				if (stone != null && !stone.Deleted)
				{
					new ResTimer( stone, owner ).Start();
					owner.SendMessage("Your Mini Coffin has been consumed.");
				}
			}
		}

		private class ResTimer : Timer
		{
			private MiniCoffin c;
			private Mobile from;

			public ResTimer( MiniCoffin co, Mobile f ) : base( TimeSpan.FromSeconds( 1.0 ))
			{
				c = co;
				from = f;
			}

			protected override void OnTick()
			{
				if ( c != null && from != null )
				{
					from.SendGump( new ResurrectGump( from, 10000 ));
					ArrayList petlist = new ArrayList();

					foreach ( Mobile m in World.Mobiles.Values )
					{
						if ( m is BaseCreature )
						{
							BaseCreature bc = (BaseCreature)m;

							if ( (bc.Controlled && bc.ControlMaster == from) || (bc.Summoned && bc.SummonMaster == from) )
								petlist.Add( bc );
						}
					}

					if ( petlist != null && petlist.Count > 0 )
					{
						for( int i = 0; i < petlist.Count; i++ )
						{
							Mobile m = (Mobile)petlist[i];

							if ( m is BaseCreature )
							{
								BaseCreature bc = m as BaseCreature;

								if ( bc.IsBonded == true && bc.Hits <= 0 )
									bc.ResurrectPet();
							} 
						}
					}

					c.Delete();
				}
			}
		}		

		public MiniCoffin( Serial serial ) : base( serial )
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

	public class HotHearthstone : Item
	{
		public static void Initialize()
		{
			EventSink.PlayerDeath += new PlayerDeathEventHandler(EventSink_Death);
		}

		private Point3D homeloc;
		private Map homemap;
		private int turnedon;

		[Constructable]
		public HotHearthstone() : base( 4968 )
		{
			Name = "Home of the Hearth stone";
			Weight = 1.0;
			Hue = 1766;
			Movable = true;
			LootType = LootType.Blessed;
			homeloc = new Point3D( 1435, 1698, 0 );
			homemap = Map.Felucca;
			turnedon = 1;
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( this.IsChildOf( from.Backpack ) && turnedon == 1 )
			{
				from.SendMessage( 0, "You deactivate the Home of the Hearth stone." );
				this.turnedon = 0;
			}

			if ( this.IsChildOf( from.Backpack ) && turnedon == 0 )
			{
				from.SendMessage( 0, "You activate the Home of the Hearth stone. When you die, you will be moved to a safe location, along with your Corpse, and any pets you have." );
				this.turnedon = 1;
			}
		}

		public void DoMove( Mobile from )
		{
			if ( this.turnedon == 0 )
				return;

			from.MoveToWorld( homeloc, homemap );

			if ( from.Corpse != null )
				from.Corpse.MoveToWorld( homeloc, homemap );

			ArrayList petlist = new ArrayList();

			foreach ( Mobile m in World.Mobiles.Values )
			{
				if ( m is BaseCreature )
				{
					BaseCreature bc = (BaseCreature)m;

					if ( (bc.Controlled && bc.ControlMaster == from) || (bc.Summoned && bc.SummonMaster == from) )
						petlist.Add( bc );
				}
			}

			if ( petlist != null && petlist.Count > 0 )
			{
				for( int i = 0; i < petlist.Count; i++ )
				{
					Mobile m = (Mobile)petlist[i];
					m.MoveToWorld( homeloc, homemap );
				}
			}

			this.Consume();
		}

		private static void EventSink_Death(PlayerDeathEventArgs e)
		{
			PlayerMobile owner = e.Mobile as PlayerMobile;

			if (owner != null && !owner.Deleted)
			{
				if (owner.Alive)
					return;

				if (owner.Backpack == null || owner.Backpack.Deleted)
					return;

				HotHearthstone stone = owner.Backpack.FindItemByType(typeof(HotHearthstone)) as HotHearthstone;

				if (stone != null && !stone.Deleted)
				{
					new ResTimer( stone, owner ).Start();
					owner.SendMessage("Your Home of the Hearth stone has been consumed.");
				}
			}
		}

		private class ResTimer : Timer
		{
			private HotHearthstone c;
			private Mobile from;

			public ResTimer( HotHearthstone co, Mobile f ) : base( TimeSpan.FromSeconds( 1.0 ))
			{
				c = co;
				from = f;
			}

			protected override void OnTick()
			{
				if ( c != null && from != null )
				{
					c.DoMove( from );
				}
			}
		}		

		public HotHearthstone( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );

			writer.Write( (Point3D) homeloc );
			writer.Write( (Map) homemap );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();

			homeloc = reader.ReadPoint3D();
			homemap = reader.ReadMap();
		}
	}

	public class HellstoneObelisk : Item
	{
		[Constructable]
		public HellstoneObelisk() : base( 13903 )
		{
			Name = "Hellstone Obelisk";
			Weight = 1.0;
			Movable = true;
			Hue = 1760;
		}		

		public HellstoneObelisk( Serial serial ) : base( serial )
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

	public class ShadowSpike : Item
	{
		[Constructable]
		public ShadowSpike() : base( 13900 )
		{
			Name = "Shadow Spike";
			Weight = 1.0;
			Movable = true;
			Hue = 1765;
		}		

		public ShadowSpike( Serial serial ) : base( serial )
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

	public class ShadowCrystal : Item
	{
		[Constructable]
		public ShadowCrystal() : base( 12252 )
		{
			Name = "Shadow Crystal";
			Weight = 1.0;
			Movable = true;
			Hue = 1765;
		}		

		public ShadowCrystal( Serial serial ) : base( serial )
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

	public class DarkDemonStatue : Item
	{
		[Constructable]
		public DarkDemonStatue() : base( 13899 )
		{
			Name = "Demon Statue";
			Weight = 1.0;
			Movable = true;
			Hue = 1760;
		}		

		public DarkDemonStatue( Serial serial ) : base( serial )
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

	[Flipable( 11764, 11763 )]
	public class ShadowBox : FillableContainer
	{
		[Constructable]
		public ShadowBox() : base( 11764 )
		{
			Name = "Shadow Box";
			Hue = 1765;
			Movable = true;
		}

		public ShadowBox( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.WriteEncodedInt( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadEncodedInt();

			if ( version == 0 && Weight == 3 )
				Weight = 1;
		}
	}

	public class DaishoOfTheDamned : Item
	{
		[Constructable]
		public DaishoOfTheDamned() : base( 10822 )
		{
			Name = "Daisho of the Damned";
			Weight = 1.0;
			Movable = true;
			Hue = 1756;
		}		

		public DaishoOfTheDamned( Serial serial ) : base( serial )
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

	public class TalismanOfFear : GoldBracelet
	{
		public override int ArtifactRarity{ get{ return 100; } }
		private int m_Charges;
		private DateTime m_NextRecharge;
		private DateTime m_NextUse;

		[Constructable]
		public TalismanOfFear() : base()
		{
			Name = "Talisman of Fear";
			ItemID = 12121;
			Layer = Layer.Talisman;
			Hue = 1771;
			Attributes.RegenStam = 6;
			Attributes.WeaponSpeed = 10;
			m_NextRecharge = DateTime.Now + TimeSpan.FromHours( 16 );
			m_Charges = 5;
			m_NextUse = DateTime.Now;
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( m_NextRecharge < DateTime.Now )
			{
				this.m_Charges = 5;
				m_NextRecharge = DateTime.Now + TimeSpan.FromHours( 16 );
			}

			if ( (this.IsChildOf( from.Backpack ) || from.FindItemOnLayer( Layer.Talisman ) is TalismanOfFear) && m_Charges > 0 && m_NextUse < DateTime.Now )
			{
				from.SendMessage( 0, "You activate the Talisman's magic. Charges left: {0}", m_Charges - 1 );
				from.PlaySound( 1480 );
				ArrayList alist = new ArrayList();
				IPooledEnumerable eable = from.Map.GetMobilesInRange( from.Location, 5 );

				foreach( Mobile m in eable )
					alist.Add( m );

				eable.Free();

				if ( alist != null && alist.Count > 0 )
				{
					for( int i = 0; i < alist.Count; i++ )
					{
						Mobile m = (Mobile)alist[i];

						if ( m is BaseCreature )
						{
							BaseCreature c = m as BaseCreature;

							if ( c.ControlMaster != null && (c.ControlMaster.Combatant != from && from.Combatant != c.ControlMaster))
							{
								c.AddStatMod( new StatMod( StatType.Str, "Talisman of Fear", -10, TimeSpan.FromSeconds( 5.0 ) ) );
								c.BeginFlee( TimeSpan.FromSeconds( 1.0 ));
							}

							if ( c.ControlMaster == null )
							{
								c.AddStatMod( new StatMod( StatType.Str, "Talisman of Fear", -10, TimeSpan.FromSeconds( 5.0 ) ) );
								c.BeginFlee( TimeSpan.FromSeconds( 1.0 ));
							}
						}
						else if ( from.Combatant == m || m.Combatant == from )
						{
							m.AddStatMod( new StatMod( StatType.Str, "Talisman of Fear", -10, TimeSpan.FromSeconds( 5.0 ) ) );
							m.SendMessage( 0, "You feel terrified of {0}!", from.Name );
						}
						else if ( m.Combatant == from || from.Combatant == m )
						{
							m.AddStatMod( new StatMod( StatType.Str, "Talisman of Fear", -10, TimeSpan.FromSeconds( 5.0 ) ) );
							m.SendMessage( 0, "You feel terrified of {0}!", from.Name );
						}
					}
				}
				m_NextUse = DateTime.Now + TimeSpan.FromMinutes( 15.0 );
				m_Charges -= 1;
			}
			else if ( (this.IsChildOf( from.Backpack ) || from.FindItemOnLayer( Layer.Talisman ) is TalismanOfFear) && this.m_Charges > 0 )
				from.SendMessage( 0, "You must wait for that to cool down. Time left: {0}", m_NextUse - DateTime.Now );
			else if ( this.IsChildOf( from.Backpack ) || from.FindItemOnLayer( Layer.Talisman ) is TalismanOfFear )
				from.SendMessage( 0, "That is out of charges. Time left to recharge: {0}", m_NextRecharge - DateTime.Now );
			else
				from.SendMessage( 0, "That must be either in your backpack or equipped to use." );
		}

		public TalismanOfFear( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );

			writer.Write( (int) m_Charges );
			writer.Write( (DateTime) m_NextUse );
			writer.Write( (DateTime) m_NextRecharge );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();

			m_Charges = reader.ReadInt();
			m_NextUse = reader.ReadDateTime();
			m_NextRecharge = reader.ReadDateTime();
		}
	}

	public class TalismanOfMagic : GoldBracelet
	{
		public override int ArtifactRarity{ get{ return 100; } }
		private int m_Charges;
		private DateTime m_NextRecharge;
		private DateTime m_NextUse;

		[Constructable]
		public TalismanOfMagic() : base()
		{
			Name = "Talisman of Magic";
			ItemID = 12120;
			Layer = Layer.Talisman;
			Hue = 1195;
			Attributes.RegenMana = 4;
			Attributes.BonusMana = 20;
			Attributes.CastRecovery = 2;
			Attributes.CastSpeed = 2;
			m_NextRecharge = DateTime.Now + TimeSpan.FromHours( 12 );
			m_Charges = 5;
			m_NextUse = DateTime.Now;
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( m_NextRecharge < DateTime.Now )
			{
				this.m_Charges = 5;
				m_NextRecharge = DateTime.Now + TimeSpan.FromHours( 12 );
			}

			if ( (this.IsChildOf( from.Backpack ) || from.FindItemOnLayer( Layer.Talisman ) is TalismanOfMagic) && m_Charges > 0 && m_NextUse < DateTime.Now )
			{
				from.SendMessage( 0, "You activate the Talisman's magic. Charges left: {0}", m_Charges - 1 );
				from.PlaySound( 1472 );
				from.Mana += 100;
				Effects.SendLocationParticles( EffectItem.Create( from.Location, from.Map, EffectItem.DefaultDuration ), 0x373A, 1, 17, 5, 7, 9914, 0 );
				Effects.SendLocationParticles( EffectItem.Create( from.Location, from.Map, EffectItem.DefaultDuration ), 0x376A, 1, 22, 67, 7, 9502, 0 );
				m_NextUse = DateTime.Now + TimeSpan.FromHours( 24.0 );
				m_Charges -= 1;
			}
			else if ( (this.IsChildOf( from.Backpack ) || from.FindItemOnLayer( Layer.Talisman ) is TalismanOfMagic) && this.m_Charges > 0 )
				from.SendMessage( 0, "You must wait for that to cool down. Time left: {0}", m_NextUse - DateTime.Now );
			else if ( this.IsChildOf( from.Backpack ) || from.FindItemOnLayer( Layer.Talisman ) is TalismanOfMagic )
				from.SendMessage( 0, "That is out of charges. Time left to recharge: {0}", m_NextRecharge - DateTime.Now );
			else
				from.SendMessage( 0, "That must be either in your backpack or equipped to use." );
		}

		public TalismanOfMagic( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );

			writer.Write( (int) m_Charges );
			writer.Write( (DateTime) m_NextUse );
			writer.Write( (DateTime) m_NextRecharge );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();

			m_Charges = reader.ReadInt();
			m_NextUse = reader.ReadDateTime();
			m_NextRecharge = reader.ReadDateTime();
		}
	}

	public class TalismanOfProtection : GoldBracelet
	{
		public override int ArtifactRarity{ get{ return 100; } }
		private int m_Charges;
		private DateTime m_NextRecharge;
		private DateTime m_NextUse;
		private DateTime m_ActiveUntil;
		private static Hashtable m_Table = new Hashtable();

		[Constructable]
		public TalismanOfProtection() : base()
		{
			Name = "Talisman of Protection";
			ItemID = 12122;
			Layer = Layer.Talisman;
			Hue = 1150;
			Resistances.Physical = 7;
			Resistances.Fire = 7;
			Resistances.Cold = 7;
			Resistances.Energy = 7;
			Resistances.Poison = 7;
			m_NextRecharge = DateTime.Now + TimeSpan.FromHours( 12 );
			m_Charges = 5;
			m_NextUse = DateTime.Now;
		}

		public virtual bool IsActive()
		{
			if ( this.m_ActiveUntil > DateTime.Now )
				return true;
			return false;
		}

		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );

			if ( m_Charges > 0 )
				list.Add( 1060741, m_Charges.ToString() ); // charges: ~1_val~
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( m_NextRecharge < DateTime.Now )
			{
				this.m_Charges = 5;
				m_NextRecharge = DateTime.Now + TimeSpan.FromHours( 12 );
			}

			if ( (this.IsChildOf( from.Backpack ) || from.FindItemOnLayer( Layer.Talisman ) is TalismanOfProtection) && m_Charges > 0 && m_NextUse < DateTime.Now )
			{
				from.SendMessage( 0, "You activate the Talisman's magic. Charges left: {0}", m_Charges - 1 );
				from.SendMessage( 0, "You feel more resistant to damage." );
				
				ResistanceMod[] mods = (ResistanceMod[])m_Table[from];

				if ( mods == null )
				{
					from.PlaySound( 0x1E9 );
					from.FixedParticles( 0x376A, 9, 32, 5008, EffectLayer.Waist );

					mods = new ResistanceMod[5]
					{
						new ResistanceMod( ResistanceType.Physical, +5 ),
						new ResistanceMod( ResistanceType.Fire, +5 ),
						new ResistanceMod( ResistanceType.Cold, +5 ),
						new ResistanceMod( ResistanceType.Poison, +5 ),
						new ResistanceMod( ResistanceType.Energy, +5 )
					};

					m_Table[from] = mods;

					for (int i = 0; i < mods.Length; ++i)
						from.AddResistanceMod(mods[i]);
					//from.ResistanceType.Physical += 5;
					//from.ResistanceType.Fire += 5;
					//from.ResistanceType.Cold += 5;
					//from.ResistanceType.Poison += 5;
					//from.ResistanceType.Energy += 5;
					from.PlaySound( 534 );
					m_ActiveUntil = DateTime.Now + TimeSpan.FromMinutes( 60.0 );
					m_NextUse = DateTime.Now + TimeSpan.FromHours( 24.0 );
					m_Charges -= 1;
				}
			}
			else if ( (this.IsChildOf( from.Backpack ) || from.FindItemOnLayer( Layer.Talisman ) is TalismanOfProtection) && this.m_Charges > 0 )
			{
				TimeSpan time = m_NextUse - DateTime.Now;
				if ( time.TotalHours > 0.0 )
					from.SendMessage(string.Format ("You must wait for that to cool down. Time left: {0} hours", ((int)time.TotalHours).ToString() ));
				//from.SendMessage( 0, "You must wait for that to cool down. Time left: {0}", m_NextUse - DateTime.Now );
			}
			else if ( this.IsChildOf( from.Backpack ) || from.FindItemOnLayer( Layer.Talisman ) is TalismanOfProtection )
			{
				TimeSpan rtime = m_NextRecharge - DateTime.Now;
				if ( rtime.TotalHours > 0.0 )
					from.SendMessage(string.Format ("That is out of charges. Time left to recharge: {0} hours", ((int)rtime.TotalHours).ToString() ));
				//from.SendMessage( 0, "That is out of charges. Time left to recharge: {0}", m_NextRecharge - DateTime.Now );
			}
			else
				from.SendMessage( 0, "That must be either in your backpack or equipped to use." );
		}

		public TalismanOfProtection( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
			writer.Write( (int) m_Charges );
			writer.Write( (DateTime) m_NextUse );
			writer.Write( (DateTime) m_NextRecharge );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
			m_Charges = reader.ReadInt();
			m_NextUse = reader.ReadDateTime();
			m_NextRecharge = reader.ReadDateTime();
		}
	}

	public class TalismanOfWar : GoldBracelet
	{
		public override int ArtifactRarity{ get{ return 100; } }
		private int m_Charges;
		private DateTime m_NextRecharge;
		private DateTime m_NextUse;
		private DateTime m_ActiveUntil;

		[Constructable]
		public TalismanOfWar() : base()
		{
			Name = "Talisman of War";
			ItemID = 12123;
			Layer = Layer.Talisman;
			Hue = 1157;
			
			Attributes.AttackChance = 20;
			Attributes.DefendChance = 20;
			m_NextRecharge = DateTime.Now + TimeSpan.FromHours( 16 );
			m_Charges = 5;
			m_NextUse = DateTime.Now;
		}

		public virtual bool IsActive()
		{
			if ( this.m_ActiveUntil > DateTime.Now )
				return true;
			return false;
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( m_NextRecharge < DateTime.Now )
			{
				this.m_Charges = 5;
				m_NextRecharge = DateTime.Now + TimeSpan.FromHours( 16 );
			}

			if ( (this.IsChildOf( from.Backpack ) || from.FindItemOnLayer( Layer.Talisman ) is TalismanOfWar) && m_Charges > 0 && m_NextUse < DateTime.Now )
			{
				from.SendMessage( 0, "You activate the Talisman's magic. Charges left: {0}", m_Charges - 1 );
				from.SendMessage( 0, "You feel as if you could deal more damage in combat." );
				from.PlaySound( 488 );
				m_ActiveUntil = DateTime.Now + TimeSpan.FromMinutes( 1.0 );
				m_NextUse = DateTime.Now + TimeSpan.FromMinutes( 5.0 );
				m_Charges -= 1;
			}
			else if ( (this.IsChildOf( from.Backpack ) || from.FindItemOnLayer( Layer.Talisman ) is TalismanOfWar) && this.m_Charges > 0 )
				from.SendMessage( 0, "You must wait for that to cool down. Time left: {0}", m_NextUse - DateTime.Now );
			else if ( this.IsChildOf( from.Backpack ) || from.FindItemOnLayer( Layer.Talisman ) is TalismanOfWar )
				from.SendMessage( 0, "That is out of charges. Time left to recharge: {0}", m_NextRecharge - DateTime.Now );
			else
				from.SendMessage( 0, "That must be either in your backpack or equipped to use." );
		}

		public TalismanOfWar( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
			writer.Write( (int) m_Charges );
			writer.Write( (DateTime) m_NextUse );
			writer.Write( (DateTime) m_NextRecharge );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
			m_Charges = reader.ReadInt();
			m_NextUse = reader.ReadDateTime();
			m_NextRecharge = reader.ReadDateTime();
		}
	}

	public class TalismanOfTheMoon : GoldBracelet
	{
		public override int ArtifactRarity{ get{ return 100; } }
		private int m_Charges;
		private DateTime m_NextRecharge;
		private DateTime m_NextUse;

		[Constructable]
		public TalismanOfTheMoon() : base()
		{
			Name = "Talisman of the Moon";
			ItemID = 12120;
			Layer = Layer.Talisman;
			Hue = 1779;
			
			Resistances.Fire = 3;
			Resistances.Cold = 3;
			Resistances.Energy = 3;
			Attributes.LowerManaCost = 20;
			m_NextRecharge = DateTime.Now + TimeSpan.FromHours( 16 );
			m_Charges = 5;
			m_NextUse = DateTime.Now;
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( m_NextRecharge < DateTime.Now )
			{
				this.m_Charges = 5;
				m_NextRecharge = DateTime.Now + TimeSpan.FromHours( 16 );
			}

			if ( (this.IsChildOf( from.Backpack ) || from.FindItemOnLayer( Layer.Talisman ) is TalismanOfTheMoon) && m_Charges > 0 && m_NextUse < DateTime.Now && from.Combatant != null )
			{
				from.SendMessage( 0, "You activate the Talisman's magic. Charges left: {0}", m_Charges - 1 );
				from.SendMessage( 0, "You paralyze your foe." );
				from.Combatant.Paralyzed = true;
				from.PlaySound( 488 );
				m_NextUse = DateTime.Now + TimeSpan.FromMinutes( 5.0 );
				m_Charges -= 1;
			}
			else if ( (this.IsChildOf( from.Backpack ) || from.FindItemOnLayer( Layer.Talisman ) is TalismanOfTheMoon) && this.m_Charges > 0 && from.Combatant != null )
				from.SendMessage( 0, "You must wait for that to cool down. Time left: {0}", m_NextUse - DateTime.Now );
			else if ( (this.IsChildOf( from.Backpack ) || from.FindItemOnLayer( Layer.Talisman ) is TalismanOfTheMoon) && from.Combatant != null )
				from.SendMessage( 0, "That is out of charges. Time left to recharge: {0}", m_NextRecharge - DateTime.Now );
			else if ( this.IsChildOf( from.Backpack ) || from.FindItemOnLayer( Layer.Talisman ) is TalismanOfTheMoon )
				from.SendMessage( 0, "You cannot use that ability outside of battle." );
			else
				from.SendMessage( 0, "That must be either in your backpack or equipped to use." );
		}

		public TalismanOfTheMoon( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
			writer.Write( (int) m_Charges );
			writer.Write( (DateTime) m_NextUse );
			writer.Write( (DateTime) m_NextRecharge );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
			m_Charges = reader.ReadInt();
			m_NextUse = reader.ReadDateTime();
			m_NextRecharge = reader.ReadDateTime();
		}
	}

	public class TalismanOfFlame : GoldBracelet
	{
		public override int ArtifactRarity{ get{ return 100; } }
		private int m_Charges;
		private DateTime m_NextRecharge;
		private DateTime m_NextUse;

		[Constructable]
		public TalismanOfFlame() : base()
		{
			Name = "Talisman of Flame";
			ItemID = 12121;
			Layer = Layer.Talisman;
			Hue = 1161;
			
			Resistances.Fire = 18;
			Resistances.Cold = 1;
			Attributes.AttackChance = 15;
			m_NextRecharge = DateTime.Now + TimeSpan.FromHours( 16 );
			m_Charges = 5;
			m_NextUse = DateTime.Now;
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( m_NextRecharge < DateTime.Now )
			{
				this.m_Charges = 5;
				m_NextRecharge = DateTime.Now + TimeSpan.FromHours( 16 );
			}

			if ( (this.IsChildOf( from.Backpack ) || from.FindItemOnLayer( Layer.Talisman ) is TalismanOfFlame) && m_Charges > 0 && m_NextUse < DateTime.Now && from.Combatant != null )
			{
				from.SendMessage( 0, "You activate the Talisman's magic. Charges left: {0}", m_Charges - 1 );
				from.SendMessage( 0, "You blast your foe with Fire." );
				AOS.Damage( from.Combatant, from, Utility.Random( 50, 100 ), true, 0, 100, 0, 0, 0 );
				from.Combatant.FixedParticles( 0x36BD, 20, 10, 5044, EffectLayer.Head );
				from.PlaySound( 519 );
				m_NextUse = DateTime.Now + TimeSpan.FromMinutes( 5.0 );
				m_Charges -= 1;
			}
			else if ( (this.IsChildOf( from.Backpack ) || from.FindItemOnLayer( Layer.Talisman ) is TalismanOfFlame) && this.m_Charges > 0 && from.Combatant != null )
				from.SendMessage( 0, "You must wait for that to cool down. Time left: {0}", m_NextUse - DateTime.Now );
			else if ( (this.IsChildOf( from.Backpack ) || from.FindItemOnLayer( Layer.Talisman ) is TalismanOfFlame) && from.Combatant != null )
				from.SendMessage( 0, "That is out of charges. Time left to recharge: {0}", m_NextRecharge - DateTime.Now );
			else if ( this.IsChildOf( from.Backpack ) || from.FindItemOnLayer( Layer.Talisman ) is TalismanOfFlame )
				from.SendMessage( 0, "You cannot use that ability outside of battle." );
			else
				from.SendMessage( 0, "That must be either in your backpack or equipped to use." );
		}

		public TalismanOfFlame( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
			writer.Write( (int) m_Charges );
			writer.Write( (DateTime) m_NextUse );
			writer.Write( (DateTime) m_NextRecharge );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
			m_Charges = reader.ReadInt();
			m_NextUse = reader.ReadDateTime();
			m_NextRecharge = reader.ReadDateTime();
		}
	}

	public class TalismanOfTheUnderworld : GoldBracelet
	{
		public override int ArtifactRarity{ get{ return 100; } }
		private int m_Charges;
		private DateTime m_NextRecharge;
		private DateTime m_NextUse;

		[Constructable]
		public TalismanOfTheUnderworld() : base()
		{
			Name = "Talisman of the Underworld";
			ItemID = 12123;
			Layer = Layer.Talisman;
			Hue = 1765;
			
			Attributes.CastRecovery = 2;
			Attributes.CastSpeed = 2;
			Attributes.AttackChance = 5;
			Attributes.DefendChance = 5;
			m_NextRecharge = DateTime.Now + TimeSpan.FromHours( 16 );
			m_Charges = 5;
			m_NextUse = DateTime.Now;
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( m_NextRecharge < DateTime.Now )
			{
				this.m_Charges = 5;
				m_NextRecharge = DateTime.Now + TimeSpan.FromHours( 16 );
			}

			if ( (this.IsChildOf( from.Backpack ) || from.FindItemOnLayer( Layer.Talisman ) is TalismanOfTheUnderworld) && m_Charges > 0 && m_NextUse < DateTime.Now && from.Combatant != null )
			{
				from.SendMessage( 0, "You activate the Talisman's magic. Charges left: {0}", m_Charges - 1 );
				from.SendMessage( 0, "You steal some of your foe's Life Essence." );
				from.Hits += AOS.Damage( from.Combatant, from, Utility.Random( 50, 100 ), 20, 20, 20, 20, 20 );
				Effects.SendLocationParticles( EffectItem.Create( from.Combatant.Location, from.Combatant.Map, EffectItem.DefaultDuration ), 0x373A, 1, 17, 1175, 7, 9914, 0 );
				Effects.SendLocationParticles( EffectItem.Create( from.Combatant.Location, from.Combatant.Map, EffectItem.DefaultDuration ), 0x376A, 1, 22, 1155, 7, 9502, 0 );
				from.PlaySound( 481 );
				m_NextUse = DateTime.Now + TimeSpan.FromMinutes( 5.0 );
				m_Charges -= 1;
			}
			else if ( (this.IsChildOf( from.Backpack ) || from.FindItemOnLayer( Layer.Talisman ) is TalismanOfTheUnderworld) && this.m_Charges > 0 && from.Combatant != null )
				from.SendMessage( 0, "You must wait for that to cool down. Time left: {0}", m_NextUse - DateTime.Now );
			else if ( (this.IsChildOf( from.Backpack ) || from.FindItemOnLayer( Layer.Talisman ) is TalismanOfTheUnderworld) && from.Combatant != null )
				from.SendMessage( 0, "That is out of charges. Time left to recharge: {0}", m_NextRecharge - DateTime.Now );
			else if ( this.IsChildOf( from.Backpack ) || from.FindItemOnLayer( Layer.Talisman ) is TalismanOfTheUnderworld )
				from.SendMessage( 0, "You cannot use that ability outside of battle." );
			else
				from.SendMessage( 0, "That must be either in your backpack or equipped to use." );
		}

		public TalismanOfTheUnderworld( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
			writer.Write( (int) m_Charges );
			writer.Write( (DateTime) m_NextUse );
			writer.Write( (DateTime) m_NextRecharge );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
			m_Charges = reader.ReadInt();
			m_NextUse = reader.ReadDateTime();
			m_NextRecharge = reader.ReadDateTime();
		}
	}
}