using System;
using System.Linq;
using Microsoft.Extensions.Configuration;

namespace SpringFestival.Card.Common.Enums
{
    public class AppSettings
    {
        private static IConfiguration Configuration { get; set; }

        public AppSettings(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// 封装要操作的字符
        /// </summary>
        /// <param name="sections">节点配置</param>
        /// <returns></returns>
        public static string Apply(params string[] sections)
        {
            try
            {
                if (sections.Any())
                {
                    return Configuration[string.Join(":", sections)];
                }
            }
            catch (Exception)
            {
                // ignored
            }

            return "";
        }
    }
}