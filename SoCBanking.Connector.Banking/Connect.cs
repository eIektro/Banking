using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SoCBanking.Types.Banking;
using SoCBanking.Types.Banking.Enums;
using SoCBanking.Process.Banking;
using System.Reflection;
using SoCBanking.Connector.Banking.Exceptions;


namespace SoCBanking.Connector.Banking
{
    public class Connect<T> where  T: ResponseBase
    {
        private object instance;

        public T Execute(RequestBase request) //
        {
            Type requestType = request.GetType();
            string requestedClass = requestType.Name.Replace("Request", "");
            /*string requestedClass = (from x in Enum.GetNames(typeof(RequestTypes)) where requestType.Name.Contains(x) select x).FirstOrDefault();*/ //remove
            var assembly = Assembly.LoadFrom(@"..\..\..\..\SoCBanking.Process.Banking\bin\Debug\SoCBanking.Process.Banking.dll"); //Assemly.Load("SoCBanking.Process.Banking")
            
            Type t = assembly.GetType(assembly.GetName().Name + "." + requestedClass);

            try
            {
                instance = Activator.CreateInstance(t);
            }
            catch (Exception)
            {
                throw new RequestedClassNotFoundException($"Requested class ({requestedClass}) not found in namespace");
            }

            try
            {
                var processMethodInfo = t.GetMethod(request.MethodName);
                var response = processMethodInfo.Invoke(instance, new Object[] { request });
                return (T)response;
            }
            catch (Exception)
            { 
                throw new MethodNameNotFoundException($"Requested method name ({request.MethodName}) is not defined.");
            }

        }
    }
}
