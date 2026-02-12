SELECT d.DeptName,
       e.Name AS EmployeeName,
       e.Salary AS HighestSalary
FROM Employees e
JOIN Department d 
    ON e.DeptId = d.DeptId
WHERE e.Salary = (
        SELECT MAX(e2.Salary)
        FROM Employees e2
        WHERE e2.DeptId = e.DeptId
)
AND e.DeptId IN (
        SELECT DeptId
        FROM Employees
        GROUP BY DeptId
        HAVING AVG(Salary) > 70000
);
