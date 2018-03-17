using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace WorkerAsyncCSharp5Impl
{
    internal class Worker
    {
        public bool WorkCompleted { get; set; }

        [DebuggerStepThrough]
        public void DoWork()
        {
            DoWorkAsyncStateMachine asyncStateMachine = new DoWorkAsyncStateMachine();
            asyncStateMachine.Worker = this;
            asyncStateMachine.AsyncVoidMethodBuilder = AsyncVoidMethodBuilder.Create();
            asyncStateMachine.State = -1;

            AsyncVoidMethodBuilder localBuilder = asyncStateMachine.AsyncVoidMethodBuilder;
            localBuilder.Start<DoWorkAsyncStateMachine>(ref asyncStateMachine);
        }

        [DebuggerStepThrough]
        private Task LongOperation()
        {
            LongOperationAsyncStateMachine asyncStateMachine = new LongOperationAsyncStateMachine();
            asyncStateMachine.Worker = this;
            asyncStateMachine.AsyncTaskMethodBuilder = AsyncTaskMethodBuilder.Create();
            asyncStateMachine.State = -1;

            AsyncTaskMethodBuilder localBuilder = asyncStateMachine.AsyncTaskMethodBuilder;
            localBuilder.Start<LongOperationAsyncStateMachine>(ref asyncStateMachine);

            return asyncStateMachine.AsyncTaskMethodBuilder.Task;
        }

        private sealed class DoWorkAsyncStateMachine : IAsyncStateMachine
        {
            public int State;

            public AsyncVoidMethodBuilder AsyncVoidMethodBuilder;

            public Worker Worker;

            private TaskAwaiter _awaiter;
            
            public void MoveNext()
            {
                var num = this.State;
                try
                {
                    TaskAwaiter awaiter;
                    if (num != 0)
                    {
                        this.Worker.WorkCompleted = false;
                        Console.WriteLine("Начало работы");
                        awaiter = this.Worker.LongOperation().GetAwaiter();
                        if (!awaiter.IsCompleted)
                        {
                            this.State = 0;
                            this._awaiter = awaiter;
                            DoWorkAsyncStateMachine asyncStateMachine = this;
                            
                            this.AsyncVoidMethodBuilder.AwaitUnsafeOnCompleted<TaskAwaiter, DoWorkAsyncStateMachine>(ref awaiter, ref asyncStateMachine);
                            return;
                        }
                    }
                    else
                    {
                        awaiter = this._awaiter;
                        this._awaiter = default(TaskAwaiter);
                        this.State = -1;
                    }
                    
                    awaiter.GetResult();
                    this.Worker.WorkCompleted = true;
                    Console.WriteLine();
                    Console.WriteLine("Работа завершена");
                }
                catch (Exception e)
                {
                    this.State = -2;
                    this.AsyncVoidMethodBuilder.SetException(e);
                    return;
                }

                this.State = -2;
                this.AsyncVoidMethodBuilder.SetResult();
            }

            [DebuggerHidden]
            public void SetStateMachine(IAsyncStateMachine stateMachine)
            {
            }
        }

        private sealed class LongOperationAsyncStateMachine : IAsyncStateMachine
        {
            public int State;

            public AsyncTaskMethodBuilder AsyncTaskMethodBuilder;

            public Worker Worker;

            private TaskAwaiter _awaiter;
            
            public void MoveNext()
            {

                var num = this.State;
                try
                {
                    TaskAwaiter awaiter;
                    if (num != 0)
                    {
                        Console.WriteLine("Работаю...");
                        awaiter = Task.Delay(5000).GetAwaiter();
                        if (!awaiter.IsCompleted)
                        {
                            this.State = 0;
                            this._awaiter = awaiter;
                            LongOperationAsyncStateMachine asyncStateMachine = this;
                            this.AsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<TaskAwaiter, LongOperationAsyncStateMachine>(ref awaiter, ref asyncStateMachine);

                            return;
                        }
                    }
                    else
                    {
                        awaiter = this._awaiter;
                        this._awaiter = default(TaskAwaiter);
                        this.State = -1;
                    }
                    
                    awaiter.GetResult();
                }
                catch (Exception e)
                {
                    this.State = -2;
                    this.AsyncTaskMethodBuilder.SetException(e);
                    return;
                }

                this.State = -2;
                this.AsyncTaskMethodBuilder.SetResult();
            }

            [DebuggerHidden]
            public void SetStateMachine(IAsyncStateMachine stateMachine)
            {
            }
        }
    }
}