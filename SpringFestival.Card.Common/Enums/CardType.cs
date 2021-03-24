using System.ComponentModel;

namespace SpringFestival.Card.Common.Enums
{
    /// <summary>
    /// 节目类型
    /// </summary>
    public enum CardType
    {
        /// <summary>
        /// 歌曲类
        /// </summary>
        [Description("歌曲类")] Song = 1,

        /// <summary>
        /// 舞蹈类
        /// </summary>
        [Description("舞蹈类")] Dance = 2,

        /// <summary>
        /// 语言类
        /// </summary>
        [Description("语言类")] Language = 3,

        /// <summary>
        /// 创意类
        /// </summary>
        [Description("创意类")] Creative = 4,

        /// <summary>
        /// 戏曲类
        /// </summary>
        [Description("戏曲类")] Drama = 5,

        /// <summary>
        /// 功夫类
        /// </summary>
        [Description("功夫类")] kungFu = 6
    }
}