-- MySQL dump 10.13  Distrib 8.0.39, for Win64 (x86_64)
--
-- Host: localhost    Database: db_infocare_project
-- ------------------------------------------------------
-- Server version	8.0.39

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `tb_adminlogin`
--

DROP TABLE IF EXISTS `tb_adminlogin`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tb_adminlogin` (
  `ID` int NOT NULL AUTO_INCREMENT,
  `A_Username` varchar(255) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `A_Password` varchar(255) COLLATE utf8mb4_general_ci DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tb_adminlogin`
--

LOCK TABLES `tb_adminlogin` WRITE;
/*!40000 ALTER TABLE `tb_adminlogin` DISABLE KEYS */;
INSERT INTO `tb_adminlogin` VALUES (1,'admin','61646d696e313233');
/*!40000 ALTER TABLE `tb_adminlogin` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tb_appointmenthistory`
--

DROP TABLE IF EXISTS `tb_appointmenthistory`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tb_appointmenthistory` (
  `id` int NOT NULL AUTO_INCREMENT,
  `ah_Patient_Name` varchar(255) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `ah_Specialization` varchar(255) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `ah_Doctor_Name` varchar(255) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `ah_time` varchar(255) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `ah_date` date DEFAULT NULL,
  `ah_consfee` decimal(10,2) DEFAULT NULL,
  `ah_status` varchar(255) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `P_bdate` varchar(255) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `P_height` double DEFAULT NULL,
  `P_weight` double DEFAULT NULL,
  `P_bmi` double DEFAULT NULL,
  `P_Blood_type` varchar(255) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `P_precondition` varchar(255) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `P_treatment` varchar(255) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `P_prevsurgery` varchar(255) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `P_alergy` varchar(255) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `P_medication` varchar(255) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `d_diagnosis` text COLLATE utf8mb4_general_ci,
  `d_additionalnotes` text COLLATE utf8mb4_general_ci,
  `d_doctoroder` text COLLATE utf8mb4_general_ci,
  `d_prescription` text COLLATE utf8mb4_general_ci,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=61 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tb_appointmenthistory`
--

LOCK TABLES `tb_appointmenthistory` WRITE;
/*!40000 ALTER TABLE `tb_appointmenthistory` DISABLE KEYS */;
INSERT INTO `tb_appointmenthistory` VALUES (37,'Johnson, Alex','Dentist','Dr. Morant, Fernando','11:00','2024-12-25',5000.00,'InvoiceChecked',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL),(38,'Tuba, JohnMiichael','Dentist','Dr. Morant, Fernando','09:00','2024-12-20',5000.00,'InvoiceChecked','2005-11-07 00:00:00',1.67,46,16.49,'A+','Asthma','Inhaler',NULL,NULL,'Marijuana','asd','asd','asd','asd'),(39,'awd asd, asd asd','Dentist','Dr. Morant, Fernando','08:00','2024-12-27',5000.00,'InvoiceChecked','2024-11-26 00:00:00',1.23,123,81.3,'A','sadsd sad','asdsa da','sadwa sad','asda sd','sadwaas','dsffffffffffff','dsaaaaaaaaaaaaa','sssssssssssss','dddddddddddddd'),(40,'Tuba, JohnMiichael','Optician','Dr. Morant, Fernando','11:00','2024-12-20',5000.00,'InvoiceChecked','2005-11-07 00:00:00',1.67,46,16.49,'A+','Asthma','Inhaler',NULL,NULL,'Marijuana','asd','asd','asd','asd'),(41,'ASDASD, WDASD','Dentist','Dr. Morant, Fernando','10:00','2024-12-13',5000.00,'InvoiceChecked','2024-07-12 00:00:00',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'zxczczs','xczc','czxczxczx','xcz'),(42,'Tuba, JohnMiichael','Optician','Dr. Morant, Fernando','10:00','2024-12-20',5000.00,'InvoiceChecked','2005-11-07 00:00:00',1.67,46,16.49,'A+','Asthma','Inhaler',NULL,NULL,'Marijuana','asd','asd','asd','asd'),(43,'Brown, Lisa','Dentist','Dr. Morant, Fernando','10:00','2024-12-27',5000.00,'InvoiceChecked','1988-03-10 00:00:00',5.7,150,26,'AB-','Asthma','Inhaler','None','Dust','Albuterol','asdaweqe','q',NULL,NULL),(44,'awd asd, asd asd','Optician','Dr. Morant, Fernando','09:00','2024-12-20',5000.00,'InvoiceChecked','2024-11-26 00:00:00',1.23,123,81.3,'A','sadsd sad','asdsa da','sadwa sad','asda sd','sadwaas','dsffffffffffff','dsaaaaaaaaaaaaa','sssssssssssss','dddddddddddddd'),(45,'Tuba, JohnMiichael','Dentist','Dr. Morant, Fernando','09:00','2024-12-27',5000.00,'InvoiceChecked','2005-11-07 00:00:00',1.67,46,16.49,'A+','Asthma','Inhaler',NULL,NULL,'Marijuana','asd','asd','asd','asd'),(46,'Tuba, JohnMiichael','Dentist','Dr. Morant, Fernando','09:00','2024-12-25',5000.00,'InvoiceChecked','2005-11-07 00:00:00',1.67,46,16.49,'A+','Asthma','Inhaler',NULL,NULL,'Marijuana','asd','asd','asd','asd'),(47,'Tuba, JohnMiichael','Dentist','Dr. Morant, Fernando','10:00','2024-12-25',5000.00,'InvoiceChecked','2005-11-07 00:00:00',1.67,46,16.49,'A+','Asthma','Inhaler',NULL,NULL,'Marijuana','asd','asd','asd','asd'),(48,'Tuba, JohnMiichael','Dentist','Dr. Morant, Fernando','09:00','2024-12-20',5000.00,'InvoiceChecked','2005-11-07 00:00:00',1.67,46,16.49,'A+','Asthma','Inhaler',NULL,NULL,'Marijuana','asd','asd','asd','asd'),(49,'Tuba, JohnMiichael','Dentist','Dr. Morant, Fernando','09:00','2024-12-13',5000.00,'InvoiceChecked','2005-11-07 00:00:00',1.67,46,16.49,'A+','Asthma','Inhaler',NULL,NULL,'Marijuana','asd','sd','asd','asd'),(50,'sad, SAD','Dentist','Dr. Morant, Fernando','09:00','2024-12-13',5000.00,'InvoiceChecked','2024-12-04 00:00:00',234,2312,23,'B+','wer','ewee','sdger','aw','asd','jsbccbsc','sccscsc','csccs','cscscsc'),(52,'Garrote, Mark','General Doctor','Dr. Alcaide, Krisha','10:00','2025-03-11',1200.00,'InvoiceChecked','2024-12-07 00:00:00',1.67,70,25.1,'A+','n/a','n/a','n/a','n/a','n/a','mahilig mag bigay ng tres',NULL,'mag comply sa revisions','REFRESH!!'),(53,'Teniggra, Antonio','General Doctor','Dr. Mamayson, Ferkeem','10:00','2025-03-05',1400.00,'InvoiceChecked','1753-03-07 00:00:00',2,80,20,'A+','negro','gluta','anti rabies','araw','sabon','maitim','malibag','maligo','sabon'),(54,'Garrote, Mark','General','Dr. Morant, Fernando','08:00','2024-12-16',5000.00,'InvoiceChecked','2024-12-07 00:00:00',1.67,70,25.1,'A+','n/a','n/a','n/a','n/a','n/a',NULL,NULL,NULL,NULL),(55,'Tuba, JohnMiichael','General','Dr. Morant, Fernando','09:00','2024-12-12',5000.00,'InvoiceChecked','2005-11-07 00:00:00',1.67,46,16.49,'A+','Asthma','Inhaler',NULL,NULL,'Marijuana','Jscsjcscbsbbc','cscsc','scsccs','cscscsc'),(56,'Johnson, Alex','General','Dr. Morant, Fernando','09:00','2024-12-12',5000.00,'InvoiceChecked','1992-09-23 00:00:00',6,180,25,'B+','Diabetes','Insulin','Knee Surgery','Peanuts','Metformin','axbabax','xaxa','xxax','axaxax'),(57,'Teniggra, Antonio','General','Dr. Morant, Fernando','09:00','2024-12-20',5000.00,'InvoiceChecked','1753-03-07 00:00:00',2,80,20,'A+','negro','gluta','anti rabies','araw','sabon','sjcschsuch','scscsc','csscs','csccs'),(58,'awd asd, asd asd','General','Dr. Morant, Fernando','09:00','2024-12-16',5000.00,'InvoiceChecked','2024-11-26 00:00:00',1.23,123,81.3,'A','sadsd sad','asdsa da','sadwa sad','asda sd','sadwaas','jsjchjchsjhcjc','cscscsc','sscscs','csccsc'),(59,'Tuba, JohnMiichael','Optician','Dr. Morant, Fernando','09:00','2024-12-14',5000.00,'InvoiceChecked','2005-11-07 00:00:00',1.67,46,16.49,'A+','Asthma','Inhaler',NULL,NULL,'Marijuana','asassa','xaxxaa','axxa','xaxavc'),(60,'awd asd, asd asd','General','Dr. Morant, Fernando','09:00','2024-12-16',5000.00,'Completed','2024-11-26 00:00:00',1.23,123,81.3,'A','sadsd sad','asdsa da','sadwa sad','asda sd','sadwaas','scscsc','scscs','scscs','csccsc');
/*!40000 ALTER TABLE `tb_appointmenthistory` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tb_doctor_specializations`
--

DROP TABLE IF EXISTS `tb_doctor_specializations`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tb_doctor_specializations` (
  `id` int NOT NULL AUTO_INCREMENT,
  `doctor_id` int NOT NULL,
  `specialization` varchar(255) COLLATE utf8mb4_general_ci NOT NULL,
  PRIMARY KEY (`id`),
  KEY `doctor_id` (`doctor_id`),
  CONSTRAINT `tb_doctor_specializations_ibfk_1` FOREIGN KEY (`doctor_id`) REFERENCES `tb_doctorinfo` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=25 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tb_doctor_specializations`
--

LOCK TABLES `tb_doctor_specializations` WRITE;
/*!40000 ALTER TABLE `tb_doctor_specializations` DISABLE KEYS */;
INSERT INTO `tb_doctor_specializations` VALUES (1,46,'Dentist'),(2,46,'Optician'),(3,46,'General'),(4,46,'Dentist'),(5,46,'Optician'),(6,46,'General'),(7,47,'Dentist'),(8,47,'General'),(9,47,'Radiology'),(10,47,'Dentist'),(11,47,'General'),(12,47,'Radiology'),(13,48,'General'),(14,48,'General'),(15,49,'Ortopedic'),(16,49,'Dentist'),(17,49,'Ortopedic'),(18,49,'Dentist'),(19,50,'Specialist1'),(20,50,'Specialist1'),(21,51,'General Doctor'),(22,51,'General Doctor'),(23,52,'General doctor'),(24,52,'General doctor');
/*!40000 ALTER TABLE `tb_doctor_specializations` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tb_doctorinfo`
--

DROP TABLE IF EXISTS `tb_doctorinfo`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tb_doctorinfo` (
  `id` int NOT NULL AUTO_INCREMENT,
  `firstname` varchar(255) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `middlename` varchar(255) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `lastname` varchar(255) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `username` varchar(255) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `password` varchar(255) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `day_availability` varchar(255) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `start_time` time DEFAULT NULL,
  `end_time` time DEFAULT NULL,
  `specialization` varchar(255) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `consultationfee` decimal(10,2) DEFAULT NULL,
  `contactNumber` varchar(255) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `email` varchar(255) COLLATE utf8mb4_general_ci DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=53 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tb_doctorinfo`
--

LOCK TABLES `tb_doctorinfo` WRITE;
/*!40000 ALTER TABLE `tb_doctorinfo` DISABLE KEYS */;
INSERT INTO `tb_doctorinfo` VALUES (46,'Fernando','Keempy','Morant','fernando','123','Monday-Wednesday-Friday','08:00:00','12:00:00','Dentist, Optician, General',5000.00,NULL,NULL),(47,'Bob','Marley','Sponge','bob','123','Tuesday-Thursday-Saturday','12:00:00','16:00:00','Dentist, General, Radiology',5000.00,NULL,NULL),(48,'Paul','Boy','Nyakol','pol','123','Tuesday-Thursday-Saturday','12:00:00','16:00:00','General',2500.00,NULL,NULL),(49,'asd','asda','wewa','Kupaos','Kupal@1234','Monday-Wednesday-Friday','12:00:00','16:00:00','Ortopedic, Dentist',699.00,'09121234567',NULL),(50,'asdas','asdsad','sdasd','asd','Ahsfhsyfh@2','Monday-Wednesday-Friday','08:00:00','12:00:00','Specialist1',2333.00,'09938275894',NULL),(51,'Krisha','','Alcaide','krisha','Krisha1!','Monday-Wednesday-Friday','08:00:00','12:00:00','General Doctor',1200.00,'09999999999',NULL),(52,'Ferkeem','Flores','Mamayson','ferks','Ferkeem!123','Monday-Wednesday-Friday','08:00:00','12:00:00','General doctor',1400.00,'09999999999',NULL);
/*!40000 ALTER TABLE `tb_doctorinfo` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tb_patientinfo`
--

DROP TABLE IF EXISTS `tb_patientinfo`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tb_patientinfo` (
  `id` int NOT NULL AUTO_INCREMENT,
  `P_Firstname` varchar(255) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `P_Lastname` varchar(255) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `P_Middlename` varchar(255) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `P_Suffix` varchar(255) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `P_username` varchar(255) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `P_Password` varchar(255) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `P_ContactNumber` varchar(255) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `P_Bdate` varchar(255) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `P_Sex` varchar(255) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `P_Height` double DEFAULT NULL,
  `P_Weight` double DEFAULT NULL,
  `P_BMI` double DEFAULT NULL,
  `P_Blood_Type` varchar(255) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `P_Precondition` varchar(255) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `P_Treatment` varchar(255) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `P_PrevSurgery` varchar(255) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `P_address` text COLLATE utf8mb4_general_ci,
  `P_Alergy` varchar(255) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `P_Medication` varchar(255) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `Eme_Firstname` varchar(255) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `Eme_Lastname` varchar(255) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `Eme_Middlename` varchar(255) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `Eme_Suffix` varchar(255) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `Eme_Address` varchar(255) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `email` varchar(255) COLLATE utf8mb4_general_ci DEFAULT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `P_username` (`P_username`)
) ENGINE=InnoDB AUTO_INCREMENT=72 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tb_patientinfo`
--

LOCK TABLES `tb_patientinfo` WRITE;
/*!40000 ALTER TABLE `tb_patientinfo` DISABLE KEYS */;
INSERT INTO `tb_patientinfo` VALUES (40,'Lisa','Brown','Marie','jr','lisabrown',NULL,'09578895953','12-12-2024','Female',5.7,200,200,'AB-','Asthma','Inhaler','None','0,323213, 1231231212,              asasdawdasda street street street street street street street street street street street street street street, Brgy.  Brgy.  Brgy.  Brgy. Brgy.  Brgy.  Brgy.  Brgy.  Brgy.  Brgy.  Brgy.  Brgy.  Brgy.  Brgy. wdawda,              sdasdwa','Dust','Albuterol','Tom','Orange','Martha',NULL,'12,9, 10,  street, Brgy. Brgy. Brgy. Brgy. Brgy. 171, Caloocan','jhonwellespaanola@gmail.com'),(42,'SAD','sad','SAW',NULL,'VASD',NULL,'123','04-12-2024','Female',234,2312,2312,'B+','wer','ewee','sdger','0,123123, 1232,  123 street street, Brgy.  Brgy. ssd,  wqas','aw','asd','Tom','Brown','Martha',NULL,'12,9, 10, manggahan street, Brgy. 171, Caloocan',NULL),(45,'JAKOLSKSK','ASJDJWj','SADW','as','HOSP-11',NULL,'123123','13-11-2024','Female',123,223,21312,'B+','wasd','awd','asd','0,332, 2132, 23 street, Brgy. asda, asdaswww','sadas','asd','Tom','Brown','Martha',NULL,'12,9, 10, manggahan street, Brgy. 171, Caloocan',NULL),(48,'asdas','dasdsad','sdasd','wdadasd','weqeqweq',NULL,'34234241','20-11-2024','Female',NULL,NULL,NULL,NULL,NULL,NULL,NULL,'0,323213, 1231231212, asasdawdasda street, Brgy. wdawda, sdasdwa',NULL,NULL,'Tom','Brown','Martha',NULL,'12,9, 10, manggahan street, Brgy. 171, Caloocan',NULL),(49,'WDASD','ASDASD','SDAW','AWE','ASDAWASD',NULL,'123232','12-07-2024','Male',NULL,NULL,NULL,NULL,NULL,NULL,NULL,'0,123, 123, 123 street, Brgy. ASDW, AWDSA',NULL,NULL,'Tom','Brown','Martha',NULL,'12,9, 10, manggahan street, Brgy. 171, Caloocan',NULL),(50,'adw','adsaw','asd','a','adsfawe',NULL,'3412313','29-10-2024','Male',NULL,NULL,NULL,NULL,NULL,NULL,NULL,'0,123, 232, 123 street, Brgy. asdw, asddsfa',NULL,NULL,'Tom','Brown','Martha',NULL,'12,9, 10, manggahan street, Brgy. 171, Caloocan',NULL),(57,'JohnMiichael','Tuba','Surio','Jr','jmtuba69@gmail.com',NULL,'9815745098','07-11-2005','Male',1.67,46,16.49,'A+','Asthma','Inhaler',NULL,'0,0, 0, Gladys street, Brgy. 176, Caloocan City',NULL,'Marijuana','Tom','Brown','Martha',NULL,'12,9, 10, manggahan street, Brgy. 171, Caloocan',NULL),(60,'asd asd','awd asd','sad as','n/a','asdsadasdawdasdsagsdg',NULL,'9877654566','26-11-2024','Male',1.23,123,81.3,'A','sadsd sad',' asdsa da ','sadwa sad','0,0, 0, ad street, Brgy. aw, sad','asda sd','sadwaas ','Tom','Brown','Martha',NULL,'12,9, 10, manggahan street, Brgy. 171, Caloocan',NULL),(63,'Mark','Garrote','basta','n/a','markaaa',NULL,'9111111111','07-12-2024','Male',1.67,70,25.1,'A+','n/a','n/a','n/a','0,0, 0, ucc street, Brgy. ucc, caloocan','n/a','n/a','Tom','Brown','Martha',NULL,'12,9, 10, manggahan street, Brgy. 171, Caloocan',NULL),(69,'Antonio','Teniggra','Opena','Jr','pakyu@gmail.com',NULL,'9999999999','07-03-1753','Male',2,80,20,'A+','negro','gluta','anti rabies','0,0, 0, Masipag street, Brgy. 178, Caloocan City','araw','sabon','Tom','Brown','Martha',NULL,'12,9, 10, manggahan street, Brgy. 171, Caloocan',NULL);
/*!40000 ALTER TABLE `tb_patientinfo` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tb_staffinfo`
--

DROP TABLE IF EXISTS `tb_staffinfo`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tb_staffinfo` (
  `id` int NOT NULL AUTO_INCREMENT,
  `s_Firstname` varchar(255) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `s_middlename` varchar(255) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `s_Lastname` varchar(255) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `username` varchar(255) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `s_Password` varchar(255) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `s_Suffix` varchar(255) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `s_contactNumber` varchar(255) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `email` varchar(255) COLLATE utf8mb4_general_ci DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=12 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tb_staffinfo`
--

LOCK TABLES `tb_staffinfo` WRITE;
/*!40000 ALTER TABLE `tb_staffinfo` DISABLE KEYS */;
INSERT INTO `tb_staffinfo` VALUES (2,'Jessica','Macapagal','Sojo','jes','123',NULL,NULL,NULL),(5,'qwewqeqweq','eqweqweq','wqeqweq','eqweqweq','@!#1qWdqwdqw',NULL,'09123123123','weqweqweqw'),(6,'asdasd asd','asd asd','asd  sad','GHJT','Ajshfudh!2',NULL,'09878967890','asdsad'),(7,'Staff','staff','StaffLast','staff','Staffnga1!',NULL,'09999999999','ddkfldfsdfls'),(8,'staff','P','staff','staffnga','Staff123!',NULL,'09999999999','krisha@gmail.com'),(9,'John Michael','Surio','Tuba','jeyem','Jm!12345',NULL,'09815123111','qqqq'),(10,'Jhonwell','Agas','Espanola','Ryuzaki','313233404173686c69654b696d','N/A','09514406062','jhonwellespanola@gmail.com'),(11,'Light','snncncsc','Yagami','Light','7265616e6e654d6f72616e323240','','09262637263','jhonwellespanola4@gmail.com');
/*!40000 ALTER TABLE `tb_staffinfo` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping routines for database 'db_infocare_project'
--
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2024-12-14 23:25:04
