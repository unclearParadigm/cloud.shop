using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using CloudShop.DAL.Entities;
using CloudShop.DAL.Implementations.Infrastructure;
using CloudShop.DAL.Interfaces;
using Cloudshop.Domain;
using CSharpFunctionalExtensions;
using Dapper;

namespace CloudShop.DAL.Implementations
{
    internal class DbAgnosticDal : ICloudShopDal
    {
        private readonly Func<IDbConnection> _openConnectionFunction;
        private readonly DatabaseConnectionOpener _databaseConnectionOpener;

        internal DbAgnosticDal(DatabaseConnectionOpener databaseConnectionOpener)
        {
            _databaseConnectionOpener = databaseConnectionOpener;
            _openConnectionFunction = databaseConnectionOpener.OpenConnection;
        }

        public Result Migrate()
        {
            try
            {
                var upgradeResult = _databaseConnectionOpener
                    .GetMigrationUpdateEngine()
                    .PerformUpgrade();

                return upgradeResult.Successful ? Result.Success() : Result.Failure(upgradeResult.Error.Message);
            }
            catch (Exception exc)
            {
                return Result.Failure(exc.Message);
            }
        }

        public Result<Maybe<UserDto>> GetUser(long userId)
        {
            using (var connection = _openConnectionFunction())
            {
                try
                {
                    var user = connection
                        .Query<UserEntity>(DbAgnosticQueries.SelectUserById, new {userId})
                        .ToList();

                    return Result.Success(user.Any()
                        ? Maybe<UserDto>.From(user.Single().Map<UserDto>())
                        : Maybe<UserDto>.None);
                }
                catch (Exception exc)
                {
                    return Result.Failure<Maybe<UserDto>>(exc.Message);
                }
            }
        }

        public Result<IList<UserDto>> GetUsers()
        {
            using (var connection = _openConnectionFunction())
            {
                try
                {
                    var users = connection
                        .Query<UserEntity>(DbAgnosticQueries.SelectAllUsers)
                        .ToList();

                    return Result.Success(users.Map<IList<UserDto>>());
                }
                catch (Exception exc)
                {
                    return Result.Failure<IList<UserDto>>(exc.Message);
                }
            }
        }

        public Result<Maybe<ArticleDto>> GetArticle(long articleId)
        {
            using (var connection = _openConnectionFunction())
            {
                try
                {
                    var article = connection
                        .Query<ArticleEntity>(DbAgnosticQueries.SelectArticleById, new {articleId})
                        .ToList();

                    return Result.Success(article.Any()
                        ? Maybe<ArticleDto>.From(article.Single().Map<ArticleDto>())
                        : Maybe<ArticleDto>.None);
                }
                catch (Exception exc)
                {
                    return Result.Failure<Maybe<ArticleDto>>(exc.Message);
                }
            }
        }

        public Result<IList<ArticleDto>> GetArticles()
        {
            using (var connection = _openConnectionFunction())
            {
                try
                {
                    var articles = connection
                        .Query<ArticleEntity>(DbAgnosticQueries.SelectAllArticles)
                        .ToList();

                    return Result.Success(articles.Map<IList<ArticleDto>>());
                }
                catch (Exception exc)
                {
                    return Result.Failure<IList<ArticleDto>>(exc.Message);
                }
            }
        }

        public Result<IList<ArticleDto>> GetActiveArticles()
        {
            return GetArticles()
                .Map(articles => (IList<ArticleDto>) articles.Where(a => a.IsArticleInValidRange).ToList());
        }

        public Result<IList<TransactionDto>> GetTransactions()
        {
            using (var connection = _openConnectionFunction())
            {
                try
                {
                    var transactions = connection
                        .Query<TransactionEntity>(DbAgnosticQueries.SelectAllTransactions)
                        .ToList();

                    return Result.Success(transactions.Map<IList<TransactionDto>>());
                }
                catch (Exception exc)
                {
                    return Result.Failure<IList<TransactionDto>>(exc.Message);
                }
            }
        }

        public Result AddTransaction(TransactionDto transactionDto)
        {
            try
            {
                using (var connection = _openConnectionFunction()) {
                    using (var dbTransaction = connection.BeginTransaction()) {
                        connection.Execute(DbAgnosticQueries.InsertIntoTransactions, new {
                            transactionDto.UserId,
                            transactionDto.ArticleId,
                            transactionDto.Quantity,
                            transactionDto.TransactionDate,
                        });
                        dbTransaction.Commit();
                        return Result.Success();
                    }
                }
            }
            catch(Exception exc)
            {
                return Result.Failure(exc.Message);
            }
        }

        public Result DecreaseStockCount(long articleId, long quantity)
        {
            throw new NotImplementedException();
        }
    }
}