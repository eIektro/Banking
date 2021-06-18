using SoCBanking.Types.Banking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoCBanking.Process.Banking
{
    public class Branch
    {
        public GenericResponse<List<CityContract>> getAllCities(BranchRequest request)
        {
            Business.Banking.City cityBusiness = new Business.Banking.City();
            var response = cityBusiness.getAllCities();
            return response;
            
        }

        public GenericResponse<List<BranchContract>> FilterBranchsByProperties(BranchRequest request)
        {
            if (request.DataContract.DateOfLaunch.GetValueOrDefault() < new DateTime(1753, 01, 01))
            {
                DateTime sqlRange = new DateTime(1753, 01, 01);
                request.DataContract.DateOfLaunch = sqlRange;
            }

            Business.Banking.Branch branchBusiness = new Business.Banking.Branch();
            var response = branchBusiness.FilterBranchsByProperties(request);
            return response;
        }

        public GenericResponse<BranchContract> AddNewBranch(BranchRequest request)
        {
            Business.Banking.Branch branchBusiness = new Business.Banking.Branch();
            var response = branchBusiness.AddNewBranch(request);
            return response;
        }

        public GenericResponse<List<BranchContract>> GetAllBranches(BranchRequest request)
        {
            Business.Banking.Branch branchBusiness = new Business.Banking.Branch();
            var response = branchBusiness.GetAllBranches(request);
            return response;
        }

        public GenericResponse<BranchContract> UpdateBranchDetailsById(BranchRequest request)
        {
            Business.Banking.Branch branchBusiness = new Business.Banking.Branch();
            var response = branchBusiness.UpdateBranchDetailsById(request);
            return response;
        }

        public GenericResponse<BranchContract> DeleteBranchById(BranchRequest request)
        {
            Business.Banking.Branch branchBusiness = new Business.Banking.Branch();
            var response = branchBusiness.DeleteBranchById(request);
            return response;
        }
    }
}
