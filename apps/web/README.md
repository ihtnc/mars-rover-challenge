# Mars Rover Challenge Web App
This project is the web-based wrapper for the `MarsRoverChallenge.Send` library.

The application is a ReactJS SPA, calling a .Net Core web API.

## To run
```
... web> run.cmd
```

This command builds the ReactJS application in `rovercmd`, copies the output to the wwwroot folder within the `rovercmd.api`, then starts the .Net Core API in `rovercmd.api`.

The application can then be accessed by navigating to the hosted instance (i.e.: https://localhost:7107/).
