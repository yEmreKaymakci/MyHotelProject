﻿using System.ComponentModel.DataAnnotations;

namespace HotelProject.WebUI.Dtos.RegisterDto
{
    public class CreateNewUserDto
    {
        [Required(ErrorMessage = "Ad Alanı Gereklidir")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Soyad Alanı Gereklidir")]
        public string SurName { get; set; }

        [Required(ErrorMessage = "Kullanıcı Adı Alanı Gereklidir")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Mail Alanı Gereklidir")]
        public string Mail { get; set; }

        [Required(ErrorMessage = "Şifre Alanı Gereklidir")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Şifre Tekrar Alanı Gereklidir")]
        [Compare("Password", ErrorMessage = "Şifreler Uyuşmuyor!")]
        public string ConfirmPassword { get; set; }
    }
}
