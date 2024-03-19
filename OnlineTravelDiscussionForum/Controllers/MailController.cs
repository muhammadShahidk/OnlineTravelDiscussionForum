using MailKit;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using OnlineTravelDiscussionForum.Dtos;
using OnlineTravelDiscussionForum.Interfaces;

namespace OnlineTravelDiscussionForum.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MailController : ControllerBase
    {
       
        
            private readonly IEMailService _emailService;

        public MailController(IEMailService emailService)
        {
            _emailService = emailService;
        }

        [HttpPost]
        public IActionResult SendEmail(EmailDto request)
        {
            try
            {
                var isSent = _emailService.SendEmail(request);
                return Ok("mail Sent");

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        
        }
    }
}
