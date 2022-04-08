using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vending2022
{
    public interface IVending
    {
        public void Purchase();
        public void ShowAll();
        public void InsertMoney();
        public void EndTransaction();

    }
}
