// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: Network/P2P/Packets/HandshakePacket.proto
// </auto-generated>
#pragma warning disable 1591, 0612, 3021
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
namespace Tuckfirtle.Core.Network.P2P.Packets {

  /// <summary>Holder for reflection information generated from Network/P2P/Packets/HandshakePacket.proto</summary>
  public static partial class HandshakePacketReflection {

    #region Descriptor
    /// <summary>File descriptor for Network/P2P/Packets/HandshakePacket.proto</summary>
    public static pbr::FileDescriptor Descriptor {
      get { return descriptor; }
    }
    private static pbr::FileDescriptor descriptor;

    static HandshakePacketReflection() {
      byte[] descriptorData = global::System.Convert.FromBase64String(
          string.Concat(
            "CilOZXR3b3JrL1AyUC9QYWNrZXRzL0hhbmRzaGFrZVBhY2tldC5wcm90byKb",
            "AQoPSGFuZHNoYWtlUGFja2V0EkgKIGtleV9lbmNhcHN1bGF0aW9uX21lY2hh",
            "bmlzbV90eXBlGAEgASgOMh4uS2V5RW5jYXBzdWxhdGlvbk1lY2hhbmlzbVR5",
            "cGUSJgoOaGFuZHNoYWtlX3R5cGUYAiABKA4yDi5IYW5kc2hha2VUeXBlEhYK",
            "DmhhbmRzaGFrZV9kYXRhGAMgASgMKosCCh1LZXlFbmNhcHN1bGF0aW9uTWVj",
            "aGFuaXNtVHlwZRI5CjVLRVlfRU5DQVBTVUxBVElPTl9NRUNIQU5JU01fVFlQ",
            "RV9TSUtFX1A0MzRfQ09NUFJFU1NFRBAAEjkKNUtFWV9FTkNBUFNVTEFUSU9O",
            "X01FQ0hBTklTTV9UWVBFX1NJS0VfUDUwM19DT01QUkVTU0VEEAESOQo1S0VZ",
            "X0VOQ0FQU1VMQVRJT05fTUVDSEFOSVNNX1RZUEVfU0lLRV9QNjEwX0NPTVBS",
            "RVNTRUQQAhI5CjVLRVlfRU5DQVBTVUxBVElPTl9NRUNIQU5JU01fVFlQRV9T",
            "SUtFX1A3NTFfQ09NUFJFU1NFRBADKk4KDUhhbmRzaGFrZVR5cGUSHQoZSEFO",
            "RFNIQUtFX1RZUEVfUFVCTElDX0tFWRAAEh4KGkhBTkRTSEFLRV9UWVBFX0NJ",
            "UEhFUl9URVhUEAFCJqoCI1R1Y2tmaXJ0bGUuQ29yZS5OZXR3b3JrLlAyUC5Q",
            "YWNrZXRzYgZwcm90bzM="));
      descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
          new pbr::FileDescriptor[] { },
          new pbr::GeneratedClrTypeInfo(new[] {typeof(global::Tuckfirtle.Core.Network.P2P.Packets.KeyEncapsulationMechanismType), typeof(global::Tuckfirtle.Core.Network.P2P.Packets.HandshakeType), }, new pbr::GeneratedClrTypeInfo[] {
            new pbr::GeneratedClrTypeInfo(typeof(global::Tuckfirtle.Core.Network.P2P.Packets.HandshakePacket), global::Tuckfirtle.Core.Network.P2P.Packets.HandshakePacket.Parser, new[]{ "KeyEncapsulationMechanismType", "HandshakeType", "HandshakeData" }, null, null, null)
          }));
    }
    #endregion

  }
  #region Enums
  public enum KeyEncapsulationMechanismType {
    [pbr::OriginalName("KEY_ENCAPSULATION_MECHANISM_TYPE_SIKE_P434_COMPRESSED")] SikeP434Compressed = 0,
    [pbr::OriginalName("KEY_ENCAPSULATION_MECHANISM_TYPE_SIKE_P503_COMPRESSED")] SikeP503Compressed = 1,
    [pbr::OriginalName("KEY_ENCAPSULATION_MECHANISM_TYPE_SIKE_P610_COMPRESSED")] SikeP610Compressed = 2,
    [pbr::OriginalName("KEY_ENCAPSULATION_MECHANISM_TYPE_SIKE_P751_COMPRESSED")] SikeP751Compressed = 3,
  }

  public enum HandshakeType {
    [pbr::OriginalName("HANDSHAKE_TYPE_PUBLIC_KEY")] PublicKey = 0,
    [pbr::OriginalName("HANDSHAKE_TYPE_CIPHER_TEXT")] CipherText = 1,
  }

  #endregion

  #region Messages
  public sealed partial class HandshakePacket : pb::IMessage<HandshakePacket> {
    private static readonly pb::MessageParser<HandshakePacket> _parser = new pb::MessageParser<HandshakePacket>(() => new HandshakePacket());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<HandshakePacket> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::Tuckfirtle.Core.Network.P2P.Packets.HandshakePacketReflection.Descriptor.MessageTypes[0]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public HandshakePacket() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public HandshakePacket(HandshakePacket other) : this() {
      keyEncapsulationMechanismType_ = other.keyEncapsulationMechanismType_;
      handshakeType_ = other.handshakeType_;
      handshakeData_ = other.handshakeData_;
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public HandshakePacket Clone() {
      return new HandshakePacket(this);
    }

    /// <summary>Field number for the "key_encapsulation_mechanism_type" field.</summary>
    public const int KeyEncapsulationMechanismTypeFieldNumber = 1;
    private global::Tuckfirtle.Core.Network.P2P.Packets.KeyEncapsulationMechanismType keyEncapsulationMechanismType_ = 0;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public global::Tuckfirtle.Core.Network.P2P.Packets.KeyEncapsulationMechanismType KeyEncapsulationMechanismType {
      get { return keyEncapsulationMechanismType_; }
      set {
        keyEncapsulationMechanismType_ = value;
      }
    }

    /// <summary>Field number for the "handshake_type" field.</summary>
    public const int HandshakeTypeFieldNumber = 2;
    private global::Tuckfirtle.Core.Network.P2P.Packets.HandshakeType handshakeType_ = 0;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public global::Tuckfirtle.Core.Network.P2P.Packets.HandshakeType HandshakeType {
      get { return handshakeType_; }
      set {
        handshakeType_ = value;
      }
    }

    /// <summary>Field number for the "handshake_data" field.</summary>
    public const int HandshakeDataFieldNumber = 3;
    private pb::ByteString handshakeData_ = pb::ByteString.Empty;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public pb::ByteString HandshakeData {
      get { return handshakeData_; }
      set {
        handshakeData_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as HandshakePacket);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(HandshakePacket other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (KeyEncapsulationMechanismType != other.KeyEncapsulationMechanismType) return false;
      if (HandshakeType != other.HandshakeType) return false;
      if (HandshakeData != other.HandshakeData) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      if (KeyEncapsulationMechanismType != 0) hash ^= KeyEncapsulationMechanismType.GetHashCode();
      if (HandshakeType != 0) hash ^= HandshakeType.GetHashCode();
      if (HandshakeData.Length != 0) hash ^= HandshakeData.GetHashCode();
      if (_unknownFields != null) {
        hash ^= _unknownFields.GetHashCode();
      }
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
      if (KeyEncapsulationMechanismType != 0) {
        output.WriteRawTag(8);
        output.WriteEnum((int) KeyEncapsulationMechanismType);
      }
      if (HandshakeType != 0) {
        output.WriteRawTag(16);
        output.WriteEnum((int) HandshakeType);
      }
      if (HandshakeData.Length != 0) {
        output.WriteRawTag(26);
        output.WriteBytes(HandshakeData);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(output);
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (KeyEncapsulationMechanismType != 0) {
        size += 1 + pb::CodedOutputStream.ComputeEnumSize((int) KeyEncapsulationMechanismType);
      }
      if (HandshakeType != 0) {
        size += 1 + pb::CodedOutputStream.ComputeEnumSize((int) HandshakeType);
      }
      if (HandshakeData.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeBytesSize(HandshakeData);
      }
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(HandshakePacket other) {
      if (other == null) {
        return;
      }
      if (other.KeyEncapsulationMechanismType != 0) {
        KeyEncapsulationMechanismType = other.KeyEncapsulationMechanismType;
      }
      if (other.HandshakeType != 0) {
        HandshakeType = other.HandshakeType;
      }
      if (other.HandshakeData.Length != 0) {
        HandshakeData = other.HandshakeData;
      }
      _unknownFields = pb::UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(pb::CodedInputStream input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, input);
            break;
          case 8: {
            KeyEncapsulationMechanismType = (global::Tuckfirtle.Core.Network.P2P.Packets.KeyEncapsulationMechanismType) input.ReadEnum();
            break;
          }
          case 16: {
            HandshakeType = (global::Tuckfirtle.Core.Network.P2P.Packets.HandshakeType) input.ReadEnum();
            break;
          }
          case 26: {
            HandshakeData = input.ReadBytes();
            break;
          }
        }
      }
    }

  }

  #endregion

}

#endregion Designer generated code
