using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BOA.Types.Banking;
using BOA.Types.Banking.Enums;
using BOA.Process.Banking;
using System.Reflection;
using BOA.Connector.Banking.Exceptions;


namespace BOA.Connector.Banking
{
    public class Connect
    {
        private object instance;

        public ResponseBase Execute(RequestBase request) 
        {
            Type requestType = request.GetType();
            string requestedClass = requestType.Name.Replace("Request", "");
            /*string requestedClass = (from x in Enum.GetNames(typeof(RequestTypes)) where requestType.Name.Contains(x) select x).FirstOrDefault();*/ //remove
            var assembly = Assembly.LoadFrom(@"..\..\..\..\BOA.Process.Banking\bin\Debug\BOA.Process.Banking.dll"); //Assemly.Load("BOA.Process.Banking")
            
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
                return (ResponseBase)response;
            }
            catch (Exception e)
            { 
                throw new MethodNameNotFoundException($"Requested method name ({request.MethodName}) is not defined.");
            }

        }
    }
}
