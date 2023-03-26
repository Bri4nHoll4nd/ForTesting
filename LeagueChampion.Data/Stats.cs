﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeagueChampion.Data
{
    public class Stats
    {
        public int StatsId { get; set; }
        public int Hp { get; set; }
        public int HpPerLevel { get; set; }
        public int Mp { get; set; }
        public int MpPerLevel { get; set; }
        public int MoveSpeed { get; set; }
        public int Armour { get; set; }
        public int ArmourPerLevel { get; set; }
        public int SpellBlock { get; set; }
        public int SpellBlockPerLevel { get; set; }
        public int AttackRange { get; set; }
        public int HpRegen { get; set; }
        public int HpRegenPerLevel { get; set; }
        public int MpRegen { get; set; }
        public int MpRegenPerLevel { get; set; }
        public int Crit { get; set; }
        public int CritPerLevel { get; set; }
        public int AttackDamage { get; set; }
        public int AttackDamagePerLevel { get; set; }
        public int AttackSpeedPerLevel { get; set; }
        public int AttackSpeed { get; set; }

        public Guid ChampionId { get; set; }
        public Champion Champion { get; set; }
    }
}