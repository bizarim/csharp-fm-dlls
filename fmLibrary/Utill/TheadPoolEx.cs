using System;
using System.Threading;

namespace fmLibrary
{
    public static class TheadPoolEx
    {

        // Test 결과
        // app을 실행하는 컴퓨터의 Core개수가 2개 이하 일 때 쓰레드 수를 조절 하는 건 의미 없다.
        // 또한 지금과 같이 비동기 쓰레드를 이용하는 방식은 비효율 적이다.
        // 그러면 예전 방식 처럼 이벤트가 발생하면 Queue에다 모두 담고 싱글 쓰레드가 처리 하는 방식이 훨씬 효율적이다.
        // 이건 Core가 2개 이하일 때만 그렇다. 요즘 처럼 Core의 개수가 4개 이상 8개 이상 12개 뭐 이렇게 코어가 많을 때는
        // 역시 멀티 쓰레드를 이용하는게 좋다.
        // C#은 쓰레드 수를 이용 할 수 있게 해 놓았다.

        public static bool SetMinMaxThreads(int minWorkerThreads, int maxWorkerThreads, int minCompletionPortThreads, int maxCompletionPortThreads)
        {
            if (false == ThreadPool.SetMinThreads(minWorkerThreads, minCompletionPortThreads))
                return false;

            if (false == ThreadPool.SetMaxThreads(maxWorkerThreads, maxCompletionPortThreads))
                return false;

            return true;
        }

        // 기타
        // 처음건 sync thread , 두번째건 Async thread 이다.
        // 이 설정은 cpu core 개수와 상관이 있다.
        // 쿼드 코어 일 때는 Async 4 초과해서 세팅하면 cpu 사용율이 100%를 사용하므로
        // 컴퓨터가 다운될 가능 성이 높다.
    }
}
