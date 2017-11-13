using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class GameStateTests {

	[Test]
	public void CreateEmptyField() 
	{
		var gameState = new GameState(10,10);
		foreach (var gameStateCellState in gameState.CellStates)
		{
			Assert.IsFalse(gameStateCellState.IsTaken);
		}
	}

	[Test]
	public void CreateSimpleFigrue()
	{
		var blocks = new Block[4];
		blocks[0] = new Block(0,0);
		blocks[1] = new Block(0,1);
		blocks[2] = new Block(1,1);
		blocks[3] = new Block(1,0);
		
		var squareFigure = new Figure(blocks);
		
		// 0 0 x x 0
		// 0 0 x x 0
		// 0 0 0 0 0
		// 0 0 0 0 0
		// 0 0 0 0 0
		
		var gameState = new GameState(5,5);
		
		Assert.IsTrue(gameState.CanMoveTo(squareFigure, 2,3));
		
		gameState.MoveTo(squareFigure, 2, 3);
		
		Assert.AreEqual(squareFigure, gameState.GetFigureAt(2,3));
		Assert.AreEqual(squareFigure, gameState.GetFigureAt(2,4));
		Assert.AreEqual(squareFigure, gameState.GetFigureAt(3,3));
		Assert.AreEqual(squareFigure, gameState.GetFigureAt(3,4));
		
		Assert.IsTrue(gameState.IsTaken(2,3));
		Assert.IsTrue(gameState.IsTaken(3,3));
		Assert.IsTrue(gameState.IsTaken(3,4));
		Assert.IsTrue(gameState.IsTaken(3,3));
		
		Assert.IsFalse(gameState.IsTaken(0,0));
	}

	[Test]
	public void FigureRotation()
	{
		var blocks = new Block[]
		{
			new Block(0, 0),
			new Block(1, 0),
			new Block(2, 0),
			new Block(3, 0),
		};
		
		var line = new Figure(blocks);

		line.RotationSets = new RotationSet[]
		{
			new RotationSet
			{
				new Vector2(0, 0),
				new Vector2(1, 0),
				new Vector2(2, 0),
				new Vector2(3, 0),
			},
			new RotationSet
			{
				new Vector2(0, 0),
				new Vector2(0, 1),
				new Vector2(0, 2),
				new Vector2(0, 3),
			},
			new RotationSet
			{
				new Vector2(0, 0),
				new Vector2(-1, 0),
				new Vector2(-2, 0),
				new Vector2(-3, 0),
			},
			new RotationSet
			{
				new Vector2(0, 0),
				new Vector2(-1, 0),
				new Vector2(-2, 0),
				new Vector2(-3, 0),
			},
		};
		
		Assert.AreEqual(new Vector2(0,0), line.Blocks[0].Position);
		Assert.AreEqual(new Vector2(1,0), line.Blocks[1].Position);
		Assert.AreEqual(new Vector2(2,0), line.Blocks[2].Position);
		Assert.AreEqual(new Vector2(3,0), line.Blocks[3].Position);
		
		line.Rotate(1);
		
		Assert.AreEqual(new Vector2(0,0), line.Blocks[0].Position);
		Assert.AreEqual(new Vector2(0,1), line.Blocks[1].Position);
		Assert.AreEqual(new Vector2(0,2), line.Blocks[2].Position);
		Assert.AreEqual(new Vector2(0,3), line.Blocks[3].Position);
	}
	

	[Test]
	public void RotateFigure()
	{
		var gameState = new GameState(5,5);

		var blocks = new Block[]
		{
			new Block(0, 0),
			new Block(1, 0),
			new Block(2, 0),
			new Block(3, 0),
		};
		
		var line = new Figure(blocks);

		line.RotationSets = new RotationSet[]
		{
			new RotationSet
			{
				new Vector2(0, 0),
				new Vector2(1, 0),
				new Vector2(2, 0),
				new Vector2(3, 0),
			},
			new RotationSet
			{
				new Vector2(0, 0),
				new Vector2(0, 1),
				new Vector2(0, 2),
				new Vector2(0, 3),
			},
			new RotationSet
			{
				new Vector2(0, 0),
				new Vector2(-1, 0),
				new Vector2(-2, 0),
				new Vector2(-3, 0),
			},
			new RotationSet
			{
				new Vector2(0, 0),
				new Vector2(-1, 0),
				new Vector2(-2, 0),
				new Vector2(-3, 0),
			},
		};
		
		gameState.MoveTo(line, 1,1);
		
		// 0 0 0 0 0
		// 0 0 0 0 0
		// 0 0 0 0 0
		// 0 x x x x
		// 0 0 0 0 0
		
		Assert.IsTrue(gameState.IsTaken(1,1));
		Assert.IsTrue(gameState.IsTaken(2,1));
		Assert.IsTrue(gameState.IsTaken(3,1));
		Assert.IsTrue(gameState.IsTaken(4,1));
		

		gameState.RotateCounterClockwise(line);
		
		// 0 x 0 0 0
		// 0 x 0 0 0
		// 0 x 0 0 0
		// 0 x 0 0 0
		// 0 0 0 0 0
		
		Assert.IsTrue(gameState.IsTaken(1,1));
		Assert.IsTrue(gameState.IsTaken(1,2));
		Assert.IsTrue(gameState.IsTaken(1,3));
		Assert.IsTrue(gameState.IsTaken(1,4));

	}


	[Test]
	public void FillLine1()
	{
		var gameState = new GameState(3, 3);

		var blocks1 = new Block[]
		{
			new Block(0, 0),
			new Block(1, 0),
		};

		var figure1 = new Figure(blocks1);

		var blocks2 = new Block[]
		{
			new Block(0, 0),
			new Block(0, 1)
		};
		
		var figure2 = new Figure(blocks2);
		
		Assert.AreEqual(0, gameState.FullLines.Count);
		
		gameState.MoveTo(figure1, 0,0);
		gameState.MoveTo(figure2, 2, 0);

		gameState.UpdateFullLines();
		
		Assert.AreEqual(1, gameState.FullLines.Count);
		Assert.AreEqual(0, gameState.FullLines[0]);
	}
	
	[Test]
	public void FillLine2()
	{
		var gameState = new GameState(3, 3);

		var blocks1 = new Block[]
		{
			new Block(0, 0),
			new Block(1, 0),
			new Block(1, 1),
			new Block(1, 2),
			new Block(0, 2),
		};

		var figure1 = new Figure(blocks1);

		var blocks2 = new Block[]
		{
			new Block(0, 0),
			new Block(0, 1),
			new Block(0, 2)
		};
		
		var figure2 = new Figure(blocks2);
		
		Assert.AreEqual(0, gameState.FullLines.Count);
		
		gameState.MoveTo(figure1, 0,0);
		gameState.MoveTo(figure2, 2, 0);

		gameState.UpdateFullLines();
		
		Assert.AreEqual(2, gameState.FullLines.Count);
		Assert.AreEqual(0, gameState.FullLines[0]);
		Assert.AreEqual(2, gameState.FullLines[1]);
	}

	[Test]
	public void MoveLefTest()
	{
		var gameState = new GameState(3, 3);
		
		
		var blocks1 = new Block[]
		{
			new Block(0, 0),
		};

		var figure1 = new Figure(blocks1);
		
		gameState.MoveTo(figure1, 1,2);
		Assert.AreEqual(figure1, gameState.GetFigureAt(1,2));
		Assert.IsTrue(gameState.CanMoveTo(figure1, 0,2));
		
		var control = new FigureControl(gameState);
		control.CurrentFigure = figure1;
		control.MoveLeft();
		Assert.AreEqual(null, gameState.GetFigureAt(1,2));
		Assert.AreEqual(figure1, gameState.GetFigureAt(0,2));
	}
	
	[Test]
	public void MoveRightTest()
	{
		var gameState = new GameState(3, 3);
		
		
		var blocks1 = new Block[]
		{
			new Block(0, 0),
		};

		var figure1 = new Figure(blocks1);
		
		gameState.MoveTo(figure1, 1,2);
		Assert.AreEqual(figure1, gameState.GetFigureAt(1,2));
		Assert.IsTrue(gameState.CanMoveTo(figure1, 2,2));
		
		var control = new FigureControl(gameState);
		control.CurrentFigure = figure1;
		control.MoveRight();
		Assert.AreEqual(null, gameState.GetFigureAt(1,2));
		Assert.AreEqual(figure1, gameState.GetFigureAt(2,2));
	}

	[Test]
	public void KillLineTest()
	{
		var gameState = new GameState(3, 3);
		
		var blocks1 = new Block[]
		{
			new Block(0, 0),
			new Block(1, 0),
			new Block(1, 1),
			new Block(1, 2)
		};

		var figure1 = new Figure(blocks1);

		var blocks2 = new Block[]
		{
			new Block(0, 0),
			new Block(0, 1),
			new Block(0, 2)
		};
		
		var figure2 = new Figure(blocks2);
		
		Assert.AreEqual(0, gameState.FullLines.Count);
		
		gameState.MoveTo(figure1, 0,0);
		gameState.MoveTo(figure2, 2, 0);
		
		// 0 1 2
		// 0 1 2
		// 1 1 2

		gameState.UpdateFullLines();
		
		Assert.IsTrue(gameState.IsTaken(0, 0));
		Assert.IsTrue(gameState.IsTaken(1, 0));
		Assert.IsTrue(gameState.IsTaken(2, 0));

		var control = new FigureControl(gameState);
		
		var killSystem = new KillLinesSystem(gameState, control);
		killSystem.Tick();
		
		// 0 0 0
		// 0 1 2
		// 0 1 2
		
		Assert.IsFalse(gameState.IsTaken(0, 0));
		Assert.IsTrue(gameState.IsTaken(1, 0));
		Assert.IsTrue(gameState.IsTaken(2, 0));
		
		Assert.AreEqual(null, gameState.GetFigureAt(0,0));
		Assert.AreEqual(figure1, gameState.GetFigureAt(1,0));
		Assert.AreEqual(figure2, gameState.GetFigureAt(2,0));
		
		Assert.IsFalse(gameState.IsTaken(0, 1));
		Assert.IsFalse(gameState.IsTaken(0, 2));
		Assert.IsFalse(gameState.IsTaken(0, 3));

	}
}
