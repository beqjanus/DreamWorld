--
-- Database: `robust`
--
-- --------------------------------------------------------
--
-- Table structure for table `stats`
--

use robust;

CREATE TABLE IF NOT EXISTS `stats` (  
  `regionname` varchar(255) DEFAULT NULL,
  `regionsize` int(11) DEFAULT NULL,
  `locationX` int(11) DEFAULT NULL,
  `locationY` int(11) DEFAULT NULL,
  `UUID` varchar(36) DEFAULT NULL,
  `dateupdated` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

CREATE TABLE IF NOT EXISTS `visitor` (
  `name` varchar(255) DEFAULT NULL,  
  `regionname` varchar(255) DEFAULT NULL,  
  `locationX` int(11) DEFAULT NULL,
  `locationY` int(11) DEFAULT NULL,  
  `dateupdated` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

quit;
