using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOA.Types.Banking
{
    public class GetAllEducationLevelsResponse : ResponseBase
    {
        public List<EducationLevelContract> DataContract;
    }
}
