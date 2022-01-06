// Copyright (C) 2020, The Tuckfirtle Developers
// 
// Please see the included LICENSE file for more information.

using System.Threading;
using System.Threading.Tasks;

namespace Tuckfirtle.Core.Utility
{
    public static class TaskUtility
    {
        public static async Task WaitUntilCancellation(Task task, CancellationToken cancellationToken = default)
        {
            using var cancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
            var waitForCancellationTask = Task.Delay(-1, cancellationTokenSource.Token);

            var taskCompleted = await Task.WhenAny(task, waitForCancellationTask).ConfigureAwait(false);

            if (taskCompleted == task)
            {
                cancellationTokenSource.Cancel();
            }
        }

        public static async Task<T> WaitUntilCancellation<T>(Task<T> task, CancellationToken cancellationToken = default)
        {
            using var cancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
            var waitForCancellationTask = Task.Delay(-1, cancellationTokenSource.Token);

            var taskCompleted = await Task.WhenAny(task, waitForCancellationTask).ConfigureAwait(false);

            if (taskCompleted != task) return await Task.FromResult<T>(default!);

            cancellationTokenSource.Cancel();
            return await task.ConfigureAwait(false);
        }
    }
}