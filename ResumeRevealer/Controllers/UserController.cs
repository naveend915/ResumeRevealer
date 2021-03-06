﻿using ResumeParser.Model;
using ResumeParser.ResumeProcessor;
using System;
using System.Net;
using System.Net.Mail;
using System.Web.Http;

namespace ResumeRevealer.Controllers
{
    public class UserController : ApiController
    {
        private UserProcessor userProcessor;

        public UserController()
        {
            userProcessor = new UserProcessor();
        }
        // POST
        [HttpPost]
        [Route("Register")]
        public bool Register(User user)
        {
            return userProcessor.InsertUser(user);
        }
        [HttpPost]
        [Route("Login")]
        public IHttpActionResult Login(User user)
        {
            User userObj = null;
            userObj = userProcessor.GetValidUser(user.Name,user.Password);
            return Ok(userObj);
        }
        [HttpPost]
        [Route("ShareTheResume")]

        public IHttpActionResult ShareTheResume(Email email)
        {
            SmtpClient client = new SmtpClient("smtp-mail.outlook.com");
            client.Port = 587;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.EnableSsl = true;
            client.Credentials = new NetworkCredential("vbelle@teksystems.com", "W0rking@firebase");
            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress("vbelle@teksystems.com");
            mailMessage.To.Add(email.ToPerson);
            mailMessage.Subject = email.subject;
            mailMessage.Body = email.body;
            mailMessage.Attachments.Add(new Attachment(email.FilePath));
            client.Send(mailMessage);
            return Ok("Success");
        }

        [HttpGet]
        [Route("GetInterviewers")]
        public IHttpActionResult GetInterviewers()
        {
            var result = userProcessor.GetInterviewers();
            return Ok(result);
        }

        [HttpGet]
        [Route("GetFavoriteCandidate")]
        public IHttpActionResult GetFavoriteCandidate(string userId)
        {
            var result = userProcessor.GetFavoriteCandidate(Convert.ToInt32(userId));
            return Ok(result);
        }


        [HttpPost]
        [Route("SaveUserCriteria")]
        public IHttpActionResult SaveUserCriteria(User user)
        {
            var result = userProcessor.SaveUserCriteria(user);
            return Ok(result);
        }

        [HttpPost]
        [Route("SaveIsFavorite")]
        public IHttpActionResult SaveIsFavorite(string userId, string emailId, bool isFavorite)
        {
            return Ok(userProcessor.SaveIsFavoriteCandidate(userId, emailId, isFavorite));
        }
    }
}
