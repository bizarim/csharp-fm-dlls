using fmCommon;
//using Lz4Net;
//using Noemax.Compression;

namespace Compress
{
    public class LZ4_PT_GC_Broadcast_Public_NT : PT_GC_Broadcast_Public_NT
    {
        public LZ4_PT_GC_Broadcast_Public_NT()
        {
            m_eProtocolType = eProtocolType.PT_GC_Broadcast_Public_NT;
        }

        protected override byte[] Compress(Packet p)
        {

            //return Lz4.CompressBytes(p.GetBuffer(), 4, p.GetPacketLen(), Lz4Mode.HighCompression);

            //byte[] buffer = p.GetContents();
            return LZMACompress.Instance.CompressMemory(p.GetContents());
        }
    }

    public class LZ4_PT_CG_Item_GetList_RS : PT_CG_Item_GetList_RS
    {
        public LZ4_PT_CG_Item_GetList_RS()
        {
            m_eProtocolType = eProtocolType.PT_CG_Item_GetList_RS;
        }

        protected override byte[] Compress(Packet p)
        {
            //return Lz4.CompressBytes(p.GetBuffer(), 4, p.GetPacketLen(), Lz4Mode.HighCompression);
            //byte[] buffer = p.GetContents();
            return LZMACompress.Instance.CompressMemory(p.GetContents());
        }
    }

    //public class LZ4_PT_CG_Lord_EnterWorld_RS : PT_CG_Lord_EnterWorld_RS
    //{
    //    public LZ4_PT_CG_Lord_EnterWorld_RS()
    //    {
    //        m_eProtocolType = eProtocolType.PT_CG_Lord_EnterWorld_RS;
    //    }

    //    protected override byte[] Compress(Packet p)
    //    {
    //        //return Lz4.CompressBytes(p.GetBuffer(), 4, p.GetPacketLen(), Lz4Mode.HighCompression);
    //        //byte[] buffer = p.GetContents();
    //        return LZMACompress.Instance.CompressMemory(p.GetContents());
    //    }
    //}

    //public class LZ4_PT_CG_Lord_CreateLord_RS : PT_CG_Lord_CreateLord_RS
    //{
    //    public LZ4_PT_CG_Lord_CreateLord_RS()
    //    {
    //        m_eProtocolType = eProtocolType.PT_CG_Lord_CreateLord_RS;
    //    }

    //    protected override byte[] Compress(Packet p)
    //    {
    //        //return Lz4.CompressBytes(p.GetBuffer(), 4, p.GetPacketLen(), Lz4Mode.HighCompression);
    //        //byte[] buffer = p.GetContents();
    //        return LZMACompress.Instance.CompressMemory(p.GetContents());
    //    }
    //}

    //public class LZ4_PT_CG_Explore_Search_RS : PT_CG_Explore_Search_RS
    //{
    //    public LZ4_PT_CG_Explore_Search_RS()
    //    {
    //        m_eProtocolType = eProtocolType.PT_CG_Explore_Search_RS;
    //    }

    //    protected override byte[] Compress(Packet p)
    //    {
    //        //return Lz4.CompressBytes(p.GetBuffer(), 4, p.GetPacketLen(), Lz4Mode.HighCompression);
    //        //byte[] buffer = p.GetContents();
    //        return LZMACompress.Instance.CompressMemory(p.GetContents());
    //    }

    //}

    //public class LZ4_PT_CG_DHeart_Search_RS : PT_CG_DHeart_Search_RS
    //{
    //    public LZ4_PT_CG_DHeart_Search_RS()
    //    {
    //        m_eProtocolType = eProtocolType.PT_CG_DHeart_Search_RS;
    //    }

    //    protected override byte[] Compress(Packet p)
    //    {
    //        //return Lz4.CompressBytes(p.GetBuffer(), 4, p.GetPacketLen(), Lz4Mode.HighCompression);
    //        //byte[] buffer = p.GetContents();
    //        return LZMACompress.Instance.CompressMemory(p.GetContents());
    //    }

    //}

    //public class LZ4_PT_CG_Maze_Search_RS : PT_CG_Maze_Search_RS
    //{
    //    public LZ4_PT_CG_Maze_Search_RS()
    //    {
    //        m_eProtocolType = eProtocolType.PT_CG_Maze_Search_RS;
    //    }

    //    protected override byte[] Compress(Packet p)
    //    {
    //        //return Lz4.CompressBytes(p.GetBuffer(), 4, p.GetPacketLen(), Lz4Mode.HighCompression);
    //        //byte[] buffer = p.GetContents();
    //        return LZMACompress.Instance.CompressMemory(p.GetContents());
    //    }

    //}
}
