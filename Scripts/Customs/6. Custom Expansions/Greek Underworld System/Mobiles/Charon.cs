using System;
using System.Collections;
using Server;
using Server.Items;
using Server.Mobiles;
using Server.Network;

namespace Server.Mobiles
{
	[CorpseName( "a ghostly corpse" )]
	public class Charon : BaseCreature
	{
		public static void Initialize()
		{
			EventSink.Movement += new MovementEventHandler( EventSink_Movement );
		}

		private DateTime m_NextAbility;

		[Constructable]
		public Charon() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Blessed = true;
			Name = "Charon";
			Body = 400;
			this.Hue = 0;
			this.FacialHairItemID = 8268;
			this.FacialHairHue = 1175;

			SetStr( 200 );
			SetDex( 60 );
			SetInt( 100 );
			SetHits( 1000 );
			SetDamage( 15, 20 );
			SetDamageType( ResistanceType.Physical, 60 );
			SetResistance( ResistanceType.Physical, 60 );
			SetResistance( ResistanceType.Fire, 80 );
			SetResistance( ResistanceType.Cold, 60 );
			SetResistance( ResistanceType.Poison, 60 );
			SetResistance( ResistanceType.Energy, -50 );
			SetSkill( SkillName.MagicResist, 120.0 );
			SetSkill( SkillName.Tactics, 120.0 );
			SetSkill( SkillName.Wrestling, 120.0 );
			SetSkill( SkillName.Anatomy, 120.0 );
			AddItem( new Robe( 1157 ));
			AddItem( new WizardsHat( 1157 ));
			AddItem( new Sandals( 1818 ));
			AddItem( new BlackStaff() );

			Fame = 5000;
			Karma = -5000;
			VirtualArmor = 40;
		}

		public static void EventSink_Movement( MovementEventArgs args )
		{
			if ( args.Mobile is PlayerMobile && args.Mobile.InRange( new Point3D( 610, 1922, -91 ), 4 ))
				args.Mobile.SendGump( new UnderworldEnterGump() );
		}

		public override void OnThink()
		{
			this.Frozen = true;
			this.Blessed = true;
			this.Direction = Direction.South;
		}

		public override bool Unprovokable{ get{ return true; } }
		public override bool BleedImmune{ get{ return true; } }
		public override Poison PoisonImmune{ get{ return Poison.Lethal; } }

		public override void GenerateLoot()
		{
			AddLoot( LootPack.FilthyRich, 2 );
		}

		public Charon( Serial serial ) : base( serial )
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