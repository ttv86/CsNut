namespace OpenTTD
{
    public class CargoID { internal CargoID() { } }
    public class GroupID { internal GroupID() { } }
    public class EngineID { internal EngineID() { } }
    public class IndustryID { internal IndustryID() { } }
    public class IndustryType { internal IndustryType() { } }
    public class SignID { internal SignID() { } }
    public class OrderFlags { internal OrderFlags() { } }
    public class CargoClass { internal CargoClass() { } }
    public class RailTrack { internal RailTrack() { } }
    public class SignalType { internal SignalType() { } }
    public class Slope { internal Slope() { } }
    public class VehicleType { internal VehicleType() { } }
    public class RailType { internal RailType() { } }
    public class AirportType { internal AirportType() { } }
    public class PlaneType { internal PlaneType() { } }
    public class Quarter { internal Quarter() { } }
    public class CompanyID { internal CompanyID() { } }
    public class StationID { internal StationID() { } }
    public class StationType { internal StationType() { } }
    public class SubsidyID { internal SubsidyID() { } }
    public class RoadVehicleType { internal RoadVehicleType() { } }
    public class TileIndex
    {
        internal TileIndex(int index)
        {
            this.Index = index;
        }

        internal int Index { get; set; }

        public static TileIndex operator +(TileIndex a, TileIndex b)
        {
            return new TileIndex(a.Index + b.Index);
        }

        public static TileIndex operator -(TileIndex a, TileIndex b)
        {
            return new TileIndex(a.Index - b.Index);
        }

        public static bool operator ==(TileIndex a, TileIndex b)
        {
            return a.Index == b.Index;
        }

        public static bool operator !=(TileIndex a, TileIndex b)
        {
            return a.Index != b.Index;
        }

        public override int GetHashCode()
        {
            return this.Index;
        }

        public override bool Equals(object obj)
        {
            if (obj is TileIndex other)
            {
                return this.Index == other.Index;
            }

            return false;
        }

        public override string ToString()
        {
            if ((Testing.AIMap.GetTileX != null) && (Testing.AIMap.GetTileY != null))
            {
                return Testing.AIMap.GetTileX(this) + ", " + Testing.AIMap.GetTileY(this);
            }

            return this.Index.ToString();
        }
    }
    public class TownID { internal TownID() { } }
    public class VehicleID { internal VehicleID() { } }
    public class AIErrorType { internal AIErrorType() { } }
    public class BridgeID { internal BridgeID() { } }
    public class TownEffect { internal TownEffect() { } }
    public class SpecialCargoID { internal SpecialCargoID() { } }
    public class DistributionType { internal DistributionType() { } }
    public class WaypointType { internal WaypointType() { } }
    public class TownAction { internal TownAction() { } }
    public class TownRating { internal TownRating() { } }
    public class RoadLayout { internal RoadLayout() { } }
    public class TownSize { internal TownSize() { } }
    public class TownGrowth { internal TownGrowth() { } }
    public class Corner { internal Corner() { } }
    public class TransportType { internal TransportType() { } }
    public class BuildType { internal BuildType() { } }
    public class TerrainType { internal TerrainType() { } }
    public class VehicleState { internal VehicleState() { } }
    public class Date { internal Date() { } }
    public class Gender { internal Gender() { } }
    public class RoadType { internal RoadType() { } }
    public class ErrorMessage { internal ErrorMessage() { } }
    public class ErrorCategory { internal ErrorCategory() { } }
}