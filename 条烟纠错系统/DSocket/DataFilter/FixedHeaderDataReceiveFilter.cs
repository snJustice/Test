using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuperSocket.ProtoBase;

namespace 条烟纠错系统.DSocket.DataFilter

{
    class FixedHeaderDataReceiveFilter : FixedHeaderReceiveFilter<DataPackageInfo>
    {

        private readonly DataReceiveFilter _switchFilter;
        public FixedHeaderDataReceiveFilter(DataReceiveFilter filter) :base(9)
        {
            _switchFilter = filter;
        }

        public override DataPackageInfo ResolvePackage(IBufferStream bufferStream)
        {
            var length = Convert.ToInt32( bufferStream.Length);
            var code = bufferStream.ReadString(length, Encoding.ASCII);
            var responseInfo = new DataPackageInfo(code);
            return responseInfo;
        }

        protected override int GetBodyLengthFromHeader(IBufferStream bufferStream, int length)
        {
            return bufferStream.Skip(length - 1).ReadByte();
        }
    }
}
