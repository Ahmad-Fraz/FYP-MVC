﻿using Microsoft.AspNetCore.Identity;
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

    }
}