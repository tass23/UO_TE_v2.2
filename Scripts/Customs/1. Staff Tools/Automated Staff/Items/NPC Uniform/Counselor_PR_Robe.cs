
#region Automated Server Staff Acknowledgements
/*
    This Automated NPC System Idea Originated
    With A Script Coded By: Tresdni from the
    RunUO (www.runuo.com) Forums; It Is Named:
    
       **Completely Automated Staff Team** 
 http://www.runuo.com/community/threads/completely-automated-staff-team-oh-yes-i-did.460720/
 
    This Released Version Of The Script Named
    Above Is My Own Variation On What I Think
    Might Complete The System Tresdni Started.
    
    The Code In This Script File Is Annoted. I
    Have Regioned Out Most Areas And Outlined
    Others So That You Know What Code Can Be
    Copy And Pasted To Other Scripts To Add The
    Same Functionality For Another System. 
 
    The Author Of Each Line Of Code Varies, I
    Got The Shell Of This Script From Tresdni,
    However A Lot Has Come From Many Other 
    Sources Over The Years; I Have A Library
    Of Annotated Methods I've Been Working On,
    That Help Me Build The Scripts I Upload.
 
    A Special Thank You Goes Out To The Following
    People For Helping Me Complete This System
    Addition To The Completely Automated Staff Team,
    Written By: Tresdni:
 
    THANK YOU GUYS!! THE HELP WAS MUCH APPRECIATED
                   -Sythen (A.A.R)-
    ______________________________________________
    ** JamzeMcC | Morexton | Soteric | James420 **
    ----------------------------------------------
 */
#endregion Edited By: A.A.R

using System;
using Server;

namespace Server.Items
{
	public class Counselor_PR_Robe : BaseSuit
	{
		[Constructable]
		public Counselor_PR_Robe() : base( AccessLevel.Counselor, 0x3, 0x204F )
		{
            Movable = false;
		}

		public Counselor_PR_Robe( Serial serial ) : base( serial )
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
	}
}