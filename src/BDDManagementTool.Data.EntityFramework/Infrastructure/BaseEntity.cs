using System;
using System.ComponentModel.DataAnnotations;

namespace Bohrium.Tools.BDDManagementTool.Data.EntityFramework.Infrasctructure
{
    public abstract class BaseEntity
    {
        [Key]
        public virtual Guid Id { get; set; }
    }
}