namespace OpenTTD
{
    /**
     * Class that handles all event related functions.
     * You can lookup the type, and than convert it to the real event-class.
     * That way you can request more detailed information about the event.
     */
    public class AIEvent
    {
        /// <summary>The type of event. Needed to lookup the detailed class.</summary>
        public enum AIEventType
        {
            ET_INVALID = 0,
            ET_TEST,
            ET_SUBSIDY_OFFER,
            ET_SUBSIDY_OFFER_EXPIRED,
            ET_SUBSIDY_AWARDED,
            ET_SUBSIDY_EXPIRED,
            ET_ENGINE_PREVIEW,
            ET_COMPANY_NEW,
            ET_COMPANY_IN_TROUBLE,
            ET_COMPANY_ASK_MERGER,
            ET_COMPANY_MERGER,
            ET_COMPANY_BANKRUPT,
            ET_VEHICLE_CRASHED,
            ET_VEHICLE_LOST,
            ET_VEHICLE_WAITING_IN_DEPOT,
            ET_VEHICLE_UNPROFITABLE,
            ET_INDUSTRY_OPEN,
            ET_INDUSTRY_CLOSE,
            ET_ENGINE_AVAILABLE,
            ET_STATION_FIRST_VEHICLE,
            ET_DISASTER_ZEPPELINER_CRASHED,
            ET_DISASTER_ZEPPELINER_CLEARED,
            ET_TOWN_FOUNDED,
            ET_AIRCRAFT_DEST_TOO_FAR,
            ET_ADMIN_PORT,
            ET_WINDOW_WIDGET_CLICK,
            ET_GOAL_QUESTION_ANSWER,
            ET_EXCLUSIVE_TRANSPORT_RIGHTS,
            ET_ROAD_RECONSTRUCTION,
        }

        /// <summary>Constructor of AIEvent, to get the type of event.</summary>
        public AIEvent(AIEvent.AIEventType type) { throw null; }

        /// <summary>Get the event-type.</summary><returns>The @c AIEventType.</returns>
        AIEvent.AIEventType GetEventType() { throw null; }
    }

    /**
     * Class that handles all event related functions.
     * @note it is not needed to create an instance of AIEvent to access it, as
     *  all members are static, and all data is stored script instance-wide.
     */
    public static class AIEventController
    {
        /// <summary>Check if there is an event waiting.</summary><returns>true if there is an event on the stack.</returns>
        public static bool IsEventWaiting() { throw null; }

        /// <summary>Get the next event.</summary><returns>a class of the event-child issues.</returns>
        public static AIEvent GetNextEvent() { throw null; }
    }

    /**
     * Event Vehicle Crash, indicating a vehicle of yours is crashed.
     *  It contains the crash site, the crashed vehicle and the reason for the crash.
     */
    public class AIEventVehicleCrashed : AIEvent
    {
        /// <summary>The reasons for vehicle crashes</summary>
        public enum CrashReason
        {
            CRASH_TRAIN,
            CRASH_RV_LEVEL_CROSSING,
            CRASH_RV_UFO,
            CRASH_PLANE_LANDING,
            CRASH_AIRCRAFT_NO_AIRPORT,
            CRASH_FLOODED,
        }

        /// <param name="vehicle">The vehicle that crashed.</param>
        /// <param name="crash_site">Where the vehicle crashed.</param>
        /// <param name="crash_reason">The reason why the vehicle crashed.</param>
        public AIEventVehicleCrashed(VehicleID vehicle, TileIndex crash_site, AIEventVehicleCrashed.CrashReason crash_reason)
            : base(AIEventType.ET_VEHICLE_CRASHED)
        { throw null; }

        /**
         * Convert an AIEvent to the real instance.
         * @param instance The instance to convert.
         * @returns The converted instance.
         */
        static public AIEventVehicleCrashed Convert(AIEvent instance) { throw null; }

        /// <summary>Get the VehicleID of the crashed vehicle.</summary><returns>The crashed vehicle.</returns>
        public VehicleID GetVehicleID() { throw null; }

        /// <summary>Find the tile the vehicle crashed.</summary><returns>The crash site.</returns>
        public TileIndex GetCrashSite() { throw null; }

        /// <summary>Get the reason for crashing</summary><returns>The reason for crashing</returns>
        public AIEventVehicleCrashed.CrashReason GetCrashReason() { throw null; }
    }

    /// <summary>Event Subsidy Offered, indicating someone offered a subsidy.</summary>
    public class AIEventSubsidyOffer : AIEvent
    {
        /// <param name="subsidy_id">The index of this subsidy in the _subsidies array.</param>
        public AIEventSubsidyOffer(SubsidyID subsidy_id) : base(AIEventType.ET_SUBSIDY_OFFER) { throw null; }

        /**
         * Convert an AIEvent to the real instance.
         * @param instance The instance to convert.
         * @returns The converted instance.
         */
        static public AIEventSubsidyOffer Convert(AIEvent instance) { throw null; }

        /// <summary>Get the SubsidyID of the subsidy.</summary><returns>The subsidy id.</returns>
        public SubsidyID GetSubsidyID() { throw null; }
    }

    /// <summary>Event Subsidy Offer Expired, indicating a subsidy will no longer be awarded.</summary>
    public class AIEventSubsidyOfferExpired : AIEvent
    {
        /// <param name="subsidy_id">The index of this subsidy in the _subsidies array.</param>
        public AIEventSubsidyOfferExpired(SubsidyID subsidy_id) : base(AIEventType.ET_SUBSIDY_OFFER_EXPIRED) { throw null; }

        /**
         * Convert an AIEvent to the real instance.
         * @param instance The instance to convert.
         * @returns The converted instance.
         */
        static public AIEventSubsidyOfferExpired Convert(AIEvent instance) { throw null; }

        /// <summary>Get the SubsidyID of the subsidy.</summary><returns>The subsidy id.</returns>
        public SubsidyID GetSubsidyID() { throw null; }
    }

    /// <summary>Event Subsidy Awarded, indicating a subsidy is awarded to some company.</summary>
    public class AIEventSubsidyAwarded : AIEvent
    {
        /// <param name="subsidy_id">The index of this subsidy in the _subsidies array.</param>
        public AIEventSubsidyAwarded(SubsidyID subsidy_id) : base(AIEventType.ET_SUBSIDY_AWARDED) { throw null; }

        /**
         * Convert an AIEvent to the real instance.
         * @param instance The instance to convert.
         * @returns The converted instance.
         */
        static public AIEventSubsidyAwarded Convert(AIEvent instance) { throw null; }

        /// <summary>Get the SubsidyID of the subsidy.</summary><returns>The subsidy id.</returns>
        public SubsidyID GetSubsidyID() { throw null; }
    }

    /// <summary>Event Subsidy Expired, indicating a route that was once subsidized no longer is.</summary>
    public class AIEventSubsidyExpired : AIEvent
    {
        /// <param name="subsidy_id">The index of this subsidy in the _subsidies array.</param>
        public AIEventSubsidyExpired(SubsidyID subsidy_id) : base(AIEventType.ET_SUBSIDY_EXPIRED) { throw null; }

        /**
         * Convert an AIEvent to the real instance.
         * @param instance The instance to convert.
         * @returns The converted instance.
         */
        static public AIEventSubsidyExpired Convert(AIEvent instance) { throw null; }

        /// <summary>Get the SubsidyID of the subsidy.</summary><returns>The subsidy id.</returns>
        public SubsidyID GetSubsidyID() { throw null; }
    }

    /**
     * Event Engine Preview, indicating a manufacturer offer you to test a new engine.
     *  You can get the same information about the offered engine as a real user
     *  would see in the offer window. And you can also accept the offer.
     */
    public class AIEventEnginePreview : AIEvent
    {
        /// <param name="engine">The engine offered to test.</param>
        public AIEventEnginePreview(EngineID engine) : base(AIEventType.ET_ENGINE_PREVIEW) { throw null; }

        /**
         * Convert an AIEvent to the real instance.
         * @param instance The instance to convert.
         * @returns The converted instance.
         */
        static public AIEventEnginePreview Convert(AIEvent instance) { throw null; }

        /// <summary>Get the name of the offered engine.</summary><returns>The name the engine has.</returns>
        public string GetName() { throw null; }

        /**
         * Get the cargo-type of the offered engine. In case it can transport multiple cargoes, it
         *  returns the first/main.
         * @returns The cargo-type of the engine.
         */
        public CargoID GetCargoType() { throw null; }

        /**
         * Get the capacity of the offered engine. In case it can transport multiple cargoes, it
         *  returns the first/main.
         * @returns The capacity of the engine.
         */
        public int GetCapacity() { throw null; }

        /**
         * Get the maximum speed of the offered engine.
         * @returns The maximum speed the engine has.
         * @note The speed is in OpenTTD's internal speed unit.
         *       This is mph / 1.6, which is roughly km/h.
         *       To get km/h multiply this number by 1.00584.
         */
        public int GetMaxSpeed() { throw null; }

        /// <summary>Get the new cost of the offered engine.</summary><returns>The new cost the engine has.</returns>
        public long GetPrice() { throw null; }

        /**
         * Get the running cost of the offered engine.
         * @returns The running cost of the vehicle per year.
         * @note Cost is per year; divide by 365 to get per day.
         */
        public long GetRunningCost() { throw null; }

        /// <summary>Get the type of the offered engine.</summary><returns>The type the engine has.</returns>
        public VehicleType GetVehicleType() { throw null; }

        /// <summary>Accept the engine preview.</summary><returns>True when the accepting succeeded.</returns>
        public bool AcceptPreview() { throw null; }
    }

    /// <summary>Event Company New, indicating a new company has been created.</summary>
    public class AIEventCompanyNew : AIEvent
    {
        /// <param name="owner">The new company.</param>
        public AIEventCompanyNew(Owner owner) : base(AIEventType.ET_COMPANY_NEW) { throw null; }

        /**
         * Convert an AIEvent to the real instance.
         * @param instance The instance to convert.
         * @returns The converted instance.
         */
        static public AIEventCompanyNew Convert(AIEvent instance) { throw null; }

        /// <summary>Get the CompanyID of the company that has been created.</summary><returns>The CompanyID of the company.</returns>
        public CompanyID GetCompanyID() { throw null; }
    }

    /**
     * Event Company In Trouble, indicating a company is in trouble and might go
     *  bankrupt soon.
     */
    public class AIEventCompanyInTrouble : AIEvent
    {
        /// <param name="owner">The company that is in trouble.</param>
        public AIEventCompanyInTrouble(Owner owner) : base(AIEventType.ET_COMPANY_IN_TROUBLE) { throw null; }

        /**
         * Convert an AIEvent to the real instance.
         * @param instance The instance to convert.
         * @returns The converted instance.
         */
        static public AIEventCompanyInTrouble Convert(AIEvent instance) { throw null; }

        /// <summary>Get the CompanyID of the company that is in trouble.</summary>
        /// <returns>The CompanyID of the company in trouble.</returns>
        public CompanyID GetCompanyID() { throw null; }
    }

    /// <summary>Event Company Ask Merger, indicating a company can be bought (cheaply) by you.</summary>
    public class AIEventCompanyAskMerger : AIEvent
    {
        /// <param name="owner">The company that can be bought.</param>
        /// <param name="value">The value/costs of buying the company.</param>
        public AIEventCompanyAskMerger(Owner owner, int value) : base(AIEventType.ET_COMPANY_ASK_MERGER) { throw null; }

        /**
         * Convert an AIEvent to the real instance.
         * @param instance The instance to convert.
         * @returns The converted instance.
         */
        static public AIEventCompanyAskMerger Convert(AIEvent instance) { throw null; }

        /**
         * Get the CompanyID of the company that can be bought.
         * @returns The CompanyID of the company that can be bought.
         * @note If the company is bought this will become invalid.
         */
        public CompanyID GetCompanyID() { throw null; }

        /// <summary>Get the value of the new company.</summary><returns>The value of the new company.</returns>
        public int GetValue() { throw null; }

        /// <summary>Take over the company for this merger.</summary><returns>true if the merger was a success.</returns>
        public bool AcceptMerger() { throw null; }
    }

    /**
     * Event Company Merger, indicating a company has been bought by another
     *  company.
     */
    public class AIEventCompanyMerger : AIEvent
    {
        /// <param name="old_owner">The company bought off.</param><param name="new_owner">The company that bought owner.</param>
        public AIEventCompanyMerger(Owner old_owner, Owner new_owner) : base(AIEventType.ET_COMPANY_MERGER) { throw null; }

        /**
         * Convert an AIEvent to the real instance.
         * @param instance The instance to convert.
         * @returns The converted instance.
         */
        static public AIEventCompanyMerger Convert(AIEvent instance) { throw null; }

        /**
         * Get the CompanyID of the company that has been bought.
         * @returns The CompanyID of the company that has been bought.
         * @The note value below is not valid anymore as CompanyID, and
         *  AICompany.ResolveCompanyID will return COMPANY_COMPANY. It's
         *  only useful if you're keeping track of company's yourself.
         */
        public CompanyID GetOldCompanyID() { throw null; }

        /// <summary>Get the CompanyID of the new owner.</summary><returns>The CompanyID of the new owner.</returns>
        public CompanyID GetNewCompanyID() { throw null; }
    }

    /// <summary>Event Company Bankrupt, indicating a company has gone bankrupt.</summary>
    public class AIEventCompanyBankrupt : AIEvent
    {
        /// <param name="owner">The company that has gone bankrupt.</param>
        public AIEventCompanyBankrupt(Owner owner) : base(AIEventType.ET_COMPANY_BANKRUPT) { throw null; }

        /**
         * Convert an AIEvent to the real instance.
         * @param instance The instance to convert.
         * @returns The converted instance.
         */
        static AIEventCompanyBankrupt Convert(AIEvent instance) { throw null; }

        /// <summary>Get the CompanyID of the company that has gone bankrupt.</summary><returns>The CompanyID of the company that has gone bankrupt.</returns>
        public CompanyID GetCompanyID() { throw null; }
    }

    /// <summary>Event Vehicle Lost, indicating a vehicle can't find its way to its destination.</summary>
    public class AIEventVehicleLost : AIEvent
    {
        /// <param name="vehicle_id">The vehicle that is lost.</param>
        public AIEventVehicleLost(VehicleID vehicle_id) : base(AIEventType.ET_VEHICLE_LOST) { throw null; }

        /**
         * Convert an AIEvent to the real instance.
         * @param instance The instance to convert.
         * @returns The converted instance.
         */
        static public AIEventVehicleLost Convert(AIEvent instance) { throw null; }

        /// <summary>Get the VehicleID of the vehicle that is lost.</summary><returns>The VehicleID of the vehicle that is lost.</returns>
        public VehicleID GetVehicleID() { throw null; }
    }

    /// <summary>Event VehicleWaitingInDepot, indicating a vehicle has arrived a depot and is now waiting there.</summary>
    public class AIEventVehicleWaitingInDepot : AIEvent
    {
        /// <param name="vehicle_id">The vehicle that is waiting in a depot.</param>
        public AIEventVehicleWaitingInDepot(VehicleID vehicle_id) : base(AIEventType.ET_VEHICLE_WAITING_IN_DEPOT) { throw null; }

        /**
         * Convert an AIEvent to the real instance.
         * @param instance The instance to convert.
         * @returns The converted instance.
         */
        static public AIEventVehicleWaitingInDepot Convert(AIEvent instance) { throw null; }

        /// <summary>Get the VehicleID of the vehicle that is waiting in a depot.</summary><returns>The VehicleID of the vehicle that is waiting in a depot.</returns>
        public VehicleID GetVehicleID() { throw null; }
    }

    /// <summary>Event Vehicle Unprofitable, indicating a vehicle lost money last year.</summary>
    public class AIEventVehicleUnprofitable : AIEvent
    {
        /// <param name="vehicle_id">The vehicle that was unprofitable.</param>
        public AIEventVehicleUnprofitable(VehicleID vehicle_id) : base(AIEventType.ET_VEHICLE_UNPROFITABLE) { throw null; }

        /**
         * Convert an AIEvent to the real instance.
         * @param instance The instance to convert.
         * @returns The converted instance.
         */
        static public AIEventVehicleUnprofitable Convert(AIEvent instance) { throw null; }

        /// <summary>Get the VehicleID of the vehicle that lost money.</summary><returns>The VehicleID of the vehicle that lost money.</returns>
        public VehicleID GetVehicleID() { throw null; }
    }

    /// <summary>Event Industry Open, indicating a new industry has been created.</summary>
    public class AIEventIndustryOpen : AIEvent
    {
        /// <param name="industry_id">The new industry.</param>
        public AIEventIndustryOpen(IndustryID industry_id) : base(AIEventType.ET_INDUSTRY_OPEN) { throw null; }

        /**
         * Convert an AIEvent to the real instance.
         * @param instance The instance to convert.
         * @returns The converted instance.
         */
        static public AIEventIndustryOpen Convert(AIEvent instance) { throw null; }

        /// <summary>Get the IndustryID of the new industry.</summary><returns>The IndustryID of the industry.</returns>
        public IndustryID GetIndustryID() { throw null; }
    }

    /// <summary>Event Industry Close, indicating an industry is going to be closed.</summary>
    public class AIEventIndustryClose : AIEvent
    {
        /// <param name="industry_id">The new industry.</param>
        public AIEventIndustryClose(IndustryID industry_id) : base(AIEventType.ET_INDUSTRY_CLOSE) { throw null; }

        /**
         * Convert an AIEvent to the real instance.
         * @param instance The instance to convert.
         * @returns The converted instance.
         */
        static public AIEventIndustryClose Convert(AIEvent instance) { throw null; }

        /// <summary>Get the IndustryID of the closing industry.</summary><returns>The IndustryID of the industry.</returns>
        public IndustryID GetIndustryID() { throw null; }
    }

    /// <summary>Event Engine Available, indicating a new engine is available.</summary>
    public class AIEventEngineAvailable : AIEvent
    {
        /// <param name="engine">The engine that is available.</param>
        public AIEventEngineAvailable(EngineID engine) : base(AIEventType.ET_ENGINE_AVAILABLE) { throw null; }

        /**
         * Convert an AIEvent to the real instance.
         * @param instance The instance to convert.
         * @returns The converted instance.
         */
        static public AIEventEngineAvailable Convert(AIEvent instance) { throw null; }

        /// <summary>Get the EngineID of the new engine.</summary><returns>The EngineID of the new engine.</returns>
        public EngineID GetEngineID() { throw null; }
    }

    /// <summary>Event Station First Vehicle, indicating a station has been visited by a vehicle for the first time.</summary>
    public class AIEventStationFirstVehicle : AIEvent
    {
        /// <param name="station">The station visited for the first time.</param><param name="vehicle">The vehicle visiting the station.</param>
        public AIEventStationFirstVehicle(StationID station, VehicleID vehicle) : base(AIEventType.ET_STATION_FIRST_VEHICLE) { throw null; }

        /**
         * Convert an AIEvent to the real instance.
         * @param instance The instance to convert.
         * @returns The converted instance.
         */
        static public AIEventStationFirstVehicle Convert(AIEvent instance) { throw null; }

        /// <summary>Get the StationID of the visited station.</summary><returns>The StationID of the visited station.</returns>
        public StationID GetStationID() { throw null; }

        /// <summary>Get the VehicleID of the first vehicle.</summary><returns>The VehicleID of the first vehicle.</returns>
        public VehicleID GetVehicleID() { throw null; }
    }

    /// <summary>Event Disaster Zeppeliner Crashed, indicating a zeppeliner has crashed on an airport and is blocking the runway.</summary>
    public class AIEventDisasterZeppelinerCrashed : AIEvent
    {
        /// <param name="station">The station containing the affected airport</param>
        public AIEventDisasterZeppelinerCrashed(StationID station) : base(AIEventType.ET_DISASTER_ZEPPELINER_CRASHED) { throw null; }

        /**
         * Convert an AIEvent to the real instance.
         * @param instance The instance to convert.
         * @returns The converted instance.
         */
        static public AIEventDisasterZeppelinerCrashed Convert(AIEvent instance) { throw null; }

        /// <summary>Get the StationID of the station containing the affected airport.</summary><returns>The StationID of the station containing the affected airport.</returns>
        public StationID GetStationID() { throw null; }
    }

    /// <summary>Event Disaster Zeppeliner Cleared, indicating a previously crashed zeppeliner has been removed, and the airport is operating again.</summary>
    public class AIEventDisasterZeppelinerCleared : AIEvent
    {
        /// <param name="station">The station containing the affected airport</param>
        public AIEventDisasterZeppelinerCleared(StationID station) : base(AIEventType.ET_DISASTER_ZEPPELINER_CLEARED) { throw null; }

        /**
         * Convert an AIEvent to the real instance.
         * @param instance The instance to convert.
         * @returns The converted instance.
         */
        static public AIEventDisasterZeppelinerCleared Convert(AIEvent instance) { throw null; }

        /// <summary>Get the StationID of the station containing the affected airport.</summary><returns>The StationID of the station containing the affected airport.</returns>
        public StationID GetStationID() { throw null; }
    }

    /// <summary>Event Town Founded, indicating a new town has been created.</summary>
    public class AIEventTownFounded : AIEvent
    {
        /// <param name="town">The town that was created.</param>
        public AIEventTownFounded(TownID town) : base(AIEventType.ET_TOWN_FOUNDED) { throw null; }

        /**
         * Convert an AIEvent to the real instance.
         * @param instance The instance to convert.
         * @returns The converted instance.
         */
        static public AIEventTownFounded Convert(AIEvent instance) { throw null; }

        /// <summary>Get the TownID of the town.</summary><returns>The TownID of the town that was created.</returns>
        public TownID GetTownID() { throw null; }
    }

    /**
     * Event AircraftDestTooFar, indicating the next destination of an aircraft is too far away.
     * This event can be trigger when the current oder of an aircraft changes, usually either when
     * loading is done or when switch manually.
     */
    public class AIEventAircraftDestTooFar : AIEvent
    {
        /// <param name="vehicle_id">The aircraft whose destination is too far away.</param>
        public AIEventAircraftDestTooFar(VehicleID vehicle_id) : base(AIEventType.ET_AIRCRAFT_DEST_TOO_FAR) { throw null; }

        /**
         * Convert an AIEvent to the real instance.
         * @param instance The instance to convert.
         * @returns The converted instance.
         */
        static public AIEventAircraftDestTooFar Convert(AIEvent instance) { throw null; }

        /// <summary>Get the VehicleID of the aircraft whose destination is too far away.</summary><returns>The VehicleID of the aircraft whose destination is too far away.</returns>
        public VehicleID GetVehicleID() { throw null; }
    }

    /// <summary>Base class for events involving a town and a company.</summary>
    public class AIEventCompanyTown : AIEvent
    {
        /// <param name="event">The eventtype.</param><param name="company">The company.</param><param name="town">The town.</param>
        public AIEventCompanyTown(AIEvent.AIEventType @event, CompanyID company, TownID town) : base(@event) { throw null; }

        /**
         * Convert an AIEvent to the real instance.
         * @param instance The instance to convert.
         * @returns The converted instance.
         */
        static public AIEventCompanyTown Convert(AIEvent instance) { throw null; }

        /// <summary>Get the CompanyID of the company.</summary><returns>The CompanyID of the company involved into the event.</returns>
        public CompanyID GetCompanyID() { throw null; }

        /// <summary>Get the TownID of the town.</summary><returns>The TownID of the town involved into the event.</returns>
        public TownID GetTownID() { throw null; }
    }

    /**
     * Event Exclusive Transport Rights, indicating that company bought
     * exclusive transport rights in a town.
     */
    public class AIEventExclusiveTransportRights : AIEventCompanyTown
    {
        /// <param name="company">The company.</param><param name="town">The town.</param>
        public AIEventExclusiveTransportRights(CompanyID company, TownID town)
            : base(AIEventType.ET_EXCLUSIVE_TRANSPORT_RIGHTS, company, town) { throw null; }

        /**
         * Convert an AIEvent to the real instance.
         * @param instance The instance to convert.
         * @returns The converted instance.
         */
        static public AIEventExclusiveTransportRights Convert(AIEventCompanyTown instance) { throw null; }
    }

    /**
     * Event Road Reconstruction, indicating that company triggered
     * road reconstructions in a town.
     */
    public class AIEventRoadReconstruction : AIEventCompanyTown
    {
        /// <param name="company">The company.</param><param name="town">The town.</param>
        public AIEventRoadReconstruction(CompanyID company, TownID town) : base(AIEventType.ET_ROAD_RECONSTRUCTION, company, town) { throw null; }

        /**
         * Convert an AIEvent to the real instance.
         * @param instance The instance to convert.
         * @returns The converted instance.
         */
        static public AIEventRoadReconstruction Convert(AIEventCompanyTown instance) { throw null; }
    }

}
