using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

#region << 创建和验证签名Cng >>
/*----------------------------------------------------------------
 * 版权所有 (c) 2022   保留所有权利。
 * CLR版本：4.0.30319.42000
 * 公司名称：
 * 命名空间：CodeCollect
 * 文件名：SigningDemo
 * 创建者：蔡程健
 * 创建时间：2022/6/24 21:00:39
 * 版本：V1.0.0
 * 描述：需要添加包System.Security.Cryptography.Cng
 *
 * ----------------------------------------------------------------
 * 修改人：
 * 时间：
 * 修改说明：
 *
 * 版本：V1.0.1
 *----------------------------------------------------------------*/
#endregion << 版 本 注 释 >>

namespace CodeCollect
{
    internal class SigningDemo
    {
        //System.Security.Cryptography.Cng
        CngKey _aliceKeySignature;
        private byte[] _alicePubKeyBlob;

        public void Run()
        {
            InitAliceKeys();

            byte[] aliceData = Encoding.UTF8.GetBytes("Alice");
            byte[] aliceSignature = CreateSignature(aliceData, _aliceKeySignature);
            Console.WriteLine($"Alice created signature: {Convert.ToBase64String(aliceSignature)}");

            if (VerifySignature(aliceData, aliceSignature, _alicePubKeyBlob))
            {
                Console.WriteLine("Alice signature verified successfully");
            }
        }

        /// <summary>
        /// 创建新的密钥对
        /// </summary>
        public void InitAliceKeys()
        {
            _aliceKeySignature = CngKey.Create(CngAlgorithm.ECDsaP521);
            _alicePubKeyBlob = _aliceKeySignature.Export(CngKeyBlobFormat.GenericPublicBlob);
        }

        /// <summary>
        /// 创建签名
        /// </summary>
        /// <param name="data"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        private byte[] CreateSignature(byte[] data, CngKey key)
        {
            byte[] signature;
            using (var signingAlg = new ECDsaCng(key))
            {   //给数据签名
                signature = signingAlg.SignData(data, HashAlgorithmName.SHA512);
                signingAlg.Clear();
            }
            return signature;
        }
        /// <summary>
        /// 验证签名
        /// </summary>
        /// <param name="data"></param>
        /// <param name="signature"></param>
        /// <param name="pubKey"></param>
        /// <returns></returns>
        private bool VerifySignature(byte[] data, byte[] signature, byte[] pubKey)
        {
            bool retValue = false;
            using (CngKey key = CngKey.Import(pubKey, CngKeyBlobFormat.GenericPublicBlob))
            using (var signingAlg = new ECDsaCng(key))
            {   //验证签名
                retValue = signingAlg.VerifyData(data, signature, HashAlgorithmName.SHA512);
                signingAlg.Clear();
            }
            return retValue;
        }
    }
}
