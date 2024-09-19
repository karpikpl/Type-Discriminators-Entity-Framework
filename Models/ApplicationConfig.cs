using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

public class ApplicationConfig
{
    public int Id { get; set; }
    public string Key { get; set; }
    public string Value { get; set; }
    public string Comment { get; set; }
    public int Environment { get; set; }

    public int PlantId { get; set; }
    public virtual Plant Plant { get; set; }
}

public class ApplicationConfigConfiguration: IEntityTypeConfiguration<ApplicationConfig>
{
    private readonly ModelBuilder _modelBuilder;
    
    public ApplicationConfigConfiguration(ModelBuilder modelBuilder)
    {
        _modelBuilder = modelBuilder;
    }

    public void Configure(EntityTypeBuilder<ApplicationConfig> builder)
    {
        ConfigureProperties(builder);
    }

    public static void ConfigureProperties(ModelBuilder modelBuilder)
    {
        var builder = modelBuilder.Entity<ApplicationConfig>();

        ConfigureProperties(builder);        
    }

    public static void ConfigureProperties(EntityTypeBuilder<ApplicationConfig> builder)
    {
        builder.ToTable("ApplicationConfigCustomName");
        builder.Property(x => x.Id).HasColumnName("ApplicationConfigId");
        builder.Property(e => e.Key).HasColumnName("ApplicationConfigKey").IsRequired().HasMaxLength(64);
        builder.Property(e => e.Value).HasColumnName("ApplicationConfigValue").IsRequired().HasMaxLength(2048);
        builder.Property(e => e.Comment).HasColumnName("CommentTx").HasMaxLength(64).IsRequired();
        builder.Property(e => e.Environment).IsRequired();
        builder.HasOne(e => e.Plant).WithMany().HasForeignKey(e => e.PlantId);
    }
}