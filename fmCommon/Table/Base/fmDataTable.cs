using System;
using System.Linq;
using System.Collections.Generic;

namespace fmCommon
{
    public class fmDataTable
    {
        protected Dictionary<eFmDataType, Dictionary<int, fmData>> m_dicFmDatas = new Dictionary<eFmDataType, Dictionary<int, fmData>>();

        protected Dictionary<string, eFmDataType> m_dicTypes = new Dictionary<string, eFmDataType>();

        public void Clear()
        {
            foreach (var nodes in m_dicFmDatas)
            {
                nodes.Value.Clear();
            }
            m_dicFmDatas.Clear();
            m_dicTypes.Clear();
        }

        public bool Add(fmData data)
        {
            Type classType = data.GetType();

            eFmDataType eType = data.GetFmDataType();


            bool isHave = m_dicFmDatas.ContainsKey(eType);
            if (false == isHave)
                m_dicFmDatas.Add(eType, new Dictionary<int, fmData>());

            if (0 == data.Code)
                return false;

            m_dicFmDatas[eType].Add(data.Code, data);

            string className = "class " + classType.Name;

            if (false == m_dicTypes.ContainsKey(className))
            {
                m_dicTypes.Add(className, eType);
            }

            return true;
        }

        public T Find<T>(int index) where T : fmData
        {
            fmData data = Find(typeof(T), index);
            if (null == data)
                return default(T);

            return data as T;
        }

        private fmData Find(Type classType, int index)
        {
            if (false == m_dicTypes.ContainsKey(classType.Name))
                return null;

            eFmDataType type = m_dicTypes[classType.Name];


            if (false == m_dicFmDatas.ContainsKey(type))
                return null;
            if (false == m_dicFmDatas[type].ContainsKey(index))
                return null;

            return m_dicFmDatas[type][index];
        }


        public fmData Find(eFmDataType type, int index)
        {

            bool isHave = m_dicFmDatas.ContainsKey(type);
            if (false == isHave)
            {
                return null;
            }

            isHave = m_dicFmDatas[type].ContainsKey(index);
            if (false == isHave)
            {
                return null;
            }

            return m_dicFmDatas[type][index];
        }

        public Dictionary<int, fmData> Find(eFmDataType type)
        {

            bool isHave = m_dicFmDatas.ContainsKey(type);
            if (false == isHave)
            {
                return null;
            }

            return m_dicFmDatas[type];
        }

        public Dictionary<int, T> Find<T>(eFmDataType type) where T : fmData
        {

            bool isHave = m_dicFmDatas.ContainsKey(type);
            if (false == isHave)
            {
                return null;
            }

            Dictionary<int, T> dic = new Dictionary<int, T>();

            foreach (var node in m_dicFmDatas[type])
            {
                dic.Add(node.Key, node.Value as T);
            }

            return dic;

            //return m_dicFmDatas[type].ToDictionary(k => k.Key, kv => kv.Value as T);
        }

        // fmData 만들기 순서 05
        protected fmData CreateFmData(eFmDataType eType)
        {
            switch (eType)
            {
                //case eFmDataType.GameConst:     return new fmDataGameConst();
                case eFmDataType.Explore:       return new fmDataExplore();
                case eFmDataType.DTomb:         return new fmDataDragonTomb();
                case eFmDataType.Maze:          return new fmDataMaze();
                case eFmDataType.Monster:       return new fmDataMonster();
                case eFmDataType.Item:          return new fmDataItem();
                case eFmDataType.DropValue:     return new fmDataDropValue();
                case eFmDataType.Mission:       return new fmDataMission();
                case eFmDataType.Shop:          return new fmDataShop();
                //case eFmDataType.Good:          return new fmDataGood();
                case eFmDataType.Exp:           return new fmDataExp();
                //case eFmDataType.SetEffect:     return new fmDataSetEffect();
                case eFmDataType.PvpDummy:      return new fmDataPvpDummy();
                //case eFmDataType.DHeart:        return new fmDataDHeart();
                //case eFmDataType.Goblin:        return new fmDataGoblin();
                case eFmDataType.Option:        return new fmDataOption();
                case eFmDataType.Map:           return new fmDataMap();
                case eFmDataType.InDun:         return new fmDataInDun();

                default:
                    break;
            }

            return null;
        }

        public void EncodeDecode(eCoderType eCoder, BufferCoder coder)
        {
            if (eCoderType.Encode == eCoder)
            {
                {
                    int count = m_dicTypes.Count;
                    coder.EncodeDecode(eCoder, ref count);
                    foreach (var node in m_dicTypes)
                    {
                        string dicKey = node.Key.ToString();
                        eFmDataType dataType = node.Value;
                        coder.EncodeDecode(eCoder, ref dicKey);
                        coder.EncodeDecode(eCoder, ref dataType, sizeof(int));
                    }
                }
                {
                    // 1. Dictionary<eFmDataType, Dictionary<int, fmData>> dicTypeFmData
                    // 1-1 dicTypeFmData.Count;
                    int count = m_dicFmDatas.Count;
                    coder.EncodeDecode(eCoder, ref count);

                    // 1-2 dicKey
                    foreach (var node in m_dicFmDatas)
                    {
                        int dicKey = (int)node.Key;
                        coder.EncodeDecode(eCoder, ref dicKey);
                    }

                    // 2. Dictionary<int, fmData> dicFmData
                    foreach (var node in m_dicFmDatas)
                    {
                        // 2-1 dicFmData.Count
                        int cnt = node.Value.Count;
                        coder.EncodeDecode(eCoder, ref cnt);

                        // 2-2 dicKey
                        foreach (var item in node.Value)
                        {
                            item.Value.EncodeDecode(eCoder, coder);
                        }
                    }
                }
            }
            else
            {
                {
                    Dictionary<string, eFmDataType> dicTypes = new Dictionary<string, eFmDataType>();
                    dicTypes.Clear();

                    int count = 0;
                    coder.EncodeDecode(eCoder, ref count);
                    for (int i = 0; i < count; ++i)
                    {
                        string dicKey = string.Empty;
                        eFmDataType dataType = eFmDataType.None;
                        coder.EncodeDecode(eCoder, ref dicKey);
                        coder.EncodeDecode(eCoder, ref dataType, sizeof(int));
                        dicTypes.Add(dicKey, dataType);
                    }

                    m_dicTypes = dicTypes;
                }
                {
                    Dictionary<eFmDataType, Dictionary<int, fmData>> dicFmDatas = new Dictionary<eFmDataType, Dictionary<int, fmData>>();

                    int typeCount = 0;
                    coder.EncodeDecode(eCoder, ref typeCount);

                    for (int i = 0; i < typeCount; ++i)
                    {
                        eFmDataType eDataType = eFmDataType.None;
                        int dicKey = 0;
                        coder.EncodeDecode(eCoder, ref dicKey);
                        eDataType = (eFmDataType)dicKey;
                        dicFmDatas.Add(eDataType, new Dictionary<int, fmData>());
                    }

                    foreach (var node in dicFmDatas)
                    {
                        int listCount = 0;
                        coder.EncodeDecode(eCoder, ref listCount);

                        for (int i = 0; i < listCount; ++i)
                        {
                            fmData _fmData = CreateFmData(node.Key);

                            _fmData.EncodeDecode(eCoder, coder);

                            node.Value.Add(_fmData.Code, _fmData);
                        }
                    }

                    m_dicFmDatas = dicFmDatas;
                }
            }
        }
    }
}
