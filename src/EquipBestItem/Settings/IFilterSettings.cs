
namespace EquipBestItem
{
    public enum FilterInventorySlot
    {
        None, Helm, Cloak, Body, Gloves, Boot, HorseHarness, Weapon1, Weapon2, Weapon3, Weapon4, Horse
    }

    public interface IFilterSettings
    {
        bool IsNotDefault();
        bool IsZero();
        void Clear();
        void ClearZero();
    }
}
