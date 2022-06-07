using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ShortUrlDemo.Models
{
    /// <summary>
    /// 上下文
    /// </summary>
    public class dbContext : DbContext
    {
        public dbContext(DbContextOptions<dbContext> options) : base(options)
        {

        }

        public virtual DbSet<Short> Short { get; set; }

    }
}
