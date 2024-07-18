using MineSweeper.Business;
using Moq;

namespace MineSweeper.Tests
{
    public class WhenSettingUpBoardWithMoq
    {
        MineSweeperGame _game = MineSweeperGame.Instance;

        public WhenSettingUpBoardWithMoq() 
        {
            Mock<MineSweeperBoard> boardMock = 
                new Mock<MineSweeperBoard>();
            HashSet<int> bogusReturn =
                new HashSet<int>() { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            boardMock.Setup(
                x=> x.GenerateRandomMinePositions(10)).Returns(bogusReturn);
            _game.Initialize(boardMock.Object);
        }
        [Fact]
        public void ShouldHave10Mines()
        {
            int mineCount = 0;
            foreach (Square square in _game.Squares.Values)
            {
                if (square.IsMine) mineCount++;
            }
        }

        [Fact]
        public void ShouldHaveCorrectAdjacentMineCountOnNonMineSquares()
        {
            // on a 10x10 board with mines being the first 10 squares,
            // the first two rows should look like this:
            // |*|*|*|*|*|*|*|*|*|*|
            // |2|3|3|3|3|3|3|3|3|2|
            Assert.Equal(2, _game.Squares[10].Value);
            Assert.Equal(3, _game.Squares[11].Value);
            Assert.Equal(3, _game.Squares[12].Value);
            Assert.Equal(3, _game.Squares[13].Value);
            Assert.Equal(3, _game.Squares[14].Value);
            Assert.Equal(3, _game.Squares[15].Value);
            Assert.Equal(3, _game.Squares[16].Value);
            Assert.Equal(3, _game.Squares[17].Value);
            Assert.Equal(3, _game.Squares[18].Value);
            Assert.Equal(2, _game.Squares[19].Value);

        }
    }
}