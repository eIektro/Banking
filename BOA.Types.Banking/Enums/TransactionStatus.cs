using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace BOA.Types.Banking.Enums
{
    public enum TransactionStatus {
        [Description("Aktif")]
        Active = 0,
        [Description("İptal Edildi")]
        Cancelled 
    }
}
