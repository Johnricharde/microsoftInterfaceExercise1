using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M01_Implement_Interfaces.Items
{
    internal interface IEquipable
    {
        public bool Equipped { get; set; }
        public void Equip();
        public void Unequip();
    }
}
