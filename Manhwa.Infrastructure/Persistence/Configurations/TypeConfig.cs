using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Manhwa.Domain.Entities;

namespace Manhwa.Infrastructure.Persistence.Configurations
{
    public class TypeConfig : IEntityTypeConfiguration<Domain.Entities.Type>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.Type> e)
        {
            e.ToTable("story_types");

            e.HasKey(x => x.TypeId);

            e.HasMany(x => x.Stories)
                .WithOne(x => x.Type)
                .HasForeignKey(x => x.TypeId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
