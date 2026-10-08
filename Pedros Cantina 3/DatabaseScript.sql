create Table MonthlyPlan
(
    planid INT IDENTITY(1,1) PRIMARY KEY,
    month INT NOT NULL,
    year INT NOT NULL,
    createddate DATETIME NOT NULL,
    status VARCHAR(50) NOT NULL
)

create Table role
(
    roleid int identity(1,1) primary key,
    name varchar (100) not null,
    isleader bit not null default 0
)

create Table workloadreport
(
    reportid int primary key,
    numberofshifts varchar(255) not null,
    numberofhours varchar(255) not null,
    [period] DECIMAL(10,2)
    )

create Table employee
(
    employeeid INT IDENTITY(1,1) PRIMARY KEY,
    name VARCHAR(255) NOT NULL,
    phone VARCHAR(20) NOT NULL,
    email VARCHAR(255) NOT NULL UNIQUE,
    active BIT NOT NULL DEFAULT 1,
    roleid INT,
    FOREIGN KEY (roleid) REFERENCES role(roleid) ON DELETE SET NULL
)

create Table shift
(
    shiftid INT IDENTITY(1,1) PRIMARY KEY,
    date DATE NOT NULL,
    starttime TIME NOT NULL,
    endtime TIME NOT NULL,
    status VARCHAR(50) NOT NULL,
    planid INT NOT NULL,
    FOREIGN KEY (planid) REFERENCES MonthlyPlan(planid) ON DELETE CASCADE
)

create Table shiftassignment
(
    assignmentid INT IDENTITY(1,1) PRIMARY KEY,
    isleader BIT NOT NULL DEFAULT 0,
    status VARCHAR(50) NOT NULL,
    shiftid INT NOT NULL,
    employeeid INT NOT NULL,
    FOREIGN KEY (shiftid) REFERENCES shift(shiftid) ON DELETE CASCADE,
    FOREIGN KEY (employeeid) REFERENCES employee(employeeid) ON DELETE CASCADE
)

create Table availability
(
    availabilityId INT IDENTITY(1,1) PRIMARY KEY,
    date DATE NOT NULL,
    fromtime TIME NOT NULL,
    totime TIME NOT NULL,
    available BIT NOT NULL DEFAULT 1,
    employeeid INT NOT NULL,
    FOREIGN KEY (employeeid) REFERENCES employee(employeeid) ON DELETE CASCADE
)

create Table contact
(
    contactid INT IDENTITY(1,1) PRIMARY KEY,
    timestamp DATETIME NOT NULL,
    message VARCHAR(MAX) NOT NULL,
    responsestatus VARCHAR(50) NOT NULL,
    senderEmployeeId INT NOT NULL,
    targetEmployeeId INT NOT NULL,
    FOREIGN KEY (senderEmployeeId) REFERENCES employee(employeeid),
    FOREIGN KEY (targetEmployeeId) REFERENCES employee(employeeid)
)

    INSERT INTO role (name, isleader)
VALUES ('Manager', 1),
       ('Tjener', 0),
       ('Kok', 0),
       ('Bartender', 0),
       ('Front of House', 0);

INSERT INTO role (name, isleader)
VALUES ('Manager', 1),
       ('Tjener', 0),
       ('Kok', 0),
       ('Bartender', 0),
       ('Front of House', 0);

INSERT INTO role (name, isleader) VALUES ('Leder', 1);
INSERT INTO role (name, isleader) VALUES ('Medarbejder', 0);


INSERT INTO employee (name, phone, email, active, roleid)
VALUES
    ('Mads Andersen', '67894351', 'Mads@example.com', 1, (SELECT TOP 1 roleid FROM role WHERE name = 'Leder')),
    ('Sofie Jensen', '87654321', 'sofie@example.com', 1, (SELECT TOP 1 roleid FROM role WHERE name = 'Leder')),
    ('Lars Møller', '22334455', 'lars@example.com', 1, (SELECT TOP 1 roleid FROM role WHERE name = 'Medarbejder')),
    ('Mette Poulsen', '66778899', 'mette@example.com', 1, (SELECT TOP 1 roleid FROM role WHERE name = 'Medarbejder'));

INSERT INTO MonthlyPlan (month, year, createddate, status)
VALUES
    (10, 2026, GETDATE(), 'Aktiv'),
    (11, 2026, GETDATE(), 'Udkast');

INSERT INTO shift (date, starttime, endtime, status, planid)
VALUES
    ('2026-10-05', '08:00:00', '16:00:00', 'Aktiv', '1'),
    ('2026-10-05', '16:00:00', '23:00:00', 'Aktiv','2'),
    ('2026-10-06', '10:00:00', '18:00:00', 'Planlagt','1');

INSERT INTO contact (senderEmployeeId, targetEmployeeId, timestamp, message, responsestatus)
VALUES
    ((SELECT TOP 1 employeeid FROM employee WHERE name LIKE 'Mads%'),
     (SELECT TOP 1 employeeid FROM employee WHERE name LIKE 'Sofie%'),
     GETDATE(), 'Kan du tage min vagt?', 'Afventer'),

    ((SELECT TOP 1 employeeid FROM employee WHERE name LIKE 'Sofie%'),
     (SELECT TOP 1 employeeid FROM employee WHERE name LIKE 'Mads%'),
     GETDATE(), 'Ja, det kan jeg godt!', 'Godkendt');

INSERT INTO shiftassignment (isleader, status, shiftid, employeeid)
VALUES
    (1, 'Godkendt',
     (SELECT TOP 1 shiftid FROM shift WHERE date = '2026-10-05'),
    (SELECT TOP 1 employeeid FROM employee WHERE name LIKE 'Mads%')),

    (0, 'Planlagt',
     (SELECT TOP 1 shiftid FROM shift WHERE date = '2026-10-05'),
     (SELECT TOP 1 employeeid FROM employee WHERE name LIKE 'Sofie%')),

    (0, 'Afventer',
     (SELECT TOP 1 shiftid FROM shift WHERE date = '2026-10-06'),
     (SELECT TOP 1 employeeid FROM employee WHERE name LIKE 'Lars%'));

INSERT INTO availability (date, fromtime, totime, available, employeeid)
VALUES
    ('2026-10-10', '09:00:00', '14:00:00', 1,
     (SELECT TOP 1 employeeid FROM employee WHERE name LIKE 'Mads%')),

    ('2026-10-12', '09:00:00', '14:00:00', 0,
     (SELECT TOP 1 employeeid FROM employee WHERE name LIKE 'Sofie%')),

    ('2026-10-15', '09:00:00', '19:00:00', 1,
     (SELECT TOP 1 employeeid FROM employee WHERE name LIKE 'Lars%'));


INSERT INTO workloadreport (reportid, numberofshifts, numberofhours, [period])
VALUES
    (1, '12', '96.0', 10.2026),
    (2, '15', '120.5', 10.2026),
    (3, '8', '64.0', 10.2026);

