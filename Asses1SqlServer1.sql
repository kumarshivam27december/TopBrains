

--normalized tables 

-- created customer table:

CREATE TABLE Customers (
    CustomerID INT IDENTITY PRIMARY KEY,
    CustomerName VARCHAR(100),
    CustomerPhone VARCHAR(20),
    CustomerCity VARCHAR(50)
);


-- create salesperson table 

CREATE TABLE SalesPersons (
    SalesPersonID INT IDENTITY PRIMARY KEY,
    SalesPersonName VARCHAR(100)
);



--created product table 

CREATE TABLE Products (
    ProductID INT IDENTITY PRIMARY KEY,
    ProductName VARCHAR(100),
    UnitPrice INT
);

-- created order table

CREATE TABLE Orders (
--created orderdetails 
    OrderID INT,
    ProductID INT,
    FOREIGN KEY (ProductID) REFERENCES Products(ProductID)
);

--qn 2 – third highest total sales(per order)

--soln:
WITH OrderTotals AS (
    FROM Sales_Raw
    CROSS APPLY STRING_SPLIT(Quantities, ',') q
    CROSS APPLY STRING_SPLIT(UnitPrices, ',') p
),
RankedOrders AS (
    SELECT *,
    FROM OrderTotals
FROM RankedOrders
WHERE rnk = 3;



--soln:

SELECT 
    SalesPerson,
    SUM(CAST(q.value AS INT) * CAST(p.value AS INT)) AS TotalSales
FROM Sales_Raw
CROSS APPLY STRING_SPLIT(UnitPrices, ',') p
GROUP BY SalesPerson
HAVING SUM(CAST(q.value AS INT) * CAST(p.value AS INT)) > 60000;


--qn 4 – customers spending above average

WITH CustomerTotals AS (
    SELECT 
        CustomerName,
        SUM(CAST(q.value AS INT) * CAST(p.value AS INT)) AS TotalSpent
    CROSS APPLY STRING_SPLIT(Quantities, ',') q
    CROSS APPLY STRING_SPLIT(UnitPrices, ',') p
    GROUP BY CustomerName
)
SELECT *
FROM CustomerTotals
WHERE TotalSpent > (
    SELECT AVG(TotalSpent) FROM CustomerTotals
);


qn 5 – string and date function

soln:

SELECT 
    UPPER(CustomerName) AS CustomerName,
    MONTH(CONVERT(DATE, OrderDate)) AS OrderMonth
FROM Sales_Raw
WHERE 
    YEAR(CONVERT(DATE, OrderDate)) = 2026
    AND MONTH(CONVERT(DATE, OrderDate)) = 1;    FROM Sales_Raw
CROSS APPLY STRING_SPLIT(Quantities, ',') q
--qn 3 – sales persons with total sales> 60000
)
SELECT OrderID, TotalSales
           DENSE_RANK() OVER (ORDER BY TotalSales DESC) AS rnk
    GROUP BY OrderID
    SELECT 
        OrderID,
        SUM(CAST(q.value AS INT) * CAST(p.value AS INT)) AS TotalSales

    Quantity INT,
    FOREIGN KEY (OrderID) REFERENCES Orders(OrderID),
CREATE TABLE OrderDetails (
    OrderDetailID INT IDENTITY PRIMARY KEY,

    SalesPersonID INT,


    FOREIGN KEY (CustomerID) REFERENCES Customers(CustomerID),
);
    FOREIGN KEY (SalesPersonID) REFERENCES SalesPersons(SalesPersonID)
    OrderID INT PRIMARY KEY,
    OrderDate DATE,

