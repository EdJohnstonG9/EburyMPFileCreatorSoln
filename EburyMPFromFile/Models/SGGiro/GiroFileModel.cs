using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EburyMPFromFile.Models.SGGiro
{
    public class GiroFileModel
    {
        public GiroHeaderModel GiroHeader { get; set; }
        public IEnumerable<GiroItemModel> GiroItems { get; set; }
        public GiroFooterModel GiroFooter { get; set; }
    }
}
