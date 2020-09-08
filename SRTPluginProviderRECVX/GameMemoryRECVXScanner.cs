﻿using SRTPluginProviderRECVX.Enumerations;
using SRTPluginProviderRECVX.Models;
using SRTPluginProviderRECVX.Utilities;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace SRTPluginProviderRECVX
{
    public class GameMemoryRECVXScanner : IDisposable
    {
        public GameMemoryRECVX Memory { get; private set; } = new GameMemoryRECVX();
        public GamePointers Pointers { get; private set; } = new GamePointers();
        public GameEmulator Emulator { get; private set; }

        private Process Process { get; set; }
        public bool ProcessRunning => Process != null && Process.IsRunning();
        public int ProcessExitCode => Process != null ? Process.ExitCode() : 0;

        public bool HasScanned { get; private set; }

        public GameMemoryRECVXScanner(GameEmulator emulator) =>
            Initialize(emulator);

        public void Initialize(GameEmulator emulator)
        {
            if ((Emulator = emulator) == null)
                return;

            Process = Emulator.Process;

            if (ProcessRunning)
                UpdatePointerAddresses();
        }

        public void UpdateGameVersion() =>
            Memory.Version = new GameVersion(Process.ReadString(Emulator.ProductPointer, Emulator.ProductLength));

        public void UpdatePointerAddresses()
        {
            UpdateGameVersion();

            switch (Memory.Version.Code)
            {
                case GameVersion.SLPM_65022:
                    Pointers.Time = IntPtr.Add(Emulator.VirtualMemoryPointer, 0x004314A0);
                    Pointers.Room = IntPtr.Add(Emulator.VirtualMemoryPointer, 0x004314B4);
                    Pointers.Status = IntPtr.Add(Emulator.VirtualMemoryPointer, 0x0042FE6A);
                    Pointers.Health = IntPtr.Add(Emulator.VirtualMemoryPointer, 0x004301FC);
                    Pointers.Character = IntPtr.Add(Emulator.VirtualMemoryPointer, 0x00430C84);
                    Pointers.Inventory = IntPtr.Add(Emulator.VirtualMemoryPointer, 0x00430E70);
                    Pointers.Enemy = IntPtr.Add(Emulator.VirtualMemoryPointer, 0x00403DE0);
                    Pointers.EnemyCount = IntPtr.Add(Emulator.VirtualMemoryPointer, 0x0124CD88);
                    Pointers.Difficulty = IntPtr.Add(Emulator.VirtualMemoryPointer, 0x00430C8C);
                    Pointers.Saves = IntPtr.Add(Emulator.VirtualMemoryPointer, 0x00430C80);
                    Pointers.Retry = IntPtr.Add(Emulator.VirtualMemoryPointer, 0x004314AA);
                    Pointers.RDXHeader = IntPtr.Add(Emulator.VirtualMemoryPointer, 0x0124CC80);
                    break;

                case GameVersion.SLUS_20184:
                    Pointers.Time = IntPtr.Add(Emulator.VirtualMemoryPointer, 0x004339A0);
                    Pointers.Room = IntPtr.Add(Emulator.VirtualMemoryPointer, 0x004339B4);
                    Pointers.Status = IntPtr.Add(Emulator.VirtualMemoryPointer, 0x0043236A);
                    Pointers.Health = IntPtr.Add(Emulator.VirtualMemoryPointer, 0x004326FC);
                    Pointers.Character = IntPtr.Add(Emulator.VirtualMemoryPointer, 0x00433184);
                    Pointers.Inventory = IntPtr.Add(Emulator.VirtualMemoryPointer, 0x00433370);
                    Pointers.Enemy = IntPtr.Add(Emulator.VirtualMemoryPointer, 0x004062E0);
                    Pointers.EnemyCount = IntPtr.Add(Emulator.VirtualMemoryPointer, 0x012503C8);
                    Pointers.Difficulty = IntPtr.Add(Emulator.VirtualMemoryPointer, 0x0043318C);
                    Pointers.Saves = IntPtr.Add(Emulator.VirtualMemoryPointer, 0x00433180);
                    Pointers.Retry = IntPtr.Add(Emulator.VirtualMemoryPointer, 0x004339AA);
                    Pointers.RDXHeader = IntPtr.Add(Emulator.VirtualMemoryPointer, 0x012502C0);
                    break;

                case GameVersion.SLES_50306:
                    Pointers.Time = IntPtr.Add(Emulator.VirtualMemoryPointer, 0x0044A1D0);
                    Pointers.Room = IntPtr.Add(Emulator.VirtualMemoryPointer, 0x0044A1E4);
                    Pointers.Status = IntPtr.Add(Emulator.VirtualMemoryPointer, 0x00448B9A);
                    Pointers.Health = IntPtr.Add(Emulator.VirtualMemoryPointer, 0x00448F2C);
                    Pointers.Character = IntPtr.Add(Emulator.VirtualMemoryPointer, 0x004499B4);
                    Pointers.Inventory = IntPtr.Add(Emulator.VirtualMemoryPointer, 0x00449BA0);
                    Pointers.Enemy = IntPtr.Add(Emulator.VirtualMemoryPointer, 0x0041CB10);
                    Pointers.EnemyCount = IntPtr.Add(Emulator.VirtualMemoryPointer, 0x01270688);
                    Pointers.Difficulty = IntPtr.Add(Emulator.VirtualMemoryPointer, 0x004499BC);
                    Pointers.Saves = IntPtr.Add(Emulator.VirtualMemoryPointer, 0x004499B0);
                    Pointers.Retry = IntPtr.Add(Emulator.VirtualMemoryPointer, 0x0044A1DA);
                    Pointers.RDXHeader = IntPtr.Add(Emulator.VirtualMemoryPointer, 0x01270580);
                    break;

                case GameVersion.NPUB30467:
                    Pointers.Time = IntPtr.Add(Emulator.VirtualMemoryPointer, 0x00BB3DB8);
                    Pointers.Room = IntPtr.Add(Emulator.VirtualMemoryPointer, 0x00BB3DCC);
                    Pointers.Status = IntPtr.Add(Emulator.VirtualMemoryPointer, 0x00BDE689);
                    Pointers.Health = IntPtr.Add(Emulator.VirtualMemoryPointer, 0x00BDEA1C);
                    Pointers.Character = IntPtr.Add(Emulator.VirtualMemoryPointer, 0x00BB359C);
                    Pointers.Inventory = IntPtr.Add(Emulator.VirtualMemoryPointer, 0x00BB3788);
                    Pointers.Enemy = IntPtr.Add(Emulator.VirtualMemoryPointer, 0x00BDEB78);
                    Pointers.EnemyCount = IntPtr.Add(Emulator.VirtualMemoryPointer, 0x00BDE298);
                    Pointers.Difficulty = IntPtr.Add(Emulator.VirtualMemoryPointer, 0x00BB36A4);
                    Pointers.Saves = IntPtr.Add(Emulator.VirtualMemoryPointer, 0x00BB3598);
                    Pointers.Retry = IntPtr.Add(Emulator.VirtualMemoryPointer, 0x00BB3DC2);
                    Pointers.RDXHeader = IntPtr.Add(Emulator.VirtualMemoryPointer, 0x00BDE0EC);
                    break;

                case GameVersion.NPEB00553:
                    Pointers.Time = IntPtr.Add(Emulator.VirtualMemoryPointer, 0x00BC40B8);
                    Pointers.Room = IntPtr.Add(Emulator.VirtualMemoryPointer, 0x00BC40CC);
                    Pointers.Status = IntPtr.Add(Emulator.VirtualMemoryPointer, 0x00BEE989);
                    Pointers.Health = IntPtr.Add(Emulator.VirtualMemoryPointer, 0x00BEED1C);
                    Pointers.Character = IntPtr.Add(Emulator.VirtualMemoryPointer, 0x00BC389C);
                    Pointers.Inventory = IntPtr.Add(Emulator.VirtualMemoryPointer, 0x00BC3A88);
                    Pointers.Enemy = IntPtr.Add(Emulator.VirtualMemoryPointer, 0x00BEEE78);
                    Pointers.EnemyCount = IntPtr.Add(Emulator.VirtualMemoryPointer, 0x00BEE598);
                    Pointers.Difficulty = IntPtr.Add(Emulator.VirtualMemoryPointer, 0x00BC38A4);
                    Pointers.Saves = IntPtr.Add(Emulator.VirtualMemoryPointer, 0x00BC3898);
                    Pointers.Retry = IntPtr.Add(Emulator.VirtualMemoryPointer, 0x00BC40C2);
                    Pointers.RDXHeader = IntPtr.Add(Emulator.VirtualMemoryPointer, 0x00BEE3EC);
                    break;

                default: // GameVersion.NPJB00135
                    Pointers.Time = IntPtr.Add(Emulator.VirtualMemoryPointer, 0x00BB3E38);
                    Pointers.Room = IntPtr.Add(Emulator.VirtualMemoryPointer, 0x00BB3E4C);
                    Pointers.Status = IntPtr.Add(Emulator.VirtualMemoryPointer, 0x00BDE709);
                    Pointers.Health = IntPtr.Add(Emulator.VirtualMemoryPointer, 0x00BDEA9C);
                    Pointers.Character = IntPtr.Add(Emulator.VirtualMemoryPointer, 0x00BB361C);
                    Pointers.Inventory = IntPtr.Add(Emulator.VirtualMemoryPointer, 0x00BB3808);
                    Pointers.Enemy = IntPtr.Add(Emulator.VirtualMemoryPointer, 0x00BDEBF8);
                    Pointers.EnemyCount = IntPtr.Add(Emulator.VirtualMemoryPointer, 0x00BDE318);
                    Pointers.Difficulty = IntPtr.Add(Emulator.VirtualMemoryPointer, 0x00BB3624);
                    Pointers.Saves = IntPtr.Add(Emulator.VirtualMemoryPointer, 0x00BB3618);
                    Pointers.Retry = IntPtr.Add(Emulator.VirtualMemoryPointer, 0x00BB3E42);
                    Pointers.RDXHeader = IntPtr.Add(Emulator.VirtualMemoryPointer, 0x00BDE16C);
                    break;
            }
        }

        public IGameMemoryRECVX Refresh()
        {
            Memory.Difficulty = GetDifficulty(Process.ReadValue<byte>(Pointers.Difficulty));

            Memory.IGT.RunningTimer = Process.ReadValue<int>(Pointers.Time, Emulator.IsBigEndian);

            Memory.Room.Id = Process.ReadValue<short>(Pointers.Room, true); // Room Id bytes are always swapped!
            Memory.Room.IsLoaded = Process.ReadValue<int>(Pointers.RDXHeader, true) == GetRDXHeader();

            Memory.Player.Character = GetCharacter(Process.ReadValue<byte>(Pointers.Character));
            Memory.Player.CurrentHP = Process.ReadValue<int>(Pointers.Health, Emulator.IsBigEndian);
            Memory.Player.Status = Process.ReadValue<byte>(Pointers.Status);
            Memory.Player.Saves = Process.ReadValue<int>(Pointers.Saves, Emulator.IsBigEndian);
            Memory.Player.Retry = Process.ReadValue<short>(Pointers.Retry, Emulator.IsBigEndian);

            if (Memory.Version.Country == CountryEnumeration.JP)
                Memory.Player.MaximumHP = Memory.Difficulty == DifficultyEnumeration.VeryEasy ? 400 : 200;
            else
                Memory.Player.MaximumHP = 160;

            RefreshInventory();
            RefreshEnemy();

            HasScanned = true;

            return Memory;
        }

        public void RefreshInventory()
        {
            InventoryEntry[] inventory = new InventoryEntry[12];

            IntPtr pointer = IntPtr.Add(Pointers.Inventory, (int)Memory.Player.Character * 0x40);

            int index = 0;
            int equip = 0;

            for (int i = 0; i < 12; ++i)
            {
                byte[] data = Process.ReadBytes(pointer, 4, Emulator.IsBigEndian);

                if (i <= 0)
                    equip = BitConverter.ToInt32(data, 0);
                else
                {
                    inventory[++index] = new InventoryEntry(index, data, equip == index);

                    if (inventory[index].IsEquipped)
                        inventory[0] = new InventoryEntry(0, data);
                }

                pointer = IntPtr.Add(pointer, 0x4);
            }

            if (inventory[0] == null)
                inventory[0] = new InventoryEntry(0);

            Memory.Player.Inventory = inventory;
        }

        public void RefreshEnemy()
        {
            List<EnemyEntry> enemy = new List<EnemyEntry>();

            if (!Memory.Room.IsLoaded)
            {
                Memory.Enemy = enemy;
                return;
            }

            IntPtr pointer = new IntPtr(Pointers.Enemy.ToInt64());
            int count = Process.ReadValue<int>(Pointers.EnemyCount, Emulator.IsBigEndian);

            int entryOffset = Memory.Version.Console == ConsoleEnumeration.PS2 ? 0x0580 : 0x0578;
            int modelOffset = Memory.Version.Console == ConsoleEnumeration.PS2 ? 0x008B : 0x0088;

            for (int i = 0; i < count; ++i)
            {
                EnemyEnumeration type = GetEnemyType(Process.ReadValue<short>(IntPtr.Add(pointer, 0x0004), Emulator.IsBigEndian));

                if (type == EnemyEnumeration.Unknown)
                {
                    EnemyEntry entry = new EnemyEntry(type);

                    entry.Slot = Process.ReadValue<int>(IntPtr.Add(pointer, 0x039C), Emulator.IsBigEndian);
                    entry.Damage = Process.ReadValue<int>(IntPtr.Add(pointer, 0x0574), Emulator.IsBigEndian);
                    entry.CurrentHP = Process.ReadValue<int>(IntPtr.Add(pointer, 0x041C), Emulator.IsBigEndian);

                    // Not sure what to call these values. They are useful to help determine enemy life status.
                    entry.Action = Process.ReadValue<byte>(IntPtr.Add(pointer, 0x000C));
                    entry.Status = Process.ReadValue<byte>(IntPtr.Add(pointer, 0x000F));
                    entry.Model = Process.ReadValue<byte>(IntPtr.Add(pointer, modelOffset));

                    switch (entry.Type)
                    {
                        case EnemyEnumeration.Tenticle:
                            entry.MaximumHP = 160;
                            entry.IsAlive = entry.Active && entry.CurrentHP >= 0 && entry.Model == 0 && Memory.Room.Id != 0x091E;
                            break;

                        case EnemyEnumeration.GlupWorm:
                            entry.MaximumHP = 300;
                            entry.IsAlive = entry.Active && entry.Status > 0;
                            break;

                        case EnemyEnumeration.AnatomistZombie:
                            entry.MaximumHP = 200;
                            entry.IsAlive = entry.Active && entry.Status > 0;
                            break;

                        case EnemyEnumeration.Tyrant:
                            entry.MaximumHP = Memory.Room.Id == 0x0501 ? 700 : 500;
                            entry.IsAlive = entry.Active && entry.CurrentHP >= 0;
                            break;

                        case EnemyEnumeration.Nosferatu:
                            entry.MaximumHP = 600;
                            entry.IsAlive = entry.Active && entry.CurrentHP == 0;
                            break;

                        case EnemyEnumeration.AlbinoidAdult:
                        case EnemyEnumeration.MutatedSteve:
                            entry.MaximumHP = 250;
                            entry.IsAlive = entry.Active && entry.CurrentHP == 0;
                            break;

                        case EnemyEnumeration.GiantBlackWidow:
                            entry.MaximumHP = 250;
                            entry.IsAlive = entry.Active && entry.CurrentHP > 0;
                            break;

                        case EnemyEnumeration.AlexiaAshford:
                            entry.MaximumHP = 300;
                            entry.IsAlive = entry.Active && Memory.Room.Id != 0x091E;
                            break;

                        case EnemyEnumeration.AlexiaAshfordB:
                            entry.MaximumHP = 700;
                            entry.IsAlive = entry.Active;
                            break;

                        case EnemyEnumeration.AlexiaAshfordC:
                            entry.MaximumHP = 400;
                            entry.IsAlive = !Memory.Enemy[0].IsAlive;
                            break;

                        case EnemyEnumeration.AlexiaBaby:
                            entry.IsAlive = Memory.Enemy[0].IsAlive;
                            break;

                        case EnemyEnumeration.Hunter:
                            entry.IsAlive = entry.Active && entry.Model == 0;
                            break;

                        default:
                            entry.IsAlive = entry.Active && entry.Status > 0;
                            break;
                    }

                    enemy.Add(entry);
                }

                pointer = IntPtr.Add(pointer, entryOffset);
            }

            Memory.Enemy = enemy;
        }

        private DifficultyEnumeration GetDifficulty(byte data)
        {
            if (Enum.IsDefined(typeof(DifficultyEnumeration), (DifficultyEnumeration)data))
                return (DifficultyEnumeration)data;
            return DifficultyEnumeration.Normal;
        }

        private CharacterEnumeration GetCharacter(byte data)
        {
            if (Enum.IsDefined(typeof(CharacterEnumeration), (CharacterEnumeration)data))
                return (CharacterEnumeration)data;
            return CharacterEnumeration.Claire;
        }

        private EnemyEnumeration GetEnemyType(short data)
        {
            if (Enum.IsDefined(typeof(EnemyEnumeration), (EnemyEnumeration)data))
                return (EnemyEnumeration)data;
            return EnemyEnumeration.Unknown;
        }

        private int GetRDXHeader()
        {
            if (Memory.Version.Console == ConsoleEnumeration.PS3)
            {
                switch (Memory.Player.Character)
                {
                    case CharacterEnumeration.Chris:
                        return 0x0003C4FC;
                    case CharacterEnumeration.Steve:
                        return 0x0003A4D0;
                    case CharacterEnumeration.Wesker:
                        return 0x0003E814;
                    default:
                        return 0x0003D650;
                }
            }

            return 0x00002041; // PS2
        }

        #region IDisposable Support
        private bool disposedValue;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~GameMemoryRECVXScanner()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
        #endregion IDisposable Support
    }
}