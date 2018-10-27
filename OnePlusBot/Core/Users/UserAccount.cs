﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnePlusBot.Core.UserAccounts
{
    public class UserAccount
    {
        public string Name { get; set; }

        public ulong ID { get; set; }

        public uint Points { get; set; }

        public uint XP { get; set; }

        public uint LevelNumber
        {
            get
            {
                return (uint)Math.Sqrt(XP / 50);
            }
        }

        public bool IsMuted { get; set; }

        public uint NumberOfWarnings { get; set; }
    }
}