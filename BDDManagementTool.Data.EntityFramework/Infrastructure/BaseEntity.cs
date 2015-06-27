using Bohrium.Tools.BDDManagementTool.Data.Infrasctructure;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Bohrium.Tools.BDDManagementTool.Data.EntityFramework.Infrasctructure
{
    public abstract class BaseEntity : IBaseEntity
    {
        [Key]
        public virtual long Id { get; set; }
    }
}