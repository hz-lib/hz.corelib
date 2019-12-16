namespace Hz.Infrastructure.Ocr {
    public interface IOcr {
        /// <summary>
        /// 根据图片地址，识别图片中的文字
        /// </summary>
        /// <param name="path">图片物理地址</param>
        /// <returns></returns>
        string GeneralBasic (string path);
        /// <summary>
        /// 识别图片文字
        /// </summary>
        /// <param name="image">图片</param>
        /// <returns></returns>
        string GeneralBasic (byte[] image);

        /// <summary>
        /// 识别图片文字
        /// </summary>
        /// <param name="url">图片url地址</param>
        /// <returns></returns>
        string GeneralBasicUrl (string url);
    }
}