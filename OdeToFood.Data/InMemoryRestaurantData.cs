using OdeToFood.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OdeToFood.Data
{
    public class InMemoryRestaurantData : IRestaurantData
    {
        readonly List<Restaurant> restaurants;

        public InMemoryRestaurantData()
        {
            restaurants = new List<Restaurant>()
                {
                    new Restaurant
                    {
                        Id = 0,
                        Name = "Scott's Pizza",
                        Location = "New York",
                        Cusisine = CusisineType.Italian
                    },
                    new Restaurant
                    {
                        Id = 1,
                        Name = "Cinnamon Club",
                        Location = "New Jersy",
                        Cusisine = CusisineType.Mexican
                    },
                    new Restaurant
                    {
                        Id = 2,
                        Name = "PVK's",
                        Location = "New Dehli",
                        Cusisine = CusisineType.Indian
                    }
                };
        }
        public IEnumerable<Restaurant> GetRestaurantByName(string name = null)
        {
            return from r in restaurants
                   where string.IsNullOrEmpty(name) || r.Name.StartsWith(name)
                   orderby r.Name
                   select r;
        }
        public Restaurant GetRestaurantById(int id)
        {
            return restaurants.SingleOrDefault(r => r.Id == id);
        }
        public Restaurant UpdateRestaurant(Restaurant updatedRestaurant)
        {
            var restaurant = restaurants.SingleOrDefault(r => r.Id == updatedRestaurant.Id);
            if(restaurant != null)
            {
                restaurant.Name = updatedRestaurant.Name;
                restaurant.Location = updatedRestaurant.Location;
                restaurant.Cusisine = updatedRestaurant.Cusisine;
            }

            return restaurant;
        }
        public Restaurant AddRestaurant(Restaurant newRestaurant)
        {
            newRestaurant.Id = restaurants.Max(r => r.Id + 1);
            restaurants.Add(newRestaurant);
            return newRestaurant;
        }
        public int Commit()
        {
            return 0;
        }

        public Restaurant DeleteRestaurant(int id)
        {
            var restaurant = restaurants.FirstOrDefault(r => r.Id == id);
            if(restaurant != null)
            {
                restaurants.Remove(restaurant);
            }
            return restaurant;
        }

        public int Count()
        {
            return restaurants.Count();
        }
    }
}
