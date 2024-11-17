DROP TABLE Transactions;
DROP TABLE DailySummaries;

CREATE TABLE Transactions (
    Id UUID PRIMARY KEY,
    Amount NUMERIC(18, 2) NOT NULL,
    TransactionType VARCHAR(50) NOT NULL,
    Date DATE NOT NULL,
    TransactionDate TIMESTAMP DEFAULT CURRENT_TIMESTAMP
    CONSTRAINT FK_DailySummaries FOREIGN KEY (Date) REFERENCES DailySummaries (Date)
);

CREATE TABLE DailySummaries (
    Date DATE PRIMARY KEY,
    TotalBalance NUMERIC(18, 2) NOT NULL
);

INSERT INTO DailySummaries (Date, TotalBalance) VALUES ('2024-11-12', 1000.00);