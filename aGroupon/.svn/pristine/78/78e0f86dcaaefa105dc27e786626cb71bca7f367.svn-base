<?php
/* 
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */

?>






<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>Dealers</title>

<link href="style/style.css" rel="stylesheet" type="text/css" />
<link href="style/reset.css" rel="stylesheet" type="text/css" />

<!--[if IE 8]>
<link rel="stylesheet" href="style/ie8.css" media="screen, projection" type="text/css" >
<![endif]-->

<!--[if IE 9]>
<link rel="stylesheet" href="style/ie9.css" media="screen, projection" type="text/css" >
<![endif]-->


<script src="./script/jquery/jquery-1.5.1.min.js" type="text/javascript"></script>
<!--MENU-->
<link rel="stylesheet" media="screen" href="./script/menu/superfish.css" />
<script src="./script/menu/superfish.js"></script>

<script type="text/javascript">
    jQuery(document).ready(function(){

        //MENU
        $("ul.sf-menu").superfish();



        //BRISANJE ZADNJE LINIJE U MENIJU
        jQuery('.sf-menu li:last').attr('style', 'background:none');
        jQuery('.nav-my-profile ul li:last').attr('style', 'background:none');


        // HOVER-IMAGES
        jQuery('.images a').hover(function(){
           jQuery('img',this).stop().animate({opacity:0.4},500);
        },function(){
           jQuery('img',this).stop().animate({opacity:1},300);
        });

        jQuery('.images-small a').hover(function(){
           jQuery('img',this).stop().animate({opacity:0.4},500);
        },function(){
           jQuery('img',this).stop().animate({opacity:1},300);
        });

    });
</script>

</head>
<body>

<div id="container">



    <!------ HEADER ------>
    <div class="header">
    	<div class="wrapper">



            <div class="header-left">

                <!--LOGO-->
                <div class="logo left">
                    <a href="index.php"><img src="style/img/logo.png" alt="Logo" title="logo" /></a>
                </div>




                <!--ACTIVE LINK-->
                <?php
                function curPageURL() {
                 $pageURL = 'http';
                 if ($_SERVER["HTTPS"] == "on") {$pageURL .= "s";}
                 $pageURL .= "://";
                 if ($_SERVER["SERVER_PORT"] != "80") {
                  $pageURL .= $_SERVER["SERVER_NAME"].":".$_SERVER["SERVER_PORT"].$_SERVER["REQUEST_URI"];
                 } else {
                  $pageURL .= $_SERVER["SERVER_NAME"].$_SERVER["REQUEST_URI"];
                 }
                 return $pageURL;
                }

                $url = curPageURL();
                $url = explode('/',$url);

                $url = end($url);
                    $url = str_replace('.php', '', $url);
                ?>



                <!--MENU-->
                <div class="nav left">
                    <ul class="sf-menu">
                        <li <?php if($url == 'index'){ echo 'class="active" '; } ?> style="padding-left: 0px;" ><a href="index.php">Home</a>
                        <li <?php if($url == 'about'){ echo 'class="active" '; } ?> ><a href="about.php">About</a>
                            <ul class="sub-menu">
                                <li class="sub-menu-top"></li>
                                <li><a href="login.html">Login</a></li>
                                <li><a href="my-profile.php">My profile</a></li>
                                <li><a href="404.php">404</a></li>
                                <li class="sub-menu-bottom"></li>
                            </ul>
                        </li>
                        <li <?php if($url == 'all-deales'){ echo 'class="active" '; } ?> ><a href="all-deales.php">All Deales</a>
                        <li <?php if($url == 'submit'){ echo 'class="active" '; } ?> ><a href="submit.php">Submit Your Deal</a>
                        <li <?php if($url == 'contact'){ echo 'class="active" '; } ?>  style="padding-right: 0;"><a href="contact.php"><img src="style/img/menu-contact.png" alt="images" title="images" /></a>
                    </ul>
                </div><!--/nav-->

            </div><!--header-left-->




            <div class="header-right">
                <div class="header-right-background">
                    <h3>Sign up for free to get deals directly to your mailbox</h3>


                    <!--SEARCH-->
                    <div class="search-header">
                        <div class="sidebar_widget_holder">
                            <form id="searchform">
                                <input id="s" class="idleField" onfocus="if(value==defaultValue)value=''" onblur="if(value=='')value=defaultValue" value="Enter Your E-mail Address" name="name" />
                                <a id="searchsubmit" href="#">Subscribe Me</a>
                            </form>
                        </div><!--/sidebar_widget_holder-->
                    </div><!--search-header-->

                    <div class="pecat"></div>

                </div><!--header-right-background-->
            </div><!--header-right-->


        </div><!--/wrapper-->
    </div><!--/header-->