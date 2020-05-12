# Atp Analyser
A visualisation of tennis player rankings from any date, any time.

![Atp-Analyser Demo](demo/atpdemo.gif)

## Run the application
Use these instructions to get the project up and running.

### Dependencies
You will need:

* [Visual Studio Code or Visual Studio 2019](https://visualstudio.microsoft.com/vs/) (version 16.3 or later)
* [.NET Core SDK 3](https://dotnet.microsoft.com/download/dotnet-core/3.0)
* [Node.js](https://nodejs.org/en/) (version 10 or later) with npm (version 6.11.3 or later)

### Setup
Follow these steps to get your development environment set up:

  1. Clone the repository
  2. From the root directory, restore the required packages by running:
      ```
     dotnet restore
     ```
  3. Then build the solution by running:
     ```
     dotnet build
     ```
  4. For the next step, you have the option to run the project with the db file included in the project directory. To do this, navigate to the `\WebApi` directory, and launch the backend by running:
      ```
     dotnet run
     ```

If however, you want to build the db file yourself open ATP-Analyser.sln file with Visual Studio, and change the startup project to "DataCollection". The DataCollection Program.cs file contains code where you can scrape the info from the ATP site and store it in a text file. The code allows you to edit how much you wish to add to the text file at a time (as I found adding the entire thing at once caused some timeout issues).

Then set the startup project back to WebApi and find the PlayerInit.cs file in the Data directory. Again, this can be edited to add small amounts of data at a time to the database or it can be done all at once (again, timeout issues may occur). If you are adding small amounts, comment out the if(context.Players.Any()) code as well as AddExamplePlayer(context) after the first entry.

Once the db file has been created, perform step 4 and continue as normal.

  5. After the backend has started, open another command window and navigate to the `\clientapp` directory, and restore the required packages by running:
     ```
	 npm install
	 ```

  6. Finally, launch the frontend by running:
      ```
     npm start
     ```


  7. Launch [http://localhost:5000/](http://localhost:5000/api) in your browser to view the API
  
  8. Launch [http://localhost:4200/](http://localhost:4200/) in your browser to view the UI

## Technologies
* .NET Core 3
* ASP.NET Core 3
* Entity Framework Core 3
* React 16.8
