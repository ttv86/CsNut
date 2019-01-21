using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenTTD;

namespace CsAi.UnitTests
{
    [TestClass]
    public class RailBuilderTests
    {
        [TestMethod]
        public void FindRouteTest1()
        {
            using (var world = new OpenTTD.Testing.TestWorld(20, 20))
            {
                var path = RailBuilder.FindPath(AIMap.GetTileIndex(10, 10), AIMap.GetTileIndex(15, 15), AIMap.GetTileIndex(9, 10));
                foreach (var item in path)
                {
                    Debug.WriteLine(item.Tile);
                }

                Assert.AreEqual(12, path.Count);
                Assert.AreEqual(AIMap.GetTileIndex(15, 15), path[0].Tile);
                Assert.AreEqual(AIMap.GetTileIndex(14, 14), path[2].Tile);
                Assert.AreEqual(AIMap.GetTileIndex(13, 13), path[4].Tile);
                Assert.AreEqual(AIMap.GetTileIndex(12, 12), path[6].Tile);
                Assert.AreEqual(AIMap.GetTileIndex(11, 11), path[8].Tile);
                Assert.AreEqual(AIMap.GetTileIndex(10, 10), path[10].Tile);
                Assert.AreEqual(AIMap.GetTileIndex(9, 10), path[11].Tile);
            }
        }

        [TestMethod]
        public void CalculateAngleTest1()
        {
            using (var world = new OpenTTD.Testing.TestWorld(20, 20))
            {
                /*   /\
                 *  /\/ 
                 * /\/ 
                 * \/    */
                var currentTile = AIMap.GetTileIndex(10, 10);
                var cameFrom = new PathInfo(
                    AIMap.GetTileIndex(9, 10),
                    1,
                    1,
                    BuildType.Basic,
                    new PathInfo(
                        AIMap.GetTileIndex(8, 1),
                        1,
                        1,
                        BuildType.Basic,
                        null
                    )
                );

                double actual = RailBuilder.CalculateAngle(currentTile, cameFrom.Tile, cameFrom.Previous);
                Assert.AreEqual(0.0, actual);

                /*  /\
                 * /\/
                 * \/\ 
                 * /\/ 
                 * \/    */
                cameFrom = new PathInfo(
                    AIMap.GetTileIndex(9, 10),
                    1,
                    1,
                    BuildType.Basic,
                    new PathInfo(
                        AIMap.GetTileIndex(9, 9),
                        1,
                        1,
                        BuildType.Basic,
                        new PathInfo(
                            AIMap.GetTileIndex(8, 9),
                            1,
                            1,
                            BuildType.Basic,
                            null
                        )
                    )
                );

                actual = RailBuilder.CalculateAngle(currentTile, cameFrom.Tile, cameFrom.Previous);
                Assert.AreEqual(0.0, actual);

                /*   /\
                 *  /\/
                 *  \/\ 
                 *  /\/ 
                 * /\/
                 * \/    */
                cameFrom = new PathInfo(
                    AIMap.GetTileIndex(9, 10),
                    1,
                    1,
                    BuildType.Basic,
                    new PathInfo(
                        AIMap.GetTileIndex(8, 10),
                        1,
                        1,
                        BuildType.Basic,
                        new PathInfo(
                            AIMap.GetTileIndex(8, 9),
                            1,
                            1,
                            BuildType.Basic,
                            new PathInfo(
                                AIMap.GetTileIndex(7, 9),
                                1,
                                1,
                                BuildType.Basic,
                                null
                            )
                        )
                    )
                );

                actual = RailBuilder.CalculateAngle(currentTile, cameFrom.Tile, cameFrom.Previous);
                Assert.AreEqual(1.0, actual);

                /*  /\
                 * /\/\ 
                 * \/\/ 
                 *  \/    */
                cameFrom = new PathInfo(
                    AIMap.GetTileIndex(9, 10),
                    1,
                    1,
                    BuildType.Basic,
                    new PathInfo(
                        AIMap.GetTileIndex(9, 9),
                        1,
                        1,
                        BuildType.Basic,
                        new PathInfo(
                            AIMap.GetTileIndex(10, 9),
                            1,
                            1,
                            BuildType.Basic,
                            null
                        )
                    )
                );

                actual = RailBuilder.CalculateAngle(currentTile, cameFrom.Tile, cameFrom.Previous);
                Assert.AreEqual(100.0, actual);
            }
        }
    }
}
