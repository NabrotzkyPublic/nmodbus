using System;
using System.Collections.Generic;
using System.Text;
using Modbus.Data;
using Modbus.Util;

namespace Modbus.Message
{
	class ReadHoldingInputRegistersResponse : ModbusMessageWithData<RegisterCollection>, IModbusMessage
	{	
		private const int _minimumFrameSize = 3;

		public ReadHoldingInputRegistersResponse()
		{
		}

		public ReadHoldingInputRegistersResponse(byte functionCode, byte slaveAddress, byte byteCount, RegisterCollection data)
			: base(slaveAddress, functionCode)
		{
			ByteCount = byteCount;
			Data = data;
		}

		public byte ByteCount
		{
			get { return MessageImpl.ByteCount; }
			set { MessageImpl.ByteCount = value; }
		}

		public override int MinimumFrameSize
		{
			get { return _minimumFrameSize; }
		}

		protected override void InitializeUnique(byte[] frame)
		{
			if (frame.Length < _minimumFrameSize + frame[2])
				throw new FormatException("Message frame does not contain enough bytes.");

			ByteCount = frame[2];
			Data = new RegisterCollection(CollectionUtil.Slice<byte>(frame, 3, ByteCount));
		}
	}
}
