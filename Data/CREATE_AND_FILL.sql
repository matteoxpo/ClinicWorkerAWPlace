BEGIN TRANSACTION;
CREATE TABLE IF NOT EXISTS "Clinic" (
	"AddressId"	INTEGER NOT NULL,
	"ContactId"	INTEGER NOT NULL,
	"Id"	INTEGER NOT NULL,
	FOREIGN KEY("ContactId") REFERENCES "Contact"("Id"),
	FOREIGN KEY("AddressId") REFERENCES "Address"("Id"),
	PRIMARY KEY("Id" AUTOINCREMENT)
);
CREATE TABLE IF NOT EXISTS "Cabinet" (
	"Number"	TEXT NOT NULL,
	"Type"	TEXT NOT NULL,
	"ClinicId"	INTEGER NOT NULL,
	"Id"	INTEGER NOT NULL,
	PRIMARY KEY("Id" AUTOINCREMENT),
	FOREIGN KEY("ClinicId") REFERENCES "Clinic"("Id")
);
CREATE TABLE IF NOT EXISTS "HumanUser" (
	"Login"	TEXT NOT NULL,
	"Password"	TEXT NOT NULL,
	"Name"	TEXT NOT NULL,
	"Surname"	TEXT NOT NULL,
	"PatronymicName"	TEXT,
	"AddressId"	INTEGER,
	"DateOfBirth"	DATETIME,
	"Sex"	TEXT NOT NULL,
	"MedicalPolicyId"	INTEGER NOT NULL,
	"Id"	INTEGER NOT NULL,
	PRIMARY KEY("Id" AUTOINCREMENT),
	FOREIGN KEY("MedicalPolicyId") REFERENCES "MedicalPolicy"("Id"),
	FOREIGN KEY("AddressId") REFERENCES "Address"("Id")
);
CREATE TABLE IF NOT EXISTS "Address" (
	"Street"	TEXT NOT NULL,
	"City"	TEXT NOT NULL,
	"State"	TEXT NOT NULL,
	"ZipCode"	TEXT NOT NULL,
	"Country"	TEXT NOT NULL,
	"Id"	INTEGER NOT NULL,
	PRIMARY KEY("Id" AUTOINCREMENT)
);
CREATE TABLE IF NOT EXISTS "MedicalPolicy" (
	"Serial"	TEXT NOT NULL,
	"Number"	TEXT NOT NULL,
	"Id"	INTEGER NOT NULL,
	PRIMARY KEY("Id" AUTOINCREMENT)
);
CREATE TABLE IF NOT EXISTS "HumanUserContact" (
	"ContactId"	INTEGER NOT NULL,
	"HumanUserId"	INTEGER NOT NULL,
	"Id"	INTEGER NOT NULL,
	PRIMARY KEY("Id" AUTOINCREMENT),
	FOREIGN KEY("ContactId") REFERENCES "Contact"("Id"),
	FOREIGN KEY("HumanUserId") REFERENCES "HumanUser"("Id")
);
CREATE TABLE IF NOT EXISTS "Contact" (
	"PhoneNumber"	TEXT,
	"Email"	TEXT,
	"Id"	INTEGER NOT NULL,
	PRIMARY KEY("Id" AUTOINCREMENT)
);
CREATE TABLE IF NOT EXISTS "Education" (
	"Serial"	TEXT NOT NULL,
	"Number"	TEXT NOT NULL,
	"Description"	TEXT NOT NULL,
	"Date"	DATETIME NOT NULL,
	"HumanUserId"	INTEGER NOT NULL,
	"Id"	INTEGER NOT NULL,
	PRIMARY KEY("Id" AUTOINCREMENT),
	FOREIGN KEY("HumanUserId") REFERENCES "HumanUser"("Id")
);
CREATE TABLE IF NOT EXISTS "HumanUserBenefit" (
	"BenefitId"	INTEGER NOT NULL,
	"HumanUserId"	INTEGER NOT NULL,
	"Id"	INTEGER NOT NULL,
	PRIMARY KEY("Id" AUTOINCREMENT),
	FOREIGN KEY("BenefitId") REFERENCES "Benefit"("Id"),
	FOREIGN KEY("HumanUserId") REFERENCES "HumanUser"("Id")
);
CREATE TABLE IF NOT EXISTS "Benefit" (
	"Type"	TEXT NOT NULL,
	"Description"	TEXT NOT NULL,
	"Discount"	REAL NOT NULL,
	"RetirementAge"	INTEGER NOT NULL,
	"Id"	INTEGER NOT NULL,
	PRIMARY KEY("Id" AUTOINCREMENT)
);
CREATE TABLE IF NOT EXISTS "EmployeeUser" (
	"SalaryPerHour"	REAL NOT NULL,
	"JobTittleId"	INTEGER NOT NULL,
	"HumanUserId"	INTEGER NOT NULL,
	"Id"	INTEGER NOT NULL,
	PRIMARY KEY("Id" AUTOINCREMENT),
	FOREIGN KEY("JobTittleId") REFERENCES "JobTittle"("Id"),
	FOREIGN KEY("HumanUserId") REFERENCES "HumanUser"("Id")
);
CREATE TABLE IF NOT EXISTS "Appointment" (
	"ClinicId"	INTEGER NOT NULL,
	"EmployeeUserId"	INTEGER NOT NULL,
	"HumanUserId"	INTEGER NOT NULL,
	"Date"	DATETIME,
	"Id"	INTEGER NOT NULL,
	PRIMARY KEY("Id" AUTOINCREMENT),
	FOREIGN KEY("HumanUserId") REFERENCES "HumanUser"("Id"),
	FOREIGN KEY("EmployeeUserId") REFERENCES "EmployeeUser"("Id"),
	FOREIGN KEY("ClinicId") REFERENCES "Clinic"("Id")
);
CREATE TABLE IF NOT EXISTS "TreatmentCourse" (
	"HumanUserId"	INTEGER,
	"Description"	TEXT,
	"Id"	INTEGER,
	FOREIGN KEY("HumanUserId") REFERENCES "HumanUser"("Id"),
	PRIMARY KEY("Id" AUTOINCREMENT)
);
CREATE TABLE IF NOT EXISTS "TreatmentStage" (
	"EmployeeUserId"	INTEGER NOT NULL,
	"Description"	TEXT,
	"DiseaseId"	INTEGER NOT NULL,
	"TreatmentCourseId"	INTEGER NOT NULL,
	"Id"	INTEGER NOT NULL,
	FOREIGN KEY("DiseaseId") REFERENCES "Disease"("Id"),
	FOREIGN KEY("TreatmentCourseId") REFERENCES "TreatmentCourse"("Id"),
	PRIMARY KEY("Id" AUTOINCREMENT)
);
CREATE TABLE IF NOT EXISTS "DiseaseTransmission" (
	"TransmissionType"	TEXT NOT NULL,
	"Id"	INTEGER NOT NULL,
	PRIMARY KEY("Id" AUTOINCREMENT)
);
CREATE TABLE IF NOT EXISTS "TreatmentStageReferralForAnalysis" (
	"TreatmentStageId"	INTEGER NOT NULL,
	"ReferralForAnalysisId"	INTEGER NOT NULL,
	"Id"	INTEGER NOT NULL,
	FOREIGN KEY("ReferralForAnalysisId") REFERENCES "ReferralForAnalysis"("Id"),
	FOREIGN KEY("TreatmentStageId") REFERENCES "TreatmentStage"("Id"),
	PRIMARY KEY("Id" AUTOINCREMENT)
);
CREATE TABLE IF NOT EXISTS "ReferralForAnalysis" (
	"HumanUserId"	INTEGER NOT NULL,
	"EmployeeUserId"	INTEGER,
	"Date"	DATE NOT NULL,
	"AnalysisId"	INTEGER NOT NULL,
	"Description"	TEXT,
	"Result"	TEXT,
	"Id"	INTEGER NOT NULL,
	FOREIGN KEY("EmployeeUserId") REFERENCES "EmployeeUser"("Id"),
	FOREIGN KEY("HumanUserId") REFERENCES "HumanUser"("Id"),
	FOREIGN KEY("AnalysisId") REFERENCES "Analysis"("Id"),
	PRIMARY KEY("Id" AUTOINCREMENT)
);
CREATE TABLE IF NOT EXISTS "Analysis" (
	"Type"	TEXT NOT NULL,
	"Description"	TEXT NOT NULL,
	"Id"	INTEGER NOT NULL,
	PRIMARY KEY("Id" AUTOINCREMENT)
);
CREATE TABLE IF NOT EXISTS "TreatmentStageDrug" (
	"TreatmentStageId"	INTEGER NOT NULL,
	"DrugId"	INTEGER NOT NULL,
	"Id"	INTEGER NOT NULL,
	FOREIGN KEY("TreatmentStageId") REFERENCES "TreatmentStage"("Id"),
	FOREIGN KEY("DrugId") REFERENCES "Drug"("Id"),
	PRIMARY KEY("Id" AUTOINCREMENT)
);
CREATE TABLE IF NOT EXISTS "Drug" (
	"Name"	TEXT NOT NULL,
	"Description"	TEXT NOT NULL,
	"Recipe"	BOOLEAN NOT NULL,
	"Id"	INTEGER NOT NULL,
	PRIMARY KEY("Id" AUTOINCREMENT)
);
CREATE TABLE IF NOT EXISTS "Disease" (
	"Name"	TEXT NOT NULL,
	"Description"	TEXT NOT NULL,
	"TransmissionId"	int,
	"Id"	INTEGER NOT NULL,
	FOREIGN KEY("TransmissionId") REFERENCES "DiseaseTransmission"("Id"),
	PRIMARY KEY("Id" AUTOINCREMENT)
);
CREATE TABLE IF NOT EXISTS "JobTittle" (
	"Name"	TEXT NOT NULL,
	"Description"	TEXT NOT NULL,
	"Id"	INTEGER NOT NULL,
	PRIMARY KEY("Id" AUTOINCREMENT)
);
INSERT INTO "Clinic" VALUES (1,1,1);
INSERT INTO "Clinic" VALUES (2,2,2);
INSERT INTO "Clinic" VALUES (3,3,3);
INSERT INTO "HumanUser" VALUES ('user1','password1','John','Doe','Middle',1,'1990-01-01','Male',1,1);
INSERT INTO "HumanUser" VALUES ('user2','password2','Jane','Smith',NULL,2,'1985-05-05','Female',2,2);
INSERT INTO "HumanUser" VALUES ('user3','password3','Alice','Johnson','Ann',3,'1980-10-10','Female',3,3);
INSERT INTO "Address" VALUES ('123 Main St','CityA','StateA','12345','CountryA',1);
INSERT INTO "Address" VALUES ('456 Elm St','CityB','StateB','67890','CountryB',2);
INSERT INTO "Address" VALUES ('789 Oak St','CityC','StateC','13579','CountryC',3);
INSERT INTO "Address" VALUES ('123 Main St','CityA','StateA','12345','CountryA',4);
INSERT INTO "Address" VALUES ('456 Elm St','CityB','StateB','67890','CountryB',5);
INSERT INTO "Address" VALUES ('789 Oak St','CityC','StateC','13579','CountryC',6);
INSERT INTO "MedicalPolicy" VALUES ('ABCD','123456',1);
INSERT INTO "MedicalPolicy" VALUES ('EFGH','789012',2);
INSERT INTO "MedicalPolicy" VALUES ('IJKL','345678',3);
INSERT INTO "MedicalPolicy" VALUES ('ABCD','123456',4);
INSERT INTO "MedicalPolicy" VALUES ('EFGH','789012',5);
INSERT INTO "MedicalPolicy" VALUES ('IJKL','345678',6);
INSERT INTO "HumanUserContact" VALUES (1,1,1);
INSERT INTO "HumanUserContact" VALUES (2,2,2);
INSERT INTO "HumanUserContact" VALUES (3,3,3);
INSERT INTO "Contact" VALUES ('123-456-7890','email1@example.com',1);
INSERT INTO "Contact" VALUES ('987-654-3210','email2@example.com',2);
INSERT INTO "Contact" VALUES ('111-222-3333','email3@example.com',3);
INSERT INTO "Contact" VALUES ('123-456-7890','email1@example.com',4);
INSERT INTO "Contact" VALUES ('987-654-3210','email2@example.com',5);
INSERT INTO "Contact" VALUES ('111-222-3333','email3@example.com',6);
INSERT INTO "Education" VALUES ('ABC123','456DEF','Medical degree','2010-05-15',1,1);
INSERT INTO "Education" VALUES ('GHI789','012JKL','Nursing diploma','2015-08-20',2,2);
INSERT INTO "Education" VALUES ('MNO345','678PQR','Pharmacy course','2020-03-10',3,3);
INSERT INTO "HumanUserBenefit" VALUES (1,1,1);
INSERT INTO "HumanUserBenefit" VALUES (2,2,2);
INSERT INTO "HumanUserBenefit" VALUES (3,3,3);
INSERT INTO "Benefit" VALUES ('TypeA','DescriptionA',0.1,65,1);
INSERT INTO "Benefit" VALUES ('TypeB','DescriptionB',0.15,70,2);
INSERT INTO "Benefit" VALUES ('TypeC','DescriptionC',0.2,75,3);
INSERT INTO "Benefit" VALUES ('TypeA','DescriptionA',0.1,65,4);
INSERT INTO "Benefit" VALUES ('TypeB','DescriptionB',0.15,70,5);
INSERT INTO "Benefit" VALUES ('TypeC','DescriptionC',0.2,75,6);
INSERT INTO "EmployeeUser" VALUES (50.0,1,1,1);
INSERT INTO "EmployeeUser" VALUES (30.0,2,2,2);
INSERT INTO "EmployeeUser" VALUES (25.0,3,3,3);
INSERT INTO "Appointment" VALUES (1,1,1,'2023-01-15',1);
INSERT INTO "Appointment" VALUES (2,2,2,'2023-02-20',2);
INSERT INTO "Appointment" VALUES (3,3,3,'2023-03-25',3);
INSERT INTO "TreatmentCourse" VALUES (1,'Course for flu treatment',1);
INSERT INTO "TreatmentCourse" VALUES (2,'Liver treatment course',2);
INSERT INTO "TreatmentCourse" VALUES (3,'Malaria treatment',3);
INSERT INTO "TreatmentStage" VALUES (1,'Stage 1 for flu',1,1,1);
INSERT INTO "TreatmentStage" VALUES (2,'Stage 2 for hepatitis',2,2,2);
INSERT INTO "TreatmentStage" VALUES (3,'Stage 3 for malaria',3,3,3);
INSERT INTO "DiseaseTransmission" VALUES ('Airborne',1);
INSERT INTO "DiseaseTransmission" VALUES ('Waterborne',2);
INSERT INTO "DiseaseTransmission" VALUES ('Vector-borne',3);
INSERT INTO "TreatmentStageReferralForAnalysis" VALUES (1,1,1);
INSERT INTO "TreatmentStageReferralForAnalysis" VALUES (2,2,2);
INSERT INTO "TreatmentStageReferralForAnalysis" VALUES (3,3,3);
INSERT INTO "ReferralForAnalysis" VALUES (1,1,'2023-01-20',1,'Blood test for flu','Normal',1);
INSERT INTO "ReferralForAnalysis" VALUES (2,2,'2023-02-25',2,'Liver function test','Abnormal',2);
INSERT INTO "ReferralForAnalysis" VALUES (3,3,'2023-03-30',3,'Malaria parasite test','Positive',3);
INSERT INTO "Analysis" VALUES ('Blood Test','Complete Blood Count',1);
INSERT INTO "Analysis" VALUES ('MRI Scan','Magnetic Resonance Imaging',2);
INSERT INTO "Analysis" VALUES ('X-Ray','Bone X-Ray',3);
INSERT INTO "TreatmentStageDrug" VALUES (1,1,1);
INSERT INTO "TreatmentStageDrug" VALUES (2,2,2);
INSERT INTO "TreatmentStageDrug" VALUES (3,3,3);
INSERT INTO "Drug" VALUES ('Aspirin','Pain reliever',0,1);
INSERT INTO "Drug" VALUES ('Amoxicillin','Antibiotic',1,2);
INSERT INTO "Drug" VALUES ('Lisinopril','Blood pressure medication',1,3);
INSERT INTO "Disease" VALUES ('Flu','Respiratory infection',1,1);
INSERT INTO "Disease" VALUES ('Hepatitis','Liver inflammation',2,2);
INSERT INTO "Disease" VALUES ('Malaria','Mosquito-borne disease',3,3);
INSERT INTO "JobTittle" VALUES ('Doctor','Medical Doctor',1);
INSERT INTO "JobTittle" VALUES ('Nurse','Medical Nurse',2);
INSERT INTO "JobTittle" VALUES ('Administrator','Administrative Staff',3);
COMMIT;
