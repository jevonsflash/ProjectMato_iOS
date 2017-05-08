namespace ProjectMato.iOS.Model
{
    /// <summary>
    /// 请求参数抽象类(模版)
    /// </summary>
    public abstract class RequestData
    {
        /// <summary>
        /// 接口地址
        /// </summary>
        public abstract string Url { get; }
        
        /// <summary>
        /// 请求方式(默认GET)
        /// </summary>
        public virtual string Method { get; set; } = "GET";
        
        /// <summary>
        /// 请求参数[匿名对象]
        /// </summary>
        public object FormData { get; set; }
    }
}
