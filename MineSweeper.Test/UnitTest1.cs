using Moq;
using Moq.Protected;
using MineSweeper.Business;

namespace MineSweeper.Test
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            HashSet<int> minePositions = new HashSet<int>();
            minePositions.Add(6);
            minePositions.Add(7);
            minePositions.Add(8);
            minePositions.Add(9);   
            minePositions.Add(10);
            minePositions.Add(11);
            minePositions.Add(12);
            minePositions.Add(13);
            minePositions.Add(14);
            minePositions.Add(15);

            /*
            var gameMock = new Mock<System.Math>();
            gameMock.CallBase = true;
            gameMock.Protected().Setup<HashSet<int>>
                ("GenerateRandomMinePositions", 10)
                .Returns(minePositions);
            gameMock.Object.Initialize();
            */
        }
    }
}