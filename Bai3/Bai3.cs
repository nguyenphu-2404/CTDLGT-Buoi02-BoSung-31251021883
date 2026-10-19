using System;
using System.Collections.Generic;

namespace Bai3
{
    // Lớp Student lưu trữ thông tin cá nhân và điểm số của từng sinh viên
    public class Student
    {
        // Thuộc tính Id lưu mã sinh viên dùng để định danh duy nhất
        public string Id { get; set; } = "";

        // Thuộc tính FullName lưu họ và tên của sinh viên
        public string FullName { get; set; } = "";

        // Thuộc tính Score lưu điểm tổng kết trong đoạn từ 0.0 đến 10.0
        public double Score { get; set; }

        // Ghi đè phương thức ToString để trả về chuỗi thông tin định dạng sẵn
        public override string ToString()
        {
            // Trả về chuỗi gồm Id, FullName và Score đã được căn chỉnh độ rộng đẹp mắt
            return $"ID: {Id,-10} | Name: {FullName,-20} | Score: {Score,5:F2}";
        }
    }

    class Program
    {
        // Danh sách động List<Student> lưu trữ toàn bộ sinh viên trong bộ nhớ
        static List<Student> students = new List<Student>();

        // Điểm khởi chạy chính của chương trình C#
        static void Main3(string[] args)
        {
            // Nạp dữ liệu giả lập ban đầu để kiểm thử chương trình (5 sinh viên mẫu)
            SeedData();

            // Vòng lặp vô hạn duy trì menu điều khiển liên tục
            while (true)
            {

                // In các tùy chọn chức năng cho người dùng chọn
                Console.WriteLine("1. Add Student");
                Console.WriteLine("2. Update Student by ID");
                Console.WriteLine("3. Delete Student by ID");
                Console.WriteLine("4. Search Student by Name");
                Console.WriteLine("5. Display List & Statistics");
                Console.WriteLine("0. Exit");

                // Yêu cầu người dùng nhập lựa chọn
                Console.Write("Choose an option (0-5): ");

                // Đọc lựa chọn từ bàn phím và loại bỏ khoảng trắng dư thừa
                string choice = Console.ReadLine()?.Trim() ?? "";

                // In dòng trống tạo khoảng cách thẩm mỹ
                Console.WriteLine();

                // Kiểm tra lựa chọn và chuyển hướng thực thi phương thức tương ứng
                switch (choice)
                {
                    // Người dùng chọn 1: Gọi hàm thêm sinh viên
                    case "1": AddStudent(); break;

                    // Người dùng chọn 2: Gọi hàm cập nhật sinh viên
                    case "2": UpdateStudent(); break;

                    // Người dùng chọn 3: Gọi hàm xóa sinh viên
                    case "3": DeleteStudent(); break;

                    // Người dùng chọn 4: Gọi hàm tìm kiếm sinh viên theo tên
                    case "4": SearchStudent(); break;

                    // Người dùng chọn 5: Gọi hàm hiển thị danh sách và thống kê
                    case "5": DisplayAll(); break;

                    // Người dùng chọn 0: Dừng hàm Main để kết thúc chương trình
                    case "0": return;

                    // Trường hợp nhập ngoài từ 0 đến 5: Thông báo nhập không hợp lệ
                    default: Console.WriteLine("Invalid option! Please try again."); break;
                }
            }
        }

        // Phương thức khởi tạo dữ liệu mẫu ban đầu gồm 5 sinh viên
        static void SeedData()
        {
            // Thêm sinh viên mẫu thứ nhất
            students.Add(new Student { Id = "ST001", FullName = "Nguyen Van An", Score = 8.5 });

            // Thêm sinh viên mẫu thứ hai
            students.Add(new Student { Id = "ST002", FullName = "Tran Thi Binh", Score = 4.2 });

            // Thêm sinh viên mẫu thứ ba
            students.Add(new Student { Id = "ST003", FullName = "Than Thi Det", Score = 9.0 });

            // Thêm sinh viên mẫu thứ tư
            students.Add(new Student { Id = "ST004", FullName = "Nguyen Van Teo", Score = 3.5 });

            // Thêm sinh viên mẫu thứ năm
            students.Add(new Student { Id = "ST005", FullName = "Vo Hoang Yen", Score = 6.8 });
        }

        // Phương thức hỗ trợ tìm kiếm tuyến tính sinh viên theo mã Id (không dùng LINQ)
        static Student FindById(string id)
        {
            // Duyệt từng đối tượng sinh viên trong danh sách List<Student> với độ phức tạp O(n)
            foreach (var s in students)
            {
                // So sánh mã sinh viên không phân biệt chữ hoa chữ thường
                if (s.Id.Equals(id, StringComparison.OrdinalIgnoreCase))
                {
                    // Trả về đối tượng sinh viên khi tìm thấy mã trùng khớp
                    return s;
                }
            }

            // Trả về null nếu duyệt hết danh sách mà không thấy mã khớp
            return null;
        }

        // Phương thức thêm sinh viên mới có kiểm tra trùng mã và điểm hợp lệ
        static void AddStudent()
        {
            // Yêu cầu người dùng nhập mã sinh viên
            Console.Write("Enter Student ID: ");

            // Đọc mã sinh viên từ bàn phím và xóa khoảng trắng ở hai đầu
            string id = Console.ReadLine()?.Trim() ?? "";

            // Kiểm tra nếu mã sinh viên đã tồn tại bằng hàm FindById (độ phức tạp O(n))
            if (FindById(id) != null)
            {
                // Thông báo lỗi nếu mã đã tồn tại và thoát phương thức
                Console.WriteLine("ERROR: Student ID already exists!");
                return;
            }

            // Yêu cầu người dùng nhập họ tên
            Console.Write("Enter Full Name: ");

            // Đọc tên sinh viên và cắt khoảng trắng thừa
            string name = Console.ReadLine()?.Trim() ?? "";

            // Yêu cầu người dùng nhập điểm tổng kết
            Console.Write("Enter Score (0.0 - 10.0): ");

            // Chuyển đổi chuỗi sang double và kiểm tra điểm nằm trong phạm vi từ 0 đến 10
            if (!double.TryParse(Console.ReadLine(), out double score) || score < 0 || score > 10)
            {
                // Thông báo lỗi khi điểm nhập vào không hợp lệ và thoát
                Console.WriteLine("ERROR: Invalid score! Score must be between 0.0 and 10.0.");
                return;
            }

            // Thêm sinh viên mới vào cuối danh sách List<Student>
            students.Add(new Student { Id = id, FullName = name, Score = score });

            // Thông báo thêm sinh viên thành công
            Console.WriteLine("Student added successfully!");
        }

        // Phương thức cập nhật thông tin sinh viên theo mã Id
        static void UpdateStudent()
        {
            // Yêu cầu người dùng nhập mã sinh viên cần sửa
            Console.Write("Enter Student ID to update: ");

            // Đọc mã nhập vào và cắt khoảng trắng
            string id = Console.ReadLine()?.Trim() ?? "";

            // Tìm sinh viên tương ứng trong danh sách
            Student s = FindById(id);

            // Kiểm tra nếu sinh viên không tồn tại
            if (s == null)
            {
                // Thông báo lỗi không tìm thấy sinh viên và dừng phương thức
                Console.WriteLine("ERROR: Student ID not found!");
                return;
            }

            // Nhập họ tên mới, giải thích rõ nếu để trống sẽ giữ nguyên tên cũ
            Console.Write($"Enter new Full Name (press Enter to keep '{s.FullName}'): ");

            // Đọc chuỗi họ tên mới
            string newName = Console.ReadLine()?.Trim() ?? "";

            // Nếu người dùng có nhập tên mới thì tiến hành cập nhật
            if (!string.IsNullOrEmpty(newName))
            {
                // Cập nhật thuộc tính FullName của đối tượng sinh viên
                s.FullName = newName;
            }

            // Nhập điểm mới, giải thích rõ nếu để trống sẽ giữ nguyên điểm cũ
            Console.Write($"Enter new Score (press Enter to keep '{s.Score}'): ");

            // Đọc chuỗi điểm mới
            string scoreInput = Console.ReadLine()?.Trim() ?? "";

            // Nếu người dùng có nhập điểm mới thì tiến hành kiểm tra và gán
            if (!string.IsNullOrEmpty(scoreInput))
            {
                // Kiểm tra chuỗi điểm mới có là số hợp lệ và nằm trong [0, 10]
                if (double.TryParse(scoreInput, out double newScore) && newScore >= 0 && newScore <= 10)
                {
                    // Cập nhật thuộc tính Score của sinh viên
                    s.Score = newScore;
                }
                else
                {
                    // Thông báo điểm nhập sai và giữ lại điểm cũ
                    Console.WriteLine("Invalid score input! Keeping old score.");
                }
            }

            // Thông báo cập nhật thành công
            Console.WriteLine("Student updated successfully!");
        }

        // Phương thức xóa sinh viên khỏi danh sách theo mã Id
        static void DeleteStudent()
        {
            // Yêu cầu người dùng nhập mã sinh viên cần xóa
            Console.Write("Enter Student ID to delete: ");

            // Đọc mã nhập vào
            string id = Console.ReadLine()?.Trim() ?? "";

            // Tìm sinh viên cần xóa theo mã Id
            Student s = FindById(id);

            // Kiểm tra nếu không tìm thấy sinh viên
            if (s == null)
            {
                // In thông báo lỗi không tìm thấy và kết thúc hàm
                Console.WriteLine("ERROR: Student ID not found!");
                return;
            }

            // Xóa đối tượng sinh viên khỏi List<Student> (Thao tác xóa dịch mảng O(n))
            students.Remove(s);

            // Thông báo xóa thành công
            Console.WriteLine("Student deleted successfully!");
        }

        // Phương thức tìm kiếm sinh viên theo một phần họ tên (không dùng LINQ)
        static void SearchStudent()
        {
            // Yêu cầu người dùng nhập từ khóa họ tên
            Console.Write("Enter name keyword to search: ");

            // Đọc từ khóa tìm kiếm
            string keyword = Console.ReadLine()?.Trim() ?? "";

            // In tiêu đề khu vực kết quả tìm kiếm
            Console.WriteLine("\n--- SEARCH RESULTS ---");

            // Đặt cờ đánh dấu kết quả tìm kiếm
            bool found = false;

            // Duyệt từng phần tử của danh sách bằng vòng lặp for thủ công (không dùng LINQ)
            for (int i = 0; i < students.Count; i++)
            {
                // So sánh từ khóa chứa trong FullName không phân biệt hoa thường
                if (students[i].FullName.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    // In ra thông tin sinh viên khớp với từ khóa
                    Console.WriteLine(students[i]);

                    // Đánh dấu là đã tìm thấy ít nhất 1 kết quả
                    found = true;
                }
            }

            // Kiểm tra nếu duyệt hết danh sách mà found vẫn là false
            if (!found)
            {
                // Thông báo không tìm thấy sinh viên nào khớp từ khóa
                Console.WriteLine("No matching students found!");
            }
        }

        // Phương thức hiển thị danh sách, điểm trung bình và đếm sinh viên đạt
        static void DisplayAll()
        {
            // Kiểm tra trường hợp biên danh sách sinh viên bị rỗng
            if (students.Count == 0)
            {
                // In thông báo danh sách trống để tránh lỗi chia cho 0
                Console.WriteLine("Student list is empty!");
                return;
            }

            // In tiêu đề danh sách sinh viên
            Console.WriteLine("--- STUDENT LIST ---");

            // Khởi tạo biến tổng điểm để tích lũy
            double totalScore = 0;

            // Khởi tạo biến đếm số lượng sinh viên đạt (điểm >= 5.0)
            int passCount = 0;

            // Duyệt qua từng sinh viên s trong danh sách List<Student>
            foreach (var s in students)
            {
                // In thông tin chi tiết của sinh viên s
                Console.WriteLine(s);

                // Cộng dồn điểm của sinh viên s vào tổng điểm totalScore
                totalScore += s.Score;

                // Kiểm tra nếu điểm số lớn hơn hoặc bằng 5.0
                if (s.Score >= 5.0)
                {
                    // Tăng số lượng sinh viên đạt lên 1
                    passCount++;
                }
            }

            // In dòng phân cách báo cáo thống kê
            Console.WriteLine("------------------------------------------");

            // In tổng số lượng sinh viên có trong danh sách
            Console.WriteLine($"Total Students : {students.Count}");

            // In điểm trung bình làm tròn 2 chữ số thập phân
            Console.WriteLine($"Average Score  : {totalScore / students.Count:F2}");

            // In số lượng sinh viên đạt (điểm >= 5.0)
            Console.WriteLine($"Passed Students: {passCount}");
        }
    }
}