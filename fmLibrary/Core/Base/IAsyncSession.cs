using System;
using System.Threading.Tasks;

namespace fmLibrary
{
    // 2016.03.31
    // ver 2.0 비동기 세션 추가
    public interface IAsyncSession
    {
    }

    public static class Async
    {

        public static Task AsyncRun(this IAsyncSession session, Action task)
        {
            return AsyncRun(session, task, TaskCreationOptions.None);
        }

        public static Task AsyncRun(this IAsyncSession session, Action task, TaskCreationOptions taskOption)
        {
            return AsyncRun(session, task, taskOption, null);
        }

        public static Task AsyncRun(this IAsyncSession session, Action task, Action<Exception> exceptionHandler)
        {
            return AsyncRun(session, task, TaskCreationOptions.None, exceptionHandler);
        }

        public static Task AsyncRun(this IAsyncSession session, Action task, TaskCreationOptions taskOption, Action<Exception> exceptionHandler)
        {
            return Task.Factory.StartNew(task, taskOption).ContinueWith(t =>
            {
                if (exceptionHandler != null)
                    exceptionHandler(t.Exception);

            }, TaskContinuationOptions.OnlyOnFaulted);
        }
    }
}
