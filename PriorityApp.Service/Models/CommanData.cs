using System;
using System.Collections.Generic;
using System.Text;

namespace @enum
{
    public class CommanData
    {
        public enum Priorities
        {
            No = 2,
            Norm = 3,
            Extra = 4,
        };

        public enum NonExistSubmitNumber
        {
            NoSubmitBeforeForThisPriorityDate = -1,
            ThereOneSubmitForThisPriorityDate = 0

        };

        public enum DispatchMenu
        {
            All = 1,
            Dispatched = 2,
            NotDispatched = 3,
        };

        public enum OrderCategory
        {
            Delivery = 1,
            Pickup = 2,
            Warehouse = 3,
        };
    }
}
