using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using UnityEngine;

public class Game : MonoBehaviour
{
	public Config Config;
	
	public GameState GameState;

	private List<IExecuteSystem> _systems = new List<IExecuteSystem>();

	public void Start()
	{
		GameState = new GameState(Config.Width, Config.Height);

		var control = new FigureControl(GameState); 
		var figureControl = new KeyboardPlayerInput(control);
		var autoMove = new CurrentFigureMoveDownSystem(control, Config);
		var figureViewManager = new FigureViewManager(Config);
		var scoreSystem = new ScoreSystem(Config);
		var killSystem = new KillLinesSystem(Config, GameState, scoreSystem, control);
		var figureFactory = new FigureFactory(Config);
		var gameSimulation = new GameSimulation(Config, figureFactory, figureViewManager, GameState, control);
		
		_systems.Add(autoMove);
		_systems.Add(figureControl);
		_systems.Add(killSystem);
		_systems.Add(gameSimulation);
		_systems.Add(figureViewManager);
	}

	public void Update()
	{
		foreach (var updateable in _systems)
		{
			updateable.Tick();
		}
	}
}