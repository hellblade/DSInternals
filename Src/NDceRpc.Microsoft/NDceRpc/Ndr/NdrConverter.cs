using System;

namespace NDceRpc.Ndr
{
	public static class NdrConverter
	{
		public const uint NDR_LITTLE_ENDIAN = 16u;

		public const uint NDR_BIG_ENDIAN = 0u;

		public static uint NDR_LOCAL_DATA_REPRESENTATION;

		public static uint NDR_LOCAL_ENDIAN;

		static NdrConverter()
		{
			NDR_LOCAL_DATA_REPRESENTATION = 16u;
			NDR_LOCAL_ENDIAN = 16u;
			PlatformID platform = Environment.OSVersion.Platform;
			if (platform == PlatformID.MacOSX || platform == PlatformID.Unix)
			{
				NDR_LOCAL_ENDIAN = 0u;
				NDR_LOCAL_DATA_REPRESENTATION = 0u;
			}
		}

		public static byte[] NdrFcLong(int s)
		{
			return new byte[4]
			{
				(byte)((uint)s & 0xFFu),
				(byte)((s & 0xFF00) >> 8),
				(byte)((s & 0xFF0000) >> 16),
				(byte)(s >> 24)
			};
		}
	}
}
