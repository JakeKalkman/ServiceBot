using Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class ServiceBotContext : DbContext
    {
        public DbSet<Service> Service => Set<Service>();
        public DbSet<ServiceUpcharge> ServiceUpcharge => Set<ServiceUpcharge>();
        public DbSet<SkillingMethod> SkillingMethod => Set<SkillingMethod>();
        public DbSet<SkillingMethodUpcharge> SkillingMethodUpcharge => Set<SkillingMethodUpcharge>();
        public DbSet<Settings> Settings => Set<Settings>();
        public DbSet<Wallet> Wallet => Set<Wallet>();
        public DbSet<WalletTransaction> WalletTransaction => Set<WalletTransaction>();

        public Task FirstOrDefaultAsync { get; internal set; }

        public ServiceBotContext(DbContextOptions<ServiceBotContext> options) : base(options)
        {
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source =.\\LOCALDB;Initial Catalog=ServiceBot;User ID=sa;Password=root; MultipleActiveResultSets=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Service
            modelBuilder.Entity<Service>()
                .HasMany(x => x.ServiceUpcharges)
                .WithOne(x => x.Service)
                .HasForeignKey(x => x.ServiceId);

            //ServiceUpcharge
            modelBuilder.Entity<ServiceUpcharge>()
                .HasOne(x => x.Service)
                .WithMany(x => x.ServiceUpcharges);

            //SkillingMethod

            modelBuilder.Entity<SkillingMethod>()
                .HasMany(x => x.SkillingMethodUpcharges)
                .WithOne(x => x.SkillingMethod)
                .HasForeignKey(x => x.SkillingMethodId);

            //Wallet / Wallet Transactions

            modelBuilder.Entity<WalletTransaction>()
                .HasOne(x => x.SenderWallet)
                .WithMany(x => x.SentTransactions)
                .HasForeignKey(x => x.SendersWalletId);

            modelBuilder.Entity<WalletTransaction>()
                .HasOne(x => x.RecieversWallet)
                .WithMany(x => x.RecievedTransactions)
                .HasForeignKey(x => x.RecieversWalletId);

            ////SkillingMethodUpcharge
            //modelBuilder.Entity<SkillingMethodUpcharge>()
            //    .HasOne(x => x.SkillingMethod)
            //    .WithMany(x => x.SkillingMethodUpcharges);

        }

    }
}