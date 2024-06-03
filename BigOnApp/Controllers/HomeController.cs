using BigOnApp.DAL.context;
using BigOnApp.Helpers.Services.Interfaces;
using BigOnApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Web;

namespace BigOnApp.Controllers;

public class HomeController : Controller
{
    private readonly AppDbContext _context;
    private readonly IEmailService _emailService;

    public HomeController(AppDbContext context, IEmailService emailService)
    {
        _context = context;
        _emailService = emailService;
    }

    public async Task<IActionResult> Index()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Subscribe(string email)
    {
        bool isEmail = Regex.IsMatch(email, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
        if (!isEmail)
        {
            return Json(new
            {
                error = true,
                message = "Zehmet olmasa duzgn email daxil edin"
            });
        }

        var dbEmail = _context.Subscribers.FirstOrDefault(s => s.EmailAdreess == email);
        if (dbEmail != null && !dbEmail.IsAprovved)
        {
            return Json(new
            {
                error = true,
                message = "Bu email artiq qeydiyatdan kecib zehmet olmasa mailnizi tesqid edin"
            });
        }
        if (dbEmail != null && !dbEmail.IsAprovved)
        {
            return Json(new
            {
                error = true,
                message = "Bu email artiq qeydiyatdan kecib zehmet olmasa mailnizi tesqid edin"
            });
        }
        if (dbEmail != null && dbEmail.IsAprovved)
        {
            return Json(new
            {
                error = true,
                message = "Bu email artiq abune olub"
            });
        }
        var newSubscriber = new Subscriber
        {
            EmailAdreess = email,
            CreatedAt = DateTime.UtcNow.AddHours(4)
        };


        _context.Subscribers.Add(newSubscriber);
        _context.SaveChanges();

        string token = $"#demo-{newSubscriber.EmailAdreess}-{newSubscriber.CreatedAt:yyyy-MM-dd HH:mm:ss.fff}-bigon";
        token = HttpUtility.UrlEncode(token);

        string url = $"{Request.Scheme}://{Request.Host}/subscribe-approve?token={token}";
        string body = $"Please click to link accept subscription <a href=\"{url}\">Click!</a>";

        await _emailService.SendMailAsync(email,"New Letter Subscribe", body);
        return Ok(new
        {
            success = true,
            message = $"Bu {email}-ə link göndərildi, zəhmət olmasa təsdiq edin"
        });
    }
    [Route("/subscribe-approve")]
    public async Task<IActionResult> SubscribeApprove(string token)
    {
        string pattern = @"#demo-(?<email>[^-]*)-(?<date>\d{4}-\d{2}-\d{2}\s\d{2}:\d{2}:\d{2}.\d{3})-bigon";

        Match match = Regex.Match(token, pattern);

        if (!match.Success)
        {
            return Content("token is broken!");
        }

        string email = match.Groups["email"].Value;
        string dateStr = match.Groups["date"].Value;

        if (!DateTime.TryParseExact(dateStr, "yyyy-MM-dd HH:mm:ss.fff", null, DateTimeStyles.None, out DateTime date))
        {
            return Content("token is broken!");
        }

        var subscriber = await _context.Subscribers
            .FirstOrDefaultAsync(m => m.EmailAdreess.Equals(email) && m.CreatedAt == date);
        
        if (subscriber == null)
        {
            return Content("token is broken!");
        }

        if (!subscriber.IsAprovved)
        {
            subscriber.IsAprovved = true;
            subscriber.AprovvedAt = DateTime.Now;
        }
        await _context.SaveChangesAsync();


        return Content($"Success: Email: {email}\n" +
            $"Date: {date}");
    }
}
