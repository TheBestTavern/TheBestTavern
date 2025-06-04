using System;
using System.Collections.Generic;

public static class EventBus
{
    static Dictionary<Type, Delegate> eventsTable = new();

    // 델리게이트 구독 로직
    public static void Subscribe<T>(Action<T> a)
    {
        if (eventsTable.TryGetValue(typeof(T), out var del))
            eventsTable[typeof(T)] = Delegate.Combine(a, del);
        else 
            eventsTable[typeof(T)] = a;
    }

    // 델리게이트 구독 해제 로직
    public static void UnSubscribe<T>(Action<T> a)
    {
        if (eventsTable.TryGetValue(typeof(T), out var del))
        {
            var cur = Delegate.Remove(del, a);
            if (cur == null) eventsTable.Remove(typeof(T));
            else eventsTable[typeof(T)] = cur;
        }
    }

    // 델리게이트 실행 로직
    public static void Publish<T>(T evt)
    {
        if(eventsTable.TryGetValue(typeof(T),out var del))
        {
            (del as Action<T>)?.Invoke(evt);
        }
    }   
}