using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ShopAppClient.Models;

namespace ShopAppClient.Controllers
{
    public class CategoryController : Controller
    {
        private readonly HttpClient _httpClient;

        public CategoryController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        string apiUrl = "https://localhost:7237/api/category";

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<ApiResponse>(apiUrl); 
                var categories = response.Data;
                return View(categories); 
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Category category)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync(apiUrl, category);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    // Xử lý lỗi nếu thêm không thành công
                    ModelState.AddModelError("", "Failed to create category.");
                }
            }
            catch (HttpRequestException e)
            {
                ModelState.AddModelError("", e.Message);
            }

            return View(category);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            string apiUrl = $"https://localhost:7237/api/category/{id}"; // URL lấy category theo id

            try
            {
                var category = await _httpClient.GetFromJsonAsync<Category>(apiUrl);
                return View(category);
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Category category)
        {
            string apiUrl = $"https://localhost:7237/api/category/{category.CategoryId}"; // URL sửa category

            try
            {
                var response = await _httpClient.PutAsJsonAsync(apiUrl, category);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    // Xử lý lỗi nếu sửa không thành công
                    ModelState.AddModelError("", "Failed to update category.");
                }
            }
            catch (HttpRequestException e)
            {
                ModelState.AddModelError("", e.Message);
            }

            return View(category);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            string apiUrl = $"https://localhost:7237/api/category/{id}"; // URL xóa category

            try
            {
                var response = await _httpClient.DeleteAsync(apiUrl);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    // Xử lý lỗi nếu xóa không thành công
                    Console.WriteLine("errr");
                    return null;
                }
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
    }
}
