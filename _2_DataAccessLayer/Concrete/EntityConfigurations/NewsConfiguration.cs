using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2_DataAccessLayer.Concrete.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace _2_DataAccessLayer.Concrete.EntityConfigurations
{
    public class NewsConfiguration : IEntityTypeConfiguration<TrendingPost>
    {
        public void Configure(EntityTypeBuilder<TrendingPost> builder)
        {
            // Configuring the primary key
            builder.HasKey(news => news.TrendingPostId);
        }
    }
}
