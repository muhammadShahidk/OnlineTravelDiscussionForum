using Microsoft.AspNetCore.Identity;
using MimeKit.Text;
using MimeKit;
using OnlineTravelDiscussionForum.Dtos;
using OnlineTravelDiscussionForum.Interfaces;
using OnlineTravelDiscussionForum.Modals;
using System.Security.Policy;
using Azure.Core;
using System.Globalization;
using System.Web;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace OnlineTravelDiscussionForum.Services
{
    public class ProfileService : IProfileService
    {
        private readonly IEMailService _emailService;
        private readonly IHostEnvironment _env;
        private readonly IConfiguration configuration;
        private readonly UserManager<ApplicationUser> _userManager;

        public ProfileService(IEMailService emailService,IHostEnvironment env, IConfiguration configuration,UserManager<ApplicationUser> userManager)
        {
            _emailService = emailService;
            this._env = env;
            this.configuration = configuration;
            _userManager = userManager;
        }
        public Task<bool> ResetPassword(ResetPasswordDto resetPasswordDto)
        {
            throw new NotImplementedException();

        }

        public async Task<bool> SendForgotPasswordEmail(ApplicationUser user)
        {

            // Generate password reset link


            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            string encodedToken = EncodeTokenForUrl(token);

            var settings = this.configuration.GetSection("AppSettings").Get<AppSettings>();


            var resetLink = $"{settings.getBaseUrl()}/reset-password?userId={user.Id}&token={encodedToken}";


            var body = GenrateBody(user, resetLink);

            // Send email
            if (user.Email == null)
            {
                return false;
            }

            var email = new EmailDto() { To = user.Email, Body = body, Subject = "Reset Password" };

            _emailService.SendEmail(email);
            return true;

        }
        private static string EncodeTokenForUrl(string token)
        {
            return HttpUtility.UrlEncode(token);
        }

        private  string GenrateBody(ApplicationUser user, string resetLink)
        {
            //var templatePath = "C:\\Users\\HP\\Source\\Repos\\muhammadShahidk\\OnlineTravelDiscussionForum\\OnlineTravelDiscussionForum\\Email\\EmailTemplate.html";
            var projectDirectory = Directory.GetCurrentDirectory();
            var wwwRootPath =  _env.ContentRootPath;

            //throw new NotImplementedException($"projectDirectory: {projectDirectory} wwwRootPath: {wwwRootPath}");


            var templatePath = Path.Combine(wwwRootPath, "Email", "EmailTemplate.html");

            string template = "";
            try
            {

                template = File.ReadAllText(templatePath);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }


            // Calculate expiration time (adjust as needed)
            var expirationTimeUtc = DateTime.UtcNow.AddHours(1);

            // Convert UTC time to local time (adjust the time zone accordingly)
            var expirationTimeLocal = ConvertUtcToTimeZone(expirationTimeUtc, "Pakistan Standard Time");

            // Calculate the remaining minutes
            var remainingMinutes = (int)(expirationTimeLocal - DateTime.Now).TotalMinutes;

            // Replace placeholders in the template
            template = template.Replace("[fullName]", $"{user.FirstName} {user.LastName}");
            template = template.Replace("[UserName]", $"{user.UserName}");
            template = template.Replace("[ResetLink]", resetLink);
            template = template.Replace("[ExpirationTime]", FormatDateTime(expirationTimeLocal, "ur-PK"));
            template = template.Replace("[RemainingMinutes]", remainingMinutes.ToString());


            // Use the template in the email body
            // Use the template in the email body
            return template;
        }
        private static DateTime ConvertUtcToTimeZone(DateTime utcTime, string timeZoneId)
        {
            var localTimeZone = TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);
            return TimeZoneInfo.ConvertTimeFromUtc(utcTime, localTimeZone);
        }

        private static string FormatDateTime(DateTime dateTime, string culture)
        {
            CultureInfo customCulture = new CultureInfo(culture);
            return dateTime.ToString("hh:mm:ss tt dd-MM-yyyy", customCulture);
        }



        public async Task<bool> SendForgotPasswordEmail(ApplicationUser user, string resetUrl)
        {

            var resetLink = resetUrl;

            var body = GenrateBody(user, resetLink);

            // Send email
            if (user.Email == null)
            {
                return false;
            }

            var email = new EmailDto() { To = user.Email, Body = body, Subject = "Reset Password" };

            _emailService.SendEmail(email);
            return true;
        }

        public Task<bool> UpdateProfile(UpdateUserDetailsDto updateProfileDto)
        {
            throw new NotImplementedException();
        }
    }
}
