
using DataBase;
using Microsoft.AspNetCore.Identity;
using Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System.Runtime.InteropServices;
using Models.Dashboard;
using Microsoft.EntityFrameworkCore;
using FYP.Models.Dashboard;

namespace Encapsulation
{
    public class InterfaceImplementaion : Interface
    {
        private readonly DBase dBase;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly UserManager<ApplicationUser> userManager;

        public InterfaceImplementaion(DBase dBase, SignInManager<ApplicationUser> _signInManager, UserManager<ApplicationUser> _userManager)
        {
            this.dBase = dBase;
            signInManager = _signInManager;
            userManager = _userManager;
        }

        public async Task<SignInResult> SignInAsync(SignInModel signIn)
        {
            var result = await signInManager.PasswordSignInAsync(signIn.Email, signIn.password, signIn.keepMeSignedIn, false);
            return result;
        }

        public async Task<IdentityResult> signup(SignUpModel signUp)
        {

            var user = new ApplicationUser()
            {
                UserName = signUp.Email,
                Email = signUp.Email,
                PhoneNumber = signUp.PhoneNo,
                Degree = signUp.Degree,
                Member_Type = signUp.Member_Type,
                FullName = signUp.Name,
                DOB = signUp.DOB,
                Gender = signUp.Gender,
                City_Name = signUp.City_Name,
                Home_Address = signUp.Home_Address,
                Profile_Photo_Path = signUp.Profile_Photo_Path
            };

            var result = await userManager.CreateAsync(user, signUp.password);
            return result;
        }

        public async Task<string> getUserNameByid(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            var username = user.FullName;
            return username;
        }

        public async Task signout()
        {
            await signInManager.SignOutAsync();
        }


        public async Task AddPhotoAsync(string filename, string id)
        {

            ApplicationUser? user = await userManager.FindByIdAsync(id);
            user.Profile_Photo_Path = filename;
            await userManager.UpdateAsync(user);

        }

        // Update Basic Info || Home
        public async Task BasicInfoUpdateAsync(string id, SignUpModel signUp)
        {

            ApplicationUser? user = await userManager.FindByIdAsync(id);
            user.FullName = signUp.Name;
            user.Gender = signUp.Gender;
            await userManager.UpdateAsync(user);

        }

        // Update Contact Info || Home
        public async Task ContactInfoUpdateAsync(string id, SignUpModel signUp)
        {

            ApplicationUser? user = await userManager.FindByIdAsync(id);
            user.Email = signUp.Email;
            user.PhoneNumber = signUp.PhoneNo;
            await userManager.UpdateAsync(user);

        }
        // Update Address Info || Home
        public async Task AddressInfoUpdateAsync(string id, SignUpModel signUp)
        {

            ApplicationUser? user = await userManager.FindByIdAsync(id);
            user.Home_Address = signUp.Home_Address;
            user.City_Name = signUp.City_Name;
            await userManager.UpdateAsync(user);

        }

        // Update Basic Info || Home
        public async Task SignInInfoUpdateAsync(string id, SignUpModel signUp)
        {

            ApplicationUser? user = await userManager.FindByIdAsync(id);
            user.Two_step_Verification_Phone = signUp.Two_step_Verification_Phone;
            user.Recovery_Email = signUp.Recovery_Email;
            await userManager.UpdateAsync(user);

        }

        public async Task<int> Create_News_Event(news_events news_Events)
        {
            _ = dBase.News_Events.AddAsync(news_Events);
            var result = await dBase.SaveChangesAsync();
            return result;
        }

        public async Task<news_events> findEventAsync(int? id)
        {
            return await dBase.News_Events.FindAsync(id);

        }

        

        public news_events UpdateEvent(news_events news_Events)

        {
            var _event = dBase.News_Events.Attach(news_Events);

            _event.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            dBase.SaveChanges();
            return news_Events;
        }

        public async Task DeleteEvent(news_events news_Events)
        {
            dBase.News_Events.Remove(news_Events);
            await dBase.SaveChangesAsync();
        }

        public async Task<int> AddLink(Links Link)
        {
            _ = dBase.Links.AddAsync(Link);
            var result = await dBase.SaveChangesAsync();
            return result;
        }

        public async Task<int> DelLink(int? id)
        {
            var link = dBase.Links.Where(x => x.id == id).FirstOrDefault();
            dBase.Links.Remove(link);
            var result = await dBase.SaveChangesAsync();
            return result;
        }
        
        public async Task<int> AddAssignmet(Assignments assignments)
        {
            
            _ = dBase.Assignments.AddAsync(assignments);
           
            var result = await dBase.SaveChangesAsync();
            return result;
        }

        public Assignments UpdateAssignment(Assignments Assignment)

        {
            var _assignment = dBase.Assignments.Attach(Assignment);

            _assignment.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            dBase.SaveChanges();
            return Assignment;
        }

        public async Task<int> DelAssignment(int? id)
        {
            var assignments = dBase.Assignments.Where(x => x.id == id).FirstOrDefault();
            dBase.Assignments.Remove(assignments);
            var result = await dBase.SaveChangesAsync();
            return result;
        }



        //Notes Section Starts Here

        public async Task<int> AddNote(Notes notes)
        {

            _ = dBase.MyNotes.AddAsync(notes);

            var result = await dBase.SaveChangesAsync();
            return result;
        }

        public Notes UpdateNote(Notes notes)

        {
            var note = dBase.MyNotes.Attach(notes);

            note.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            dBase.SaveChanges();
            return notes;
        }
        public async Task<int> DelNote(int? id)
        {
            var Notes = dBase.MyNotes.Where(x => x.id == id).FirstOrDefault();
            dBase.MyNotes.Remove(Notes);
            var result = await dBase.SaveChangesAsync();
            return result;
        }

        public async Task<int> DelQuiz(int? id)
        {
            var quizz = dBase.Quizzs.Where(x => x.id == id).FirstOrDefault();
            dBase.Quizzs.Remove(quizz);
            var result = await dBase.SaveChangesAsync();
            return result;
        }

    }
}