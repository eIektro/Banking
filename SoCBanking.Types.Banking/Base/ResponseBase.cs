using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoCBanking.Types.Banking
{
    public class ResponseBase
    {

        public bool IsSuccess;

        public string ErrorMessage;
    }


    public class GenericResponse<T> : ResponseBase
    {

        public T Value { get; set; }
    }
}
