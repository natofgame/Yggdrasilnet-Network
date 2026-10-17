using System;
using System.Numerics;
using LiteNetLib.Utils;

namespace Yggdrasilnet.Network.Protocol;

/// <summary>
///  Helper class for protocols.
/// </summary>
/// <param name="protocol"></param>
public sealed class ProtocolHelper(GameProtocol protocol) {
    /// <summary>
    ///  The protocol version of the current protocol.
    /// </summary>
    public int ProtocolVersion => protocol.ProtocolVersion;

    /// <summary>
    ///  Writes an enum to the writer.
    /// </summary>
    /// <param name="writer"></param>
    /// <param name="value"></param>
    /// <typeparam name="TEnum"></typeparam>
    public void WriteEnum<TEnum>(NetDataWriter writer, TEnum value) where TEnum : struct, Enum {
        writer.Put(Convert.ToByte(value));
    }

    /// <summary>
    ///  Reads an enum from the reader.
    /// </summary>
    /// <param name="reader"></param>
    /// <typeparam name="TEnum"></typeparam>
    /// <returns></returns>
    public TEnum ReadEnum<TEnum>(NetDataReader reader) where TEnum : struct, Enum {
        return (TEnum)Enum.ToObject(typeof(TEnum), reader.GetByte());
    }

    /// <summary>
    ///  Writes a Vector3 to the writer.
    /// </summary>
    /// <param name="writer"></param>
    /// <param name="value"></param>
    public void WriteVector3(NetDataWriter writer, Vector3 value) {
        writer.Put(value.X);
        writer.Put(value.Y);
        writer.Put(value.Z);
    }

    /// <summary>
    ///  Reads a Vector3 from the reader.
    /// </summary>
    /// <param name="reader"></param>
    /// <returns></returns>
    public Vector3 ReadVector3(NetDataReader reader) {
        return new Vector3(reader.GetFloat(), reader.GetFloat(), reader.GetFloat());
    }
}
