using System.Runtime.CompilerServices;

namespace ExosHeroesManager;

public class Equipment : INotifyPropertyChanged
{
    #region Implementations
    public event PropertyChangedEventHandler? PropertyChanged;
    private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
    {
        PropertyChanged?.Invoke(this, new(propertyName));
    }
    #endregion

    public EquipmentPieceBase Accessory
    {
        get
        {
            if (Convert.ToString(Hero.Data?[(int)HeroColumn.Accessory] ?? string.Empty).Equals(string.Empty))
            {
                return pendingAccessory;
            }

            string id = Convert.ToString(Hero.Data?[(int)HeroColumn.Accessory]) ?? string.Empty;
            EquipmentPiece equipment = new(id, Hero);
            return equipment;
        }
        private set
        {
            if (value is EquipmentPiece piece)
            {
                bool hasUpdated = Accessor.Update(Hero.PrimaryKeyValue,
                    new() { new(HeroColumn.Accessory, piece.ID) }) == 1;
                if (hasUpdated)
                {
                    Hero.Data = Accessor.GetMe(Hero.PrimaryKeyValue);
                }
            }
            else if (value is PendingEquipmentPiece pending)
            {
                pendingAccessory = pending;
            }
            NotifyPropertyChanged();
        }
    }
    public EquipmentPieceBase Armor
    {
        get
        {
            if (Convert.ToString(Hero.Data?[(int)HeroColumn.Armor] ?? string.Empty).Equals(string.Empty))
            {
                return pendingArmor;
            }

            string id = Convert.ToString(Hero.Data?[(int)HeroColumn.Armor]) ?? string.Empty;
            EquipmentPiece equipment = new(id, Hero);
            return equipment;
        }
        private set
        {
            if (value is EquipmentPiece piece)
            {
                bool hasUpdated = Accessor.Update(Hero.PrimaryKeyValue,
                    new() { new(HeroColumn.Armor, piece.ID) }) == 1;
                if (hasUpdated)
                {
                    Hero.Data = Accessor.GetMe(Hero.PrimaryKeyValue);
                }
            }
            else if (value is PendingEquipmentPiece pending)
            {
                pendingArmor = pending;
            }
            NotifyPropertyChanged();
        }
    }
    public EquipmentPieceBase Boots
    {
        get
        {
            if (Convert.ToString(Hero.Data?[(int)HeroColumn.Boots] ?? string.Empty).Equals(string.Empty))
            {
                return pendingBoots;
            }

            string id = Convert.ToString(Hero.Data?[(int)HeroColumn.Boots]) ?? string.Empty;
            EquipmentPiece equipment = new(id, Hero);
            return equipment;
        }
        private set
        {
            if (value is EquipmentPiece piece)
            {
                bool hasUpdated = Accessor.Update(Hero.PrimaryKeyValue,
                    new() { new(HeroColumn.Boots, piece.ID) }) == 1;
                if (hasUpdated)
                {
                    Hero.Data = Accessor.GetMe(Hero.PrimaryKeyValue);
                }
            }
            else if (value is PendingEquipmentPiece pending)
            {
                pendingBoots = pending;
            }
            NotifyPropertyChanged();
        }
    }
    public EquipmentPieceBase Gloves
    {
        get
        {
            if (Convert.ToString(Hero.Data?[(int)HeroColumn.Gloves] ?? string.Empty).Equals(string.Empty))
            {
                return pendingGloves;
            }

            string id = Convert.ToString(Hero.Data?[(int)HeroColumn.Gloves]) ?? string.Empty;
            EquipmentPiece equipment = new(id, Hero);
            return equipment;
        }
        private set
        {
            if (value is EquipmentPiece piece)
            {
                bool hasUpdated = Accessor.Update(Hero.PrimaryKeyValue,
                    new() { new(HeroColumn.Gloves, piece.ID) }) == 1;
                if (hasUpdated)
                {
                    Hero.Data = Accessor.GetMe(Hero.PrimaryKeyValue);
                }
            }
            else if (value is PendingEquipmentPiece pending)
            {
                pendingGloves = pending;
            }
            NotifyPropertyChanged();
        }
    }
    public EquipmentPieceBase Helmet
    {
        get
        {
            if (Convert.ToString(Hero.Data?[(int)HeroColumn.Helm] ?? string.Empty).Equals(string.Empty))
            {
                return pendingHelmet;
            }

            string id = Convert.ToString(Hero.Data?[(int)HeroColumn.Helm]) ?? string.Empty;
            EquipmentPiece equipment = new(id, Hero);
            return equipment;
        }
        private set
        {
            if (value is EquipmentPiece piece)
            {
                bool hasUpdated = Accessor.Update(Hero.PrimaryKeyValue,
                    new() { new(HeroColumn.Helm, piece.ID) }) == 1;
                if (hasUpdated)
                {
                    Hero.Data = Accessor.GetMe(Hero.PrimaryKeyValue);
                }
            }
            else if (value is PendingEquipmentPiece pending)
            {
                pendingHelmet = pending;
            }
            NotifyPropertyChanged();
        }
    }
    public EquipmentPieceBase Weapon
    {
        get
        {
            if (Convert.ToString(Hero.Data?[(int)HeroColumn.Weapon] ?? string.Empty).Equals(string.Empty))
            {
                return pendingWeapon;
            }

            string id = Convert.ToString(Hero.Data?[(int)HeroColumn.Weapon]) ?? string.Empty;
            EquipmentPiece equipment = new(id, Hero);
            return equipment;
        }
        private set
        {
            if (value is EquipmentPiece piece)
            {
                bool hasUpdated = Accessor.Update(Hero.PrimaryKeyValue,
                    new() { new(HeroColumn.Weapon, piece.ID) }) == 1;
                if (hasUpdated)
                {
                    Hero.Data = Accessor.GetMe(Hero.PrimaryKeyValue);
                }
            }
            else if (value is PendingEquipmentPiece pending)
            {
                pendingWeapon = pending;
            }
            NotifyPropertyChanged();
        }
    }

    private readonly HeroesAccessor Accessor = new();
    private readonly Hero Hero;
    private PendingEquipmentPiece? pendingAccessory;
    private PendingEquipmentPiece? pendingArmor;
    private PendingEquipmentPiece? pendingBoots;
    private PendingEquipmentPiece? pendingGloves;
    private PendingEquipmentPiece? pendingHelmet;
    private PendingEquipmentPiece? pendingWeapon;


    public Equipment(Hero hero)
    {
        Hero = hero;
        Initialize();
    }


    public void Equip(string id)
    {
        if (id == null || id.Length != 8) return;
        EquipmentPieceBase.EquipmentType type = Enum.GetValues<EquipmentPieceBase.EquipmentType>()
            .ToList().Find(val => val.IDPart().Equals(id[1]));

        EquipmentAccessor accessor = new();
        DataRow row = accessor.GetMe(id);

        switch (type)
        {
            case EquipmentPieceBase.EquipmentType.Accessory:
                Accessory = new EquipmentPiece(row, Hero);
                break;
            case EquipmentPieceBase.EquipmentType.Armor:
                Armor = new EquipmentPiece(row, Hero);
                break;
            case EquipmentPieceBase.EquipmentType.Boots:
                Boots = new EquipmentPiece(row, Hero);
                break;
            case EquipmentPieceBase.EquipmentType.Gloves:
                Gloves = new EquipmentPiece(row, Hero);
                break;
            case EquipmentPieceBase.EquipmentType.Helmet:
                Helmet = new EquipmentPiece(row, Hero);
                break;
            case EquipmentPieceBase.EquipmentType.Weapon:
                Weapon = new EquipmentPiece(row, Hero);
                break;
            default: break;
        }
    }
    private void Initialize()
    {
        pendingAccessory = new(EquipmentPieceBase.EquipmentType.Accessory, Hero);
        pendingArmor = new(EquipmentPieceBase.EquipmentType.Armor, Hero);
        pendingBoots = new(EquipmentPieceBase.EquipmentType.Boots, Hero);
        pendingGloves = new(EquipmentPieceBase.EquipmentType.Gloves, Hero);
        pendingHelmet = new(EquipmentPieceBase.EquipmentType.Helmet, Hero);
        pendingWeapon = new(EquipmentPieceBase.EquipmentType.Weapon, Hero);
    }
}
