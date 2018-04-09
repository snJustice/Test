using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 条烟纠错系统.TableHandles
{
    public interface IGetOrder
    {
        void Connect();
        DataTable GetRemoteOrder();

    }
}
