using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

// Services DTO - v1
namespace Backend_Android_2024.Models.ServicesDTOModel
{

    namespace Backend_Android_2024.Models.DTOModel
    {
        public class ProductServiceDTO
        {
            public int IDSP { get; set; }
            public string TenSP { get; set; }
            public int GiaBan { get; set; }
            public int GiaMua { get; set; }
            public int SoLuong { get; set; }
            public string MoTa { get; set; }
            public int IDKichThuoc { get; set; }

            // thương hiệu
            public int IDThuongHieu { get; set; }
            public string TenThuongHieu { get; set; }

            // loại sản phẩm
            public int IDLoai { get; set; }
            public string TenLoai { get; set; }

            // màu sắc
            public int IDMau { get; set; }
            public string TenMau { get; set; }

            // trạng thái sản phẩm
            public int IDTrangThai { get; set; }
            public string TenTrangThai { get; set; }

            // nhân viên
            public int IDNV { get; set; }
            public string TenNV { get; set; }
        }
    }

}