using System.Collections.Generic;
using AstarNet.Solver.Model;
using AstarNet.Solver.ValueObjects;
using FluentAssertions;
using Xunit;

namespace AstarNet.Solver.UnitTests
{
    public class PathSolverTests
    {
        [Fact]
        public void FindPath_ShouldReturnTwoSteps_ForDirectConnection()
        {
            // Arrange
            var solver = new PathSolver();
            var from = new GraphTravelVertex(new Coord(10, 10));
            var to = new GraphTravelVertex(new Coord(10, 20));
            from.SetNeighbors(new List<Coord>()
                              {
                                  to.Position
                              });

            var travelVertices = new Dictionary<Coord, ITravelVertex<Coord>>()
                                 {
                                     { from.Position, from },
                                     { to.Position, to }
                                 };

            // Act
            var result = solver.FindPath(from, to, travelVertices);

            // Assert
            result.Success.Should().Be(true);
            result.Steps.Should().HaveCount(2);
        }
        

        [Fact]
        public void FindPath_ShouldReturnThreeSteps_ForIndirectConnection()
        {
            // Arrange
            var solver = new PathSolver();
            var from = new GraphTravelVertex(new Coord(10, 10));
            var middle = new GraphTravelVertex(new Coord(20, 10));
            var to = new GraphTravelVertex(new Coord(20, 20));
            from.SetNeighbors(new List<Coord>()
                              {
                                  middle.Position
                              });

            middle.SetNeighbors(new List<Coord>()
                                {
                                    to.Position
                                });

            var travelVertices = new Dictionary<Coord, ITravelVertex<Coord>>()
                                 {
                                     { from.Position, from },
                                     { to.Position, to },
                                     { middle.Position, middle }
                                 };

            // Act
            var result = solver.FindPath(from, to, travelVertices);

            // Assert
            result.Success.Should().Be(true);
            result.Steps.Should().HaveCount(3);
        }
        [Fact]
        public void FindPath_ShouldHaveSteps_InCorrectOrder()
        {
            // Arrange
            var solver = new PathSolver();
            var from = new GraphTravelVertex(new Coord(10, 10));
            var middle = new GraphTravelVertex(new Coord(20, 10));
            var to = new GraphTravelVertex(new Coord(20, 20));
            from.SetNeighbors(new List<Coord>()
                              {
                                  middle.Position
                              });

            middle.SetNeighbors(new List<Coord>()
                                {
                                    to.Position
                                });

            var travelVertices = new Dictionary<Coord, ITravelVertex<Coord>>()
                                 {
                                     { from.Position, from },
                                     { to.Position, to },
                                     { middle.Position, middle }
                                 };

            // Act
            var result = solver.FindPath(from, to, travelVertices);

            // Assert

            result.Steps[0].Should().Be(from.Position);
            result.Steps[1].Should().Be(middle.Position);
            result.Steps[2].Should().Be(to.Position);
        }
    }
}