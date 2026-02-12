SELECT *
FROM Products p
WHERE NOT EXISTS (
    SELECT 1
    FROM Sales s
    WHERE s.ProductId = p.ProductId
);
