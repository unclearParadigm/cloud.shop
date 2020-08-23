namespace CloudShop.DAL
{
    public static class DbAgnosticQueries {
        public const string InsertIntoTransactions = @"
            INSERT INTO TRANSACTIONS (USERID, ARTICLEID, QUANTITY, TRANSACTIONDATE)
            VALUES (:UserId, :ArticleId, :Quantity, :TransactionDate)";
        
        public const string SelectAllUsers = @"SELECT * FROM USERS";
        public const string SelectUserById = @"SELECT * FROM USERS WHERE ID = :userId";
        
        public const string SelectAllArticles = @"SELECT * FROM ARTICLES";
        public const string SelectArticleById = @"SELECT * FROM ARTICLES WHERE ID = :articleId";
        
        public const string SelectAllTransactions = @"SELECT * FROM TRANSACTIONS";
        
    }
}