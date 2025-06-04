using BaiTapGTSC.Models;

namespace BaiTapGTSC
{
    class Program
    {
        static void Main(string[] args)
        {
            using var db = new AppDbContext();
            Console.WriteLine("Chon chuc nang:");
            Console.WriteLine("1. Them nhan vien");
            Console.WriteLine("2. Sua nhan vien");
            Console.WriteLine("3. Xoa nhan vien");

            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    ThemNhanVien(db);
                    break;
                case "2":
                    SuaNhanVien(db);
                    break;
                case "3":
                    XoaNhanVien(db);
                    break;
            }
        }

        static void ThemNhanVien(AppDbContext db)
        {
            Console.Write("Nhap ma: ");
            string ma = Console.ReadLine();
            Console.Write("Nhap ten: ");
            string ten = Console.ReadLine();
            Console.Write("Nhap ngay sinh (yyyy-MM-dd): ");
            DateOnly.TryParse(Console.ReadLine(), out DateOnly ngaySinh);

            var nv = new NhanVien
            {
                Ma = ma,
                Ten = ten,
                NgaySinh = ngaySinh
            };

            db.NhanViens.Add(nv);
            db.SaveChanges();
            Console.WriteLine("Da them thanh cong.");
        }

        static void SuaNhanVien(AppDbContext db)
        {
            Console.Write("Id: ");
            int.TryParse(Console.ReadLine(), out int id);

            var nv = db.NhanViens.Find(id);
            if (nv == null)
            {
                Console.WriteLine("Not found.");
                return;
            }

            Console.WriteLine($"Ma: {nv.Ma}, Ten: {nv.Ten}, Ngay sinh: {nv.NgaySinh:yyyy-MM-dd}");
            Console.Write("Nhap ten moi: ");
            string tenMoi = Console.ReadLine();

            nv.Ten = tenMoi;
            db.SaveChanges();
            Console.WriteLine("Successful.");
        }

        static void XoaNhanVien(AppDbContext db)
        {
            Console.Write("Id: ");
            int.TryParse(Console.ReadLine(), out int id);

            var nv = db.NhanViens.Find(id);
            if (nv == null)
            {
                Console.WriteLine("not found.");
                return;
            }

            Console.Write("Ban co chac chan (Yes/No): ");
            string confirm = Console.ReadLine();
            if (confirm?.ToLower() == "yes")
            {
                db.NhanViens.Remove(nv);
                db.SaveChanges();
                Console.WriteLine("Successfully.");
            }
            else
            {
                Console.WriteLine("Cancelled.");
            }
        }
    }

}
