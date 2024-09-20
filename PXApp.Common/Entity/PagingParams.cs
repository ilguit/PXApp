namespace PXApp.Common.Entity;

public class PagingParams
{
    private readonly int _take;
    private readonly int _skip;

    public PagingParams(int take, int skip)
    {
        _take = take;
        _skip = skip;
    }

    public int Take => Math.Max(0, _take);
    public int Skip => Math.Max(0, _skip);
}