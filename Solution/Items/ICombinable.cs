﻿namespace Solution.Items
{
    internal interface ICombinable
    {
        public Item? Combine(Item item);

        public bool CanCombine(Item item);
    }
}
