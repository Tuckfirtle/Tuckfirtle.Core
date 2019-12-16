// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: Network/P2P/Packet.proto
// </auto-generated>
#pragma warning disable 1591, 0612, 3021
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
namespace Tuckfirtle.Core.Network.P2P {

  /// <summary>Holder for reflection information generated from Network/P2P/Packet.proto</summary>
  public static partial class PacketReflection {

    #region Descriptor
    /// <summary>File descriptor for Network/P2P/Packet.proto</summary>
    public static pbr::FileDescriptor Descriptor {
      get { return descriptor; }
    }
    private static pbr::FileDescriptor descriptor;

    static PacketReflection() {
      byte[] descriptorData = global::System.Convert.FromBase64String(
          string.Concat(
            "ChhOZXR3b3JrL1AyUC9QYWNrZXQucHJvdG8aLk5ldHdvcmsvUDJQL0hlYWRl",
            "ci9QYWNrZXRDb21wcmVzc2lvblR5cGUucHJvdG8aLU5ldHdvcmsvUDJQL0hl",
            "YWRlci9QYWNrZXRFbmNyeXB0aW9uVHlwZS5wcm90bxojTmV0d29yay9QMlAv",
            "SGVhZGVyL1BhY2tldFR5cGUucHJvdG8aK05ldHdvcmsvUDJQL0hlYWRlci9Q",
            "YWNrZXRDaGVja3N1bVR5cGUucHJvdG8i4AIKBlBhY2tldBIWCg5wYWNrZXRf",
            "bmV0d29yaxgBIAEoDBInCh9wYWNrZXRfbmV0d29ya19wcm90b2NvbF92ZXJz",
            "aW9uGAIgASgFEiIKGnBhY2tldF9rZWVwX2FsaXZlX2R1cmF0aW9uGAMgASgF",
            "EjcKF3BhY2tldF9jb21wcmVzc2lvbl90eXBlGAQgASgOMhYuUGFja2V0Q29t",
            "cHJlc3Npb25UeXBlEjUKFnBhY2tldF9lbmNyeXB0aW9uX3R5cGUYBSABKA4y",
            "FS5QYWNrZXRFbmNyeXB0aW9uVHlwZRIgCgtwYWNrZXRfdHlwZRgGIAEoDjIL",
            "LlBhY2tldFR5cGUSMQoUcGFja2V0X2NoZWNrc3VtX3R5cGUYByABKA4yEy5Q",
            "YWNrZXRDaGVja3N1bVR5cGUSEwoLcGFja2V0X2RhdGEYCCABKAwSFwoPcGFj",
            "a2V0X2NoZWNrc3VtGAkgASgMQh6qAhtUdWNrZmlydGxlLkNvcmUuTmV0d29y",
            "ay5QMlBiBnByb3RvMw=="));
      descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
          new pbr::FileDescriptor[] { global::Tuckfirtle.Core.Network.P2P.Header.PacketCompressionTypeReflection.Descriptor, global::Tuckfirtle.Core.Network.P2P.Header.PacketEncryptionTypeReflection.Descriptor, global::Tuckfirtle.Core.Network.P2P.Header.PacketTypeReflection.Descriptor, global::Tuckfirtle.Core.Network.P2P.Header.PacketChecksumTypeReflection.Descriptor, },
          new pbr::GeneratedClrTypeInfo(null, new pbr::GeneratedClrTypeInfo[] {
            new pbr::GeneratedClrTypeInfo(typeof(global::Tuckfirtle.Core.Network.P2P.Packet), global::Tuckfirtle.Core.Network.P2P.Packet.Parser, new[]{ "PacketNetwork", "PacketNetworkProtocolVersion", "PacketKeepAliveDuration", "PacketCompressionType", "PacketEncryptionType", "PacketType", "PacketChecksumType", "PacketData", "PacketChecksum" }, null, null, null)
          }));
    }
    #endregion

  }
  #region Messages
  public sealed partial class Packet : pb::IMessage<Packet> {
    private static readonly pb::MessageParser<Packet> _parser = new pb::MessageParser<Packet>(() => new Packet());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<Packet> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::Tuckfirtle.Core.Network.P2P.PacketReflection.Descriptor.MessageTypes[0]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public Packet() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public Packet(Packet other) : this() {
      packetNetwork_ = other.packetNetwork_;
      packetNetworkProtocolVersion_ = other.packetNetworkProtocolVersion_;
      packetKeepAliveDuration_ = other.packetKeepAliveDuration_;
      packetCompressionType_ = other.packetCompressionType_;
      packetEncryptionType_ = other.packetEncryptionType_;
      packetType_ = other.packetType_;
      packetChecksumType_ = other.packetChecksumType_;
      packetData_ = other.packetData_;
      packetChecksum_ = other.packetChecksum_;
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public Packet Clone() {
      return new Packet(this);
    }

    /// <summary>Field number for the "packet_network" field.</summary>
    public const int PacketNetworkFieldNumber = 1;
    private pb::ByteString packetNetwork_ = pb::ByteString.Empty;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public pb::ByteString PacketNetwork {
      get { return packetNetwork_; }
      set {
        packetNetwork_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    /// <summary>Field number for the "packet_network_protocol_version" field.</summary>
    public const int PacketNetworkProtocolVersionFieldNumber = 2;
    private int packetNetworkProtocolVersion_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int PacketNetworkProtocolVersion {
      get { return packetNetworkProtocolVersion_; }
      set {
        packetNetworkProtocolVersion_ = value;
      }
    }

    /// <summary>Field number for the "packet_keep_alive_duration" field.</summary>
    public const int PacketKeepAliveDurationFieldNumber = 3;
    private int packetKeepAliveDuration_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int PacketKeepAliveDuration {
      get { return packetKeepAliveDuration_; }
      set {
        packetKeepAliveDuration_ = value;
      }
    }

    /// <summary>Field number for the "packet_compression_type" field.</summary>
    public const int PacketCompressionTypeFieldNumber = 4;
    private global::Tuckfirtle.Core.Network.P2P.Header.PacketCompressionType packetCompressionType_ = 0;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public global::Tuckfirtle.Core.Network.P2P.Header.PacketCompressionType PacketCompressionType {
      get { return packetCompressionType_; }
      set {
        packetCompressionType_ = value;
      }
    }

    /// <summary>Field number for the "packet_encryption_type" field.</summary>
    public const int PacketEncryptionTypeFieldNumber = 5;
    private global::Tuckfirtle.Core.Network.P2P.Header.PacketEncryptionType packetEncryptionType_ = 0;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public global::Tuckfirtle.Core.Network.P2P.Header.PacketEncryptionType PacketEncryptionType {
      get { return packetEncryptionType_; }
      set {
        packetEncryptionType_ = value;
      }
    }

    /// <summary>Field number for the "packet_type" field.</summary>
    public const int PacketTypeFieldNumber = 6;
    private global::Tuckfirtle.Core.Network.P2P.Header.PacketType packetType_ = 0;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public global::Tuckfirtle.Core.Network.P2P.Header.PacketType PacketType {
      get { return packetType_; }
      set {
        packetType_ = value;
      }
    }

    /// <summary>Field number for the "packet_checksum_type" field.</summary>
    public const int PacketChecksumTypeFieldNumber = 7;
    private global::Tuckfirtle.Core.Network.P2P.Header.PacketChecksumType packetChecksumType_ = 0;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public global::Tuckfirtle.Core.Network.P2P.Header.PacketChecksumType PacketChecksumType {
      get { return packetChecksumType_; }
      set {
        packetChecksumType_ = value;
      }
    }

    /// <summary>Field number for the "packet_data" field.</summary>
    public const int PacketDataFieldNumber = 8;
    private pb::ByteString packetData_ = pb::ByteString.Empty;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public pb::ByteString PacketData {
      get { return packetData_; }
      set {
        packetData_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    /// <summary>Field number for the "packet_checksum" field.</summary>
    public const int PacketChecksumFieldNumber = 9;
    private pb::ByteString packetChecksum_ = pb::ByteString.Empty;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public pb::ByteString PacketChecksum {
      get { return packetChecksum_; }
      set {
        packetChecksum_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as Packet);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(Packet other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (PacketNetwork != other.PacketNetwork) return false;
      if (PacketNetworkProtocolVersion != other.PacketNetworkProtocolVersion) return false;
      if (PacketKeepAliveDuration != other.PacketKeepAliveDuration) return false;
      if (PacketCompressionType != other.PacketCompressionType) return false;
      if (PacketEncryptionType != other.PacketEncryptionType) return false;
      if (PacketType != other.PacketType) return false;
      if (PacketChecksumType != other.PacketChecksumType) return false;
      if (PacketData != other.PacketData) return false;
      if (PacketChecksum != other.PacketChecksum) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      if (PacketNetwork.Length != 0) hash ^= PacketNetwork.GetHashCode();
      if (PacketNetworkProtocolVersion != 0) hash ^= PacketNetworkProtocolVersion.GetHashCode();
      if (PacketKeepAliveDuration != 0) hash ^= PacketKeepAliveDuration.GetHashCode();
      if (PacketCompressionType != 0) hash ^= PacketCompressionType.GetHashCode();
      if (PacketEncryptionType != 0) hash ^= PacketEncryptionType.GetHashCode();
      if (PacketType != 0) hash ^= PacketType.GetHashCode();
      if (PacketChecksumType != 0) hash ^= PacketChecksumType.GetHashCode();
      if (PacketData.Length != 0) hash ^= PacketData.GetHashCode();
      if (PacketChecksum.Length != 0) hash ^= PacketChecksum.GetHashCode();
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
      if (PacketNetwork.Length != 0) {
        output.WriteRawTag(10);
        output.WriteBytes(PacketNetwork);
      }
      if (PacketNetworkProtocolVersion != 0) {
        output.WriteRawTag(16);
        output.WriteInt32(PacketNetworkProtocolVersion);
      }
      if (PacketKeepAliveDuration != 0) {
        output.WriteRawTag(24);
        output.WriteInt32(PacketKeepAliveDuration);
      }
      if (PacketCompressionType != 0) {
        output.WriteRawTag(32);
        output.WriteEnum((int) PacketCompressionType);
      }
      if (PacketEncryptionType != 0) {
        output.WriteRawTag(40);
        output.WriteEnum((int) PacketEncryptionType);
      }
      if (PacketType != 0) {
        output.WriteRawTag(48);
        output.WriteEnum((int) PacketType);
      }
      if (PacketChecksumType != 0) {
        output.WriteRawTag(56);
        output.WriteEnum((int) PacketChecksumType);
      }
      if (PacketData.Length != 0) {
        output.WriteRawTag(66);
        output.WriteBytes(PacketData);
      }
      if (PacketChecksum.Length != 0) {
        output.WriteRawTag(74);
        output.WriteBytes(PacketChecksum);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(output);
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (PacketNetwork.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeBytesSize(PacketNetwork);
      }
      if (PacketNetworkProtocolVersion != 0) {
        size += 1 + pb::CodedOutputStream.ComputeInt32Size(PacketNetworkProtocolVersion);
      }
      if (PacketKeepAliveDuration != 0) {
        size += 1 + pb::CodedOutputStream.ComputeInt32Size(PacketKeepAliveDuration);
      }
      if (PacketCompressionType != 0) {
        size += 1 + pb::CodedOutputStream.ComputeEnumSize((int) PacketCompressionType);
      }
      if (PacketEncryptionType != 0) {
        size += 1 + pb::CodedOutputStream.ComputeEnumSize((int) PacketEncryptionType);
      }
      if (PacketType != 0) {
        size += 1 + pb::CodedOutputStream.ComputeEnumSize((int) PacketType);
      }
      if (PacketChecksumType != 0) {
        size += 1 + pb::CodedOutputStream.ComputeEnumSize((int) PacketChecksumType);
      }
      if (PacketData.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeBytesSize(PacketData);
      }
      if (PacketChecksum.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeBytesSize(PacketChecksum);
      }
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(Packet other) {
      if (other == null) {
        return;
      }
      if (other.PacketNetwork.Length != 0) {
        PacketNetwork = other.PacketNetwork;
      }
      if (other.PacketNetworkProtocolVersion != 0) {
        PacketNetworkProtocolVersion = other.PacketNetworkProtocolVersion;
      }
      if (other.PacketKeepAliveDuration != 0) {
        PacketKeepAliveDuration = other.PacketKeepAliveDuration;
      }
      if (other.PacketCompressionType != 0) {
        PacketCompressionType = other.PacketCompressionType;
      }
      if (other.PacketEncryptionType != 0) {
        PacketEncryptionType = other.PacketEncryptionType;
      }
      if (other.PacketType != 0) {
        PacketType = other.PacketType;
      }
      if (other.PacketChecksumType != 0) {
        PacketChecksumType = other.PacketChecksumType;
      }
      if (other.PacketData.Length != 0) {
        PacketData = other.PacketData;
      }
      if (other.PacketChecksum.Length != 0) {
        PacketChecksum = other.PacketChecksum;
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
          case 10: {
            PacketNetwork = input.ReadBytes();
            break;
          }
          case 16: {
            PacketNetworkProtocolVersion = input.ReadInt32();
            break;
          }
          case 24: {
            PacketKeepAliveDuration = input.ReadInt32();
            break;
          }
          case 32: {
            PacketCompressionType = (global::Tuckfirtle.Core.Network.P2P.Header.PacketCompressionType) input.ReadEnum();
            break;
          }
          case 40: {
            PacketEncryptionType = (global::Tuckfirtle.Core.Network.P2P.Header.PacketEncryptionType) input.ReadEnum();
            break;
          }
          case 48: {
            PacketType = (global::Tuckfirtle.Core.Network.P2P.Header.PacketType) input.ReadEnum();
            break;
          }
          case 56: {
            PacketChecksumType = (global::Tuckfirtle.Core.Network.P2P.Header.PacketChecksumType) input.ReadEnum();
            break;
          }
          case 66: {
            PacketData = input.ReadBytes();
            break;
          }
          case 74: {
            PacketChecksum = input.ReadBytes();
            break;
          }
        }
      }
    }

  }

  #endregion

}

#endregion Designer generated code
