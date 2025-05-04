
-- Gender Table and Seed

CREATE TABLE Gender (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Code CHAR(1) NOT NULL UNIQUE, -- M, F, N
    Name NVARCHAR(50) NOT NULL
);

INSERT INTO Gender (Code, Name) VALUES
('M', 'Male'),
('F', 'Female'),
('N', 'Non-binary');


-- PartnerType Table and Seed

CREATE TABLE PartnerType (
    Id TINYINT PRIMARY KEY,
    Name NVARCHAR(50) NOT NULL
);

INSERT INTO PartnerType (Id, Name) VALUES
(1, 'Personal'),
(2, 'Legal');


-- Partner Table 
CREATE TABLE Partner (
    Id INT PRIMARY KEY IDENTITY(1,1),
    FirstName NVARCHAR(255) NOT NULL,
    LastName NVARCHAR(255) NOT NULL,
    Address NVARCHAR(255) NULL,
    PartnerNumber CHAR(20) NOT NULL,
    CroatianPIN CHAR(11) NULL,
    PartnerTypeId TINYINT NOT NULL,
    CreatedAtUtc DATETIME NOT NULL DEFAULT GETUTCDATE(),
    CreatedByUser NVARCHAR(255) NOT NULL,
    IsForeign BIT NOT NULL,
    ExternalCode NVARCHAR(20) NOT NULL UNIQUE,
    GenderId INT NOT NULL,
    CONSTRAINT FK_Partner_Gender FOREIGN KEY (GenderId) REFERENCES Gender(Id),
    CONSTRAINT FK_Partner_PartnerType FOREIGN KEY (PartnerTypeId) REFERENCES PartnerType(Id)
);



-- Policy Table 

CREATE TABLE Policy (
    Id INT PRIMARY KEY IDENTITY(1,1),
    PartnerId INT NOT NULL,
    PolicyNumber NVARCHAR(15) NOT NULL,
    PolicyAmount DECIMAL(18,2) NOT NULL,
    CONSTRAINT FK_Policy_Partner FOREIGN KEY (PartnerId) REFERENCES Partner(Id)
);


