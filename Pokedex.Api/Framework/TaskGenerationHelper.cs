using System;
using System.Threading.Tasks;

namespace Pokedex.Api.Framework
{
    internal static class TaskGenerationHelper
    {
        //This Class has been added temporarily to allow Async Api responses from day 1 and meet the initial Design decision to go Async 
        //for both performance and integration with modern front end frameworks/libraries - it creates the async nature the controllers' operations
        //need to be awaiting for and release client request thread to allow other operations while awaiting (Rich UIs), while keeping the code readable and not repeated on the API Controllers
        public static Task<T> CreateTask<T>(T item)
        {            
            return Task.FromResult(item);
        }
    }
}
