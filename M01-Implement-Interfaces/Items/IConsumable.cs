using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M01_Implement_Interfaces.Items
{
    internal interface IConsumable
    {
        bool Consumed { get; set; }
        void Consume();
    }
}
