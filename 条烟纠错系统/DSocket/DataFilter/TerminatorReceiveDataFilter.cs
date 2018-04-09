using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuperSocket.ProtoBase;
using 条烟纠错系统.DSocket;

namespace 条烟纠错系统.DSocket.DataFilter
{
    class TerminatorDataReceiveFilter : TerminatorReceiveFilter<DataPackageInfo>
    {
        private readonly DataReceiveFilter _switchFilter;
        public TerminatorDataReceiveFilter(DataReceiveFilter filter) : base(new byte[] { 0x3B})
        {
            _switchFilter = filter;
        }

        public override DataPackageInfo ResolvePackage(IBufferStream bufferStream)
        {
            var length = Convert.ToInt32(bufferStream.Length);
            
            var code = bufferStream.ReadString(length, Encoding.ASCII);
            var responseInfo = new DataPackageInfo(code.Substring(0,length-1));
            NextReceiveFilter = _switchFilter;
            return responseInfo;
        }
    }
}
