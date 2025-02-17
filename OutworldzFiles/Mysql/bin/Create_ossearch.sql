-- phpMyAdmin SQL Dump
-- version 2.7.0-beta1
-- http://www.phpmyadmin.net
-- 
-- Host: localhost
-- Generatie Tijd: 24 Jan 2009 om 15:48
-- Server versie: 5.0.67
-- PHP Versie: 5.2.6-2ubuntu5
-- 
-- Database: `ossearch`
-- 

-- --------------------------------------------------------

-- 
-- Tabel structuur voor tabel `allparcels`
-- 
CREATE DATABASE IF NOT EXISTS  ossearch character set = 'utf8' collate = 'utf8_general_ci';
use ossearch;


CREATE TABLE IF NOT EXISTS `allparcels` (
  `regionUUID` char(36) NOT NULL,
  `parcelname` varchar(255) NOT NULL,
  `ownerUUID` char(36) NOT NULL default '00000000-0000-0000-0000-000000000000',
  `groupUUID` char(36) NOT NULL default '00000000-0000-0000-0000-000000000000',
  `landingpoint` varchar(255) NOT NULL,
  `parcelUUID` char(36) NOT NULL default '00000000-0000-0000-0000-000000000000',
  `infoUUID` char(36) NOT NULL default '00000000-0000-0000-0000-000000000000',
  `parcelarea` int(11) NOT NULL,
  `gateway` char(255) NOT NULL default '',
  PRIMARY KEY  (`parcelUUID`),
  KEY `regionUUID` (`regionUUID`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- --------------------------------------------------------

-- 
-- Tabel structuur voor tabel `classifieds`
-- 

CREATE TABLE IF NOT EXISTS `classifieds` (
  `classifieduuid` char(36) NOT NULL,
  `creatoruuid` char(36) NOT NULL,
  `creationdate` int(20) NOT NULL,
  `expirationdate` int(20) NOT NULL,
  `category` varchar(20) NOT NULL,
  `name` varchar(255) NOT NULL,
  `description` text NOT NULL,
  `parceluuid` char(36) NOT NULL,
  `parentestate` int(11) NOT NULL,
  `snapshotuuid` char(36) NOT NULL,
  `simname` varchar(255) NOT NULL,
  `posglobal` varchar(255) NOT NULL,
  `parcelname` varchar(255) NOT NULL,
  `classifiedflags` int(8) NOT NULL,
  `priceforlisting` int(5) NOT NULL,
  `gateway` char(255) NOT NULL default '',
  PRIMARY KEY  (`classifieduuid`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- --------------------------------------------------------

-- 
-- Tabel structuur voor tabel `events`
-- 

CREATE TABLE IF NOT EXISTS `events` (
  `owneruuid` char(36) NOT NULL,
  `name` varchar(255) NOT NULL,
  `eventid` int(11) unsigned NOT NULL AUTO_INCREMENT,
  `creatoruuid` char(36) NOT NULL,
  `category` int(2) NOT NULL,
  `description` text NOT NULL,
  `dateUTC` int(10) NOT NULL,
  `duration` int(10) NOT NULL,
  `covercharge` tinyint(1) NOT NULL,
  `coveramount` int(10) NOT NULL,
  `simname` varchar(255) NOT NULL,
  `parcelUUID` char(36) NOT NULL,
  `globalPos` varchar(255) NOT NULL,
  `eventflags` int(1) NOT NULL,
  `gateway` char(255) NOT NULL default '',
  PRIMARY KEY (`eventid`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 AUTO_INCREMENT=1 ;

-- --------------------------------------------------------

-- 
-- Tabel structuur voor tabel `hostsregister`
-- 

CREATE TABLE IF NOT EXISTS `hostsregister` (
  `host` varchar(255) NOT NULL,
  `port` int(5) NOT NULL,
  `register` int(10) NOT NULL,
  `nextcheck` int(10) NOT NULL,
  `checked` tinyint(1) NOT NULL,
  `failcounter` int(10) NOT NULL,
  `online` int(1) NOT NULL,
  `gateway` char(255) NOT NULL default '',
  PRIMARY KEY (`host`,`port`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- --------------------------------------------------------

-- 
-- Tabel structuur voor tabel `objects`
-- 

CREATE TABLE IF NOT EXISTS `objects` (
  `objectuuid` char(36) NOT NULL,
  `parceluuid` char(36) NOT NULL,
  `location` varchar(255) NOT NULL,
  `name` varchar(255) NOT NULL,
  `description` varchar(255) NOT NULL,
  `regionuuid` char(36) NOT NULL default '',
  `gateway` char(255) NOT NULL default '',
  `flags` int(11),
  PRIMARY KEY  (`objectuuid`,`parceluuid`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- --------------------------------------------------------

-- 
-- Tabel structuur voor tabel `parcels`
-- 

CREATE TABLE IF NOT EXISTS `parcels` (
  `regionUUID` char(36) NOT NULL,
  `parcelname` varchar(255) NOT NULL,
  `parcelUUID` char(36) NOT NULL,
  `landingpoint` varchar(255) NOT NULL,
  `description` varchar(255) NOT NULL,
  `searchcategory` varchar(50) NOT NULL,
  `build` enum('true','false') NOT NULL,
  `script` enum('true','false') NOT NULL,
  `public` enum('true','false') NOT NULL,
  `dwell` float NOT NULL default '0',
  `infouuid` varchar(36) NOT NULL default '',
  `mature` varchar(10) NOT NULL default 'PG',
  `gateway` char(255) NOT NULL default '',
  PRIMARY KEY  (`regionUUID`,`parcelUUID`),
  KEY `name` (`parcelname`),
  KEY `description` (`description`),
  KEY `searchcategory` (`searchcategory`),
  KEY `dwell` (`dwell`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- --------------------------------------------------------

-- 
-- Tabel structuur voor tabel `parcelsales`
-- 

CREATE TABLE IF NOT EXISTS `parcelsales` (
  `regionUUID` char(36) NOT NULL,
  `parcelname` varchar(255) NOT NULL,
  `parcelUUID` char(36) NOT NULL,
  `area` int(6) NOT NULL,
  `saleprice` int(11) NOT NULL,
  `landingpoint` varchar(255) NOT NULL,
  `infoUUID` char(36) NOT NULL default '00000000-0000-0000-0000-000000000000',
  `dwell` int(11) NOT NULL,
  `parentestate` int(11) NOT NULL default '1',
  `mature` varchar(10) NOT NULL default 'PG',
  `gateway` char(255) NOT NULL default '',
  PRIMARY KEY  (`regionUUID`,`parcelUUID`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- --------------------------------------------------------

-- 
-- Tabel structuur voor tabel `popularplaces`
-- 

CREATE TABLE IF NOT EXISTS `popularplaces` (
  `parcelUUID` char(36) NOT NULL,
  `name` varchar(255) NOT NULL,
  `dwell` float NOT NULL,
  `infoUUID` char(36) NOT NULL,
  `has_picture` tinyint(1) NOT NULL,
  `mature` varchar(10) COLLATE utf8_unicode_ci NOT NULL,
  `gateway` char(255) NOT NULL default '',
  PRIMARY KEY  (`parcelUUID`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- --------------------------------------------------------

-- 
-- Tabel structuur voor tabel `regions`
-- 

CREATE TABLE IF NOT EXISTS `regions` (
  `regionname` varchar(255) NOT NULL,
  `regionUUID` char(36) NOT NULL,
  `regionhandle` varchar(255) NOT NULL,
  `url` varchar(255) NOT NULL,
  `owner` varchar(255) NOT NULL,
  `owneruuid` char(36) NOT NULL,
  `gateway` char(255) NOT NULL default '',
  PRIMARY KEY  (`regionUUID`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

grant all on ossearch.* to 'robustuser'@'localhost';
quit;
