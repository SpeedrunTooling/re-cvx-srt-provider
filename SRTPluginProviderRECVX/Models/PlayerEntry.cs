﻿using SRTPluginProviderRECVX.Enumerations;
using System;
using System.Diagnostics;

namespace SRTPluginProviderRECVX.Models
{
    [DebuggerDisplay("{_DebuggerDisplay,nq}")]
    public class PlayerEntry : BaseNotifyModel
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public string _DebuggerDisplay
        {
            get
            {
                if (IsAlive)
                    return String.Format("{0} {1} / {2} ({3:P1})", CharacterName, CurrentHP, MaximumHP, Percentage);
                else
                    return String.Format("{0} DEAD / DEAD (0%)", CharacterName);
            }
        }

        private CharacterEnumeration _character = CharacterEnumeration.Claire;
        public CharacterEnumeration Character
        {
            get => _character;
            set
            {
                if (_character != value)
                {
                    _character = value;
                    OnPropertyChanged();
                    OnPropertyChanged("CharacterName");
                    OnPropertyChanged("CharacterFirstName");
                }
            }
        }

        public InventoryEntry Equipment { get; } = new InventoryEntry(0);

        private InventoryEntry[] _inventory = new InventoryEntry[11];
        public InventoryEntry[] Inventory
        {
            get
            {
                if (_inventory[0] == null)
                {
                    for (int i = 0; i < _inventory.Length; i++)
                        _inventory[i] = new InventoryEntry(i);
                    OnPropertyChanged();
                }

                return _inventory;
            }
        }

        private int _maximumHP;
        public int MaximumHP
        {
            get => _maximumHP;
            set
            {
                if (_maximumHP != value)
                {
                    _maximumHP = value;
                    OnPropertyChanged();
                    OnPropertyChanged("Percentage");
                }
            }
        }

        private int _currentHP;
        public int CurrentHP
        {
            get => _currentHP;
            set
            {
                if (_currentHP != value)
                {
                    _currentHP = value;
                    OnPropertyChanged();
                    OnPropertyChanged("IsAlive");
                    OnPropertyChanged("IsFine");
                    OnPropertyChanged("IsCautionYellow");
                    OnPropertyChanged("IsCautionOrange");
                    OnPropertyChanged("IsDanger");
                    OnPropertyChanged("DisplayHP");
                    OnPropertyChanged("Percentage");
                    OnPropertyChanged("StatusName");
                }
            }
        }

        public int DisplayHP
            => Math.Max(CurrentHP, 0);

        public float Percentage
            => IsAlive ? (float)DisplayHP / (float)MaximumHP : 0f;

        private byte _status;
        public byte Status
        {
            get => _status;
            set
            {
                if (_status != value)
                {
                    _status = value;
                    OnPropertyChanged();
                    OnPropertyChanged("IsPoison");
                    OnPropertyChanged("IsGassed");
                    OnPropertyChanged("StatusName");
                }
            }
        }

        public bool IsPoison
            => (Status & 0x08) != 0;

        public bool IsGassed
            => (Status & 0x20) != 0;

        public bool IsAlive
            => CurrentHP >= 0;

        public bool IsFine
            => CurrentHP >= 120;

        public bool IsCautionYellow
            => CurrentHP < 120 && CurrentHP >= 60;

        public bool IsCautionOrange
            => CurrentHP < 60 && CurrentHP >= 30;

        public bool IsDanger
            => CurrentHP < 30;

        // S rank requirments
        private int _retry;
        public int Retry
        {
            get => _retry;
            set
            {
                if (_retry != value)
                {
                    _retry = value;
                    OnPropertyChanged();
                }
            }
        }

        private int _saves;
        public int Saves
        {
            get => _saves;
            set
            {
                if (_saves != value)
                {
                    _saves = value;
                    OnPropertyChanged();
                }
            }
        }

        private int _fas;
        public int FAS
        {
            get => _fas;
            set
            {
                if (_fas != value)
                {
                    _fas = value;
                    OnPropertyChanged();
                }
            }
        }

        public int[] Map { get; } = new int[3];

        private int _steve;
        public int Steve
        {
            get => _steve;
            set
            {
                if (_steve != value)
                {
                    _steve = value;
                    OnPropertyChanged();
                }
            }
        }

        private int _rodrigo;
        public int Rodrigo
        {
            get => _rodrigo;
            set
            {
                if (_rodrigo != value)
                {
                    _rodrigo = value;
                    OnPropertyChanged();
                }
            }
        }

        public string CharacterName
        {
            get
            {
                switch (Character)
                {
                    case CharacterEnumeration.Chris:
                        return "Chris Redfield";
                    case CharacterEnumeration.Steve:
                        return "Steve Burnside";
                    case CharacterEnumeration.Wesker:
                        return "Albert Wesker";
                    default:
                        return "Claire Redfield";
                }
            }
        }

        public string CharacterFirstName
        {
            get
            {
                switch (Character)
                {
                    case CharacterEnumeration.Chris:
                        return "Chris";
                    case CharacterEnumeration.Steve:
                        return "Steve";
                    case CharacterEnumeration.Wesker:
                        return "Albert";
                    default:
                        return "Claire";
                }
            }
        }

        public string StatusName
        {
            get
            {
                if (!IsAlive)
                    return "Dead";
                else if (IsGassed)
                    return "Gassed";
                else if (IsPoison)
                    return "Poison";
                else if (IsDanger)
                    return "Danger";
                else if (IsCautionOrange)
                    return "Caution";
                else if (IsCautionYellow)
                    return "Caution";
                else
                    return "Fine";
            }
        }
    }
}