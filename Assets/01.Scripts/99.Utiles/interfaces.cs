using System;
using Cysharp.Threading.Tasks;

public interface ITooltipable
{
    public Action<int> OnHover { get; }
    public Action<int> OnDisHover { get; }
}

public interface IRunAlready
{
    public UniTask RunAlready();
}

public interface IItemStackFactory
{
    ItemStack Create(Data_Foods data_Foods, int amount, InvenType invenType);
}