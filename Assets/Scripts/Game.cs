using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using UnityEngine;

public class Game : MonoBehaviour
{
	public Config Config;
	
	public GameState GameState;

	private List<IUpdateable> _updateables = new List<IUpdateable>();

	public void Start()
	{
		GameState = new GameState(Config.Width, Config.Height);

		var control = new FigureControl(GameState); 
		var playerInput = new KeyboardPlayerInput(control);
		_updateables.Add(playerInput);

		var figureFactory = new FigureFactory(Config);

		var gameSimulation = new GameSimulation(Config, figureFactory, GameState, control);
		_updateables.Add(gameSimulation);

	}

	public void Update()
	{
		foreach (var updateable in _updateables)
		{
			updateable.Tick();
		}
	}
}