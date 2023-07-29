namespace vDesk.Model
{
    /// <summary>
    /// 服务端给前端返回的消息格式
    /// </summary>
    public class ResponseMessage<T>
    {
        /// <summary>
        /// 返回的状态码
        /// </summary>
        public ResponseMessageCode Code { get; set; } = ResponseMessageCode.失败;

        /// <summary>
        /// 返回消息
        /// </summary>
        public string Msg { get; set; }

        /// <summary>
        /// 返回的数据，泛型
        /// </summary>
        public T Data { get; set; } = default;
    }

    public enum ResponseMessageCode : int
    {
        失败 = -1,
        成功 = 200
    }
}