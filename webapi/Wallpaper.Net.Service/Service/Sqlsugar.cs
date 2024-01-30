using SqlSugar;

public static class Sqlsugar
{
    //创建数据库对象
    public static SqlSugarClient db = new SqlSugarClient(new ConnectionConfig()
    {
        ConnectionString = File.ReadAllText("D:\\db.txt"),
        DbType = DbType.SqlServer,
        IsAutoCloseConnection = true
    });
}