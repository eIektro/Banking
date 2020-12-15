using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BOA.Types.Banking;
using BOA.Process.Banking;
using System.Reflection;

namespace BOA.Connector.Banking
{
    public class Connect
    {

        public ResponseBase Execute(RequestBase request) //TO-DO: Tekil hale getirilecek, generic olacak
        {
            var response = new ResponseBase();

            var assemblies = Assembly.GetExecutingAssembly().GetReferencedAssemblies();
            var assemblyName = assemblies.FirstOrDefault(i=>i.Name=="BOA.Process.Banking");
            var assembly = Assembly.Load(assemblyName);


           

            return response;


            //var response = new ResponseBase();

            //Type type = request.GetType();

            //if (type.Namespace == "BOA.Types.Banking")
            //{
            //    var pr = new BOA.Process.Banking.Customer();

            //    if(request.MethodName == "GetAllCustomers")
            //    {
            //        response = pr.GetAllCustomers((GetAllCustomersRequest)request);
            //    }

            //    //if (request.MethodName == "CustomerSave")
            //    //{
            //    //    response = pr.CustomerSave((CustomerRequest)request);
            //    //}
            //}

            //if(type.FullName == "BOA.Types.Banking.LoginRequest")
            //{
            //    var pr = new BOA.Process.Banking.Login();

            //    if (request.MethodName == "UserLogin")
            //    {

            //        response = pr.UserLogin((LoginRequest)request);
            //    }
            //}




            //return response;

        }
    }
}
