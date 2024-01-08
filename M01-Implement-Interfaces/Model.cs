using M01_Implement_Interfaces.Items;

namespace Solution
{
    internal class Model
    {
        private const string none = "Select";
        private const string craft = "Craft";
        private const string equip = "Equip";
        private const string unequip = "Unequip";
        private const string consume = "Consume";
        private readonly List<Item> inventory;
        private List<Item> selectedItems;

        public Model(List<Item> inventory)
        {
            this.inventory = inventory;
            selectedItems = new List<Item>();
        }
        
        public string GetItemAction()
        {
            if (selectedItems[0] is IEquipable)
                return equip;
            else if (selectedItems[0] is IConsumable)
                return consume;
            return none;
        }

        public List<Item> GetEquipables()
        {
            var items = new List<Item>();

            foreach (Item item in inventory)
                if (item is IEquipable)
                    items.Add(item);

            return items;
        }

        public List<Item> GetConsumables()
        {
            var items = new List<Item>();

            foreach(Item item in inventory)
                if (item is IConsumable)
                    items.Add(item);
            return items;
        }

        public List<Item> GetAllItems()
        {
            inventory.Sort();
            return inventory;
        }

        public void SetSelectedItems(List<Item> selectedItems)
        {
            this.selectedItems = selectedItems;
        }
    }

}
