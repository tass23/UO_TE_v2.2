using System;
using System.Collections;
using System.Collections.Generic;
using Server;
using Server.Gumps;
using Server.Network;
using Server.Mobiles;
using Server.Targeting;
using Server.Commands.Generic;
using Server.Scripts;
using Server.Commands;

namespace Server.StableHelper
{
     public class StableHelper
     {
         public static void Initialize()
         {
             Register();
         }
         public static void Register()
         {
             CommandSystem.Register("ViewStabled", AccessLevel.GameMaster, new CommandEventHandler( ViewStabled_OnCommand ) );
         }
         [Usage( "ViewStabled" )]
         [Description( "Opens target's claim list." )]
         public static void ViewStabled_OnCommand( CommandEventArgs e )
         {
              Mobile m = e.Mobile;

              if( m == null )
                   return;

              m.Target = new StableTarget( m );
          }
     }

     public class StableTarget : Target
     {
          private Mobile m_Mobile;

          public StableTarget( Mobile from ) : base( 12, false, TargetFlags.None )
          {
               m_Mobile = from;
          }

          protected override void OnTarget( Mobile from, object targeted )
          {
               if( targeted is PlayerMobile )
               {
                    PlayerMobile pm = ( PlayerMobile )targeted;
                    
                    List<Mobile> list = pm.Stabled;
                    if( list.Count < 1 )
                    {
                         from.SendMessage( "This player does not have any pets stabled." );
                         return;
                    }
                    from.CloseGump( typeof( StabledGump ) );
                    from.SendGump( new StabledGump( from, pm, list ) );
                     
               }
               else
               {
                    from.SendMessage( "That is not a valid target, please select a Player." );
                    from.Target = new StableTarget( from );
               }
          }
    }
    
    public class StabledGump : Gump
    {
          private Mobile m_Targeted;
          private Mobile m_From;
          private List<Mobile> m_List;

          
          public StabledGump( Mobile from, Mobile targeted, List<Mobile> list ) : base( 50, 50 )
          {
	           m_From = from;
               m_Targeted = targeted;
               m_List = list;

               AddPage( 0 );

               AddBackground( 0, 0, 500, 50 + ( list.Count * 20 ), 9250 );
               AddAlphaRegion( 5, 5, 495, 40 + ( list.Count * 20 ) );

               AddHtml( 15, 15, 275, 20, "<BASEFONT COLOR=#FFFFFF>Select a pet to retrieve from the stables:</BASEFONT>", false, false );

               for( int i = 0; i < list.Count; ++i )
               {
                    BaseCreature pet = list[i] as BaseCreature;

                    if( pet == null || pet.Deleted )
                         continue;
		            string fullPetType = pet.GetType().ToString();
                    string petType = fullPetType.Substring( 15 ); //remove the Server.Mobile. from Type string
                    AddButton( 15, 39 + ( i * 20 ), 10006, 10006, i + 1, GumpButtonType.Reply, 0 );
                    AddHtml( 32, 35 + ( i * 20 ), 218, 18, String.Format( "<BASEFONT COLOR=#C0C0EE>{0}</BASEFONT>", pet.Name ), false, false );
		            AddHtml( 251, 35 + ( i * 20 ), 218, 18, String.Format( "<BASEFONT COLOR=#C0C0EE>{0}</BASEFONT>", petType ), false, false );
               }
          }

          public override void OnResponse( NetState sender, RelayInfo info )
          {
               int index = 0;
               if( info.ButtonID > 0 )
               {
                    index = info.ButtonID - 1;
               }
	          else
	          {
                    m_From.CloseGump( typeof( StabledGump ) );
		          return;
	          }
            
               BaseCreature pet = ( BaseCreature )m_List[index];

               pet.SetControlMaster( m_Targeted );

               if( pet.Summoned )
               pet.SummonMaster = m_Targeted;

               pet.ControlTarget = m_Targeted;
               pet.ControlOrder = OrderType.Follow;

               pet.MoveToWorld( m_Targeted.Location, m_Targeted.Map );

               pet.IsStabled = false;
               m_Targeted.Stabled.Remove( pet );
          }
     }
}