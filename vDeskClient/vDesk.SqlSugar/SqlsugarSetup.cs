using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SqlSugar;

namespace vDesk.SqlSugar;

public static class SqlsugarSetup
{
    public static void AddSqlsugarSetup(this IServiceCollection services, IConfiguration configuration,
    string dbName = "SqlLiteDbName")
    {
        dbName = configuration.GetSection(dbName).Value;
        dbName = Path.Combine(Environment.CurrentDirectory, $"{dbName}.db");
        string connStr = new SqliteConnectionStringBuilder()
        {
            DataSource = dbName,
            Mode = SqliteOpenMode.ReadWriteCreate,
            //Password = "zht@vDesk"
        }.ToString();

        SqlSugarScope sqlSugar = new SqlSugarScope(new ConnectionConfig()
        {
            DbType = DbType.Sqlite,
            ConnectionString = connStr,
            IsAutoCloseConnection = true,
        },
            db =>
            {
                //单例参数配置，所有上下文生效       
                db.Aop.OnLogExecuting = (sql, pars) =>
                {
                    Console.WriteLine(sql);//输出sql
                };

                //技巧：拿到非ORM注入对象
                //services.GetService<注入对象>();
            });

        services.AddSingleton<ISqlSugarClient>(sqlSugar);//这边是SqlSugarScope用AddSingleton
    }
}
