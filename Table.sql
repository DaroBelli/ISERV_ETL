CREATE TABLE EducationalInstitutions
(
    Id INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_Employees PRIMARY KEY,
    Name NVARCHAR(100),
    Country NVARCHAR(100),
	WebSites NVARCHAR(MAX)
)
