using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTTD;

namespace CsAi
{
    class TownNetwork
    {
        private Dictionary<TownID, RoadStationInfo> stations = new Dictionary<TownID, RoadStationInfo>();
        private Dictionary<TownID, RoadStationInfo> depots = new Dictionary<TownID, RoadStationInfo>();
        private Dictionary<Edge<TownNode>, List<VehicleID>> vehicles = new Dictionary<Edge<TownNode>, List<VehicleID>>();

        public TownNetwork()
        {
            List<Graph<TownNode>> graphs = new List<Graph<TownNode>>();
            var towns = new AITownList();
            foreach (var (town, _) in towns)
            {
                graphs.Add(new Graph<TownNode>(new TownNode(town, new Point(AITown.GetLocation(town)), AITown.GetName(town))));
            }

            while (graphs.Count > 1)
            {
                //if (graphs.Count % 10 == 0)
                //{
                //    AILog.Info($"Count: {graphs.Count}");
                //}

                int graphMin = 100000;
                Graph<TownNode> graph1 = null;
                Graph<TownNode> graph2 = null;
                TownNode bestNode1 = null;
                TownNode bestNode2 = null;
                int graph2Index = -1;
                for (int a = 0; a < graphs.Count; a++)
                {
                    for (int b = 0; b < graphs.Count; b++)
                    {
                        if (a != b)
                        {
                            TownNode node1 = null;
                            TownNode node2 = null;
                            int dist = graphMin;
                            foreach (var n1 in graphs[a].Nodes)
                            {
                                foreach (var n2 in graphs[b].Nodes)
                                {
                                    var nodeDist = n1.GetDistance(n2);
                                    if (nodeDist < dist)
                                    {
                                        dist = nodeDist;
                                        node1 = n1;
                                        node2 = n2;
                                    }
                                }
                            }

                            if (dist < graphMin)
                            {
                                bestNode1 = node1;
                                bestNode2 = node2;
                                graph1 = graphs[a];
                                graph2 = graphs[b];
                                graph2Index = b;
                                graphMin = dist;
                            }
                        }
                    }
                }
                
                graph1.Nodes.AddRange(graph2.Nodes);
                graph1.Edges.AddRange(graph2.Edges);
                graph1.Edges.Add(new Edge<TownNode>() { Node1 = bestNode1, Node2 = bestNode2 });
                graphs.RemoveAt(graph2Index);
                AILog.Info($"Combine {bestNode1.Name} and {bestNode2.Name} ({graphMin}).");
            }

            this.Graph = graphs[0];
            foreach (var edge in this.Graph.Edges)
            {
                AILog.Info($"- {edge.Node1.Name} <-> {edge.Node2.Name}");
            }
        }

        internal void UpdateRoutes()
        {
            AILog.Error(" -- UpdateRoutes --");
            CargoID passangerCargo = null;
            var cargoList = new AICargoList();
            foreach (var (cargo, _) in cargoList)
            {
                if (AICargo.HasCargoClass(cargo, AICargo.CC_PASSENGERS))
                {
                    passangerCargo = cargo;
                }
            }

            if (passangerCargo == null)
            {
                AILog.Error("No passanger cargo found.");
                return;
            }

            foreach (var edge in this.Graph.Edges)
            {
                if (!this.vehicles.ContainsKey(edge))
                {
                    continue;
                }

                var list = this.vehicles[edge];
                var s1 = AIStation.GetStationID(stations[edge.Node1.TownId].tile);
                var s2 = AIStation.GetStationID(stations[edge.Node2.TownId].tile);
                var waiting1 = AIStation.GetCargoWaitingVia(s1, s2, passangerCargo);
                var waiting2 = AIStation.GetCargoWaitingVia(s2, s1, passangerCargo);
                var waiting = waiting1 + waiting2;

                AILog.Info($"Route from {edge.Node1.Name} to {edge.Node2.Name} has {waiting} passangers waiting.");
                var target = (waiting / 31) - 1; // TODO: Capacity
                AILog.Info($"{list.Count} vehicles operating. Target: {target}");
                if (list.Count < target)
                {
                    if (waiting1 > waiting2)
                    {
                        MakeRoute(edge.Node1, edge.Node2);
                    }
                    else
                    {
                        MakeRoute(edge.Node2, edge.Node1);
                    }
                }
            }

            //var stations = new AIStationList(AIStation.STATION_BUS_STOP);
            //foreach (var (s1, _) in stations)
            //{
            //    AILog.Info(AIStation.GetName(s1) + ":");
            //    var stations2 = new AIStationList(AIStation.STATION_BUS_STOP);
            //    var totalWaiting = AIStation.GetCargoWaiting(s1, passangerCargo);
            //    foreach (var (s2, _) in stations2)
            //    {
            //        if (s1 != s2)
            //        {
            //            var waiting = AIStation.GetCargoWaitingVia(s1, s2, passangerCargo);
            //            AILog.Info($"- {AIStation.GetName(s2)}: {waiting}");
            //            totalWaiting -= waiting;
            //        }
            //    }

            //    AILog.Info($"- any: {totalWaiting}");
            //}
        }

        internal bool MakeRoute(TownNode fromTown, TownNode toTown)
        {
            AILog.Warning($"Building a route from {fromTown.Name} to {toTown.Name}.");
            RoadStationInfo fromStationTile = stations[fromTown.TownId];
            RoadStationInfo toStationTile = stations[toTown.TownId];

            Edge<TownNode> foundEdge = null;
            foreach (var edge in this.Graph.Edges)
            {
                if (((edge.Node1 == fromTown) && (edge.Node2 == toTown)) || ((edge.Node2 == fromTown) && (edge.Node1 == toTown)))
                {
                    foundEdge = edge;
                }
            }

            if (foundEdge == null)
            {
                AILog.Warning($"No route found from {fromTown.Name} to {toTown.Name}.");
                return false;
            }

            var list = new AIEngineList(AIVehicle.VT_ROAD);
            list.Valuate(AIEngine.GetMaxSpeed);
            VehicleID vehicleId = null;
            foreach (var (engineType, _) in list)
            {
                if (AICargo.HasCargoClass(AIEngine.GetCargoType(engineType), AICargo.CC_PASSENGERS))
                {
                    var price = AIEngine.GetPrice(engineType);
                    CsTestAi.EnsureMoney(price + 1000);
                    vehicleId = AIVehicle.BuildVehicle(depots[fromTown.TownId].tile, engineType);
                    break;
                }
            }

            if (vehicleId == null)
            {
                AILog.Error("No passnger vehicle found.");
            }

            if (AIVehicle.IsValidVehicle(vehicleId))
            {
                AIOrder.AppendOrder(vehicleId, fromStationTile.tile, AIOrder.OF_NONE);
                AIOrder.AppendOrder(vehicleId, toStationTile.tile, AIOrder.OF_NONE);
                AIVehicle.StartStopVehicle(vehicleId);
                if (!vehicles.ContainsKey(foundEdge))
                {
                    vehicles.Add(foundEdge, new List<VehicleID>());
                }

                var vehicleList = vehicles[foundEdge];
                vehicleList.Add(vehicleId);
                AILog.Info($"Route contains now {vehicleList.Count} vehicles.");
                return true;
            }

            AILog.Error("Invalid vehicle.");
            return false;
        }

        internal RoadStationInfo EnsureStation(TownNode townNode)
        {
            if (this.stations.ContainsKey(townNode.TownId))
            {
                return this.stations[townNode.TownId];
            }

            var place = RoadStationBuilder.FindPlaceForStation(townNode.Location.Tile);
            AIRoad.BuildRoadStation(place.tile, place.entryPoint, AIRoad.ROADVEHTYPE_BUS, AIStation.STATION_NEW);
            AIRoad.BuildRoad(place.tile, place.entryPoint);
            this.stations.Add(townNode.TownId, place);
            return place;
        }

        internal RoadStationInfo EnsureDepot(TownNode townNode)
        {
            if (this.depots.ContainsKey(townNode.TownId))
            {
                return this.depots[townNode.TownId];
            }

            var place = RoadStationBuilder.FindPlaceForStation(townNode.Location.Tile);
            if (place != null)
            {
                AIRoad.BuildRoadDepot(place.tile, place.entryPoint);
                AIRoad.BuildRoad(place.tile, place.entryPoint);
                this.depots.Add(townNode.TownId, place);
                return place;
            }
            else
            {
                return null;
            }
        }

        public Graph<TownNode> Graph { get; }
    }

    class TownNode : IGraphNode<TownNode>
    {
        public TownNode(TownID townId, Point location, string name)
        {
            this.TownId = townId;
            this.Location = location;
            this.Name = name;
        }

        public TownID TownId { get; }

        public Point Location { get; }

        public string Name { get; }

        public int Population => AITown.GetPopulation(this.TownId);

        public int GetDistance(TownNode other) => this.Location.Distance(other.Location);

        public override string ToString()
        {
            return this.Name;
        }
    }
}
