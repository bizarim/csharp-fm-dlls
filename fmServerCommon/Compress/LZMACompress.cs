using System;
using System.IO;
//using System.Collections;
//using System.Runtime.Serialization;
//using System.Runtime.Serialization.Formatters.Binary;
using fmServerCommon;

namespace Compress
{
    public class LZMACompress : Singleton<LZMACompress>
    {
        public byte[] CompressMemory(byte[] input)
        {
            SevenZip.Compression.LZMA.Encoder coder = new SevenZip.Compression.LZMA.Encoder();
            using (MemoryStream ms = new MemoryStream(input))
            {
                using (MemoryStream os = new MemoryStream())
                {
                    // Write the encoder properties
                    coder.WriteCoderProperties(os);

                    // Write the decompressed file size.
                    os.Write(BitConverter.GetBytes(ms.Length), 0, 8);

                    // Encode the file.
                    coder.Code(ms, os, input.Length, -1, null);
                    os.Flush();

                    return os.ToArray();
                }
            }

        }

        public byte[] DecompressFromBuffer(byte[] enc)
        {
            SevenZip.Compression.LZMA.Decoder coder = new SevenZip.Compression.LZMA.Decoder();

            using (MemoryStream ims = new MemoryStream(enc))
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    byte[] properties = new byte[5];
                    ims.Read(properties, 0, 5);

                    // Read in the decompress file size.
                    byte[] fileLengthBytes = new byte[8];
                    ims.Read(fileLengthBytes, 0, 8);

                    long fileLength = BitConverter.ToInt64(fileLengthBytes, 0);

                    coder.SetDecoderProperties(properties);
                    coder.Code(ims, ms, ims.Length, fileLength, null);

                    ms.Flush();
                    return ms.ToArray();
                }
            }
        }
    }
}