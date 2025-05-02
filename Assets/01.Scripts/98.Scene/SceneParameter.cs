using System;
using System.Collections;
using System.Collections.Generic;

public class SceneParam
{
    public Object value;
}

public static class SceneParameter
{
    private static Dictionary<string, SceneParam> paramDict = new Dictionary<string, SceneParam>();

    public static void Set(string key , Object value)
    {
        paramDict[key] = new SceneParam { value = value };
    }

    public static T Get<T>(string key)
    {
        if (paramDict.TryGetValue(key, out var sceneParam))
            return (T)sceneParam.value;
        return default;
    }
}
