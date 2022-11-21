using System;
using Server.Network;
using Server.Prompts;
using Server.Mobiles;
using Server.Misc;
using Server.Items;
using Server.SkillHandlers;
using Server.Targeting;
using Server.Targets;
using Server.Engines.Craft;

namespace Server.Items
{
	public class BreweryLabelMaker : Item
	{
		private string m_BreweryName;

		[CommandProperty( AccessLevel.GameMaster )]
		public string BreweryName
		{
			get{ return m_BreweryName; }
			set{ m_BreweryName = value; InvalidateProperties(); }
		}

		[Constructable]
		public BreweryLabelMaker() : base( 0xFC0 )
		{
			Name = "Brewery Label Maker";
			Hue = 0x2E;
		}

		public BreweryLabelMaker( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );

			writer.Write( (string) m_BreweryName );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			m_BreweryName = reader.ReadString();
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( !IsChildOf( from.Backpack ) )
			{
				from.SendLocalizedMessage( 1042001 );

				return;
			}
			else
			{
				from.SendMessage( "Click on this label tool to name your brewery or a mead keg/bottle you crafted to add a new label." );
				from.Target = new RenameItemTarget( this );
			}
		}

		private class RenameItemTarget : Target
		{
			private BreweryLabelMaker m_LabelMaker;

			public RenameItemTarget( BreweryLabelMaker labelmaker ) : base( -1, false, TargetFlags.None )
			{
				m_LabelMaker = labelmaker;
			}

			protected override void OnTarget( Mobile from, object target )
			{
				if ( target is Mobile )
				{
					from.SendMessage( "Invalid Target.  Only crafted mead hegs, mead bottles, or this label maker can be labeled." );
				}
				else if ( target is Item )
				{
					Item item = (Item)target;

					if ( target == m_LabelMaker || target is BaseCraftMead || target is MeadKeg )
					{
						if( item.RootParent != from )
						{
							from.SendMessage( "The item must be in your pack to label it." );
						}
						else
						{
							if ( item is BaseCraftMead )
							{
								BaseCraftMead mead = (BaseCraftMead)item;
								if ( mead.Crafter != from )
								{
									from.SendMessage( "That bottle is either not worth labeling or was not crafted at your brewery!" );
								}
								else
								{
									from.SendMessage( "Enter label name now..." );
									from.Prompt = new LabelPrompt( item );
								}
							}
							else if ( item is MeadKeg )
							{
								MeadKeg keg = (MeadKeg)item;
								if (keg.Crafter != from )
								{
									from.SendMessage( "That keg is either not worth labeling or was not crafted at your brewery!" );
								}
								else
								{
									from.SendMessage( "Enter label name now..." );
									from.Prompt = new LabelPrompt( item );
								}
							}
							else
							{
								from.SendMessage( "Enter your brewery name now..." );
								from.Prompt = new LabelPrompt2( m_LabelMaker );
							}
						}
					}
					else
					{
						from.SendMessage( "Invalid Target.   Only crafted mead kegs, mead bottles, or this label maker can be labeled." );
					}
				}
				else
				{
					from.SendMessage( "Invalid Target.   Only crafted mead kegs, mead bottles, or this label maker can be labeled." );
				}
			}
		}

		private class LabelPrompt : Prompt
		{
			private Item m_Item;

			public LabelPrompt( Item item )
			{
				m_Item = item;
			}

			public override void OnResponse( Mobile from, string text )
			{
				m_Item.Name = text;
				from.SendMessage( "The mead has been labeled" );
			}
		}
		private class LabelPrompt2 : Prompt
		{
			private BreweryLabelMaker m_Item;

			public LabelPrompt2( BreweryLabelMaker item )
			{
				m_Item = item;
			}

			public override void OnResponse( Mobile from, string text )
			{
				m_Item.m_BreweryName = text;
				from.SendMessage( "As long as you keep this tool in your pack, all future mead you create will be labeled with this brewery name." );
			}
		}
	}
}