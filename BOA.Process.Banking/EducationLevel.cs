using BOA.Types.Banking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOA.Process.Banking
{
    public class EducationLevel
    {
        public GetAllEducationLevelsResponse getAllEducationLevels(EducationLevelRequest request)
        {
            Business.Banking.EducationLevel educationLvBusiness = new Business.Banking.EducationLevel();
            var response = educationLvBusiness.getAllEducationLevels(request);

            return response;

        }
    }
}
