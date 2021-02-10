using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using OdeToFood.Core;
using OdeToFood.Data;

namespace OdeToFood.Pages.Restaurants
{
    public class EditModel : PageModel
    {
        private readonly IRestaurantData restaurantData;
        private readonly IHtmlHelper htmlHelper;

        [BindProperty]
        public Restaurant Restaurant { get; set; }
        public IEnumerable<SelectListItem> Cuisines { get; set; }

        public EditModel(IRestaurantData restaurantData, IHtmlHelper htmlHelper)
        {
            this.htmlHelper = htmlHelper;
            this.restaurantData = restaurantData;
        }

        public IActionResult OnGet(int? restaurantId)
        {
            Cuisines = htmlHelper.GetEnumSelectList<CusisineType>();
            
            if(restaurantId.HasValue)
            {
                Restaurant = restaurantData.GetRestaurantById(restaurantId.Value);
                if(Restaurant == null)
                {
                    return RedirectToPage("./NotFound");
                }
            }
            else
            {
                Restaurant = new Restaurant();
            }
            return Page();
        }

        public IActionResult OnPost()
        {
            if(!ModelState.IsValid)
            {
                Cuisines = htmlHelper.GetEnumSelectList<CusisineType>();
                return Page();
            }
            if (Restaurant.Id > 0)
            {
                restaurantData.UpdateRestaurant(Restaurant);
            }
            else
            {
                restaurantData.AddRestaurant(Restaurant);
            }

            restaurantData.Commit();
            TempData["Message"] = $"Restaurant {Restaurant.Name} saved";
            return RedirectToPage("./List");

        }
    }
}
