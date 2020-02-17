using Microsoft.EntityFrameworkCore;
using NSI.DAL.Entities;
using Microsoft.Extensions.Configuration;

namespace NSI.DAL
{
    public class AvailabilityLeaveContext: DbContext
    {
        private readonly IConfiguration configuration;

        public AvailabilityLeaveContext(DbContextOptions<AvailabilityLeaveContext> options, IConfiguration configuration) : base(options)
        {
            base.Database.Migrate();
            this.configuration = configuration;
        }
        //entities
        public DbSet<ResourceSchedule> ResourceSchedules { get; set; }
        public DbSet<EventType> EventTypes { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<LeaveBalance> LeaveBalances { get; set; }
        public DbSet<LeaveTransaction> LeaveTransactions { get; set; }
        public DbSet<AvailabilityRule> AvailabilityRules { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Event>(mb => {
                mb.HasAlternateKey(_event => _event.Name);
                mb.ToTable("Event");
            });
            modelBuilder.Entity<EventType>(mb => {
                mb.HasAlternateKey(eventType => eventType.Name);
                mb.ToTable("EventType");
            });
            modelBuilder.Entity<ResourceSchedule>()
                .ToTable("ResourceSchedule");
            modelBuilder.Entity<AvailabilityRule>()
                .ToTable("AvailabilityRule");
            modelBuilder.Entity<LeaveBalance>()
                .ToTable("LeaveBalance");
            modelBuilder.Entity<LeaveTransaction>()
                .ToTable("LeaveTransaction");
            // modelBuilder.Entity<ResourceAvailabilityRule>()
            //     .ToTable("ResourceAvailabilityRule");

            // if(configuration == null){
            //     System.Console.WriteLine("NULL JE BOZE DRAGI.");
            // } else {
            //     System.Console.WriteLine("HVALA BOGOVIMA");
            // }

            // Load Event Types from Configuration
            // string[] eventTypesString = configuration.GetSection("EventTypes").Get<string[]>();
            // EventType[] eventTypes = eventTypesString.Select((item, index) => new EventType {ID = index + 1, Name = item}).ToArray();
            // for(int i = 0; i < eventTypes.Length; i++){
            //     System.Console.WriteLine(eventTypes[i].ID);
            //     System.Console.WriteLine(eventTypes[i].Name);
            // }

            // modelBuilder.Entity<EventType>().HasData(eventTypes);
            
        }
    }
}

