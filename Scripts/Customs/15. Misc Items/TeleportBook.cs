using System; 
using Server;
using Server.Misc;
using Server.Spells;
using Server.Regions;
using Server.Items;
using Server.Gumps; 
using Server.Network; 
using Server.Menus; 
using Server.Menus.Questions; 

namespace Server.Gumps
{
	public class TeleportBookGump : Gump
	{
		// TODO: 
			bool playerslounge = false;		// does this server have a players lounge?
			bool checkred = false;			// ignore murderer status for travel?
			bool tramInterest = true;		// show trammel points of Interest?
			bool tramcity = true;			// allow travel to trammel cities?
			bool felcity = true;			// allow travel to felluca cities?
			bool tokucity = true;			// allow travel to tokuno?
			bool malascity = true;			// allow travel to malas?
			bool tramdung = false;			// allow transportation to tram dung entrances?
			bool feldung = false;			// allow transportation to fel dungeon entrances?
			bool malasInterest = true;		// show malas parts after luna/umbra/doom?
			bool felInterest = true;			// show felucca points of Interest?
			bool ilishdung = false;			// show ilish dungeons and forts page?
			bool ilishshrine = false;		// show ilshenar shrines + cities?
			bool showtramshrines = false;		// show trammel shrines?
			bool showfelshrines = false;		// show felucca shrines?
			bool showtramL2 = false;			// show trammel dungeon L2 points
			bool showtramL3 = false;			// show trammel dungeon L3 points
			bool showfelL2 = false;			// show felucca dungeon L2 points
			bool showfelL3 = false;			// show felucca dungeon L3 points
		public static bool allowfeldungeonuse = false;	// allow use even in fel ruleset dungeons?
			bool hideonteleport = false;		// hide players on teleport?
			bool checkvalidtravel = true;		// check for murderer/combat/criminal?
			bool allowcriminaltravel = false;	// allow/disallow criminals to travel? dependant upon checkvalidtravel
			bool allowcombattravel = false;		// allow/disallow players in combat to travel? dependant upon checkvalidtravel
			bool allowsigiltravel = false;		// allow players with the sigil in their pack to use the book?
			bool preventoverweighttravel = true;	// prevent overweight players from using the book?
		public static TimeSpan	CombatHeatDelay = TimeSpan.FromSeconds( 30.0 );	// combat delay
		public DateTime gumpOpened = DateTime.MinValue;
		public TimeSpan	travelbookdelay = TimeSpan.FromMinutes( 1 );

		public TeleportBookGump( Mobile owner ) : base( 150, 60 )
		{
			gumpOpened = DateTime.Now;
			bool tramrules = false;
			bool felrules = false;
			bool ilshrules = false;
			bool malasrules = false;
			bool tokurules = false;
			bool notramtravel = false;

			if ( Map.Trammel.Rules == MapRules.TrammelRules )
				tramrules = true;

			if ( Map.Felucca.Rules == MapRules.TrammelRules )
				felrules = true;

			if ( Map.Ilshenar.Rules == MapRules.TrammelRules )
				ilshrules = true;

			if ( Map.Malas.Rules == MapRules.TrammelRules )
				malasrules = true;

			if ( Map.Tokuno.Rules == MapRules.TrammelRules )
				tokurules = true;

			if ( owner.AccessLevel == AccessLevel.Player ) {
				if ( checkred == false && owner.Kills < 5 ) { notramtravel = true; } }
				else { notramtravel = true; }

			Closable=true;
			Disposable=true;
			Dragable=true;
			Resizable=false;

			AddPage(1);
			AddBackground(0, 0, 330, 260, 5054);

			if ( tramcity && ( ( tramrules && notramtravel ) || !tramrules ) ) {
			AddLabel(10, 10, 0, "Trammel Cities");
			AddButton(200, 12, 2361, 2361, 1, GumpButtonType.Page, 2); }

			if ( tramdung && ( ( tramrules && notramtravel ) || !tramrules ) ) {
			AddLabel(10, 30, 0, "Trammel Dungeons");
			AddButton(200, 32, 2361, 2361, 2, GumpButtonType.Page, 3); }

			if ( tramInterest && ( ( tramrules && notramtravel ) || !tramrules ) ) {
			AddLabel(10, 50, 0, "Trammel Points of Interest");
			AddButton(200, 52, 2361, 2361, 3, GumpButtonType.Page, 4); }

			if ( felcity && ( ( felrules && notramtravel ) || !felrules ) ) {
			AddLabel(10, 70, 0, "Felucca Cities");
			AddButton(200, 72, 2361, 2361, 4, GumpButtonType.Page, 5); }

			if ( feldung && ( ( felrules && notramtravel ) || !felrules ) ) {
			AddLabel(10, 90, 0, "Felucca Dungeons");
			AddButton(200, 92, 2361, 2361, 5, GumpButtonType.Page, 6); }

			if ( felInterest &&( ( felrules && notramtravel ) || !felrules ) ) {
			AddLabel(10, 110, 0, "Felucca Points of Interest");
			AddButton(200, 112, 2361, 2361, 6, GumpButtonType.Page, 7); }

			if ( ilishshrine && ( ( ilshrules && notramtravel ) || !ilshrules ) ) {
			AddLabel(10, 130, 0, "Ilish Shrines & Cities");
			AddButton(200, 132, 2361, 2361, 7, GumpButtonType.Page, 8); }

			if ( ilishdung && ( ( ilshrules && notramtravel ) || !ilshrules ) ) {
			AddLabel(10, 150, 0, "Ilish Dungeons + Forts");
			AddButton(200, 152, 2361, 2361, 8, GumpButtonType.Page, 9); }

			if ( malascity && ( ( malasrules && notramtravel ) || !malasrules ) ) {
			AddLabel(10, 170, 0, "Malas & Points of Interest");
			AddButton(200, 172, 2361, 2361, 9, GumpButtonType.Page, 10); }

			if ( tokucity && ( ( tokurules && notramtravel ) || !tokurules ) ) {
			AddLabel(10, 190, 0, "Tokuno & Points of Interest");
			AddButton(200, 192, 2361, 2361, 10, GumpButtonType.Page, 11); }

			if ( showtramshrines && ( ( tramrules && notramtravel ) || !tramrules ) ) {
			AddLabel(10, 210, 0, "Trammel Shrines");
			AddButton(200, 212, 2361, 2361, 11, GumpButtonType.Page, 12); }

			if ( showfelshrines && ( ( felrules && notramtravel ) || !felrules ) ) {
			AddLabel(10, 230, 0, "Felucca Shrines");
			AddButton(200, 232, 2361, 2361, 12, GumpButtonType.Page, 13); }

			if ( playerslounge ) {
			AddLabel(230, 125, 0, "Players Lounge");
			AddButton(240, 150, 1149, 1148, 13, GumpButtonType.Reply, 14); }

			AddButton(240, 75, 5508, 5509, 14, GumpButtonType.Page, 14);
			AddButton(240, 225, 1028, 1027, 0, GumpButtonType.Reply, 0);
			AddImage(255, 15, 113);

			AddPage(2);
			AddBackground(0, 0, 310, 355, 5054);
			AddLabel(10, 10, 0, "Trammel Towns");
			AddLabel(10, 30, 0, "Britian");
			AddLabel(10, 50, 0, "Cove");
			AddLabel(10, 70, 0, "Delucia");
			AddLabel(10, 90, 0, "Haven");
			AddLabel(10, 110, 0, "Jhelom");
			AddLabel(10, 130, 0, "Magincia");
			AddLabel(10, 150, 0, "Minoc");
			AddLabel(10, 170, 0, "Moonglow");
			AddLabel(10, 190, 0, "Nujel 'm");
			AddLabel(10, 210, 0, "Papua");
			AddLabel(10, 230, 0, "Serpents Hold");
			AddLabel(10, 250, 0, "Skara Brae");
			AddLabel(10, 270, 0, "Trinsic");
			AddLabel(10, 290, 0, "Vesper");
			AddLabel(10, 310, 0, "Wind");
			AddLabel(10, 330, 0, "Yew");
			AddLabel(260, 10, 0, "Center");
			AddLabel(190, 10, 0, "Banks");
			AddButton(200, 32, 2361, 2361, 101, GumpButtonType.Reply, 0);
			AddButton(200, 52, 2361, 2361, 102, GumpButtonType.Reply, 0);
			AddButton(200, 72, 2361, 2361, 103, GumpButtonType.Reply, 0);
			AddButton(200, 92, 2361, 2361, 104, GumpButtonType.Reply, 0);
			AddButton(200, 112, 2361, 2361, 105, GumpButtonType.Reply, 0);
			AddButton(200, 132, 2361, 2361, 106, GumpButtonType.Reply, 0);
			AddButton(200, 152, 2361, 2361, 107, GumpButtonType.Reply, 0);
			AddButton(200, 172, 2361, 2361, 108, GumpButtonType.Reply, 0);
			AddButton(200, 192, 2361, 2361, 109, GumpButtonType.Reply, 0);
			AddButton(200, 212, 2361, 2361, 110, GumpButtonType.Reply, 0);
			AddButton(200, 232, 2361, 2361, 111, GumpButtonType.Reply, 0);
			AddButton(200, 252, 2361, 2361, 112, GumpButtonType.Reply, 0);
			AddButton(200, 272, 2361, 2361, 113, GumpButtonType.Reply, 0);
			AddButton(200, 292, 2361, 2361, 114, GumpButtonType.Reply, 0);
			AddButton(200, 312, 2361, 2361, 115, GumpButtonType.Reply, 0);
			AddButton(200, 332, 2361, 2361, 116, GumpButtonType.Reply, 0);
			AddButton(270, 32, 2361, 2361, 117, GumpButtonType.Reply, 0);
			AddButton(270, 52, 2361, 2361, 118, GumpButtonType.Reply, 0);
			AddButton(270, 72, 2361, 2361, 119, GumpButtonType.Reply, 0);
			AddButton(270, 92, 2361, 2361, 120, GumpButtonType.Reply, 0);
			AddButton(270, 112, 2361, 2361, 121, GumpButtonType.Reply, 0);
			AddButton(270, 132, 2361, 2361, 122, GumpButtonType.Reply, 0);
			AddButton(270, 152, 2361, 2361, 123, GumpButtonType.Reply, 0);
			AddButton(270, 172, 2361, 2361, 124, GumpButtonType.Reply, 0);
			AddButton(270, 192, 2361, 2361, 125, GumpButtonType.Reply, 0);
			AddButton(270, 212, 2361, 2361, 126, GumpButtonType.Reply, 0);
			AddButton(270, 232, 2361, 2361, 127, GumpButtonType.Reply, 0);
			AddButton(270, 252, 2361, 2361, 128, GumpButtonType.Reply, 0);
			AddButton(270, 272, 2361, 2361, 129, GumpButtonType.Reply, 0);
			AddButton(270, 292, 2361, 2361, 130, GumpButtonType.Reply, 0);
			AddButton(270, 312, 2361, 2361, 131, GumpButtonType.Reply, 0);
			AddButton(270, 332, 2361, 2361, 132, GumpButtonType.Reply, 0);

			AddPage(3);
			AddBackground(0, 0, 310, 355, 5054);
			AddLabel(10, 10, 0, "Trammel Dungeons");
			AddLabel(140, 10, 0, "Ent");
			AddLabel(10, 40, 0, "Covetus");
			AddLabel(10, 60, 0, "Deciet");
			AddLabel(10, 80, 0, "Despise");
			AddLabel(10, 100, 0, "Destard");
			AddLabel(10, 120, 0, "Hythloth");
			AddLabel(10, 140, 0, "Shame");
			AddLabel(10, 160, 0, "Wrong");
			AddLabel(10, 200, 0, "Fire");
			AddLabel(10, 220, 0, "Ice");
			AddLabel(10, 240, 0, "Fire Temple");
			AddLabel(10, 260, 0, "Terathan Keep");
			AddButton(150, 42, 2361, 2361, 201, GumpButtonType.Reply, 0);
			AddButton(150, 62, 2361, 2361, 202, GumpButtonType.Reply, 0);
			AddButton(150, 82, 2361, 2361, 203, GumpButtonType.Reply, 0);
			AddButton(150, 102, 2361, 2361, 204, GumpButtonType.Reply, 0);
			AddButton(150, 122, 2361, 2361, 205, GumpButtonType.Reply, 0);
			AddButton(150, 142, 2361, 2361, 206, GumpButtonType.Reply, 0);
			AddButton(150, 162, 2361, 2361, 207, GumpButtonType.Reply, 0);
			AddButton(150, 202, 2361, 2361, 208, GumpButtonType.Reply, 0);
			AddButton(150, 222, 2361, 2361, 209, GumpButtonType.Reply, 0);
			AddButton(150, 242, 2361, 2361, 210, GumpButtonType.Reply, 0);
			AddButton(150, 262, 2361, 2361, 211, GumpButtonType.Reply, 0);

			if ( showtramL2 == true) {
			AddLabel(190, 10, 0, "Lev 2");
			AddButton(200, 42, 2361, 2361, 221, GumpButtonType.Reply, 0);
			AddButton(200, 62, 2361, 2361, 222, GumpButtonType.Reply, 0);
			AddButton(200, 82, 2361, 2361, 223, GumpButtonType.Reply, 0);
			AddButton(200, 102, 2361, 2361, 224, GumpButtonType.Reply, 0);
			AddButton(200, 122, 2361, 2361, 225, GumpButtonType.Reply, 0);
			AddButton(200, 142, 2361, 2361, 226, GumpButtonType.Reply, 0);
			AddButton(200, 162, 2361, 2361, 227, GumpButtonType.Reply, 0); }

			if ( showtramL3 == true ) {
			AddLabel(250, 10, 0, "Lev 3");
			AddButton(260, 42, 2361, 2361, 241, GumpButtonType.Reply, 0);
			AddButton(260, 62, 2361, 2361, 242, GumpButtonType.Reply, 0);
			AddButton(260, 82, 2361, 2361, 243, GumpButtonType.Reply, 0);
			AddButton(260, 102, 2361, 2361, 244, GumpButtonType.Reply, 0);
			AddButton(260, 122, 2361, 2361, 245, GumpButtonType.Reply, 0);
			AddButton(260, 142, 2361, 2361, 246, GumpButtonType.Reply, 0);
			AddButton(260, 162, 2361, 2361, 247, GumpButtonType.Reply, 0); }

			AddPage(4);
			AddBackground(0, 0, 310, 355, 5054);
			AddLabel(65, 10, 0, "Trammel Points of Interest");
			AddLabel(10, 40, 0, "Buccaneer's Den");
			AddLabel(10, 60, 0, "Cove Orc Fort");
			AddLabel(10, 80, 0, "Fishermans Hut");
			AddLabel(10, 100, 0, "Great Waterfall");
			AddLabel(10, 120, 0, "Heart Clearing");
			AddLabel(10, 140, 0, "Hedge Maze");
			AddLabel(10, 160, 0, "Hidden Valley");
			AddLabel(10, 180, 0, "Island Temple");
			AddLabel(10, 200, 0, "Marble Island");
			AddLabel(10, 220, 0, "Ophidian Fort");
			AddLabel(10, 240, 0, "Statue & Bridge");
			//AddLabel(10, 260, 0, "Wind Park");
			AddLabel(10, 280, 0, "Yew Brigands");
			AddLabel(10, 300, 0, "Yew Orc Fort");
			AddLabel(10, 320, 0, "Yew Crypts");
			AddButton(190, 42, 2361, 2361, 301, GumpButtonType.Reply, 0);
			AddButton(190, 62, 2361, 2361, 302, GumpButtonType.Reply, 0);
			AddButton(190, 82, 2361, 2361, 303, GumpButtonType.Reply, 0);
			AddButton(190, 102, 2361, 2361, 304, GumpButtonType.Reply, 0);
			AddButton(190, 122, 2361, 2361, 305, GumpButtonType.Reply, 0);
			AddButton(190, 142, 2361, 2361, 306, GumpButtonType.Reply, 0);
			AddButton(190, 162, 2361, 2361, 307, GumpButtonType.Reply, 0);
			AddButton(190, 182, 2361, 2361, 308, GumpButtonType.Reply, 0);
			AddButton(190, 202, 2361, 2361, 309, GumpButtonType.Reply, 0);
			AddButton(190, 222, 2361, 2361, 310, GumpButtonType.Reply, 0);
			AddButton(190, 242, 2361, 2361, 311, GumpButtonType.Reply, 0);
			//AddButton(190, 262, 2361, 2361, 312, GumpButtonType.Reply, 0);
			AddButton(190, 282, 2361, 2361, 313, GumpButtonType.Reply, 0);
			AddButton(190, 302, 2361, 2361, 314, GumpButtonType.Reply, 0);
			AddButton(190, 322, 2361, 2361, 315, GumpButtonType.Reply, 0);

			AddPage(5);
			AddBackground(0, 0, 310, 355, 5054);
			AddLabel(10, 10, 0, "Felucca Towns");
			AddLabel(10, 30, 0, "Britian");
			AddLabel(10, 50, 0, "Cove");
			AddLabel(10, 70, 0, "Delucia");
			AddLabel(10, 90, 0, "Occlo");
			AddLabel(10, 110, 0, "Jhelom");
			//AddLabel(10, 130, 0, "Magincia");
			AddLabel(10, 150, 0, "Minoc");
			AddLabel(10, 170, 0, "Moonglow");
			AddLabel(10, 190, 0, "Nujel 'm");
			AddLabel(10, 210, 0, "Papua");
			AddLabel(10, 230, 0, "Serpents Hold");
			AddLabel(10, 250, 0, "Skara Brae");
			AddLabel(10, 270, 0, "Trinsic");
			AddLabel(10, 290, 0, "Vesper");
			AddLabel(10, 310, 0, "Wind");
			AddLabel(10, 330, 0, "Yew");
			AddLabel(260, 10, 0, "Center");
			AddLabel(190, 10, 0, "Banks");
			AddButton(200, 32, 2361, 2361, 401, GumpButtonType.Reply, 0);
			AddButton(200, 52, 2361, 2361, 402, GumpButtonType.Reply, 0);
			AddButton(200, 72, 2361, 2361, 403, GumpButtonType.Reply, 0);
			AddButton(200, 92, 2361, 2361, 404, GumpButtonType.Reply, 0);
			AddButton(200, 112, 2361, 2361, 405, GumpButtonType.Reply, 0);
			//AddButton(200, 132, 2361, 2361, 406, GumpButtonType.Reply, 0);
			AddButton(200, 152, 2361, 2361, 407, GumpButtonType.Reply, 0);
			AddButton(200, 172, 2361, 2361, 408, GumpButtonType.Reply, 0);
			AddButton(200, 192, 2361, 2361, 409, GumpButtonType.Reply, 0);
			AddButton(200, 212, 2361, 2361, 410, GumpButtonType.Reply, 0);
			AddButton(200, 232, 2361, 2361, 411, GumpButtonType.Reply, 0);
			AddButton(200, 252, 2361, 2361, 412, GumpButtonType.Reply, 0);
			AddButton(200, 272, 2361, 2361, 413, GumpButtonType.Reply, 0);
			AddButton(200, 292, 2361, 2361, 414, GumpButtonType.Reply, 0);
			AddButton(200, 312, 2361, 2361, 415, GumpButtonType.Reply, 0);
			AddButton(200, 332, 2361, 2361, 416, GumpButtonType.Reply, 0);
			AddButton(270, 32, 2361, 2361, 417, GumpButtonType.Reply, 0);
			AddButton(270, 52, 2361, 2361, 418, GumpButtonType.Reply, 0);
			AddButton(270, 72, 2361, 2361, 419, GumpButtonType.Reply, 0);
			AddButton(270, 92, 2361, 2361, 420, GumpButtonType.Reply, 0);
			AddButton(270, 112, 2361, 2361, 421, GumpButtonType.Reply, 0);
			//AddButton(270, 132, 2361, 2361, 422, GumpButtonType.Reply, 0);
			AddButton(270, 152, 2361, 2361, 423, GumpButtonType.Reply, 0);
			AddButton(270, 172, 2361, 2361, 424, GumpButtonType.Reply, 0);
			AddButton(270, 192, 2361, 2361, 425, GumpButtonType.Reply, 0);
			AddButton(270, 212, 2361, 2361, 426, GumpButtonType.Reply, 0);
			AddButton(270, 232, 2361, 2361, 427, GumpButtonType.Reply, 0);
			AddButton(270, 252, 2361, 2361, 428, GumpButtonType.Reply, 0);
			AddButton(270, 272, 2361, 2361, 429, GumpButtonType.Reply, 0);
			AddButton(270, 292, 2361, 2361, 430, GumpButtonType.Reply, 0);
			AddButton(270, 312, 2361, 2361, 431, GumpButtonType.Reply, 0);
			AddButton(270, 332, 2361, 2361, 432, GumpButtonType.Reply, 0);

			AddPage(6);
			AddBackground(0, 0, 310, 355, 5054);
			AddLabel(10, 10, 0, "Felucca Dungeons");
			AddLabel(140, 10, 0, "Ent");
			AddLabel(10, 40, 0, "Covetus");
			AddLabel(10, 60, 0, "Deciet");
			AddLabel(10, 80, 0, "Despise");
			AddLabel(10, 100, 0, "Destard");
			AddLabel(10, 120, 0, "Hythloth");
			AddLabel(10, 140, 0, "Shame");
			AddLabel(10, 160, 0, "Wrong");
			AddLabel(10, 200, 0, "Fire");
			AddLabel(10, 220, 0, "Ice");
			AddLabel(10, 240, 0, "Fire Temple");
			AddLabel(10, 260, 0, "Terathan Keep");
			AddButton(150, 42, 2361, 2361, 501, GumpButtonType.Reply, 0);
			AddButton(150, 62, 2361, 2361, 502, GumpButtonType.Reply, 0);
			AddButton(150, 82, 2361, 2361, 503, GumpButtonType.Reply, 0);
			AddButton(150, 102, 2361, 2361, 504, GumpButtonType.Reply, 0);
			AddButton(150, 122, 2361, 2361, 505, GumpButtonType.Reply, 0);
			AddButton(150, 142, 2361, 2361, 506, GumpButtonType.Reply, 0);
			AddButton(150, 162, 2361, 2361, 507, GumpButtonType.Reply, 0);
			AddButton(150, 202, 2361, 2361, 508, GumpButtonType.Reply, 0);
			AddButton(150, 222, 2361, 2361, 509, GumpButtonType.Reply, 0);
			AddButton(150, 242, 2361, 2361, 510, GumpButtonType.Reply, 0);
			AddButton(150, 262, 2361, 2361, 511, GumpButtonType.Reply, 0);

			if ( showfelL2 == true ) {
			AddLabel(190, 10, 0, "Lev 2");
			AddButton(200, 42, 2361, 2361, 521, GumpButtonType.Reply, 0);
			AddButton(200, 62, 2361, 2361, 522, GumpButtonType.Reply, 0);
			AddButton(200, 82, 2361, 2361, 523, GumpButtonType.Reply, 0);
			AddButton(200, 102, 2361, 2361, 524, GumpButtonType.Reply, 0);
			AddButton(200, 122, 2361, 2361, 525, GumpButtonType.Reply, 0);
			AddButton(200, 142, 2361, 2361, 526, GumpButtonType.Reply, 0);
			AddButton(200, 162, 2361, 2361, 527, GumpButtonType.Reply, 0); }

			if ( showfelL3 == true ) {
			AddLabel(250, 10, 0, "Lev 3");
			AddButton(260, 42, 2361, 2361, 541, GumpButtonType.Reply, 0);
			AddButton(260, 62, 2361, 2361, 542, GumpButtonType.Reply, 0);
			AddButton(260, 82, 2361, 2361, 543, GumpButtonType.Reply, 0);
			AddButton(260, 102, 2361, 2361, 544, GumpButtonType.Reply, 0);
			AddButton(260, 122, 2361, 2361, 545, GumpButtonType.Reply, 0);
			AddButton(260, 142, 2361, 2361, 546, GumpButtonType.Reply, 0);
			AddButton(260, 162, 2361, 2361, 547, GumpButtonType.Reply, 0); }


			AddPage(7);
			AddBackground(0, 0, 310, 355, 5054);
			AddLabel(65, 10, 0, "Felucca Points of Interest");
			AddLabel(10, 40, 0, "Buccaneer's Den");
			AddLabel(10, 60, 0, "Cove Orc Fort");
			AddLabel(10, 80, 0, "Fishermans Hut");
			AddLabel(10, 100, 0, "Great Waterfall");
			AddLabel(10, 120, 0, "Heart Clearing");
			AddLabel(10, 140, 0, "Hedge Maze");
			AddLabel(10, 160, 0, "Hidden Valley");
			AddLabel(10, 180, 0, "Island Temple");
			AddLabel(10, 200, 0, "Marble Island");
			AddLabel(10, 220, 0, "Ophidian Fort");
			AddLabel(10, 240, 0, "Statue & Bridge");
			AddLabel(10, 260, 0, "Wind Park");
			AddLabel(10, 280, 0, "Yew Brigands");
			AddLabel(10, 300, 0, "Yew Orc Fort");
			AddLabel(10, 320, 0, "Yew Crypts");
			AddButton(190, 42, 2361, 2361, 601, GumpButtonType.Reply, 0);
			AddButton(190, 62, 2361, 2361, 602, GumpButtonType.Reply, 0);
			AddButton(190, 82, 2361, 2361, 603, GumpButtonType.Reply, 0);
			AddButton(190, 102, 2361, 2361, 604, GumpButtonType.Reply, 0);
			AddButton(190, 122, 2361, 2361, 605, GumpButtonType.Reply, 0);
			AddButton(190, 142, 2361, 2361, 606, GumpButtonType.Reply, 0);
			AddButton(190, 162, 2361, 2361, 607, GumpButtonType.Reply, 0);
			AddButton(190, 182, 2361, 2361, 608, GumpButtonType.Reply, 0);
			AddButton(190, 202, 2361, 2361, 609, GumpButtonType.Reply, 0);
			AddButton(190, 222, 2361, 2361, 610, GumpButtonType.Reply, 0);
			AddButton(190, 242, 2361, 2361, 611, GumpButtonType.Reply, 0);
			AddButton(190, 262, 2361, 2361, 612, GumpButtonType.Reply, 0);
			AddButton(190, 282, 2361, 2361, 613, GumpButtonType.Reply, 0);
			AddButton(190, 302, 2361, 2361, 614, GumpButtonType.Reply, 0);
			AddButton(190, 322, 2361, 2361, 615, GumpButtonType.Reply, 0);

			AddPage(8);
			AddBackground(0, 0, 310, 355, 5054);
			AddLabel(10, 10, 0, "Ilish Shrines & Cities");
			AddLabel(10, 40, 0, "Chaos");
			AddLabel(10, 60, 0, "Compassion");
			AddLabel(10, 80, 0, "Honesty");
			AddLabel(10, 100, 0, "Honor");
			AddLabel(10, 120, 0, "Humility");
			AddLabel(10, 140, 0, "Justice");
			AddLabel(10, 160, 0, "Sacrifice");
			AddLabel(10, 180, 0, "Spirituality");
			AddLabel(10, 200, 0, "Valor");
			AddLabel(10, 220, 0, "Lakeshire");
			AddLabel(10, 240, 0, "Gargoyle City");
			AddLabel(10, 260, 0, "Mistas");
			AddLabel(10, 280, 0, "Montor");

			AddButton(190, 42, 2361, 2361, 701, GumpButtonType.Reply, 0);
			AddButton(190, 62, 2361, 2361, 702, GumpButtonType.Reply, 0);
			AddButton(190, 82, 2361, 2361, 703, GumpButtonType.Reply, 0);
			AddButton(190, 102, 2361, 2361, 704, GumpButtonType.Reply, 0);
			AddButton(190, 122, 2361, 2361, 705, GumpButtonType.Reply, 0);
			AddButton(190, 142, 2361, 2361, 706, GumpButtonType.Reply, 0);
			AddButton(190, 162, 2361, 2361, 707, GumpButtonType.Reply, 0);
			AddButton(190, 182, 2361, 2361, 708, GumpButtonType.Reply, 0);
			AddButton(190, 202, 2361, 2361, 709, GumpButtonType.Reply, 0);
			AddButton(190, 222, 2361, 2361, 710, GumpButtonType.Reply, 0);
			AddButton(190, 242, 2361, 2361, 711, GumpButtonType.Reply, 0);
			AddButton(190, 262, 2361, 2361, 712, GumpButtonType.Reply, 0);
			AddButton(190, 282, 2361, 2361, 713, GumpButtonType.Reply, 0);

			AddPage(9);
			AddBackground(0, 0, 310, 355, 5054);
			AddLabel(10, 10, 0, "Ilish Dungeons + Forts");
			AddLabel(10, 40, 0, "Anchient Lair");
			AddLabel(10, 60, 0, "Ankh");
			AddLabel(10, 80, 0, "Blackthorn's Castle");
			AddLabel(10, 100, 0, "Blood");
			AddLabel(10, 120, 0, "Cyclops");
			AddLabel(10, 140, 0, "Elemental Arena");
			AddLabel(10, 160, 0, "Exodus");
			AddLabel(10, 180, 0, "Lizardman Fort");
			AddLabel(10, 200, 0, "Ratman Fort");
			AddLabel(10, 220, 0, "Rock");
			AddLabel(10, 240, 0, "Savage Camp");
			AddLabel(10, 260, 0, "Sorcerer");
			AddLabel(10, 280, 0, "Spectre");
			AddLabel(10, 300, 0, "Wisp");
			AddButton(190, 42, 2361, 2361, 801, GumpButtonType.Reply, 0);
			AddButton(190, 62, 2361, 2361, 802, GumpButtonType.Reply, 0);
			AddButton(190, 82, 2361, 2361, 803, GumpButtonType.Reply, 0);
			AddButton(190, 102, 2361, 2361, 804, GumpButtonType.Reply, 0);
			AddButton(190, 122, 2361, 2361, 805, GumpButtonType.Reply, 0);
			AddButton(190, 142, 2361, 2361, 806, GumpButtonType.Reply, 0);
			AddButton(190, 162, 2361, 2361, 807, GumpButtonType.Reply, 0);
			AddButton(190, 182, 2361, 2361, 808, GumpButtonType.Reply, 0);
			AddButton(190, 202, 2361, 2361, 809, GumpButtonType.Reply, 0);
			AddButton(190, 222, 2361, 2361, 810, GumpButtonType.Reply, 0);
			AddButton(190, 242, 2361, 2361, 811, GumpButtonType.Reply, 0);
			AddButton(190, 262, 2361, 2361, 812, GumpButtonType.Reply, 0);
			AddButton(190, 282, 2361, 2361, 813, GumpButtonType.Reply, 0);
			AddButton(190, 302, 2361, 2361, 814, GumpButtonType.Reply, 0);

			AddPage(10);
			AddBackground(0, 0, 310, 355, 5054);
			AddLabel(10, 10, 0, "Malas & Points of Interest");
			AddLabel(10, 40, 0, "Luna");
			AddButton(190, 42, 2361, 2361, 901, GumpButtonType.Reply, 0);
			AddLabel(10, 60, 0, "Umbra");
			AddButton(190, 62, 2361, 2361, 902, GumpButtonType.Reply, 0);
			AddLabel(10, 80, 0, "Doom");
			AddButton(190, 82, 2361, 2361, 903, GumpButtonType.Reply, 0);

			if ( malasInterest == true ) {
			AddLabel(10, 100, 0, "Arena");
			AddLabel(10, 120, 0, "Orc Fort Desert");
			AddLabel(10, 140, 0, "Orc Fort Mountain");
			AddLabel(10, 160, 0, "Corrupted Forest");
			AddLabel(10, 180, 0, "Crystal Fens");
			AddLabel(10, 200, 0, "Forgotten Pyramid");
			//AddLabel(10, 220, 0, "Grimswind Ruins");
			AddLabel(10, 240, 0, "Hanse's Hostel");
			AddLabel(10, 260, 0, "Mining Mountians");
			AddLabel(10, 280, 0, "Northern Mountians");
			AddButton(190, 102, 2361, 2361, 904, GumpButtonType.Reply, 0);
			AddButton(190, 122, 2361, 2361, 905, GumpButtonType.Reply, 0);
			AddButton(190, 142, 2361, 2361, 906, GumpButtonType.Reply, 0);
			AddButton(190, 162, 2361, 2361, 907, GumpButtonType.Reply, 0);
			AddButton(190, 182, 2361, 2361, 908, GumpButtonType.Reply, 0);
			AddButton(190, 202, 2361, 2361, 910, GumpButtonType.Reply, 0);
			//AddButton(190, 222, 2361, 2361, 911, GumpButtonType.Reply, 0);
			AddButton(190, 242, 2361, 2361, 912, GumpButtonType.Reply, 0);
			AddButton(190, 262, 2361, 2361, 913, GumpButtonType.Reply, 0);
			AddButton(190, 282, 2361, 2361, 914, GumpButtonType.Reply, 0); }

			AddPage(11);
			AddBackground(0, 0, 310, 355, 5054);
			AddLabel(10, 10, 0, "Tokuno & Points of Interest");
			AddLabel(10, 40, 0, "Makoto - Zento City");
			AddLabel(10, 60, 0, "Makoto - The Waste");
			AddLabel(10, 80, 0, "Homarae - Bushido Dojo");
			AddLabel(10, 100, 0, "Homarae - Echo Fields");
			AddLabel(10, 120, 0, "Homarae - Crane Marsh");
			AddLabel(10, 140, 0, "Homarae - Yomotsu Mines");
			AddLabel(10, 160, 0, "Homarae - Kitsune Woods");
			AddLabel(10, 180, 0, "Homarae - Defiance Point");
			AddLabel(10, 200, 0, "Isamu - Winter Spur");
			AddLabel(10, 220, 0, "Isamu - Fan Dancer Dojo");
			AddLabel(10, 240, 0, "Isamu - Mount Sho");
			AddLabel(10, 260, 0, "Isamu - Lotus Lake");
			AddLabel(10, 280, 0, "Isamu - Storm Point");
			AddLabel(10, 300, 0, "Isamu - Sleeping Dragon Valley");
			AddButton(220, 42, 2361, 2361, 1001, GumpButtonType.Reply, 0);
			AddButton(220, 62, 2361, 2361, 1002, GumpButtonType.Reply, 0);
			AddButton(220, 82, 2361, 2361, 1003, GumpButtonType.Reply, 0);
			AddButton(220, 102, 2361, 2361, 1004, GumpButtonType.Reply, 0);
			AddButton(220, 122, 2361, 2361, 1005, GumpButtonType.Reply, 0);
			AddButton(220, 142, 2361, 2361, 1006, GumpButtonType.Reply, 0);
			AddButton(220, 162, 2361, 2361, 1007, GumpButtonType.Reply, 0);
			AddButton(220, 182, 2361, 2361, 1008, GumpButtonType.Reply, 0);
			AddButton(220, 202, 2361, 2361, 1009, GumpButtonType.Reply, 0);
			AddButton(220, 222, 2361, 2361, 1010, GumpButtonType.Reply, 0);
			AddButton(220, 242, 2361, 2361, 1011, GumpButtonType.Reply, 0);
			AddButton(220, 262, 2361, 2361, 1012, GumpButtonType.Reply, 0);
			AddButton(220, 282, 2361, 2361, 1013, GumpButtonType.Reply, 0);
			AddButton(220, 302, 2361, 2361, 1014, GumpButtonType.Reply, 0);

			AddPage(12);
			AddBackground(0, 0, 310, 355, 5054);
			AddLabel(10, 10, 0, "Trammel Shrines");
			AddLabel(10, 40, 0, "Chaos");
			AddLabel(10, 60, 0, "Compassion");
			AddLabel(10, 80, 0, "Honesty");
			AddLabel(10, 100, 0, "Honor");
			AddLabel(10, 120, 0, "Humility");
			AddLabel(10, 140, 0, "Justice");
			AddLabel(10, 160, 0, "Sacrifice");
			AddLabel(10, 180, 0, "Spirituality");
			AddLabel(10, 200, 0, "Valor");
			AddButton(190, 42, 2361, 2361, 1101, GumpButtonType.Reply, 0);
			AddButton(190, 62, 2361, 2361, 1102, GumpButtonType.Reply, 0);
			AddButton(190, 82, 2361, 2361, 1103, GumpButtonType.Reply, 0);
			AddButton(190, 102, 2361, 2361, 1104, GumpButtonType.Reply, 0);
			AddButton(190, 122, 2361, 2361, 1105, GumpButtonType.Reply, 0);
			AddButton(190, 142, 2361, 2361, 1106, GumpButtonType.Reply, 0);
			AddButton(190, 162, 2361, 2361, 1107, GumpButtonType.Reply, 0);
			AddButton(190, 182, 2361, 2361, 1108, GumpButtonType.Reply, 0);
			AddButton(190, 202, 2361, 2361, 1109, GumpButtonType.Reply, 0);

			AddPage(13);
			AddBackground(0, 0, 310, 355, 5054);
			AddLabel(10, 10, 0, "Felucca Shrines");
			AddLabel(10, 40, 0, "Chaos");
			AddLabel(10, 60, 0, "Compassion");
			AddLabel(10, 80, 0, "Honesty");
			AddLabel(10, 100, 0, "Honor");
			AddLabel(10, 120, 0, "Humility");
			AddLabel(10, 140, 0, "Justice");
			AddLabel(10, 160, 0, "Sacrifice");
			AddLabel(10, 180, 0, "Spirituality");
			AddLabel(10, 200, 0, "Valor");
			AddButton(190, 42, 2361, 2361, 1201, GumpButtonType.Reply, 0);
			AddButton(190, 62, 2361, 2361, 1202, GumpButtonType.Reply, 0);
			AddButton(190, 82, 2361, 2361, 1203, GumpButtonType.Reply, 0);
			AddButton(190, 102, 2361, 2361, 1204, GumpButtonType.Reply, 0);
			AddButton(190, 122, 2361, 2361, 1205, GumpButtonType.Reply, 0);
			AddButton(190, 142, 2361, 2361, 1206, GumpButtonType.Reply, 0);
			AddButton(190, 162, 2361, 2361, 1207, GumpButtonType.Reply, 0);
			AddButton(190, 182, 2361, 2361, 1208, GumpButtonType.Reply, 0);
			AddButton(190, 202, 2361, 2361, 1209, GumpButtonType.Reply, 0);

			AddPage(14);
			AddBackground(0, 0, 230, 250, 5054);
			AddLabel(100, 10, 0, "The");
			AddLabel(50, 35, 0, "Ultimate Travel Book");
			AddLabel(75, 90, 0, "Created by:");
			AddLabel(55, 120, 0, "Murzin @ RunUO");
			AddLabel(45, 160, 0, "using GumpStudio 1.7");
			AddButton(80, 200, 1303, 1302, 1301, GumpButtonType.Page, 1);
		}

		public override void OnResponse( NetState state, RelayInfo info ) {

		Mobile m = state.Mobile;
		Mobile from = state.Mobile;

		if ( DateTime.Now > gumpOpened + travelbookdelay ) { from.SendMessage("The magic of the book has dissipated."); return; }

		if ( !allowsigiltravel && Server.Factions.Sigil.ExistsOn( from ) ) {
			m.SendMessage( "You are carrying a sigil and may not travel by this method!" ); return; }

		if ( m.AccessLevel == AccessLevel.Player && checkvalidtravel ) {
			if ( !allowcriminaltravel && m.Criminal ) { m.SendMessage( "You are criminal and may not travel." ); return; }
			else if ( !allowcombattravel && Server.Spells.SpellHelper.CheckCombat( m ) || m.Combatant != null )
			{ m.SendMessage( "You may not flee from combat!" ); return; } }

		if ( preventoverweighttravel && m.AccessLevel == AccessLevel.Player ) { if ( ( Mobile.BodyWeight + m.TotalWeight ) > ( 40 + (3.5 * m.Str) + 4 ) ) {
				m.SendMessage( "You may not travel when carrying too much!" ); return; } }

		if ( m.Region is Regions.Jail && m.AccessLevel == AccessLevel.Player ) { m.Say( "You may not use this here to escape punishment!" ); return; }

		if ( hideonteleport && info.ButtonID != 0 ) { m.Hidden = true; }

		switch ( info.ButtonID ) {
			case 0: { break; }
			case 13:  { from.Map = Map.Trammel; from.Location = new Point3D(5835,926,-20); break; } // players lounge location
			case 101: { from.Map = Map.Trammel; from.Location = new Point3D(1437,1703,2); break; }
			case 102: { from.Map = Map.Trammel; from.Location = new Point3D(2236,1200,0); break; }
			case 103: { from.Map = Map.Trammel; from.Location = new Point3D(5273,3995,37); break; }
			case 104: { from.Map = Map.Trammel; from.Location = new Point3D(3497,2572,14); break; }
			case 105: { from.Map = Map.Trammel; from.Location = new Point3D(1331,3782,0); break; }
			case 106: { from.Map = Map.Trammel; from.Location = new Point3D(3732,2169,20); break; }
			case 107: { from.Map = Map.Trammel; from.Location = new Point3D(2494,566,0); break; }
			case 108: { from.Map = Map.Trammel; from.Location = new Point3D(4461,1175,0); break; }
			case 109: { from.Map = Map.Trammel; from.Location = new Point3D(3776,1311,0); break; }
			case 110: { from.Map = Map.Trammel; from.Location = new Point3D(5675,3128,15); break; }
			case 111: { from.Map = Map.Trammel; from.Location = new Point3D(2870,3472,35); break; }
			case 112: { from.Map = Map.Trammel; from.Location = new Point3D(596,2135,0); break; }
			case 113: { from.Map = Map.Trammel; from.Location = new Point3D(1828,2821,0); break; }
			case 114: { from.Map = Map.Trammel; from.Location = new Point3D(2893,685,0); break; }
			case 115: { from.Map = Map.Trammel; from.Location = new Point3D(5348,94,15); break; }
			case 116: { from.Map = Map.Trammel; from.Location = new Point3D(624,823,0); break; }
			case 117: { from.Map = Map.Trammel; from.Location = new Point3D(1475,1645,21); break; }
			case 118: { from.Map = Map.Trammel; from.Location = new Point3D(2267,1211,0); break; }
			case 119: { from.Map = Map.Trammel; from.Location = new Point3D(5246,4055,37); break; }
			case 120: { from.Map = Map.Trammel; from.Location = new Point3D(3506,2570,14); break; }
			case 121: { from.Map = Map.Trammel; from.Location = new Point3D(1414,3829,5); break; }
			case 122: { from.Map = Map.Trammel; from.Location = new Point3D(3701,2196,20); break; }
			case 123: { from.Map = Map.Trammel; from.Location = new Point3D(2467,434,15); break; }
			case 124: { from.Map = Map.Trammel; from.Location = new Point3D(4442,1123,5); break; }
			case 125: { from.Map = Map.Trammel; from.Location = new Point3D(3714,1238,0); break; }
			case 126: { from.Map = Map.Trammel; from.Location = new Point3D(5732,3208,0); break; }
			case 127: { from.Map = Map.Trammel; from.Location = new Point3D(2993,3405,15); break; }
			case 128: { from.Map = Map.Trammel; from.Location = new Point3D(610,2194,0); break; }
			case 129: { from.Map = Map.Trammel; from.Location = new Point3D(1914,2720,20); break; }
			case 130: { from.Map = Map.Trammel; from.Location = new Point3D(2857,866,0); break; }
			case 131: { from.Map = Map.Trammel; from.Location = new Point3D(5222,190,5); break; }
			case 132: { from.Map = Map.Trammel; from.Location = new Point3D(535,993,0); break; }
			case 201: { from.Map = Map.Trammel; from.Location = new Point3D(2499,922,0); break; }
			case 202: { from.Map = Map.Trammel; from.Location = new Point3D(4111,434,5); break; }
			case 203: { from.Map = Map.Trammel; from.Location = new Point3D(1302,1081,0); break; }
			case 204: { from.Map = Map.Trammel; from.Location = new Point3D(1176,2641,3); break; }
			case 205: { from.Map = Map.Trammel; from.Location = new Point3D(4723,3819,41); break; }
			case 206: { from.Map = Map.Trammel; from.Location = new Point3D(511,1566,0); break; }
			case 207: { from.Map = Map.Trammel; from.Location = new Point3D(2044,239,10); break; }
			case 208: { from.Map = Map.Trammel; from.Location = new Point3D(2921,3409,10); break; }
			case 209: { from.Map = Map.Trammel; from.Location = new Point3D(2001,83,5); break; }
			case 210: { from.Map = Map.Trammel; from.Location = new Point3D(4597,3631,30); break; }
			case 211: { from.Map = Map.Trammel; from.Location = new Point3D(5434,3159,-60); break; }
			case 221: { from.Map = Map.Trammel; from.Location = new Point3D(5613,1998,0); break; }
			case 222: { from.Map = Map.Trammel; from.Location = new Point3D(5309,531,0); break; }
			case 223: { from.Map = Map.Trammel; from.Location = new Point3D(5518,673,20); break; }
			case 224: { from.Map = Map.Trammel; from.Location = new Point3D(5144,804,0); break; }
			case 225: { from.Map = Map.Trammel; from.Location = new Point3D(5975,171,0); break; }
			case 226: { from.Map = Map.Trammel; from.Location = new Point3D(5517,15,0); break; }
			case 227: { from.Map = Map.Trammel; from.Location = new Point3D(5690,568,25); break; }
			case 241: { from.Map = Map.Trammel; from.Location = new Point3D(2546,857,0); break; }
			case 242: { from.Map = Map.Trammel; from.Location = new Point3D(5139,654,0); break; }
			case 243: { from.Map = Map.Trammel; from.Location = new Point3D(5402,869,45); break; }
			case 244: { from.Map = Map.Trammel; from.Location = new Point3D(5139,972,0); break; }
			case 245: { from.Map = Map.Trammel; from.Location = new Point3D(6083,149,-22); break; }
			case 246: { from.Map = Map.Trammel; from.Location = new Point3D(5516,143,20); break; }
			case 247: { from.Map = Map.Trammel; from.Location = new Point3D(5700,662,0); break; }
			case 301: { from.Map = Map.Trammel; from.Location = new Point3D(2730,2142,0); break; }
			case 302: { from.Map = Map.Trammel; from.Location = new Point3D(2206,1270,0); break; }
			case 303: { from.Map = Map.Trammel; from.Location = new Point3D(2372,3487,5); break; }
			case 304: { from.Map = Map.Trammel; from.Location = new Point3D(1321,551,30); break; }
			case 305: { from.Map = Map.Trammel; from.Location = new Point3D(782,1457,0); break; }
			case 306: { from.Map = Map.Trammel; from.Location = new Point3D(1150,2236,40); break; }
			case 307: { from.Map = Map.Trammel; from.Location = new Point3D(1687,2986,0); break; }
			case 308: { from.Map = Map.Trammel; from.Location = new Point3D(2494,3597,5); break; }
			case 309: { from.Map = Map.Trammel; from.Location = new Point3D(1918,2091,0); break; }
			case 310: { from.Map = Map.Trammel; from.Location = new Point3D(5758,2692,45); break; }
			case 311: { from.Map = Map.Trammel; from.Location = new Point3D(1012,2677,0); break; }
			//case 312: { from.Map = Map.Trammel; from.Location = new Point3D(5212,25,15); break; }
			case 313: { from.Map = Map.Trammel; from.Location = new Point3D(885,1682,0); break; }
			case 314: { from.Map = Map.Trammel; from.Location = new Point3D(632,1510,0); break; }
			case 315: { from.Map = Map.Trammel; from.Location = new Point3D(972,772,0); break; }
			case 401: { from.Map = Map.Felucca; from.Location = new Point3D(1437,1703,2); break; }
			case 402: { from.Map = Map.Felucca; from.Location = new Point3D(2236,1200,0); break; }
			case 403: { from.Map = Map.Felucca; from.Location = new Point3D(5273,3995,37); break; }
			case 404: { from.Map = Map.Felucca; from.Location = new Point3D(3686,2525,0); break; }
			case 405: { from.Map = Map.Felucca; from.Location = new Point3D(1331,3782,0); break; }
			case 406: { from.Map = Map.Felucca; from.Location = new Point3D(3732,2169,20); break; }
			case 407: { from.Map = Map.Felucca; from.Location = new Point3D(2494,566,0); break; }
			case 408: { from.Map = Map.Felucca; from.Location = new Point3D(4461,1175,0); break; }
			case 409: { from.Map = Map.Felucca; from.Location = new Point3D(3776,1311,0); break; }
			case 410: { from.Map = Map.Felucca; from.Location = new Point3D(5675,3128,15); break; }
			case 411: { from.Map = Map.Felucca; from.Location = new Point3D(2870,3472,35); break; }
			case 412: { from.Map = Map.Felucca; from.Location = new Point3D(596,2135,0); break; }
			case 413: { from.Map = Map.Felucca; from.Location = new Point3D(1828,2821,0); break; }
			case 414: { from.Map = Map.Felucca; from.Location = new Point3D(2893,685,0); break; }
			case 415: { from.Map = Map.Felucca; from.Location = new Point3D(5348,94,15); break; }
			case 416: { from.Map = Map.Felucca; from.Location = new Point3D(624,823,0); break; }
			case 417: { from.Map = Map.Felucca; from.Location = new Point3D(1475,1645,21); break; }
			case 418: { from.Map = Map.Felucca; from.Location = new Point3D(2267,1211,0); break; }
			case 419: { from.Map = Map.Felucca; from.Location = new Point3D(5246,4055,37); break; }
			case 420: { from.Map = Map.Felucca; from.Location = new Point3D(3651,2616,0); break; }
			case 421: { from.Map = Map.Felucca; from.Location = new Point3D(1414,3829,5); break; }
			case 422: { from.Map = Map.Felucca; from.Location = new Point3D(3701,2196,20); break; }
			case 423: { from.Map = Map.Felucca; from.Location = new Point3D(2467,434,15); break; }
			case 424: { from.Map = Map.Felucca; from.Location = new Point3D(4442,1123,5); break; }
			case 425: { from.Map = Map.Felucca; from.Location = new Point3D(3714,1238,0); break; }
			case 426: { from.Map = Map.Felucca; from.Location = new Point3D(5732,3208,0); break; }
			case 427: { from.Map = Map.Felucca; from.Location = new Point3D(2993,3405,15); break; }
			case 428: { from.Map = Map.Felucca; from.Location = new Point3D(610,2194,0); break; }
			case 429: { from.Map = Map.Felucca; from.Location = new Point3D(1914,2720,20); break; }
			case 430: { from.Map = Map.Felucca; from.Location = new Point3D(2857,866,0); break; }
			case 431: { from.Map = Map.Felucca; from.Location = new Point3D(5222,190,5); break; }
			case 432: { from.Map = Map.Felucca; from.Location = new Point3D(535,993,0); break; }
			case 501: { from.Map = Map.Felucca; from.Location = new Point3D(2499,922,0); break; }
			case 502: { from.Map = Map.Felucca; from.Location = new Point3D(4111,434,5); break; }
			case 503: { from.Map = Map.Felucca; from.Location = new Point3D(1302,1081,0); break; }
			case 504: { from.Map = Map.Felucca; from.Location = new Point3D(1176,2641,3); break; }
			case 505: { from.Map = Map.Felucca; from.Location = new Point3D(4723,3819,41); break; }
			case 506: { from.Map = Map.Felucca; from.Location = new Point3D(511,1566,0); break; }
			case 507: { from.Map = Map.Felucca; from.Location = new Point3D(2044,239,10); break; }
			case 508: { from.Map = Map.Felucca; from.Location = new Point3D(2921,3409,10); break; }
			case 509: { from.Map = Map.Felucca; from.Location = new Point3D(2001,83,5); break; }
			case 510: { from.Map = Map.Felucca; from.Location = new Point3D(4597,3631,30); break; }
			case 511: { from.Map = Map.Felucca; from.Location = new Point3D(5434,3159,-60); break; }
			case 521: { from.Map = Map.Felucca; from.Location = new Point3D(5613,1998,0); break; }
			case 522: { from.Map = Map.Felucca; from.Location = new Point3D(5309,531,0); break; }
			case 523: { from.Map = Map.Felucca; from.Location = new Point3D(5518,673,20); break; }
			case 524: { from.Map = Map.Felucca; from.Location = new Point3D(5144,804,0); break; }
			case 525: { from.Map = Map.Felucca; from.Location = new Point3D(5975,171,0); break; }
			case 526: { from.Map = Map.Felucca; from.Location = new Point3D(5517,15,0); break; }
			case 527: { from.Map = Map.Felucca; from.Location = new Point3D(5690,568,25); break; }
			case 541: { from.Map = Map.Felucca; from.Location = new Point3D(2546,857,0); break; }
			case 542: { from.Map = Map.Felucca; from.Location = new Point3D(5139,654,0); break; }
			case 543: { from.Map = Map.Felucca; from.Location = new Point3D(5402,869,45); break; }
			case 544: { from.Map = Map.Felucca; from.Location = new Point3D(5139,972,0); break; }
			case 545: { from.Map = Map.Felucca; from.Location = new Point3D(6083,149,-22); break; }
			case 546: { from.Map = Map.Felucca; from.Location = new Point3D(5516,143,20); break; }
			case 547: { from.Map = Map.Felucca; from.Location = new Point3D(5700,662,0); break; }
			case 601: { from.Map = Map.Felucca; from.Location = new Point3D(2730,2142,0); break; }
			case 602: { from.Map = Map.Felucca; from.Location = new Point3D(2206,1270,0); break; }
			case 603: { from.Map = Map.Felucca; from.Location = new Point3D(2372,3487,5); break; }
			case 604: { from.Map = Map.Felucca; from.Location = new Point3D(1321,550,30); break; }
			case 605: { from.Map = Map.Felucca; from.Location = new Point3D(782,1457,0); break; }
			case 606: { from.Map = Map.Felucca; from.Location = new Point3D(1150,2236,40); break; }
			case 607: { from.Map = Map.Felucca; from.Location = new Point3D(1687,2986,0); break; }
			case 608: { from.Map = Map.Felucca; from.Location = new Point3D(2494,3597,5); break; }
			case 609: { from.Map = Map.Felucca; from.Location = new Point3D(1918,2091,0); break; }
			case 610: { from.Map = Map.Felucca; from.Location = new Point3D(5758,2692,45); break; }
			case 611: { from.Map = Map.Felucca; from.Location = new Point3D(1012,2677,0); break; }
			case 612: { from.Map = Map.Felucca; from.Location = new Point3D(5212,25,15); break; }
			case 613: { from.Map = Map.Felucca; from.Location = new Point3D(885,1682,0); break; }
			case 614: { from.Map = Map.Felucca; from.Location = new Point3D(632,1510,0); break; }
			case 615: { from.Map = Map.Felucca; from.Location = new Point3D(972,772,0); break; }
			case 701: { from.Map = Map.Ilshenar; from.Location = new Point3D(1747,236,58); break; }
			case 702: { from.Map = Map.Ilshenar; from.Location = new Point3D(1217,469,-13); break; }
			case 703: { from.Map = Map.Ilshenar; from.Location = new Point3D(720,1356,-59); break; }
			case 704: { from.Map = Map.Ilshenar; from.Location = new Point3D(748,731,-29); break; }
			case 705: { from.Map = Map.Ilshenar; from.Location = new Point3D(287,1019,0); break; }
			case 706: { from.Map = Map.Ilshenar; from.Location = new Point3D(987,1002,-36); break; }
			case 707: { from.Map = Map.Ilshenar; from.Location = new Point3D(1172,1288,-30); break; }
			case 708: { from.Map = Map.Ilshenar; from.Location = new Point3D(1528,1341,-3); break; }
			case 709: { from.Map = Map.Ilshenar; from.Location = new Point3D(529,212,-42); break; }
			case 710: { from.Map = Map.Ilshenar; from.Location = new Point3D(1203,1124,-25); break; }
			case 711: { from.Map = Map.Ilshenar; from.Location = new Point3D(836,641,-20); break; }
			case 712: { from.Map = Map.Ilshenar; from.Location = new Point3D(820,1155,-30); break; }
			case 713: { from.Map = Map.Ilshenar; from.Location = new Point3D(1643,310,48); break; }
			case 801: { from.Map = Map.Ilshenar; from.Location = new Point3D(940,503,-30); break; }
			case 802: { from.Map = Map.Ilshenar; from.Location = new Point3D(576,1145,-100); break; }
			case 803: { from.Map = Map.Ilshenar; from.Location = new Point3D(1118,652,-80); break; }
			case 804: { from.Map = Map.Ilshenar; from.Location = new Point3D(1747,1225,-1); break; }
			case 805: { from.Map = Map.Ilshenar; from.Location = new Point3D(884,1303,-71); break; }
			case 806: { from.Map = Map.Ilshenar; from.Location = new Point3D(349,1432,15); break; }
			case 807: { from.Map = Map.Ilshenar; from.Location = new Point3D(852,777,-80); break; }
			case 808: { from.Map = Map.Ilshenar; from.Location = new Point3D(322,1363,-26); break; }
			case 809: { from.Map = Map.Ilshenar; from.Location = new Point3D(643,860,-59); break; }
			case 810: { from.Map = Map.Ilshenar; from.Location = new Point3D(1788,573,71); break; }
			case 811: { from.Map = Map.Ilshenar; from.Location = new Point3D(1188,692,-80); break; }
			case 812: { from.Map = Map.Ilshenar; from.Location = new Point3D(547,464,-58); break; }
			case 813: { from.Map = Map.Ilshenar; from.Location = new Point3D(1363,1041,-13); break; }
			case 814: { from.Map = Map.Ilshenar; from.Location = new Point3D(651,1305,-57); break; }
			case 901: { from.Map = Map.Malas; from.Location = new Point3D(982,519,-50); break; }
			case 902: { from.Map = Map.Malas; from.Location = new Point3D(2029,1343,-90); break; }
			case 903: { from.Map = Map.Malas; from.Location = new Point3D(2368,1268,-85); break; }
			case 904: { from.Map = Map.Malas; from.Location = new Point3D(2368,1160,-90); break; }
			case 905: { from.Map = Map.Malas; from.Location = new Point3D(1597,1843,-102); break; }
			case 906: { from.Map = Map.Malas; from.Location = new Point3D(1343,1272,-90); break; }
			case 907: { from.Map = Map.Malas; from.Location = new Point3D(2161,1164,-84); break; }
			case 908: { from.Map = Map.Malas; from.Location = new Point3D(1355,601,-89); break; }
			case 910: { from.Map = Map.Malas; from.Location = new Point3D(1861,1809,-107); break; }
			case 911: { from.Map = Map.Malas; from.Location = new Point3D(2192,351,-90); break; }
			case 912: { from.Map = Map.Malas; from.Location = new Point3D(1072,1435,-90); break; }
			case 913: { from.Map = Map.Malas; from.Location = new Point3D(1257,1416,-95); break; }
			case 914: { from.Map = Map.Malas; from.Location = new Point3D(1530,436,-86); break; }
			case 1001: { from.Map = Map.Tokuno; from.Location = new Point3D(738,1242,25); break; }
			case 1002: { from.Map = Map.Tokuno; from.Location = new Point3D(729,1034,30); break; }
			case 1003: { from.Map = Map.Tokuno; from.Location = new Point3D(320,461,32); break; }
			case 1004: { from.Map = Map.Tokuno; from.Location = new Point3D(204,650,33); break; }
			case 1005: { from.Map = Map.Tokuno; from.Location = new Point3D(204,986,17); break; }
			case 1006: { from.Map = Map.Tokuno; from.Location = new Point3D(254,787,64); break; }
			case 1007: { from.Map = Map.Tokuno; from.Location = new Point3D(502,503,32); break; }
			case 1008: { from.Map = Map.Tokuno; from.Location = new Point3D(278,1192,20); break; }
			case 1009: { from.Map = Map.Tokuno; from.Location = new Point3D(925,155,48); break; }
			case 1010: { from.Map = Map.Tokuno; from.Location = new Point3D(979,244,21); break; }
			case 1011: { from.Map = Map.Tokuno; from.Location = new Point3D(1101,765,30); break; }
			case 1012: { from.Map = Map.Tokuno; from.Location = new Point3D(1068,845,41); break; }
			case 1013: { from.Map = Map.Tokuno; from.Location = new Point3D(1191,1114,17); break; }
			case 1014: { from.Map = Map.Tokuno; from.Location = new Point3D(1013,535,29); break; }
			case 1101: { from.Map = Map.Trammel; from.Location = new Point3D(1458,844,5); break; }
			case 1102: { from.Map = Map.Trammel; from.Location = new Point3D(1858,875,-1); break; }
			case 1103: { from.Map = Map.Trammel; from.Location = new Point3D(4210,563,42); break; }
			case 1104: { from.Map = Map.Trammel; from.Location = new Point3D(1727,3528,3); break; }
			case 1105: { from.Map = Map.Trammel; from.Location = new Point3D(4274,3697,0); break; }
			case 1106: { from.Map = Map.Trammel; from.Location = new Point3D(1301,634,16); break; }
			case 1107: { from.Map = Map.Trammel; from.Location = new Point3D(3355,290,4); break; }
			case 1108: { from.Map = Map.Trammel; from.Location = new Point3D(1595,2490,20); break; }
			case 1109: { from.Map = Map.Trammel; from.Location = new Point3D(2492,3931,5); break; }
			case 1201: { from.Map = Map.Felucca; from.Location = new Point3D(1458,844,5); break; }
			case 1202: { from.Map = Map.Felucca; from.Location = new Point3D(1858,875,-1); break; }
			case 1203: { from.Map = Map.Felucca; from.Location = new Point3D(4210,563,42); break; }
			case 1204: { from.Map = Map.Felucca; from.Location = new Point3D(1727,3528,3); break; }
			case 1205: { from.Map = Map.Felucca; from.Location = new Point3D(4274,3697,0); break; }
			case 1206: { from.Map = Map.Felucca; from.Location = new Point3D(1301,634,16); break; }
			case 1207: { from.Map = Map.Felucca; from.Location = new Point3D(3355,290,4); break; }
			case 1208: { from.Map = Map.Felucca; from.Location = new Point3D(1595,2490,20); break; }
			case 1209: { from.Map = Map.Felucca; from.Location = new Point3D(2492,3931,5); break; } } } } }

namespace Server.Items { 
	public class TeleportBook : Item { 
		[Constructable] 
		public TeleportBook() : base( 0x2252 ) { 
			Hue = 39; Weight = 3; Movable = true; Name = "a Travel Book"; LootType = LootType.Blessed; } 

        public TeleportBook( Serial serial ) : base( serial ) { } 

        public override void OnDoubleClick( Mobile m ) {
		if ( m.Region is Regions.DungeonRegion && m.Map.Rules == MapRules.FeluccaRules && !Server.Gumps.TeleportBookGump.allowfeldungeonuse ) {
			if ( m.AccessLevel == AccessLevel.Player ) { m.SendMessage( "You may not use this here." ); }
			else { m.SendGump( new TeleportBookGump( m ) ); } } else { m.SendGump( new TeleportBookGump( m ) ); } } 

        public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); } 

        public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); } } }