using Assignment_DataAccesslayer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_DataAccesslayer.Data.Configrations
{
    public class EmployeeConfigrations : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.Property(D => D.Id).UseIdentityColumn(1, 1);
            builder.Property(E=>E.Salary).HasColumnType("decimal");

            builder.HasOne(E=>E.Department).WithMany(E=>E.Employees).HasForeignKey(E=>E.DepartmentId)
                    .OnDelete(DeleteBehavior.SetNull);       //keep it null  or   OnDelete(DeleteBehavior.cascade) //Delete both

        }
    }
}
