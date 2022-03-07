// This file is part of Agile Player
// An Audio player with downsampler, upsampler and bit-converter
// written in C#.
// 
// Copyright © Alaa Ibrahim Hadid 2022
//
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.
// 
// Author email: mailto:alaahadidfreeware@gmail.com
//
using System;
using System.Collections.Generic;
using SlimDX.DirectInput;
namespace APlayer
{
    internal class ShortcutsManager
    {
        public ShortcutsManager()
        {
            Shortcuts = new List<ShortcutItem>();
        }
        private Keyboard keyboard;
        private KeyboardState state;
        private int timerCounter = 0;
        private const int timerReload = 15;
        private Key[][] input_keys;
        private bool IsShortcutsMapLoaded;

        public List<ShortcutItem> Shortcuts { get; private set; }

        public void Initialize(IntPtr window_handle)
        {
            input_keys = new Key[0][];

            input_keys = new Key[Shortcuts.Count][];
            for (int i = 0; i < Shortcuts.Count; i++)
            {
                string[] kkkk = Shortcuts[i].ShortcutKey.Split(new char[] { '+' });
                input_keys[i] = new Key[kkkk.Length];
                for (int k = 0; k < kkkk.Length; k++)
                {
                    if (kkkk[k] != "")
                        input_keys[i][k] = ((SlimDX.DirectInput.Key)Enum.Parse(typeof(SlimDX.DirectInput.Key), kkkk[k]));
                    else
                        input_keys[i][k] = Key.Unknown;
                }
            }

            DirectInput di = new DirectInput();
            keyboard = new Keyboard(di);
            keyboard.SetCooperativeLevel(window_handle, CooperativeLevel.Nonexclusive | CooperativeLevel.Foreground);
            IsShortcutsMapLoaded = true;
        }
        /// <summary>
        /// Should be called on timer
        /// </summary>
        public void CheckShortcuts()
        {
            if (!IsShortcutsMapLoaded)
                return;
            if (timerCounter > 0)
            { timerCounter--; return; }
            if (keyboard.Acquire().IsSuccess)
            {
                state = keyboard.GetCurrentState();
                for (int i = 0; i < input_keys.Length; i++)
                {
                    ShortcutItem sss = Shortcuts[i];
                    string[] kkkk = Shortcuts[i].ShortcutKey.Split(new char[] { '+' });
                    int accessed = 0;
                    for (int j = 0; j < input_keys[i].Length; j++)
                    {
                        if (state.IsPressed(input_keys[i][j]))
                        {
                            accessed++;
                        }
                    }
                    if (accessed == kkkk.Length)
                    {
                        timerCounter = timerReload;

                        if (sss.ShortcutMethod != null)
                            sss.ShortcutMethod();

                        break;
                    }
                }
            }
        }
    }
}
