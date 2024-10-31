using CologneStore.Models;
using Microsoft.AspNetCore.Mvc;

namespace CologneStore.Controllers
{
    public class CustomersController : Controller
    {
        private readonly FirestoreService _firestoreService;

        public CustomersController()
        {
            // Initialize Firestore service
            _firestoreService = new FirestoreService();
        }
        public async Task<IActionResult> Index()
        {
            // Retrieve items from Firestore
            List<CustomerModel> customers = await _firestoreService.GetCustomersAsync("Users");

            // Pass the items to the view
            return View(customers);
        }
    }
}
