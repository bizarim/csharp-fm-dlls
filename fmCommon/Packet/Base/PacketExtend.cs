using System.Collections.Generic;

namespace fmCommon
{
    /// <summary>
    /// PacketExtend
    /// 목적:
    ///     collection에서 Generic 하게 사용하기 위함
    /// </summary>
    public static class PacketExtend
    {
        public static void Read(this List<eOption> list, ref Packet p)
        {
            int count = p.ReadInt();
            list.Capacity = count;

            for (int i = 0; i < count; ++i)
            {
                eOption value = (eOption)p.ReadInt();
                list.Add(value);
            }
        }

        public static void Write(this List<eOption> list, ref Packet p)
        {
            p.WriteInt(list.Count);
            foreach (var node in list)
                p.WriteInt((int)node);
        }

        public static void Read(this List<int> list, ref Packet p)
        {
            int count = p.ReadInt();
            list.Capacity = count;

            for (int i = 0; i < count; ++i)
            {
                int value = p.ReadInt();
                list.Add(value);
            }
        }

        public static void Write(this List<int> list, ref Packet p)
        {
            p.WriteInt(list.Count);
            foreach (var node in list)
                p.WriteInt(node);
        }

        public static void Read(this List<long> list, ref Packet p)
        {
            int count = p.ReadInt();
            list.Capacity = count;

            for (int i = 0; i < count; ++i)
            {
                long value = p.ReadLong();
                list.Add(value);
            }
        }

        public static void Write(this List<long> list, ref Packet p)
        {
            p.WriteInt(list.Count);
            foreach (var node in list)
                p.WriteLong(node);
        }

        public static void Read<T>(this List<T> list, ref Packet p) where T : IPacketable, new()
        {
            int count = p.ReadInt();
            list.Capacity = count;

            for (int index = 0; index < count; ++index)
            {
                T item = new T();
                item.Read(ref p);
                list.Add(item);
            }
        }

        public static void Write<T>(this List<T> list, ref Packet p) where T : IPacketable, new()
        {
            p.WriteInt(list.Count);
            foreach (T item in list)
                item.Write(ref p);
        }

        public static void ReadLongKey<T>(this Dictionary<long, T> dic, ref Packet p) where T : IPacketable, new()
        {
            int count = p.ReadInt();
            for (int index = 0; index < count; ++index)
            {
                long key = p.ReadLong();
                T item = new T();
                item.Read(ref p);
                dic.Add(key, item);
            }
        }

        public static void WriteLongKey<T>(this Dictionary<long, T> dic, ref Packet p) where T : IPacketable, new()
        {
            p.WriteInt(dic.Count);
            foreach (var node in dic)
            {
                p.WriteLong(node.Key);
                node.Value.Write(ref p);
            }
        }

        public static void ReadIntKey<T>(this Dictionary<int, T> dic, ref Packet p) where T : IPacketable, new()
        {
            int count = p.ReadInt();
            for (int index = 0; index < count; ++index)
            {
                int key = p.ReadInt();
                T item = new T();
                item.Read(ref p);
                dic.Add(key, item);
            }
        }

        public static void WriteIntKey<T>(this Dictionary<int, T> dic, ref Packet p) where T : IPacketable, new()
        {
            p.WriteInt(dic.Count);
            foreach (var node in dic)
            {
                p.WriteInt(node.Key);
                node.Value.Write(ref p);
            }
        }

        public static void ReadIntKey(this Dictionary<int, int[]> dic, ref Packet p)
        {
            int count = p.ReadInt();
            for (int index = 0; index < count; ++index)
            {
                int key = p.ReadInt();
                int length = p.ReadInt();
                int[] item = new int[length];
                for (int i = 0; i < length; ++i)
                {
                    item[i] = p.ReadInt();
                }

                dic.Add(key, item);
            }
        }

        public static void WriteIntKey(this Dictionary<int, int[]> dic, ref Packet p)
        {
            p.WriteInt(dic.Count);
            foreach (var node in dic)
            {
                p.WriteInt(node.Key);
                int length = node.Value.Length;
                p.WriteInt(length);
                for (int i = 0; i < length; ++i)
                {
                    p.WriteInt(node.Value[i]);
                }
            }
        }
    }
}
