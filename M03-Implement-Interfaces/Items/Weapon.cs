﻿using Shared.Properties;

namespace M03_Implement_Interfaces.Items
{
    public enum WeaponClass
    {
        Sword,
        Hammer,
        Wand,
        Axe
    }

    internal class Weapon : Item, IEquipable, ICombinable, ISynergyEffect
    {
        private readonly Random random = new();
        private readonly WeaponClass weaponClass;
        private int attack = 0;
        private int magic = 0;
        private int weight = 0;
        private int mana = 0;

        public Weapon(string resourceName, Bitmap image) : base(ParseResourceName(resourceName), image)
        {
            string type = resourceName[..resourceName.IndexOf("_")];

            if (WeaponClass.TryParse(type, true, out weaponClass))
                AssignStats();
        }

        public Weapon(string name, Bitmap image, WeaponClass weaponClass) : base(name, image)
        {
            this.weaponClass = weaponClass;
            AssignStats();
        }

        public bool CanCombine(Item item)
        {
            if (item == null)
                return false;

            if (item is Material material)
                return material.GetMaterialType() == MaterialType.Rune;

            return false;
        }

        public Item? Combine(Item item)
        {
            if (CanCombine(item))
            {
                return CreateRandomWeapon((Material)item);
            }
            return null;
        }

        public bool Equipped { get; set; }

        public void Equip()
        {
            Player.attack += attack;
            Player.magic += magic;
            Player.load += weight;
            Player.mana += mana;
            Equipped = true;
            equipped.Add(this);
        }

        public void Unequip()
        {
            Player.attack -= attack;
            Player.magic -= magic;
            Player.load -= weight;
            Player.mana -= mana;
            Equipped = false;
            equipped.Remove(this);
        }

        private void AssignStats()
        {
            switch (weaponClass)
            {
                case WeaponClass.Sword:
                    attack = 50 + random.Next(0, 10);
                    weight = 10 + random.Next(0, 10);
                    break;
                case WeaponClass.Wand:
                    attack = 5 + random.Next(0, 5);
                    magic = 50 + random.Next(0, 10);
                    weight = 2 + random.Next(0, 7);
                    mana = 17 + random.Next(0, 10);
                    break;
                case WeaponClass.Axe:
                    attack = 80 + random.Next(0, 20);
                    weight = 20 + random.Next(0, 10);
                    break;
                case WeaponClass.Hammer:
                    attack = 115 + random.Next(0, 30);
                    weight = 35 + random.Next(0, 10);
                    break;
            }
        }

        public bool HasSynergyWith(Item item)
        {
            if (this.Collection != null && this.Collection.Equals(item.Collection))
            {
                return true;
            }
            return false;
        }

        public bool HasSynergyWith(List<Item> items)
        {
            if (this.Collection != null)
            {
                return items.All(item => item.Collection.Equals(this.Collection));
            }

            return false;
        }

        public void ActivateSynergy(bool active)
        {
            SynergyBonus bonus = this.GetSynergyBonus();
            if (bonus != null)
            {
                if (bonus.Stat.Equals("attack"))
                {
                    if (active)
                        attack += bonus.Bonus;
                    else
                        attack -= bonus.Bonus;
                }

            }
        }

        public bool SynergyActive()
        {
            return this.HasSynergyWith(equipped);
        }

        public SynergyBonus GetSynergyBonus()
        {
            return new SynergyBonus("attack", 20);
        }
        
        public int Attack => attack;
        public int Magic => magic;
        public int Mana => mana;
        public int Weight => weight;

        private Weapon CreateRandomWeapon(Material item)
        {
            int combination = random.Next(1, 6);
            string name = item.GetRuneType() + " Hammer";
            Bitmap image;
            switch (combination)
            {
                case 1:
                    image = Resources.weapons_axe_helm_splitter;
                    break;
                case 2:
                    image = Resources.weapons_axe_gygantallax;
                    break;
                case 3:
                    image = Resources.weapons_axe_stone_winder;
                    break;
                default:
                    image = Resources.weapons_sword_death_s_edge;
                    break;
            }
            return new Weapon(name, image, WeaponClass.Hammer);
        }

        protected override int InternalSortOrder { get { return 2; } }
    }
}
