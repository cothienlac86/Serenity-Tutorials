using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MovieTutorial.Models
{
    public class UserModels
    {
        public int UserId { get; set; }
        public string RoleName { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public bool isActived { get; set; }
        public List<UserModels> ListsUser2ndRole { get; set; }
        public UserModels()
        {
            UserId = 0;
            RoleName = string.Empty;
            UserName = string.Empty;
            FullName = string.Empty;
            PhoneNumber = string.Empty;
            Email = string.Empty;

            Address = string.Empty;
            isActived = false;
            ListsUser2ndRole = new List<UserModels>();
        }
    }

    public class LoginUserModel
    {
        public LoginUserModel()
        {
            PhoneNumber = String.Empty;
            Password = String.Empty;
            Permission = String.Empty;
            Status = String.Empty;
            FullName = String.Empty;
            UserId = 0;
            Remember = false;
        }

        [Required(ErrorMessage = "Số điện thoại không được để trống")]
        [StringLength(50, MinimumLength = 10, ErrorMessage = "Số điện thoại phải lớn hơn 10 kí tự")]
        public String PhoneNumber { get; set; }
        [Required(ErrorMessage = "Password không được để trống")]
        public String Password { get; set; }
        public int UserId { get; set; }
        public String FullName { get; set; }
        public String Permission { get; set; }
        public String Status { get; set; }
        public bool Remember { get; set; }
    }

    public class RegisterUserModel
    {
        public RegisterUserModel()
        {
            Id = 0;
            FullName = String.Empty;
            PhoneNumber = String.Empty;
            UserName = String.Empty;
            Email = String.Empty;
            Address = String.Empty;
            Password = String.Empty;
            RePassword = String.Empty;
        }

        public int Id { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        [Required(ErrorMessage = "Số điện thoại không được để trống")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Số điện thoại chỉ chứ ký tự số")]
        [StringLength(50, MinimumLength = 10, ErrorMessage = "Số điện thoại phải lớn hơn 10 kí tự")]
        [Remote("CheckPhoneNumber", "User")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Email không được để trống")]
        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "Định dạng email không hợp lệ")]
        public string Email { get; set; }
        public string Address { get; set; }
        [Required(ErrorMessage = "Password không được để trống")]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Password phải từ 5 đến 50 kí tự")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Nhập lại Password không được để trống")]
        [System.Web.Mvc.CompareAttribute("Password", ErrorMessage = "Mật khẩu nhập lại không khớp")]
        public string RePassword { get; set; }
    }

    public class ChangePasswordModel
    {
        public ChangePasswordModel()
        {
            NewPassword = String.Empty;
            ReNewPassword = String.Empty;
            PhoneNumber = String.Empty;
            UserId = 0;
        }

        [Required(ErrorMessage = "Password mới không được để trống")]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Password mới phải từ 5 đến 50 kí tự")]
        public string NewPassword { get; set; }
        [System.Web.Mvc.CompareAttribute("NewPassword", ErrorMessage = "Mật khẩu mới nhập lại không khớp")]
        public string ReNewPassword { get; set; }
        public int UserId { get; set; }
        public String PhoneNumber { get; set; }
    }

    public class InfoUserModel
    {
        public InfoUserModel()
        {
            Id = 0;
            FullName = String.Empty;
            UserName = String.Empty;
            Email = String.Empty;
            Address = String.Empty;
        }

        public int Id { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "Định dạng email không hợp lệ")]
        public string Email { get; set; }
        public string Address { get; set; }
    }

    public class RoleModel
    {
        [Required(ErrorMessage = "Enter Role name")]
        //[StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        public string RoleName { get; set; }
    }

    public class AssignRoleVM
    {

        [Required(ErrorMessage = "Enter Role Name")]
        public string RoleName { get; set; }
        [Required(ErrorMessage = "Enter User name")]
        public string UserName { get; set; }
        public List<SelectListItem> Userlist { get; set; }
        public List<SelectListItem> RolesList { get; set; }

    }

    public class User2ndRole
    {


    }
}