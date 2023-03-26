using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeagueChampion.Data
{
    public class ChampionDBContext : DbContext
    {
        public DbSet<Champion> Champions { get; set; }
        public DbSet<Info> Infos { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Stats> Stats { get; set; }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=(local); Initial Catalog=ChampionDB; Integrated Security=True; TrustServerCertificate=True");

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Champion>(mb =>
            {
                mb.Property(champion => champion.Id);
                mb.Property(champion => champion.Version);
                mb.Property(champion => champion.RiotId);
                mb.Property(champion => champion.RiotKey);
                mb.Property(champion => champion.Name);
                mb.Property(champion => champion.Title);
                mb.Property(champion => champion.Blurb);

                mb.HasOne(champion => champion.Info)
                    .WithOne(info => info.Champion)
                    .HasForeignKey<Info>(champion => champion.ChampionId);

                mb.HasOne(champion => champion.Image)
                    .WithOne(image => image.Champion)
                    .HasForeignKey<Image>(champion => champion.ChampionId);

                mb.Property(champion => champion.Tag1);
                mb.Property(champion => champion.Tag2);
                mb.Property(champion => champion.Partype);

                mb.HasOne(champion => champion.Stats)
                    .WithOne(stats => stats.Champion)
                    .HasForeignKey<Stats>(champion => champion.ChampionId);


                mb.HasKey(champion =>champion.Id);
            });

            modelBuilder.Entity<Info>(mb =>
            {
                mb.Property(info => info.InfoId);
                mb.Property(info => info.Attack);
                mb.Property(info => info.Defence);
                mb.Property(info => info.Magic);
                mb.Property(info => info.Difficulty);

                mb.HasKey(info => info.InfoId);
            });

            modelBuilder.Entity<Image>(mb =>
            {
                mb.Property(image => image.ImageId);
                mb.Property(image => image.Full);
                mb.Property(image => image.Sprite);
                mb.Property(image => image.Group);
                mb.Property(image => image.X);
                mb.Property(image => image.Y);
                mb.Property(image => image.Width);
                mb.Property(image => image.Height);

                mb.HasKey(image => image.ImageId);
            });

            modelBuilder.Entity<Stats>(mb =>
            {
                mb.Property(stats  => stats.StatsId);
                mb.Property(stats => stats.Hp);
                mb.Property(stats => stats.HpPerLevel);
                mb.Property(stats => stats.Mp);
                mb.Property(stats => stats.MpPerLevel);
                mb.Property(stats => stats.MoveSpeed);
                mb.Property(stats => stats.Armour);
                mb.Property(stats => stats.ArmourPerLevel);
                mb.Property(stats => stats.SpellBlock);
                mb.Property(stats => stats.SpellBlockPerLevel);
                mb.Property(stats => stats.AttackRange);
                mb.Property(stats => stats.HpRegen);
                mb.Property(stats => stats.HpRegenPerLevel);
                mb.Property(stats => stats.MpRegen);
                mb.Property(stats => stats.MpRegenPerLevel);
                mb.Property(stats => stats.Crit);
                mb.Property(stats => stats.CritPerLevel);
                mb.Property(stats => stats.AttackDamage);
                mb.Property(stats => stats.AttackDamagePerLevel);
                mb.Property(stats => stats.AttackSpeedPerLevel);
                mb.Property(stats => stats.AttackSpeed);

                mb.HasKey(stats => stats.StatsId);
            });
        }
    }
}
