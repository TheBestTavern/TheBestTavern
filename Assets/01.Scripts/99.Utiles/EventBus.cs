using System;
using System.Collections.Generic;

public static class EventBus
{
    static Dictionary<Type, Delegate> eventsTable = new();

    public static void Subscribe<T>(Action<T> a)
    {
        if (eventsTable.TryGetValue(typeof(T), out var del))
            eventsTable[typeof(T)] = Delegate.Combine(a, del);
        else 
            eventsTable[typeof(T)] = a;
    }

    public static void UnSubscribe<T>(Action<T> a)
    {
        if (eventsTable.TryGetValue(typeof(T), out var del))
        {
            var cur = Delegate.Remove(del, a);
            if (cur == null) eventsTable.Remove(typeof(T));
            else eventsTable[typeof(T)] = cur;
        }
    }

    public static void Publish<T>(T evt)
    {
        if(eventsTable.TryGetValue(typeof(T),out var del))
        {
            (del as Action<T>)?.Invoke(evt);
        }
    }   
}