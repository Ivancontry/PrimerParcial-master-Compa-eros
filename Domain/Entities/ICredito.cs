using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public interface ICredito
    {
        string Abonar(double valor);
        IList<string> CanAbonar(double valor);
    }
}
