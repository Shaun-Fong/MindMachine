using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Cysharp.Threading.Tasks;
using System.Reflection;
using System.Linq;
using System.Threading;

namespace MindMachine
{
    public class MindMachineBehavior<T>
    {

        private T _instance;

        private List<MindMachineBehaviorNode<T>> _BehaviorNodes;

        private MindMachineBehaviorNode<T> _CurrentBehaviorNode;

        private CancellationTokenSource _CurrentBehaviorCTS;

        public void Run(T instance)
        {
            _instance = instance;

            _BehaviorNodes = new List<MindMachineBehaviorNode<T>>();

            var _AllBehaviorNodeTypes = ReflectionUtils.GetGenericAllTypes(instance.GetType());

            for (int i = 0; i < _AllBehaviorNodeTypes.Count; i++)
            {
                var behaviorNodeInstance = Activator.CreateInstance(_AllBehaviorNodeTypes[i]);
                _BehaviorNodes.Add(behaviorNodeInstance as MindMachineBehaviorNode<T>);
            }

            StartBehavior().Forget();
        }

        public void Release()
        {
            _CurrentBehaviorCTS?.Cancel();
            _CurrentBehaviorCTS?.Dispose();
            _CurrentBehaviorCTS = null;
        }

        private async UniTask StartBehavior()
        {
            while (true)
            {
                for (int i = _BehaviorNodes.Count - 1; i >= 0; i--)
                {
                    var behaviorNode = _BehaviorNodes[i];
                    if (behaviorNode.Check(_instance) && _CurrentBehaviorNode != behaviorNode)
                    {
                        _CurrentBehaviorCTS?.Cancel();
                        _CurrentBehaviorCTS?.Dispose();
                        _CurrentBehaviorCTS = new CancellationTokenSource();
                        behaviorNode.INTERNAL_Init(_CurrentBehaviorCTS);
                        _CurrentBehaviorNode = behaviorNode;
                        BehaviorTick().Forget();
                    }
                }
                await UniTask.NextFrame();
            }
        }

        private async UniTask BehaviorTick()
        {
            while (true)
            {
                if (_CurrentBehaviorNode != null)
                {
                    await _CurrentBehaviorNode.Tick(_instance);
                }
                await UniTask.NextFrame();
            }
        }
    }
}
