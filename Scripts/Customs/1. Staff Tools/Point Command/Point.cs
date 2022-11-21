using System; 
using System.Text;
using Server; 
using Server.Network; 
using Server.Targeting;
using Server.Items;
using Server.Commands;

namespace Server.Commands 
{ 
	public class Point 
	{ 
		// Use [Point to point at anything you wish
		public static void Initialize() 
		{ 
			CommandSystem.Register( "Point", AccessLevel.Player, new CommandEventHandler( Point_OnCommand ) ); 
		} 

		public static void Point_OnCommand( CommandEventArgs e ) 
		{ 
			Mobile from = e.Mobile;
			from.Target = new PointTarget();
		} 
	}
   
	public class PointTarget : Target 
	{ 
		public PointTarget() : base( -1, true, TargetFlags.None ) 
		{
		}
        
		public PointTarget(Mobile from, Item targeted) : base( -1, true, TargetFlags.None ) 
		{
			from.PublicOverheadMessage(MessageType.Emote ,20, true, from.Name.ToString() + " Points There");
			targeted.PublicOverheadMessage(MessageType.Emote ,20,true, from.Name.ToString() + " Points Here"); 	
		}
        
		protected override void OnTarget( Mobile from, object targeted ) 
		{ 
			if ( from.Name == null)
			{
				from.SendMessage("Your name is not valid fix it now");
				return;
			}
           
			if ( targeted is Mobile )
			{ 
				Mobile m_target = (Mobile)targeted;
				from.PublicOverheadMessage(MessageType.Emote ,20, true,"*" + from.Name + " Points at*");
            	
				if ( m_target.Name != null)
					m_target.PublicOverheadMessage(MessageType.Emote ,20,true, "*" + m_target.Name + "*");
				else
					m_target.PublicOverheadMessage(MessageType.Emote ,20,true,"*"+ from.Name + " whatever it is!*");
			}
			else if ( targeted is Item ) 
			{ 
				Item m_target = (Item)targeted;
				from.PublicOverheadMessage(MessageType.Emote ,20, true,"*" + from.Name + " Points at*");
				if (m_target.Name != null)
					m_target.PublicOverheadMessage(MessageType.Emote ,20,true, "* " + m_target.Name + "*");
				else
					m_target.PublicOverheadMessage(MessageType.Emote ,20,true, "*Points Here*");
			}
			else 
			{
				IPoint3D p = targeted as IPoint3D; 

				if ( p != null ) 
				{
					Map map = from.Map;
					Item pointer = new Item (8302);
					Point3D m_point = new Point3D(p);
					pointer.MoveToWorld(m_point,map);
					pointer.Movable = false;
					PointTimer p_time = new PointTimer(pointer);
					from.PublicOverheadMessage(MessageType.Emote ,20, true, "*" + from.Name.ToString() + " Points at*");
					pointer.PublicOverheadMessage(MessageType.Emote ,20, true, "*This Spot*" );
				}
				else
				{ 
					from.SendMessage( "Cannot point at this for some reason!" ); 
				} 
			}     
		}
	}
	
	public class PointTimer : Timer
	{
		private Item m_item;

		public PointTimer( Item m) : base( TimeSpan.FromSeconds( 5.0 ) )// Change the Message delay here for static tiles
		{
			m_item = m;
			Start();
		}

		protected override void OnTick()
		{
			m_item.Delete();
			Stop();
		}
	}
}