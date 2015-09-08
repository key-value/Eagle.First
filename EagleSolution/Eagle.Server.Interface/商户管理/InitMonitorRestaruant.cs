namespace Eagle.Server
{
    public interface IInitMonitorRestaruant : IAppServices
    {
        /// <summary>
        /// 导入餐厅数据
        /// </summary>
        /// <returns></returns>
        int Init();

        int Clear();
    }
}