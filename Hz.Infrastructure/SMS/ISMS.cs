namespace Hz.Infrastructure.SMS
{
    public interface ISMS
    {
        /// <summary>
        /// 单短信
        /// </summary>
        /// <param name="phone"></param>
        /// <param name="templateId">短信模板ID</param>
        /// <param name="sign">签名</param>
        /// <param name="args"></param>
        /// <returns></returns>
         bool SendMessage(string phone,object templateId,string sign, params string[] args);
         /// <summary>
         /// 群发短信
         /// </summary>
         /// <param name="phones"></param>
         /// <param name="templateId">短信模板ID</param>
         /// <param name="sign">签名</param>
         /// <param name="args"></param>
         /// <returns></returns>
         bool SendMultiMessage(string[] phones,object templateId,string sign, params string[] args);
    }
}