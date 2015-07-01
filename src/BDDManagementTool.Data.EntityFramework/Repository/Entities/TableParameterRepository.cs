using Bohrium.Tools.BDDManagementTool.Data.EntityFramework.Entities;
using Bohrium.Tools.BDDManagementTool.Data.EntityFramework.Infrasctructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bohrium.Tools.BDDManagementTool.Data.EntityFramework.Repository.Entities
{
    public class TableParameterRepository : BaseRepository
    {
        public TableParameterRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public void Add(TableParameter entity)
        {
            UnitOfWork.Context.Set<TableParameter>().Add(entity);
        }

        public void AddColumn(TableParameterColumn entity)
        {
            UnitOfWork.Context.Set<TableParameterColumn>().Add(entity);
        }

        public void AddRow(TableParameterRow entity)
        {
            UnitOfWork.Context.Set<TableParameterRow>().Add(entity);
        }

        public void AddCell(TableParameterCell entity)
        {
            UnitOfWork.Context.Set<TableParameterCell>().Add(entity);
        }
    }
}
