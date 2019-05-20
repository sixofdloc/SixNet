-- MySQL dump 10.13  Distrib 5.7.26, for Linux (x86_64)
--
-- Host: localhost    Database: bbs
-- ------------------------------------------------------
-- Server version	5.7.26-0ubuntu0.18.04.1

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `AccessGroups`
--

DROP TABLE IF EXISTS `AccessGroups`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `AccessGroups` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Title` varchar(25) CHARACTER SET utf8 DEFAULT NULL,
  `Description` varchar(255) CHARACTER SET utf8 DEFAULT NULL,
  `CallsPerDay` int(11) DEFAULT NULL,
  `MinutesPerCall` int(11) DEFAULT NULL,
  `AllowSysop` bit(1) DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `AccessGroups`
--

LOCK TABLES `AccessGroups` WRITE;
/*!40000 ALTER TABLE `AccessGroups` DISABLE KEYS */;
/*!40000 ALTER TABLE `AccessGroups` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `BBSConfigs`
--

DROP TABLE IF EXISTS `BBSConfigs`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `BBSConfigs` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `SysOpUserId` int(11) DEFAULT NULL,
  `BBSName` varchar(255) CHARACTER SET utf8 DEFAULT NULL,
  `BBSUrl` varchar(255) CHARACTER SET utf8 DEFAULT NULL,
  `BBSPort` int(11) DEFAULT NULL,
  `SysOpPublicHandle` varchar(255) CHARACTER SET utf8 DEFAULT NULL,
  `SysOpEmail` varchar(255) CHARACTER SET utf8 DEFAULT NULL,
  `SysOpMenuPassword` varchar(255) CHARACTER SET utf8 DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `SYSOPUSERID` (`SysOpUserId`),
  CONSTRAINT `fk_BBSConfig_1` FOREIGN KEY (`SysOpUserId`) REFERENCES `Users` (`Id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `BBSConfigs`
--

LOCK TABLES `BBSConfigs` WRITE;
/*!40000 ALTER TABLE `BBSConfigs` DISABLE KEYS */;
INSERT INTO `BBSConfigs` VALUES (4,5,'The Darkside','thedarkside.dnsalias.net',6969,'Six/DLoC','sixofdloc@gmail.com','bellmaster');
/*!40000 ALTER TABLE `BBSConfigs` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `CallLogs`
--

DROP TABLE IF EXISTS `CallLogs`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `CallLogs` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `UserId` int(11) NOT NULL,
  `Connected` datetime DEFAULT NULL,
  `Disconnected` datetime DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `fk_new_table_1_idx` (`UserId`),
  CONSTRAINT `fk_new_table_1` FOREIGN KEY (`UserId`) REFERENCES `Users` (`Id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=74 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `CallLogs`
--

LOCK TABLES `CallLogs` WRITE;
/*!40000 ALTER TABLE `CallLogs` DISABLE KEYS */;
INSERT INTO `CallLogs` VALUES (6,5,'2019-04-17 10:24:19','2019-04-17 10:24:19'),(7,5,'2019-04-17 10:30:11','2019-04-17 10:30:11'),(8,5,'2019-04-17 10:31:03','2019-04-17 10:31:03'),(9,5,'2019-04-17 10:38:49','2019-04-17 10:38:49'),(10,5,'2019-04-17 12:48:40','2019-04-17 12:48:40'),(11,5,'2019-04-17 12:51:35','2019-04-17 12:51:35'),(12,5,'2019-04-17 12:52:33','2019-04-17 12:52:33'),(13,5,'2019-04-17 13:15:21','2019-04-17 13:15:21'),(14,5,'2019-04-17 14:00:06','2019-04-17 14:00:06'),(15,5,'2019-04-17 17:09:10','2019-04-17 17:09:10'),(16,5,'2019-04-17 17:20:57','2019-04-17 17:20:57'),(18,6,'2019-04-17 17:36:07','2019-04-17 17:36:07'),(19,6,'2019-04-18 00:03:30','2019-04-18 00:03:30'),(20,6,'2019-04-18 08:02:39','2019-04-18 08:02:39'),(21,5,'2019-04-18 08:02:57','2019-04-18 08:02:57'),(22,6,'2019-04-18 08:49:13','2019-04-18 08:49:13'),(23,7,'2019-04-18 09:59:31','2019-04-18 09:59:31'),(25,6,'2019-04-18 10:11:25','2019-04-18 10:11:25'),(26,5,'2019-04-18 14:58:25','2019-04-18 14:58:25'),(27,5,'2019-04-18 15:00:10','2019-04-18 15:00:10'),(28,5,'2019-04-18 15:01:51','2019-04-18 15:01:51'),(29,5,'2019-04-18 15:19:39','2019-04-18 15:19:39'),(30,5,'2019-04-18 15:27:50','2019-04-18 15:27:50'),(31,5,'2019-04-18 15:29:00','2019-04-18 15:29:00'),(32,5,'2019-04-18 15:33:11','2019-04-18 15:33:11'),(33,5,'2019-04-18 15:38:06','2019-04-18 15:38:06'),(34,5,'2019-04-18 15:41:33','2019-04-18 15:41:33'),(35,5,'2019-04-18 15:42:45','2019-04-18 15:42:45'),(36,5,'2019-04-18 15:46:54','2019-04-18 15:46:54'),(37,5,'2019-04-18 15:51:10','2019-04-18 15:51:10'),(38,5,'2019-04-18 15:51:59','2019-04-18 15:51:59'),(39,5,'2019-04-18 15:58:34','2019-04-18 15:58:34'),(42,5,'2019-04-19 09:33:41','2019-04-19 09:33:41'),(44,13,'2019-04-19 10:00:46','2019-04-19 10:00:46'),(45,14,'2019-04-19 10:05:30','2019-04-19 10:05:30'),(46,5,'2019-04-19 10:06:18','2019-04-19 10:06:18'),(47,5,'2019-04-19 11:50:31','2019-04-19 11:50:31'),(48,5,'2019-04-19 11:51:11','2019-04-19 11:51:11'),(49,5,'2019-04-19 11:52:30','2019-04-19 11:52:30'),(50,5,'2019-04-19 11:53:37','2019-04-19 11:53:37'),(51,15,'2019-04-19 12:01:20','2019-04-19 12:01:20'),(52,5,'2019-04-19 12:14:42','2019-04-19 12:14:42'),(53,5,'2019-04-19 12:17:20','2019-04-19 12:17:20'),(54,5,'2019-04-19 12:18:57','2019-04-19 12:18:57'),(55,5,'2019-04-22 16:22:12','2019-04-22 16:22:12'),(56,5,'2019-05-01 10:56:17','2019-05-01 10:56:17'),(57,5,'2019-05-01 13:15:09','2019-05-01 13:15:09'),(58,5,'2019-05-01 13:23:17','2019-05-01 13:23:17'),(59,5,'2019-05-01 13:25:15','2019-05-01 13:25:15'),(60,5,'2019-05-01 13:26:46','2019-05-01 13:26:46'),(61,5,'2019-05-01 14:39:12','2019-05-01 14:39:12'),(62,5,'2019-05-01 17:19:24','2019-05-01 17:19:24'),(63,5,'2019-05-06 16:07:36','2019-05-06 16:07:36'),(64,5,'2019-05-06 16:22:08','2019-05-06 16:22:08'),(65,5,'2019-05-06 16:29:30','2019-05-06 16:29:30'),(66,5,'2019-05-06 16:36:33','2019-05-06 16:36:33'),(67,5,'2019-05-06 16:44:39','2019-05-06 16:44:39'),(68,5,'2019-05-06 17:51:04','2019-05-06 17:51:04'),(69,5,'2019-05-08 17:06:32','2019-05-08 17:06:32'),(70,5,'2019-05-08 17:10:17','2019-05-08 17:10:17'),(71,5,'2019-05-09 13:51:19','2019-05-09 13:51:19'),(72,5,'2019-05-09 13:53:41','2019-05-09 13:53:41'),(73,5,'2019-05-09 13:57:11','2019-05-09 13:57:11');
/*!40000 ALTER TABLE `CallLogs` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Feedbacks`
--

DROP TABLE IF EXISTS `Feedbacks`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `Feedbacks` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Read` bit(1) DEFAULT NULL,
  `Subject` varchar(255) CHARACTER SET utf8 DEFAULT NULL,
  `Body` text,
  `Sent` datetime DEFAULT NULL,
  `UserId` int(11) NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `useridindex` (`UserId`),
  CONSTRAINT `fk_Feedback_User` FOREIGN KEY (`UserId`) REFERENCES `Users` (`Id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=23 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Feedbacks`
--

LOCK TABLES `Feedbacks` WRITE;
/*!40000 ALTER TABLE `Feedbacks` DISABLE KEYS */;
INSERT INTO `Feedbacks` VALUES (16,_binary '\0','New User Feedback','I am so gay.~ÿ~~ÿ~','2019-04-19 10:00:41',13),(17,_binary '\0','New User Feedback','i are dick hors~ÿ~~ÿ~','2019-04-19 10:05:30',14),(18,_binary '\0','Feedback','maoar feed~ÿ~back~ÿ~~ÿ~','2019-04-19 10:06:00',14),(19,_binary '\0','New User Feedback','Time to test this again.~ÿ~~ÿ~And see if I can break something.~ÿ~~ÿ~Oooh, I just did, look at the way the ca~ÿ~se flipped!~ÿ~~ÿ~Yep!~ÿ~/~ÿ~~ÿ~','2019-04-19 12:01:20',15),(20,_binary '\0','Feedback','Hey horse dick nick! Cool to see you bac~ÿ~k in cyberspace!~ÿ~~ÿ~','2019-04-19 12:02:07',15),(21,_binary '\0','Feedback','~ÿ~','2019-04-22 16:22:42',5),(22,_binary '\0','Feedback','Testing~ÿ~~ÿ~','2019-04-24 10:31:25',5);
/*!40000 ALTER TABLE `Feedbacks` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Fields`
--

DROP TABLE IF EXISTS `Fields`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `Fields` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `UserId` int(11) DEFAULT NULL,
  `FieldName` varchar(255) CHARACTER SET utf8 NOT NULL,
  `FieldContents` text,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `Id_UNIQUE` (`Id`),
  KEY `User` (`UserId`),
  KEY `fieldname` (`FieldName`),
  CONSTRAINT `genericfiled_user` FOREIGN KEY (`UserId`) REFERENCES `Users` (`Id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Fields`
--

LOCK TABLES `Fields` WRITE;
/*!40000 ALTER TABLE `Fields` DISABLE KEYS */;
INSERT INTO `Fields` VALUES (1,5,'EMPIRE','{\"UserId\":5,\"Username\":\"Six\",\"Land\":25,\"Grain\":22,\"Gold\":172,\"Tax\":25,\"Mills\":0,\"Markets\":0,\"Serfs\":0,\"Soldiers\":0,\"Nobles\":0,\"Castle\":0,\"Shipyards\":0,\"Foundries\":0,\"Turns\":68,\"ArmyMobile\":true,\"LastPlay\":\"2019-05-09T13:59:01.534073-04:00\"}'),(4,NULL,'EMPIRENEWS','|Six was too STINGY!');
/*!40000 ALTER TABLE `Fields` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `GFileAreaAccessGroups`
--

DROP TABLE IF EXISTS `GFileAreaAccessGroups`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `GFileAreaAccessGroups` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `GFileAreaId` int(11) DEFAULT NULL,
  `AccessGroupId` int(11) DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `gfilearea` (`GFileAreaId`),
  KEY `accessgroup` (`AccessGroupId`),
  CONSTRAINT `fk_GFileAreaAccessGroups_1` FOREIGN KEY (`AccessGroupId`) REFERENCES `AccessGroups` (`Id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `fk_GFileAreaAccessGroups_2` FOREIGN KEY (`GFileAreaId`) REFERENCES `GFileAreas` (`Id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `GFileAreaAccessGroups`
--

LOCK TABLES `GFileAreaAccessGroups` WRITE;
/*!40000 ALTER TABLE `GFileAreaAccessGroups` DISABLE KEYS */;
/*!40000 ALTER TABLE `GFileAreaAccessGroups` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `GFileAreas`
--

DROP TABLE IF EXISTS `GFileAreas`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `GFileAreas` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Title` varchar(25) CHARACTER SET utf8 DEFAULT NULL,
  `Description` varchar(255) CHARACTER SET utf8 DEFAULT NULL,
  `ParentAreaId` int(11) DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `GFileAreas`
--

LOCK TABLES `GFileAreas` WRITE;
/*!40000 ALTER TABLE `GFileAreas` DISABLE KEYS */;
INSERT INTO `GFileAreas` VALUES (1,'PETSCII','C64 C/G (PETSCII) pictures and animations',NULL),(2,'Classic HPA','80\'s era Hack/Phreak/Anarchy textfiles',NULL),(3,'Ebooks','Online library of books',NULL),(4,'DLoC','Files from DLoC',2);
/*!40000 ALTER TABLE `GFileAreas` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `GFileDetails`
--

DROP TABLE IF EXISTS `GFileDetails`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `GFileDetails` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Title` varchar(25) CHARACTER SET utf8 DEFAULT NULL,
  `Description` varchar(255) CHARACTER SET utf8 DEFAULT NULL,
  `Filename` varchar(255) CHARACTER SET utf8 DEFAULT NULL,
  `PETSCII` bit(1) DEFAULT NULL,
  `Added` datetime DEFAULT NULL,
  `GFIleAreaId` int(11) DEFAULT NULL,
  `FileSizeInBytes` int(11) DEFAULT NULL,
  `FilePath` varchar(512) DEFAULT NULL,
  `Notes` varchar(512) DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `area` (`GFIleAreaId`),
  CONSTRAINT `fk_GFileDetails_1` FOREIGN KEY (`GFIleAreaId`) REFERENCES `GFileAreas` (`Id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=258 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `GFileDetails`
--

LOCK TABLES `GFileDetails` WRITE;
/*!40000 ALTER TABLE `GFileDetails` DISABLE KEYS */;
INSERT INTO `GFileDetails` VALUES (1,'001.txt','How to Make a Dry Ice Bomb, by Vortex','001.txt',_binary '\0','2019-05-01 10:53:20',2,1151,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(2,'176.txt','The Ninja Warrior Presents: Poison #1','176.txt',_binary '\0','2019-05-01 10:53:39',2,7551,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(3,'201.txt','ANFOS: Ammonium Nitrate - Fuel Oil Solution','201.txt',_binary '\0','2019-05-01 10:53:41',2,5949,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(4,'202.txt','Picric Acid, by Exodus','202.txt',_binary '\0','2019-05-01 10:53:42',2,3362,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(5,'203.txt','Chemical Fire Bottle','203.txt',_binary '\0','2019-05-01 10:53:43',2,5428,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(6,'206.txt','Film Canisters 2, by Bill and Exodus','206.txt',_binary '\0','2019-05-01 10:53:43',2,681,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(7,'214.txt','Lists of Suppliers and More Information','214.txt',_binary '\0','2019-05-01 10:53:43',2,3231,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(8,'aa.txt','Samaurai Cat Presents Scanning: The Art of Non-Selective Intervention','aa.txt',_binary '\0','2019-05-01 10:53:43',2,10217,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(9,'ac.ana','Anarchist\'s Ordering Catalog','ac.ana',_binary '\0','2019-05-01 10:53:43',2,3725,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(10,'acbfile.txt','Recipes for Nonsurvival - The Anarchist Cookbook by William Powell (Review by Esperanza Godot)','acbfile.txt',_binary '\0','2019-05-01 10:53:43',2,12119,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(11,'address.ana','Gray-Area Company List - Technology 1 BBS - 89-01-15','address.ana',_binary '\0','2019-05-01 10:53:43',2,21415,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(12,'alarm.fun','Fun with Alarms, from the Stainless Steal Rat','alarm.fun',_binary '\0','2019-05-01 10:53:43',2,2404,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(13,'alko170.txt','The ALKO Buying Guide, for People Under 20 Years Version 1.7, by Matte (1991)','alko170.txt',_binary '\0','2019-05-01 10:53:43',2,25645,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(14,'anarc25.txt','The Anarchives: Power and Language','anarc25.txt',_binary '\0','2019-05-01 10:53:43',2,16753,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(15,'anarchist.txt','Anarchists: The Social Loosers, by Mr. C','anarchist.txt',_binary '\0','2019-05-01 10:53:43',2,5041,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(16,'anarchy.txt','A Guide to Anarchy by Berzerker','anarchy.txt',_binary '\0','2019-05-01 10:53:43',2,3999,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(17,'anarchy3','Anarchy Today Issue #2: Evading Bomb Squad Tactics','anarchy3',_binary '\0','2019-05-01 10:53:43',2,57272,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(18,'anarcy1.txt','A Collection of Anarchist Files downloaded from Thrasher\'s Way on the Pipeline BBS','anarcy1.txt',_binary '\0','2019-05-01 10:53:43',2,61440,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(19,'anarcy1b.txt','A Collection of Anarchist Files downloaded from Thrasher\'s Way on the Pipeline BBS (Part II)','anarcy1b.txt',_binary '\0','2019-05-01 10:53:43',2,41057,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(20,'anarcy2.txt','A Collection of Anarchist Files downloaded from Thrasher\'s Way on the Pipeline BBS (III)','anarcy2.txt',_binary '\0','2019-05-01 10:53:43',2,32768,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(21,'anarcy4.txt','A Collection of Anarchist Files downloaded from Thrasher\'s Way on the Pipeline BBS (Part IV)','anarcy4.txt',_binary '\0','2019-05-01 10:53:43',2,43008,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(22,'anarkunc.txt','You\'re Wrong! An Irregular Column, by Mykel Board','anarkunc.txt',_binary '\0','2019-05-01 10:53:43',2,29247,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(23,'anarky.txt','iXHS (Information Express by Helter Skelter Issue #18):  r00iners AnArKy pHilEz v0l 1.','anarky.txt',_binary '\0','2019-05-01 10:53:43',2,8510,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(24,'anti.modem.weap','The Anti-Modem Weapon from the Enemy Within','anti.modem.weap',_binary '\0','2019-05-01 10:53:44',2,2509,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(25,'aoa4.txt','How to Make your Own Poison, by Moradian of AoA','aoa4.txt',_binary '\0','2019-05-01 10:53:44',2,3233,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(26,'aofa-1.txt','Anarchist of America by Clarence Bodicker (Part 1)','aofa-1.txt',_binary '\0','2019-05-01 10:53:44',2,5443,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(27,'aofa-2.txt','Anarchist of America by Clarence Bodicker (Part 2)','aofa-2.txt',_binary '\0','2019-05-01 10:53:44',2,4851,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(28,'aofa-3.txt','Anarchist of America by Clarence Bodicker (Part 3(','aofa-3.txt',_binary '\0','2019-05-01 10:53:44',2,4592,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(29,'aofa-4.txt','Anarchist of America by Clarence Bodicker (Part 4)','aofa-4.txt',_binary '\0','2019-05-01 10:53:44',2,4619,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(30,'assassination.txt','CIA Assassination Manual (From Freedom of Information Act) (1997)','assassination.txt',_binary '\0','2019-05-01 10:53:44',2,32741,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(31,'at.txt','Anarchy Today Issue #1 by Jack The Ripper','at.txt',_binary '\0','2019-05-01 10:53:44',2,44184,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(32,'atmosph.ere','Kenmore Square, Boston...atmospheric test site discovered','atmosph.ere',_binary '\0','2019-05-01 10:53:44',2,16080,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(33,'badmind1.txt','Dr. Badmind\'s Chemistry Files #1','badmind1.txt',_binary '\0','2019-05-01 10:53:44',2,3654,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(34,'badmind2.txt','Dr. Badmind\'s Chemistry Files #2','badmind2.txt',_binary '\0','2019-05-01 10:53:44',2,5394,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(35,'badmind3.txt','Dr. Badmind\'s Chemistry Files #3','badmind3.txt',_binary '\0','2019-05-01 10:53:44',2,4374,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(36,'badmind4.txt','Dr. Badmind\'s Chemistry Files #4','badmind4.txt',_binary '\0','2019-05-01 10:53:44',2,1024,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(37,'badmind5.txt','Dr. Badmind\'s Chemistry Files #5','badmind5.txt',_binary '\0','2019-05-01 10:53:44',2,2114,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(38,'badmind6.txt','Dr. Badmind\'s Chemistry Files #6','badmind6.txt',_binary '\0','2019-05-01 10:53:44',2,3539,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(39,'badmind7.txt','Dr. Badmind\'s Chemistry Files #7','badmind7.txt',_binary '\0','2019-05-01 10:53:44',2,2721,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(40,'balloon.txt','Balloon Bombs, by Anarchist Elf','balloon.txt',_binary '\0','2019-05-01 10:53:44',2,960,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(41,'bank','The Brotherhood of the Falcon Presents How to Rip Off Your Local Bank, by Conan the Barbarian','bank',_binary '\0','2019-05-01 10:53:44',2,3003,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(42,'bankomat.txt','How to Take Money From a Bankomat Without Paying For It','bankomat.txt',_binary '\0','2019-05-01 10:53:44',2,876,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(43,'beaspy.ana','A Quick Quiz to See if you\'d make a Good Spy','beaspy.ana',_binary '\0','2019-05-01 10:53:44',2,3752,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(44,'beat.the.scan.t','Mastering the Scantron by The Warhead','beat.the.scan.t',_binary '\0','2019-05-01 10:53:44',2,3712,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(45,'bibfraud.txt','The Bible of Fraud, by Sneak Thief (1985)','bibfraud.txt',_binary '\0','2019-05-01 10:53:44',2,9159,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(46,'bigsecr1.txt','Big Secrets Volume 1, by the Wyvern','bigsecr1.txt',_binary '\0','2019-05-01 10:53:44',2,5535,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(47,'bigsecr2.txt','Big Secrets Volume 2, by the Wyvern','bigsecr2.txt',_binary '\0','2019-05-01 10:53:44',2,8199,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(48,'books','The MAD! Source Book Part #3 Revised Addition (December 20, 1987)','books',_binary '\0','2019-05-01 10:53:44',2,8458,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(49,'break.in.car','How to Break into a Car, by Zoron','break.in.car',_binary '\0','2019-05-01 10:53:44',2,5048,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(50,'bridged.txt','Bridge Destruction','bridged.txt',_binary '\0','2019-05-01 10:53:44',2,5617,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(51,'build_an_h_bomb.txt','Basement H-Bomb Production (Have You Really Come to Love the Bomb?)','build_an_h_bomb.txt',_binary '\0','2019-05-01 10:53:44',2,23675,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(52,'catlogs.txt','The Canonical List of Undergroup Catalogs','catlogs.txt',_binary '\0','2019-05-01 10:53:44',2,14636,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(53,'change.machine','Change Machine Fraud by The Prisoner of The 3rd Reich','change.machine',_binary '\0','2019-05-01 10:53:44',2,2304,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(54,'chekgame.txt','Check Games, by Dark Grif (Septmber 26, 1993)','chekgame.txt',_binary '\0','2019-05-01 10:53:44',2,14172,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(55,'chemical.txt','Know What You\'re Handling: A List of Chemicals, Their Dangers and Uses','chemical.txt',_binary '\0','2019-05-01 10:53:44',2,50881,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(56,'ciaman.txt','The FM 95-1A CIA Guerrilla War Manual (Part I) by Samurai Cat','ciaman.txt',_binary '\0','2019-05-01 10:53:44',2,14772,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(57,'civilian.war','The Anarchist\'s Guide to Civilian Warfare and Sabotage by The Tracker (February, 1987(','civilian.war',_binary '\0','2019-05-01 10:53:44',2,22535,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(58,'commando.1','The Commando Series Part 1 by Elliot Ness and The Untouchable','commando.1',_binary '\0','2019-05-01 10:53:44',2,5843,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(59,'commando.2','The Commando Series File 2: Vietnam Phrases, by Elliot Ness and The Untouchable','commando.2',_binary '\0','2019-05-01 10:53:44',2,4115,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(60,'cookbook.iv','The Anarchist Cookbook Version IV from Exodus (1994)','cookbook.iv',_binary '\0','2019-05-01 10:53:44',2,5107,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(61,'courier.txt','Fly Free: The Courier Route','courier.txt',_binary '\0','2019-05-01 10:53:44',2,12526,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(62,'curing.boredom','Anti-Boredom Activities: 44 Things to Do When You\'re Bored, by Shooting Shark','curing.boredom',_binary '\0','2019-05-01 10:53:44',2,9979,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(63,'departmentt.sto','Department Store Fun, by Agent 81, from TAP Magazine','departmentt.sto',_binary '\0','2019-05-01 10:53:44',2,2102,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(64,'depth.charge.fi','Fishing with Depth Charges by The Blue Buccaneer','depth.charge.fi',_binary '\0','2019-05-01 10:53:44',2,2560,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(65,'dingdong.txt','Ding Dong Ditch by Jonin Meka of the Black Hand Society','dingdong.txt',_binary '\0','2019-05-01 10:53:44',2,1208,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(66,'dmv.txt','Driver\'s License Fingerprint and Document Requirments','dmv.txt',_binary '\0','2019-05-01 10:53:44',2,1894,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(67,'drive.killing','Head Grinder, by The Talon','drive.killing',_binary '\0','2019-05-01 10:53:44',2,2537,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(68,'drugs','Smuggling and Dope, by John Shaver (December, 1982)','drugs',_binary '\0','2019-05-01 10:53:44',2,3632,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(69,'dry.ice.gun','A Step by Step Guide to Making a Dry Ice Gun, by The Voice Over','dry.ice.gun',_binary '\0','2019-05-01 10:53:44',2,12482,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(70,'dstrylox.txt','Lock Destruction by Captain Hack (March 18, 1995)','dstrylox.txt',_binary '\0','2019-05-01 10:53:44',2,1746,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(71,'dust.bomb','Initiator for Dust Explosions, by Mr. Byte-Zap','dust.bomb',_binary '\0','2019-05-01 10:53:44',2,2432,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(72,'easy.money','Earn Large Amounts of Money With YOur Computer From Your Own Home!','easy.money',_binary '\0','2019-05-01 10:53:44',2,11660,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(73,'elecfun.txt','Fun With Electronics by The King of Roo','elecfun.txt',_binary '\0','2019-05-01 10:53:44',2,2271,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(74,'electronic.bugs','The Bug Detector! By Ford Prefect','electronic.bugs',_binary '\0','2019-05-01 10:53:44',2,10976,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(75,'fakeid','Getting a Fake Driver\'s License by The Sultan (February 26, 1986)','fakeid',_binary '\0','2019-05-01 10:53:44',2,2843,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(76,'famphun.txt','The Pyromaniac\'s Guide to Family Entertainment Volume 1 by The Great White and Cal Songsinger','famphun.txt',_binary '\0','2019-05-01 10:53:44',2,3061,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(77,'feds1.txt','The Feds Part I: Electronic Surveillance by The Dutchman of The Shadow Brotherhood II','feds1.txt',_binary '\0','2019-05-01 10:53:44',2,4352,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(78,'feds2.txt','The Feds Part II: Electronic Surveillance by The Dutchman of Shadow Brotherhood','feds2.txt',_binary '\0','2019-05-01 10:53:44',2,2560,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(79,'feds3.txt','The Feds Part III: Fed Fraud, by The Dutchman of Shadow Brotherhood','feds3.txt',_binary '\0','2019-05-01 10:53:44',2,3072,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(80,'feds4.txt','The Feds Part IV: Fed Fraud by The Dutchman of Shadow Brotherhood','feds4.txt',_binary '\0','2019-05-01 10:53:44',2,3072,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(81,'freenews.ana','Everyone Has a Right to the News','freenews.ana',_binary '\0','2019-05-01 10:53:44',2,3279,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(82,'freenews.txt','Getting Free Access to Newspapers','freenews.txt',_binary '\0','2019-05-01 10:53:44',2,2736,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(83,'fuksomeone.txt','How to Get Detailed Information on Anybody by BTS','fuksomeone.txt',_binary '\0','2019-05-01 10:53:44',2,6923,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(84,'gambling.txt','Running a Successful and Profitable Gambling Ring by Candyman (1993)','gambling.txt',_binary '\0','2019-05-01 10:53:44',2,8190,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(85,'games1.con','Tap Corner, Word Processed by the Mulcher ][ for Apple Trek Systems','games1.con',_binary '\0','2019-05-01 10:53:44',2,3157,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(86,'getinfo.ana','How to Get Anything on Anyone, by Toxic Tunic','getinfo.ana',_binary '\0','2019-05-01 10:53:44',2,10252,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(87,'glassdem.txt','Glass Demolition by Captain Hack (November 16, 1995)','glassdem.txt',_binary '\0','2019-05-01 10:53:44',2,2349,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(88,'good_start.txt','A Good Start to Being a Survivalist by J. Moschell (September 15, 1994)','good_start.txt',_binary '\0','2019-05-01 10:53:44',2,2977,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(89,'guide.to.shopli','How to Shoplift, by Ziggy','guide.to.shopli',_binary '\0','2019-05-01 10:53:44',2,2402,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(90,'gunfact.dos','Firearms, Hunting and Trapping: Facts and Positions by Stephen P. Jeffries','gunfact.dos',_binary '\0','2019-05-01 10:53:44',2,51283,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(91,'gunpowder','How to Make Gunpowder and Detonation Devices by The Wave','gunpowder',_binary '\0','2019-05-01 10:53:44',2,4052,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(92,'hackcabl.txt','The Hacker\'s Guide to Cable TV on San Francisco Viacom','hackcabl.txt',_binary '\0','2019-05-01 10:53:44',2,2189,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(93,'handbook.txt','The Anarchists\' Home Companion: First Release: June 1st 1989','handbook.txt',_binary '\0','2019-05-01 10:53:44',2,122702,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(94,'highway.txt','Roadway Fun, by Hell\'s Angel (June 14, 1996)','highway.txt',_binary '\0','2019-05-01 10:53:44',2,2858,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(95,'hijack.hac','Nuclear Transit on the Highways, by Data Line.','hijack.hac',_binary '\0','2019-05-01 10:53:44',2,8354,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(96,'holiday.anarchy','Happy Holidays, The Latest File from The Static Syndicate and The Ninja','holiday.anarchy',_binary '\0','2019-05-01 10:53:44',2,6272,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(97,'home.fun.2','A Nitroglycerin Recipe','home.fun.2',_binary '\0','2019-05-01 10:53:44',2,11945,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(98,'homemade.fun','Anarchy: How to Make Smoke Bombs','homemade.fun',_binary '\0','2019-05-01 10:53:44',2,7476,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(99,'hotwire','How to Hot-Wire a Car, from the Boca Bandit','hotwire',_binary '\0','2019-05-01 10:53:44',2,1536,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(100,'how.hotwire.car','How to Hot-Wire a Car by the Boca Bandit','how.hotwire.car',_binary '\0','2019-05-01 10:53:44',2,1408,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(101,'hydrocloric.aci','The Hydrochloric Acid Goody','hydrocloric.aci',_binary '\0','2019-05-01 10:53:45',2,4352,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(102,'illegalcomp.txt','A Listing of Illegal Stuff, by Desiro Nurvoso of the ATDT BBS (January 24, 1993)','illegalcomp.txt',_binary '\0','2019-05-01 10:53:45',2,21918,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(103,'imh1.txt','Improvised Munitions Handbook TM 31-210 from the Department of the Army','imh1.txt',_binary '\0','2019-05-01 10:53:45',2,44823,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(104,'imh2.txt','Improvised Munitions Handbook TM 31-210 from the Department of the Army (Part II)','imh2.txt',_binary '\0','2019-05-01 10:53:45',2,21464,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(105,'imh3.txt','Improvised Munitions Handbook TM 31-210 from the Department of the Army (Part III)','imh3.txt',_binary '\0','2019-05-01 10:53:45',2,30296,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(106,'index.txt','Index to the Anarchist Cookbook IV, ver. 4.14','index.txt',_binary '\0','2019-05-01 10:53:45',2,6980,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(107,'info.ana','How to get Anything on Anyone','info.ana',_binary '\0','2019-05-01 10:53:45',2,11546,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(108,'info.txt','What Information Can be Obtained, and Where to Obtain it, by CarbonBoy (1995)','info.txt',_binary '\0','2019-05-01 10:53:45',2,32067,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(109,'ipecac.txt','IPECAC: The Anarchist\'s Friend by The Mage','ipecac.txt',_binary '\0','2019-05-01 10:53:45',2,2285,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(110,'irritant.txt','Chemical Warfare, by Sir Francis Drake','irritant.txt',_binary '\0','2019-05-01 10:53:45',2,9109,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(111,'irview.txt','Infrared Viewer Plans, by Dick Smith, alias The Ghost (July 13, 1986)','irview.txt',_binary '\0','2019-05-01 10:53:45',2,14592,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(112,'irviewer.txt','Infrared Viewer Plans','irviewer.txt',_binary '\0','2019-05-01 10:53:45',2,14366,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(113,'jacob.fun','How to Construct a High Voltage Jacob\'s Ladder by Count Zero','jacob.fun',_binary '\0','2019-05-01 10:53:45',2,12327,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(114,'jammer','Highway Radar Jamming','jammer',_binary '\0','2019-05-01 10:53:45',2,2302,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(115,'kanalx.txt','Pirate TV in Eastern Europe by Evelyn Messinger','kanalx.txt',_binary '\0','2019-05-01 10:53:45',2,19150,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(116,'killbees.txt','How to Have Phun with Bottle Rockets and Bees, Hornets and Large Stinging Insects by Joe Shmoe the Eskimo and Tour De France','killbees.txt',_binary '\0','2019-05-01 10:53:45',2,12754,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(117,'kurt.txt','A Listing of Kurt Saxon Books by The Wild Bunch','kurt.txt',_binary '\0','2019-05-01 10:53:45',2,8582,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(118,'lab-safe.txt','An Introduction to Laboratory Safety (October 31, 1993)','lab-safe.txt',_binary '\0','2019-05-01 10:53:45',2,14205,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(119,'laughing.gas','Laughing Gas, from the Poor Man\'s James Bond by Scarlet Armadillo','laughing.gas',_binary '\0','2019-05-01 10:53:45',2,4096,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(120,'letter.bomb','How to Make a Working Letter-Bomb by The Revel Warhead','letter.bomb',_binary '\0','2019-05-01 10:53:45',2,3234,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(121,'lock.picking','Lock Picking, by Black Cobra','lock.picking',_binary '\0','2019-05-01 10:53:45',2,3586,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(122,'lockerdocs.txt','Locked Docs: by Cablecast Operator and Silver Sphere','lockerdocs.txt',_binary '\0','2019-05-01 10:53:45',2,7786,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(123,'lockpick','FAQ: The Alt.Locksmithing FAQ (June 16, 1992)','lockpick',_binary '\0','2019-05-01 10:53:45',2,21854,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(124,'lookup.txt','Sources for Doing Lookups and Checkups on People (from 2600)','lookup.txt',_binary '\0','2019-05-01 10:53:45',2,1879,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(125,'maim.nfo','The MAIM Member List and BBS List','maim.nfo',_binary '\0','2019-05-01 10:53:45',2,2447,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(126,'makealco.txt','The Guys Six Feet Under Present Part I of the Getting Homemade Highs File: Alcohol','makealco.txt',_binary '\0','2019-05-01 10:53:45',2,12990,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(127,'makelsd','Making LSD','makelsd',_binary '\0','2019-05-01 10:53:45',2,2056,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(128,'makepois.on','The Ninja Warrior Presents: Poison #1','makepois.on',_binary '\0','2019-05-01 10:53:45',2,6656,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(129,'makestun.txt','The Underground! Present How to make a Stun Gun by the Green Dean, April 5, 1999','makestun.txt',_binary '\0','2019-05-01 10:53:45',2,3840,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(130,'making.afpo.n','AFPO, by Ripper (January 11, 1987)','making.afpo.n',_binary '\0','2019-05-01 10:53:45',2,2010,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(131,'making.tnt','How to make TNT by The Computer Pirates of Utah and Screamer','making.tnt',_binary '\0','2019-05-01 10:53:45',2,2544,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(132,'mallphun.txt','Phun Things To Do at a Mall, by the Road Warrior','mallphun.txt',_binary '\0','2019-05-01 10:53:45',2,4901,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(133,'master.locks','Master Combination Locks (April 29, 1991)','master.locks',_binary '\0','2019-05-01 10:53:45',2,5632,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(134,'minibomb','Making a Gunpowder Minibomb by D_L0K (August 5, 1987)','minibomb',_binary '\0','2019-05-01 10:53:45',2,2306,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(135,'miscan2.txt','Miscellaneous Anarchy (Tennis Ball Cannons)','miscan2.txt',_binary '\0','2019-05-01 10:53:45',2,4728,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(136,'mism5.hac','General Nuclear Reactor Design','mism5.hac',_binary '\0','2019-05-01 10:53:45',2,6272,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(137,'napalm.ana','Very Small Napalm Grenades, by Bountyhunter','napalm.ana',_binary '\0','2019-05-01 10:53:45',2,796,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(138,'napalm.grenade','Napalm Made Easy by Sir Knight','napalm.grenade',_binary '\0','2019-05-01 10:53:45',2,2114,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(139,'newid.txt','How to Create a New Identity by the Walking Glitch','newid.txt',_binary '\0','2019-05-01 10:53:45',2,7312,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(140,'nicotine.ana','Nicotine, from The Poor Man\'s James Bond','nicotine.ana',_binary '\0','2019-05-01 10:53:45',2,2388,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(141,'night.fun','The Anarchist\'s Guide to Nighttime Fun (February 3, 1986)','night.fun',_binary '\0','2019-05-01 10:53:45',2,7729,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(142,'nightvis.txt','Nightvision by Morpheus','nightvis.txt',_binary '\0','2019-05-01 10:53:45',2,5564,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(143,'ninja1.ana','the Ninja Warrior presents Poison #1','ninja1.ana',_binary '\0','2019-05-01 10:53:45',2,6656,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(144,'nitro','How to Make and Use Nitroglycerin, by Karl Marx','nitro',_binary '\0','2019-05-01 10:53:45',2,3313,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(145,'nitro.txt','How to Make and Use Nitroglycerin by Ninja Master','nitro.txt',_binary '\0','2019-05-01 10:53:45',2,3540,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(146,'no.school.1','20 Ways to Sabotage Your School by Cosmic Charlie','no.school.1',_binary '\0','2019-05-01 10:53:45',2,4736,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(147,'no.school.2','School Stoppers VOlume #2 by Cosmic Charlie and The Doctor','no.school.2',_binary '\0','2019-05-01 10:53:45',2,4864,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(148,'nuclear.ana','Nuclear Transit on the Highways','nuclear.ana',_binary '\0','2019-05-01 10:53:45',2,8461,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(149,'nuclear.txt','Nuclear Transit on the Highways: A Primer by Data Line','nuclear.txt',_binary '\0','2019-05-01 10:53:45',2,8529,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(150,'nucreact.txt','Sir Death\'s Laboratory: How to Build a Nuclear Reactor (October 23, 1990)','nucreact.txt',_binary '\0','2019-05-01 10:53:45',2,4912,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(151,'onetimepad.txt','How to use One-Time Pads for Secret Communications','onetimepad.txt',_binary '\0','2019-05-01 10:53:45',2,12047,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(152,'pipe.bomb','How to Make a Really Nice Pipe Bomb Out of Everyday Materials by Gray Mouser','pipe.bomb',_binary '\0','2019-05-01 10:53:45',2,2731,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(153,'piratetv.txt','How To Build Your Own Television Station by Jersey Devil (April 5, 1991)','piratetv.txt',_binary '\0','2019-05-01 10:53:45',2,6223,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(154,'pisecret.msg','How to Get Anything on Anyone from Mike Enlow, Private Detective 1992','pisecret.msg',_binary '\0','2019-05-01 10:53:45',2,110235,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(155,'pocket.flamethr','How to Make a Flamethrower, by The Cracksman','pocket.flamethr',_binary '\0','2019-05-01 10:53:45',2,2796,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(156,'postal.fraud','The Poor Man\'s Guide to Fraud Part 1 by The Prince of Darkness','postal.fraud',_binary '\0','2019-05-01 10:53:45',2,3959,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(157,'powder','Gun Powder by The Moritician','powder',_binary '\0','2019-05-01 10:53:45',2,751,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(158,'pranks','Cat Trax Presents Phuking Pholks With a Fone (June 19, 1985)','pranks',_binary '\0','2019-05-01 10:53:45',2,8388,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(159,'premchan.txt','How to Get Premium Channels by Steve Rochin','premchan.txt',_binary '\0','2019-05-01 10:53:45',2,1806,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(160,'pressure.txt','Manuscript I: Pressure Devices (June 1988)','pressure.txt',_binary '\0','2019-05-01 10:53:45',2,10855,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(161,'psonpens','The Poison Pen','psonpens',_binary '\0','2019-05-01 10:53:45',2,2546,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(162,'pyro.2','Pyro Book II by Captain Hack and Grey Wolf','pyro.2',_binary '\0','2019-05-01 10:53:46',2,3685,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(163,'pyro.delights','Pyrotechnical Delights by Ragner Rocker','pyro.delights',_binary '\0','2019-05-01 10:53:46',2,3200,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(164,'pyro1.txt','Preparation of Contact Explosives','pyro1.txt',_binary '\0','2019-05-01 10:53:46',2,25088,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(165,'pyro2.txt','Touch Paper, Self Igniting Mixtures, Percussion Explosives','pyro2.txt',_binary '\0','2019-05-01 10:53:46',2,32640,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(166,'pyromag.feb','The Spoke Gun by Dark Angel','pyromag.feb',_binary '\0','2019-05-01 10:53:46',2,14906,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(167,'quicky.bomb','A Quickie Bomb Stolen from MacGyver by Shadow Hawk of The J-Men (1986)','quicky.bomb',_binary '\0','2019-05-01 10:53:46',2,1123,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(168,'radar.jamming','Radar Jamming: Motorists Fight Back! Byt eh Scarlet Armadillo','radar.jamming',_binary '\0','2019-05-01 10:53:46',2,7612,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(169,'rcd.ana','Making Radio Controlled Detonators, by The Road Warrior','rcd.ana',_binary '\0','2019-05-01 10:53:46',2,8270,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(170,'rdiocont.txt','How to Make Things Radio Controlled!','rdiocont.txt',_binary '\0','2019-05-01 10:53:46',2,7138,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(171,'revenge','Revenge II by Night Crawler (December 29, 1984)','revenge',_binary '\0','2019-05-01 10:53:46',2,2048,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(172,'revengehtg.txt','All About Revenge, by Surfer Bob','revengehtg.txt',_binary '\0','2019-05-01 10:53:46',2,2946,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(173,'riflemic.ana','Rifle Mikes, by Omnipotent, Inc.','riflemic.ana',_binary '\0','2019-05-01 10:53:46',2,4143,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(174,'roadpizz.ana','The Complete Guide to Road Pizza, by Black Kat','roadpizz.ana',_binary '\0','2019-05-01 10:53:46',2,5547,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(175,'rocket.txt','Home Built Rocket Enginers, by Chris Beauregard','rocket.txt',_binary '\0','2019-05-01 10:53:46',2,20825,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(176,'rockwool.txt','Information on Rockwool','rockwool.txt',_binary '\0','2019-05-01 10:53:46',2,11353,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(177,'rod001.txt','Riders of Death Issue 1','rod001.txt',_binary '\0','2019-05-01 10:53:46',2,2800,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(178,'rod002.txt','Riders of Death Issue 2','rod002.txt',_binary '\0','2019-05-01 10:53:46',2,2085,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(179,'rod003.txt','Riders of Death Issue 3','rod003.txt',_binary '\0','2019-05-01 10:53:46',2,4737,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(180,'rod004.txt','Riders of Death Issue 4 (ANSI)','rod004.txt',_binary '\0','2019-05-01 10:53:46',2,4701,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(181,'san-bug1.txt','Audio Surveillance, by SANctuary','san-bug1.txt',_binary '\0','2019-05-01 10:53:46',2,8611,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(182,'san-bug2.txt','Audio Surveillance Part 2, by SANctuary','san-bug2.txt',_binary '\0','2019-05-01 10:53:46',2,3824,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(183,'san-bug3.txt','Audio Surveillance Part 3, by SANctuary','san-bug3.txt',_binary '\0','2019-05-01 10:53:46',2,6090,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(184,'san-bug4.txt','Audio Surveillance Part 4, by SANctuary','san-bug4.txt',_binary '\0','2019-05-01 10:53:46',2,3363,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(185,'sanlock','The Art of Lockpicking Volume #1 (October 5, 1990)','sanlock',_binary '\0','2019-05-01 10:53:46',2,65402,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(186,'scanning.txt','Samurai Cat Presents Scanning, the Art of Non-Selective Intervention','scanning.txt',_binary '\0','2019-05-01 10:53:46',2,10453,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(187,'school.fun','How to Have Fun at School, by Walkon','school.fun',_binary '\0','2019-05-01 10:53:46',2,14336,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(188,'screwpst.txt','A Nice Way to Screw Over the Post Office','screwpst.txt',_binary '\0','2019-05-01 10:53:46',2,3696,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(189,'secretmeetings.txt','Secret Meetings','secretmeetings.txt',_binary '\0','2019-05-01 10:53:46',2,7671,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(190,'secrets.msg','On-Line Investigations: Investigating by Computer! By Michael E. Enlow','secrets.msg',_binary '\0','2019-05-01 10:53:46',2,30519,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(191,'secrs.gph','Big Secrets Volume #1 by The Wyvern','secrs.gph',_binary '\0','2019-05-01 10:53:46',2,5120,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(192,'secrserv.ana','How to Contact the Secret Service, by Jedi Warrior','secrserv.ana',_binary '\0','2019-05-01 10:53:46',2,2019,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(193,'shopping.center','Shopping Center Antics by The Trooper','shopping.center',_binary '\0','2019-05-01 10:53:46',2,2299,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(194,'shortfil.txt','A Short File on the Central Intelligence Agency','shortfil.txt',_binary '\0','2019-05-01 10:53:46',2,4579,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(195,'sinksub.txt','Sinking a Sub by Hellrazor','sinksub.txt',_binary '\0','2019-05-01 10:53:46',2,2242,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(196,'sirius.1','The Sirius Affairs #1 by Sylex Sirius (February 3, 1986)','sirius.1',_binary '\0','2019-05-01 10:53:46',2,3330,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(197,'sirius.2','The Sirius Affairs #2 by Sylex Sirius (February 3, 1986)','sirius.2',_binary '\0','2019-05-01 10:53:46',2,3252,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(198,'sirius.3','Sirius Affair #3 - Terrorism (February 9-11, 1986)','sirius.3',_binary '\0','2019-05-01 10:53:46',2,3732,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(199,'sirius.4','Sirius Affair #4 - Trashing by Sylex Sirius (February 18, 1986)','sirius.4',_binary '\0','2019-05-01 10:53:46',2,2710,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(200,'sirius.txt','The Sirius Affairs #1 by Sylex Sirius (February 3, 1986)','sirius.txt',_binary '\0','2019-05-01 10:53:46',2,13315,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(201,'skippy.hum','Ask The Expert by D\'Ark Angel','skippy.hum',_binary '\0','2019-05-01 10:53:46',2,5490,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(202,'soil2pnt.txt','Extraction of Potassium Nitrate from Soil','soil2pnt.txt',_binary '\0','2019-05-01 10:53:46',2,3125,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(203,'somethin.ana','A wide selection of KY and IN Radio Frequencies','somethin.ana',_binary '\0','2019-05-01 10:53:46',2,12855,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(204,'sonicjam.txt','Ultra Sonic Jammer Plans by Xanrek the Conjurer','sonicjam.txt',_binary '\0','2019-05-01 10:53:46',2,7821,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(205,'specialw','Special Warfare Manual by Baby Huey and The Titan (1985)','specialw',_binary '\0','2019-05-01 10:53:46',2,10799,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(206,'ssnum.ana','About Social Security Numbers, by Scan Man','ssnum.ana',_binary '\0','2019-05-01 10:53:46',2,1791,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(207,'ssurvival.txt','The Suburban Survival Guide Issue #1 from Incorsis Daethr','ssurvival.txt',_binary '\0','2019-05-01 10:53:46',2,7762,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(208,'stench.txt','Stenches For All by Kurt Saxon, typed by Mach Three','stench.txt',_binary '\0','2019-05-01 10:53:46',2,6764,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(209,'still.ana','The Still by Cobalt-60 and Airborne Ranger','still.ana',_binary '\0','2019-05-01 10:53:46',2,4557,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(210,'still.hac','The Still: An Article from the Poor Man\'s James Bond','still.hac',_binary '\0','2019-05-01 10:53:46',2,5139,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(211,'still.txt','How to Make a Still, by Lex Luthor (needs formatting)','still.txt',_binary '\0','2019-05-01 10:53:46',2,4693,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(212,'stinkbom.txt','How to Make the Smelliest Stink-Bomb of All by the Hitmen, Vito and Vinnie','stinkbom.txt',_binary '\0','2019-05-01 10:53:46',2,2299,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(213,'stinkum','Stinkum: From the Poor Man\'s James Bond by Kurt Saxon by The Penguin','stinkum',_binary '\0','2019-05-01 10:53:46',2,36096,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(214,'streetfi.ana','Survival by The Nimpha','streetfi.ana',_binary '\0','2019-05-01 10:53:46',2,6417,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(215,'stungun.txt','How to Make a Home-Made Stun Gun, from Morbid Angel of MAIM','stungun.txt',_binary '\0','2019-05-01 10:53:47',2,5571,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(216,'sugar.rocket','How to Make Sugar Rockets by Cloaked Warrior','sugar.rocket',_binary '\0','2019-05-01 10:53:47',2,9986,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(217,'sugar.rocket.2','Improving Your Rocket by Cloaked Warrior (1986)','sugar.rocket.2',_binary '\0','2019-05-01 10:53:47',2,6284,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(218,'sun.bomb','Oxygen: Very Flammable, Sammy the God (September 6, 1987)','sun.bomb',_binary '\0','2019-05-01 10:53:47',2,945,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(219,'supermar.fun','Supermarket Fun by 007','supermar.fun',_binary '\0','2019-05-01 10:53:47',2,9856,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(220,'surrvival','WDP UU\'s Survival Files for the 507 College District','surrvival',_binary '\0','2019-05-01 10:53:47',2,2652,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(221,'survaili.ana','Sir Francis Drake Presents Remote Surveilance','survaili.ana',_binary '\0','2019-05-01 10:53:47',2,6623,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(222,'tapcorner.txt','Tap Corner (Various Articles from TAP) Word-Processed by The Mulcher ][','tapcorner.txt',_binary '\0','2019-05-01 10:53:47',2,7671,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(223,'tapfones','Private Audience (*A Basic Lesson in the Art of Listening In*)','tapfones',_binary '\0','2019-05-01 10:53:47',2,11956,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(224,'tbbom15.txt','The Big Book of Mischief by David Richards Version 1.5 (1993)','tbbom15.txt',_binary '\0','2019-05-01 10:53:47',2,195597,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(225,'techter.txt','Some Facts About Oil Pipelines and Technological Terrorism','techter.txt',_binary '\0','2019-05-01 10:53:47',2,1890,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(226,'terrcomp','The Terrorist Home Companion by The Mentor and The Dead Kennedy','terrcomp',_binary '\0','2019-05-01 10:53:47',2,34752,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(227,'terror.hb','How to Make a Blackmatch Fuse','terror.hb',_binary '\0','2019-05-01 10:53:47',2,95508,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(228,'test','The Avenger\'s Handboot by The Last Viking Version 1.20 (1995)','test',_binary '\0','2019-05-01 10:53:47',2,124443,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(229,'therm.txt','Creation of My Favorite, Thermite!','therm.txt',_binary '\0','2019-05-01 10:53:47',2,2038,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(230,'thieving.ups','UPSetting, by Axiom Codex and Lazar','thieving.ups',_binary '\0','2019-05-01 10:53:47',2,4096,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(231,'toliet.bomb','The Toilet Bomb by Angus Young','toliet.bomb',_binary '\0','2019-05-01 10:53:47',2,6113,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(232,'toxins.txt','Information on Plutonium typed in by Mike Pompura, November 1989','toxins.txt',_binary '\0','2019-05-01 10:53:47',2,2776,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(233,'tracing.msg','Tracing Made Simple: Track \'Em Down by Michael E. Enlow, 1993','tracing.msg',_binary '\0','2019-05-01 10:53:47',2,44658,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(234,'trash3.phk','Trashing Techniques by The Underground Alliance','trash3.phk',_binary '\0','2019-05-01 10:53:47',2,3412,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(235,'tripwire.ana','Trip Wires by The Mortician','tripwire.ana',_binary '\0','2019-05-01 10:53:47',2,1854,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(236,'trukpipe.txt','Truck Weaponry by Captain Hack (June 26, 1995)','trukpipe.txt',_binary '\0','2019-05-01 10:53:47',2,1761,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(237,'tv.box','How to Put Together a TV Station','tv.box',_binary '\0','2019-05-01 10:53:47',2,3961,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(238,'tvjam.txt','TV Jammer, by Dr. Rat 1985','tvjam.txt',_binary '\0','2019-05-01 10:53:47',2,7484,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(239,'tvro.txt','How to Monitor Microwave Telecommunication Links with an orfinary TVRO by Thallion of WUFO (1984)','tvro.txt',_binary '\0','2019-05-01 10:53:47',2,18317,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(240,'unlaw1.txt','The Book of the Unlawfuls by Shadowspawn Volume I','unlaw1.txt',_binary '\0','2019-05-01 10:53:47',2,4864,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(241,'unlaw2.txt','Other Unlawfuls by The Hoe Hoper and The Blue Buccaneer','unlaw2.txt',_binary '\0','2019-05-01 10:53:47',2,2048,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(242,'upc.txt','Cracking the Universal Product Code','upc.txt',_binary '\0','2019-05-01 10:53:47',2,6726,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(243,'v23.faq','VIRUS 23 FAQsheet, by Darren Wershler-Henry','v23.faq',_binary '\0','2019-05-01 10:53:47',2,6891,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(244,'vending.machine','Vending Machines Issue #2 by Lord Knight','vending.machine',_binary '\0','2019-05-01 10:53:47',2,4352,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(245,'vomitgas.ana','Vomit Gas, by The Primo Pyro','vomitgas.ana',_binary '\0','2019-05-01 10:53:47',2,2560,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(246,'warehous.e','Tag File for the Warehouse BBS','warehous.e',_binary '\0','2019-05-01 10:53:47',2,1776,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(247,'warfare','How to Make a Landmine, by Marlin and Black Knight','warfare',_binary '\0','2019-05-01 10:53:47',2,2525,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(248,'warrior.txt','Axiom of a Warrior by Doctor Murdock','warrior.txt',_binary '\0','2019-05-01 10:53:47',2,4262,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(249,'weapons.txt','Stealth Systems Product Listings','weapons.txt',_binary '\0','2019-05-01 10:53:47',2,2265,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(250,'weekend.anarchy','Weekend Anarchy: A Documentary, Nay, Manual on the Widely Studied and Practiced Art of Creating Havoc and General Social Disarray','weekend.anarchy',_binary '\0','2019-05-01 10:53:47',2,18441,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(251,'werehere.mad','Parody of Anarchy Files','werehere.mad',_binary '\0','2019-05-01 10:53:47',2,13168,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(252,'wildkill.txt','WildMan\'s Complete Guide to \"Assinations\"','wildkill.txt',_binary '\0','2019-05-01 10:53:47',2,21011,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(253,'wrist.rocket.bo','A Wrist Rocket by Sammy the God (September 5, 1987)','wrist.rocket.bo',_binary '\0','2019-05-01 10:53:47',2,635,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(254,'writers.txt','How to Properly Write an Anarchy File by The Freddy, December 1991','writers.txt',_binary '\0','2019-05-01 10:53:47',2,6514,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(255,'wwiii.ana','WWIII - a fantasy, from Shadow Stories, Inc.','wwiii.ana',_binary '\0','2019-05-01 10:53:47',2,11934,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(256,'zencor.txt','The ZenCor Products and Services Catalog v4.0 (October 1st, 1992)','zencor.txt',_binary '\0','2019-05-01 10:53:47',2,19957,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/',''),(257,'zip.gun','The Ringmaster Gun Part I: How to Build a .22 (March 7, 1986)','zip.gun',_binary '\0','2019-05-01 10:53:47',2,8576,'/home/six/Desktop/6Net/6Net/bin/Debug/GFiles/HPA/Anarchy/','');
/*!40000 ALTER TABLE `GFileDetails` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Graffiti`
--

DROP TABLE IF EXISTS `Graffiti`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `Graffiti` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Content` varchar(80) DEFAULT NULL,
  `UserId` int(11) NOT NULL,
  `Posted` datetime NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `index2` (`UserId`),
  KEY `posted` (`Posted`),
  CONSTRAINT `fk_Graffiti_User` FOREIGN KEY (`UserId`) REFERENCES `Users` (`Id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Graffiti`
--

LOCK TABLES `Graffiti` WRITE;
/*!40000 ALTER TABLE `Graffiti` DISABLE KEYS */;
INSERT INTO `Graffiti` VALUES (1,'No matter how thin you slice it, it\'s still bologna.',5,'2019-04-17 17:05:46'),(2,'disky was here',6,'2019-04-17 17:37:42'),(3,'I wish I remembered MCI codes!',6,'2019-04-18 10:12:51'),(4,'I are horse nick',14,'2019-04-19 10:06:10'),(5,'testing',5,'2019-04-19 11:51:22'),(6,'This is some test text.',5,'2019-04-24 10:31:31');
/*!40000 ALTER TABLE `Graffiti` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `MessageBaseAreas`
--

DROP TABLE IF EXISTS `MessageBaseAreas`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `MessageBaseAreas` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `ParentAreaId` int(11) DEFAULT NULL,
  `Title` varchar(25) CHARACTER SET utf8 DEFAULT NULL,
  `Description` varchar(255) CHARACTER SET utf8 DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `index2` (`ParentAreaId`),
  CONSTRAINT `fk_MessageBaseArea_MessageBaseArea` FOREIGN KEY (`ParentAreaId`) REFERENCES `MessageBaseAreas` (`Id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `MessageBaseAreas`
--

LOCK TABLES `MessageBaseAreas` WRITE;
/*!40000 ALTER TABLE `MessageBaseAreas` DISABLE KEYS */;
/*!40000 ALTER TABLE `MessageBaseAreas` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `MessageBases`
--

DROP TABLE IF EXISTS `MessageBases`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `MessageBases` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Title` varchar(25) CHARACTER SET utf8 DEFAULT NULL,
  `Description` varchar(255) CHARACTER SET utf8 DEFAULT NULL,
  `MessageBaseAreaId` int(11) DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `index2` (`MessageBaseAreaId`),
  CONSTRAINT `fk_MessageBase_MessageBaseArea` FOREIGN KEY (`MessageBaseAreaId`) REFERENCES `MessageBaseAreas` (`Id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `MessageBases`
--

LOCK TABLES `MessageBases` WRITE;
/*!40000 ALTER TABLE `MessageBases` DISABLE KEYS */;
/*!40000 ALTER TABLE `MessageBases` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `MessageBodies`
--

DROP TABLE IF EXISTS `MessageBodies`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `MessageBodies` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `MessageHeaderId` int(11) DEFAULT NULL,
  `Body` text,
  PRIMARY KEY (`Id`),
  KEY `messageheader` (`MessageHeaderId`),
  CONSTRAINT `fk_MessageBodies_1` FOREIGN KEY (`MessageHeaderId`) REFERENCES `MessageHeaders` (`Id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `MessageBodies`
--

LOCK TABLES `MessageBodies` WRITE;
/*!40000 ALTER TABLE `MessageBodies` DISABLE KEYS */;
/*!40000 ALTER TABLE `MessageBodies` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `MessageHeaders`
--

DROP TABLE IF EXISTS `MessageHeaders`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `MessageHeaders` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Subject` varchar(45) CHARACTER SET utf8 DEFAULT NULL,
  `Anonymous` bit(1) DEFAULT NULL,
  `Posted` datetime DEFAULT NULL,
  `MessageThreadId` int(11) DEFAULT NULL,
  `MessageBaseId` int(11) DEFAULT NULL,
  `UserId` int(11) DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `user` (`UserId`),
  KEY `messagebase` (`MessageBaseId`),
  KEY `messagethread` (`MessageThreadId`),
  KEY `posted` (`Posted`),
  CONSTRAINT `fk_MessageHeader_MessageBase` FOREIGN KEY (`MessageBaseId`) REFERENCES `MessageBases` (`Id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `fk_MessageHeader_MessageThread` FOREIGN KEY (`MessageThreadId`) REFERENCES `MessageThreads` (`Id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `fk_MessageHeader_User` FOREIGN KEY (`UserId`) REFERENCES `Users` (`Id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `MessageHeaders`
--

LOCK TABLES `MessageHeaders` WRITE;
/*!40000 ALTER TABLE `MessageHeaders` DISABLE KEYS */;
/*!40000 ALTER TABLE `MessageHeaders` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `MessageThreads`
--

DROP TABLE IF EXISTS `MessageThreads`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `MessageThreads` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `MessageBaseId` int(11) DEFAULT NULL,
  `InitialMessageHeaderId` int(11) DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `messagebase` (`MessageBaseId`),
  KEY `InitialMessageHeader` (`InitialMessageHeaderId`),
  CONSTRAINT `fk_MessageThread_1` FOREIGN KEY (`MessageBaseId`) REFERENCES `MessageBases` (`Id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `fk_MessageThreads_1` FOREIGN KEY (`InitialMessageHeaderId`) REFERENCES `MessageHeaders` (`Id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `MessageThreads`
--

LOCK TABLES `MessageThreads` WRITE;
/*!40000 ALTER TABLE `MessageThreads` DISABLE KEYS */;
/*!40000 ALTER TABLE `MessageThreads` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `NewsItems`
--

DROP TABLE IF EXISTS `NewsItems`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `NewsItems` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Subject` varchar(45) CHARACTER SET utf8 DEFAULT NULL,
  `Body` text,
  `Sent` datetime DEFAULT NULL,
  `UserId` int(11) DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `sent` (`Sent`),
  KEY `user` (`UserId`),
  CONSTRAINT `fk_newsitem_user` FOREIGN KEY (`UserId`) REFERENCES `Users` (`Id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `NewsItems`
--

LOCK TABLES `NewsItems` WRITE;
/*!40000 ALTER TABLE `NewsItems` DISABLE KEYS */;
/*!40000 ALTER TABLE `NewsItems` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `PFileAreaAccessGroups`
--

DROP TABLE IF EXISTS `PFileAreaAccessGroups`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `PFileAreaAccessGroups` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `AccessGroupId` int(11) DEFAULT NULL,
  `PFileAreaId` int(11) DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `accessgroup` (`AccessGroupId`),
  KEY `pfilearea` (`PFileAreaId`),
  CONSTRAINT `fk_PFileAreaAccessGroup_1` FOREIGN KEY (`AccessGroupId`) REFERENCES `AccessGroups` (`Id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `fk_PFileAreaAccessGroup_2` FOREIGN KEY (`PFileAreaId`) REFERENCES `PFileAreas` (`Id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `PFileAreaAccessGroups`
--

LOCK TABLES `PFileAreaAccessGroups` WRITE;
/*!40000 ALTER TABLE `PFileAreaAccessGroups` DISABLE KEYS */;
/*!40000 ALTER TABLE `PFileAreaAccessGroups` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `PFileAreas`
--

DROP TABLE IF EXISTS `PFileAreas`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `PFileAreas` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Title` varchar(45) CHARACTER SET utf8 DEFAULT NULL,
  `Description` varchar(255) CHARACTER SET utf8 DEFAULT NULL,
  `ParentAreaId` int(11) DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `index2` (`ParentAreaId`),
  CONSTRAINT `fk_PFileAreas_1` FOREIGN KEY (`ParentAreaId`) REFERENCES `PFileAreas` (`Id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `PFileAreas`
--

LOCK TABLES `PFileAreas` WRITE;
/*!40000 ALTER TABLE `PFileAreas` DISABLE KEYS */;
INSERT INTO `PFileAreas` VALUES (1,'Games','Classic BBS Door Games',NULL);
/*!40000 ALTER TABLE `PFileAreas` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `PFileDetails`
--

DROP TABLE IF EXISTS `PFileDetails`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `PFileDetails` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Title` varchar(45) CHARACTER SET utf8 NOT NULL,
  `Description` varchar(255) CHARACTER SET utf8 NOT NULL,
  `PFileAreaId` int(11) DEFAULT NULL,
  `Filename` varchar(255) CHARACTER SET utf8 NOT NULL,
  `FilePath` varchar(255) CHARACTER SET utf8 NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `index2` (`PFileAreaId`),
  CONSTRAINT `fk_PFileDetails_1` FOREIGN KEY (`PFileAreaId`) REFERENCES `PFileAreas` (`Id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `PFileDetails`
--

LOCK TABLES `PFileDetails` WRITE;
/*!40000 ALTER TABLE `PFileDetails` DISABLE KEYS */;
INSERT INTO `PFileDetails` VALUES (1,'Empire','Classic BBS Game',1,'Empire.dll','/home/six/Desktop/6Net/6Net/bin/Debug/PFiles/');
/*!40000 ALTER TABLE `PFileDetails` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `UserAccessGroups`
--

DROP TABLE IF EXISTS `UserAccessGroups`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `UserAccessGroups` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `AccessGroupId` int(11) DEFAULT NULL,
  `UserId` int(11) DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `User` (`UserId`),
  KEY `AccessGroup` (`AccessGroupId`),
  CONSTRAINT `fk_AccessGroup_UserAccessGroup` FOREIGN KEY (`AccessGroupId`) REFERENCES `AccessGroups` (`Id`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `fk_User_UserAccessGroup` FOREIGN KEY (`UserId`) REFERENCES `Users` (`Id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `UserAccessGroups`
--

LOCK TABLES `UserAccessGroups` WRITE;
/*!40000 ALTER TABLE `UserAccessGroups` DISABLE KEYS */;
/*!40000 ALTER TABLE `UserAccessGroups` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Users`
--

DROP TABLE IF EXISTS `Users`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `Users` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Username` varchar(25) CHARACTER SET utf8 DEFAULT NULL,
  `HashedPassword` varchar(45) DEFAULT NULL,
  `LastConnection` datetime DEFAULT NULL,
  `LastDisconnection` datetime DEFAULT NULL,
  `LastConnectionIP` varchar(45) CHARACTER SET utf8 DEFAULT NULL,
  `RealName` varchar(50) CHARACTER SET utf8 DEFAULT NULL,
  `ComputerType` varchar(50) CHARACTER SET utf8 DEFAULT NULL,
  `Email` varchar(255) CHARACTER SET utf8 DEFAULT NULL,
  `WebPage` varchar(255) CHARACTER SET utf8 DEFAULT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `Username_UNIQUE` (`Username`)
) ENGINE=InnoDB AUTO_INCREMENT=16 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Users`
--

LOCK TABLES `Users` WRITE;
/*!40000 ALTER TABLE `Users` DISABLE KEYS */;
INSERT INTO `Users` VALUES (5,'Six','trojan','2019-04-17 09:01:48','2019-04-17 09:01:49','localhost','Oliver Clothesoff','C64','oliver@vbssolutions.com','https://commodoresixtyfour.com'),(6,'jcompton','22data33','2019-04-17 17:23:26','2019-04-17 17:23:26','Unknown','Jason','64 Ultimate','jcompton@yahoo.com',''),(7,'jpcompton','jpjpjpjp','2019-04-18 09:59:28','2019-04-18 09:59:28','Unknown','J','u64','jason.compton@gmail.com',''),(8,'jpc3','jpjpjpjp','2019-04-18 10:02:01','2019-04-18 10:02:01','Unknown','cromptoan','u64','jcompton@yahoo.com',''),(9,'jpc4','jpjpjpjp','2019-04-18 17:26:18','2019-04-18 17:26:18','47.35.154.185:49153','JJJCCC','64','jcompton@yahoo.com',''),(10,'Optic Freeze','bandaid1989','2019-04-18 18:01:21','2019-04-18 18:01:21','73.193.62.236:57003','Eric','C64','opticfreeze.gp@gmail.com',''),(13,'The Modem Humper','password','2019-04-19 09:58:45','2019-04-19 09:58:45','127.0.0.1:33538','modem mchumpy','c69','modem@humper.com',''),(14,'horse dick nick','password','2019-04-19 10:05:19','2019-04-19 10:05:19','127.0.0.1:33566','fuck','psis','shit',''),(15,'jpc6','jpjpjpjp','2019-04-19 12:00:30','2019-04-19 12:00:30','47.35.154.185:49153','jpc11111','64','jcompton@yahoo.com','');
/*!40000 ALTER TABLE `Users` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2019-05-20 17:06:07
