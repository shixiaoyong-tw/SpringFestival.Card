using System.ComponentModel;

namespace SpringFestival.Card.Entity.Enums
{
    /// <summary>
    /// 节目类型
    /// </summary>
    public enum CardType
    {
        /// <summary>
        /// 歌曲类
        /// </summary>
        [Description("歌曲类")] Song = 0,

        /// <summary>
        /// 舞蹈类
        /// </summary>
        [Description("舞蹈类")] Dance = 1,

        /// <summary>
        /// 语言类
        /// </summary>
        [Description("语言类")] Language = 2,

        /// <summary>
        /// 创意类
        /// </summary>
        [Description("创意类")] Creative = 3,

        /// <summary>
        /// 戏曲类
        /// </summary>
        [Description("戏曲类")] Drama = 4,

        /// <summary>
        /// 功夫类
        /// </summary>
        [Description("功夫类")] kungFu = 5
    }
}