using System;

namespace fmCommon
{
    public partial class BufferCoder : IDisposable
    {
        public void EncodeDecode<T>(eCoderType eType, ref T data, int size)
        {
            if (eCoderType.Encode == eType)
            {
                # region 'T Encode'
                if (size == sizeof(byte))
                {
                    byte temp = Convert.ToByte(data);
                    EncodeByte(temp);
                }
                else if (size == sizeof(short))
                {
                    short temp = Convert.ToInt16(data);
                    //EncodeShort(temp);
                }
                else if (size == sizeof(int))
                {
                    int temp = Convert.ToInt32(data);
                    EncodeInt(temp);
                }
                else if (size == sizeof(long))
                {
                    long temp = Convert.ToInt64(data);
                    EncodeLong(temp);
                }
                # endregion
            }
            else
            {
                # region 'T Decode'
                if (size == sizeof(byte))
                {
                    byte temp = DecodeByte();
                    data = (T)(object)temp;
                }
                else if (size == sizeof(short))
                {
                    short temp = DecodeShort();
                    data = (T)(object)temp;
                }
                else if (size == sizeof(int))
                {
                    int temp = DecodeInt();
                    data = (T)(object)temp;
                }
                else if (size == sizeof(long))
                {
                    long temp = DecodeLong();
                    data = (T)(object)temp;
                }
                # endregion
            }
        }

        public void EncodeDecode(eCoderType eType, ref byte data)
        {
            if (eCoderType.Encode == eType)
            {
                EncodeByte(data);
            }
            else
            {
                data = DecodeByte();
            }
        }

        public void EncodeDecode(eCoderType eType, ref float data)
        {
            if (eCoderType.Encode == eType)
            {
                EncodeFloat(data);
            }
            else
            {
                data = DecodeFloat();
            }
        }

        public void EncodeDecode(eCoderType eType, ref int data)
        {
            if (eCoderType.Encode == eType)
            {
                EncodeInt(data);
            }
            else
            {
                data = DecodeInt();
            }
        }

        public void EncodeDecode(eCoderType eType, ref float[] data)
        {
            if (eCoderType.Encode == eType)
            {
                int length = data.Length;
                EncodeInt(length);

                for (int i = 0; i < length; ++i)
                {
                    EncodeFloat(data[i]);
                }

            }
            else
            {
                int length = DecodeInt();
                data = new float[length];
                for (int i = 0; i < length; ++i)
                {
                    data[i] = DecodeFloat();
                }
            }
        }

        public void EncodeDecode(eCoderType eType, ref int[] data)
        {
            if (eCoderType.Encode == eType)
            {
                int length = data.Length;
                EncodeInt(length);

                for (int i = 0; i < length; ++i)
                {
                    EncodeInt(data[i]);
                }

            }
            else
            {
                int length = DecodeInt();
                data = new int[length];
                for (int i = 0; i < length; ++i)
                {
                    data[i] = DecodeInt();
                }
            }
        }

        public void EncodeDecode(eCoderType eType, ref long[] data)
        {
            if (eCoderType.Encode == eType)
            {
                int length = data.Length;
                EncodeInt(length);

                for (int i = 0; i < length; ++i)
                {
                    EncodeLong(data[i]);
                }

            }
            else
            {
                int length = DecodeInt();
                data = new long[length];
                for (int i = 0; i < length; ++i)
                {
                    data[i] = DecodeLong();
                }
            }
        }

        public void EncodeDecode(eCoderType eType, ref long data)
        {
            if (eCoderType.Encode == eType)
            {
                EncodeLong(data);
            }
            else
            {
                data = DecodeLong();
            }
        }

        public void EncodeDecode(eCoderType eType, ref string data)
        {
            if (eCoderType.Encode == eType)
            {
                EncodeString(data);
            }
            else
            {
                data = DecodeString();
            }
        }

        public void EncodeDecode(eCoderType eType, ref string[] data)
        {
            if (eCoderType.Encode == eType)
            {
                int length = data.Length;
                EncodeInt(length);

                for (int i = 0; i < length; ++i)
                {
                    EncodeString(data[i]);
                }

            }
            else
            {
                int length = DecodeInt();
                data = new string[length];
                for (int i = 0; i < length; ++i)
                {
                    data[i] = DecodeString();
                }
            }
        }

        public void EncodeDecode(eCoderType eType, ref bool data)
        {
            if (eCoderType.Encode == eType)
            {
                EncodeBool(data);
            }
            else
            {
                data = DecodeBool();
            }
        }

        public void EncodeDecode(eCoderType eType, ref DataLinkList dataLink)
        {
            if (eCoderType.Encode == eType)
            {
                // Write
                string key = dataLink.GetKey();
                EncodeString(key);
                int count = dataLink.GetCount();
                EncodeInt(count);

                int fromIndex = dataLink.m_fromfmData.Code;
                //EncodeInt(fromIndex);
                eFmDataType fromFmDataType = dataLink.m_fromfmData.GetFmDataType();
                //EncodeInt((int)fromFmDataType);

                foreach (var node in dataLink.GetLinkList())
                {
                    int toIndex = node.Code;
                    EncodeInt(toIndex);
                    eFmDataType toFmDataType = node.GetFmDataType();
                    EncodeInt((int)toFmDataType);
                }
            }
            else
            {
                // Read
                string key = DecodeString();
                int count = DecodeInt();
                //int fromIndex = DecodeInt();
                //eFmDataType fromFmDataType = (eFmDataType)DecodeInt();

                for (int i = 0; i < count; ++i)
                {
                    fmLink link = new fmLink();
                    link.m_nFromIndex = dataLink.m_fromfmData.Code;
                    link.m_eFromFmDataType = dataLink.m_fromfmData.GetFmDataType();

                    link.m_nToIndex = DecodeInt();
                    link.m_eToFmDataType = (eFmDataType)DecodeInt();

                    link.m_strFromKey = key;
                    link.m_strToKey = string.Empty;

                    fmLinker.Add(link);
                }
            }
        }

        public void EncodeDecode(eCoderType eType, ref DataLinkDic dataLink)
        {
            if (eCoderType.Encode == eType)
            {
                // Write
                string key = dataLink.GetKey();
                EncodeString(key);
                int count = dataLink.GetCount();
                EncodeInt(count);

                int fromIndex = dataLink.m_fromfmData.Code;
                //EncodeInt(fromIndex);
                eFmDataType fromFmDataType = dataLink.m_fromfmData.GetFmDataType();
                //EncodeInt((int)fromFmDataType);

                foreach (var node in dataLink.GetLinkDic())
                {
                    int toIndex = node.Value.Code;
                    EncodeInt(toIndex);
                    eFmDataType toFmDataType = node.Value.GetFmDataType();
                    EncodeInt((int)toFmDataType);
                }
            }
            else
            {
                // Read
                string key = DecodeString();
                int count = DecodeInt();
                //int fromIndex = DecodeInt();
                //eFmDataType fromFmDataType = (eFmDataType)DecodeInt();

                for (int i = 0; i < count; ++i)
                {
                    fmLink link = new fmLink();
                    link.m_nFromIndex = dataLink.m_fromfmData.Code;
                    link.m_eFromFmDataType = dataLink.m_fromfmData.GetFmDataType();

                    link.m_nToIndex = DecodeInt();
                    link.m_eToFmDataType = (eFmDataType)DecodeInt();

                    link.m_strFromKey = key;
                    link.m_strToKey = string.Empty;

                    fmLinker.Add(link);
                }
            }
        }

        public void EncodeDecode(eCoderType eType, ref DataLinkOneZero dataLink)
        {
            if (eCoderType.Encode == eType)
            {
                // Write
                string key = dataLink.GetKey();
                EncodeString(key);

                int fromIndex = dataLink.m_fromfmData.Code;
                //EncodeInt(fromIndex);
                eFmDataType fromFmDataType = dataLink.m_fromfmData.GetFmDataType();
                //EncodeInt((int)fromFmDataType);

                var data = dataLink.GetLinkFmData();
                if (null == data)
                {
                    EncodeInt(0);
                    EncodeInt(0);
                }
                else
                {
                    int toIndex = data.Code;
                    EncodeInt(toIndex);
                    eFmDataType toFmDataType = data.GetFmDataType();
                    EncodeInt((int)toFmDataType);
                }
            }
            else
            {
                // Read
                string key = DecodeString();

                fmLink link = new fmLink();
                link.m_nFromIndex = dataLink.m_fromfmData.Code;
                link.m_eFromFmDataType = dataLink.m_fromfmData.GetFmDataType();

                link.m_nToIndex = DecodeInt();
                link.m_eToFmDataType = (eFmDataType)DecodeInt();

                link.m_strFromKey = key;
                link.m_strToKey = string.Empty;

                if (link.m_nToIndex != 0 || link.m_eToFmDataType != 0)
                    fmLinker.Add(link);
            }
        }
    }

        //public void ENCODEDECODE(eENCODEDECODE eType, ref Dictionary<string, int> dicTypeInt)
        //{
        //    if (eENCODEDECODE.Encode == eType)
        //    {
        //        int count = dicTypeInt.Count;
        //        EncodeInt(count);

        //        foreach (var node in dicTypeInt)
        //        {
        //            EncodeString(node.Key);
        //            EncodeInt(node.Value);
        //        }
        //    }
        //    else
        //    {
        //        int count = DecodeInt();
        //        if (null == dicTypeInt)
        //            dicTypeInt = new Dictionary<string, int>();

        //        string[] keys = new string[count];
        //        int[] values = new int[count];

        //        for (int i = 0; i < count; ++i)
        //        {
        //            keys[i] = DecodeString();
        //            values[i] = DecodeInt();
        //            dicTypeInt.Add(keys[i], values[i]);
        //        }
        //    }
        //}

        //public void ENCODEDECODE(eENCODEDECODE eType, ref Dictionary<string, long> dicTypeLong)
        //{
        //    if (eENCODEDECODE.Encode == eType)
        //    {
        //        int count = dicTypeLong.Count;
        //        EncodeInt(count);

        //        foreach (var node in dicTypeLong)
        //        {
        //            EncodeString(node.Key);
        //            EncodeLong(node.Value);
        //        }
        //    }
        //    else
        //    {
        //        int count = DecodeInt();
        //        if (null == dicTypeLong)
        //            dicTypeLong = new Dictionary<string, long>();

        //        string[] keys = new string[count];
        //        long[] values = new long[count];

        //        for (int i = 0; i < count; ++i)
        //        {
        //            keys[i] = DecodeString();
        //            values[i] = DecodeLong();
        //            dicTypeLong.Add(keys[i], values[i]);
        //        }
        //    }
        //}



        //public void EncodeDecode(eCoderType eType, ref DataLinkList dataLink)
        //{
        //    if (eCoderType.Encode == eType)
        //    {
        //        // Write
        //        string key = dataLink.GetKey();
        //        EncodeString(key);
        //        int count = dataLink.GetCount();
        //        EncodeInt(count);

        //        int fromIndex = dataLink.m_fmData.INDEX;
        //        //EncodeInt(fromIndex);
        //        eFmDataType fromFmDataType = dataLink.m_fmData.GetFmDataType();
        //        //EncodeInt((int)fromFmDataType);

        //        foreach (var node in dataLink.GetList())
        //        {
        //            int toIndex = node.INDEX;
        //            EncodeInt(toIndex);
        //            eFmDataType toFmDataType = node.GetFmDataType();
        //            EncodeInt((int)toFmDataType);
        //        }
        //    }
        //    else
        //    {
        //        // Read
        //        string key = DecodeString();
        //        int count = DecodeInt();
        //        //int fromIndex = DecodeInt();
        //        //eFmDataType fromFmDataType = (eFmDataType)DecodeInt();

        //        for (int i = 0; i < count; ++i)
        //        {
        //            LinkInfo link = new LinkInfo();
        //            link.m_nFromIndex = dataLink.m_fmData.INDEX;
        //            link.m_eFromFmDataType = dataLink.m_fmData.GetFmDataType();

        //            link.m_nToIndex = DecodeInt();
        //            link.m_eToFmDataType = (eFmDataType)DecodeInt();

        //            link.m_strFromKey = key;
        //            link.m_strToKey = string.Empty;

        //            LinkInfoManager.Add(link);
        //        }
        //    }
        //}

        //public void EncodeDecode(eCoderType eType, ref DataLinkDic dataLink)
        //{
        //    if (eCoderType.Encode == eType)
        //    {
        //        // Write
        //        string key = dataLink.GetKey();
        //        EncodeString(key);
        //        int count = dataLink.GetCount();
        //        EncodeInt(count);

        //        int fromIndex = dataLink.m_fmData.INDEX;
        //        //EncodeInt(fromIndex);
        //        eFmDataType fromFmDataType = dataLink.m_fmData.GetFmDataType();
        //        //EncodeInt((int)fromFmDataType);

        //        foreach (var node in dataLink.GetDic())
        //        {
        //            int toIndex = node.Value.INDEX;
        //            EncodeInt(toIndex);
        //            eFmDataType toFmDataType = node.Value.GetFmDataType();
        //            EncodeInt((int)toFmDataType);
        //        }
        //    }
        //    else
        //    {
        //        // Read
        //        string key = DecodeString();
        //        int count = DecodeInt();
        //        //int fromIndex = DecodeInt();
        //        //eFmDataType fromFmDataType = (eFmDataType)DecodeInt();

        //        for (int i = 0; i < count; ++i)
        //        {
        //            LinkInfo link = new LinkInfo();
        //            link.m_nFromIndex = dataLink.m_fmData.INDEX;
        //            link.m_eFromFmDataType = dataLink.m_fmData.GetFmDataType();

        //            link.m_nToIndex = DecodeInt();
        //            link.m_eToFmDataType = (eFmDataType)DecodeInt();

        //            link.m_strFromKey = key;
        //            link.m_strToKey = string.Empty;

        //            LinkInfoManager.Add(link);
        //        }
        //    }
        //}
}
