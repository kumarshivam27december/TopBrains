-- 1
DESC course; -- (SQL Server: sp_help course)

-- 2
SELECT zipcode, city, state AS State FROM zipcode;

-- 3
:r C:\RDBMS\Day2\zipquery.sql  -- (SQLCMD mode)

-- 4
SELECT DISTINCT state FROM zipcode;

-- 5
SELECT student_id, first_name + ' ' + last_name AS Name FROM student;

-- 6
SELECT zipcode + ', ' + city + ', ' + state AS Address FROM zipcode;

-- 7
SELECT description FROM course;

-- 8
SELECT description, cost FROM course;

-- 9
SELECT * FROM course_info;

-- 10
SELECT last_name, zip FROM instructor;

-- 11
SELECT DISTINCT zip FROM student;

-- 12
SELECT first_name, last_name FROM student;

-- 13
SELECT city, state, zipcode FROM zipcode;


--select using where
-- 1
SELECT student_id, section_id, numeric_grade
FROM grade
WHERE grade_code_occurrence = 'FI';

-- 2
SELECT zipcode, city FROM zipcode WHERE state = 'MI';

-- 3
SELECT student_id, first_name + ' ' + last_name AS Name
FROM student
WHERE MONTH(enroll_date) = 1 AND YEAR(enroll_date) = 1999
ORDER BY Name ASC;

-- 4
SELECT section_id, instructor_id
FROM section
WHERE course_no IN (10,20)
ORDER BY instructor_id ASC;

-- 5
SELECT student_id, section_id, numeric_grade
FROM grade
ORDER BY section_id ASC, numeric_grade DESC;

-- 6
SELECT course_no, description, cost
FROM course
WHERE description LIKE '%Intro%';

-- 7
SELECT * FROM course
WHERE description LIKE '%a__';

-- 8
SELECT first_name, last_name
FROM student
WHERE student_id BETWEEN 300 AND 350;

-- 9
SELECT * FROM course
WHERE cost BETWEEN 4000 AND 7000;

-- 10
SELECT first_name FROM instructor WHERE last_name = 'Schumer';

-- 11
SELECT first_name FROM instructor WHERE last_name <> 'Schumer';

-- 12
SELECT description, cost FROM course WHERE cost > 4000;

-- 13
SELECT description, cost FROM course WHERE cost BETWEEN 3000 AND 7000;

-- 14
SELECT description, cost FROM course WHERE cost IN (4000,4500);

-- 15
SELECT first_name, address FROM student WHERE last_name LIKE 'S%';

-- 16
SELECT first_name, address FROM student WHERE last_name LIKE '_o%';

-- 17
SELECT first_name FROM instructor WHERE last_name NOT LIKE 'S%';

-- 18
SELECT description, cost FROM course
WHERE cost BETWEEN 4000 AND 4500 AND description LIKE 'I%';

-- 19
SELECT description, cost, prerequisite
FROM course
WHERE (cost = 2000 AND prerequisite = 20)
   OR prerequisite = 25;

-- 20
SELECT description, cost, prerequisite
FROM course
WHERE cost = 2000
   OR prerequisite IN (20,25);

-- 21
SELECT * FROM course_info WHERE prerequisite IS NULL;

-- 22
SELECT last_name FROM student
WHERE zip IN (10048,11102,11209);

-- 23
SELECT first_name, last_name
FROM instructor
WHERE LOWER(last_name) LIKE '%i%'
AND zip = 10025;

-- 24
SELECT description
FROM course
WHERE prerequisite IS NOT NULL
AND cost < 1100;

-- 25
SELECT DISTINCT cost
FROM course
WHERE prerequisite IS NULL;

-- 26
SELECT course_no, description
FROM course
WHERE prerequisite IS NULL
ORDER BY description ASC;

-- 27
SELECT course_no, description
FROM course
WHERE prerequisite IS NULL
ORDER BY description DESC;

-- 28
SELECT city
FROM zipcode
WHERE state IN ('NY','CT')
ORDER BY zipcode ASC;

-- 29
SELECT zip, first_name, last_name
FROM student
WHERE last_name = 'Graham'
ORDER BY zip DESC, first_name ASC;

-- 30
SELECT city + ', ' + state AS Location FROM zipcode;

-- 31
SELECT UPPER(LEFT(first_name,1)) + LOWER(SUBSTRING(first_name,2,LEN(first_name)))
FROM student;

-- 32
SELECT first_name + ', ' + last_name AS InstructorName
FROM instructor;

-- 33
SELECT cost,
       cost+10 AS Add10,
       cost-20 AS Sub20,
       cost*30 AS Mul30,
       cost/5 AS Div5
FROM course;

-- 34
SELECT DISTINCT numeric_grade,
       FLOOR(numeric_grade/2) AS HalfGrade
FROM grade;


--To_Char/ decode / is null
-- 1 (SQL Server)
SELECT FORMAT(cost,'N0')
FROM course
WHERE course_no < 25;

-- 2
SELECT FORMAT(cost,'C2')
FROM course;

-- 3
SELECT description,
       ISNULL(CAST(prerequisite AS VARCHAR),'Not Applicable') AS Prerequisite
FROM course;

-- 4
SELECT city,
CASE
    WHEN state='NY' THEN 'New York'
    WHEN state='NJ' THEN 'New Jersey'
    ELSE 'Others'
END AS StateFull
FROM zipcode;

