using BOA.Types.Banking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOA.Process.Banking
{
    public class Branch
    {
        public ResponseBase getAllCities(BranchRequest request)
        {
            Business.Banking.City cityBusiness = new Business.Banking.City();
            var response = cityBusiness.getAllCities();
            return response;
            
        }

        public ResponseBase AddNewBranch(BranchRequest request)
        {
            Business.Banking.Branch branchBusiness = new Business.Banking.Branch();
            var response = branchBusiness.AddNewBranch(request);
            return response;
        }

        public ResponseBase GetAllBranches(BranchRequest request)
        {
            Business.Banking.Branch branchBusiness = new Business.Banking.Branch();
            var response = branchBusiness.GetAllBranches(request);
            return response;
        }

        public ResponseBase UpdateBranchDetailsById(BranchRequest request)
        {
            Business.Banking.Branch branchBusiness = new Business.Banking.Branch();
            var response = branchBusiness.UpdateBranchDetailsById(request);
            return response;
        }

        public ResponseBase DeleteBranchById(BranchRequest request)
        {
            Business.Banking.Branch branchBusiness = new Business.Banking.Branch();
            var response = branchBusiness.DeleteBranchById(request);
            return response;
        }
    }
}
