using FYP.Models.Dashboard;
using Microsoft.AspNetCore.Identity;
using Models;
using Models.Dashboard;

namespace Encapsulation
{
    public interface Interface
    {
        public Task<IdentityResult> signup(SignUpModel signUp);
        public Task<SignInResult> SignInAsync(SignInModel signIn);
        public Task signout();
        public Task AddPhotoAsync(string filename, string id);
        public Task SignInInfoUpdateAsync(string id, SignUpModel signUp);
        public Task AddressInfoUpdateAsync(string id, SignUpModel signUp);
        public Task ContactInfoUpdateAsync(string id, SignUpModel signUp);
        public Task BasicInfoUpdateAsync(string id, SignUpModel signUp);
        public Task<int> Create_News_Event(news_events news_Events);
        public Task<List<news_events>> Event_List();
        public Task<news_events> findEventAsync(int? id);
        public news_events UpdateEvent(news_events news_Events);
        public Task DeleteEvent(news_events news_Events);
        public Task<int> AddLink(Links Link);
        public Task<int> DelLink(int? id);
        public Task<int> AddAssignmet(Assignments assignments);
        public Task<int> DelAssignment(int? id);
        public Task<string> getUserNameByid(string id);
        public Assignments UpdateAssignment(Assignments Assignment);
        public Task<int> AddNote(Notes notes);
        public Notes UpdateNote(Notes notes);
        public Task<int> DelNote(int? id);

    }
}