using NetCore.Common.Files;
using Newtonsoft.Json;
using RestSharp;

namespace ApiBox.Weather;

/// <summary>
/// 全国天气的api封装
/// <para> 数据来源 http://www.nmc.cn/ 中央气象网</para>
/// </summary>
public class WeatherInfo
{
    /// <summary>
    /// 文件存放地址
    /// </summary>
    private static readonly string _WeatherFileFloder = $"{Directory.GetCurrentDirectory()}/WehtherInfo";

    #region 获取天气信息
    /// <summary>
    /// 
    /// </summary>
    /// <param name="cityCode">城市的编码</param>
    /// <returns></returns>
    public static string GetWeatherInfo(string cityCode)
    {
        string weatherInfo = string.Empty;
        if (!Directory.Exists(_WeatherFileFloder))
        {
            Directory.CreateDirectory(_WeatherFileFloder);
        }

        CityModel provinceModel = _CITYINFO.Where(d => d.code == cityCode).FirstOrDefault();

        var client = new RestClient($"http://www.nmc.cn/rest/weather?stationid={provinceModel.code}");
        var request = new RestRequest();
        request.AddHeader("Accept", "*/*");
        request.AddHeader("Host", "www.nmc.cn");
        request.AddHeader("Connection", "keep-alive");
        RestResponse response = client.Execute(request);
        return response.Content;
    }
    #endregion

    #region 省份信息
    public static List<ProvinceModel> _PROVINCEINFO = new List<ProvinceModel>();
    /// <summary>
    /// 读取省份信息
    /// </summary>
    /// <param name="proviceFileLimit">文件过期时间，默认30天</param>
    /// <returns></returns>
    public List<ProvinceModel> GetProvinceInfo(int proviceFileLimit = 30)
    {
        if (_PROVINCEINFO is not null && _PROVINCEINFO.Count > 0)
        {
            return _PROVINCEINFO;
        }
        string provinceInfo = string.Empty;
        DirectoryInfo directoryInfo = new(_WeatherFileFloder);
        if (!directoryInfo.Exists || directoryInfo is null)
        {
            provinceInfo = CreateProvinceFile();
        }
        else
        {
            FileSystemInfo[] files = directoryInfo.GetFiles();
            var file = files.Where(d => d.Name.Contains("_PROVINCE.json")).FirstOrDefault();
            switch (file)
            {
                case null:
                    provinceInfo = CreateProvinceFile();
                    break;
                default:
                    {
                        int date = int.Parse(file.Name.Split("//").Last().Split("_").First());
                        provinceInfo = date + proviceFileLimit > int.Parse(DateTime.Now.ToString("yyyyMMdd"))
                            ? CreateProvinceFile()
                            : File.ReadAllText(file.FullName);
                        break;
                    }
            }
        }
        return JsonConvert.DeserializeObject<List<ProvinceModel>>(provinceInfo);
    }

    private static string CreateProvinceFile()
    {
        var client = new RestClient("http://www.nmc.cn/rest/province/all");
        var request = new RestRequest();
        request.AddHeader("Accept", "*/*");
        request.AddHeader("Host", "www.nmc.cn");
        request.AddHeader("Connection", "keep-alive");
        RestResponse response = client.Execute(request);
        FileHelper.WriteFile(response.Content, $"{_WeatherFileFloder}/{DateTime.Now:yyyyMMdd}_PROVINCE.json");
        return response.Content;
    }
    #endregion

    #region 城市信息
    public static List<CityModel> _CITYINFO = new List<CityModel>();

    /// <summary>
    /// 获取城市信息
    /// </summary>
    /// <param name="proviceFileLimit">文件过期时间，默认30天</param>
    /// <returns></returns>
    public static List<CityModel> GetCityInfo(string provinceCode, int cityFileLimit = 30)
    {
        if (_CITYINFO is not null && _CITYINFO.Count > 0)
        {
            return _CITYINFO;
        }
        string provinceInfo = string.Empty;
        DirectoryInfo directoryInfo = new(_WeatherFileFloder);
        if (!directoryInfo.Exists || directoryInfo is null)
        {
            provinceInfo = CreateCityFile(provinceCode);
        }
        else
        {
            FileSystemInfo[] files = directoryInfo.GetFiles();
            var file = files.Where(d => d.Name.Contains($"_CITY_{provinceCode}.json")).FirstOrDefault();
            switch (file)
            {
                case null:
                    provinceInfo = CreateCityFile(provinceCode);
                    break;
                default:
                    {
                        int date = int.Parse(file.Name.Split("//").Last().Split("_").First());
                        provinceInfo = date + cityFileLimit > int.Parse(DateTime.Now.ToString("yyyyMMdd"))
                            ? CreateProvinceFile()
                            : File.ReadAllText(file.FullName);
                        break;
                    }
            }
        }
        return JsonConvert.DeserializeObject<List<CityModel>>(provinceInfo);
    }

    /// <summary>
    /// 获取城市信息
    /// </summary>
    /// <param name="provinceCode">省份编码</param>
    /// <returns></returns>
    private static string CreateCityFile(string provinceCode)
    {
        var client = new RestClient($"http://www.nmc.cn/rest/province/{provinceCode}");
        var request = new RestRequest();
        request.AddHeader("Accept", "*/*");
        request.AddHeader("Host", "www.nmc.cn");
        request.AddHeader("Connection", "keep-alive");
        RestResponse response = client.Execute(request);
        FileHelper.WriteFile(response.Content, $"{_WeatherFileFloder}/{DateTime.Now:yyyyMMdd}_CITY_{provinceCode}.json");
        return response.Content;
    }
    #endregion

}
