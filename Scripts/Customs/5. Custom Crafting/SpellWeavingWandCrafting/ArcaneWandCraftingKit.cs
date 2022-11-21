using System;
using Server;
using Server.Items;
using Server.Mobiles;
using Server.Spells;
using Server.ContextMenus;
using System.Collections.Generic;

namespace Server.Items
{
	[FlipableAttribute( 0x1EBA, 0x1EBB )]
	public class WandCraftingKit : Item
	{

		[Constructable]
		public WandCraftingKit() : base( 0x2D4A )
                {
                        Name = "Arcane Wand Crafting Kit";
			Weight = 15.0;
                        Hue = 1164;
                }

		public WandCraftingKit( Serial serial ) : base( serial )
		{
		}

               public override void OnDoubleClick(Mobile from)
                {

			if ( !IsChildOf( from.Backpack ) )
			{
				from.SendLocalizedMessage( 1042001 ); // That must be in your pack for you to use it.
			}

                        if ( from.Skills[SkillName.Spellweaving].Base < 50.0 )
			{
				from.SendLocalizedMessage( 1042594 ); // You do not understand how to use this.
			}

			if ( from.Skills[SkillName.Magery].Base < 90.0 )
			{
				from.SendLocalizedMessage( 1042594 ); // You do not understand how to use this.
			}

			Container pack = from.Backpack;

			if ( pack == null )
				return;

			int res = pack.ConsumeTotal(
				new Type[]
				{
					typeof( GoldIngot ),
					typeof( Emerald ),
					typeof( SpidersSilk ),
					typeof( SwitchItem ),
					typeof( LuminescentFungi ),
					typeof( ArcaneGem )
				},
				new int[]
				{
					1,
					1,
					1,
					1,
					1,
					1
				} );

			switch ( res )
			{
				case 0:
				{
					from.SendMessage( "You have no Gold Ingots" );
					break;
				}
				case 1:
				{
					from.SendMessage( "You have no Emeralds" );
					break;
				}
				case 2:
				{
					from.SendMessage( "You have no Spiders Silk" );
					break;
				}
				case 3:
				{
					from.SendMessage( "You have no Switch" );
					break;
				}
				case 4:
				{
					from.SendMessage( "You have no Luminescent Fungi" );
					break;
				}
				case 5:
				{
					from.SendMessage( "You have no Arcane Gems" );
					break;
				}
				default:
				{
                                          
                                        from.Backpack.AddItem( RandomSpellWeavingWand.CreateSpellWeavingWand());
                                        from.SendMessage("You create an Arcane Wand");
					from.PlaySound( 0x241 );
                                        this.Consume();

					break;
				}


			}
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