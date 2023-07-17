using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MSIT147Site.Models;

namespace MSIT147Site.Controllers
{
    public class ApiController : Controller
    {
        private readonly DemoContext _context;
        private readonly IWebHostEnvironment _host;

        public ApiController(IWebHostEnvironment host)
        {
            _context = new DemoContext();
            _host = host;

        }
        public IActionResult Index()
        {
            return Content("Hello Ajax!!");
            // return Content("<h2>Hello World!!</h2>","text/html");
            //return Content("<h2>Ajax 您好!!</h2>", "text/html", System.Text.Encoding.UTF8);

            //return Content("Hello World!!", "application/msword");
        }

        public IActionResult Cities()
        {
            var cities= _context.Address.Select(x => x.City).Distinct().ToList();
            return Json(cities);
        }
        public IActionResult Districts(string city)
        {
            var districts = _context.Address.Where(x=>x.City==city).Select(x=>x.SiteId).Distinct().ToList();
            return Json(districts);
            //https://localhost:44300/api/Districts?city=%E5%AE%9C%E8%98%AD%E7%B8%A3
        }

        public IActionResult Streets(string district)
        {
            var streets = _context.Address.Where(x => x.SiteId == district).Select(x => x.Road).Distinct().ToList();
            return Json(streets);
            
        }

        public IActionResult CheckAccount(string account)
        {
            var check = _context.Members.Where(x => x.Email == account).Select(x=>x.Email).ToList();
            return Json(check);//回傳一個email

        }
        public IActionResult AjaxEvent(string userName)
        {
            if (string.IsNullOrEmpty(userName))
            {
                userName = "Guest";
            }
            System.Threading.Thread.Sleep(5000);
            return Content("Hello " + userName);

            //測試https://localhost:44300/api/AjaxEvent?userName=123
        }
        [HttpPost]
        public IActionResult Register(Members member,IFormFile Photo)
        {
            //return Content($"Hello{member.Name}");
            //string photoInfo = $"{Photo.FileName}-{Photo.Length}-{Photo.ContentType}";
            string rootPath = Path.Combine(_host.WebRootPath, "uploads", Photo.FileName);
            using(var fileStream=new FileStream(rootPath, FileMode.Create))
            {
                Photo.CopyTo(fileStream);
            }

            //寫進資料庫
            member.FileName = Photo.FileName;
            byte[]? photoByte = null;
            using (var memorySteam=new MemoryStream())
            {
                Photo.CopyTo(memorySteam);
                photoByte = memorySteam.ToArray();                
            }
            member.FileData = photoByte;

            _context.Members.Add(member);
            _context.SaveChanges();
            return Content(rootPath);
        }


        //讀二進位的圖
        public IActionResult GetImageByte(int id = 0)
        {
            Members? _member = _context.Members.Find(id);
            byte[]? img = _member.FileData;
            return File(img, "image/jpeg");
        }
    }
}
