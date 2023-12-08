# Recipe-App
WPF app that let you stores you favourite recipes

Binamra Thapa ST10090477 

The RecipeApp was created to help home cooks manage their recipes with ease, making cooking a delightful and hassle-free experience. In today's fast-paced world, people often struggle to keep track of their favorite recipes, find it challenging to adjust ingredient quantities when cooking for different numbers of guests, or need a straightforward way to store and access their recipes.

With RecipeApp, users can quickly enter their recipes, scale them based on the number of servings required, reset the recipes to their original state, and even clear the data to enter new recipes. The intuitive console menu makes it simple for users to interact with the application and perform various tasks related to their recipes.

Minimum Reqirements

    Operating System

    Windows 7, 8.1, 10 or 11.

    These are minimum hardware requirements to run Visual Studio 2019.

    1.8 GHz or faster processor. Quad-core or better recommended.

    2 GB of RAM; 8 GB of RAM recommended (2.5 GB minimum if running on a virtual machine).

    Hard disk space: Minimum of 800MB up to 210 GB of available space, depending on features installed; typical installations require 20-50 GB of free space.

    Hard disk speed: to improve performance, install Windows and Visual Studio on a solid state drive (SSD).

    Video card that supports a minimum display resolution of 720p (1280 by 720); Visual Studio will work best at a resolution of WXGA (1366 by 768) or higher.

    How to Compile and Run

    Using Visual Studio

	- Clone or download the repository.

	- Open the solution file (.sln) in Visual Studio.

	- Press F5 or click the Start button in the toolbar to build and run the application.

	- Follow the on-screen prompts to enter a recipe and choose from the available actions (display, scale, reset, clear data, or exit).


Using Command Line

	- Clone or download the repository.

	- Open a terminal/command prompt and navigate to the folder containing the project file (.csproj).

	- Run the following command to build the project: dotnet build

	- Run the following command to execute the application: dotnet run

	- Follow the on-screen prompts to enter a recipe and choose from the available actions (display, scale, reset, clear data, or exit).

How to use this program

    1. The program starts by displaying a menu of options.
    2. Based on the user's choice, the program executes the corresponding action:
       	Option 1: Add Recipe:
		The program prompts you to provide the recipe name and then guides you to enter the number of ingredients, ingredient details, and steps for the recipe.	
		It creates a new Recipe object with the provided information, adds it to the Recipes dictionary, and displays a success message.
	
	Option 2: Display all recipes:
		The program checks if there are any recipes in the Recipes dictionary. If not, it displays an error message.
		If recipes exist, it sorts the recipe names in alphabetical order and displays each recipe's details, including total calories.
		The DisplayRecipe and TotalCalories methods of the Recipe class are used to format and calculate the recipe information.
	
	Option 3: Display a specific recipe:
		The program checks if there are any recipes in the Recipes dictionary. If not, it displays an error message.
		If there are recipes, it prompts you to enter the name of the recipe you want to display.
		If the entered recipe name exists in the Recipes dictionary, it displays the recipe's details and total calories.
		If the entered recipe name does not exist, it displays an error message.
	
	Option 4: Scale a recipe:
		The program checks if there are any recipes in the Recipes dictionary. If not, it displays an error message.
		If recipes exist, it prompts you to confirm your intention to scale a recipe.
		If you confirm, it prompts you to enter the name of the recipe you want to scale and the scaling factor (0.5, 2, or 3).
		If the entered recipe name exists in the Recipes dictionary and the scaling factor is valid, it scales the recipe using the ScaleRecipe method of the corresponding Recipe object.
		It then displays the updated recipe details and total calories.

	Option 5: Reset a recipe:
		The program checks if there are any recipes in the Recipes dictionary. If not, it displays an error message.
		If recipes exist, it prompts you to confirm your intention to reset a recipe.
		If you confirm, it prompts you to enter the name of the recipe you want to reset.
		If the entered recipe name exists in the Recipes dictionary, it resets the recipe to its original state using the ResetRecipe method of the corresponding Recipe object.
		It then displays the original recipe details and total calories.
	Option 6: Add to menu
		Add recipes to the menu: The user can select a recipe from a dropdown menu and add it to the menu. The selected recipe's ingredients and total calories are displayed in a list.
		Calculate total calories: When a recipe is added to the menu, the application calculates the total calories for that recipe and displays a message if the calorie limit is exceeded.
		Generate a pie chart: The user can click the "Create Pie Chart" button to generate a pie chart showing the distribution of food groups in the chosen recipes.
		Food group counting: The application counts the number of ingredients in each food group for the chosen recipes and updates the pie chart accordingly.
		Dependencies
		LiveCharts: This application uses the LiveCharts library to create and display the pie chart. Make sure to include the necessary dependencies in your project.	

	Option 7: Clear all recipes:
		The program checks if there are any recipes in the Recipes dictionary. If not, it displays an error message.
		If recipes exist, it prompts you to confirm your intention to clear all recipes.
		If you confirm, it clears the Recipes dictionary and displays a success message.
	
	Option 8: Exit the program:
		When you choose this option, the program sets the exit variable to -1, indicating the termination of the program.
		The program then terminates, and the console application closes.
    3. After executing an action, the program returns to the main menu until the user chooses to exit.


Updates Based on Lecturer's Feedback
	I've addressed the feedback received for Part 2 of the project and made several important enhancements. Below is a summary of the changes:

	### Alphabetical Ordering of Recipes

	- To improve readability and ease of navigation, recipes are now displayed in alphabetical order.
	- I achieved this by sorting the array of recipes within the Recipe class before they are displayed to the user.

	### Explanation of Calories

	- To enhance user understanding, I’ve added explanations about what calories are in two sections of the application.
	- The first explanation is present when the user is in the process of adding ingredients to a recipe.
	- The second explanation is displayed alongside the ingredients when a recipe is being viewed.

	### Alert for High Calorie Content

	- To ensure users are aware of the nutritional content, an alert has been implemented to trigger when the total calories of a recipe exceed 300.
	- The alert has been enhanced with additional information to provide context to the user about why it's being displayed.

These updates were made to provide a more user-friendly experience and to ensure that users are well-informed about the nutritional aspects of the recipes.

Youtube link of the POE: https://www.youtube.com/watch?v=KqxuTSN4ZhU
Github link: https://github.com/VCSTDN/prog6221---poe-BinamraST10090477


