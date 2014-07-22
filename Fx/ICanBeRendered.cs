using System.Collections.Generic;

namespace Hnefatafl.Fx
{
    public interface ICanBeRendered
    {
        IList<IGetDrawn> GetDrawables();
    }
}