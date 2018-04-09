using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuperSocket.ProtoBase;

namespace 条烟纠错系统.DSocket.DataFilter

{
    class FixedSizeDataReceiveFilter : FixedSizeReceiveFilter<DataPackageInfo>
    {
        private readonly DataReceiveFilter _switchFilter;
        public FixedSizeDataReceiveFilter(DataReceiveFilter filter,int size) :base(size)
        {
            _switchFilter = filter;
        }

        public override DataPackageInfo ResolvePackage(IBufferStream bufferStream)
        {
            var length = Convert.ToInt32(bufferStream.Length);
            var code = bufferStream.ReadString(length, Encoding.ASCII);
            var responseInfo = new DataPackageInfo(code);
            NextReceiveFilter = _switchFilter;
            return responseInfo;
        }
    }
}
