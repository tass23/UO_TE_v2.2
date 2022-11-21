/**************************************
*Script Name: FastFeet                *
*Author: Joeku AKA Demortris          *
*For use with RunUO 2.0               *
*Client Tested with: 5.0.2b           *
*Version: 1.0                         *
*Initial Release: 06/27/06            *
*Revision Date: 06/27/06              *
**************************************/

using System;
using System.Collections;
using Server;
using Server.Network;

namespace Server.Items
{
	public enum RunEffect
	{
		None,
		FlameStrike,
		MagicArrow,
		Cure,
		Lightning,
		Teleport,
		Telekinesis,
		Poison,
		Explosion,
		PoisonField,
		FireField,
		EnergyField,
		WallOfStone,
		ParalyzeField,
		xX_Devious_Xx
	}

	public enum ShoeID
	{
		Shoes,
		Boots,
		ThighBoots,
		ArcaneThighBoots,
		FurBoots,
		Sandals,
		NinjaTabi,
		SamuraiTabi,
		ElvenBoots
	}

	public class FastFoot
	{
		private static FastFoot m_Instance = new FastFoot();
		public static FastFoot Instance { get { return m_Instance; } }
		private Hashtable m_FastRunners;
		public Hashtable FastRunners { get { return m_FastRunners; } set { m_FastRunners = value; } }

		public static void Initialize()
		{
			Instance.m_FastRunners = new Hashtable();

			foreach( Mobile m in World.Mobiles.Values )
			{
				if( m != null )
				{
					Item item = m.FindItemOnLayer( Layer.Shoes );

					if( item is FastFeet && (( FastFeet )item).RunEffect != RunEffect.None )
						Instance.FastRunners.Add( m.Serial, m );
				}
			}
			EventSink.Movement += new MovementEventHandler( EventSink_Movement );
			EventSink.Login += new LoginEventHandler( World_Login );
		}

		private static void EventSink_Movement( MovementEventArgs e )
		{
			Mobile m = e.Mobile;

			if( m == null )
				return;

			if( Instance.m_FastRunners.ContainsKey( m.Serial ) )
				PlayEffect( m );
		}

		private static void World_Login( LoginEventArgs args )
		{
			if ( args.Mobile != null )
			{
				Item item = args.Mobile.FindItemOnLayer( Layer.Shoes );
				if( item != null )
				{
					if( item is FastFeet && (( FastFeet )item).SpeedIncrease )
						args.Mobile.Send( SpeedControl.MountSpeed );
				}
			}
		}

		private static void PlayEffect( Mobile m )
		{
			if( m.FindItemOnLayer( Layer.Shoes ) != null && m.FindItemOnLayer( Layer.Shoes ) is FastFeet )
			{
				FastFeet feet = m.FindItemOnLayer( Layer.Shoes ) as FastFeet;

				switch( feet.RunEffect )
				{
					case RunEffect.FlameStrike:
						Effects.SendLocationParticles( EffectItem.Create( m.Location, m.Map, EffectItem.DefaultDuration ), 0x3709, 10, 30, 0 /*Instance.Hue*/, 0, 5052, 0 );
						Effects.PlaySound( m.Location, m.Map, 0x225 );
						break;

					case RunEffect.MagicArrow:
						foreach( Object obj in m.GetObjectsInRange( 10 ) )
						{
							if( obj != null && obj is Mobile )
							{
								m.MovingParticles( obj as IEntity, 0x36E4, 5, 0, false, true, 0 /*Instance.Hue*/, 0, 3006, 4006, 0, EffectLayer.Waist, 0 );
								m.PlaySound( 0x1E5 );
							}
						}
						break;

					case RunEffect.Cure:
						Effects.SendLocationParticles( EffectItem.Create( m.Location, m.Map, EffectItem.DefaultDuration ), 0x373A, 10, 15, 0 /*Instance.Hue*/, 0, 5012, 0 );
						Effects.PlaySound( m.Location, m.Map, 0x1E0 );
						break;

					case RunEffect.Lightning:
						foreach( Mobile obj in m.GetMobilesInRange( 10 ) )
						{
							if( obj != null )
								obj.BoltEffect( 0 );
						}
						break;

					case RunEffect.Teleport:
						Effects.SendLocationParticles( EffectItem.Create( m.Location, m.Map, EffectItem.DefaultDuration ), 0x3728, 10, 10, 0 /*Instance.Hue*/, 0, 2023, 0 );
						Effects.PlaySound( m.Location, m.Map, 0x1F5 );
						break;

					case RunEffect.Telekinesis:
						Effects.SendLocationParticles( EffectItem.Create( m.Location, m.Map, EffectItem.DefaultDuration ), 0x376A, 9, 32, 0 /*Instance.Hue*/, 0, 5022, 0 );
						Effects.PlaySound( m.Location, m.Map, 0x1FE );
						break;

					case RunEffect.Poison:
						Effects.SendLocationParticles( EffectItem.Create( m.Location, m.Map, EffectItem.DefaultDuration ), 0x374A, 10, 15, 0 /*Instance.Hue*/, 0, 5021, 0 );
						Effects.PlaySound( m.Location, m.Map, 0x474 );
						break;

					case RunEffect.Explosion:
						Effects.SendLocationParticles( EffectItem.Create( m.Location, m.Map, EffectItem.DefaultDuration ), 0x36BD, 20, 10, 0 /*Instance.Hue*/, 0, 5044, 0 );
						Effects.PlaySound( m.Location, m.Map, 0x474 );
						break;

					case RunEffect.xX_Devious_Xx:
						Effects.SendLocationParticles( EffectItem.Create( m.Location, m.Map, EffectItem.DefaultDuration ), 0x376A, 100, 10, 0 /*Instance.Hue*/, 0, 5555, 0 );
						Effects.PlaySound( m.Location, m.Map, 0x474 );
						break;
				}
			}
			else
				Instance.FastRunners.Remove( m.Serial );
		}
	}

	[Flipable]
	public class FastFeet : BaseShoes
	{
		private bool b_SpeedIncrease = true;
		private RunEffect e_RunEffect;
		private ShoeID e_ShoeID;

		[CommandProperty(AccessLevel.GameMaster)]
		public bool SpeedIncrease { get { return b_SpeedIncrease; } set { b_SpeedIncrease = value; InvalidateProperties(); } }
		[CommandProperty(AccessLevel.GameMaster)]
		public RunEffect RunEffect { get { return e_RunEffect; } set { e_RunEffect = value; } }
		[CommandProperty(AccessLevel.GameMaster)]
		public ShoeID ShoeID { get { return e_ShoeID; } set { e_ShoeID = value; ItemID = InterpretShoeID( value ); InvalidateProperties(); } }

		public override CraftResource DefaultResource{ get{ return CraftResource.RegularLeather; } }

		[Constructable]
		public FastFeet() : this( RandomRunEffect(), RandomShoeID() ){}

		[Constructable]
		public FastFeet( RunEffect effect, ShoeID id ) : base( InterpretShoeID( id ), 0 )
		{
			RunEffect = effect;
			ShoeID = id;
			Weight = 0;
		}

		public void Flip()
		{
			switch( ItemID )
			{
				case 0x170f: ItemID = 0x1710; break;	//Shoes
				case 0x1710: ItemID = 0x170f; break;
				case 0x170b: ItemID = 0x170c; break;	//Boots
				case 0x170c: ItemID = 0x170b; break;
				case 0x1711: ItemID = 0x1712; break;	//ThighBoots
				case 0x1712: ItemID = 0x1711; break;
				case 0x2307: ItemID = 0x2308; break;	//FurBoots
				case 0x2308: ItemID = 0x2307; break;
				case 0x170d: ItemID = 0x170e; break;	//Sandals
				case 0x170e: ItemID = 0x170d; break;
				case 0x2797: ItemID = 0x27E2; break;	//NinjaTabi
				case 0x27E2: ItemID = 0x2797; break;
				case 0x2796: ItemID = 0x27E1; break;	//SamuraiTabi
				case 0x27E1: ItemID = 0x2796; break;
				case 0x2FC4: ItemID = 0x317A; break;	//ElvenBoots
				case 0x317A: ItemID = 0x2FC4; break;
			}
		}

		public static RunEffect RandomRunEffect()
		{
			RunEffect effect = RunEffect.None;

			switch( Utility.Random( 14 ) )
			{
				case 1: effect = RunEffect.FlameStrike; break;
				case 2: effect = RunEffect.MagicArrow; break;
				case 3: effect = RunEffect.Cure; break;
				case 4: effect = RunEffect.Lightning; break;
				case 5: effect = RunEffect.Teleport; break;
				case 6: effect = RunEffect.Telekinesis; break;
				case 7: effect = RunEffect.Poison; break;
				case 8: effect = RunEffect.Explosion; break;
				case 9: effect = RunEffect.PoisonField; break;
				case 10: effect = RunEffect.FireField; break;
				case 11: effect = RunEffect.EnergyField; break;
				case 12: effect = RunEffect.WallOfStone; break;
				case 13: effect = RunEffect.ParalyzeField; break;
				case 14: effect = RunEffect.xX_Devious_Xx; break;
			}
			return effect;
		}

		public static ShoeID RandomShoeID()
		{
			ShoeID id = ShoeID.Shoes;

			switch( Utility.Random( 8 ) )
			{
				case 1: id = ShoeID.Boots; break;
				case 2: id = ShoeID.ThighBoots; break;
				case 3: id = ShoeID.ArcaneThighBoots; break;
				case 4: id = ShoeID.FurBoots; break;
				case 5: id = ShoeID.Sandals; break;
				case 6: id = ShoeID.NinjaTabi; break;
				case 7: id = ShoeID.SamuraiTabi; break;
				case 8: id = ShoeID.ElvenBoots; break;
			}
			return id;
		}

		public static int InterpretShoeID( ShoeID id )
		{
			int i = 0x170f; //ShoeID.Shoes

			switch( id )
			{
				case ShoeID.Boots: i = 0x170b; break;
				case ShoeID.ThighBoots: i = 0x1711; break;
				case ShoeID.ArcaneThighBoots: i = 0x26AF; break;
				case ShoeID.FurBoots: i = 0x2307; break;
				case ShoeID.Sandals: i = 0x170d; break;
				case ShoeID.NinjaTabi: i = 0x2797; break;
				case ShoeID.SamuraiTabi: i = 0x2796; break;
				case ShoeID.ElvenBoots: i = 0x2FC4; break;
			}
			return i;
		}

		private string NameString()
		{
			string name = this.Name;

			if ( name == null )
				name = String.Format( "#{0}", LabelNumber );

			return name;
		}

		public override void AddNameProperty( ObjectPropertyList list )
		{
			if ( ( RunEffect != RunEffect.None || SpeedIncrease ) && Name == null )
				list.Add( 1053099, "Magical\t{0}", NameString() ); // ~1_oretype~ ~2_armortype~
			else if ( ( RunEffect != RunEffect.None || SpeedIncrease ) && Name != null )
				list.Add( Name );
			else if ( Name == null )
				list.Add( LabelNumber );
			else
				list.Add( Name );
		}

		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );
			if( SpeedIncrease )
				list.Add( 1070809 ); // Increases movement speed.
		}

		public override bool OnEquip( Mobile from )
		{
			base.OnEquip( from );

			if( SpeedIncrease )
			{
				from.Send( SpeedControl.MountSpeed );
				from.SendMessage( "You feel the power of the footwear coursing through your veins!" );
			}

			if( RunEffect != RunEffect.None )
			{
				if( FastFoot.Instance.FastRunners.ContainsKey( from.Serial ) )
					FastFoot.Instance.FastRunners.Remove( from.Serial );
				FastFoot.Instance.FastRunners.Add( from.Serial, from );
				Console.WriteLine( "Add runner" );
			}
			return true;
		}

		public override void OnRemoved( object parent )
		{
			base.OnRemoved( parent );
			Mobile from;

			if( parent is Mobile )
			{
				from = parent as Mobile;

				if( SpeedIncrease )
				{
					from.Send( SpeedControl.Disable );
					from.SendMessage( "You feel the power of the footwear diminish..." );
				}

				if( FastFoot.Instance.FastRunners.ContainsKey( from.Serial ) )
					FastFoot.Instance.FastRunners.Remove( from.Serial );

				Console.WriteLine( "Remove runner" );
			}
		}

		public FastFeet( Serial serial ) : base( serial ){}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
			writer.Write( SpeedIncrease );
			writer.Write( ( int )RunEffect );
			writer.Write( ( int )ShoeID );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();

			switch ( version )
			{
				case 0:
					SpeedIncrease = reader.ReadBool();
					RunEffect = ( RunEffect )reader.ReadInt();
					ShoeID = ( ShoeID )reader.ReadInt();
					break;
			}
		}
	}
}