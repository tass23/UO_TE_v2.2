<?
$phpname=$_SERVER['PHP_SELF'];
$servername="The Expanse";
$serverversion="7.0.12.0";
$online="0";
$updatetime="0";
$guilds="0";
$items="0";
$mobiles="0";
$uptime="0:0:0:0";
$currentmap=$_GET['map'];

if ($currentmap==""){$currentmap="Trammel";}
$xscale=0;
$xsize=500;
$ysize=400;

if ($currentmap=="Felucca"){
$ysize=746;
$xsize=1120;
$xscale=$xsize/6150;
$yscale=$ysize/4096;
}
if ($currentmap=="Ilshenar" ){
$ysize=906;
$xsize=1305;
$xscale=$xsize/2307;
$yscale=$ysize/1608;
}
if ($currentmap=="Malas" ){
$ysize=1048;
$xsize=1048;
$xscale=$xsize/2048;
$yscale=$ysize/2048;
}
if ($currentmap=="Tokuno" ){
$ysize=948;	//height
$xsize=948;	//width
$xscale=$xsize/1448;
$yscale=$ysize/1448;
}

if ($currentmap=="Trammel"){
$ysize=747;
$xsize=1120;
$xscale=$xsize/6150;
$yscale=$ysize/4096;
}

if ($currentmap=="TerMur"){
$ysize=2048;
$xsize=640;
$xscale=$xsize/1280;
$yscale=$ysize/4096;
}

$collectedpoints="";
$mapimages= array("Trammel","Felucca","Ilshenar","Malas" ,"Tokuno", "TerMur" );

foreach ($mapimages as $map) {
    
$maplinks=$maplinks."<a href='".$phpname."?map=".$map."'>".$map."</a> ";
}


$filename = "output.txt";
$fd = fopen ($filename, "r");

while (!feof($fd)) {
   $line = fgets($fd);
	$line=trim($line);
   if (substr($line, 0, 11)=="UpdateTime=")
	{$updatetime=substr($line,11,strlen($line));}
   if (substr($line, 0, 11)=="ServerName=")
	{$servername=substr($line,11,strlen($line));}
   if (substr($line, 0, 14)=="ServerVersion=")
	{$serverversion=substr($line,14,strlen($line));}


   if (substr($line, 0, 7)=="Guilds=")
	{$guilds=substr($line,7,strlen($line));}

   if (substr($line, 0, 6)=="Items=")
	{$items=substr($line,6,strlen($line));}

   if (substr($line, 0, 8)=="Mobiles=")
	{$mobiles=substr($line,8,strlen($line));}

   if (substr($line, 0, 7)=="Online=")
	{$online=substr($line,7,strlen($line));}
   
   if (substr($line, 0, 7)=="UpTime=")
	{$uptime=substr($line,7,strlen($line));}

   if (substr($line,0,7)=="Player=")
	{
	$playertext=substr($line,7,strlen($line));

	$playerarray = explode(",", $playertext);

	$playername=$playerarray[0];
	if ($currentmap=="Malas" ){
	$playerarray[1]=$playerarray[1]-512;
	}
	$playerx=($xscale*(int)$playerarray[1])-2;
	$playery=($yscale*(int)$playerarray[2])-2;
	$playermap=$playerarray[3];
	$playerlevel=$playerarray[4];
	$playerinfo="(".$playerlevel.")<br>".$playerarray[5];
	if ($playerx>=0 && $playerx<=$xsize && $playery>=0 && $playery<=$ysize && $playermap==$currentmap ){
	$collectedpoints=$collectedpoints."text=text+'<img src=\"".$playerlevel.".gif\" style=\"position: absolute; border: 0px; left: ".$playerx."px; top: ".$playery."px;\" onMouseMove=\"tip(\\'".$playername."\\',\\'".$playerinfo."\\');\" onMouseOut=\"h_tip();\">'\n";
	}
	}
}
fclose ($fd);
$collectedpoints="text=text+'<img src=\"server.gif\" height=100 style=\"position: absolute; border: 0px; left: 0; top: 0px;\" onMouseMove=\"tip(\\'".$servername."<br>".$serverversion." \\',\\'Users : ".$online."<br>Mobiles : ".$mobiles."<br>Items : ".$items."<br>Uptime : ".$uptime."\\');\" onMouseOut=\"h_tip();\">'\n".$collectedpoints;


?>
<HTML><HEAD><title>
<?
echo $servername;
?>
</title>
<style type="text/css">

body {
	margin-left: 0px;
	margin-top: 0px;
	margin-right: 0px;
	margin-bottom: 0px;
	color: #EABA28;
	background-color: #000000;
}
#world {
	position: absolute;
	height:<? echo $ysize ?>px;
	width: <? echo $xsize ?>px;
	left: 50%;
	margin-left: -<? echo $xsize/2 ?>px;
	background-image: url(
<?
echo $currentmap;
?>.gif);
	z-index: 10;
}
#points {
	position: absolute;
	height:<? echo $ysize ?>px;
	width: <? echo $xsize ?>px;
	left: 50%;
	margin-left: -<? echo $xsize/2 ?>px;
	z-index: 100;
}

#tip {
	background: #000000;
	border: 0px solid #aaaaaa;
	left: -1000px;
	padding: 0px;
	position: absolute;
	top: -1000px;
	z-index: 110;
} 
.tip_header {
	background: #bb0000;
	FONT-WEIGHT: bold;
	color: #FFFFFF;
	font-family: arial, helvetica, sans-serif;
	font-size: 12px;
	font-style: normal;
	text-align: center;
	padding: 0px;
} 
.tip_text {
	background: #000000;
	FONT-WEIGHT: normal;
	color: #ffffff;
	font-family: arial, helvetica, sans-serif;
	font-size: 12px;
	font-style: normal;
	text-align: center;
	padding: 3px;
} 
#server_info {
	font-family: Georgia, "Times New Roman", Times, serif;
	font-size: 12px;
	font-style: italic;
	text-align: center;
	font-weight: bold;
	color: #FFFF99;
	z-index: 80;
}

#info {
	position: absolute;
	height: 32px;
	width: <? echo $xsize ?>px;
	left: 50%;
	margin-left: -<? echo $xsize/2 ?>px;
	font-family: arial;
	font-size: 12px;
	font-style: normal;
	text-align: center;
	font-weight: bold;
	color: #FFFF99;
	z-index: 40;
	filter: Glow(Color=#000099, Strength=3);
}

</style>
<meta http-equiv=Refresh content="60; URL=<? echo $phpname."?map=".$currentmap ?>">

</HEAD>

<SCRIPT LANGUAGE="javascript" TYPE="text/javascript">

function tip(header,text) { 
var t; 
t=document.getElementById("tip"); 

if (window.opera) { 
x=window.event.clientX+15; 
y=window.event.clientY-10; 
} else if (navigator.appName=="Netscape") {
document.onmousemove=function(e) { x = e.pageX+15; y = e.pageY-10; return true}
} else { 
x=window.event.clientX + document.documentElement.scrollLeft + document.body.scrollLeft + 15; 
y=window.event.clientY + document.documentElement.scrollTop + document.body.scrollTop - 10; 
} 

t.innerHTML='<table width="120" border="1" cellspacing="0" cellpadding="0"\><tr class=\'tip_header\'\><td\>'+header+'</td\></tr\><tr class=\'tip_text\'\><td\>'+text+'</td\></tr\><\/table\>';
if (screen.width-x<150) x-=150; 
t.style.left=x + "px"; 
t.style.top=y + "px"; 
} 

function h_tip() { 
var t; 
t=document.getElementById("tip"); 
t.innerHTML=""; 
t.style.left="-1000px"; 
t.style.top="-1000px"; 
} 

function start() {
text="";
<?
echo $collectedpoints;
?>

document.getElementById("points").innerHTML=text;



}
</SCRIPT>


<BODY onload=start()>
<br>
<center><b><?
echo $servername;
?>
 Server Maps
</b>
<br>
<?
echo $maplinks;
?>
</center>
<hr>
<div id="tip"></div>
<div ID="points"></div>
<div ID="info">Last Updated <?
echo $updatetime; ?></div>

<div ID="world"></div>

</body></html>