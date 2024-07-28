using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace MindMachine
{
    internal class ReflectionUtils
    {
        internal static List<Type> GetGenericAllTypes(Type targetType)
        {
            // 获取所有已加载的程序集
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();

            // 创建一个集合来存储所有包含目标类型的泛型类型
            var result = new List<Type>();

            foreach (var assembly in assemblies)
            {
                // 获取程序集中的所有类型
                var types = assembly.GetTypes();

                foreach (var type in types)
                {
                    // 如果是泛型类型定义，跳过
                    if (type.IsGenericTypeDefinition)
                        continue;

                    // 获取类型的所有泛型参数
                    Type[] genericArguments = type.GetGenericArguments();

                    // 如果泛型参数包含目标类型，将该类型加入结果集合
                    if (genericArguments.Contains(targetType))
                    {
                        result.Add(type);
                    }
                    else
                    {
                        // 检查基类
                        if (type.BaseType != null && type.BaseType.IsGenericType)
                        {
                            genericArguments = type.BaseType.GetGenericArguments();
                            if (genericArguments.Contains(targetType))
                            {
                                result.Add(type);
                            }
                        }

                        // 检查接口
                        foreach (var iface in type.GetInterfaces())
                        {
                            if (iface.IsGenericType)
                            {
                                genericArguments = iface.GetGenericArguments();
                                if (genericArguments.Contains(targetType))
                                {
                                    result.Add(type);
                                }
                            }
                        }
                    }
                }
            }

            return result;
        }
    }
}
