using System;

public interface ITooltipable
{
    public Action<int> OnHover { get; }
    public Action<int> OnDisHover { get; }
}