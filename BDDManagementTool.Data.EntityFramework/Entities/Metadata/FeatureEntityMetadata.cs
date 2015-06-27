using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bohrium.Tools.BDDManagementTool.Data.EntityFramework.Entities.Metadata
{
    public class FeatureEntityMetadata
    {
        public Guid ObjectId { get; set; }
        public string Description { get; set; }
        [ForeignKey("FeatureId")]
        public IList<ScenarioEntity> Scenarios { get; set; }
    }
}
