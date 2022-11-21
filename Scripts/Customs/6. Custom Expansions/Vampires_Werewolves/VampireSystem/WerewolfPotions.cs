using System;
using Server;
using Server.Network;
using Server.Mobiles;
using Server.Misc;
using Server.Gumps;

namespace Server.Items 
{ 	
	public class LycanthropeCurePotion : Item
	{
		private int m_Quantity;
		[CommandProperty(AccessLevel.GameMaster)]
		public int Quantity
		{
			get { return m_Quantity; }
			set { m_Quantity = value; }
		}
		
		[Constructable] 
		public  LycanthropeCurePotion() :  base( 0xF08 ) 
		{ 
			Weight = 1.0; 			
			Name = "a Lycanthrope Cure"; 
			Hue = 101;
			m_Quantity = 10;
			Movable =  true;
            LootType = LootType.Regular;
		}
		
		public override void OnDoubleClick( Mobile from )
		{
			if (from is PlayerMobile)
			{
				PlayerMobile pm = (PlayerMobile) from;
				if (m_Quantity > 0 && (pm.Vampire < 1))
				{
					pm.PlaySound( Utility.RandomList( 0x2D6 ) );
					if(pm.Werewolf > 0)
					{
						pm.SendMessage(0, "Your head feels like it might be clearing...");
						pm.WerewolfBited = 0;
						pm.HueMod = -1;
						pm.BodyMod = 0;
						pm.Title = "";
						pm.FixedParticles( 0x375A, 10, 15, 5021, EffectLayer.Waist );
						pm.Werewolf = 0;
						pm.CloseGump( typeof(WerewolfGump));
					}
					else
					{
						pm.SendMessage(0, "You feel a sense of peace...");
						//pm.IsInnocent = true;
						//pm.Kills = 0;
					}
					m_Quantity -=1;
					InvalidateProperties();
					if (m_Quantity == 0)
					{
						this.Delete();
					}
				}
				else
					from.SendMessage("The bottle is empty");
			}
		}
		
		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );
			list.Add( GetQuantityDescription() );
		}
		
		public virtual int GetQuantityDescription()
		{
			int perc = ( m_Quantity * 100 ) / 10;

			if ( perc <= 0 )
				return 1042975; // It's empty.
			else if ( perc <= 33 )
				return 1042974; // It's nearly empty.
			else if ( perc <= 66 )
				return 1042973; // It's half full.
			else
				return 1042972; // It's full.
		}
		
		public LycanthropeCurePotion( Serial serial ) : base( serial )
		{
		}
		
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version

			writer.Write( (int) m_Quantity);
		}
		
		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();

			m_Quantity = reader.ReadInt();
		}
	}
}

namespace Server.Items 
{ 	
	public class LycanthropePotion : Item
	{
		private int m_Quantity;

		[CommandProperty(AccessLevel.GameMaster)]
		public int Quantity
		{
			get { return m_Quantity; }
			set { m_Quantity = value; }
		}
		
		[Constructable]
		public  LycanthropePotion() :  base( 0xF08 ) 
		{ 
			Weight = 1.0; 			
			Name = "a Lycanthrope potion"; 
			Hue = 98;
			m_Quantity = 10;
			Movable =  true;
            LootType = LootType.Regular;
		}
		
		public override void OnDoubleClick( Mobile from )
		{
			if (from is PlayerMobile)
			{
				PlayerMobile pm = (PlayerMobile) from;
				if (m_Quantity > 0 && (pm.Vampire < 1))
				{
					pm.PlaySound( Utility.RandomList( 0x2D6 ) ); //0x31 and 0x4CC might be wrong
					if(pm.Vampire > 0)
					{
						pm.SendMessage("You are already a Vampire. You cannot be both.");
					}
					else
					{
						if(pm.Werewolf > 0)
						{
							pm.WerewolfBiteTime = TimeSpan.FromHours(1.0);
							//pm.HueMod = 0x847E;
							pm.Hits += 5;
							pm.Stam += 5;
						}
						else
						{
							pm.SendMessage(33, "You feel strange...");
							pm.Werewolf = 1;
							//pm.FixedParticles(0x3709, 1, 30, 9904, 1108, 6, EffectLayer.Head);
							pm.FixedParticles( 0x374A, 10, 15, 5021, EffectLayer.Waist );
							pm.HueMod = 0x847E;
							pm.WerewolfBiteTime = TimeSpan.FromHours(2.0);
							pm.Title = "the Werewolf";
							pm.AddStatMod(new StatMod(StatType.Str, "Werewolf Str Bonus", 20, TimeSpan.Zero));
							pm.AddStatMod(new StatMod(StatType.Dex, "Werewolf Dex Bonus", 5, TimeSpan.Zero));
							pm.AddStatMod(new StatMod(StatType.Int, "Werewolf Int Bonus", 10, TimeSpan.Zero));
							pm.SendGump( new WerewolfGump());
						}
						if (m_Quantity >0) m_Quantity -=1;
						InvalidateProperties();
						if (m_Quantity == 0)
						{
							//EmptyWineBottle eb = new EmptyWineBottle();
							//eb.MoveToWorld(this.Location, this.Map);
							//this.Delete();
						}
					}
				}
				else
					from.SendMessage("The bottle is empty");
			}
		}
		
		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );
			list.Add( GetQuantityDescription() );
		}
		
		public virtual int GetQuantityDescription()
		{
			int perc = ( m_Quantity * 100 ) / 10;

			if ( perc <= 0 )
				return 1042975; // It's empty.
			else if ( perc <= 33 )
				return 1042974; // It's nearly empty.
			else if ( perc <= 66 )
				return 1042973; // It's half full.
			else
				return 1042972; // It's full.
		}
		
		public LycanthropePotion( Serial serial ) : base( serial )
		{
		}
		
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version

			writer.Write( (int) m_Quantity);
		}
		
		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();

			m_Quantity = reader.ReadInt();
		}
	}
}