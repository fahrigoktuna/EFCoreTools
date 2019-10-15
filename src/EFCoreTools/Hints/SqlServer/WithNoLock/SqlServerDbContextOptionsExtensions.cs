using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.ExpressionVisitors.Internal;
using Microsoft.EntityFrameworkCore.Query.Sql;
using Microsoft.EntityFrameworkCore.SqlServer.Infrastructure.Internal;
using System;
using System.Collections.Generic;
using System.Text;

namespace EFCoreTools.Hints.SqlServer.WithNoLock
{
    public  static  partial class SqlServerDbContextOptionsExtensions
    {
        public static DbContextOptionsBuilder EnableSqlServerWithNoLock(this DbContextOptionsBuilder optionsBuilder)
        {
            var sqlServerOptionsExtension = optionsBuilder.Options.FindExtension<SqlServerOptionsExtension>();
            if (sqlServerOptionsExtension == null)
                return optionsBuilder;

            optionsBuilder = optionsBuilder
                .ReplaceService<IQuerySqlGeneratorFactory, WithNoLockSqlServerQuerySqlGeneratorFactory>();
            return optionsBuilder.ReplaceService<IMaterializerFactory, WithNoLockMaterializerFactory>();
        }
    }
}
