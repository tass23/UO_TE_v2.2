using System;
using Server;
using Server.Gumps;

namespace Server.Gumps
{
	public class GovHelpGump : Gump
	{
		public GovHelpGump()
			: base( 50, 50 )
		{
			Closable=true;
			Disposable=true;
			Dragable=true;
			Resizable=false;
			AddPage(0);
			AddBackground(17, 23, 489, 323, 5120);
			AddImageTiled(22, 58, 483, 10, 5121);
			AddHtml( 23, 29, 479, 22, @"<BASEFONT COLOR=WHITE><CENTER>Player Government System Help System</CENTER></BASEFONT>", (bool)false, (bool)false);
			AddImageTiled(158, 60, 6, 283, 5123);
			AddButton(25, 65, 4005, 4006, 1, GumpButtonType.Page, 1);
			AddButton(25, 90, 4005, 4006, 2, GumpButtonType.Page, 2);
			AddButton(25, 115, 4005, 4006, 3, GumpButtonType.Page, 3);
			AddButton(25, 140, 4005, 4006, 4, GumpButtonType.Page, 4);
			AddButton(25, 165, 4005, 4006, 5, GumpButtonType.Page, 5);
			AddButton(25, 190, 4005, 4006, 6, GumpButtonType.Page, 6);
			AddButton(25, 215, 4005, 4006, 7, GumpButtonType.Page, 7);
			AddButton(25, 240, 4005, 4006, 8, GumpButtonType.Page, 8);
			AddButton(25, 265, 4005, 4006, 9, GumpButtonType.Page, 9);
			AddButton(25, 290, 4005, 4006, 10, GumpButtonType.Page, 10);
			AddButton(25, 315, 4005, 4006, 11, GumpButtonType.Page, 11);
			AddLabel(60, 65, 1149, @"Starting Off");
			AddLabel(60, 90, 1149, @"Elections");
			AddLabel(60, 115, 1149, @"Taxes");
			AddLabel(60, 140, 1149, @"Treasury");
			AddLabel(60, 165, 1149, @"City Growth");
			AddLabel(60, 190, 1149, @"Maintenance");
			AddLabel(60, 215, 1149, @"Waring");
			AddLabel(60, 240, 1149, @"Allegiances");
			AddLabel(60, 265, 1149, @"City Commands");
			AddLabel(60, 290, 1149, @"Misc.");
			AddLabel(60, 315, 1149, @"Credits");
			AddPage(1);
			AddHtml ( 166, 63, 331, 275, String.Format( "<CENTER><U>Starting Off</U><BR>So you want to become at mayor? Well there are a few things you need to know before starting. First you need to get your hands on a city hall deed, there should be a city manager in town to buy them from. This is the building that you will control your city, and voting.  Now we need to find a good spot for your city. When picking your spot be sure and find a nice open area so others can place there homes near by so your city will grow. Also it will have to be at least {0} yards (Tiles) away from any other city or guarded area. Be sure you like the spot your at because once you have placed the hall you cannot reclaim its deed.<BR>Well, now that we have your city hall placed. Now we need citizens. You will have to get {1} citizens within 24 hours or your city will be deleted. Since you can have all of your characters from your account join the city. You can find a few of your close friends and have them join the city with all their characters and you should be fine. You will also want to add funds to your city treasury, Each time your city updates your city gets charged a maintenance fee, If you city lacks the funds your city deletes. You can view your maintenance report via the city management stone.<BR>The more citizens you get the more your town grows, Every {2} days your city updates and if you have enough citizens in your city, your city will level up.", ( PlayerGovernmentSystem.CityRangeOffset * 2).ToString(), PlayerGovernmentSystem.Level1.ToString(), PlayerGovernmentSystem.CityUpdate.Days.ToString() ),(bool)true, (bool)true);
			AddPage(2);
			AddHtml( 166, 63, 331, 275, String.Format( "<CENTER><U>Elections</U><BR>Every {0} days your city will hold an election. There can only be 2 running mates per election and your citizens vote for the mayor they want to see in office. A citizen can only vote once per election.<BR>If there is only one running mate the city election is null and void and the current mayor will stay in office for that term. It is always good to run for mayor and have the citizens vote for you. If the votes are tied the election is null and void and the current mayor will stay in office for that term.<BR>Keep in mind, as mayor you can always be voted out of office and the next mayor will have the powers you once did. So don't go decorating the city with your favorite weapons and rares because, if you're voted out, the next mayor takes control over the cities decorations.<BR>As long as you make the people happy and your good to them you should be a good mayor and stay in office for a very long time. Its always good to check the voting stone to see who is running against you.", PlayerGovernmentSystem.VoteUpdate.Days.ToString() ) ,(bool)true, (bool)true);
			AddPage(3);
			AddHtml( 166, 63, 331, 275, @"<CENTER><U>Taxes</U><BR>Taxes are a key element of any city, Sure we all hate them but without them, the city cannot function and maintain itself. Like in real life, Taxes go to improve the town (We hope) same goes for taxes on here. Alot of money will go into a city. You have maintenance charges, Civic buildings cost money or resources. and the more your city grows the more it will end up costing you and if the monies not in the treasury, Poof no more city. So as soon as your city is at the level to levy taxes it is a very good idea to start them off small, Of course your citizens will not want to pay taxes but it will be your job as mayor to make them understand where that money is going it will also be your job as mayor to spend the money wisely... or on that new katana you have been looking for. As mayor you can withdraw all the money from the treasury as you wish. Just be careful because when you do all online citizens are notified that you did. And if they are concerned 
			citizens they will want to know where their hard earned tax money is going.<BR>When tax time comes around (During City Update) the money will be withdrawn from you and your citizen's bank accounts. That's right even the mayor pays. If, for some reason the citizen lacks the gold in their bank at that time. There will be a check every time that player logs in and will take what money they have towards their back taxes or if they have the full amount it will take all the back taxes owned to the city. The player will not have a choice in this matter. That is part of being in the city.<BR>Charging too much for taxes can make your citizens not want to live in your city but you, as mayor can levy taxes from 0 = no taxes to 10000 in taxes each week, There are 3 types of taxes: Income, Property, and Travel. Property gets withdrawn from the citizen's bank account each week, and income from their City Player Vendors, however Travel will only get charged when someone uses a civic moongate, coming or going from your city. This way you can charge non-members of the
			city a fee to use your public moongates. and if they use a public moongate outside of your city to come to your city they get charged as well.<BR>Charging too little on taxes will end up making you pay out of your pocket a lot of money in maintenance, and even losing your city. It is up to you as mayor to levy the correct amount of taxes for your city.", (bool)true, (bool)true);
			AddPage(4);
			AddHtml( 166, 63, 331, 275, @"<CENTER><U>Treasury</U><BR>The treasury is the life line of your city. This is like your city's own bank account for gold. All taxes go to the treasury each week. Players can deposit gold into the treasury any time they wish. The mayor can also deposit and withdraw from it at any time. The money stored in the treasury is used mainly for the city maintenance. Also the mayor can choose to use it for anything he/she wishes. Again this is totally up to the mayor. Whether they use it for city needs or personal needs. Lack of money in the treasury will result in the city being deleted. Each week, when the maintenance is tallied up the city withdraws what it needs to stay running. If it doesn't have enough, the city will be deleted and all the civic structures with it. All locked down items (Other than in player homes) will be released for anyone to grab and the mayor will not be able to reclaim their deeds for civic buildings.<BR>It is a very good idea to keep a close eye on the treasury and the
			maintenance report. Here you can calculate the money needed vs. the money stored in the treasury.", (bool)true, (bool)true);
			AddPage(5);
			AddHtml( 166, 63, 331, 275, @"<CENTER><U>City Growth</U><BR>During the update period, your city will check itself and update if needed. If you have the required players for the city's next level, your city will go to its next level, unlocking all features that come with that level. You will also get more city lock downs and your cities limits will expand. Your city can get up to 250x250 from the city halls location. thats a pretty big open area for cities. Each level comes with its own rank for the city. So, say you have a lot of citizens when people enter your city. They could get a message like  'You have entered the empire of Arwen's city'. But in order to get there you must get people to move to your city. Just because a player owns a house within city limits, that does not make them a citizen. They have to join the city stone. If not, they are just wasted space in your city. They are living in your city and not paying taxes. And you cant ban anyone who owns a house in the city. So
			its always a good idea to keep your cities housing turned off so only when you enable can new people place houses within your city.<BR>As your city grows you can enable new features like hiring guards, and registering your city. When you register your city it will show up on public moongate city list (That is if your city has a moongate) and players can come to your city and view your city's vendors and features. Its always good to have a good shopping center in your city. That will bring the people into your city and if your citizens have money thats more money for the city.", (bool)true, (bool)true);
			AddPage(6);
			AddHtml( 166, 63, 331, 275, @"<CENTER><U>Maintenance</U><BR>Maintenance is very important to your city. Maintenance cost keeps your city up and going. These charges are applied per civic building/features your city has enabled. Check your maintainace report for the current breakdown of costs. This is where taxes play a big role in keeping the city alive and moving. Being a mayor is not for the faint of heart. It can be a very rewarding, and expensive job. If managed correctly one can pull it off with ease.<BR><BR>To keep your cost down it's important to know what you want in your city. If you're near a popular location, say, a dungeon or hunting site. Your going to want to have a moongate and charge a travel tax. This will bring more money into your city. If you have vendors that are popular and well stocked, you will want a moongate
			as well. If your near a dungeon you might want to add a healer. So players come to your city to be resurrected. You can charge a fee to resurrect at a city and that money goes right back into the city treasury. If your near a site where people die alot this could bring in alot of extra cash flow. All these extra building will cost your more in maintenance but if placed correctly can bring in far more money each week than they take out. Having a bank cant bring the city any money but might bring it more traffic for other ventures.<BR>Stables can bring your city money from players stabling their pets their, 30 gold per pet is nothing to turn your noise up at.", (bool)true, (bool)true);
			AddPage(7);
			AddHtml( 166, 63, 331, 275, @"<CENTER><U>Waring</U><BR>As with anything in life, you will have your share of enemies. You can go to war with other cities via the 'War Dept.' menu, Here you can declare wars, view and remove wars you have declared, View and accept war invites to your city, Cancel wars with other cities, and view the list of all cities yours is at war with.<BR>Once a city goes to war, its the same as if two guilds were at war. The city's citizens your at war with will be orange to your city's citizens at all times on all facets.", (bool)true, (bool)true);
			AddPage(8);
			AddHtml( 166, 63, 331, 275, @"<CENTER><U>Allegiances</U><BR>You can also use the 'War Dept.' menu to become allies with other cities. This works the same has if you were to war another city. Instead of the orange your allied cities citizens will appear green your cities citizens. Just like in guilds all parties can attack each other but its considered friendly fire.", (bool)true, (bool)true);
			AddPage(9);
			AddHtml( 166, 63, 331, 275, @"<CENTER><U>City Commands</U><BR>I wish to lock this down<BR>This command will secure an item inside the town. Or any civic building. You can secure anything you wish however unlike in a house, Containers will not have the items inside secured as well and you cannot secure them inside the bag. City Decore is for show only not for storage.<BR><BR>I wish to release this<BR>This command will release any item you have secured in the city. it works anywhere in the city other than inside player houses.<BR><BR>I ban thee<BR>This command will ban a person from your city. However you cannot ban a member of that city or someone who owns a house within city limits. Once a player is banned they become orange to all the city citizens while inside the city only.", (bool)true, (bool)true);
			AddPage(10);
			AddHtml( 166, 63, 331, 275, @"<CENTER><U>Misc.</U><BR>Location is key with any city. Your going to want an area thats a high traffic area for your city to grow, No one wants to live in the middle of nowhere. And no one is going to come to a city in the middle of nowhere. You need a place near a dungeon or popular fighting area. Having vendors in your city is a plus too. Everyone loves to shop.If you have the best vendor malls around its sure to bring traffic to your city.<BR>If you have any other questions. Please ask your staff." , (bool)true, (bool)true);
			AddPage(11);
			AddLabel(170, 66, 1149, @"Concept by. RoninGT A.K.A. Paige");
			AddLabel(170, 93, 1149, @" Coded by Avelyn" );
			AddLabel(170, 230, 1149, @"Built On: RunUO v2.2");
			AddLabel(170, 250, 1149, @"Verison: 2.23");
			

		}
	}
}
