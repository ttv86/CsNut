using System;
using System.Collections.Generic;
using OpenTTD;

namespace CsAi
{
    [Author("Timo")]
    public class CsTestAi : AIController
    {
        private int workStatus = 0;
        private int platformSize = 5;
        private AISignList signs = null;

        protected override void Start()
        {
            AICompany.SetLoanAmount(0);
            SignManager signManager = new SignManager();
            TownNetwork tn = new TownNetwork();

            try
            {
                AICompany.SetLoanAmount(AICompany.GetMaxLoanAmount());
                AIRoad.SetCurrentRoadType(AIRoad.ROADTYPE_ROAD);
                foreach (var edge in tn.Graph.Edges)
                {
                    AILog.Info($"Build road from {edge.Node1.Name} to {edge.Node2.Name}.");
                    var stationInfo1 = tn.EnsureStation(edge.Node1);
                    var stationInfo2 = tn.EnsureStation(edge.Node2);

                    RoadBuilder.BuildRoad(
                        stationInfo1.tile,
                        stationInfo1.entryPoint,
                        stationInfo2.entryPoint,
                        stationInfo2.tile,
                        new HashSet<TileIndex>()
                        );

                    var depotInfo1 = tn.EnsureDepot(edge.Node1);
                    var depotInfo2 = tn.EnsureDepot(edge.Node2);
                    
                    tn.MakeRoute(edge.Node1, edge.Node2);
                    tn.MakeRoute(edge.Node2, edge.Node1);
                }
            }
            catch
            {
            }
            
            Sleep(50);
            signManager.ClearSigns();
            var step = 0;
            while (true)
            {
                // Main loop
                //this.setMinLoan();
                CsTestAi.SetMinimumLoanAmount();

                tn.UpdateRoutes();

                Sleep(100);
                step++;
            }
        }

        protected /*override*/ void Start2()
        {
            SignManager signManager = new SignManager();
            this.signs = new AISignList();
            var list = new AIRailTypeList();
            AIRail.SetCurrentRailType(list.Begin());

            AICompany.SetLoanAmount(AICompany.GetMaxLoanAmount());

            ////*
            //RailBuilder.BuildRail(
            //    AIMap.GetTileIndex(42, 121),
            //    AIMap.GetTileIndex(41, 121),
            //    AIMap.GetTileIndex(31, 121),
            //    AIMap.GetTileIndex(30, 121),
            //    new HashSet<TileIndex>(),
            //    1,
            //    this.BuildSign);
            ///*/

            //var i = 39;
            //var ns = RailBuilder.GetNeighbors(
            //    AIMap.GetTileIndex(i, 121),
            //    new RailBuilder.PathInfo(
            //        AIMap.GetTileIndex(i + 1, 121),
            //        1,
            //        1,
            //        RailBuilder.BuildType.Rail,
            //        new RailBuilder.PathInfo(
            //            AIMap.GetTileIndex(i + 2, 121),
            //            1,
            //            1,
            //            RailBuilder.BuildType.Rail,
            //            null)),
            //    this.BuildSign);

            //foreach (var n in ns)
            //{
            //    this.BuildSign(n.Tile, n.Cost.ToString());
            //}
            ///* */

            ///*
            try
            {
                if (this.workStatus == 0)
                {
                    var towns = new AITownList();
                    towns.Valuate(AITown.GetPopulation);
                    RailStationBuildResult s1 = null;
                    foreach (var (town, _) in towns)
                    {
                        if (s1 == null)
                        {
                            s1 = RailStationBuilder.BuildStationNear(AITown.GetLocation(town), this.platformSize, 2);
                        }
                        else
                        {
                            RailStationBuildResult s2 = RailStationBuilder.BuildStationNear(AITown.GetLocation(town), this.platformSize, 2);
                            HashSet<TileIndex> forbidden = new HashSet<TileIndex>();
                            forbidden.Add(s1.ExitFarther);
                            forbidden.Add(s2.EntryFarther);
                            var good = RailBuilder.BuildRail(s1.EntryCloser, s1.EntryFarther, s2.ExitFarther, s2.ExitCloser, forbidden);
                            signManager.ClearSigns();
                            if (good)
                            {
                                forbidden = new HashSet<TileIndex>();
                                good = RailBuilder.BuildRail(s1.ExitCloser, s1.ExitFarther, s2.EntryFarther, s2.EntryCloser, forbidden, 1);
                            }

                            if (good)
                            {
                                if (s1.DepotTile != AIMap.TILE_INVALID)
                                {
                                    TrainManager.BuildTrain(s1.DepotTile, new StationID[] { s1.StationID, s2.StationID }, this.BuildSign);
                                }
                                else
                                {
                                    TrainManager.BuildTrain(s2.DepotTile, new StationID[] { s2.StationID, s1.StationID }, this.BuildSign);
                                }
                            }
                            else
                            {
                                AILog.Info("No route found.");
                            }

                            s1 = null;

                            //if (AICompany.GetBankBalance(AICompany.COMPANY_SELF) < 50000)
                            {
                                // Better to stop before bankrupcy.
                                AILog.Info("Better to stop before bankrupcy.");
                                break;
                            }
                        }
                    }
                }

                this.workStatus = 1;
            }
            catch
            {
                AILog.Error("An error happened");
            }
            /* */

            signManager.ClearSigns();
            AILog.Info("Sign count: " + this.signs.Count());
            AIController.Sleep(150);

            AILog.Info("Remove all signs");
            foreach (var (signId, _) in this.signs)
            {
                AISign.RemoveSign(signId);
            }

            while (true)
            {
                // Main loop
                //this.setMinLoan();
                CsTestAi.SetMinimumLoanAmount();
                Sleep(100);
            }
        }

        protected override object Save()
        {
            return new { };
        }

        private void BuildSign(TileIndex tile, string text)
        {
            AILog.Info("Sign (" + AIMap.GetTileX(tile) + ", " + AIMap.GetTileY(tile) + "): " + text);
            var signId = AISign.BuildSign(tile, "" + text);
            this.signs.AddItem(signId, 0);
        }

        internal static void SetMinimumLoanAmount(long offset = 0)
        {
            var currentLoan = AICompany.GetLoanAmount();
            if (currentLoan == 0)
            {
                return;
            }

            var interval = AICompany.GetLoanInterval();
            var maxLoan = AICompany.GetMaxLoanAmount();
            var currentMoney = AICompany.GetBankBalance(AICompany.COMPANY_SELF) - currentLoan;

            long loan = 0;
            while (((currentMoney + loan) < offset) && (loan < maxLoan))
            {
                loan += interval;
            }

            if (loan != currentLoan)
            {
                AICompany.SetLoanAmount(loan);
            }
        }

        internal static void EnsureMoney(long amount)
        {
            var currentLoan = AICompany.GetLoanAmount();
            var interval = AICompany.GetLoanInterval();
            var maxLoan = AICompany.GetMaxLoanAmount();
            var currentMoney = AICompany.GetBankBalance(AICompany.COMPANY_SELF);
            var loan = currentLoan;
            while ((currentMoney < amount) && (loan < maxLoan))
            {
                loan += interval;
                currentMoney += interval;
            }

            if (loan != currentLoan)
            {
                AICompany.SetLoanAmount(loan);
            }
        }
    }
}
