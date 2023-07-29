namespace ApiBox.Weather;

/// <summary>
/// 省份编码
/// </summary>
public class ProvinceModel
{
    public string code { get; set; }

    public string name { get; set; }

}

/// <summary>
/// 城市编码
/// </summary>
public class CityModel
{
    public string code { get; set; }

    public string province { get; set; }

    public string city { get; set; }
}
