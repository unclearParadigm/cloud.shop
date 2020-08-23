CREATE TABLE USERS (
    ID                          INTEGER     NOT NULL    PRIMARY KEY,
    FIRSTNAME                   VARCHAR     NOT NULL,
    LASTNAME                    VARCHAR     NOT NULL,
    EMAIL                       VARCHAR     NOT NULL,
    HASPURCHASEPERMISSIONS      INTEGER     NOT NULL,
    WANTSTORECEIVEBILLINGMAILS  INTEGER     NOT NULL
);

CREATE TABLE ARTICLES (
    ID                          INTEGER     NOT NULL    PRIMARY KEY,
    ARTICLENAME                 VARCHAR     NOT NULL,
    MANUFACTURER                VARCHAR     NOT NULL,
    
    ARTICLEWEIGHT               FLOAT       NOT NULL,
    ARTICLEPRICE                FLOAT       NOT NULL,
    ARTICLEIMAGE                VARCHAR     NOT NULL,
    
    KILOJOULE                   FLOAT       NOT NULL,
    KILOCALORIES                FLOAT       NOT NULL,
    
    VALIDFROM                   DATETIME    NOT NULL,
    VALIDUNTIL                  DATETIME    NOT NULL
);

CREATE TABLE TRANSACTIONS (
    ID                          INTEGER     NOT NULL    PRIMARY KEY,
    USERID                      INTEGER     NOT NULL,
    ARTICLEID                   INTEGER     NOT NULL,
    QUANTITY                    INTEGER     NOT NULL,
    TRANSACTIONDATE             DATETIME    NOT NULL,
    
    FOREIGN KEY (USERID) REFERENCES USERS (id),
    FOREIGN KEY (ARTICLEID) REFERENCES ARTICLES (id)
);

CREATE TABLE ARTICLEQUANTITIES (
    ID                          INTEGER     NOT NULL    PRIMARY KEY,
    ARTICLEID                   INTEGER     NOT NULL,
    AVAILABLEQUANTITY           INTEGER     NOT NULL
);