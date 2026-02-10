SELECT e.Dept, e.Name, e.Salary
FROM Employees e
JOIN (
    SELECT Dept, MAX(Salary) AS MaxSalary
    FROM Employees
    GROUP BY Dept
) m
ON e.Dept = m.Dept
AND e.Salary = m.MaxSalary;
