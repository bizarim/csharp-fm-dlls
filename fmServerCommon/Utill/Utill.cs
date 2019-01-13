using fmCommon;
using System;
using System.Collections.Generic;
using System.Linq;

namespace fmServerCommon
{
    //http://theeye.pe.kr/archives/2570
    //https://stackoverflow.com/questions/11115381/unable-to-get-the-subscription-information-from-google-play-android-developer-ap

    //http://blog.naver.com/bbirdk2/150188786194

    public static class Utill
    {
        public static List<fmWorld> ToFmWorld(this List<fmOtherServer> list)
        {
            return list.Select(item => item.m_desc.ToFmWorld()).ToList();
        }


        public static void Shuffle<T>(this IList<T> list)
        {
            int n = list.Count;
            Random rnd = new Random();
            while (n > 1)
            {
                int k = (rnd.Next(0, n) % n);
                n--;
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }
}
