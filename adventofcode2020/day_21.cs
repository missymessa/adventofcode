using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace adventofcode2020
{
    public static class DayTwentyOne
    {
        public static void Execute()
        {
            List<Food> foods = new List<Food>();
            HashSet<string> allergens = new HashSet<string>();
            Dictionary<string, string> ingredientToAllergenMatches = new Dictionary<string, string>();
            using (StreamReader sr = new StreamReader(Path.Combine(Environment.CurrentDirectory, "input", "day_21.txt")))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    var split = line.Split("(contains ");
                    var allergensList = split[1].TrimEnd(')');
                    foods.Add(new Food
                    {
                        Ingredients = split[0].Split(" ").Where(x => x.Length > 0).ToList(), 
                        Allergens = allergensList.Split(",").Select(x => x.Trim()).ToList()
                    });

                    allergens.UnionWith(allergensList.Split(",").Select(x => x.Trim()));
                }
            }

            while(ingredientToAllergenMatches.Count < allergens.Count)
            {
                var allergen = allergens.Where(x => !ingredientToAllergenMatches.Keys.Contains(x)).ElementAt(new Random().Next(0, allergens.Count - ingredientToAllergenMatches.Count));

                var possibleFoods = foods.Where(x => x.Allergens.Contains(allergen)).ToList();
                var intersection = possibleFoods
                    .Skip(1)
                    .Aggregate(
                        new HashSet<string>(possibleFoods.First().Ingredients),
                        (h, e) => { h.IntersectWith(e.Ingredients); return h; }
                    );
                
                if (intersection.Count > 1)
                {
                    intersection = intersection.Except(ingredientToAllergenMatches.Values).ToHashSet();
                }

                if (intersection.Count == 1)
                {
                    ingredientToAllergenMatches.Add(allergen, intersection.First());
                }
            }

            int countOfNonAllergenFood = 0;
            foreach(var food in foods)
            {
                countOfNonAllergenFood += food.Ingredients.Count(x => !ingredientToAllergenMatches.Values.Contains(x));
            }

            Console.WriteLine($"Number of ingredients that don't contain allergens: {countOfNonAllergenFood}");

            var orderedIngredients = ingredientToAllergenMatches.OrderBy(x => x.Key).Select(x => x.Value).ToList();
            Console.WriteLine($"List of ingredients with allergens: {string.Join(",", orderedIngredients)}");
        }
    }

    public class Food
    {
        public List<string> Ingredients { get; set; }
        public List<string> Allergens { get; set; }
    }
}
