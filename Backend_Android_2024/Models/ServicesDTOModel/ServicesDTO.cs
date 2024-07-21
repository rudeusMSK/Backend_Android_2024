using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

// Services DTO - v1
namespace Backend_Android_2024.Models.ServicesDTOModel
{

    namespace Backend_Android_2024.Models.DTOModel
    {
        // Service: Show Product Card:
        public class ProductCartItemDTO
        {
            public int IDSP { get; set; }
            public string TenSP { get; set; }
            public string Img_Url { get; set; }
            public int GiaBan { get; set; }

        }


        public class LoginUser
        {
            public string UserName { get; set; }
            public string Password { get; set; }
        }

    }

}