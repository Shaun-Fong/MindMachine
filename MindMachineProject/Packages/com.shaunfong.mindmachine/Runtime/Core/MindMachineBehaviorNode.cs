using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace MindMachine
{
    public class MindMachineBehaviorNode<T>
    {

        public CancellationToken CancelToken { get; private set; }

        public virtual bool Check(T instance)
        {
            return false;
        }

        public virtual async UniTask Tick(T instance)
        {
            await UniTask.Yield();
        }

        internal void INTERNAL_Init(CancellationTokenSource cancellationTokenSource)
        {
            CancelToken = cancellationTokenSource.Token;
        }
    }
}
