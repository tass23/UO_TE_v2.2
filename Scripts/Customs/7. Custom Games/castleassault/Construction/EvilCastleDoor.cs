using System;
using System.Collections;
using Server.Network;
using Server.Targeting;
using Server.Mobiles;

namespace Server.Items
{
	public class EvilCastleMetalDoor : BaseDoor
	{
		private static int MaxBreach = 100;

		private bool Breached;
		private int BreachTryCount;

		[CommandProperty( AccessLevel.GameMaster )]
		public int breachTryCount
		{
			get
			{
				return BreachTryCount;
			}
			set
			{
				if (value >= 0)
				{
					BreachTryCount = value;
				}
			}
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public bool breached
		{
			get
			{
				return Breached;
			}
			set
			{
				Breached = value;
			}
		}

		[Constructable]
		public EvilCastleMetalDoor( DoorFacing facing ) : base( 0x675 + (2 * (int)facing), 0x676 + (2 * (int)facing), 0xEC, 0xF3, BaseDoor.GetOffset( facing ) )
		{
		}

		public EvilCastleMetalDoor( Serial serial ) : base( serial )
		{
		}		

		public void Reset()
		{
			Breached = false;			
			BreachTryCount = 0;
		}

		public override void Use( Mobile from )
		{
			if ( !from.Player ) return;

			from.RevealingAction();

//			System.Console.WriteLine("DEBUG: Breach try...");

			Item item = from.FindItemOnLayer( Layer.Helm );

			if ( item is OrcishKinMask )
			{
				AOS.Damage( from, 50, 0, 100, 0, 0, 0 );
				item.Delete();
				from.FixedParticles( 0x36BD, 20, 10, 5044, EffectLayer.Head );
				from.PlaySound( 0x307 );
				from.SendMessage( "You were recognized!");
				return;
			}

			if ( from.AccessLevel >= AccessLevel.GameMaster )
			{
				from.LocalOverheadMessage( MessageType.Regular, 0x3B2, 502502 ); // That is locked, but you open it with your godly powers.
			}
			else
			{
				if(!Breached)
				{
					float chance;

					if (from.StatCap > 0)
					{
						chance = ((float)from.Str) / ((float)from.StatCap);
					}
					else
					{
						chance = 0.2f;
					}

					float rand = (float)Utility.RandomDouble(); 
	
					if (rand > chance )
					{
						if (from.Player) from.SendMessage( "You fail to even hurt the doors!" );
						return;
					}
					else
					{
						BreachTryCount++;

						int total = BreachTryCount;
	
						if (Link != null && Link is EvilCastleMetalDoor) total += ((EvilCastleMetalDoor)Link).breachTryCount;

						if (total >= MaxBreach)
						{
							if (from.Player) from.SendMessage( "You open the door!" );

							Breached = true;


							if (Link != null && Link is EvilCastleMetalDoor) ((EvilCastleMetalDoor)Link).breached = true;
						}
						else
						{
							if (from.Player) from.SendMessage( "You fail to open the door, but you sway it a little ({0}/{1})!",total,MaxBreach);

							return;
						}
					}
				}
			}

			if ( Open && !IsFreeToClose() ) return;

			if ( Open )
				OnClosed( from );
			else
				OnOpened( from );

			Open = !Open;

			BaseDoor link = this.Link;

			if ( Open && link != null && !link.Open ) link.Open = true;
		}

		public override void Serialize( GenericWriter writer ) // Default Serialize method
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
			writer.Write( Breached );
			writer.Write( BreachTryCount );
		}

		public override void Deserialize( GenericReader reader ) // Default Deserialize method
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
			Breached = reader.ReadBool();
			BreachTryCount = reader.ReadInt();
		}
	}
}