using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using System.Threading.Tasks;
using UnityEngine;

public static class DOTweenExtensions
{
    public static Task AwaitCompletion(this Tween tween)
    {
        var tcs = new TaskCompletionSource<bool>();
        if (tween == null || !tween.active)
        {
            tcs.SetResult(true);
            return tcs.Task;
        }
        tween.OnComplete(() => tcs.TrySetResult(true));
        tween.OnKill(() => {
            if (!tcs.Task.IsCompleted)
                tcs.TrySetCanceled();
        });
        return tcs.Task;
    }
}