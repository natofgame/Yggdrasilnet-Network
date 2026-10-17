using Yggdrasilnet.Network.Protocol;

namespace Yggdrasilnet.Network.Packets;

/// <summary>
///  Packet for stats.
/// </summary>
public sealed class StatsPacket : IGamePacket {
    /// <summary>
    ///  Actual TPS.
    /// </summary>
    public float ActualTps { get; set; }
    
    /// <summary>
    ///  Target TPS.
    /// </summary>
    public int TargetTps { get; set; }
    
    /// <summary>
    ///  Entity count.
    /// </summary>
    public int EntityCount { get; set; }
    
    /// <summary>
    ///  Player count.
    /// </summary>
    public int PlayerCount { get; set; }
    
    /// <summary>
    ///  Average tick time.
    /// </summary>
    public float AvgTickMs { get; set; }
    
    /// <summary>
    ///  Maximum tick time.
    /// </summary>
    public float MaxTickMs { get; set; }
    
    /// <summary>
    ///  Budget time.
    /// </summary>
    public float BudgetMs { get; set; }
}
