using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace fmServerCommon.InAppBilling
{
    public class IOSReceiptVerification
    {
        #region Constants
        private const string urlSandbox = "https://sandbox.itunes.apple.com/verifyReceipt";
        private const string urlProduction = "https://buy.itunes.apple.com/verifyReceipt";
        #endregion

        #region Public Static Methods
        /// <summary>
        /// Sends the ReceiptData to the Verification Url to be verified.
        /// </summary>
        /// <returns>If true, the Receipt Verification Server indicates a valid transaction response</returns>
        public static bool IsReceiptValid(IOSReceipt receipt)
        {
            return (receipt != null && receipt.Status == 0);
        }

        public static IOSReceipt GetReceipt(bool sandbox, string receiptData)
        {
            return GetReceipt(sandbox ? urlSandbox : urlProduction, receiptData);
        }

        public static IOSReceipt GetReceipt(string url, string receiptData)
        {
            IOSReceipt result = null;

            string post = PostRequest(url, ConvertReceiptToPost(receiptData));

            if (!string.IsNullOrEmpty(post))
            {
                try { result = new IOSReceipt(post); }
                catch (Exception ex)
                {
                    result = null;
                    throw ex;
                }
            }

            return result;
        }
        #endregion

        #region Private Static Methods

        /// <summary>
        /// Make a string with the receipt encoded
        /// </summary>
        /// <param name="receipt"></param>
        /// <returns></returns>
        private static string ConvertReceiptToPost(string receipt)
        {
            //string itunesDecodedReceipt = Encoding.UTF8.GetString(ReceiptVerification.ConvertAppStoreTokenToBytes(receipt.Replace("<", string.Empty).Replace(">", string.Empty))).Trim();
            //string itunesDecodedReceipt = Encoding.UTF8.GetString(ReceiptVerification.ConvertAppStoreTokenToBytes(receipt.Replace("<", string.Empty).Replace(">", string.Empty))).Trim();
            //string encodedReceipt = Base64Encode(itunesDecodedReceipt);
            return string.Format("{{ \"receipt-data\" : \"{0}\" }}", receipt);
        }

        /// <summary>
        /// Base64 Encoding
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private static string Base64Encode(string str)
        {
            byte[] encbuff = System.Text.Encoding.UTF8.GetBytes(str);
            return Convert.ToBase64String(encbuff);
        }

        /// <summary>
        /// Base64 Decoding
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private static string Base64Decode(string str)
        {
            byte[] decbuff = Convert.FromBase64String(str);
            return System.Text.Encoding.UTF8.GetString(decbuff);
        }

        /// <summary>
        /// Sends a request to the server and reads the response
        /// </summary>
        /// <param name="url"></param>
        /// <param name="postData"></param>
        /// <returns></returns>
        private static string PostRequest(string url, string postData)
        {
            byte[] byteArray = Encoding.UTF8.GetBytes(postData);
            return PostRequest(url, byteArray);
        }

        /// <summary>
        /// Sends a request to the server and reads the response
        /// </summary>
        /// <param name="url"></param>
        /// <param name="byteArray"></param>
        /// <returns></returns>
        private static string PostRequest(string url, byte[] byteArray)
        {
            try
            {
                WebRequest request = HttpWebRequest.Create(url);
                request.Method = "POST";
                request.ContentLength = byteArray.Length;
                request.ContentType = "application/json";

                using (System.IO.Stream dataStream = request.GetRequestStream())
                {
                    dataStream.Write(byteArray, 0, byteArray.Length);
                    dataStream.Close();
                }

                using (WebResponse r = request.GetResponse())
                {
                    using (System.IO.StreamReader sr = new System.IO.StreamReader(r.GetResponseStream()))
                    {
                        return sr.ReadToEnd();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
                //return string.Empty;
            }
            throw new Exception("no response");
        }

        /// <summary>
        /// Takes the receipt from Apple's App Store and converts it to bytes
        /// that we can understand
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        private static byte[] ConvertAppStoreTokenToBytes(string token)
        {
            token = token.Replace(" ", string.Empty);
            int i = 0;
            int b = 0;
            List<byte> bytes = new List<byte>();
            while (i < token.Length)
            {
                bytes.Add(Convert.ToByte(token.Substring(i, 2), 16));
                i += 2;
                b++;
            }

            return bytes.ToArray();
        }

        #endregion Private Static Methods
    }
}
