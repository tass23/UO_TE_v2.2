using System;
using System.Collections.Generic;
using Server.Items;
using Server.ContextMenus;
using Server.Misc;
using Server.Network;
using Server;
using System.Data;
using Server.Engines.Publisher;

namespace Server.Mobiles
{
	public class Publisher: BaseVendor
	{
		#region Construction Logic

        private bool m_Sound = true;
        public new virtual bool PlaySound { get { return m_Sound; } set { m_Sound = value; } }

		[Constructable]
		public Publisher() : base( "the Publisher" )
		{
		}
		public Publisher( Serial serial ) : base( serial )
		{
		}
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}
		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
		#endregion

		#region SBInfo
		private List<SBInfo> m_SBInfos = new List<SBInfo>();
		protected override List<SBInfo> SBInfos{ get { return m_SBInfos; } }
		
		public override void InitSBInfo()
		{
			m_SBInfos.Add( new SBPublisher() );
		}
		#endregion

		public override NpcGuild NpcGuild{ get{ return NpcGuild.BardsGuild; } }
		public override bool HandlesOnSpeech( Mobile from )
		{
			if ( from.InRange( this.Location, 12 ) )
				return true;

			return base.HandlesOnSpeech( from );
		}
		public override void OnSpeech( SpeechEventArgs e )
		{
			// TODO: read keywords and respond with information on publishing

			if (e.Mobile.Criminal)
			{
				e.Handled = true;
				this.Say(500389); // I will not do business with a criminal!
				base.OnSpeech( e );
				return;
			}
			base.OnSpeech( e );
		}	
//		public override void AddCustomContextEntries( Mobile from, ArrayList list )
//		{
//			// view media
//
//			//if ( from.Alive )
//			//	list.Add( new OpenMedia( from, this ) );
//
//			base.AddCustomContextEntries( from, list );
//		}
		public override bool OnDragDrop(Mobile from, Item dropped)
		{
			BaseBook book = dropped as BaseBook;
			if(book != null)
				Publish(from, book);
			// else can't take it ...
			return base.OnDragDrop (from, dropped);
		}
		private void Publish(Mobile contributor, BaseBook book)
		{
			PublishedBook publishedBook = book as PublishedBook;
			if(publishedBook != null)
			{
				publishedBook.AddContributor(contributor.Name);
				Republish(contributor, publishedBook);
				return;
			}

			// change into published book
			publishedBook = new PublishedBook(book);
			publishedBook.AddContributor(contributor.Name);
			if(!publishedBook.IsPublishable())
			{
				publishedBook.Delete();
				this.SayTo(contributor, "You might want to try writing something of interest first.");
			}
			else if(XmlBook.Save(publishedBook))
			{
                switch (Utility.Random( 3 ) )
                {
                    case 0:
                    {
                        contributor.AddToBackpack(new Gold(1500, 3000));
                        this.SayTo(contributor, "This seems to be a excellent material. Let me see... hm... here - your royalty. You should see the story soon. I am sure it will sell well.");
                        contributor.SendMessage("You receive a good amount of gold.");
                        if (m_Sound)
                            Effects.PlaySound(contributor.Location, contributor.Map, 0x2E5);
                        contributor.AddToBackpack(publishedBook);
                        book.Delete();
                        break;
                    }

                    case 1:
                    {
                        contributor.AddToBackpack(new Gold(1000, 1250));
                        this.SayTo(contributor, "This seems to be decent material. Let me see... hm... here - your royalty. You should see the story soon. I hope it will sell well.");
                        contributor.SendMessage("You receive a decent amount of gold.");
                        if (m_Sound)
                            Effects.PlaySound(contributor.Location, contributor.Map, 0x2E5);
                        contributor.AddToBackpack(publishedBook);
                        book.Delete();
                        break;
                    }

                    case 2: 
                    {
                        contributor.AddToBackpack(new Gold(100, 500));
                        this.SayTo(contributor, "I don't know if this is good enough to be sold, but let me see... hm... here - I can't give you more for this. I am not sure it will sell at all.");
                        contributor.SendMessage("You receive some gold.");
                        if (m_Sound)
                            Effects.PlaySound(contributor.Location, contributor.Map, 0x2E5);
                        contributor.AddToBackpack(publishedBook);
                        book.Delete();
                        break;
                    }

                  
                }


            }
			else
			{
				publishedBook.Delete();
				this.SayTo(contributor, "Our machines are not working right. Check back later.");
			}
		}

		private void Republish(Mobile contributor, PublishedBook book)
		{
			if(!book.IsPublishable())
			{
				this.SayTo(contributor, "You might want to try writing something of interest first.");
			}
			else if(book.IsModified())
			{
				book.AddContributor(contributor.Name);
				if(XmlBook.Save(book))
				{
					book.RePublish();
					//contributor.AddToBackpack(new Gold (100));
					this.SayTo(contributor, "Your book appears to have changed. Let me publish that for you!");
                    //contributor.SendMessage("You receive some gold.");
                    //if (m_Sound)
                    //    Effects.PlaySound(contributor.Location, contributor.Map, 0x2E5);
				}
				else
				{
					this.SayTo(contributor, "Our machines are not working right. Check back later.");
				}
			}
			else
			{
				this.SayTo(contributor, "It looks the same as the last story we printed.");
			}
		}
	}
}
