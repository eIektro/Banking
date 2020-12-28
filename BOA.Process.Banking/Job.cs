using BOA.Types.Banking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOA.Process.Banking
{
    public class Job
    {
        public GetAllJobsResponse getAllJobs(JobRequest request)
        {
            Business.Banking.Job jobBusiness = new Business.Banking.Job();
            var response = jobBusiness.getAllJobs(request);

            return response;

        }

    }
}
