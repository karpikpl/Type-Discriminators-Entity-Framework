using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

public class Plant
{
    public int Id { get; set; }
    public string Name { get; set; }
}

public class PlantConfiguration: IEntityTypeConfiguration<Plant>
{
    private readonly ModelBuilder _modelBuilder;
    
    public PlantConfiguration(ModelBuilder modelBuilder)
    {
        _modelBuilder = modelBuilder;
    }

    public void Configure(EntityTypeBuilder<Plant> builder)
    {
        ConfigureProperties(builder);
    }

    public static void ConfigureProperties(ModelBuilder modelBuilder)
    {
        var builder = modelBuilder.Entity<Plant>();

        ConfigureProperties(builder);        
    }

    public static void ConfigureProperties(EntityTypeBuilder<Plant> builder)
    {
        builder.ToTable("PlantX");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasColumnName("PlantId");
        builder.Property(e => e.Name).HasColumnName("PlantName").IsRequired().HasMaxLength(64);
    }
}