using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace st10090477_PROG6221_POE_GROUP_2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<Ingredient> allIngredients;
        private List<double> allOriginalQuantities;
        private List<string> allOriginalUnits;
        private List<string> allSteps;
        private string recipeName;
        Recipe recipe;
        private static Dictionary<string, Recipe> Recipes = new Dictionary<string, Recipe>();

        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += Window_Loaded;
            anotherRecipeLable.Visibility = Visibility.Collapsed;

        }

        private void btnAddRecipe_Click(object sender, RoutedEventArgs e)
        {
            AddIngredient addIngredient = new AddIngredient();
            addIngredient.ShowDialog();
            // Retrieve the ingredient details from the AddIngredient window

            allIngredients = addIngredient.ingredients;
            allOriginalQuantities = addIngredient.originalQuantities;
            allOriginalUnits = addIngredient.originalUnits;
            allSteps = addIngredient.steps;
            recipeName = addIngredient.recipeName;
            if (recipeName != null)
            {
                recipe = new Recipe(recipeName, allIngredients, allOriginalQuantities, allOriginalUnits, allSteps);

                // Check if the recipe name already exists in the dictionary

                if (!Recipes.ContainsKey(recipe.RecipeName))
                {
                    // Add the recipe to the dictionary
                    Recipes.Add(recipe.RecipeName, recipe);
                    lstDisplayDetails.Items.Clear();
                    LoadRecipeNames(); // Call the method that loads recipe names
                }
                else
                {
                    MessageBox.Show("A recipe with this name already exists.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            // Make the label visible after the function is done executing
            anotherRecipeLable.Visibility = Visibility.Visible;
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        // Event handler for the "Display All Recipes" button
        private void btnDisplayAllRecipe_Click(object sender, RoutedEventArgs e)
        {
            if (Recipes.Count == 0)
            {
                displayNoRecipe();
            }
            else
            {
                lstDisplayDetails.Items.Clear();
                List<string> sortedKeys = new List<string>(Recipes.Keys);
                sortedKeys.Sort();

                foreach (string key in sortedKeys)
                {
                    Recipe recipe = Recipes[key];

                    // Unsubscribe from the ExceededCalories event to prevent multiple subscriptions
                    recipe.ExceededCalories -= ExceededCaloriesHandler;

                    // Subscribe to the ExceededCalories event
                    recipe.ExceededCalories += ExceededCaloriesHandler;
                    lstDisplayDetails.Items.Add(recipe.DisplayRecipe());
                    recipe.notifyer(); // Call Notifyer method which internally calculates total calories
                    lstDisplayDetails.Items.Add("-------------------------------------------------------" +
                        "-------------------------------------------------------------------------------");
                }
            }
        }
        // Event handler for the "Display Specific Recipe" button
        private void btnDisplaySpecificRecipe_Click(object sender, RoutedEventArgs e)
        {
            if (Recipes.Count == 0)
            {
                displayNoRecipe();
            }
            else
            {
                lstDisplayDetails.Items.Clear();
                string recipeName = txtSpecificRecipe.Text;
                if (Recipes.ContainsKey(recipeName))
                {
                    // Unsubscribe from the ExceededCalories event to prevent multiple subscriptions
                    recipe.ExceededCalories -= ExceededCaloriesHandler;

                    // Subscribe to the ExceededCalories event
                    recipe.ExceededCalories += ExceededCaloriesHandler;

                    lstDisplayDetails.Items.Add(Recipes[recipeName].DisplayRecipe());
                    double totalCalories = Recipes[recipeName].TotalCalories();
                    Recipes[recipeName].notifyer();

                    // Display Steps with CheckBoxes
                    var stepsWithCheckBox = Recipes[recipeName].Steps.Select(step => new { Step = step }).ToList();
                    lstRecipeSteps.ItemsSource = stepsWithCheckBox;
                }
                else
                {
                    MessageBox.Show("Invalid recipe name please check the spelling of the recipe.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
        // Event handler for the ExceededCalories event
        private void ExceededCaloriesHandler(string message, double totalCalories)
        {
            // Concatenate total calories to the message
            string messageWithCalories = $"\t* Total calories (Calories quantify food energy intake): {totalCalories}\n" + message + "\n";

            // Add the message with total calories to the lstDisplayDetails ListBox
            lstDisplayDetails.Items.Add(messageWithCalories);
        }
        // Event handler for the "Scale Recipe" button
        private void btnScaleRecipe_Click(object sender, RoutedEventArgs e)
        {
            if (Recipes.Count == 0)
            {
                displayNoRecipe();
            }
            else
            {
                if (rdoZeroPointFive.IsChecked == true)
                {
                    scaleRecipeSpecificName(0.5);
                }
                else if (rdoTwo.IsChecked == true)
                {
                    scaleRecipeSpecificName(2);
                }
                else if (rdoThree.IsChecked == true)
                {
                    scaleRecipeSpecificName(3);
                }
                else
                {
                    MessageBox.Show("Please choose one number.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }

        }

        private void btnClearRecipe_Click(object sender, RoutedEventArgs e)
        {
            if (Recipes.Count == 0)
            {
                displayNoRecipe();
            }
            else
            {
                MessageBoxResult result = MessageBox.Show("Are you sure you want to clear all recipes? (y/n):", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
                lstDisplayDetails.Items.Clear();

                if (result == MessageBoxResult.Yes)
                {
                    Recipes.Clear();
                    txtSpecificRecipe.Items.Clear();
                    txtSpecificRecipeToScale.Items.Clear();
                    txtResetRecipe.Items.Clear();
                    lstRecipeSteps.ItemsSource = null;
                    MessageBox.Show("All recipes have been cleared.", "Success", MessageBoxButton.OK);
                }
                else
                {
                    MessageBox.Show("You cancled to clear the recipe.", "Cancled", MessageBoxButton.OK);
                }
            }

        }
        // Event handler for the "Reset Recipe" button
        private void btnResetRecipe_Click(object sender, RoutedEventArgs e)
        {
            if (Recipes.Count == 0)
            {
                displayNoRecipe();
            }
            else
            {
                string resetRecipeName = txtResetRecipe.Text;
                if (Recipes.ContainsKey(resetRecipeName))
                {
                    MessageBoxResult result = MessageBox.Show($"Are you sure you want to reset the {resetRecipeName} recipe?", "Confirmation", MessageBoxButton.YesNo);

                    if (result == MessageBoxResult.Yes)
                    {
                        lstDisplayDetails.Items.Clear();
                        bool success = Recipes[resetRecipeName].ResetRecipe(); 
                        if (success)
                        {
                            lstDisplayDetails.Items.Add("The recipe has been reset. Here is the original recipe:");
                            lstDisplayDetails.Items.Add(Recipes[resetRecipeName].DisplayRecipe());
                            double totalCalories = Recipes[resetRecipeName].TotalCalories();
                            Recipes[resetRecipeName].notifyer();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("There is no recipe with that name", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }
        }
        // Method to display a message when there are no recipes
        private void displayNoRecipe()
        {
            MessageBox.Show("There is no recipe in the list.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }
        // Method to scale a recipe based on a specific scaling number
        private void scaleRecipeSpecificName(double scalingNumber)
        {
            lstDisplayDetails.Items.Clear();
            string recipeName1 = txtSpecificRecipeToScale.Text;
            if (Recipes.ContainsKey(recipeName1))
            {
                MessageBoxResult result = MessageBox.Show($"Are you sure you want to scale the {recipeName1} recipe?", "Confirmation", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    Recipes[recipeName1].ScaleRecipe(scalingNumber);
                    lstDisplayDetails.Items.Add("The recipe has been scaled. Here is the updated recipe:\n");
                    lstDisplayDetails.Items.Add(Recipes[recipeName1].DisplayRecipe());
                    double totalCalories = Recipes[recipeName1].TotalCalories();
                    Recipes[recipeName1].notifyer();
                }

            }
            else
            {
                MessageBox.Show("There is no recipe with that name", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }
        // Event handler for the "Add to Menu" button
        private void btnaddMenu_Click(object sender, RoutedEventArgs e)
        {
            if (Recipes.Count == 0)
            {
                displayNoRecipe();
            }
            else
            {
                lstDisplayDetails.Items.Clear();
                RecipePieChart recipePieChart = new RecipePieChart(Recipes);
                recipePieChart.Show();
            }
        }
        // Event handler when the window is loaded
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            foreach (var recipeName in Recipes.Keys)
            {
                txtSpecificRecipe.Items.Add(recipeName);
            }

        }
        // Method to load recipe names into the text boxes
        private void LoadRecipeNames()
        {
            txtSpecificRecipe.Items.Clear();
            txtSpecificRecipeToScale.Items.Clear();
            txtResetRecipe.Items.Clear();
            foreach (var recipeName in Recipes.Keys)
            {
                txtSpecificRecipe.Items.Add(recipeName);
                txtSpecificRecipeToScale.Items.Add(recipeName);
                txtResetRecipe.Items.Add(recipeName);
            }
        }
    }
}
