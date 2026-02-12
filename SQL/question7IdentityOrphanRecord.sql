SELECT *
FROM OrderItems oi
WHERE NOT EXISTS (
    SELECT 1
    FROM Orders o
    WHERE o.OrderId = oi.OrderId
);
