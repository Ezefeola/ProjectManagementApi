﻿using Core.Domain.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Adapter.SqlServer.Data.Configurations;
public abstract class EntityTypeBaseConfiguration<T> : IEntityTypeConfiguration<T> where T : class
{
    public void Configure(EntityTypeBuilder<T> builder)
    {
        ConfigurateProperties(builder);
        ConfigurateConstraints(builder);
    }
    protected abstract void ConfigurateProperties(EntityTypeBuilder<T> builder);
    protected abstract void ConfigurateConstraints(EntityTypeBuilder<T> builder);
}

public static class BaseEntityConfig
{
    public static void ApplyTo<T, TId>(EntityTypeBuilder<T> builder)
        where T : Entity<TId>
        where TId : notnull
    {
        builder.Property(e => e.CreatedAt)
               .HasColumnType("datetime")
               .HasDefaultValueSql("NOW()")
               .ValueGeneratedOnAdd();

        builder.Property(e => e.UpdatedAt)
               .HasColumnType("datetime")
               .ValueGeneratedOnUpdate();

        builder.Property(e => e.DeletedAt)
               .HasColumnType("datetime");

        builder.HasQueryFilter(e => !e.IsDeleted);

        builder.HasIndex(e => e.IsDeleted)
               .HasFilter("IsDeleted = 0");
    }
}