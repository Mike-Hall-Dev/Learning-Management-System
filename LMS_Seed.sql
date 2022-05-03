CREATE TABLE Student (
Id UNIQUEIDENTIFIER DEFAULT NEWID() PRIMARY KEY,
FirstName varchar(255) not NULL,
MiddleInitial char(1),
LastName varchar(255) not NULL,
Email varchar(255)
);

CREATE TABLE Teacher (
Id UNIQUEIDENTIFIER DEFAULT NEWID() PRIMARY KEY,
FirstName varchar(255) not NULL,
MiddleInitial char(1),
LastName varchar(255) not NULL,
Email varchar(255)
);

CREATE TABLE Course (
Id UNIQUEIDENTIFIER DEFAULT NEWID() PRIMARY KEY,
Name varchar(255) not NULL,
Subject varchar(255),
TeacherId UNIQUEIDENTIFIER FOREIGN KEY REFERENCES Teacher(Id) ON DELETE SET NULL,
StartTime Datetime,
EndTime Datetime,
Room varchar(255)
);

CREATE TABLE Enrollment (
Id UNIQUEIDENTIFIER DEFAULT NEWID() PRIMARY KEY,
StudentId UNIQUEIDENTIFIER FOREIGN KEY REFERENCES Student(Id) ON DELETE CASCADE,
CourseId UNIQUEIDENTIFIER FOREIGN KEY REFERENCES Course(Id)  ON DELETE CASCADE,
Active bit
);


INSERT INTO Student (Id, FirstName, MiddleInitial, LastName, Email) VALUES ('b6cd6777-102b-40f7-855f-b70c09063129','Ricky','L','Smith','ricky.smith@lms.edu');
INSERT INTO Student (Id, FirstName, MiddleInitial, LastName, Email) VALUES ('9c7b9726-70e0-4e82-a385-be6b7578e99a','Ralph','N','Jacobs','ralph.jacobs@lms.edu');
INSERT INTO Student (Id, FirstName, MiddleInitial, LastName, Email) VALUES ('29827b59-a687-4e4b-978d-c7f77248b631','Bob','H','Johnson','bob.johnson@lms.edu');
INSERT INTO Student (Id, FirstName, MiddleInitial, LastName, Email) VALUES ('25484bfd-280e-4d7e-b724-5642aba6e125','Erlich','M','Bachman','erlich.bachman@lms.edu');
INSERT INTO Student (Id, FirstName, MiddleInitial, LastName, Email) VALUES ('79c9fa91-ca05-4bd0-b705-ecd33c4d2ec4','Nelson','G','Bighetti','nelson.bighetti@lms.edu');
INSERT INTO Student (Id, FirstName, MiddleInitial, LastName, Email) VALUES ('b16b02f4-3347-486d-b769-32254b389434','Jared','A','Dunn','jared.dunn@lms.edu');

INSERT INTO Teacher (Id, FirstName, MiddleInitial, LastName, Email) VALUES ('0a1c08eb-b1f1-4f1b-9a8a-97765496bbfb','Stacey','E','Jenkins','stacey.jenkins@lms.edu');
INSERT INTO Teacher (Id, FirstName, MiddleInitial, LastName, Email) VALUES ('8c6151fb-0c50-4e7b-bdbe-019c614f363f','Laurie','A','Juno','laurie.juno@lms.edu');
INSERT INTO Teacher (Id, FirstName, MiddleInitial, LastName, Email) VALUES ('09621889-cbaa-449a-bf05-8a0da6c8427f','Gavin','K','Belson','gavin.belson@lms.edu');
INSERT INTO Teacher (Id, FirstName, MiddleInitial, LastName, Email) VALUES ('82b11594-4095-4612-a617-801d3bed982d','Jack','M','Barker','jack.barker@lms.edu');
INSERT INTO Teacher (Id, FirstName, MiddleInitial, LastName, Email) VALUES ('16104a2a-63c9-402b-9d87-0b475776fc48','Donnie','T','Jobe','donnie.jobe@lms.edu');

INSERT INTO Course(Id, [Name], Subject, TeacherId, StartTime, EndTime, Room) VALUES ('327910ca-7c82-4f7a-95fc-798cfc40b81c','Algebra', 'Math', '0a1c08eb-b1f1-4f1b-9a8a-97765496bbfb','20220105 10:30 AM', '20220515 11:30 AM', '100');
INSERT INTO Course(Id, [Name], Subject, TeacherId, StartTime, EndTime, Room) VALUES ('5a4cf4ed-6a3c-4320-8f1b-db0eb9f61a45','Chemistry','Science', '09621889-cbaa-449a-bf05-8a0da6c8427f', '20220105 9:30 AM', '20220515 10:30 AM', '200');
INSERT INTO Course(Id, [Name], Subject, TeacherId, StartTime, EndTime, Room) VALUES ('8aaeddf1-83b0-4272-928c-22ffebdcc296','Ancient Civilizations','History', '82b11594-4095-4612-a617-801d3bed982d', '20220105 1:30 PM', '20220515 2:30 PM', '300');
INSERT INTO Course(Id, [Name], Subject, TeacherId, StartTime, EndTime, Room) VALUES ('69cec799-a332-4d20-9ddc-ee85eab22f44','Geometry','Math', '0a1c08eb-b1f1-4f1b-9a8a-97765496bbfb', '20220105 11:45 AM', '20220515 12:30 PM', '100');
INSERT INTO Course(Id, [Name], Subject, TeacherId, StartTime, EndTime, Room) VALUES ('3aabc714-419f-41eb-9954-c6a5cd3b83f7','Graphic Design','Art', '8c6151fb-0c50-4e7b-bdbe-019c614f363f', '20220105 12:30 PM', '20220515 1:30 PM', '400');
INSERT INTO Course(Id, [Name], Subject, TeacherId, StartTime, EndTime, Room) VALUES ('172866ee-d381-4414-bfe9-df9cdb7123b2','Creative Writing','English', '16104a2a-63c9-402b-9d87-0b475776fc48', '20220105 8:30 AM', '20220515 9:30 AM', '500');

INSERT INTO Enrollment(Id, StudentId, CourseId, Active) VALUES ('effb5f83-25aa-4ff7-b3b3-148dbb3191bf','b6cd6777-102b-40f7-855f-b70c09063129', '327910ca-7c82-4f7a-95fc-798cfc40b81c', 1);
INSERT INTO Enrollment(Id, StudentId, CourseId, Active) VALUES ('4632e138-1567-4bd6-99d5-4f8b44b0b6d4','b6cd6777-102b-40f7-855f-b70c09063129', '5a4cf4ed-6a3c-4320-8f1b-db0eb9f61a45', 1);
INSERT INTO Enrollment(Id, StudentId, CourseId, Active) VALUES ('0b1fb265-54e7-4e41-bd53-02c51f3450b0','b6cd6777-102b-40f7-855f-b70c09063129', '8aaeddf1-83b0-4272-928c-22ffebdcc296', 0);
INSERT INTO Enrollment(Id, StudentId, CourseId, Active) VALUES ('ffee49ef-561b-4d69-813f-fbd020994393','b6cd6777-102b-40f7-855f-b70c09063129', '69cec799-a332-4d20-9ddc-ee85eab22f44', 0);
INSERT INTO Enrollment(Id, StudentId, CourseId, Active) VALUES ('8e28ec77-f8d3-4847-b1a2-4d1375a6324f','b6cd6777-102b-40f7-855f-b70c09063129', '3aabc714-419f-41eb-9954-c6a5cd3b83f7', 0);
INSERT INTO Enrollment(Id, StudentId, CourseId, Active) VALUES ('baf1b813-1f4a-4f99-bfd9-6df42d9908fd','9c7b9726-70e0-4e82-a385-be6b7578e99a', '69cec799-a332-4d20-9ddc-ee85eab22f44', 1);
INSERT INTO Enrollment(Id, StudentId, CourseId, Active) VALUES ('a220f16c-4381-4ee3-80f6-5b804f085583','9c7b9726-70e0-4e82-a385-be6b7578e99a', '3aabc714-419f-41eb-9954-c6a5cd3b83f7', 1);
INSERT INTO Enrollment(Id, StudentId, CourseId, Active) VALUES ('ce83ffec-0968-4526-9827-34b1bf1df79f','29827b59-a687-4e4b-978d-c7f77248b631', '172866ee-d381-4414-bfe9-df9cdb7123b2', 1);
INSERT INTO Enrollment(Id, StudentId, CourseId, Active) VALUES ('2d01f351-878d-4de4-84b9-c268d9488fae','29827b59-a687-4e4b-978d-c7f77248b631', '5a4cf4ed-6a3c-4320-8f1b-db0eb9f61a45', 1);
INSERT INTO Enrollment(Id, StudentId, CourseId, Active) VALUES ('e3d34463-e60c-4bfc-8c6d-5b93c41e82f6','25484bfd-280e-4d7e-b724-5642aba6e125', '8aaeddf1-83b0-4272-928c-22ffebdcc296', 1);
INSERT INTO Enrollment(Id, StudentId, CourseId, Active) VALUES ('6faac40c-9009-46bd-80a0-eca1cfd4f660','25484bfd-280e-4d7e-b724-5642aba6e125', '172866ee-d381-4414-bfe9-df9cdb7123b2', 1);
INSERT INTO Enrollment(Id, StudentId, CourseId, Active) VALUES ('00ff9978-200d-4a42-970d-9aa02e857ad1','25484bfd-280e-4d7e-b724-5642aba6e125', '5a4cf4ed-6a3c-4320-8f1b-db0eb9f61a45', 0);
INSERT INTO Enrollment(Id, StudentId, CourseId, Active) VALUES ('1a16a438-e268-4877-b003-ffa4b65ee1ed','79c9fa91-ca05-4bd0-b705-ecd33c4d2ec4', '327910ca-7c82-4f7a-95fc-798cfc40b81c', 1);
INSERT INTO Enrollment(Id, StudentId, CourseId, Active) VALUES ('afba908e-f102-46ec-bf87-2ac7e2c4520c','79c9fa91-ca05-4bd0-b705-ecd33c4d2ec4', '69cec799-a332-4d20-9ddc-ee85eab22f44', 0);
INSERT INTO Enrollment(Id, StudentId, CourseId, Active) VALUES ('5c1ee7f4-470c-44c4-a465-9823c43b0956','79c9fa91-ca05-4bd0-b705-ecd33c4d2ec4', '3aabc714-419f-41eb-9954-c6a5cd3b83f7', 0);
INSERT INTO Enrollment(Id, StudentId, CourseId, Active) VALUES ('e8fc2b90-2ad9-4b22-b853-97056f494340','b16b02f4-3347-486d-b769-32254b389434', '5a4cf4ed-6a3c-4320-8f1b-db0eb9f61a45', 1);
INSERT INTO Enrollment(Id, StudentId, CourseId, Active) VALUES ('b556bdfa-8ec1-48a1-ba44-6af1170d4252','b16b02f4-3347-486d-b769-32254b389434', '172866ee-d381-4414-bfe9-df9cdb7123b2', 1);
