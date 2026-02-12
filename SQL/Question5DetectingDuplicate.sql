SELECT Email, COUNT(*) AS EmailCount
FROM Users
GROUP BY Email
HAVING COUNT(*) > 1;
