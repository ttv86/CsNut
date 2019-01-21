using System;
using OpenTTD;

namespace CsAi
{
    public static class TrainManager
    {
        public static void BuildTrain(TileIndex depot, StationID[] stations, Action<TileIndex, string> sign = null)
        {
            var list = new AIEngineList(AIVehicle.VT_RAIL);
            list.Valuate(AIEngine.GetMaxSpeed);
            EngineID passangerWagon = null;
            EngineID mailWagon = null;
            foreach (var (a, b) in list) {
                if (AIEngine.IsBuildable(a) && !AIEngine.IsWagon(a))
                {
                    //AILog.Info(a + ": " + AIEngine.GetName(a) + " | " + AIEngine.GetMaxSpeed(a) + " | " + AIEngine.GetReliability(a) + " | " + AIEngine.GetMaxTractiveEffort(a));
                }
                else if (AIEngine.IsBuildable(a) && AIEngine.IsWagon(a))
                {
                    //AILog.Info(a + ": " + AIEngine.GetName(a) + " | " + AIEngine.GetCargoType(a));
                    if (AICargo.HasCargoClass(AIEngine.GetCargoType(a), AICargo.CC_PASSENGERS))
                    {
                        passangerWagon = a;
                        //AILog.Info("passanger");
                    }
                    else if (AICargo.HasCargoClass(AIEngine.GetCargoType(a), AICargo.CC_MAIL))
                    {
                        mailWagon = a;
                        //AILog.Info("mail");
                    }
                }
            }

            var engineType = list.Begin();
            //AILog.Info("Engine id: " + engineType);
            //AILog.Info("Building: " + AIEngine.GetName(engineType));
            var train = AIVehicle.BuildVehicle(depot, engineType);
            var firstPassanger = AIVehicle.BuildVehicle(depot, passangerWagon);
            AIVehicle.BuildVehicle(depot, passangerWagon);
            AIVehicle.BuildVehicle(depot, passangerWagon);
            var firstMail = AIVehicle.BuildVehicle(depot, mailWagon);
            AIVehicle.MoveWagonChain(firstMail, 0, train, 0);
            AIVehicle.MoveWagonChain(firstPassanger, 0, train, 0);

            for (var i = 0; i < stations.Length; i++)
            {
                AIOrder.AppendOrder(train, AIStation.GetLocation(stations[i]), AIOrder.OF_NONE);
            }

            AIVehicle.StartStopVehicle(train);
        }
    }
}
