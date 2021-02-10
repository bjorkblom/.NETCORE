using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OdeToFood.Core;
using OdeToFood.Data;

namespace OdeToFood.Pages.Restaurants
{
    public class DeleteModel : PageModel
    {
        private readonly IRestaurantData RestaurantData;

        [BindProperty]
        public Restaurant Restaurant { get; set; }

        public DeleteModel(IRestaurantData restaurantData)
        {
            this.RestaurantData = restaurantData;
        }

        public IActionResult OnGet(int restaurantId)
        {
            Restaurant = RestaurantData.GetRestaurantById(restaurantId);
            if(Restaurant != null)
            {
                return Page();
            }
            return RedirectToPage("./Notfound");
        }

        public IActionResult OnPost()
        {
            if(Restaurant != null)
            {
                RestaurantData.DeleteRestaurant(Restaurant.Id);
                RestaurantData.Commit();
                TempData["Message"] = $"Restaurant {Restaurant.Name} deleted";
            }
            else
            {
                TempData["Message"] = $"Restaurant {Restaurant.Name} could not be deleted";
            }
            return RedirectToPage("./List");
        }
    }
}
