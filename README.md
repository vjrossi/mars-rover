# Mars Rover Simulation

This project simulates the movement of robotic rovers on a rectangular plateau on Mars. The rovers can be controlled through a series of commands to move and rotate.

## Getting Started

### Prerequisites

- .NET 8.0 SDK or later

### Installation

1. Clone the repository:
   git clone https://github.com/yourusername/mars-rover.git

2. Navigate to the project directory:
   cd mars-rover

3. Build the project:
   dotnet build -c Release

Alternative to cloning -> download the ZIP.

## Usage

There are three ways to run the Mars Rover simulation:

### Using the Web Interface

1. Start the web application:
   dotnet run --project MarsRover.Web

2. Open a web browser and navigate to `http://localhost:5000` (or the port specified in the console output).

3. Use the web interface to set up the grid size, add rovers, and run the simulation.

### Using dotnet run

Use the following command:

dotnet run --project MarsRover --grid <x> <y> --commands "<rover1_commands>" "<rover2_commands>" ...

### Using the compiled executable

After building the project, you can run the compiled executable directly:

1. Navigate to the output directory:
   cd MarsRover/bin/Release/net8.0

2. Run the executable:
   
   MarsRover.exe --grid <x> <y> --commands "<rover1_commands>" "<rover2_commands>" ...

Where:
- <x> and <y> are the dimensions of the plateau
- <rover_commands> are the initial position and movement commands for each rover

Example:
MarsRover.exe --grid 5 5 --commands "1 2 N LMLMLMLMM" "3 3 E MMRMMRMRRM"

This command will:
1. Create a 5x5 plateau
2. Deploy the first rover at position (1, 2) facing North, then move it according to the commands
3. Deploy the second rover at position (3, 3) facing East, then move it according to the commands

The program will output the final positions of each rover.

## Command Format

- The grid size is specified as two integers representing the upper-right coordinates of the plateau.
- Each rover's commands are given in the format: "X Y Direction Commands"
  - X and Y are the initial coordinates of the rover
  - Direction is one of N (North), E (East), S (South), W (West)
  - Commands is a string of instructions:
    - L: Turn left 90 degrees
    - R: Turn right 90 degrees
    - M: Move forward one grid point

## Project Structure

The project is structured as follows:

- MarsRover/: Contains the main application code
  - Program.cs: Entry point of the console application
  - models/: Contains the core classes for the simulation
- MarsRover.Tests/: Contains unit tests for the application
- MarsRover.Web/: Contains the web application
  - Controllers/: Contains the SimulationController for handling web requests
  - Views/: Contains the Razor views for the web interface
  - wwwroot/: Contains static files including JavaScript for the simulation

## Running Tests

To run the unit tests, use the following command:

dotnet test

## Features

- Multiple rovers can be deployed and controlled independently
- Rovers cannot move outside the defined plateau
- Rovers take photos at their final positions
- Web interface for easy simulation setup and visualization
- Real-time animation of rover movements in the web interface

## Web Interface

The web interface provides an intuitive way to set up and run Mars Rover simulations:

1. Set the grid size
2. Add one or more rovers, specifying their initial positions and commands
3. Run the simulation to see the rovers move on the grid
4. Visualize the movement of each rover step by step

