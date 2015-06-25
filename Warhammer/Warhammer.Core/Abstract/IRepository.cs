﻿namespace Warhammer.Core.Abstract
{
    /// <summary>
    /// The IRepository interface is the lowest data interface and connects directly to the data source
    /// 
    /// Any code inside implementations of IRepository are very hard to unit test and for this reason implemenatations of IRepository should be entirely bolier-plate code
    /// No business logic or filtering of data should happen in the IRepository
    ///  </summary>
    public interface IRepository
    {

    }
}
