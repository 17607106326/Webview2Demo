using SqlSugar;

namespace vDesk.Model;

[SugarTable("Weather")]
public class Weather
{
    /// <summary>
    /// 自增主键id
    /// </summary>
    [SugarColumn(IsPrimaryKey = true, IsIdentity = true, ColumnName = "id")]//数据库是自增才配自增 
    public int Id { get; set; }

    /// <summary>
    /// 数据创建时间
    /// </summary>
    [SugarColumn(ColumnName = "create_time")]//数据库与实体不一样设置列名 
    public DateTime CreateTime { get; set; }
}
