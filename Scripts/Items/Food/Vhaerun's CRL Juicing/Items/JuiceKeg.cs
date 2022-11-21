using System;
using System.Collections;
using Server;
using Server.Network;
using Server.Engines.Craft;

namespace Server.Items
{
	public class JuiceKeg : Item, ICraftable
	{
		public static readonly TimeSpan CheckDelay = TimeSpan.FromSeconds( 1.0 );

		private int m_Held;
		private Mobile m_Crafter;
		private JuiceQuality m_Quality;
		private FruitsVariety m_Variety;
		private DateTime m_Start;
		private double m_BottleDuration;
		private bool m_AllowBottling;

		[CommandProperty( AccessLevel.GameMaster )]
		public int Held
		{
			get { return m_Held; }
			set
			{
				if ( m_Held != value )
				{
					this.Weight += (value - m_Held) * 0.8;
					m_Held = value;
					InvalidateProperties();
				}
			}
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public FruitsVariety Variety
		{
			get { return m_Variety; }
			set
			{
				if ( m_Variety != value )
				{
					m_Variety = value;
					InvalidateProperties();
				}
			}
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public Mobile Crafter
		{
			get{ return m_Crafter; }
			set{ m_Crafter = value; InvalidateProperties(); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public JuiceQuality Quality
		{
			get{ return m_Quality; }
			set{ m_Quality = value; InvalidateProperties(); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public bool AllowBottling
		{
			get
			{
				if ( !m_AllowBottling ) m_AllowBottling = ( 0 >= TimeSpan.Compare( TimeSpan.FromSeconds( m_BottleDuration ), DateTime.Now.Subtract( m_Start )));
				return m_AllowBottling;
			}
			set { m_AllowBottling = value; }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public double BottleDuration
		{
			get { return m_BottleDuration; }
			set { m_BottleDuration = value; }
		}

		private static void SetSaveFlag( ref SaveFlag flags, SaveFlag toSet, bool setIf )
		{
			if ( setIf ) flags |= toSet;
		}

		private static bool GetSaveFlag( SaveFlag flags, SaveFlag toGet )
		{
			return ( (flags & toGet) != 0 );
		}

		[Flags]
		private enum SaveFlag
		{
			None				= 0x00000000,
			Held				= 0x00000001,
			Crafter				= 0x00000002,
			Quality				= 0x00000004,
			Variety				= 0x00000008
		}

		public JuiceKeg( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 2 );

			writer.Write( (DateTime)m_Start );
			writer.Write( (double)m_BottleDuration );
			writer.Write( (bool)m_AllowBottling );
			SaveFlag flags = SaveFlag.None;

			SetSaveFlag( ref flags, SaveFlag.Held, m_Held != 0 );
			SetSaveFlag( ref flags, SaveFlag.Crafter, m_Crafter != null );
			SetSaveFlag( ref flags, SaveFlag.Quality, m_Quality != JuiceQuality.Regular );
			SetSaveFlag( ref flags, SaveFlag.Variety, m_Variety != DefaultVariety );

			writer.WriteEncodedInt( (int) flags );

			if ( GetSaveFlag( flags, SaveFlag.Held ) ) writer.Write( (int) m_Held );
			if ( GetSaveFlag( flags, SaveFlag.Crafter ) ) writer.Write( (Mobile) m_Crafter );
			if ( GetSaveFlag( flags, SaveFlag.Quality ) ) writer.WriteEncodedInt( (int) m_Quality );
			if ( GetSaveFlag( flags, SaveFlag.Variety ) ) writer.WriteEncodedInt( (int) m_Variety );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			switch ( version )
			{
				case 2:
				{
					m_Start = reader.ReadDateTime();
					m_BottleDuration = reader.ReadDouble();
					m_AllowBottling = reader.ReadBool();
					goto case 1;
				}
				case 1:
				{
					SaveFlag flags = (SaveFlag)reader.ReadEncodedInt();
					if ( GetSaveFlag( flags, SaveFlag.Held ) ) m_Held = reader.ReadInt();
					if ( GetSaveFlag( flags, SaveFlag.Crafter ) ) m_Crafter = reader.ReadMobile();
					if ( GetSaveFlag( flags, SaveFlag.Quality ) ) m_Quality = (JuiceQuality)reader.ReadEncodedInt();
					else m_Quality = JuiceQuality.Regular;
					if ( m_Quality == JuiceQuality.Low ) m_Quality = JuiceQuality.Regular;
					if ( GetSaveFlag( flags, SaveFlag.Variety ) ) m_Variety = ( FruitsVariety )reader.ReadEncodedInt();
					else m_Variety = DefaultVariety;
					if ( m_Variety == FruitsVariety.None ) m_Variety = DefaultVariety;
					break;
				}
			}
		}

		public virtual FruitsVariety DefaultVariety{ get{ return FruitsVariety.Apple; } }

		[Constructable]
		public JuiceKeg( ) : base( 0x1940 )
		{
			this.Weight = 1.0;
			m_Held = 75;
			m_Quality = JuiceQuality.Regular;
			m_Crafter = null;
			m_Variety = DefaultVariety;
			m_BottleDuration = 1.0;
			m_AllowBottling = false;
			m_Start = DateTime.Now;
		}

		public override void AddNameProperty( ObjectPropertyList list )
		{
			if (this.Name == null )
			{
				if ( m_Crafter != null ) list.Add( m_Crafter.Name+" Farms" );
				else list.Add( "Juice" );
			}
			else list.Add (this.Name);
		}

		public override void AddNameProperties( ObjectPropertyList list )
 		{
 			base.AddNameProperties( list );
			string JuiceType;
			JuiceType = JuicingResources.GetName( m_Variety );
			if ( m_Quality == JuiceQuality.Exceptional ) list.Add( 1060847, "Premium\t{0}", JuiceType );
 			else list.Add( 1060847, "\t{0}", JuiceType );
 		}

		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );
			int number;
			if ( m_Held <= 0 ) number = 502246;
			else if ( m_Held < 5 ) number = 502248;
			else if ( m_Held < 10 ) number = 502249;
			else if ( m_Held < 18 ) number = 502250;
			else if ( m_Held < 25 ) number = 502251;
			else if ( m_Held < 32 ) number = 502252;
			else if ( m_Held < 38 ) number = 502254;
			else if ( m_Held < 45 ) number = 502253;
			else if ( m_Held < 56 ) number = 502255;
			else if ( m_Held < 64 ) number = 502256;
			else if ( m_Held < 75 ) number = 502257;
			else number = 502258;
			list.Add( number );
		}

		public override void OnSingleClick( Mobile from )
		{
			base.OnSingleClick( from );

			int number;
			string JuiceType;

			if (this.Name == null )
			{
				if ( m_Crafter != null ) this.LabelTo( from, "{0} Farms", m_Crafter.Name );
				else this.LabelTo( from, "Juice" );
			}
			else this.LabelTo( from, "{0}", this.Name );
			JuiceType = JuicingResources.GetName( m_Variety );
			if ( m_Quality == JuiceQuality.Exceptional ) this.LabelTo( from, "Premium {0}", JuiceType );
 			else this.LabelTo( from, "{0}", JuiceType );
			if ( m_Held <= 0 ) number = 502246;
			else if ( m_Held < 5 ) number = 502248;
			else if ( m_Held < 10 ) number = 502249;
			else if ( m_Held < 18 ) number = 502250;
			else if ( m_Held < 25 ) number = 502251;
			else if ( m_Held < 32 ) number = 502252;
			else if ( m_Held < 38 ) number = 502254;
			else if ( m_Held < 45 ) number = 502253;
			else if ( m_Held < 56 ) number = 502255;
			else if ( m_Held < 64 ) number = 502256;
			else if ( m_Held < 75 ) number = 502257;
			else number = 502258;
			this.LabelTo( from, number );
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( AllowBottling )
			{
				if ( from.InRange( GetWorldLocation(), 2 ) )
				{
					if ( m_Held > 0 )
					{
						Container pack = from.Backpack;
						if ( pack != null && pack.ConsumeTotal( typeof( EmptyJuiceBottle ), 1 ) )
						{
							from.SendLocalizedMessage( 502242 );
							BaseCraftJuice Juice = FillBottle();
							Juice.Crafter = m_Crafter;
							Juice.Quality = m_Quality;
							Juice.Variety = m_Variety;
							if (this.Name != null) Juice.Name = this.Name;
							if ( pack.TryDropItem( from, Juice, false ) )
							{
								from.SendLocalizedMessage( 502243 );
								from.PlaySound( 0x240 );
								if ( --Held == 0 )
								{
									this.Delete();
									if ( GiveKeg( from ) ) from.SendMessage( "The Keg is empty and you clean it for reuse" );
									else from.SendMessage( "The Keg is now empty and cannot be reused." );
								}
							}
							else
							{
								from.SendLocalizedMessage( 502244 );
								Juice.Delete();
							}
						}
						else from.SendMessage( "You need a Juice Bottle to put the juice into." );
					}
					else from.SendLocalizedMessage( 502246 );
				}
				else from.LocalOverheadMessage( Network.MessageType.Regular, 0x3B2, 1019045 );
			}
			else from.SendMessage( "This keg is not ready to bottle yet, the fermentation process is not yet complete." );
		}

		public bool GiveKeg( Mobile m )
		{
			Container pack = m.Backpack;
			Keg keg = new Keg();
			if ( pack == null || !pack.TryDropItem( m, keg, false ) )
			{
				keg.Delete();
				return false;
			}
			return true;
		}

		public BaseCraftJuice FillBottle() { switch ( m_Variety ) { default: return new BottleOfJuice(); } }

		public static void Initialize() { TileData.ItemTable[0x1940].Height = 4; }

		#region ICraftable Members
		public int OnCraft( int quality, bool makersMark, Mobile from, CraftSystem craftSystem, Type typeRes, BaseTool tool, CraftItem craftItem, int resHue )
		{
			Held = 75;
			Quality = (JuiceQuality)quality;
			if ( Quality == JuiceQuality.Exceptional ) Crafter = from;
			Item[] items = from.Backpack.FindItemsByType( typeof( FarmLabelMaker ) );
			if ( items.Length != 0 )
			{
				foreach( FarmLabelMaker lm in items )
				{
					if (lm.FarmName != null)
					{
						this.Name = lm.FarmName;
						break;
					}
				}
			}

			Type resourceType = typeRes;
			if ( resourceType == null ) resourceType = craftItem.Resources.GetAt( 0 ).ItemType;
			Variety = JuicingResources.GetFromType( resourceType );
			CraftContext context = craftSystem.GetContext( from );
			Hue = 0;
			BottleDuration = 1.0;
			AllowBottling = false;
			m_Start = DateTime.Now;
			return quality;
		}
		#endregion
	}
}