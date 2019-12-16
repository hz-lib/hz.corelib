using System;
using Microsoft.Extensions.DependencyInjection;
using Hz.Infrastructure.DB;
using Hz.Infrastructure.Ocr;
using Hz.Infrastructure.Token;
using System.Linq;
using Microsoft.Extensions.Configuration;

namespace Hz.Infrastructure
{
    /// <summary>
    /// 依赖注入扩展方法
    /// </summary>
    public static class DIExtentions
    {
        #region 数据库SQL

        /// <summary>
        /// 数据库服务注入
        /// </summary>
        /// <param name="services"></param>
        /// <param name="dbConfig"></param>
        /// <returns></returns>
        public static IServiceCollection AddUnitOfWork(this IServiceCollection services, Action<DB.TCDbConfig> dbConfig)
        {
            services.AddScoped<IUnitOfWork>(fac => new UnitOfWork(dbConfig));
            
            return services;
        }

        #endregion

        #region WebAPI

        /// <summary>
        /// 注入仓储及服务层
        /// </summary>
        /// <param name="services"></param>
        /// <param name="startupType"></param>
        /// <param name="respositoryEnd"></param>
        /// <param name="serviceEnd"></param>
        /// <returns></returns>
        public static IServiceCollection AddRepositoryService(this IServiceCollection services, 
        Type startupType, string respositoryEnd = "RepositoryImp", string serviceEnd = "ServiceImp")
        {
            // 注入仓储以及服务层
            var impTypes = startupType.Assembly.GetTypes().Where(x => x.Name.EndsWith(serviceEnd) || x.Name.EndsWith(respositoryEnd));
            
            foreach(var item in impTypes)
            {
                var iItem  = item.GetInterface($"I{item.Name.Remove(item.Name.Length-3)}");
                if(iItem == null)
                {
                    throw new Exception($"实现类{item.Name}没有对应接口！");
                };
                services.AddScoped(iItem,item);
            }

            return services;
        }

        #endregion

        #region Logger

        /// <summary>
        /// 注入NLogger日志
        /// </summary>
        /// <param name="services"></param>
        /// <param name="projectName">项目名称</param>
        /// <returns></returns>
        public static IServiceCollection AddLogger_NLog(this IServiceCollection services, string projectName)
        {
            services.AddSingleton<Logger.ILogger>(fac => new Logger.Nlogger(projectName));
            return services;
        }

        #endregion

        #region SMS短信

        /// <summary>
        /// 注入短信发送
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddSMS_QCloud(this IServiceCollection services, string appId, string appKey)
        {
            services.AddSingleton<SMS.ISMS>(fac => new SMS.QCloudSMS(appId, appKey));
            return services;
        }

        #endregion

        #region Ocr

        /// <summary>
        /// 注入百度Ocr
        /// </summary>
        /// <param name="services"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static IServiceCollection AddOcr_Baidu(this IServiceCollection services, Action<OcrOptions> action)
        {
            services.AddSingleton<Ocr.IOcr>(fac => new BaiduOcr(action));
            return services;
        }

        #endregion

        #region Token

        /// <summary>
        /// 注入百度token获取（提供给前台使用）
        /// </summary>
        /// <param name="services"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static IServiceCollection AddToken_Baidu(this IServiceCollection services, Action<TokenOptions> action)
        {
            services.AddSingleton<IToken>(fac => new BaiduToken(action));
            return services;
        }

        #endregion

    }
}