
using UselessFrame.Runtime;
using UselessFrame.Runtime.Types;
using UselessFrameUnity.RedPointSystem;

namespace UselessFrameUnity
{
    public static partial class Framework
    {
        private static IFrameCore _core;
        private static IRedPointModule _redPoint;

        public static ITypeSystem TypeSystem => _core.TypeSystem;

        public static IRedPointModule RedPoint => _redPoint ??= (IRedPointModule)_core.GetModule(typeof(RedPointModule));

        public static void Initialize(IFrameCore core)
        {
            _core = core;
        }
    }
}
