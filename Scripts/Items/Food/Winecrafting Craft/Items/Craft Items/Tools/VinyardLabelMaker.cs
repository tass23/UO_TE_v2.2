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
	public class VinyardLabelMaker : Item
	{
		private string m_VinyardName;

		[CommandProperty( AccessLevel.GameMaster )]
		public string VinyardName
		{
			get{ return m_VinyardName; }
			set{ m_VinyardName = value; InvalidateProperties(); }
		}

		[Constructable]
		public VinyardLabelMaker() : base( 0xFC0 )
		{
			Name = "Vinyard Label Maker";
			Hue = 0x218;
		}

		public VinyardLabelMaker( Serial serial ) : base( serial ){}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );

			writer.Write( (string) m_VinyardName );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			m_VinyardName = reader.ReadString();
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
				from.SendMessage( "Click on this label tool to name your vinyard or a wine keg/wine bottle you crafted to add a new label." );
				from.Target = new RenameItemTarget( this );
			}
		}

		private class RenameItemTarget : Target
		{
			private VinyardLabelMaker m_LabelMaker;

			public RenameItemTarget( VinyardLabelMaker labelmaker ) : base( -1, false, TargetFlags.None )
			{
				m_LabelMaker = labelmaker;
			}

			protected override void OnTarget( Mobile from, object target )
			{
				if ( target is Mobile )
				{
					from.SendMessage( "Invalid Target.  Only crafted Wine Kegs, Wine Bottles, or this label maker can be labeled." );
				}
				else if ( target is Item )
				{
					Item item = (Item)target;

					if ( target == m_LabelMaker || target is BaseCraftWine || target is WineKeg )
					{
						if( item.RootParent != from )
						{
							from.SendMessage( "The item must be in your pack to label it." );
						}
						else
						{
							if ( item is BaseCraftWine )
							{
								BaseCraftWine wine = (BaseCraftWine)item;
								if (wine.Crafter != from )
								{
									from.SendMessage( "That bottle is either not worth labeling or was not crafted at your vinyard!" );
								}
								else
								{
									from.SendMessage( "Enter label name now..." );
									from.Prompt = new LabelPrompt( item );
								}
							}
							else if ( item is WineKeg )
							{
								WineKeg keg = (WineKeg)item;
								if (keg.Crafter != from )
								{
									from.SendMessage( "That keg is either not worth labeling or was not crafted at your vinyard!" );
								}
								else
								{
									from.SendMessage( "Enter label name now..." );
									from.Prompt = new LabelPrompt( item );
								}
							}
							else
							{
								from.SendMessage( "Enter your vinyard name now..." );
								from.Prompt = new LabelPrompt2( m_LabelMaker );
							}
						}
					}
					else
					{
						from.SendMessage( "Invalid Target.   Only crafted Wine Kegs, Wine Bottles, or this label maker can be labeled." );
					}
				}
				else
				{
					from.SendMessage( "Invalid Target.   Only crafted Wine Kegs, Wine Bottles, or this label maker can be labeled." );
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
				from.SendMessage( "The wine has been labeled" );
			}
		}
		private class LabelPrompt2 : Prompt
		{
			private VinyardLabelMaker m_Item;

			public LabelPrompt2( VinyardLabelMaker item )
			{
				m_Item = item;
			}

			public override void OnResponse( Mobile from, string text )
			{
				m_Item.m_VinyardName = text;
				from.SendMessage( "As long as you keep this tool in your pack, all future wine you create will be labeled with this vinyard name." );
			}
		}
	}
}