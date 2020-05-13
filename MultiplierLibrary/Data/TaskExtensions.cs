using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MultiplierLibrary.Data
{
    // Honestly I don't know what this is for, but it was included in the tutorial for sqlite databases in xamarin
    // https://docs.microsoft.com/en-us/xamarin/xamarin-forms/data-cloud/data/databases#code-try-2
    public static class TaskExtensions
    {
        // NOTE: Async void is intentional here. This provides a way
        // to call an async method from the constructor while
        // communicating intent to fire and forget, and allow
        // handling of exceptions
        public static async void SafeFireAndForget(this Task task,
            bool returnToCallingContext,
            Action<Exception> onException = null)
        {
            try
            {
                await task.ConfigureAwait(returnToCallingContext);
            }

            // if the provided action is not null, catch and
            // pass the thrown exception
            catch (Exception ex) when (onException != null)
            {
                onException(ex);
            }
        }
    }
}
