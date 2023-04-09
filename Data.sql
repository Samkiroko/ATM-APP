CREATE TABLE cardholders (
  id INT PRIMARY KEY,
  cardNum VARCHAR(16) NOT NULL,
  pin INT NOT NULL,
  firstName VARCHAR(50) NOT NULL,
  lastName VARCHAR(50) NOT NULL,
  balance DECIMAL(18, 2) NOT NULL
);
--@block
INSERT INTO cardholders (id, cardNum, pin, firstName, lastName, balance)
VALUES (
    1,
    '4532772818527395',
    1234,
    'Samuel',
    'Njenga',
    10000.00
  ),
  (
    2,
    '4532772818527595',
    4567,
    'Joseph',
    'Kimani',
    10000.00
  ),
  (
    3,
    '4532772818527844',
    8011,
    'Luke',
    'Sharma',
    10000.00
  ),
  (
    4,
    '4532772818527145',
    9119,
    'Iren',
    'Puri',
    10000.00
  ),
  (
    5,
    '4532772818527101',
    8055,
    'Debby',
    'Naeku',
    10000.00
  ),
  (
    6,
    '4532772818527001',
    9289,
    'Joy',
    'Lisa',
    10000.00
  );
--@BLOCK
SELECT *
FROM cardholders;