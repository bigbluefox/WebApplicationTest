namespace Hsp.Test.Model
{
    /// <summary>
    ///     AnyShare 认证信息实体
    /// </summary>
    public class AnyShareAuth
    {
        /// <summary>
        ///     用户标识
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        ///     验证令牌
        /// </summary>
        public string TokenId { get; set; }

        /// <summary>
        ///     有效期，单位为秒
        /// </summary>
        public int Expires { get; set; }
    }
}