<?php
/**
 * @package     Joomla.Site
 * @subpackage  Templates.protostarPlusEighteen
 *
 * @copyright   Copyright (C) 2005 - 2018 Open Source Matters, Inc. All rights reserved.  / maarten blokdijk / cloudfation.nl
 * @license     GNU General Public License version 2 or later; see LICENSE.txt
 */

defined('_JEXEC') or die;

/** @var JDocumentHtml $this */

$app  = JFactory::getApplication();
$user = JFactory::getUser();

// Output as HTML5
$this->setHtml5(true);

// Getting params from template
$params = $app->getTemplate(true)->params;

// Detecting Active Variables
$option   = $app->input->getCmd('option', '');
$view     = $app->input->getCmd('view', '');
$layout   = $app->input->getCmd('layout', '');
$task     = $app->input->getCmd('task', '');
$itemid   = $app->input->getCmd('Itemid', '');
$sitename = $app->get('sitename');

if($task == "edit" || $layout == "form" )
{
	$fullWidth = 1;
}
else
{
	$fullWidth = 0;
}

// Add JavaScript Frameworks
JHtml::_('bootstrap.framework');

// Add template js
JHtml::_('script', 'template.js', array('version' => 'auto', 'relative' => true));

// Add html5 shiv
JHtml::_('script', 'jui/html5.js', array('version' => 'auto', 'relative' => true, 'conditional' => 'lt IE 9'));

// Add Stylesheets
if ($this->params->get('widelayout'))
{
JHtml::_('stylesheet', 'template.css', array('version' => 'auto', 'relative' => true));
}
else {
JHtml::_('stylesheet', 'template960.css', array('version' => 'auto', 'relative' => true));
}

JHtml::_('stylesheet', 'font-awesome.min.css', array('version' => 'auto', 'relative' => true));

// Check for a custom CSS file
JHtml::_('stylesheet', 'user.css', array('version' => 'auto', 'relative' => true));

// Check for a custom js file
JHtml::_('script', 'user.js', array('version' => 'auto', 'relative' => true));


// Load optional RTL Bootstrap CSS
JHtml::_('bootstrap.loadCss', false, $this->direction);

// Adjusting content width
if ($this->countModules('position-7') && $this->countModules('position-8'))
{
	$span = "span6";
}
elseif ($this->countModules('position-7') && !$this->countModules('position-8'))
{
	$span = "span9";
}
elseif (!$this->countModules('position-7') && $this->countModules('position-8'))
{
	$span = "span9";
}
else
{
	$span = "span12";
}

// Adjusting footer width

if ($this->countModules('footer1')==1):
	 $col_footer="span12";
endif;
if (($this->countModules('footer1')==1)&&($this->countModules('footer2')==1)):
	 $col_footer="span6";
endif;
if (($this->countModules('footer1')==1)&&($this->countModules('footer2')==1)&&($this->countModules('footer3')==1)):
	 $col_footer="span4";
endif;
if (($this->countModules('footer1')==1)&&($this->countModules('footer2')==1)&&($this->countModules('footer3')==1)&&($this->countModules('footer4')==1)):
	 $col_footer="span3";
endif;



// Logo file or site title param
if ($this->params->get('logoFile'))
{
	$logo = '<img src="' . JUri::root() . $this->params->get('logoFile') . '" alt="' . $sitename . '" />';
}
elseif ($this->params->get('sitetitle'))
{
	$logo = '<span class="site-title" title="' . $sitename . '">' . htmlspecialchars($this->params->get('sitetitle')) . '</span>';
}
else
{

}
?>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml" xml:lang="<?php echo $this->language; ?>" lang="<?php echo $this->language; ?>" dir="<?php echo $this->direction; ?>">
<head>
	<meta name="viewport" content="width=device-width, initial-scale=1.0" />
	<jdoc:include type="head" />
	<?php // Use of Google Font ?>
	<?php if ($this->params->get('googleFont')) : ?>
		<link href='//fonts.googleapis.com/css?family=<?php echo $this->params->get('googleFontName'); ?>' rel='stylesheet' type='text/css' />
		<style type="text/css">
			h1,h2,h3,h4,h5,h6,.site-title{
				font-family: '<?php echo str_replace('+', ' ', $this->params->get('googleFontName')); ?>', sans-serif;
			}
		</style>
	<?php endif; ?>
	<?php if ($this->params->get('googleFontDiv')) : ?>
		<link href='//fonts.googleapis.com/css?family=<?php echo $this->params->get('googleFontNameDiv'); ?>' rel='stylesheet' type='text/css' />
		<style type="text/css">
			div, p, td {
				font-family: '<?php echo str_replace('+', ' ', $this->params->get('googleFontNameDiv')); ?>', sans-serif ; font-size:<?php echo $this->params->get('fontsize'); ?>%;
			}
		</style>
	<?php endif; ?>


<style type="text/css">
.item-page h1,.item-page h2,.item-page h3,.item-page h4,.item-page h5,.item-page h6 {color:<?php echo $this->params->get('headercolor'); ?> !Important; }
.item-page a {color:<?php echo $this->params->get('linkcolor'); ?> !Important; }.item-page a:hover {color:<?php echo $this->params->get('linkhovercolor'); ?> !Important; }
#aside .well{padding:5px}
body.site{padding: 0px;border-top: 3px solid <?php echo $this->params->get('templateColor'); ?>;background-color: <?php echo $this->params->get('templateBackgroundColor'); ?>;}
<?php if ($this->params->get('bgfile')) : ?>
body.site{background: url("/<?php echo $this->params->get('bgfile'); ?>") no-repeat center center fixed;background-size: cover;}
<?php endif; ?>
a{color: <?php echo $this->params->get('templateColor'); ?>;}
.navbar-inner, .nav-list > .active > a, .nav-list > .active > a:hover, .dropdown-menu li > a:hover, .dropdown-menu .active > a, .dropdown-menu .active > a:hover, .nav-pills > .active > a, .nav-pills > .active > a:hover,
.btn-primary{background: <?php echo $this->params->get('templateColor'); ?>;}
.navbar-inner{-moz-box-shadow: 0 1px 3px rgba(0, 0, 0, .25), inset 0 -1px 0 rgba(0, 0, 0, .1), inset 0 30px 10px rgba(0, 0, 0, .2);-webkit-box-shadow: 0 1px 3px rgba(0, 0, 0, .25), inset 0 -1px 0 rgba(0, 0, 0, .1), inset 0 30px 10px rgba(0, 0, 0, .2);box-shadow: 0 1px 3px rgba(0, 0, 0, .25), inset 0 -1px 0 rgba(0, 0, 0, .1), inset 0 30px 10px rgba(0, 0, 0, .2);}
.body .container {background-color: <?php echo $this->params->get('templateTextBackgroundColor'); ?> ;background-color: rgba(<?php echo $this->params->get('templateTextBackgroundColorRGBA'); ?>) !important;color: <?php echo $this->params->get('textcolor'); ?>;}
.nav > li > a:focus {background-color: <?php echo $this->params->get('templateColor'); ?>;}
.nav-pills > li > a {color:<?php echo $this->params->get('templateColor'); ?>;}
.ftr{background-color: <?php echo $this->params->get('templateFooterBgColor'); ?> !Important; padding: 10px; }
.ftr p, .ftr a, .ftr h1, .ftr h2, .ftr h3, .ftr h4 {color:<?php echo $this->params->get('templateFooterTextColor'); ?> !important; }
.well {background:<?php echo $this->params->get('ModulesBackgroundColor'); ?>;}
.navigation {background: <?php echo $this->params->get('menugradient1'); ?>; background: -webkit-linear-gradient(<?php echo $this->params->get('menugradient1'); ?>, <?php echo $this->params->get('menugradient2'); ?>); background: -o-linear-gradient(<?php echo $this->params->get('menugradient1'); ?>, <?php echo $this->params->get('menugradient2'); ?>); background: -moz-linear-gradient(<?php echo $this->params->get('menugradient1'); ?>, <?php echo $this->params->get('menugradient2'); ?>);     background: linear-gradient(<?php echo $this->params->get('menugradient1'); ?>, <?php echo $this->params->get('menugradient2'); ?>); border: 1px solid <?php echo $this->params->get('menugradient2'); ?>;}
#top-bar{background-color: <?php echo $this->params->get('topbarbg'); ?> ;background-color: rgba(<?php echo $this->params->get('topbarbgRGBA'); ?>) !important;}
#top-bar-content div, #top-bar-content p {color:<?php echo $this->params->get('topbarcontent'); ?>;}
.carousel-inner{max-height: <?php echo $this->params->get('sliderheight'); ?>px; width:100%;}
.nav > li > a:hover,.nav > li > a:focus {text-decoration: none;color:<?php echo $this->params->get('menuactivecolor'); ?>;}
.brand{color: <?php echo $this->params->get('templateColor'); ?> !important;}
@media (min-width: 768px) and (max-width: 979px) {.navigation {background-color: <?php echo $this->params->get('templateColor'); ?>;}}
@media (min-width: 767px) {.navigation {background-color: <?php echo $this->params->get('templateColor'); ?>;}}
@media (min-width: 480px) {.navigation {background-color: <?php echo $this->params->get('templateColor'); ?>;}}
@media (min-width: 768px) {.navigation {background-color: <?php echo $this->params->get('templateColor'); ?>;}}

<?php echo $this->params->get('customcss'); ?>

</style>
	<!--[if lt IE 9]>
		<script src="<?php echo JUri::root(true); ?>/media/jui/js/html5.js"></script>
	<![endif]-->


	<?php echo $this->params->get('customhead'); ?>
</head>

<body class="site <?php echo $option
	. ' view-' . $view
	. ($layout ? ' layout-' . $layout : ' no-layout')
	. ($task ? ' task-' . $task : ' no-task')
	. ($itemid ? ' itemid-' . $itemid : '')
	. ($params->get('fluidContainer') ? ' fluid' : '');
?>">
<?php echo $this->params->get('customscr'); ?>
	<!-- Body -->
	<div class="body">
<div id="top-bar">
    <div id="top-bar-content">
        <jdoc:include type="modules" name="top-bar-content" style="xhtml" />



    </div>
</div>
		<div class="container">
			<!-- Header -->
			<header class="header" role="banner">
				<div class="header-inner clearfix">
					<a class="brand pull-left" href="<?php echo $this->baseurl; ?>/">
						<?php echo $logo; ?>
						<?php if ($this->params->get('sitedescription')) : ?>
							<?php echo '<div class="site-description">' . htmlspecialchars($this->params->get('sitedescription')) . '</div>'; ?>
						<?php endif; ?>
					</a>
					<div class="header-search pull-right">

						<jdoc:include type="modules" name="position-0" style="none" />

					</div>
				</div>
			</header>
			<?php if ($this->countModules('position-1')) : ?>
				<nav class="navigation" role="navigation">
					<div class="navbar pull-left">
						<a class="btn btn-navbar collapsed" data-toggle="collapse" data-target=".nav-collapse">
							<span class="icon-bar"></span>
							<span class="icon-bar"></span>
							<span class="icon-bar"></span>
						</a>
					</div>
					<div class="nav-collapse">
						<jdoc:include type="modules" name="position-1" style="none" />
					</div>

				</nav>
			<?php endif; ?>
			<jdoc:include type="modules" name="banner" style="xhtml" />

	<?php if ($this->params->get('slide1')) : ?>

  <div id="myCarousel" class="carousel slide" data-ride="carousel">
  <!-- Wrapper for slides -->
  <div class="carousel-inner" role="listbox">
   <div class="item active">
      <img src="<?php echo $this->params->get('slide1'); ?>" >
      <div class="carousel-caption">
              <?php echo $this->params->get('slide1text'); ?>
      </div>
    </div>
<?php if ($this->params->get('slide2')) : ?>
    <div class="item">
      <img src="<?php echo $this->params->get('slide2'); ?>" >
       <div class="carousel-caption">
       <?php echo $this->params->get('slide2text'); ?>
      </div>
    </div>
<?php endif; ?>
<?php if ($this->params->get('slide3')) : ?>
    <div class="item">
      <img src="<?php echo $this->params->get('slide3'); ?>" >
       <div class="carousel-caption">
              <?php echo $this->params->get('slide3text'); ?>
      </div>
    </div>
<?php endif; ?>
<?php if ($this->params->get('slide4')) : ?>
    <div class="item">
      <img src="<?php echo $this->params->get('slide4'); ?>" >
       <div class="carousel-caption">
              <?php echo $this->params->get('slide4text'); ?>
      </div>
    </div>
<?php endif; ?>
<?php if ($this->params->get('slide5')) : ?>
    <div class="item">
      <img src="<?php echo $this->params->get('slide5'); ?>" >
       <div class="carousel-caption">
              <?php echo $this->params->get('slide5text'); ?>
      </div>
    </div>
<?php endif; ?>

  </div>

  <!-- Left and right controls -->
  <a class="left carousel-control" href="#myCarousel" role="button" data-slide="prev">
    <span class="glyphicon glyphicon-chevron-left" aria-hidden="true"></span>
    <span class="sr-only"><</span>
  </a>
  <a class="right carousel-control" href="#myCarousel" role="button" data-slide="next">
    <span class="glyphicon glyphicon-chevron-right" aria-hidden="true"></span>
    <span class="sr-only">></span>
  </a>
</div>


		<?php endif; ?>

			<div class="row-fluid">
				<?php if ($this->countModules('position-8')) : ?>
					<!-- Begin Sidebar -->
					<div id="sidebar" class="span3">
						<div class="sidebar-nav">
							<jdoc:include type="modules" name="position-8" style="xhtml" />
						</div>
					</div>
					<!-- End Sidebar -->
				<?php endif; ?>
				<main id="content" role="main" class="<?php echo $span; ?>">
					<!-- Begin Content -->
					<jdoc:include type="modules" name="position-3" style="xhtml" />
					<jdoc:include type="message" />
					<jdoc:include type="component" />

	<?php if ($this->params->get('socialshare')) : ?>
<style>
.ssb{min-height: 50px; width: 100%; }
</style>
<div class=ssb>
<!-- AddThis Button BEGIN -->
<br><div class="addthis_toolbox addthis_default_style ">
<a class="addthis_button_facebook_like" fb:like:layout="button_count"></a>
<a class="addthis_button_tweet"></a>
</div>
<script type="text/javascript" src="//s7.addthis.com/js/300/addthis_widget.js" async="async"></script>
<!-- AddThis Button END -->
</div>
<?php endif; ?>

					<jdoc:include type="modules" name="position-2" style="none" />
					<!-- End Content -->
				</main>
				<?php if ($this->countModules('position-7')) : ?>
					<div id="aside" class="span3">

						<div align=center>
						<br>
				<?php echo $this->params->get('tplSocial'); ?>&nbsp;
					<?php if ($this->params->get('tplFacebook')) : ?>
							<a target=_blank href="<?php echo $this->params->get('tplFacebook'); ?>"><i class="fa fa-facebook fa-lg"></i></a>
						<?php endif; ?>
						<?php if ($this->params->get('tplTwitter')) : ?>
							<a target=_blank href="<?php echo $this->params->get('tplTwitter'); ?>"><i class="fa fa-twitter fa-lg"></i></a>
						<?php endif; ?>
						<?php if ($this->params->get('tplLinkedin')) : ?>
							<a target=_blank href="<?php echo $this->params->get('tplLinkedin'); ?>"><i class="fa fa-linkedin fa-lg"></i></a>
						<?php endif; ?>
						<?php if ($this->params->get('tplPinterest')) : ?>
							<a target=_blank href="<?php echo $this->params->get('tplPinterest'); ?>"><i class="fa fa-pinterest fa-lg"></i></a>
						<?php endif; ?>
						<?php if ($this->params->get('tplGplus')) : ?>
							<a target=_blank href="<?php echo $this->params->get('tplGplus'); ?>"><i class="fa fa-google-plus fa-lg"></i></a>
						<?php endif; ?>
						<?php if ($this->params->get('tplInst')) : ?>
							<a target=_blank href="<?php echo $this->params->get('tplInst'); ?>"><i class="fa fa-instagram fa-lg"></i></a>
						<?php endif; ?>
		</div><br>


						<!-- Begin Right Sidebar -->
						<jdoc:include type="modules" name="position-7" style="well" />
						<!-- End Right Sidebar -->
					</div>
				<?php endif; ?>

			</div>


			<?php if ($this->countModules('footer1')) : ?>
			     <div class="ftr">

			         <div class="row-fluid">
			            <div class="<?php echo $col_footer; ?>">
			              <jdoc:include type="modules" name="footer1" style="xhtml" />
			            </div>
			             <?php if ($this->countModules('footer2')) : ?>
			             <div class="<?php echo $col_footer; ?>">
			              <jdoc:include type="modules" name="footer2" style="xhtml" />
			            </div>
			             <?php endif;?>
			             <?php if ($this->countModules('footer3')) : ?>
			             <div class="<?php echo $col_footer; ?>">
			              <jdoc:include type="modules" name="footer3" style="xhtml" />
			            </div>
			             <?php endif;?>
			             <?php if ($this->countModules('footer4')) : ?>
			            <div class="<?php echo $col_footer; ?>">
			              <jdoc:include type="modules" name="footer4" style="xhtml" />
			            </div>
			             <?php endif;?>

                         
                        <p class="pull-right">
				<a href="#top" id="back-top">
					<?php echo JText::_('TPL_PROTOSTAR_BACKTOTOP'); ?>
				</a>
			             </p>
                          <?php echo $this->params->get('copyright_text'); ?>
			     </div>


			     </div>
			         </div>
			     <?php endif; ?>

		</div>

	</div>

	<jdoc:include type="modules" name="debug" style="none" />

<?php echo $this->params->get('googleAnalytics'); ?>

<script type="text/javascript">// <![CDATA[
var $ = jQuery.noConflict(); $(document).ready(function()  { $('#myCarousel').carousel({ interval:<?php echo $this->params->get('sliderspeed'); ?>, cycle: true }); });
// ]]></script>


</body>

</html>
