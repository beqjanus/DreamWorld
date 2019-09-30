<?php
/*
 * @component jOpenSim
 * @copyright Copyright (C) 2017 FoTo50 http://www.jopensim.com/
 * @license GNU/GPL v2 http://www.gnu.org/licenses/gpl-2.0.html
 *
 */

// no direct access
defined( '_JEXEC' ) or die( 'Restricted access' );

if(!defined("DS")) define("DS",DIRECTORY_SEPARATOR);

class pkg_jopensimInstallerScript {
	public $installredirect	= "index.php?option=com_config&view=component&component=com_opensim&path=&return=aW5kZXgucGhwP29wdGlvbj1jb21fb3BlbnNpbQ==";
	public $updateredirect	= "index.php?option=com_opensim";

	public function install($parent) {
		JFactory::getApplication()->enqueueMessage('Remember that plugins (jOpenSimRegister, jOpenSimQuickIcon) are not enabled by default', 'notice');
		$parent->getParent()->setRedirectURL($this->installredirect);
	}

	public function update($parent) {
		$parent->getParent()->setRedirectURL($this->updateredirect);
	}
}
?>